using System;
using Labs.Core.Scheme;

namespace Labs.Core.Filtering
{
    public delegate TPixel PixelTransformer<TPixel, TChannel>(in ImageBuffer<TPixel> source, Frame frame, double[,] kernel)
        where TPixel : IColor<TPixel, TChannel>;

    public static class Transformers
    {
        public static PixelTransformer<TPixel, TChannel> Summator<TPixel, TChannel>(TChannel channels)
            where TPixel : struct, IColor<TPixel, TChannel>
            => (in ImageBuffer<TPixel> image, Frame f, double[,] kernel) =>
            {
                ArraySegment<TPixel> pixels = image.Pixels;
                TPixel result = default;
                TPixel original = pixels[f.X + f.Y * image.Width];
                foreach (int y0 in f.IterateY(f.X))
                {
                    int y = Math.Clamp(y0, 0, image.Height - 1);

                    foreach (int x0 in f.IterateX(y))
                    {
                        int x = Math.Clamp(x0, 0, image.Width - 1);

                        int pixelId = x + y * image.Width;
                        int matrixY = y0 + f.RH - f.Y;
                        int matrixX = x0 + f.RW - f.X;
                        TPixel pixel = pixels[pixelId];
                        result = result.Add(pixel.Mul(kernel[matrixY, matrixX]));
                    }
                }

                result.Extract(channels, ref original);
                return original;
            };

        public static PixelTransformer<ARGB, ARGB.Channel> ARGBSummator(ARGB.Channel channel) => (in ImageBuffer<ARGB> image, Frame f, double[,] kernel) =>
        {
            ArraySegment<ARGB> pixels = image.Pixels;
            ARGB original = pixels[f.X + f.Y * f.Width];
            double R = 0, G = 0, B = 0;
            foreach (int y0 in f.IterateY(f.X))
            {
                int y = Math.Clamp(y0, 0, image.Height - 1);

                foreach (int x0 in f.IterateX(y))
                {
                    int x = Math.Clamp(x0, 0, image.Width - 1);

                    int pixelId = x + y * image.Width;
                    int matrixY = y0 + f.RH - f.Y;
                    int matrixX = x0 + f.RW - f.X;

                    if (channel.HasFlag(ARGB.Channel.Red))
                        R += pixels[pixelId].R * kernel[matrixY, matrixX];
                    if (channel.HasFlag(ARGB.Channel.Green))
                        G += pixels[pixelId].G * kernel[matrixY, matrixX];
                    if (channel.HasFlag(ARGB.Channel.Blue))
                        B += pixels[pixelId].B * kernel[matrixY, matrixX];
                }
            }

            if (channel.HasFlag(ARGB.Channel.Red))
                original.R = (byte) Math.Clamp(R, 0, 255);
            if (channel.HasFlag(ARGB.Channel.Green))
                original.G = (byte) Math.Clamp(G, 0, 255);
            if (channel.HasFlag(ARGB.Channel.Blue))
                original.B = (byte) Math.Clamp(B, 0, 255);
            return original;
        };

        public static PixelTransformer<HLSA, HLSA.Channel> HLSASummator(HLSA.Channel channel) => (in ImageBuffer<HLSA> image, Frame f, double[,] kernel) =>
        {
            HLSA original = image.Pixels[f.X + f.Y * f.Width];
            double H = 0, L = 0, S = 0;
            foreach (int y0 in f.IterateY(f.X))
            {
                int y = Math.Clamp(y0, 0, image.Height - 1);

                foreach (int x0 in f.IterateX(y))
                {
                    int x = Math.Clamp(x0, 0, image.Width - 1);

                    int pixelId = x + y * image.Width;
                    int matrixY = y0 + f.RH - f.Y;
                    int matrixX = x0 + f.RW - f.X;

                    if (channel.HasFlag(ARGB.Channel.Red))
                        H += image.Pixels[pixelId].H * kernel[matrixY, matrixX];
                    if (channel.HasFlag(ARGB.Channel.Green))
                        L += image.Pixels[pixelId].L * kernel[matrixY, matrixX];
                    if (channel.HasFlag(ARGB.Channel.Blue))
                        S += image.Pixels[pixelId].S * kernel[matrixY, matrixX];
                }
            }

            if (channel.HasFlag(HLSA.Channel.Hue))
                original.H = Math.Clamp(original.H + H, 0, 360);
            if (channel.HasFlag(HLSA.Channel.Lightness))
                original.L = Math.Clamp(original.L + L, 0, 1);
            if (channel.HasFlag(HLSA.Channel.Saturation))
                original.S = Math.Clamp(original.S + S, 0, 1);

            return original;
        };

        public static PixelTransformer<YUV, YUV.Channel> YUVSummator(YUV.Channel channel) => (in ImageBuffer<YUV> image, Frame f, double[,] kernel) =>
        {
            YUV original = image.Pixels[f.X + f.Y * f.Width];
            double Y = 0, U = 0, V = 0;
            foreach (int y0 in f.IterateY(f.X))
            {
                int y = Math.Clamp(y0, 0, image.Height - 1);

                foreach (int x0 in f.IterateX(y))
                {
                    int x = Math.Clamp(x0, 0, image.Width - 1);

                    int pixelId = x + y * image.Width;
                    int matrixY = y0 + f.RH - f.Y;
                    int matrixX = x0 + f.RW - f.X;

                    if (channel.HasFlag(ARGB.Channel.Red))
                        Y += image.Pixels[pixelId].Y * kernel[matrixY, matrixX];
                    if (channel.HasFlag(ARGB.Channel.Green))
                        U += image.Pixels[pixelId].U * kernel[matrixY, matrixX];
                    if (channel.HasFlag(ARGB.Channel.Blue))
                        V += image.Pixels[pixelId].V * kernel[matrixY, matrixX];
                }
            }

            if (channel.HasFlag(YUV.Channel.Y))
                original.Y = Math.Clamp(original.Y + Y, 0, 255);
            if (channel.HasFlag(YUV.Channel.U))
                original.U = Math.Clamp(original.U + U, -112, 112);
            if (channel.HasFlag(YUV.Channel.V))
                original.V = Math.Clamp(original.V + V, -157, 157);

            return original;
        };

        public static PixelTransformer<ARGB, ARGB.Channel> ARGBLaplacianSummator(ARGB.Channel channel, double sharpness) => (in ImageBuffer<ARGB> image, Frame f, double[,] kernel) =>
        {
            ARGB original = image.Pixels[f.X + f.Y * f.Width];
            double R = 0, G = 0, B = 0;
            foreach (int y0 in f.IterateY(f.X))
            {
                int y = Math.Clamp(y0, 0, image.Height - 1);

                foreach (int x0 in f.IterateX(y))
                {
                    int x = Math.Clamp(x0, 0, image.Width - 1);

                    int pixelId = x + y * image.Width;
                    int matrixY = y0 + f.RH - f.Y;
                    int matrixX = x0 + f.RW - f.X;

                    if (channel.HasFlag(ARGB.Channel.Red))
                        R += image.Pixels[pixelId].R * kernel[matrixY, matrixX];
                    if (channel.HasFlag(ARGB.Channel.Green))
                        G += image.Pixels[pixelId].G * kernel[matrixY, matrixX];
                    if (channel.HasFlag(ARGB.Channel.Blue))
                        B += image.Pixels[pixelId].B * kernel[matrixY, matrixX];
                }
            }

            if (channel.HasFlag(ARGB.Channel.Red))
                original.R = (byte) Math.Clamp((original.R + R) * sharpness, 0, 255);
            if (channel.HasFlag(ARGB.Channel.Green))
                original.G = (byte) Math.Clamp((original.G + G) * sharpness, 0, 255);
            if (channel.HasFlag(ARGB.Channel.Blue))
                original.B = (byte) Math.Clamp((original.B + B) * sharpness, 0, 255);
            return original;
        };
    }
}