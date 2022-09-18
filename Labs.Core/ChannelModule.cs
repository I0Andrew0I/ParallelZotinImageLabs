using Labs.Core.Filtering;

namespace Labs.Core
{
    public class ChannelModule<TPixel>
    {
        public PixelTransformer<TPixel> Summator { get; set; }
        public FrameReducer<TPixel> MinMaxReducer { get; set; }
        public FrameReducer<TPixel> MedianReducer { get; set; }
        public FrameReducer<TPixel> LaplacianReducer { get; set; }
    }
}