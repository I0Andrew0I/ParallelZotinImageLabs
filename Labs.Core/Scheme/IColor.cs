using System;

namespace Labs.Core.Scheme
{
    public interface IColor<TPixel, TChannel> : IComparable<TPixel> where TPixel : struct
    {
        public TPixel Add(in TPixel other);
        public TPixel Add(in TPixel other, ref Accumulator overflow);
        public TPixel Subtract(in TPixel other);
        public TPixel Subtract(in TPixel other, ref Accumulator overflow);
        public TPixel Mul(in double num);
        public TPixel Mul(in double num, ref Accumulator overflow);
        public TPixel Div(in double num);
        public TPixel Correct(in Accumulator overflow);

        public void Extract(in TChannel channels, ref TPixel value);
    }
}