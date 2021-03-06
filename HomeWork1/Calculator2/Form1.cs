using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Text = "Calculator2";
            textBox1.Text = "操作数1";
            textBox2.Text = "操作数2";
            comboBox1.Text = "选择运算符";
            textBox3.Text = "运算结果";
            label1.Text = "   =";
            button1.Text = "运算按钮";
            comboBox1.Items.Add("+");
            comboBox1.Items.Add("-");
            comboBox1.Items.Add("*");
            comboBox1.Items.Add("/");
            comboBox1.Items.Add("%");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            double op1, op2;
            double s = 0;
            op1 = Convert.ToSingle(textBox1.Text);
            op2 = Convert.ToSingle(textBox2.Text);
            if(comboBox1.Text=="+")
            {
                s = op1 + op2;
                textBox3.Text = s.ToString();
            }
            else if(comboBox1.Text=="-")
            {
                s = op1 - op2;
                textBox3.Text = s.ToString();
            }
            else if(comboBox1.Text=="*")
            {
                s = op1 * op2;
                textBox3.Text = s.ToString();
            }
            else if(comboBox1.Text=="/")
            {
                s = op1 / op2;
                textBox3.Text = s.ToString();
            }
            else
            {
                s = op1 % op2;
                textBox3.Text = s.ToString();
            }

        }
    }
}
