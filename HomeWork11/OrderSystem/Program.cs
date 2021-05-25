using Sean.Core.SnowFlake;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem
{
    class Program
    {
        private readonly static IdWorker worker = new IdWorker(1, 1);
        private readonly static OrderDao orderDao = new OrderDao();
        private readonly static GoodsDao goodsDao = new GoodsDao();
        private readonly static BuyerDao buyerDao = new BuyerDao();
        static void Main(string[] args)
        {
            try
            {
                using (var db = new OrderContext())
                {
                    Random rd = new Random();
                   
                    Console.WriteLine("所有订单：");
                    foreach (Order order in orderDao.getAllOrders())
                    {
                        Console.WriteLine(order);
                    }

                    Console.Write("请输入查询的买家：");
                    string str = Console.ReadLine();
                    foreach (Order order in orderDao.queryByBuyerName(str))
                    {
                        Console.WriteLine(order);
                    }

                    Console.Write("请输入查询的订单编号：");
                    long id = long.Parse(Console.ReadLine());
                    Console.WriteLine(orderDao.queryByOrderId(id));

                    Console.Write("请输入删除的订单编号：");
                    long deleteId = long.Parse(Console.ReadLine());
                    orderDao.remove(deleteId);

                    Console.WriteLine("已删除"+deleteId+"号订单");
                    foreach (Order order in orderDao.getAllOrders())
                    {
                        Console.WriteLine(order);
                    }

                    Console.Write("请输入修改的订单编号：");
                    long updateId = long.Parse(Console.ReadLine());
                    List<OrderDetails> orderDetailsList4 = new List<OrderDetails>();

                    for (int i = 0; i < 3; ++i)
                    {
                        Goods goods = new Goods(worker.NextId(), GetRand(5, false, false, true, false, "123456"), rd.NextDouble() * 50);
                        OrderDetails orderDetails = new OrderDetails(worker.NextId(), goods, rd.Next(0, 15));
                        orderDetailsList4.Add(orderDetails);
                    }
                    //修改订单
                    Order order4 = new Order(worker.NextId(), new Buyer(worker.NextId(), GetRand(3, true, false, true, false, "123456")), orderDetailsList4);
                    orderDao.update(updateId, order4);
                    Console.WriteLine("已修改" + updateId + "号订单");
                    foreach (Order order in orderDao.getAllOrders())
                    {
                        Console.WriteLine(order);
                    }
                    Console.WriteLine("按总价格排序后的所有订单：");
                    orderDao.sortByPrice();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }  
            Console.ReadKey();
        }

        public static string GetRand(int length, bool useNum, bool useLow, bool useUpp, bool useSpe, string custom)
        {
            byte[] b = new byte[4];
            new System.Security.Cryptography.RNGCryptoServiceProvider().GetBytes(b);
            Random r = new Random(BitConverter.ToInt32(b, 0));
            string s = null, str = custom;
            if (useNum == true) { str += "0123456789"; }
            if (useLow == true) { str += "abcdefghijklmnopqrstuvwxyz"; }
            if (useUpp == true) { str += "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; }
            if (useSpe == true) { str += "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~"; }
            for (int i = 0; i < length; i++)
            {
                s += str.Substring(r.Next(0, str.Length - 1), 1);
            }
            return s;
        }
    }
}
