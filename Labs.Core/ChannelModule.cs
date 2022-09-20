using Labs.Core.Filtering;
using Labs.Core.Scheme;

namespace Labs.Core
{
    public class ChannelModule<TPixel, TChannel> where TPixel : struct, IColor<TPixel, TChannel>
    {
        public PixelTransformer<TPixel, TChannel> Summator { get; set; }
        public FrameReducer<TPixel, TChannel> MinMaxReducer { get; set; }
        public FrameReducer<TPixel, TChannel> MedianReducer { get; set; }
        public PixelTransformer<TPixel, TChannel> LaplacianSharpness { get; set; }
    }
}