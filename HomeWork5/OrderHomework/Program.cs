using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace OrderHomework
{
    class Program
    {
        static void Main(string[] args)
        {
            
            try
            {
                OrderService service = new OrderService();

                Order order1 = new Order(1, "liming");
                Order order2 = new Order(2, "wangwei");
                Order order3 = new Order(3, "liufeng");

                order1.Add(new OrderDetail(10, 2));
                order2.Add(new OrderDetail(20, 3));
                order3.Add(new OrderDetail(15, 4));

                service.AddOrder(order1);
                service.AddOrder(order2);
                service.AddOrder(order3);

                service.Sort((order1, order2) => order1.total_amount - order2.total_amount);

                List<Order> orders = service.ReturnOrder();

                orders = service.SearchByClient("liming");
                orders.ForEach(o => Console.WriteLine(o));

                orders = service.SearchByMoney(20);
                orders.ForEach(o => Console.WriteLine(o));
            }
            catch(Exception e)
            {
                throw new Exception();
            }
        }
    }
}
