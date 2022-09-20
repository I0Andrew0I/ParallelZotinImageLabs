using System;
using Labs.Core.Scheme;

namespace Labs.Core.Filtering
{
    public delegate TPixel FrameReducer<TPixel, TChannel>(Span<TPixel> source, Frame frame)
        where TPixel : struct, IColor<TPixel, TChannel>;

    public static class Reducers
    {
        public static FrameReducer<ARGB, ARGB.Channel> ARGBMinMaxReducer(ARGB.Channel channel) => (pixels, _) =>
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

        public static FrameReducer<HLSA, HLSA.Channel> HLSAMinMaxReducer(HLSA.Channel channel) => (pixels, _) =>
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
                h: (min.H + max.H) / 2.0,
                l: (min.L + max.L) / 2.0,
                s: (min.S + max.S) / 2.0
            );
        };

        public static FrameReducer<YUV, YUV.Channel> YUVMinMaxReducer(YUV.Channel channel) => (pixels, _) =>
        {
            YUV pixel = pixels[pixels.Length / 2];
            YUV min = pixel;
            YUV max = pixel;

            foreach (YUV p in pixels)
            {
                if (channel.HasFlag(YUV.Channel.Y))
                {
                    min.Y = Math.Min(min.Y, p.Y);
                    max.Y = Math.Max(max.Y, p.Y);
                }

                if (channel.HasFlag(YUV.Channel.U))
                {
                    min.U = Math.Min(min.U, p.U);
                    max.U = Math.Max(max.U, p.U);
                }

                if (channel.HasFlag(YUV.Channel.V))
                {
                    min.V = Math.Min(min.V, p.V);
                    max.V = Math.Max(max.V, p.V);
                }
            }

            return new YUV(
                Y: (min.Y + max.Y) / 2.0,
                U: (min.U + max.U) / 2.0,
                V: (min.V + max.V) / 2.0);
        };


        public static FrameReducer<ARGB, ARGB.Channel> ARGBMedianReducer(ARGB.Channel channel) => (pixels, _) =>
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

        public static FrameReducer<HLSA, HLSA.Channel> HLSAMedianReducer(HLSA.Channel channel) => (pixels, _) =>
        {
            HLSA pixel = pixels[pixels.Length / 2];

            bool HUE = channel.HasFlag(HLSA.Channel.Hue);
            bool LIG = channel.HasFlag(HLSA.Channel.Lightness);
            bool SAT = channel.HasFlag(HLSA.Channel.Saturation);

            double[] hue = HUE ? new double[pixels.Length] : Array.Empty<double>();
            double[] lightness = LIG ? new double[pixels.Length] : Array.Empty<double>();
            double[] saturation = SAT ? new double[pixels.Length] : Array.Empty<double>();

            for (int i = 0; i < pixels.Length; i++)
            {
                if (HUE)
                    hue[i] = pixels[i].H;
                if (LIG)
                    lightness[i] = pixels[i].L;
                if (SAT)
                    saturation[i] = pixels[i].S;
            }

            Array.Sort(hue);
            Array.Sort(lightness);
            Array.Sort(saturation);

            if (HUE)
                pixel.H = hue[pixels.Length / 2];
            if (LIG)
                pixel.L = lightness[pixels.Length / 2];
            if (SAT)
                pixel.S = saturation[pixels.Length / 2];

            return pixel;
        };

        public static FrameReducer<YUV, YUV.Channel> YUVMedianReducer(YUV.Channel channel) => (pixels, _) =>
        {
            YUV pixel = pixels[pixels.Length / 2];

            bool Y = channel.HasFlag(YUV.Channel.Y);
            bool U = channel.HasFlag(YUV.Channel.U);
            bool V = channel.HasFlag(YUV.Channel.V);

            double[] ys = Y ? new double[pixels.Length] : Array.Empty<double>();
            double[] us = U ? new double[pixels.Length] : Array.Empty<double>();
            double[] vs = V ? new double[pixels.Length] : Array.Empty<double>();

            for (int i = 0; i < pixels.Length; i++)
            {
                if (Y)
                    ys[i] = pixels[i].Y;
                if (U)
                    us[i] = pixels[i].U;
                if (V)
                    vs[i] = pixels[i].V;
            }

            Array.Sort(ys);
            Array.Sort(us);
            Array.Sort(vs);

            if (Y)
                pixel.Y = ys[pixels.Length / 2];
            if (V)
                pixel.V = vs[pixels.Length / 2];
            if (U)
                pixel.U = us[pixels.Length / 2];

            return pixel;
        };

        private static byte mean(int a, int b) => (byte) ((a + b) / 2);
    }
}