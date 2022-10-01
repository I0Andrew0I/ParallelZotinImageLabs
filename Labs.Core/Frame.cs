using System;

namespace Labs.Core
{
    public record Frame
    {
        protected static readonly (int, int) Empty = (0, 0);

        public int RW { get; }
        public int RH { get; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; }
        public int Height { get; }

        private int? _square;

        public Frame(int X, int Y, int Width, int Height)
        {
            this.X = X;
            this.Y = Y;
            this.Width = Width;
            this.Height = Height;
            RW = (Width - 1) / 2;
            RH = (Height - 1) / 2;
        }

        public int Square => _square ??= CalculateSquare();

        public virtual (int from, int to) IterateX(int y)
        {
            Range r = new Range(0, 3);

            if (Math.Abs(y - Y) <= RH)
                return (X - RW, X + RW);

            return Empty;
        }

        public virtual (int from, int to) IterateY(int x)
        {
            if (Math.Abs(x - X) <= RW)
                return (Y - RH, Y + RH);

            return Empty;
        }

        protected virtual int CalculateSquare() =>
            Width * Height;
    }

    public record EllipsisFrame : Frame
    {
        private readonly float powRW;
        private readonly float powRH;

        public EllipsisFrame(int X, int Y, int Width, int Height) : base(X, Y, Width, Height)
        {
            powRW = RW * RW;
            powRH = RH * RH;
        }

        public override (int from, int to) IterateX(int y)
        {
            int min = X;
            int max = X;
            float yOffset = MathF.Pow(y - Y, 2) / powRH;

            for (int dx = -RW; dx <= RW; dx++)
            {
                if (MathF.Pow(dx, 2) / powRW + yOffset > 1)
                    continue;

                int x = X + dx;
                min = Math.Min(min, x);
                max = Math.Max(max, x);
            }

            return (min, max);
        }

        public override (int from, int to) IterateY(int x)
        {
            int min = Y;
            int max = Y;

            float xOffset = MathF.Pow(x - X, 2) / powRW;
            for (int dy = -RH; dy <= RH; dy++)
            {
                if (xOffset + MathF.Pow(dy, 2) / powRH > 1)
                    continue;

                int y = Y + dy;
                min = Math.Min(min, y);
                max = Math.Max(max, y);
            }

            return (min, max);
        }

        protected override int CalculateSquare()
        {
            int square = 0;
            (int yfrom, int yto) = IterateY(X);
            for (int y = yfrom; y <= yto; y++)
            {
                (int xfrom, int xto) = IterateX(y);
                for (int x = xfrom; x <= xto; x++)
                    square++;
            }

            return square;
        }
    }
}