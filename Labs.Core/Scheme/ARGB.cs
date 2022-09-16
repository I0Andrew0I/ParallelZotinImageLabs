﻿using System;
using System.Runtime.InteropServices;

namespace Labs.Core.Scheme
{
    [StructLayout(LayoutKind.Sequential)]
    public record struct ARGB
    {
        [Flags]
        public enum Channel
        {
            Undefined = 0,
            Alpha = 1,
            Red = 2,
            Green = 4,
            Blue = 8,
            RGB = 14,
            All = 15
        }

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

        public void Extract(Channel channel, ref ARGB value)
        {
            if (channel.HasFlag(ARGB.Channel.Red))
                value.R = R;

            if (channel.HasFlag(ARGB.Channel.Green))
                value.G = G;

            if (channel.HasFlag(ARGB.Channel.Blue))
                value.B = B;

            if (channel.HasFlag(ARGB.Channel.Alpha))
                value.A = A;
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
}