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

            sum = sum.Mul(Sharpness);
            return sum;
        }
    }
}