using System;
using Labs.Core.Scheme;

namespace Labs.Core.Filtering
{
    public record KernelConvolution<TPixel, TChannel>(ImageBuffer<TPixel> Image, TChannel Channels, double[,] Kernel)
        : ConvolutionMethod<TPixel, TChannel>(Image, Channels)
        where TPixel : struct, IColor<TPixel, TChannel>
    {
        protected override TPixel SlideFrame(in Frame f, ref Span<TPixel> output, int pixelId)
        {
            TPixel result = default;
            (int yfrom, int yto) = f.IterateY(f.X);

            for (int y0 = yfrom; y0 <= yto; y0++)
            {
                (int xfrom, int xto) = f.IterateX(y0);

                for (int x0 = xfrom; x0 <= xto; x0++)
                {
                    int y = Math.Clamp(y0, 0, Image.Height - 1);
                    int x = Math.Clamp(x0, 0, Image.Width - 1);

                    int matrixY = y0 + f.RH - f.Y;
                    int matrixX = x0 + f.RW - f.X;
                    int localId = x + y * Image.Width;

                    TPixel pixel = Image.Pixels[localId];
                    TPixel mul = pixel.Mul(Kernel[matrixY, matrixX]);
                    result = result.Add(ref mul);
                }
            }

            return result;
        }
    }
}