﻿using System;
using System.Buffers;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Labs.Core.Scheme;

namespace Labs.Core
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

        public static void TransformImage(ArraySegment<byte> imageBuffer, int brightness, double contrast, int threads)
        {
            ParallelOptions po = new() {MaxDegreeOfParallelism = threads};
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

        public static void ColorCorrection(ArraySegment<byte> imageBuffer, double[] curve, bool curveCorrection, int threads)
        {
            ParallelOptions po = new() {MaxDegreeOfParallelism = threads};
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
}