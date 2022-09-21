using Labs.Core.Scheme;

namespace Labs.Core
{
    public class ConvolutionModule<TPixel, TChannel> where TPixel : struct, IColor<TPixel, TChannel>
    {
        public ConvolutionMethod<TPixel, TChannel> Linear;
        public ConvolutionMethod<TPixel, TChannel> MinMax;
        public ConvolutionMethod<TPixel, TChannel> Laplacian;
        public ConvolutionMethod<TPixel, TChannel> MeanRecursive;
        public ConvolutionMethod<TPixel, TChannel> Median;
    }
}