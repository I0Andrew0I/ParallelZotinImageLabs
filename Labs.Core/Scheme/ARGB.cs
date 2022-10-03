using System;
using System.Runtime.InteropServices;

namespace Labs.Core.Scheme
{
    [StructLayout(LayoutKind.Sequential)]
    public record struct ARGB : IColor<ARGB, ARGB.Channel>
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

        public ARGB Add(ARGB other)
        {
            ARGB value = this;
            value.R = (byte) Math.Clamp(value.R + other.R, 0, 255);
            value.G = (byte) Math.Clamp(value.G + other.G, 0, 255);
            value.B = (byte) Math.Clamp(value.B + other.B, 0, 255);
            return value;
        }

        public ARGB Add(ARGB other, out ARGB overflow)
        {
            ARGB value = this;
            overflow = default;

            int r = value.R + other.R;
            int g = value.G + other.G;
            int b = value.B + other.B;
            overflow.R = (byte) Math.Clamp(r - 255, 0, 255);
            overflow.G = (byte) Math.Clamp(g - 255, 0, 255);
            overflow.B = (byte) Math.Clamp(b - 255, 0, 255);
            value.R = (byte) Math.Clamp(r, 0, 255);
            value.G = (byte) Math.Clamp(g, 0, 255);
            value.B = (byte) Math.Clamp(b, 0, 255);
            return value;
        }

        public ARGB Subtract(ARGB other)
        {
            ARGB value = this;
            value.R = (byte) Math.Clamp(value.R - other.R, 0, 255);
            value.G = (byte) Math.Clamp(value.G - other.G, 0, 255);
            value.B = (byte) Math.Clamp(value.B - other.B, 0, 255);
            return value;
        }

        public ARGB Subtract(ARGB other, out ARGB overflow)
        {
            ARGB value = this;
            overflow = default;

            int r = value.R - other.R;
            int g = value.G - other.G;
            int b = value.B - other.B;
            overflow.R = (byte) (r < 0 ? -r : 0);
            overflow.G = (byte) (g < 0 ? -g : 0);
            overflow.B = (byte) (b < 0 ? -b : 0);
            value.R = (byte) Math.Clamp(r, 0, 255);
            value.G = (byte) Math.Clamp(g, 0, 255);
            value.B = (byte) Math.Clamp(b, 0, 255);
            return value;
        }

        public ARGB Mul(double num)
        {
            ARGB value = this;
            value.R = (byte) Math.Clamp(value.R * num, 0, 255);
            value.G = (byte) Math.Clamp(value.G * num, 0, 255);
            value.B = (byte) Math.Clamp(value.B * num, 0, 255);
            return value;
        }

        public ARGB Div(double num)
        {
            ARGB value = this;
            value.R = (byte) Math.Clamp(value.R / num, 0, 255);
            value.G = (byte) Math.Clamp(value.G / num, 0, 255);
            value.B = (byte) Math.Clamp(value.B / num, 0, 255);
            return value;
        }

        public ARGB Correct(Accumulator overflow)
        {
            ARGB value = this;
            value.R = (byte) Math.Clamp(value.R + overflow.K1, 0, 255);
            value.G = (byte) Math.Clamp(value.G + overflow.K2, 0, 255);
            value.B = (byte) Math.Clamp(value.B + overflow.K3, 0, 255);
            value.A = (byte) Math.Clamp(value.A + overflow.K4, 0, 255);
            return value;
        }

        public void Extract(Channel channel, ref ARGB value)
        {
            if (channel.HasFlag(Channel.Red))
                value.R = R;

            if (channel.HasFlag(Channel.Green))
                value.G = G;

            if (channel.HasFlag(Channel.Blue))
                value.B = B;

            if (channel.HasFlag(Channel.Alpha))
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

        public int CompareTo(ARGB other)
        {
            int v1 = R + G + B;
            int v2 = other.R + other.G + other.B;
            return Math.Sign(v2 - v1);
        }
    }
}