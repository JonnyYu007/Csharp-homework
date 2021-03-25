using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test2
{
    class factory
    {
        static Random random = new Random();
        public static Shape CreateShape(string type,params double[] edges)
        {
            Shape result = null;
            switch (type)
            {
                case "rectangle":
                    result = new Rectangle(edges[0],edges[1]);
                    break;
                case "square":
                    result = new Square(edges[0]);
                    break;
                case "triangle":
                    result = new Triangle(edges[0], edges[1], edges[2]);
                    break;
                default:throw new InvalidOperationException("错误的类型:" + type);
            }
            if (!result.IsLegal())
                throw new InvalidOperationException("参数传入错误");
            return result;
        }
        public static Shape CreateRandomShape()
        {
            
            int type = random.Next(0, 3);
            Shape result = null;
            try
            {
                switch(type)
                {
                    case 0:
                        result = CreateShape("rectangle", random.Next(10),random.Next(10));
                        break;
                    case 1:
                        result = CreateShape("square", random.Next(10));
                        break;
                    case 2:
                        result = CreateShape("triangle", random.Next(10), random.Next(10), random.Next(10));
                        break;
                }
            }
            catch { throw new Exception("输入有错误"); }
            return result;
        }
    }
}
