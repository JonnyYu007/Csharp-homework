using System;
namespace homework3
{
    public interface Shape
    {
        double CalArea();
        bool IsLegal();

    }

    public class Rectangle: Shape
    {
    
        private double length;
        private double width;
        public Rectangle(double a,double b)
        {
            this.length = a;
            this.width = b;
        }
        public double CalArea(double length,double width)
        {
            return length * width;
        }
        public bool IsLegal(double length,double width)
        {
            return length > 0 && width > 0;
        }
    }
    public class Square:Shape
    {
        private double s;
        
        public Square(double a)
        {
            this.s = a;
        }
        public double CalArea(double w)
        {
            return s* s;
        }
        public bool IsLegal(double w)
        {
            return s> 0;
               
        }
    }
    public class Triangle:Shape
    {
        double x;
        double y;
        double z;
        public Triangle(double a, double b, double c)
        {
            this.x = a;
            this.y = b;
            this.z= c;
        }
        public double CalArea(double x, double y, double z)
        {
            double p = (x + y + z) / 2.0;
            return Math.Sqrt(p * (p - x) * (p - y) * (p - z));
        }
        public bool IsLegal(double x, double y, double z)
        {
            return x + y > z && x + z > y && y + z > x && x > 0 && y > 0 && z > 0;
            
        }
    }
    
}
