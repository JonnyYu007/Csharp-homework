using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HomeWork7
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int n;
        int m;
        private Graphics graphics;
        double th1 ;
        double th2 ;
        double per1;
        double per2;
        Color pencolor;
        int penwidth = 2;
        void drwaLine(double x0,double y0,double x1,double y1)
        {
            graphics.DrawLine(new Pen(pencolor,penwidth), (int)x0, (int)y0, (int)x1, (int)y1);
        }
        void drawCayleyTree(int n,double x0,double y0,double length,double th)
        {
            if (n == 0) return;
            double x1 = x0 + length * Math.Cos(th);
            double y1 = y0 + length * Math.Sin(th);
            drwaLine(x0, y0, x1, y1);
            drawCayleyTree(n - 1, x1, y1, per1 * length, th + th1);
            drawCayleyTree(n - 1, x1, y1, per2 * length, th - th2);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (graphics == null) graphics = panel1.CreateGraphics();
            drawCayleyTree(n, m, 310, 100, -Math.PI / 2);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedItem.ToString())
            {
                case "5":n = 5;break;
                case "10":n = 10;break;
                case "20":n = 20;break;
            }
        }

       
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox2.SelectedItem.ToString())
            {
                case "50":m = 50;break;
                case "100":m = 100;break;
                case "200":m = 200;break;
            }
        }
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox3.SelectedItem.ToString())
            {
                case "0.4": per1 = 0.4; break;
                case "0.6": per1= 0.6; break;
                case "0.8": per1= 0.8; break;
            }
        }
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox4.SelectedItem.ToString())
            {
                case "0.5": per2 = 0.5; break;
                case "0.7": per2 = 0.7; break;
                case "0.9": per2 = 0.9; break;
            }
        }
        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox5.SelectedItem.ToString())
            {
                case "10 * Math.PI / 180": th1 = 10 * Math.PI / 180; break;
                case "30 * Math.PI / 180": th1 = 30 * Math.PI / 180; break;
                case "50 * Math.PI / 180": th1 = 50 * Math.PI / 180; break;
            }
        }
        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox6.SelectedItem.ToString())
            {
                case "20 * Math.PI / 180": th2 = 20 * Math.PI / 180; break;
                case "40 * Math.PI / 180": th2 = 40 * Math.PI / 180; break;
                case "60 * Math.PI / 180": th2 = 60 * Math.PI / 180; break;
            }
        }
        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox7.SelectedItem.ToString())
            {
                case "Blue": pencolor=Color.Blue;break;
                case "Red": pencolor = Color.Red; break;
                case "Yellow": pencolor = Color.Yellow; break;
                case "Green": pencolor = Color.Green; break;
                case "Black": pencolor = Color.Black; break;
            }
        }
    }
}
