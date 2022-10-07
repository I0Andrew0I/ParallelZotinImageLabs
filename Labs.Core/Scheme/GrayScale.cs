using System;
using System.Runtime.CompilerServices;

namespace Labs.Core.Scheme
{
    public record struct GrayScale : IColor<GrayScale, byte>
    {
        /// <summary>
        /// (0, 255)
        /// </summary>
        public double Value;

        public GrayScale Add(in GrayScale other) =>
            ClampColor(Value + other.Value);

        public GrayScale Add(in GrayScale other, ref Accumulator overflow) =>
            GetOverflow(Value + other.Value, ref overflow);

        public GrayScale Subtract(in GrayScale other) =>
            ClampColor(Value - other.Value);

        public GrayScale Subtract(in GrayScale other, ref Accumulator overflow) =>
            GetOverflow(Value - other.Value, ref overflow);

        public GrayScale Mul(in double num) =>
            ClampColor(Value * num);

        public GrayScale Mul(in double num, ref Accumulator overflow) =>
            GetOverflow(Value * num, ref overflow);

        public GrayScale Div(in double num) =>
            ClampColor(Value / num);

        public GrayScale Correct(in Accumulator overflow)
        {
            GrayScale temp = this;
            temp.Value += overflow.K1;
            return temp;
        }

        public void Extract(in byte channels, ref GrayScale other) =>
            other.Value = Value;

        public int CompareTo(GrayScale other) =>
            Value.CompareTo(other.Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static GrayScale GetOverflow(double value, ref Accumulator overflow)
        {
            GrayScale result = default;
            result.Value = ClampValue(value);

            overflow.K1 += result.Value - value;
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static GrayScale ClampColor(double value)
        {
            GrayScale result = default;
            result.Value = ClampValue(value);
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double ClampValue(double value) =>
            Math.Clamp(value, 0, 255);
    }
}