﻿using System;
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
            bool round = frameShape is EllipsisFrame;
            int step = (int) Math.Truncate(imageHeight / 16.0);

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
            Accumulator overflow1 = default;
            Accumulator overflow2 = default;

            (int y2from, int y2to) = f.IterateY(to);
            for (int y0 = y2from; y0 <= y2to; y0++)
            {
                int y = Math.Clamp(y0, 0, Image.Height - 1);

                int matrixY = y0 + f.RH - f.Y;

                int matrixX1 = from + f.RW - f.X;
                int matrixX2 = to + f.RW - f.X;

                int localId1 = Math.Clamp(from - 1, 0, Image.Width - 1) + y * Image.Width;
                int localId2 = to + y * Image.Width;

                newSum = newSum.Add(Image.Pixels[localId2].Mul(Kernel[matrixY, matrixX2], ref overflow1), ref overflow1);
                oldSum = oldSum.Add(Image.Pixels[localId1].Mul(Kernel[matrixY, matrixX1], ref overflow2), ref overflow2);
            }

            if (oldSum.CompareTo(newSum) == 0)
                return rowSum;

            overflow1 = overflow1.Subtract(overflow2);

            rowSum = rowSum.Add(newSum, ref overflow1);
            rowSum = rowSum.Subtract(oldSum, ref overflow1);
            rowSum = rowSum.Correct(overflow1);
            return rowSum;
        }
    }
}