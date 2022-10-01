using System;
using Labs.Core.Scheme;

namespace Labs.Core.Filtering
{
    public record CustomConvolution<TPixel, TChannel>(in ImageBuffer<TPixel> Image, TChannel Channels, FrameReducer<TPixel, TChannel> Reducer)
        : ConvolutionMethod<TPixel, TChannel>(Image, Channels)
        where TPixel : struct, IColor<TPixel, TChannel>
    {
        protected override TPixel SlideFrame(in Frame f, ref Span<TPixel> output, int pixelId)
        {
            ArraySegment<TPixel> rent = UtilityExtensions.Pool(f.Width * f.Height, ArraySegment<TPixel>.Empty);
            Span<TPixel> mutable = rent;
            CopyPixels(Image, f, mutable);
            TPixel result = Reducer(mutable, f);
            UtilityExtensions.Reuse(rent);
            return result;
        }

        private static void CopyPixels(in ImageBuffer<TPixel> image, in Frame frame, Span<TPixel> result)
        {
            int i = 0;
            var source = image.Pixels.AsSpan();
            (int yfrom, int yto) = frame.IterateY(frame.X);

            for (int y0 = yfrom; y0 <= yto; y0++)
            {
                int y = Math.Clamp(y0, 0, image.Height - 1);
                (int xfrom, int xto) = frame.IterateX(y);

                for (int x0 = xfrom; x0 <= xto; x0++)
                {
                    int x = Math.Clamp(x0, 0, image.Width - 1);

                    int pixelId = x + y * image.Width;
                    result[i] = source[pixelId];
                    i++;
                }
            }
        }
    }
}