using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Labs.Core.Scheme
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly record struct GrayScale : IColor<GrayScale, byte>
    {
        /// <summary>
        /// (0, 255)
        /// </summary>
        public readonly double Value { get; init; }

        public GrayScale(double value) =>
            Value = value;

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

        public GrayScale Correct(in Accumulator overflow) =>
            new GrayScale(Value + overflow.K1);

        public Accumulator Convert()
        {
            Accumulator value = new();
            value.K1 = Value;
            return value;
        }

        public void Extract(in byte channels, ref GrayScale other) =>
            other = this;

        public int CompareTo(GrayScale other) =>
            Value.CompareTo(other.Value);

        public static explicit operator GrayScale(double value) =>
            ClampColor(value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static GrayScale GetOverflow(double value, ref Accumulator overflow)
        {
            GrayScale result = ClampColor(value);
            overflow.K1 += result.Value - value;
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static GrayScale ClampColor(double value) =>
            new(ClampValue(value));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double ClampValue(double value) =>
            Math.Clamp(value, 0, 255);
    }
}