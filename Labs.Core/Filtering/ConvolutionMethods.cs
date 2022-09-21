using Labs.Core.Scheme;

namespace Labs.Core.Filtering
{
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

        public static ConvolutionModule<HLSA, HLSA.Channel> HLSA(ImageBuffer<HLSA> input, HLSA.Channel channels, double[,] kernel, double sharpness) => new();
        public static ConvolutionModule<YUV, YUV.Channel> YUV(ImageBuffer<YUV> input, YUV.Channel channels, double[,] kernel, double sharpness) => new();
    }
}