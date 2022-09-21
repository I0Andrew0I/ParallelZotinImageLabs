using System;
using System.Threading.Tasks;
using Labs.Core.Scheme;

namespace Labs.Core.Filtering
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
}