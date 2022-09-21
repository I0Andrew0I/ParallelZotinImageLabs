using System;
using System.Threading.Tasks;
using Labs.Core.Filtering;
using Labs.Core.Scheme;

namespace Labs.Core
{
    public abstract record ConvolutionMethod<TPixel, TChannel>(in ImageBuffer<TPixel> Image, TChannel Channels)
        where TPixel : struct, IColor<TPixel, TChannel>
    {
        protected ArraySegment<TPixel> Pixels = Image.Pixels;

        public virtual void Apply(Frame f, ImageBuffer<TPixel> resultImage, int numThreads)
        {
            ParallelOptions po = new ParallelOptions {MaxDegreeOfParallelism = numThreads};
            int imageWidth = Image.Width;
            int imageHeight = Image.Height;
            int mWidth = f.Width;
            int mHeight = f.Height;
            int step = (int) Math.Truncate(imageHeight / 16.0);
            bool round = f is EllipsoidsFrame;

            Parallel.For(0, 16, po, (int iter) =>
            {
                int from = iter * step;
                int to = iter == 15 ? imageHeight : (iter + 1) * step;
                var output = resultImage.Pixels.AsSpan();
                Frame frame = round ? new EllipsoidsFrame(0, 0, mWidth, mHeight) : new Frame(0, 0, mWidth, mHeight);

                for (int y = from; y < to; y++)
                {
                    for (int x = 0; x < imageWidth; x++)
                    {
                        int pixelId = x + y * imageWidth;
                        frame.X = x;
                        frame.Y = y;

                        TPixel pixel = SlideFrame(f, output, pixelId);
                        pixel.Extract(Channels, ref output[pixelId]);
                    }
                }
            });
        }

        protected virtual TPixel SlideFrame(Frame f, Span<TPixel> output, int pixelId)
        {
            TPixel result = default;
            int iter = 0;
            foreach (int y0 in f.IterateY(f.X))
            {
                int y = Math.Clamp(y0, 0, Image.Height - 1);

                foreach (int x0 in f.IterateX(y))
                {
                    int x = Math.Clamp(x0, 0, Image.Width - 1);
                    result = result.Add(ApplyFrame(x, y, f, iter));
                    iter++;
                }
            }

            return result;
        }

        protected abstract TPixel ApplyFrame(int x, int y, Frame f, int iter);
    }

    public record MeanRecursiveConvolution<TPixel, TChannel>(in ImageBuffer<TPixel> Image, TChannel Channels, double[,] Kernel)
        : ConvolutionMethod<TPixel, TChannel>(Image, Channels)
        where TPixel : struct, IColor<TPixel, TChannel>
    {
        public virtual void Apply(Frame f, ImageBuffer<TPixel> resultImage, int numThreads)
        {
            ParallelOptions po = new ParallelOptions {MaxDegreeOfParallelism = numThreads};
            int imageWidth = Image.Width;
            int imageHeight = Image.Height;
            int mWidth = f.Width;
            int mHeight = f.Height;
            int step = (int) Math.Truncate(imageHeight / 16.0);
            bool round = f is EllipsoidsFrame;

            Parallel.For(0, 16, po, (int iter) =>
            {
                int from = iter * step;
                int to = iter == 15 ? imageHeight : (iter + 1) * step;
                var output = resultImage.Pixels.AsSpan();
                Frame frame = round ? new EllipsoidsFrame(0, 0, mWidth, mHeight) : new Frame(0, 0, mWidth, mHeight);

                for (int y = from; y < to; y++)
                {
                    TPixel rowSum = SlideFrame(f, output, y * imageWidth);
                    for (int x = 1; x < imageWidth; x++)
                    {
                        int pixelId = x + y * imageWidth;
                        frame.X = x;
                        frame.Y = y;
                        rowSum = SlideFrame(pixelId, rowSum, f, output);
                    }
                }
            });
        }

        private TPixel SlideFrame(int pixelId, TPixel rowSum, Frame f, Span<TPixel> output)
        {
            int from = f.X - f.RW;
            int to = f.X + f.RW;
            TPixel oldSum = default;
            TPixel newSum = default;

            foreach (int y0 in f.IterateY(from))
            {
                int y = Math.Clamp(y0, 0, Image.Height - 1);

                int matrixY = y0 + f.RH - f.Y;
                int matrixX = from + f.RW - f.X;
                int localId = from - 1 + y * Image.Width;

                oldSum = oldSum.Add(Image.Pixels[localId].Mul(Kernel[matrixY, matrixX]));
            }

            foreach (int y0 in f.IterateY(to))
            {
                int y = Math.Clamp(y0, 0, Image.Height - 1);

                int matrixY = y0 + f.RH - f.Y;
                int matrixX = to + f.RW - f.X;
                int localId = to + y * Image.Width;

                newSum = newSum.Add(Image.Pixels[localId].Mul(Kernel[matrixY, matrixX]));
            }

            return rowSum.Subtract(oldSum).Add(newSum);
        }

        protected override TPixel ApplyFrame(int x, int y, Frame f, int iter) =>
            default;
    }

    public record LaplacianConvolution<TPixel, TChannel>(in ImageBuffer<TPixel> Image, TChannel Channels, double[,] Kernel, double sharpness)
        : ConvolutionMethod<TPixel, TChannel>(Image, Channels)
        where TPixel : struct, IColor<TPixel, TChannel>
    {
        protected override TPixel SlideFrame(Frame f, Span<TPixel> _, int pixelId)
        {
            TPixel sum = default;
            foreach (int y0 in f.IterateY(f.X))
            {
                int y = Math.Clamp(y0, 0, Image.Height - 1);

                foreach (int x0 in f.IterateX(y))
                {
                    int x = Math.Clamp(x0, 0, Image.Width - 1);

                    int matrixY = y0 + f.RH - f.Y;
                    int matrixX = x0 + f.RW - f.X;
                    int localId = x + y * Image.Width;

                    sum = sum.Add(Image.Pixels[localId].Mul(Kernel[matrixY, matrixX]));
                }
            }

            sum = sum.Mul(sharpness);
            return sum;
        }

        protected override TPixel ApplyFrame(int x, int y, Frame f, int iter) =>
            default;
    }

    public record CustomConvolution<TPixel, TChannel>(in ImageBuffer<TPixel> Image, TChannel Channels, FrameReducer<TPixel, TChannel> Reducer)
        : ConvolutionMethod<TPixel, TChannel>(Image, Channels)
        where TPixel : struct, IColor<TPixel, TChannel>
    {
        protected override TPixel ApplyFrame(int x, int y, Frame f, int iter) =>
            Reducer(Image.Pixels, f);
    }

    public record KernelConvolution<TPixel, TChannel>(in ImageBuffer<TPixel> Image, TChannel Channels, double[,] Kernel)
        : ConvolutionMethod<TPixel, TChannel>(Image, Channels)
        where TPixel : struct, IColor<TPixel, TChannel>
    {
        protected override TPixel ApplyFrame(int x, int y, Frame f, int iter)
        {
            int pixelId = x + y * Image.Width;
            int matrixY = iter / f.Width;
            int matrixX = iter % f.Width;
            TPixel pixel = Image.Pixels[pixelId];
            return pixel.Mul(Kernel[matrixY, matrixX]);
        }
    }
}