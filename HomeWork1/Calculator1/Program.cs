using System;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            string n = "";
            double a, b;
            double s = 0;
            char c;
            Console.Write("请输入操作数1:");
            n = Console.ReadLine();
            a = Double.Parse(n);
            Console.Write("请输入操作数2:");
            n = Console.ReadLine();
            b = Double.Parse(n);
            Console.Write("请选择运算符(+,-,*,/,%):");
            c = (char)Console.Read();
            switch (c)
            {
                case '+':
                    s = a + b;
                    break;
                case '-':
                    s = a - b;
                    break;
                case '*':
                    s = a * b;
                    break;
                case '/':
                    s = a / b;
                    break;
                case '%':
                    s = a % b;
                    break;
                default:
                    Console.WriteLine("您输入的运算符不正确！");            
                    break;
            }
            Console.WriteLine("运算的结果是:" + s);
            Console.WriteLine("谢谢使用！");
        }
    }
}
