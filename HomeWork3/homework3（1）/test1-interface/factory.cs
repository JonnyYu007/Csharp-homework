using System;
using System.Collections.Generic;
using System.Text;
using homerwork3;
namespace test1
{
    class Factory
    {
        public static double SelectShape()
        {
            Random r = new Random();
            int number = r.Next(1, 3);
            int r1 = r.Next(1, 10);
            int r2 = r.Next(1, 10);
            int r3 = r.Next(1, 10);
            switch (number)
            {
                case 1:
                    {
                        Shape rec = new Rectangle(r1, r2);
                        if(!rec.IsLegal())
                        {
                            r1 = r.Next(1, 10);
                            r2 = r.Next(1, 10);
                            rec = new Rectangle(r1, r2);
                        }
                        return rec.CalArea();
                    }
                    break;
                case 2:
                    {
                        Shape sq = new Sqare(r1);
                        if (!sq.IsLegal())
                        {
                            r1 = r.Next(1, 10);
                           
                            sq = new Sqare(r1);
                        }
                        return sq.CalArea();
                    }
                    break;
                case 3:
                    {
                        Shape tri = new Triangle(r1, r2,r3);
                        if (!tri.IsLegal())
                        {
                            r1 = r.Next(1, 10);
                            r2 = r.Next(1, 10);
                            r3 = r.Next(1, 10);
                            tri = new Triangle(r1, r2,r3);
                        }
                        return tri.CalArea();
                    }
                    break;
                default:
                    return 0;
            }

        }
    }
}
