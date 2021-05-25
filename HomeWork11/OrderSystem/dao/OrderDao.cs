using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using Sean.Core.SnowFlake;

namespace OrderSystem
{
    [XmlInclude(typeof(List<Order>))]
    public class OrderDao
    {
        public OrderDao() { }
       
        //添加订单
        public int add(Order order)
        {
            try
            {
                using (var db = new OrderContext())
                {
                    db.Orders.Add(order);
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
        //根据ID删除订单
        public int remove(long id)
        {
            try
            {
                using (var db = new OrderContext())
                {
                    var order = db.Orders.FirstOrDefault(p => p.Id == id);
                    if (order != null)
                    {
                        db.Orders.Remove(order);
                        return db.SaveChanges();
                    }
                    else
                    {
                        Console.WriteLine("订单不存在!");
                        return 0;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("删除失败!");
                Console.WriteLine(e.Message);
                return 0;
            }
        }
        //根据ID修改订单
        public int update(long id, Order newOrder)
        {
            try
            {
                using (var db = new OrderContext())
                {
                    var curOrder = db.Orders.FirstOrDefault(p => p.Id == id);
                    newOrder.Id = curOrder.Id;
                    if (curOrder != null)
                    {
                        db.Orders.Remove(curOrder);
                        db.Orders.Add(newOrder);
                        return db.SaveChanges();
                    }
                    else
                    {
                        Console.WriteLine("订单不存在!");
                        return 0;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("修改失败!");
                Console.WriteLine(e.Message);
                return 0;
            }
        }
      //按ID查询
        public Order queryByOrderId(long id)
        {
            using (var db = new OrderContext())
            {
                var order =  db.Orders.Include("OrderDetails").Include("Buyer").FirstOrDefault(p => p.Id == id);
                foreach (OrderDetails orderDetails in order.OrderDetails)
                {
                    orderDetails.Goods = db.Goods.FirstOrDefault(p => p.Id == orderDetails.GoodsId);
                }
                return order;
            }
        }
        //按姓名查询
        public List<Order> queryByBuyerName(string buyerName) 
        {
            using (var db = new OrderContext())
            {
                var queryResult = db.Orders.Include("OrderDetails").Include("Buyer").Where(obj => obj.Buyer.Name == buyerName).OrderBy(p => p.Sum);
                if (queryResult != null)
                {
                    var orderList =  queryResult.ToList();
                    foreach (Order order in orderList)
                    {
                        foreach (OrderDetails orderDetails in order.OrderDetails)
                        {
                            orderDetails.Goods = db.Goods.FirstOrDefault(p => p.Id == orderDetails.GoodsId);
                        }
                    }
                    return orderList;
                }
                else
                {
                    return new List<Order>();
                }
            }
        }
        //按总金额进行排序
        public void sortByPrice()
        {
            using (var db = new OrderContext())
            {
                var orderList = db.Orders.Include("OrderDetails").Include("Buyer").ToList();
                foreach (Order order in orderList)
                {
                    foreach (OrderDetails orderDetails in order.OrderDetails)
                    {
                        orderDetails.Goods = db.Goods.FirstOrDefault(p => p.Id == orderDetails.GoodsId);
                    }
                }
                orderList.Sort((p1, p2) => p1.Sum.CompareTo(p2.Sum));
                foreach (Order order in orderList)
                {
                    Console.WriteLine(order);
                }
            }
        }

        //查询所有订单
        public List<Order> getAllOrders()
        {
            using (var db = new OrderContext())
            {
                var orderList = db.Orders.Include("OrderDetails").Include("Buyer").ToList();
                foreach(Order order in orderList)
                {
                    foreach(OrderDetails orderDetails in order.OrderDetails)
                    {
                        orderDetails.Goods = db.Goods.FirstOrDefault(p => p.Id == orderDetails.GoodsId);
                    }
                }
                return orderList;
            }
        }
    }
}
