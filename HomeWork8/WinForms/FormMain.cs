using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OrderControl;
namespace WinForms
{
    public partial class FormMain : Form
    {
        public OrderService orderService;
        public string CustomerName { get; set; }
        private float x;//定义当前窗体的宽度
        private float y;//定义当前窗体的高度
        public FormMain()
        {
            InitializeComponent();
            x = this.Width;
            y = this.Height;
            setTag(this);
            orderService = new OrderService();
        }
       
        //添加
        private void button1_Click(object sender, EventArgs e)
        {
            new Form2(orderService).ShowDialog(this);
        }
        //删除
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int orderID = (int)dataGridView1.CurrentRow.Cells[0].Value;
                orderService.remove(orderID);
                orderBindingSource.ResetBindings(false);
            }
        }
        //导出
        private void button3_Click(object sender, EventArgs e)
        {
            orderService.Export();
        }
        //导入
        private void button4_Click(object sender, EventArgs e)
        {
            orderService.Import();
            this.orderBindingSource.DataSource = orderService.Orders;
        }
        //查询
        private void button5_Click(object sender, EventArgs e)
        {
            if (CustomerName == null || CustomerName.Equals(""))
            {
                this.orderBindingSource.DataSource = orderService.Orders;
                return;
            }
            this.orderBindingSource.DataSource = orderService.querybyBuyerName(CustomerName);
        }
       //修改
        private void button6_Click(object sen, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int orderID = (int)dataGridView1.CurrentRow.Cells[0].Value;
                new Form2(orderService, orderID).ShowDialog(this);
            }
        }
        private void setTag(Control cons)
        {
            foreach (Control con in cons.Controls)
            {
                con.Tag = con.Width + ";" + con.Height + ";" + con.Left + ";" + con.Top + ";" + con.Font.Size;
                if (con.Controls.Count > 0)
                {
                    setTag(con);
                }
            }
        }
        private void setControls(float newx, float newy, Control cons)
        {
            //遍历窗体中的控件，重新设置控件的值
            foreach (Control con in cons.Controls)
            {
                //获取控件的Tag属性值，并分割后存储字符串数组
                if (con.Tag != null)
                {
                    string[] mytag = con.Tag.ToString().Split(new char[] { ';' });
                    //根据窗体缩放的比例确定控件的值
                    con.Width = Convert.ToInt32(System.Convert.ToSingle(mytag[0]) * newx);//宽度
                    con.Height = Convert.ToInt32(System.Convert.ToSingle(mytag[1]) * newy);//高度
                    con.Left = Convert.ToInt32(System.Convert.ToSingle(mytag[2]) * newx);//左边距
                    con.Top = Convert.ToInt32(System.Convert.ToSingle(mytag[3]) * newy);//顶边距
                    Single currentSize = System.Convert.ToSingle(mytag[4]) * newy;//字体大小
                    con.Font = new Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit);
                    if (con.Controls.Count > 0)
                    {
                        setControls(newx, newy, con);
                    }
                }
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            float newx = (this.Width) / x;
            float newy = (this.Height) / y;
            setControls(newx, newy, this);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.textBox1.DataBindings.Add("Text", this, "BuyerName");
            this.orderBindingSource.DataSource = orderService.Orders;
        }

        /**
         *显示订单明细内容 
         */
        private void changeData()
        {
            if (this.dataGridView1.CurrentRow != null)
            {
                Order order = orderService.querybyOrderId((int)this.dataGridView1.CurrentRow.Cells[0].Value);
                this.orderDetailsBindingSource.DataSource = order.OrderDetails;
            }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.changeData();
        }
        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            this.changeData();
        }
    }
}
