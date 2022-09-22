using System;
using Labs.Core.Scheme;

namespace Labs.Core.Filtering
{
    public record KernelConvolution<TPixel, TChannel>(in ImageBuffer<TPixel> Image, TChannel Channels, double[,] Kernel)
        : ConvolutionMethod<TPixel, TChannel>(Image, Channels)
        where TPixel : struct, IColor<TPixel, TChannel>
    {
        protected override TPixel SlideFrame(in Frame f, ref Span<TPixel> output, int pixelId)
        {
            TPixel result = default;
            foreach (int y0 in f.IterateY(f.X))
            {
                int y = Math.Clamp(y0, 0, Image.Height - 1);

                foreach (int x0 in f.IterateX(y))
                {
                    int x = Math.Clamp(x0, 0, Image.Width - 1);

                    int matrixY = y0 + f.RH - f.Y;
                    int matrixX = x0 + f.RW - f.X;
                    int localId = x + y * Image.Width;

                    TPixel pixel = Image.Pixels[localId];
                    result = result.Add(pixel.Mul(Kernel[matrixY, matrixX]));
                }
            }

            return result;
        }
    }
}