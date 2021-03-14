using System;

namespace test2
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] b = { 1, 2, 3, 4, 5 };
            double sum = 0, ave = 0;
            for(int i=0;i<5;i++)
            {
                sum += b[i];
            }
            Convert.ToDouble(sum);
            ave = (double)(sum / 5);
            Console.WriteLine("数组的和是：" + sum);
            Console.WriteLine("平均值是：" + ave);
            int c = b[0];
            for(int i=0;i<5;i++)
            {
                if(b[i+1]>=b[0])
                {
                    b[0] = b[i + 1];
                }
            }
            Console.WriteLine("最大值是：" + b[0]);
            b[0] = c;
            for(int i=0;i<5;i++)
            {
                if(b[i+1]<=b[0])
                {
                    b[0] = b[i + 1];
                }
            }
            Console.WriteLine("最小值是：" + b[0]);

        }
    }
}
