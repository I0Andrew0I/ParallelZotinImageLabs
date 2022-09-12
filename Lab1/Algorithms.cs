using System;
using System.Buffers;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Labs.Core;

namespace Lab1
{
    public class Algorithms
    {
        public static void RGBToHLSInplace(ArraySegment<byte> buffer)
        {
            Span<ARGB> pixels = MemoryMarshal.Cast<byte, ARGB>(buffer);
            for (int i = 0; i < pixels.Length; i++)
            {
                ARGB rgba = pixels[i];
                HLSA hlsa = rgba.ToHLSA();

                pixels[i].A = (byte) (hlsa.A * 255);
                pixels[i].R = (byte) Math.Round(hlsa.H * (255.0 / 360.0));
                pixels[i].G = (byte) Math.Round(hlsa.L * 255.0);
                pixels[i].B = (byte) Math.Round(hlsa.S * 255.0);
            }
        }

        public static ArraySegment<HLSA> RGBToHLS(ArraySegment<byte> buffer)
        {
            Span<ARGB> pixels = MemoryMarshal.Cast<byte, ARGB>(buffer);
            var rent = ArrayPool<HLSA>.Shared.Rent(pixels.Length);
            var result = new ArraySegment<HLSA>(rent, 0, pixels.Length);

            HLSA hlsa = default(HLSA);
            for (int i = 0; i < pixels.Length; i++)
            {
                pixels[i].ToHLSA(ref hlsa);
                result[i] = hlsa;
            }

            return result;
        }

        public static void HLStoRGBInplace(ArraySegment<byte> buffer)
        {
            Span<ARGB> pixels = MemoryMarshal.Cast<byte, ARGB>(buffer);
            for (int i = 0; i < pixels.Length; i++)
            {
                double a = pixels[i].A / 255.0;
                double h = pixels[i].R * (360 / 255.0);
                double l = pixels[i].G / 255.0;
                double s = pixels[i].B / 255.0;

                var hlsa = new HLSA(h, l, s, a);
                pixels[i] = hlsa.ToARGB();
            }
        }

        public static void HLStoRGB(ArraySegment<HLSA> pixels, ref ArraySegment<byte> output)
        {
            Span<ARGB> result = output.Cast<ARGB>();
            ARGB value = default(ARGB);

            for (int i = 0; i < pixels.Count; i++)
            {
                value = pixels[i].ToARGB();
                result[i] = value;
            }
        }

        public static void TransformImage(ArraySegment<byte> imageBuffer, int brightness, int contrast)
        {
            ParallelOptions po = new() {MaxDegreeOfParallelism = 4};
            int step = imageBuffer.Count / 16;

            Parallel.For(0, 16, po, iter =>
            {
                Span<byte> portion = imageBuffer.AsSpan(iter * step, step);
                Span<ARGB> pixels = MemoryMarshal.Cast<byte, ARGB>(portion);

                var yuv = default(YUV);
                for (int i = 0; i < pixels.Length; i++)
                {
                    pixels[i].ToYUV(ref yuv);

                    yuv.Y += brightness;
                    yuv.Y = contrast * (yuv.Y - 127.5) + 127.5;

                    yuv.ToARGB(ref pixels[i]);
                }
            });
        }

        public static void ColorCorrection(ArraySegment<byte> imageBuffer, double[] curve, bool curveCorrection)
        {
            ParallelOptions po = new() {MaxDegreeOfParallelism = 4};
            int step = imageBuffer.Count / 16;

            Parallel.For(0, 16, po, iter =>
            {
                Span<byte> portion = imageBuffer.AsSpan(iter * step, step);
                Span<ARGB> pixels = MemoryMarshal.Cast<byte, ARGB>(portion);

                var yuv = default(YUV);
                for (int i = 0; i < pixels.Length; i++)
                {
                    pixels[i].ToYUV(ref yuv);

                    yuv.Y = curveCorrection
                        ? curve[(int) Math.Round(yuv.Y)]
                        : EquationSystem(yuv.Y);

                    yuv.ToARGB(ref pixels[i]);
                }
            });
        }

        public static (uint[] R, uint[] G, uint[] B) Histogram(Span<ARGB> pixels)
        {
            uint[] histR = new uint[256];
            uint[] histG = new uint[256];
            uint[] histB = new uint[256];

            foreach (ARGB pix in pixels)
            {
                histR[pix.R]++;
                histG[pix.G]++;
                histB[pix.B]++;
            }

            return (histR, histG, histB);
        }

        public static (uint[] H, uint[] L, uint[] S) Histogram(Span<HLSA> pixels)
        {
            uint[] H = new uint[361];
            uint[] L = new uint[361];
            uint[] S = new uint[361];

            foreach (HLSA pix in pixels)
            {
                var l = (int) Math.Round(pix.L * 360);
                var s = (int) Math.Round(pix.S * 360);
                H[(int) pix.H]++;
                L[l]++;
                S[s]++;
            }

            return (H, L, S);
        }

        public static (uint[] R, uint[] G, uint[] B) Histogram(Bitmap image)
        {
            uint[] histR = new uint[256];
            uint[] histG = new uint[256];
            uint[] histB = new uint[256];

            for (int i = 0; i < image.Width; i++)
            for (int j = 0; j < image.Height; j++)
            {
                Color pix = image.GetPixel(i, j);
                histR[pix.R]++;
                histG[pix.G]++;
                histB[pix.B]++;
            }

            return (histR, histG, histB);
        }

        /// <code>
        /// y = (12+y)^2
        /// y = tan(y)
        /// y = sin(y)
        /// y = 5y
        /// y = y^(1/3)
        /// </code>
        /// <param name="y"></param>
        /// <returns></returns>
        public static double EquationSystem(double y)
        {
            double Ynew = y switch
            {
                >= 0 and <= 50 => Math.Pow(12 + y, 2),
                > 50 and <= 100 => Math.Tan(y),
                > 100 and <= 150 => Math.Sin(y),
                > 150 and <= 200 => 5 * y,
                > 200 and <= 255 => Math.Pow(y, 1 / 3.0),
                _ => 0
            };

            return Ynew;
        }
    }

    public record struct YUV
    {
        public double Y { get; set; }
        public double U { get; set; }
        public double V { get; set; }

        public YUV(double Y, double U, double V)
        {
            this.Y = Y;
            this.U = U;
            this.V = V;
        }

        public void ToARGB(ref ARGB value)
        {
            value.R = (byte) Math.Clamp((int) Math.Round(Y + 1.14 * V), Byte.MinValue, Byte.MaxValue);
            value.G = (byte) Math.Clamp((int) Math.Round(Y - 0.395 * U - 0.581 * V), Byte.MinValue, Byte.MaxValue);
            value.B = (byte) Math.Clamp((int) Math.Round(Y + 2.032 * U), Byte.MinValue, Byte.MaxValue);
            value.A = 255;
        }

        public ARGB ToARGB()
        {
            var argb = default(ARGB);
            ToARGB(ref argb);
            return argb;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public record struct ARGB
    {
        public byte B;
        public byte G;
        public byte R;
        public byte A;

        public ARGB(Span<byte> memory)
        {
            if (memory.Length != 4)
                throw new ArgumentException();

            A = memory[3];
            R = memory[2];
            G = memory[1];
            B = memory[0];
        }

        public ARGB(byte R, byte G, byte B, byte A = 255)
        {
            this.R = R;
            this.G = G;
            this.B = B;
            this.A = A;
        }

        public YUV ToYUV()
        {
            var value = default(YUV);
            ToYUV(ref value);
            return value;
        }

        public void ToYUV(ref YUV value)
        {
            value.Y = 0.299 * R + 0.587 * G + 0.114 * B;
            value.U = -0.14713 * R - 0.28886 * G + 0.436 * B;
            value.V = 0.615 * R - 0.51499 * G - 0.10001 * B;
        }

        public HLSA ToHLSA()
        {
            var value = default(HLSA);
            ToHLSA(ref value);
            return value;
        }

        public void ToHLSA(ref HLSA value)
        {
            double h = 0;
            double l = 0;
            double s = 0;

            // нормализовать значения красного, зеленого, синего
            double r = R / 255.0;
            double g = G / 255.0;
            double b = B / 255.0;

            double max = Math.Max(r, Math.Max(g, b));
            double min = Math.Min(r, Math.Min(g, b));

            // тон
            if (max == min)
                h = 0; // неопределённый
            else if (max == r && g >= b)
                h = 60.0 * (g - b) / (max - min);
            else if (max == r && g < b)
                h = 60.0 * (g - b) / (max - min) + 360.0;
            else if (max == g)
                h = 60.0 * (b - r) / (max - min) + 120.0;
            else if (max == b)
                h = 60.0 * (r - g) / (max - min) + 240.0;

            // яркость
            l = (max + min) / 2.0;

            // насыщенность
            if (l == 0 || max == min)
                s = 0;
            else if (0 < l && l <= 0.5)
                s = (max - min) / (max + min);
            else if (l > 0.5)
                s = (max - min) / (2 - (max + min)); //(max-min > 0)?

            value.H = h;
            value.L = l;
            value.S = s;
            value.A = A / 255.0;
        }
    }

    public record struct HLSA
    {
        /// <summary>
        /// Hue (0, 360)
        /// </summary>
        public double H;

        /// <summary>
        /// Lightness (0, 1)
        /// </summary>
        public double L;

        /// <summary>
        /// Saturation (0, 1)
        /// </summary>
        public double S;

        /// <summary>
        /// Alpha (0, 1)
        /// </summary>
        public double A;

        public HLSA(double h, double l, double s, double a = 1)
        {
            H = h;
            L = l;
            S = s;
            A = a;
        }

        public ARGB ToARGB()
        {
            if (S == 0)
            {
                // ахроматический цвет (шкала серого)
                var v = (byte) Math.Round(L * 255.0);
                return new ARGB(v, v, v, (byte) (A * 255));
            }

            double q = (L < 0.5) ? (L * (1.0 + S)) : (L + S - (L * S));
            double p = (2.0 * L) - q;

            double Hk = H / 360.0;
            double[] T = new double[3];
            T[0] = Hk + (1.0 / 3.0); // Tr
            T[1] = Hk; // Tb
            T[2] = Hk - (1.0 / 3.0); // Tg

            for (int a = 0; a < 3; a++)
            {
                if (T[a] < 0) T[a] += 1.0;
                if (T[a] > 1) T[a] -= 1.0;

                if ((T[a] * 6) < 1)
                {
                    T[a] = p + ((q - p) * 6.0 * T[a]);
                }
                else if ((T[a] * 2.0) < 1) //(1.0/6.0)<=T[a] && T[a]<0.5
                {
                    T[a] = q;
                }
                else if ((T[a] * 3.0) < 2) // 0.5<=T[a] && T[a]<(2.0/3.0)
                {
                    T[a] = p + (q - p) * ((2.0 / 3.0) - T[a]) * 6.0;
                }
                else T[a] = p;
            }

            var r = (byte) Math.Round(T[0] * 255.0);
            var g = (byte) Math.Round(T[1] * 255.0);
            var b = (byte) Math.Round(T[2] * 255.0);
            return new ARGB(r, g, b, (byte) (A * 255));
        }
    }
}