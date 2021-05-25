using Sean.Core.SnowFlake;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem
{
    class GoodsDao
    {
        public GoodsDao() { }
        
        public Goods queryByGoodsId(long id)
        {
            using (var db = new OrderContext())
            {
                return db.Goods.FirstOrDefault(p => p.Id == id);
            }
        }
        public List<Goods> getAllGoods()
        {
            using (var db = new OrderContext())
            {
                return db.Goods.ToList();
            }
        }
        public List<OrderDetails> getAllOrderDetails()
        {
            using (var db = new OrderContext())
            {
                return db.OrderDetails.Include("Goods").ToList();
            }
        }
    }
}
