using System;

namespace test3
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] a = new int[101];
            for(int i=0;i<101;i++)
            {
                a[i] = 0;
            }
            for(int i = 2; i < 101; i++)
            {
                for (int j = 2; j < 51; j++)
                {
                    for (int n = 2; n < 51; n++)
                    {
                        if (i % (n * j) == 0) a[i] = 1;
                    }
                }
            }
            for(int k = 2; k < 101; k++)
            {
                if (a[k] == 0) Console.Write(k + " ");
            }
        }
    }
}
