using System;
using System.Runtime.CompilerServices;

namespace Labs.Core.Scheme
{
    public record struct HLSA : IColor<HLSA, HLSA.Channel>
    {
        [Flags]
        public enum Channel : byte
        {
            Undefined = 0,
            Alpha = 1,
            Hue = 2,
            Lightness = 4,
            Saturation = 8,
            HLS = 14,
            All = 15
        }

        /// <summary>
        /// Hue (0, 360)
        /// </summary>
        public double H;

        /// <summary>
        /// Lightness (0, 1)
        /// </summary>
        public double L;

        /// <summary>
        /// Saturation (0, 1)
        /// </summary>
        public double S;

        /// <summary>
        /// Alpha (0, 1)
        /// </summary>
        public double A;

        public HLSA(double h, double l, double s, double a = 1)
        {
            H = h;
            L = l;
            S = s;
            A = a;
        }

        public ARGB ToARGB()
        {
            ARGB value = default;
            ToARGB(ref value);
            return value;
        }

        public void ToARGB(ref ARGB value)
        {
            if (S == 0)
            {
                // ахроматический цвет (шкала серого)
                var v = (byte) Math.Round(L * 255.0);
                value = new ARGB(v, v, v, (byte) (A * 255));
            }

            double q = (L < 0.5) ? (L * (1.0 + S)) : (L + S - (L * S));
            double p = (2.0 * L) - q;

            double Hk = H / 360.0;
            double[] T = new double[3];
            T[0] = Hk + (1.0 / 3.0); // Tr
            T[1] = Hk; // Tb
            T[2] = Hk - (1.0 / 3.0); // Tg

            for (int a = 0; a < 3; a++)
            {
                if (T[a] < 0) T[a] += 1.0;
                if (T[a] > 1) T[a] -= 1.0;

                if ((T[a] * 6) < 1)
                {
                    T[a] = p + ((q - p) * 6.0 * T[a]);
                }
                else if ((T[a] * 2.0) < 1) //(1.0/6.0)<=T[a] && T[a]<0.5
                {
                    T[a] = q;
                }
                else if ((T[a] * 3.0) < 2) // 0.5<=T[a] && T[a]<(2.0/3.0)
                {
                    T[a] = p + (q - p) * ((2.0 / 3.0) - T[a]) * 6.0;
                }
                else T[a] = p;
            }

            value.R = (byte) Math.Round(T[0] * 255.0);
            value.G = (byte) Math.Round(T[1] * 255.0);
            value.B = (byte) Math.Round(T[2] * 255.0);
            value.A = (byte) Math.Round(A * 255);
        }

        public HLSA Add(ref HLSA other)
        {
            HLSA value = this;
            value.H += other.H;
            value.L += other.L;
            value.S += other.S;
            return value;
        }

        public HLSA Add(ref HLSA other, ref Accumulator overflow)
        {
            double h = H + other.H;
            double l = L + other.L;
            double s = S + other.S;

            return GetOverflow(h, l, s, ref overflow);
        }

        public HLSA Subtract(ref HLSA other)
        {
            HLSA value = this;
            value.H -= other.H;
            value.L -= other.L;
            value.S -= other.S;
            return value;
        }

        public HLSA Subtract(ref HLSA other, ref Accumulator overflow)
        {
            double h = H - other.H;
            double l = L - other.L;
            double s = S - other.S;

            return GetOverflow(h, l, s, ref overflow);
        }

        public HLSA Mul(double num)
        {
            HLSA value = this;
            value.H *= num;
            value.L *= num;
            value.S *= num;
            return value;
        }

        public HLSA Mul(double num, ref Accumulator overflow)
        {
            double h = H * num;
            double l = L * num;
            double s = S * num;

            return GetOverflow(h, l, s, ref overflow);
        }

        public HLSA Div(double num)
        {
            HLSA value = this;
            value.H /= num;
            value.L /= num;
            value.S /= num;
            return value;
        }

        public HLSA Correct(ref Accumulator overflow)
        {
            HLSA value = this;
            value.H = Math.Clamp(value.H + overflow.K1, 0, 360);
            value.L = Math.Clamp(value.L + overflow.K2, 0, 1);
            value.S = Math.Clamp(value.S + overflow.K3, 0, 1);
            value.A = Math.Clamp(value.A + overflow.K4, 0, 1);
            return value;
        }

        public void Extract(Channel channels, ref HLSA value)
        {
            if ((channels & Channel.Hue) != 0)
                value.H = H;

            if ((channels & Channel.Lightness) != 0)
                value.L = L;

            if ((channels & Channel.Saturation) != 0)
                value.S = S;

            if ((channels & Channel.Alpha) != 0)
                value.A = A;
        }

        public int CompareTo(HLSA other)
        {
            int hComparison = H.CompareTo(other.H);
            if (hComparison != 0) return hComparison;
            int lComparison = L.CompareTo(other.L);
            if (lComparison != 0) return lComparison;
            return S.CompareTo(other.S);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static HLSA GetOverflow(double h, double l, double s, ref Accumulator overflow)
        {
            HLSA value = default;
            value.H = Math.Clamp(h, 0, 360);
            value.L = Math.Clamp(l, 0, 1);
            value.S = Math.Clamp(l, 0, 1);

            overflow.K1 += h - value.H;
            overflow.K2 += l - value.L;
            overflow.K3 += s - value.S;

            return value;
        }
    }
}