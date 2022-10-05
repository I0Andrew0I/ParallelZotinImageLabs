using System;
using System.Threading.Tasks;
using Labs.Core.Scheme;

namespace Labs.Core.Filtering
{
    public sealed record MeanRecursiveConvolution<TPixel, TChannel>(ImageBuffer<TPixel> Image, TChannel Channels, double[,] Kernel)
        : KernelConvolution<TPixel, TChannel>(Image, Channels, Kernel)
        where TPixel : struct, IColor<TPixel, TChannel>
    {
        public override void Apply(Frame frameShape, ImageBuffer<TPixel> resultImage, int numThreads)
        {
            ParallelOptions po = new ParallelOptions { MaxDegreeOfParallelism = numThreads };
            int imageWidth = Image.Width;
            int imageHeight = Image.Height;
            int mWidth = frameShape.Width;
            int mHeight = frameShape.Height;
            bool round = frameShape is EllipsisFrame;
            int step = (int)Math.Truncate(imageHeight / 16.0);

            Parallel.For(0, 16, po, (int iter) =>
            {
                int from = iter * step;
                int to = iter == 15 ? imageHeight : (iter + 1) * step;
                var output = resultImage.Pixels.AsSpan();
                Frame frame = round ? new EllipsisFrame(0, 0, mWidth, mHeight) : new Frame(0, 0, mWidth, mHeight);

                for (int y = from; y < to; y++)
                {
                    frame.X = 0;
                    frame.Y = y;
                    int yOffset = y * imageWidth;
                    TPixel rowSum = SlideFrame(frame, ref output, yOffset);
                    rowSum.Extract(Channels, ref output[yOffset]);
                    Accumulator recSum = rowSum.Convert();

                    for (int x = 1; x < imageWidth; x++)
                    {
                        int pixelId = x + yOffset;
                        frame.X = x;
                        frame.Y = y;
                        recSum = SlideFrame(pixelId, recSum, frame, ref output);
                        recSum.Convert<TPixel, TChannel>(out rowSum);
                        rowSum.Extract(Channels, ref output[pixelId]);
                    }
                }
            });
        }

        private Accumulator SlideFrame(int pixelId, Accumulator rowSum, Frame f, ref Span<TPixel> output)
        {
            int from = Math.Clamp(f.X - f.RW, 0, Image.Width - 1);
            int to = Math.Clamp(f.X + f.RW, 0, Image.Width - 1);
            Accumulator oldSum = default;
            Accumulator newSum = default;

            (int y2from, int y2to) = f.IterateY(to);
            for (int y0 = y2from; y0 <= y2to; y0++)
            {
                int y = Math.Clamp(y0, 0, Image.Height - 1);

                int matrixY = y0 + f.RH - f.Y;

                int matrixX1 = from + f.RW - f.X;
                int matrixX2 = to + f.RW - f.X;

                int localId1 = Math.Clamp(from - 1, 0, Image.Width - 1) + y * Image.Width;
                int localId2 = to + y * Image.Width;

                Accumulator nul1 = Pixels[localId1].Convert().Mul(Kernel[matrixY, matrixX1]);
                Accumulator mul2 = Pixels[localId2].Convert().Mul(Kernel[matrixY, matrixX2]);
                
                oldSum = oldSum.Add(ref nul1);
                newSum = newSum.Add(ref mul2);
            }

            if (oldSum.CompareTo(newSum) == 0)
                return rowSum;

            rowSum = rowSum.Add(ref newSum);
            rowSum = rowSum.Subtract(ref oldSum);
            return rowSum;
        }
    }
}