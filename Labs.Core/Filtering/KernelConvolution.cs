using Labs.Core.Scheme;

namespace Labs.Core.Filtering
{
    public sealed record KernelConvolution<TPixel, TChannel>(in ImageBuffer<TPixel> Image, TChannel Channels, double[,] Kernel)
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