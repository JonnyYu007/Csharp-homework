using System;
using System.Collections.Generic;
using System.Text;
using test1
namespace homerwork3
{
    
    class Program
    {
        public static void main()
        {
            double sum = 0;
            for(int s = 0; s < 10; s++)
            {
                sum += Factory.SelectShape();
            }
            Console.WriteLine("随机产生10个对象的面积之和为：" + sum);
        }
    }
}
