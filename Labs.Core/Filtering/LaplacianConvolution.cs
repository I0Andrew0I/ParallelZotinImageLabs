using System;
using Labs.Core.Scheme;

namespace Labs.Core.Filtering
{
    public sealed record LaplacianConvolution<TPixel, TChannel>(in ImageBuffer<TPixel> Image, TChannel Channels, double[,] Kernel, double Sharpness)
        : ConvolutionMethod<TPixel, TChannel>(Image, Channels)
        where TPixel : struct, IColor<TPixel, TChannel>
    {
        protected override TPixel SlideFrame(in Frame f, ref Span<TPixel> _, int pixelId)
        {
            TPixel sum = default;
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

                    if (K >= 0)
                    {
                        sum = sum.Add(Image.Pixels[localId].Mul(K, ref overflow1), ref overflow1);
                    }
                    else
                    {
                        Accumulator o1 = default;
                        sum = sum.Subtract(Image.Pixels[localId].Mul(Math.Abs(K), ref o1), ref overflow1);
                        overflow1 = overflow1.Subtract(o1);
                    }
                }
            }

            sum = sum.Mul(Sharpness).Correct(overflow1.Mul(Sharpness));
            sum = sum.Add(Image.Pixels[pixelId]);
            return sum;
        }
    }
}