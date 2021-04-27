using System;
using System.Collections.Generic;
using System.Text;

namespace OrderControl
{
    public class Goods
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public Goods(string name,double price)
        {
            Name = name;
            Price = price;
        }
        public override string ToString()
        {
            return "Goods'name:" + Name + " Goods'price:" + Price;
        }
    }
}
