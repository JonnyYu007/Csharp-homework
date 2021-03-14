using System;

namespace test4
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] matrix = { { 1, 2, 3, 4 }, { 5, 1, 2, 3 }, { 9, 5, 1, 2 } };
            bool judge(int[,] n )
            {
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        if (i != 0 && j != 0 && matrix[i, j] != matrix[i - 1, j - 1]) return false;
                    }
                }
                return true;
            }
            judge(matrix);

        }
    }
}
