using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OrderControl;
namespace WinForms
{
    public partial class FormSecond : Form
    {
        private OrderService orderService;
        private List<OrderDetails> orderDetailsList;
        private Order oldOrder;
        private int id;
      //当前页数
        private int count;
        public FormSecond(OrderService orderService)
        {
            InitializeComponent();
            this.count = 0;
            this.orderService = orderService;
            this.id = -1;
            orderDetailsList = new List<OrderDetails>();
        }
        public FormSecond(OrderService orderService, int id)
        {
            InitializeComponent();
            this.count = 0;
            this.orderService = orderService;
            this.id = id;
            this.oldOrder = orderService.querybyOrderId(id);
            this.textBox1.Text = oldOrder.Buyer.Name;
            this.textBox2.Text = oldOrder.OrderDetails[0].Goods.Name;
            this.textBox3.Text = oldOrder.OrderDetails[0].Goods.Price.ToString();
            this.textBox4.Text = oldOrder.OrderDetails[0].GoodsNum.ToString();
            orderDetailsList = new List<OrderDetails>();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (!this.textBox1.Text.Equals("") && !this.textBox2.Text.Equals("") && !this.textBox3.Text.Equals("") && !this.textBox4.Text.Equals(""))
            {
                if (this.button1.Text.Equals("添加"))
                {
                    Customer customer = new Customer(this.textBox1.Text);
                    Goods good = new Goods(this.textBox2.Text, Double.Parse(this.textBox3.Text));
                    OrderDetails orderDetails = new OrderDetails(good, Int32.Parse(this.textBox4.Text));
                    orderDetailsList.Add(orderDetails);
                    OrderDetails[] orderDetailsArray = orderDetailsList.ToArray();
                    Order order = new Order(getRandomId(), customer, orderDetailsArray);
                    orderService.add(order);
                    ((FormMain)this.Owner).orderBindingSource.ResetBindings(false);
                    this.Close();
                }
                else if (this.button1.Text.Equals("修改"))
                {
                    Customer customer = new Customer(this.textBox1.Text);
                    Goods good = new Goods(this.textBox2.Text, Double.Parse(this.textBox3.Text));
                    OrderDetails orderDetails = new OrderDetails(good, Int32.Parse(this.textBox4.Text));
                    orderDetailsList.Add(orderDetails);
                    OrderDetails[] orderDetailsArray = orderDetailsList.ToArray();
                    Order order = new Order(getRandomId(), buyer, orderDetailsArray);
                    orderService.update(id, order);
                    ((FormMain)this.Owner).orderBindingSource.ResetBindings(false);
                    this.Close();
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (!this.textBox1.Text.Equals("") && !this.textBox2.Text.Equals("") && !this.textBox3.Text.Equals("") && !this.textBox4.Text.Equals(""))
            {
                if (this.button2.Text.Equals("继续添加"))
                {
                    Customer customer = new Customer(this.textBox1.Text);
                    Goods good = new Goods(this.textBox2.Text, Double.Parse(this.textBox3.Text));
                    OrderDetails orderDetails = new OrderDetails(good, Int32.Parse(this.textBox4.Text));
                    orderDetailsList.Add(orderDetails);
                    count++;
                    this.textBox2.Text = "";
                    this.textBox3.Text = "";
                    this.textBox4.Text = "";
                }
                else if (this.button2.Text.Equals("继续修改"))
                {
                    if (++count < oldOrder.OrderDetails.Length)
                    {
                        Customer customer = new Customer(this.textBox1.Text);
                        Goods good = new Goods(this.textBox2.Text, Double.Parse(this.textBox3.Text));
                        OrderDetails orderDetails = new OrderDetails(good, Int32.Parse(this.textBox4.Text));
                        orderDetailsList.Add(orderDetails);
                        this.textBox2.Text = oldOrder.OrderDetails[count].Goods.Name;
                        this.textBox3.Text = oldOrder.OrderDetails[count].Goods.Price.ToString();
                        this.textBox4.Text = oldOrder.OrderDetails[count].GoodsNum.ToString();
                    }
                    else
                    {
                        this.textBox2.Text = oldOrder.OrderDetails[oldOrder.OrderDetails.Length - 1].Goods.Name;
                        this.textBox3.Text = oldOrder.OrderDetails[oldOrder.OrderDetails.Length - 1].Goods.Price.ToString();
                        this.textBox4.Text = oldOrder.OrderDetails[oldOrder.OrderDetails.Length - 1].GoodsNum.ToString();
                    }
                }
            }
        }
    }
}
