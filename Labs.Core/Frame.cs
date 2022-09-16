namespace Labs.Core
{
    public record Frame(int X, int Y, int Width, int Heigth)
    {
        public int RW { get; } = (Width - 1) / 2;
        public int RH { get; } = (Heigth - 1) / 2;
    }
}