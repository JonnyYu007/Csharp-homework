using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem
{
    public class Order
    {
        public Order() { }
        public Order(long id, Buyer buyer, List<OrderDetails> orderDetails)
        {
            Id = id;
            Buyer = buyer;
            OrderDetails = orderDetails;
            Sum = 0;
            this.calculate();
        }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }
        public long BuyerId { get; set; }
        [ForeignKey("BuyerId")]
        public Buyer Buyer { get; set; }
        
        public List<OrderDetails> OrderDetails { get; set; }
        public double Sum { set; get; }
        public override string ToString()
        {
            StringBuilder details = new StringBuilder();
            //序号
            int i = 1;
            foreach (OrderDetails orderDetail in OrderDetails)
            {
                details.Append(i + "--");
                details.Append(orderDetail.ToString());
                details.Append("\n");
                ++i;
            }
            return "orderId:" + Id
                + ",buyerName:" + Buyer.Name
                + ",$sum:" + Sum
                + "\n" + details;
        }
        public void calculate()                                   //计算订单总金额
        {
            foreach (OrderDetails orderDetail in OrderDetails)
            {
                Sum += (orderDetail.Goods.Price) * (orderDetail.GoodsNum);
            }
        }

        public override bool Equals(object obj)
        {
            return obj is Order order &&
                   Id == order.Id &&
                   BuyerId == order.BuyerId &&
                   EqualityComparer<Buyer>.Default.Equals(Buyer, order.Buyer) &&
                   EqualityComparer<List<OrderDetails>>.Default.Equals(OrderDetails, order.OrderDetails) &&
                   Sum == order.Sum;
        }

        public override int GetHashCode()
        {
            int hashCode = 1808312084;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + BuyerId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Buyer>.Default.GetHashCode(Buyer);
            hashCode = hashCode * -1521134295 + EqualityComparer<List<OrderDetails>>.Default.GetHashCode(OrderDetails);
            hashCode = hashCode * -1521134295 + Sum.GetHashCode();
            return hashCode;
        }
    }
}
