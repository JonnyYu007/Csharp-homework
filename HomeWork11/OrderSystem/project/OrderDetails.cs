using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem
{
    public class OrderDetails
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }
        public long GoodsId { set; get; }
        [ForeignKey("GoodsId")]
        public Goods Goods { set; get; }
        public int GoodsNum { set; get; }
        public long OrderId { get; set; }
        public OrderDetails() { }
        public OrderDetails(long id, Goods good, int goodNum)
        {
            Id = id;
            Goods = good;
            GoodsNum = goodNum;
        }
        public override string ToString()
        {
            return Goods.ToString() + ",goodsNum:" + GoodsNum;
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
    }
}
