using Labs.Core.Scheme;

namespace Labs.Core.Filtering
{
    public record CustomConvolution<TPixel, TChannel>(in ImageBuffer<TPixel> Image, TChannel Channels, FrameReducer<TPixel, TChannel> Reducer)
        : ConvolutionMethod<TPixel, TChannel>(Image, Channels)
        where TPixel : struct, IColor<TPixel, TChannel>
    {
        protected override TPixel ApplyFrame(int x, int y, Frame f, int iter) =>
            Reducer(Image.Pixels, f);
    }
}