using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderHomework
{
    public class Order : IComparable
    {
        public List<OrderDetail> details = new List<OrderDetail>();
        public DateTime time;
        public int id;//订单编号
        public string client;//订单的客户
        public double total_amount
        {
            get => details.Sum(a => a.money);
        }
        public Order(int id, string client)
        {
            this.id = id;
            this.client = client;
            time = DateTime.Now;
        }
        //比较器
        public int CompareTo(object obj)
        {
            Order a = obj as Order;
            return this.id - a.id;
         }            
        public void Add(OrderDetail detail)
        {
            if (details.Contains(detail)||detail==null) throw new ArgumentException("要加入的订单已存在或为空");
            else details.Add(detail);
        }
        public void Remove(int num)
        {
            if (num < 0) throw new ArgumentException("订单不存在");
            else details.RemoveAt(num);
        }

        
        public override bool Equals(object obj)
        {
            Order details = obj as Order;
            return details != null && this.id == details.id;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override string ToString()
        {
            String r = $"id:{this.id},client:{this.client}";
            details.ForEach(a => r += "\t"+a);
            return r;
        }
    }
}
