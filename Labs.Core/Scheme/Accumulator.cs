namespace Labs.Core.Scheme
{
    public struct Accumulator : IColor<Accumulator, byte>
    {
        public double K1;
        public double K2;
        public double K3;
        public double K4;

        public Accumulator Add(ref Accumulator other)
        {
            var value = this;
            value.K1 += other.K1;
            value.K2 += other.K2;
            value.K3 += other.K3;
            value.K4 += other.K4;
            return value;
        }

        public Accumulator Add(ref Accumulator other, ref Accumulator acc)
        {
            acc = default;
            return Add(ref other);
        }

        public Accumulator Subtract(ref Accumulator other)
        {
            var value = this;
            value.K1 -= other.K1;
            value.K2 -= other.K2;
            value.K3 -= other.K3;
            value.K4 -= other.K4;
            return value;
        }

        public Accumulator Subtract(ref Accumulator other, ref Accumulator acc)
        {
            acc = default;
            return Subtract(ref other);
        }

        public Accumulator Mul(double num)
        {
            var value = this;
            value.K1 *= num;
            value.K2 *= num;
            value.K3 *= num;
            value.K4 *= num;
            return value;
        }

        public Accumulator Mul(double num, ref Accumulator overflow)
        {
            overflow = default;
            return Mul(num);
        }

        public Accumulator Div(double num)
        {
            var value = this;
            value.K1 *= num;
            value.K2 *= num;
            value.K3 *= num;
            value.K4 *= num;
            return value;
        }


        public void Convert<TPixel, TChannel>(out TPixel pixel) where TPixel : struct, IColor<TPixel, TChannel>
        {
            pixel = default;
            pixel = pixel.Correct(ref this);
        }

        public Accumulator Correct(ref Accumulator overflow) =>
            Add(ref overflow);

        public Accumulator Convert() =>
            this;

        public void Extract(byte channels, ref Accumulator value)
        {
            if ((channels & 0x1) == 0x1)
                value.K1 = K1;
            if ((channels & 0x2) == 0x1)
                value.K2 = K2;
            if ((channels & 0x4) == 0x1)
                value.K3 = K3;
            if ((channels & 0x8) == 0x1)
                value.K4 = K4;
        }

        public int CompareTo(Accumulator other) =>
            (K1 + K2 + K3 + K4)
            .CompareTo(other.K1 + other.K2 + other.K3 + other.K4);
    }
}