using System;
using Labs.Core.Scheme;

namespace Labs.Core.Filtering
{
    public delegate TPixel FrameReducer<TPixel>(TPixel[] source, Frame frame);

    public static class Reducers
    {
        public static FrameReducer<ARGB> ARGBMinMaxReducer(ARGB.Channel channel) => (pixels, _) =>
        {
            ARGB pixel = pixels[pixels.Length / 2];
            ARGB min = pixel;
            ARGB max = pixel;

            foreach (ARGB p in pixels)
            {
                if (channel.HasFlag(ARGB.Channel.Red))
                {
                    min.R = Math.Min(min.R, p.R);
                    max.R = Math.Max(max.R, p.R);
                }

                if (channel.HasFlag(ARGB.Channel.Green))
                {
                    min.G = Math.Min(min.G, p.G);
                    max.G = Math.Max(max.G, p.G);
                }

                if (channel.HasFlag(ARGB.Channel.Blue))
                {
                    min.B = Math.Min(min.B, p.B);
                    max.B = Math.Max(max.B, p.B);
                }
            }

            return new ARGB(
                R: mean(min.R, max.R),
                G: mean(min.G, max.G),
                B: mean(min.B, max.B));
        };

        public static FrameReducer<HLSA> HLSAMinMaxReducer(HLSA.Channel channel) => (pixels, _) =>
        {
            HLSA pixel = pixels[pixels.Length / 2];
            HLSA min = pixel;
            HLSA max = pixel;

            foreach (HLSA p in pixels)
            {
                if (channel.HasFlag(HLSA.Channel.Hue))
                {
                    min.H = Math.Min(min.H, p.H);
                    max.H = Math.Max(max.H, p.H);
                }

                if (channel.HasFlag(HLSA.Channel.Lightness))
                {
                    min.L = Math.Min(min.L, p.L);
                    max.L = Math.Max(max.L, p.L);
                }

                if (channel.HasFlag(HLSA.Channel.Saturation))
                {
                    min.S = Math.Min(min.S, p.S);
                    max.S = Math.Max(max.S, p.S);
                }
            }

            return new HLSA(
                h: (min.H + max.H) / 2,
                l: (min.L + max.L) / 2,
                s: (min.S + max.S) / 2
            );
        };


        public static FrameReducer<ARGB> ARGBMedianReducer(ARGB.Channel channel) => (pixels, _) =>
        {
            ARGB pixel = pixels[pixels.Length / 2];

            bool RED = channel.HasFlag(ARGB.Channel.Red);
            bool BLUE = channel.HasFlag(ARGB.Channel.Blue);
            bool GREEN = channel.HasFlag(ARGB.Channel.Green);

            byte[] red = RED ? new byte[pixels.Length] : Array.Empty<byte>();
            byte[] blue = BLUE ? new byte[pixels.Length] : Array.Empty<byte>();
            byte[] green = GREEN ? new byte[pixels.Length] : Array.Empty<byte>();

            for (int i = 0; i < pixels.Length; i++)
            {
                if (RED)
                    red[i] = pixels[i].R;
                if (BLUE)
                    blue[i] = pixels[i].B;
                if (GREEN)
                    green[i] = pixels[i].G;
            }

            Array.Sort(red);
            Array.Sort(blue);
            Array.Sort(green);

            if (RED)
                pixel.R = red[pixels.Length / 2];
            if (GREEN)
                pixel.G = green[pixels.Length / 2];
            if (BLUE)
                pixel.B = blue[pixels.Length / 2];

            return pixel;
        };

        private static byte mean(int a, int b) => (byte) ((a + b) / 2);
    }
}