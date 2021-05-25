using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sean.Core.SnowFlake;


namespace OrderSystem
{
    class BuyerDao
    {
        
        public BuyerDao() { }
        public int add(Buyer buyer)
        {
            try
            {
                using (var db = new OrderContext())
                {
                    db.Buyers.Add(buyer);
                    return db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("添加失败!");
                Console.WriteLine(e.Message);
                return 0;
            }
        }

        public List<Buyer> getAllBuyers()
        {
            using (var db = new OrderContext())
            {
                return db.Buyers.ToList();
            }
        }
    }
}
