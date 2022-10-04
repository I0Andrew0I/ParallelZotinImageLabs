using System;
using Labs.Core.Scheme;

namespace Labs.Core.Filtering
{
    public sealed record LaplacianConvolution<TPixel, TChannel>(ImageBuffer<TPixel> Image, TChannel Channels, double[,] Kernel, double Sharpness)
        : ConvolutionMethod<TPixel, TChannel>(Image, Channels)
        where TPixel : struct, IColor<TPixel, TChannel>
    {
        protected override TPixel SlideFrame(in Frame f, ref Span<TPixel> _, int pixelId)
        {
            TPixel sum = default;
            TPixel source = Pixels[pixelId];
            (int yfrom, int yto) = f.IterateY(f.X);

            Accumulator overflow1 = default;
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
                    double K = Kernel[matrixY, matrixX];

                    TPixel mul = Pixels[localId].Mul(K * Sharpness, ref overflow1);
                    sum = sum.Add(ref mul, ref overflow1);
                }
            }

            sum = sum.Correct(ref overflow1);
            sum = sum.Add(ref source);
            return sum;
        }
    }
}