using System;
using System.Buffers;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Lab1
{
    public class Algorithms
    {
        public static void RGBToHLS(ArraySegment<byte> buffer)
        {
            for (int i = 0; i < buffer.Count; i += 4)
            {
                double h = 0;
                double l = 0;
                double s = 0;

                // нормализовать значения красного, зеленого, синего
                double r = (double) buffer[i + 2] / 255.0;
                double g = (double) buffer[i + 1] / 255.0;
                double b = (double) buffer[i] / 255.0;

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

                buffer[i + 2] = (byte) Math.Round(h / 360.0 * 255.0);
                buffer[i + 1] = (byte) Math.Round(l * 255.0);
                buffer[i] = (byte) Math.Round(s * 255.0);
                buffer[i + 3] = 255;
            }
        }

        public static void HLStoRGB(ArraySegment<byte> buffer)
        {
            for (int i = 0; i < buffer.Count; i += 4)
            {
                double h = buffer[i + 2] / 255.0 * 360;
                double l = buffer[i + 1] / 255.0;
                double s = buffer[i] / 255.0;

                if (s == 0)
                {
                    // ахроматический цвет (шкала серого)
                    buffer[i + 2] = (byte) Math.Round(l * 255.0);
                    buffer[i + 1] = (byte) Math.Round(l * 255.0);
                    buffer[i] = (byte) Math.Round(l * 255.0);
                    buffer[i + 3] = 255;
                }
                else
                {
                    double q = (l < 0.5) ? (l * (1.0 + s)) : (l + s - (l * s));
                    double p = (2.0 * l) - q;

                    double Hk = h / 360.0;
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

                    buffer[i + 2] = (byte) Math.Round(T[0] * 255.0);
                    buffer[i + 1] = (byte) Math.Round(T[1] * 255.0);
                    buffer[i] = (byte) Math.Round(T[2] * 255.0);
                    buffer[i + 3] = 255;
                }
            }
        }

        public static ArraySegment<byte> BmpToByte(Bitmap source)
        {
            BitmapData sourceData = source.LockBits(new Rectangle(0, 0, source.Width, source.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            int size = sourceData.Stride * sourceData.Height;
            byte[] rent = ArrayPool<byte>.Shared.Rent(size);
            Marshal.Copy(sourceData.Scan0, rent, 0, size);
            source.UnlockBits(sourceData);
            return new ArraySegment<byte>(rent, 0, size);
        }

        public static void ByteToBmp(Bitmap bmp, ArraySegment<byte> bytes)
        {
            BitmapData resultData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            Marshal.Copy(bytes.Array, 0, resultData.Scan0, bytes.Count);
            bmp.UnlockBits(resultData);
        }

        public static Image ReadImage(string filePath)
        {
            using FileStream fs = new(filePath, FileMode.Open);
            Image img = Image.FromStream(fs);
            return img;
        }

        public static void TransformImage(ArraySegment<byte> imageBuffer, int brightness, int contrast)
        {
            int exclusive = imageBuffer.Count / 4;
            ParallelOptions po = new() {MaxDegreeOfParallelism = 4};
            Parallel.For(0, exclusive, po, i =>
            {
                int iter = i * 4;

                var argb = new ARGB(imageBuffer.AsSpan(iter, 4));
                var yuv = argb.ToYUV();

                yuv.Y += brightness;
                yuv.Y = contrast * (yuv.Y - 127.5) + 127.5;

                var argb1 = yuv.ToARGB();

                imageBuffer[iter + 2] = argb1.R;
                imageBuffer[iter + 1] = argb1.G;
                imageBuffer[iter] = argb1.B;
                imageBuffer[iter + 3] = argb1.A;
            });
        }

        public static void ColorCorrection(ArraySegment<byte> buffer, double[] curve, bool curveCorrection)
        {
            ParallelOptions po = new() {MaxDegreeOfParallelism = 4};
            Parallel.For(0, buffer.Count / 4, po, i =>
            {
                int iter = i * 4;

                var argb = new ARGB(buffer.AsSpan(iter, 4));
                var yuv = argb.ToYUV();

                yuv.Y = curveCorrection
                    ? curve[(int) Math.Round(yuv.Y)]
                    : Algorithms.EquationSystem(yuv.Y);

                var argb1 = yuv.ToARGB();

                buffer[iter + 3] = argb1.A;
                buffer[iter + 2] = argb1.R;
                buffer[iter + 1] = argb1.G;
                buffer[iter] = argb1.B;
            });
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

        public ARGB ToARGB()
        {
            byte r = (byte) Math.Clamp((int) Math.Round(Y + 1.14 * V), Byte.MinValue, Byte.MaxValue);
            byte g = (byte) Math.Clamp((int) Math.Round(Y - 0.395 * U - 0.581 * V), Byte.MinValue, Byte.MaxValue);
            byte b = (byte) Math.Clamp((int) Math.Round(Y + 2.032 * U), Byte.MinValue, Byte.MaxValue);

            return new ARGB(r, g, b);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public record struct ARGB
    {
        public byte B { get; set; }
        public byte G { get; set; }
        public byte R { get; set; }

        public byte A { get; set; }

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
            double y = 0.299 * R + 0.587 * G + 0.114 * B;
            double u = -0.14713 * R - 0.28886 * G + 0.436 * B;
            double v = 0.615 * R - 0.51499 * G - 0.10001 * B;
            return new YUV(y, u, v);
        }
    }
}