using System;
using System.Collections.Generic;
using System.Text;

namespace OrderControl
{
    public class OrderDetails
    {
        public int GoodsNum { get; set; }
        public Goods Goods { get; set; }
        public OrderDetails(Goods goods,int goodsnum)
        {
            Goods = goods;
            GoodsNum = goodsnum;
        }
        public override bool Equals(object obj)
        {
            return obj is OrderDetails details &&
                   EqualityComparer<Goods>.Default.Equals(Goods, details.Goods) &&
                   GoodsNum == details.GoodsNum;
        }
        public override int GetHashCode()
        {
            int hashCode = -1633355496;
            hashCode = hashCode * -1521134295 + EqualityComparer<Goods>.Default.GetHashCode(Goods);
            hashCode = hashCode * -1521134295 + GoodsNum.GetHashCode();
            return hashCode;
        }
        public override string ToString()
        {
            return "goods'name:" + Goods.Name + " goods'price:" + Goods.Price + " goods'number:" + GoodsNum;
        }
    }
}
