using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderControl
{
    public class Order
    {
        public int ID { get; set; }
        public Customer Customer { get; set; }
        public OrderDetails[] OrderDetails { get; set; }
        public double Sum { get; set; }

        public Order(int id,Customer customer,OrderDetails[] orderDetails)
        {
            ID = id;
            Customer = customer;
            OrderDetails = orderDetails;
            this.calculate();
        }
        //public override string ToString()
        //{
        //    return base.ToString();
        //}
        public void calculate()//计算订单总金额
        {
            foreach(OrderDetails orderDetails in OrderDetails)
            {
                Sum += (orderDetails.Goods.Price) * orderDetails.GoodsNum;
            }
        }
    }
}
