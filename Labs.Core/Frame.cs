using System;
using System.Collections.Generic;
using System.Linq;

namespace Labs.Core
{
    public record Frame
    {
        public int RW { get; }
        public int RH { get; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; }
        public int Height { get; }
        
        public Frame(int X, int Y, int Width, int Height)
        {
            this.X = X;
            this.Y = Y;
            this.Width = Width;
            this.Height = Height;
            RW = (Width - 1) / 2;
            RH = (Height - 1) / 2;
        }


        public virtual IEnumerable<int> IterateX(int y)
        {
            if (Math.Abs(y - Y) <= RH)
                return Enumerable.Range(X - RW, Width);

            return Enumerable.Empty<int>();
        }

        public virtual IEnumerable<int> IterateY(int x)
        {
            if (Math.Abs(x - X) <= RW)
                return Enumerable.Range(Y - RH, Height);

            return Enumerable.Empty<int>();
        }

        public void Deconstruct(out int X, out int Y, out int Width, out int Height)
        {
            X = this.X;
            Y = this.Y;
            Width = this.Width;
            Height = this.Height;
        }
    }

    public record EllipsoidsFrame(int X, int Y, int Width, int Height) : Frame(X, Y, Width, Height)
    {
        public override IEnumerable<int> IterateX(int y)
        {
            int min = X;
            int max = X;

            if (Math.Sqrt(y * y - Y * Y) <= RH)
            {
                for (int dx = -RW; dx <= RW; dx++)
                {
                    int x = X + dx;
                    if (Math.Sqrt(x * x - X * X) <= RW)
                    {
                        min = Math.Min(min, x);
                        max = Math.Max(max, x);
                    }
                }

                return Enumerable.Range(min, max - min);
            }

            return Enumerable.Empty<int>();
        }

        public override IEnumerable<int> IterateY(int x)
        {
            int min = Y;
            int max = Y;

            if (Math.Sqrt(x * x - X * X) <= RW)
            {
                for (int dy = -RH; dy <= RH; dy++)
                {
                    int y = Y + dy;
                    if (Math.Sqrt(y * y - Y * Y) <= RH)
                    {
                        min = Math.Min(min, y);
                        max = Math.Max(max, y);
                    }
                }

                return Enumerable.Range(min, max - min);
            }

            return Enumerable.Empty<int>();
        }
    }
}