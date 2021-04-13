using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderHomework
{
    public class OrderDetail
    {
        
        public DateTime trade_time;
        public double goods;
        public int  amount;
        public double Money { get => goods * amount; }
        public OrderDetail(double goods,int amount)
        {
            this.goods = goods;
            this.amount = amount;
            trade_time = DateTime.Now;
        }
        public override bool Equals(object obj)
        {
            OrderDetail a = obj as OrderDetail;
            return a != null && a.goods == this.goods && a.amount == this.amount;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override string ToString()
        {
            return $"OrderDetail goods'price:{this.goods},amount:{this.amount}";
        }
    }
    
}
