using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test2
{
    class Rectangle:Shape
    {
        double length;
        double width;
        public Rectangle(double a,double b)
        {
            this.length = a;
            this.width = b;
        }
        public double Length
        {
            get;set;
        }
        public double Width
        {
            get;set;
        }
        public override bool IsLegal()
        {
            //throw new NotImplementedException();
            return Length > 0 && Width > 0;
        }
        public override double CalArea()
        {
            if (!IsLegal())
                throw new NotImplementedException("矩形的形状无效，无法计算面积");
            else
                return Length * Width;
        }
    }
    class Square:Shape
    {
        double side;
        public Square(double a)
        {
            this.side = a;
        }
        public double Side
        {
            get;set;
        }
        public override bool IsLegal()
        {
            return Side > 0;
        }
        public override double CalArea()
        {

            if (!IsLegal())
                throw new NotImplementedException("正方形形状无效");
            else
                return Side * Side;
        }
    }
    class Triangle : Shape
    {
        double[] edges = new double[3];
        public Triangle(double x,double y,double z)
        {
            double[] newedges = new double[3] { x, y, z };
            this.edges = newedges;
        }
        public override bool IsLegal()
        {
            //throw new NotImplementedException();
            return edges[0] > 0 && edges[1] > 0 && edges[2] > 0 && edges[0] + edges[1] > edges[2] && edges[0] + edges[2] > edges[1] && edges[1] + edges[2] > edges[0];
        }
        public override double CalArea()
        {
            if(!IsLegal())
            throw new NotImplementedException("三角形的形状无效");
            double p = (edges[0] + edges[1] + edges[2]) / 3;
            return Math.Sqrt(p * (p - edges[0]) * (p - edges[1]) * (p - edges[2]));
        }
    }
}
