using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
namespace OrderControl
{
    [XmlInclude(typeof(List<Order>))]
    public class OrderService
    {
        private List<Order> orders = new List<Order>();
        public List<Order> Orders { get { return this.orders; } }
        public OrderService(List<Order> orders)
        {
            this.orders = orders;
        }
        //添加订单
        public void addOrder(Order order)
        {
            if (!order.Equals(queryByOrderID(order.ID)))
            {
                orders.Add(order);
            }
            else { Console.WriteLine("要添加的订单已存在！"); }
        }
        //根据订单号删除订单
        public void deleteByID(int id)
        {
            foreach(Order order in orders)
            {
                if (order.ID == id)
                {
                    orders.Remove(order);
                    break;
                }
            }
        }
        //根据订单号修改订单
        public void changeByID(int id,Order newOrder)
        {
            try
            {
                foreach (Order order in orders)
                {
                    if (order.ID == id)
                    {
                        orders.Remove(order);
                        orders.Add(newOrder);
                        break;
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        //按每个订单总金额排序
        public void sortByPrice()
        {
            orders.Sort((p1, p2) => p1.Sum.CompareTo(p2.Sum));
        }
        //按订单ID进行查询
        public Order queryByOrderID(int id)
        {
            var result = orders.Where(obj => obj.ID == id).FirstOrDefault();
            return result;
        }
        //按客户姓名查询
        public List<Order> sortByCustomerName(string name)
        {
            var result = orders.Where(obj => obj.Customer.Name == name).OrderBy(p => p.Sum);
            if (result != null)
            {
                return result.ToList();
            }
            else
            {
                return new List<Order>();
            }
        }
        //导出为xml文件
        public void Export()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Order>));
            using (FileStream fs = new FileStream("D://orderlist.xml", FileMode.Create))
            {
                xmlSerializer.Serialize(fs, orders);
            }
            Console.WriteLine(File.ReadAllText("D://orderlist.xml"));
        }
        //导入xml文件
        public void Import()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Order>));
            using (FileStream fs = new FileStream("D://orderlist.xml", FileMode.Open))
            {
                orders = (List<Order>)xmlSerializer.Deserialize(fs);
            }
        }
    }
}
