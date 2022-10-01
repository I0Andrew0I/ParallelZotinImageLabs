using System;
using System.Threading.Tasks;
using Labs.Core.Scheme;

namespace Labs.Core.Filtering
{
    public sealed record MeanRecursiveConvolution<TPixel, TChannel>(in ImageBuffer<TPixel> Image, TChannel Channels, double[,] Kernel)
        : KernelConvolution<TPixel, TChannel>(Image, Channels, Kernel)
        where TPixel : struct, IColor<TPixel, TChannel>
    {
        public override void Apply(Frame frameShape, ImageBuffer<TPixel> resultImage, int numThreads)
        {
            ParallelOptions po = new ParallelOptions {MaxDegreeOfParallelism = numThreads};
            int imageWidth = Image.Width;
            int imageHeight = Image.Height;
            int mWidth = frameShape.Width;
            int mHeight = frameShape.Height;
            bool round = frameShape is EllipsoidsFrame;
            int step = (int) Math.Truncate(imageHeight / 16.0);

            Parallel.For(0, 16, po, (int iter) =>
            {
                int from = iter * step;
                int to = iter == 15 ? imageHeight : (iter + 1) * step;
                var output = resultImage.Pixels.AsSpan();
                Frame frame = round ? new EllipsoidsFrame(0, 0, mWidth, mHeight) : new Frame(0, 0, mWidth, mHeight);

                for (int y = from; y < to; y++)
                {
                    frame.X = 0;
                    frame.Y = y;
                    int yOffset = y * imageWidth;
                    TPixel rowSum = SlideFrame(frame, ref output, yOffset);
                    rowSum.Extract(Channels, ref output[yOffset]);

                    for (int x = 1; x < imageWidth; x++)
                    {
                        int pixelId = x + yOffset;
                        frame.X = x;
                        frame.Y = y;
                        rowSum = SlideFrame(pixelId, rowSum, frame, ref output);
                        rowSum.Extract(Channels, ref output[pixelId]);
                    }
                }
            });
        }

        private TPixel SlideFrame(int pixelId, TPixel rowSum, Frame f, ref Span<TPixel> output)
        {
            int from = Math.Clamp(f.X - f.RW, 0, Image.Width - 1);
            int to = Math.Clamp(f.X + f.RW, 0, Image.Width - 1);
            TPixel oldSum = default;
            TPixel newSum = default;

            (int y1from, int y1to) = f.IterateY(from);
            for (int y0 = y1from; y0 <= y1to; y0++)
            {
                int y = Math.Clamp(y0, 0, Image.Height - 1);

                int matrixY = y0 + f.RH - f.Y;
                int matrixX = from + f.RW - f.X;
                int localId = Math.Clamp(from - 1, 0, Image.Width - 1) + y * Image.Width;

                oldSum = oldSum.Add(Image.Pixels[localId].Mul(Kernel[matrixY, matrixX]));
            }

            (int y2from, int y2to) = f.IterateY(to);
            for (int y0 = y2from; y0 <= y2to; y0++)
            {
                int y = Math.Clamp(y0, 0, Image.Height - 1);

                int matrixY = y0 + f.RH - f.Y;
                int matrixX = to + f.RW - f.X;
                int localId = to + y * Image.Width;

                newSum = newSum.Add(Image.Pixels[localId].Mul(Kernel[matrixY, matrixX]));
            }

            if (oldSum.CompareTo(newSum) == 0)
                return rowSum;

            rowSum = rowSum.Add(newSum, out var overflow);
            oldSum = oldSum.Subtract(overflow, out overflow);
            rowSum = rowSum.Add(overflow).Subtract(oldSum);
            return rowSum;
        }
    }
}