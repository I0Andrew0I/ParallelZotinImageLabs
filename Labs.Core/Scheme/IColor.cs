using System;

namespace Labs.Core.Scheme
{
    public interface IColor<TPixel, TChannel>: IComparable<TPixel>
    {
        public TPixel Add(TPixel other);
        public TPixel Add(TPixel other, out TPixel overflow);
        public TPixel Subtract(TPixel other);
        public TPixel Subtract(TPixel other, out TPixel overflow);
        public TPixel Mul(double num);
        public TPixel Div(double num);

        public void Extract(TChannel channels, ref TPixel value);
    }
}