using System;
using Labs.Core.Scheme;

namespace Labs.Core.Filtering
{
    public delegate TPixel PixelTransformer<TPixel>(TPixel source, double K, TPixel accumulator);

    public static class Transformers
    {
        public static PixelTransformer<ARGB> ARGBSummator(ARGB.Channel channel) => (pixel, K, result) =>
        {
            if (channel.HasFlag(ARGB.Channel.Red))
                result.R = (byte) Math.Clamp(result.R + pixel.R * K, 0, 255);
            if (channel.HasFlag(ARGB.Channel.Green))
                result.G = (byte) Math.Clamp(result.G + pixel.G * K, 0, 255);
            if (channel.HasFlag(ARGB.Channel.Blue))
                result.B = (byte) Math.Clamp(result.B + pixel.B * K, 0, 255);

            return result;
        };

        public static PixelTransformer<HLSA> HLSASummator(HLSA.Channel channel) => (pixel, K, result) =>
        {
            if (channel.HasFlag(HLSA.Channel.Hue))
                result.H = Math.Clamp(result.H + pixel.H * K, 0, 360);
            if (channel.HasFlag(HLSA.Channel.Lightness))
                result.L = Math.Clamp(result.L + pixel.L * K, 0, 1);
            if (channel.HasFlag(HLSA.Channel.Saturation))
                result.S = Math.Clamp(result.S + pixel.S * K, 0, 1);

            return result;
        };

        public static PixelTransformer<YUV> YUVSummator(YUV.Channel channel) => (pixel, K, result) =>
        {
            if (channel.HasFlag(YUV.Channel.Y))
                result.Y = Math.Clamp(result.Y + pixel.Y * K, 0, 255);
            if (channel.HasFlag(YUV.Channel.U))
                result.U = Math.Clamp(result.U + pixel.U * K, -112, 112);
            if (channel.HasFlag(YUV.Channel.V))
                result.V = Math.Clamp(result.V + pixel.V * K, -157, 157);

            return result;
        };
    }
}