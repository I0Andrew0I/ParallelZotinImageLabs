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
        public enum Channel
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
        public double Y { get; set; }

        /// <summary>
        /// Blue projection (-112, 112)
        /// </summary>
        public double U { get; set; }

        /// <summary>
        /// Red projection (-157, 157)
        /// </summary>
        public double V { get; set; }

        public YUV(double Y, double U, double V)
        {
            this.Y = Y;
            this.U = U;
            this.V = V;
        }

        public YUV Add(YUV other)
        {
            YUV value = this;
            value.Y += other.Y;
            value.U += other.U;
            value.V += other.V;
            return value;
        }

        public YUV Add(YUV other, out YUV overflow)
        {
            YUV value = this;
            overflow = default;

            double y = value.Y + other.Y;
            double u = value.U + other.U;
            double v = value.V + other.V;
            overflow.Y = Math.Clamp(y - 255, 0, 255);
            overflow.U = UtilityExtensions.Overflow(u, -112, 112); //Math.Clamp(u - 225, -112, 112) + 225;
            overflow.V = UtilityExtensions.Overflow(v, -157, 157);
            value.Y = Math.Clamp(y, 0, 255);
            value.U = Math.Clamp(u, -112, 112);
            value.V = Math.Clamp(v, -157, 157);
            return value;
        }

        public YUV Subtract(YUV other)
        {
            YUV value = this;
            value.Y -= other.Y;
            value.U -= other.U;
            value.V -= other.V;
            return value;
        }

        public YUV Subtract(YUV other, out YUV overflow)
        {
            YUV value = this;
            overflow = default;

            double y = value.Y - other.Y;
            double u = value.U - other.U;
            double v = value.V - other.V;
            overflow.Y = y < 0 ? -y : 0;
            overflow.U = UtilityExtensions.Overflow(u, -112, 112); //Math.Clamp(u - 225, -112, 112) + 225;
            overflow.V = UtilityExtensions.Overflow(v, -157, 157);
            // overflow.U = u < -112 ? -112 - u : u > 112 ? 112 + u : 0; // Math.Clamp(225 - u, -112, 112);
            // overflow.V = v < -157 ? -157 - v : v > 157 ? 157 + v : 0; //Math.Clamp(315 - v - 315, -157, 157);
            value.Y = Math.Clamp(y, 0, 255);
            value.U = Math.Clamp(u, -112, 112);
            value.V = Math.Clamp(v, -157, 157);
            return value;
        }

        public YUV Mul(double num)
        {
            YUV value = this;
            value.Y *= num;
            value.U *= num;
            value.V *= num;
            return value;
        }

        public YUV Div(double num)
        {
            YUV value = this;
            value.Y /= num;
            value.U /= num;
            value.V /= num;
            return value;
        }

        public YUV Correct(Accumulator overflow)
        {
            YUV value = this;
            value.Y = Math.Clamp(value.Y + overflow.K1, 0, 255);
            value.U = Math.Clamp(value.U + overflow.K2, -112, 112);
            value.V = Math.Clamp(value.V + overflow.K3, -157, 157);
            return value;
        }

        public void Extract(Channel channels, ref YUV value)
        {
            if (channels.HasFlag(Channel.Y))
                value.Y = Y;

            if (channels.HasFlag(Channel.U))
                value.U = U;

            if (channels.HasFlag(Channel.V))
                value.V = V;
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

        public int CompareTo(YUV other)
        {
            int yComparison = Y.CompareTo(other.Y);
            if (yComparison != 0) return yComparison;
            int uComparison = U.CompareTo(other.U);
            if (uComparison != 0) return uComparison;
            return V.CompareTo(other.V);
        }
    }
}