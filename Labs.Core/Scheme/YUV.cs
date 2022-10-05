using System;
using System.Runtime.CompilerServices;

namespace Labs.Core.Scheme
{
    /// <summary>
    /// YUV [PAL]
    /// </summary>
    public record struct YUV : IColor<YUV, YUV.Channel>
    {
        [Flags]
        public enum Channel : byte
        {
            Undefined = 0,
            Y = 1,
            U = 2,
            V = 4,
            All = 7
        }

        /// <summary>
        /// Luminance (0, 255)
        /// </summary>
        public double Y;

        /// <summary>
        /// Blue projection (-112, 112)
        /// </summary>
        public double U;

        /// <summary>
        /// Red projection (-157, 157)
        /// </summary>
        public double V;

        public YUV(double Y, double U, double V)
        {
            this.Y = Y;
            this.U = U;
            this.V = V;
        }

        public YUV Add(ref YUV other)
        {
            YUV value = this;
            value.Y += other.Y;
            value.U += other.U;
            value.V += other.V;
            return value;
        }

        public YUV Add(ref YUV other, ref Accumulator overflow)
        {
            double y = Y + other.Y;
            double u = U + other.U;
            double v = V + other.V;

            return GetOverflow(y, u, v, ref overflow);
        }

        public YUV Subtract(ref YUV other)
        {
            YUV value = this;
            value.Y -= other.Y;
            value.U -= other.U;
            value.V -= other.V;
            return value;
        }

        public YUV Subtract(ref YUV other, ref Accumulator overflow)
        {
            double y = Y - other.Y;
            double u = U - other.U;
            double v = V - other.V;

            return GetOverflow(y, u, v, ref overflow);
        }

        public YUV Mul(double num)
        {
            YUV value = this;
            value.Y *= num;
            value.U *= num;
            value.V *= num;
            return value;
        }

        public YUV Mul(double num, ref Accumulator overflow)
        {
            double y = Y * num;
            double u = U * num;
            double v = V * num;

            return GetOverflow(y, u, v, ref overflow);
        }

        public YUV Div(double num)
        {
            YUV value = this;
            value.Y /= num;
            value.U /= num;
            value.V /= num;
            return value;
        }

        public YUV Correct(ref Accumulator overflow)
        {
            YUV value = this;
            value.Y = Math.Clamp(value.Y + overflow.K1, 0, 255);
            value.U = Math.Clamp(value.U + overflow.K2, -112, 112);
            value.V = Math.Clamp(value.V + overflow.K3, -157, 157);
            return value;
        }

        public Accumulator Convert()
        {
            Accumulator value = default;
            value.K1 = Y;
            value.K2 = U;
            value.K3 = V;
            return value;
        }

        public void Extract(Channel channels, ref YUV value)
        {
            if ((channels & Channel.Y) != 0)
                value.Y = Y;

            if ((channels & Channel.U) != 0)
                value.U = U;

            if ((channels & Channel.V) != 0)
                value.V = V;
        }


        public void ToARGB(ref ARGB value)
        {
            value.R = (byte)Math.Clamp((int)Math.Round(Y + 1.14 * V), 0, 255);
            value.G = (byte)Math.Clamp((int)Math.Round(Y - 0.395 * U - 0.581 * V), 0, 255);
            value.B = (byte)Math.Clamp((int)Math.Round(Y + 2.032 * U), 0, 255);
            value.A = 255;
        }

        public ARGB ToARGB()
        {
            var argb = default(ARGB);
            ToARGB(ref argb);
            return argb;
        }

        public int CompareTo(YUV other)
        {
            int yComparison = Y.CompareTo(other.Y);
            if (yComparison != 0) return yComparison;
            int uComparison = U.CompareTo(other.U);
            if (uComparison != 0) return uComparison;
            return V.CompareTo(other.V);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static YUV GetOverflow(double y, double u, double v, ref Accumulator overflow)
        {
            YUV value = default;
            value.Y = Math.Clamp(y, 0, 255);
            value.U = Math.Clamp(u, -112, 112);
            value.V = Math.Clamp(v, -157, 157);

            overflow.K1 += y - value.Y;
            overflow.K2 += u - value.U;
            overflow.K3 += v - value.V;
            return value;
        }
    }
}