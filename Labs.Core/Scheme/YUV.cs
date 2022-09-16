using System;

namespace Labs.Core.Scheme
{
    /// <summary>
    /// YUV [PAL]
    /// </summary>
    public record struct YUV
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
}