using Labs.Core.Scheme;

namespace Labs.Core.Filtering
{
    public static class FilterModule
    {
        public static ChannelModule<ARGB, ARGB.Channel> ARGB(ARGB.Channel channel, double sharpness) => new()
        {
            Summator = Transformers.Summator<ARGB, ARGB.Channel>(channel),
            LaplacianSharpness = Transformers.ARGBLaplacianSummator(channel, sharpness),
            MedianReducer = Reducers.ARGBMedianReducer(channel),
            MinMaxReducer = Reducers.ARGBMinMaxReducer(channel)
        };

        public static ChannelModule<HLSA, HLSA.Channel> HLSA(HLSA.Channel channel, double sharpness) => new()
        {
            Summator = Transformers.HLSASummator(channel),
            LaplacianSharpness = null, //Reducers.HLSA(channel, laplacian, sharpness),
            MedianReducer = Reducers.HLSAMedianReducer(channel),
            MinMaxReducer = Reducers.HLSAMinMaxReducer(channel)
        };

        public static ChannelModule<YUV, YUV.Channel> YUV(YUV.Channel channel, double sharpness) => new()
        {
            Summator = Transformers.YUVSummator(channel),
            LaplacianSharpness = null, //Reducers.HLSA(channel, laplacian, sharpness),
            MedianReducer = Reducers.YUVMedianReducer(channel),
            MinMaxReducer = Reducers.YUVMinMaxReducer(channel)
        };
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

        public static ConvolutionModule<HLSA, HLSA.Channel> HLSA(ImageBuffer<HLSA> input, HLSA.Channel channels, double[,] kernel, double sharpness) => new();
        public static ConvolutionModule<YUV, YUV.Channel> YUV(ImageBuffer<YUV> input, YUV.Channel channels, double[,] kernel, double sharpness) => new();
    }
}