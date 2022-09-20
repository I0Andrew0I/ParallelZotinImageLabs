namespace Labs.Core.Scheme
{
    public interface IColor<TPixel, TChannel>
    {
        public TPixel Add(TPixel other);
        public TPixel Mul(double num);
        public TPixel Div(double num);

        public void Extract(TChannel channels, ref TPixel value);
    }
}