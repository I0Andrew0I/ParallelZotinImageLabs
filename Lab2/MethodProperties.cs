using Labs.Core;

namespace Lab2
{
    public class MethodParameters
    {
        public MethodParameters(Filter filter, Frame frame, double[,] kernel)
        {
            Filter = filter;
            Frame = frame;
            Kernel = kernel;
        }

        public double[,] Kernel { get; set; }
        public Filter Filter { get; set; }
        public Frame Frame { get; set; }

        public void Deconstruct(out double[,] kernel, out Filter filter, out Frame frame)
        {
            kernel = Kernel;
            filter = Filter;
            frame = Frame;
        }
    }
}