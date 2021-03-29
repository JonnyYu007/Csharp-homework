using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlarmClock
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            timer1.Start();
            MyInitial();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString();
            label2.Text = "实时时间是：";
        }
        private void MyInitial()
        {
            //1秒定时
            this.timer2.Interval = 1000;
            this.timer2.Enabled = true;
            //界面初始化
            this.nowTime.Text = DateTime.Now.ToString();
            this.selectHours.SelectedIndex = 0;
            this.selectMiniuts.SelectedIndex = 0;
        }

        bool isSatrt = false; //闹钟是否开始
        DateTime dt; //闹钟时间
        private void TimeStart_Click(object sender, EventArgs e)
        {
            if (!isSatrt)
            {//点开始
                this.TimeStart.Text = "停止结束";
                int hours = Convert.ToInt32(this.selectHours.SelectedItem.ToString());
                int miniuts = Convert.ToInt32(this.selectMiniuts.SelectedItem.ToString());
                dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hours, miniuts, 0);
                isSatrt = true;
            }
            else
            {//点结束
                this.haveTime.Text = "------";
                this.TimeStart.Text = "闹钟开始";
                isSatrt = false;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            DateTime nt = DateTime.Now;
            this.nowTime.Text = nt.ToString();
            if (isSatrt)
            {//做判断，闹钟响
                TimeSpan ts = dt - nt; //相差的时间
                if (ts.TotalSeconds < 0)
                {
                    this.TimeStart.PerformClick();
                    MessageBox.Show("闹钟响");//ToDo
                    //添加引用-COM-MediaPlayer
                    MediaPlayer.MediaPlayer mp = new MediaPlayer.MediaPlayer();
                    mp.FileName = @"C:\Users\Administrator\Music\Go.mp3";
                    mp.Play();
                }
                else
                {
                    int ch = ts.Hours;
                    int cm = ts.Minutes;
                    int cs = ts.Seconds;
                    //剩余时间
                    this.haveTime.Text = ch + " 小时 " + cm + " 分钟 " + cs + " 秒 ";
                }
            }
        }
       
    }
}
