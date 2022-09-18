using Labs.Core.Scheme;

namespace Labs.Core.Filtering
{
    public static class FilterModule
    {
        public static ChannelModule<ARGB> ARGB(ARGB.Channel channel, double[,] laplacian, double sharpness) => new()
        {
            Summator = Transformers.ARGBSummator(channel),
            LaplacianReducer = Reducers.ARGBLaplacianReducer(channel, laplacian, sharpness),
            MedianReducer = Reducers.ARGBMedianReducer(channel),
            MinMaxReducer = Reducers.ARGBMinMaxReducer(channel)
        };

        public static ChannelModule<HLSA> HLSA(HLSA.Channel channel, double[,] laplacian, double sharpness) => new()
        {
            Summator = Transformers.HLSASummator(channel),
            LaplacianReducer = null, //Reducers.HLSA(channel, laplacian, sharpness),
            MedianReducer = Reducers.HLSAMedianReducer(channel),
            MinMaxReducer = Reducers.HLSAMinMaxReducer(channel)
        };
        
        public static ChannelModule<YUV> YUV(YUV.Channel channel, double[,] laplacian, double sharpness) => new()
        {
            Summator = Transformers.YUVSummator(channel),
            LaplacianReducer = null, //Reducers.HLSA(channel, laplacian, sharpness),
            MedianReducer = Reducers.YUVMedianReducer(channel),
            MinMaxReducer = Reducers.YUVMinMaxReducer(channel)
        };
    }
}