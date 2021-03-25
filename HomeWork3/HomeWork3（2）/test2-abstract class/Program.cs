using System;

namespace test2
{
    class Program
    {
        static void Main(string[] args)
        {
            double sum=0;
            
            for(int i = 0; i < 10; i++)
            {
                sum += factory.CreateRandomShape().CalArea();
            }
            Console.WriteLine("利用工厂模式随机创建的10个对象面积之和为:" + sum);
        }
    }
}
