namespace Labs.Core.Filtering
{
    public static class Kernel
    {
        public static double[,] CalculateLaplacian()
        {
            var matrix = new double[3, 3];
            for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                if (i == 1 && j == 1)
                    matrix[i, j] = 8;
                else
                    matrix[i, j] = -1;

            return matrix;
        }

        public static double[,] CalculateMean(Frame size)
        {
            var matrix = new double[size.Height, size.Width];
            double value = 1.0 / size.Square;

            for (int i = 0; i < size.Height; i++)
            for (int j = 0; j < size.Width; j++)
                matrix[i, j] = value;

            return matrix;
        }
    }
}