using System;

namespace Labs.Core.Scheme
{
    public interface IColor<TPixel, TChannel> : IComparable<TPixel> where TPixel : struct
    {
        public TPixel Add(ref TPixel other);
        public TPixel Add(ref TPixel other, ref Accumulator overflow);
        public TPixel Subtract(ref TPixel other);
        public TPixel Subtract(ref TPixel other, ref Accumulator overflow);
        public TPixel Mul(double num);
        public TPixel Mul(double num, ref Accumulator overflow);
        public TPixel Div(double num);
        public Accumulator Convert();
        public TPixel Correct(ref Accumulator overflow);

        public void Extract(TChannel channels, ref TPixel value);
    }
}