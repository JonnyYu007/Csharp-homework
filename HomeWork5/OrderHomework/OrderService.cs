using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderHomework
{
    class OrderService
    {
        public List<Order> orderlist = new List<Order>();
        public List<Order> ReturnOrder()
        {
            return orderlist;
        }
        public void ShowOrder()
        {
            Console.WriteLine("订单编号" + "\t" + "订单客户" + "\t" + "订单总金额");
            foreach(Order order in orderlist)
            {
                Console.WriteLine(order.ToString());
            }
        }
        public void AddOrder(Order order)
        {
            if (orderlist.Contains(order)) throw new ArgumentException(order.id + "订单已存在");
            else orderlist.Add(order);
        }
        public void RemoveById(int id)
        {
            if (id < 0) throw new ArgumentException(id + "订单不存在");
            else orderlist.RemoveAt(id);
        }
        public void Modify(int id,Order order)
        {

            if (id >= 0 && order != null) orderlist[id] = order;
            else throw new ArgumentException("输入有误");
        }
        //默认订单排序
        public void Sort()
        {
            orderlist.Sort();
        }
        //使用lambda表达式排序
        public void sort(Func<Order,Order,int> func)
        {
            orderlist.Sort((order1, order2) => func(order1, order2));
        }
        //使用LINQ语言实现各种查询功能
        public List<Order> SearchByClient(string client_name)
        {
            var order = from o in orderlist where o.client == client_name orderby o.total_amount select o;
            List<Order> result = order.ToList();
            if (result == null) throw new ArgumentException("订单不存在");
            else return result;
        }
        public List<Order> SearchByMoney(double money)
        {
            var order = from o in orderlist where o.total_amount == money  orderby o.total_amount select o;
            List<Order> result = order.ToList();
            if (result == null) throw new ArgumentException("订单不存在");
            else return result;
        }
        public List<Order> SearchByTime(DateTime t)
        {
            var order = from o in orderlist where o.time == t orderby o.total_amount select o;
            List<Order> result = order.ToList();
            if (result == null) throw new ArgumentException("订单不存在");
            else return result;
        }
    }
}
