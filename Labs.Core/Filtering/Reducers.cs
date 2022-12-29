using System;
using Labs.Core.Scheme;

namespace Labs.Core.Filtering
{
    public delegate TPixel FrameReducer<TPixel, TChannel>(Span<TPixel> source, Frame frame)
        where TPixel : struct, IColor<TPixel, TChannel>;

    public static class Reducers
    {
        public static FrameReducer<ARGB, ARGB.Channel> ARGBKasaburiReducer(ARGB.Channel channel, double threshold) => (pixels, _) =>
        {
            decimal total_red = 0;
            decimal total_green = 0;
            decimal total_blue = 0;
            decimal count_red = 0;
            decimal count_green = 0;
            decimal count_blue = 0;

            ARGB source = pixels[pixels.Length / 2];

            bool RED = (channel & ARGB.Channel.Red) != 0;
            bool GREEN = (channel & ARGB.Channel.Green) != 0;
            bool BLUE = (channel & ARGB.Channel.Blue) != 0;

            for (var i = 0; i < pixels.Length; i++)
            {
                ARGB p = pixels[i];
                if (RED && Math.Abs(p.R-source.R) < threshold)
                {
                    total_red += p.R;
                    count_red++;
                }

                if (GREEN && Math.Abs(p.G-source.G) < threshold)
                {
                    total_green += p.G;
                    count_green++;
                }

                if (BLUE && Math.Abs(p.B - source.B) < threshold)
                {
                    total_blue += p.B;
                    count_blue++;
                }
            }

            byte R = RED ? (byte)( total_red / count_red) : source.R;
            byte G = GREEN ? (byte)( total_green / count_green) : source.G;
            byte B = BLUE ? (byte)( total_blue / count_blue) : source.B;

            return new ARGB(R, G, B);
        };
        //TODO: fix
        public static FrameReducer<HLSA, HLSA.Channel> HLSAKasaburiReducer(HLSA.Channel channel, double threshold) => (pixels, _) =>
        {
            double total_hue = 0;
            double total_satur = 0;
            double total_light = 0;
            double count_hue = 0;
            double count_light = 0;
            double count_satur = 0;

            HLSA source = pixels[pixels.Length / 2];

            bool Hue = (channel & HLSA.Channel.Hue) != 0;
            bool Light = (channel & HLSA.Channel.Lightness) != 0;
            bool Satur = (channel & HLSA.Channel.Saturation) != 0;

            for (var i = 0; i < pixels.Length; i++)
            {
                HLSA p = pixels[i];
                if (Hue && Math.Abs(p.H-source.H) < threshold)
                {
                    total_hue += p.H;
                    count_hue++;
                }

                if (Light && Math.Abs(p.L-source.L) < threshold)
                {
                    total_light += p.L;
                    count_light++;
                }
                
                if (Satur && Math.Abs(p.S-source.S) < threshold)
                {
                    total_satur += p.S;
                    count_satur++;
                }
            }

            double H = Hue ?  total_hue / count_hue : source.H;
            double L = Light ?  total_light / count_light : source.L;
            double S = Satur ?  total_satur / count_satur : source.S;

            return new HLSA(H, L, S);
        };
        
        
        public static FrameReducer<YUV, YUV.Channel> YUVKasaburiReducer(YUV.Channel channel, double threshold) => (pixels, _) =>
        {
            double total_Y = 0;
            double total_U = 0;
            double total_V = 0;
            double count_Y = 0;
            double count_U = 0;
            double count_V = 0;
        
            YUV source = pixels[pixels.Length / 2];
        
            bool Y = (channel & YUV.Channel.Y) != 0;
            bool U = (channel & YUV.Channel.U) != 0;
            bool V = (channel & YUV.Channel.V) != 0;
        
            for (var i = 0; i < pixels.Length; i++)
            {
                YUV p = pixels[i];
                if (Y && Math.Abs(p.Y-source.Y) < threshold)
                {
                    total_Y += p.Y;
                    count_Y++;
                }
        
                if (U && Math.Abs(p.U-source.U) < threshold)
                {
                    total_U += p.U;
                    count_U++;
                }

                if (V && Math.Abs(p.V - source.V) < threshold)
                {
                    total_V += p.V;
                    count_V++;
                }
            }
        
            double newY = Y ? ( total_Y / count_Y) : source.Y;
            double newU = U ? ( total_U / count_U) : source.U;
            double newV = V ? ( total_V / count_V) : source.V;
        
            return new YUV(newY, newU, newV);
        };
        
        
        public static FrameReducer<ARGB, ARGB.Channel> ARGBMinMaxReducer(ARGB.Channel channel) => (pixels, _) =>
        {
            ARGB pixel = pixels[pixels.Length / 2];
            ARGB min = pixel;
            ARGB max = pixel;

            bool RED = (channel & ARGB.Channel.Red) != 0;
            bool BLUE = (channel & ARGB.Channel.Blue) != 0;
            bool GREEN = (channel & ARGB.Channel.Green) != 0;

            for (var i = 0; i < pixels.Length; i++)
            {
                ARGB p = pixels[i];
                if (RED)
                {
                    min.R = Math.Min(min.R, p.R);
                    max.R = Math.Max(max.R, p.R);
                }

                if (GREEN)
                {
                    min.G = Math.Min(min.G, p.G);
                    max.G = Math.Max(max.G, p.G);
                }

                if (BLUE)
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
            bool H = (channel & HLSA.Channel.Hue) != 0;
            bool L = (channel & HLSA.Channel.Lightness) != 0;
            bool S = (channel & HLSA.Channel.Saturation) != 0;

            for (var i = 0; i < pixels.Length; i++)
            {
                HLSA p = pixels[i];
                if (H)
                {
                    min.H = Math.Min(min.H, p.H);
                    max.H = Math.Max(max.H, p.H);
                }

                if (L)
                {
                    min.L = Math.Min(min.L, p.L);
                    max.L = Math.Max(max.L, p.L);
                }

                if (S)
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
            bool Y = (channel & YUV.Channel.Y) != 0;
            bool U = (channel & YUV.Channel.U) != 0;
            bool V = (channel & YUV.Channel.V) != 0;

            foreach (YUV p in pixels)
            {
                if (Y)
                {
                    min.Y = Math.Min(min.Y, p.Y);
                    max.Y = Math.Max(max.Y, p.Y);
                }

                if (U)
                {
                    min.U = Math.Min(min.U, p.U);
                    max.U = Math.Max(max.U, p.U);
                }

                if (V)
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

            bool RED = (channel & ARGB.Channel.Red) != 0;
            bool BLUE = (channel & ARGB.Channel.Blue) != 0;
            bool GREEN = (channel & ARGB.Channel.Green) != 0;

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

            bool HUE = (channel & HLSA.Channel.Hue) != 0;
            bool LIG = (channel & HLSA.Channel.Lightness) != 0;
            bool SAT = (channel & HLSA.Channel.Saturation) != 0;

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

            bool Y = (channel & YUV.Channel.Y) != 0;
            bool U = (channel & YUV.Channel.U) != 0;
            bool V = (channel & YUV.Channel.V) != 0;

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