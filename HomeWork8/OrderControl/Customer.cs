using System;
using System.Collections.Generic;
using System.Text;

namespace OrderControl
{
    public class Customer
    {
        public string Name { get; set; }
        public Customer(string name)
        {
            Name = name;//设置客户名字
        }
        public override string ToString()
        {
            return "customer's name:" + Name;
        }
    }
}
