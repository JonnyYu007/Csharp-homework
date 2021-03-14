using System;

namespace test_of_class
{
    class Program
    {

        static void Main(string[] args)
        {
            //var a = "tab";
            //Console.WriteLine(a);
            int a;
            Console.WriteLine("请输入数据：");
            a = Console.Read();
            bool judge(int n)
            {
                if (n < 2) return false;
                for(int i=2;i<n;i++)
                {
                    if ((n % i) == 0) return false;
                }
                return true;
            }
            Console.WriteLine("分解为质数因子：");
            for(int j=2;j<=a;j++)
            {
                while ((a % j) == 0)
                {
                    if (judge(j)) Console.Write(j+" ");
                    a = a / j;
                }
            }
        }
    }
}
