using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrderHomework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
namespace OrderHomework.Tests
{
    [TestClass()]
    public class OrderServiceTests
    {
        Order o1 = new Order(1, "xiaohong");
        Order o2 = new Order(2, "xiaoli");
        List<Order> order_test = new List<Order>();
        OrderService servicetest = new OrderService();
        List<Order> test = new List<Order>();
        [TestMethod()]
        public void ReturnOrderTest()
        {
            order_test.Add(o1);
           test = OrderService.ReturnOrder(order_test);
            CollectionAssert.Equals(test, order_test);
        }

      

        [TestMethod()]
        public void AddOrderTest()
        {
           
            test.Add(o1);
            servicetest.AddOrder(o1);
            CollectionAssert.Equals(test, order_test);
        }

        [TestMethod()]
        public void RemoveByIdTest()
        {
            order_test.Add(o1);
            order_test.Add(o2);
            test.Add(o1);
            servicetest.RemoveById(2);
            CollectionAssert.Equals(test, order_test);
        }

        [TestMethod()]
        public void ModifyTest()
        {
            test.Add(o1);
            servicetest.Modify(1, o1);
            CollectionAssert.Equals(test, order_test);
        }

        [TestMethod()]
        public void SortTest()
        {
            test.Add(o1);
            test.Add(o2);
            test.Sort();
            order_test.Add(o1);
            order_test.Add(o2);
            servicetest.Sort();
            CollectionAssert.Equals(test, order_test);
        }

        [TestMethod()]
        public void sortTest()
        {
            test.Add(o1);
            test.Add(o2);
            order_test.Add(o1);
            order_test.Add(o2);
            servicetest.sort((o1, o2) => o2.TotalAmount.CompareTo(o1.TotalAmount))
            test.Sort();
            CollectionAssert.Equals(test, order_test);
        }

        [TestMethod()]
        public void SearchByClientTest()
        {
            order_test.Add(o1);
            List<Order> a = servicetest.SearchByClient("xiaohong");
            CollectionAssert.Equals(o1, a);
        }

        [TestMethod()]
        public void SearchByMoneyTest()
        {
            o1.details = new List<OrderHomework.OrderDetail>();
            OrderDetail a = new OrderDetail(2, 50);
            o1.details.Add(a);
            order_test.Add(o1);
            List<Order>b= servicetest.SearchByMoney(100);
            CollectionAssert.Equals(o1, b);
        }

        [TestMethod()]
        public void SearchByTimeTest()
        {
            o1.details = new List<OrderHomework.OrderDetail>();
            OrderDetail a = new OrderDetail(0, 0);
            o1.details.Add(a);
            order_test.Add(o1);
      
            List<Order> b = servicetest.SearchByTime(a.trade_time);
            CollectionAssert.Equals(o1, b);
        }

        [TestMethod()]
        public void ExportTest()
        {
            order_test.Add(o1);
            order_test.Add(o2);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Order));
            using (FileStream fs = new FileStream("a.xml", FileMode.Create))
            {
                xmlSerializer.Serialize(fs, order_test);
            }
            servicetest.Export(order_test);
            CollectionAssert.Equals("a.xml", "s.xml");
        }

        [TestMethod()]
        public void ImportTest()
        {
            List<Order> newlist2 = new List<Order>();
            List<Order> newlist = new List<Order>();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Order));
            using (FileStream fs = new FileStream("a.xml", FileMode.Open))
            {
                newlist2 = (List<Order>)xmlSerializer.Deserialize(fs);
            }
            newlist=servicetest.Import("a.xml");
            CollectionAssert.Equals(newlist2, newlist);
        }

      
    }
}