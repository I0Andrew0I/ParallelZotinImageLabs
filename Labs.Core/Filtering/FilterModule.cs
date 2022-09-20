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
}