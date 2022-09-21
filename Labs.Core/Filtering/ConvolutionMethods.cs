using Labs.Core.Scheme;

namespace Labs.Core.Filtering
{
    public class ConvolutionModule<TPixel, TChannel> where TPixel : struct, IColor<TPixel, TChannel>
    {
        public ConvolutionMethod<TPixel, TChannel> Linear;
        public ConvolutionMethod<TPixel, TChannel> MinMax;
        public ConvolutionMethod<TPixel, TChannel> Laplacian;
        public ConvolutionMethod<TPixel, TChannel> MeanRecursive;
        public ConvolutionMethod<TPixel, TChannel> Median;
    }

    public static class ConvolutionMethods
    {
        public static ConvolutionModule<ARGB, ARGB.Channel> ARGB(ImageBuffer<ARGB> input, ARGB.Channel channels, double[,] kernel, double sharpness) => new()
        {
            Linear = new KernelConvolution<ARGB, ARGB.Channel>(input, channels, kernel),
            Laplacian = new LaplacianConvolution<ARGB, ARGB.Channel>(input, channels, kernel, sharpness),
            Median = new CustomConvolution<ARGB, ARGB.Channel>(input, channels, Reducers.ARGBMedianReducer(channels)),
            MinMax = new CustomConvolution<ARGB, ARGB.Channel>(input, channels, Reducers.ARGBMinMaxReducer(channels)),
            MeanRecursive = new MeanRecursiveConvolution<ARGB, ARGB.Channel>(input, channels, kernel)
        };

        public static ConvolutionModule<HLSA, HLSA.Channel> HLSA(ImageBuffer<HLSA> input, HLSA.Channel channels, double[,] kernel, double sharpness) => new()
        {
            Linear = new KernelConvolution<HLSA, HLSA.Channel>(input, channels, kernel),
            Laplacian = new LaplacianConvolution<HLSA, HLSA.Channel>(input, channels, kernel, sharpness),
            Median = new CustomConvolution<HLSA, HLSA.Channel>(input, channels, Reducers.HLSAMedianReducer(channels)),
            MinMax = new CustomConvolution<HLSA, HLSA.Channel>(input, channels, Reducers.HLSAMinMaxReducer(channels)),
            MeanRecursive = new MeanRecursiveConvolution<HLSA, HLSA.Channel>(input, channels, kernel)
        };

        public static ConvolutionModule<YUV, YUV.Channel> YUV(ImageBuffer<YUV> input, YUV.Channel channels, double[,] kernel, double sharpness) => new()
        {
            Linear = new KernelConvolution<YUV, YUV.Channel>(input, channels, kernel),
            Laplacian = new LaplacianConvolution<YUV, YUV.Channel>(input, channels, kernel, sharpness),
            Median = new CustomConvolution<YUV, YUV.Channel>(input, channels, Reducers.YUVMedianReducer(channels)),
            MinMax = new CustomConvolution<YUV, YUV.Channel>(input, channels, Reducers.YUVMinMaxReducer(channels)),
            MeanRecursive = new MeanRecursiveConvolution<YUV, YUV.Channel>(input, channels, kernel)
        };
    }
}