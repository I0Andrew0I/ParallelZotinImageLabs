namespace Labs.Core.Scheme
{
    public struct Accumulator : IColor<Accumulator, byte>
    {
        public double K1;
        public double K2;
        public double K3;
        public double K4;

        public Accumulator Add(in Accumulator other)
        {
            var value = this;
            value.K1 += other.K1;
            value.K2 += other.K2;
            value.K3 += other.K3;
            value.K4 += other.K4;
            return value;
        }

        public Accumulator Add(in Accumulator other, ref Accumulator acc)
        {
            acc = default;
            return Add(other);
        }

        public Accumulator Subtract(in Accumulator other)
        {
            var value = this;
            value.K1 -= other.K1;
            value.K2 -= other.K2;
            value.K3 -= other.K3;
            value.K4 -= other.K4;
            return value;
        }

        public Accumulator Subtract(in Accumulator other, ref Accumulator acc)
        {
            acc = default;
            return Subtract(other);
        }

        public Accumulator Mul(in double num)
        {
            var value = this;
            value.K1 *= num;
            value.K2 *= num;
            value.K3 *= num;
            value.K4 *= num;
            return value;
        }

        public Accumulator Mul(in double num, ref Accumulator overflow)
        {
            overflow = default;
            return Mul(num);
        }

        public Accumulator Div(in double num)
        {
            var value = this;
            value.K1 *= num;
            value.K2 *= num;
            value.K3 *= num;
            value.K4 *= num;
            return value;
        }

        public Accumulator Correct(in Accumulator overflow) =>
            Add(overflow);

        public void Extract(in byte channels, ref Accumulator value)
        {
            if ((channels & 1) == 1u)
                value.K1 = K1;
            if ((channels & 2) == 1u)
                value.K2 = K2;
            if ((channels & 4) == 1u)
                value.K3 = K3;
            if ((channels & 8) == 1u)
                value.K4 = K4;
        }

        public int CompareTo(Accumulator other) =>
            (K1 + K2 + K3 + K4)
            .CompareTo(other.K1 + other.K2 + other.K3 + other.K4);
    }
}