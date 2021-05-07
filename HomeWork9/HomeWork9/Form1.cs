using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace HomeWork9
{
    public partial class Form1 : Form
    {
        Crawler spider = new Crawler();
        public Form1()
        {
            InitializeComponent();
            spider.SpiderStopped += this.SpiderStopped;
            spider.CurPageDownloaded += this.CurPageDownloaded;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            spider.StartURL = this.textBox1.Text;
            this.textBox2.Clear();
            this.textBox2.AppendText("爬虫已启动...." + "\r\n");
            Match match = Regex.Match(spider.StartURL, Crawler.urlParseRegex);
            //未匹配到有效url
            if (match.Length == 0)
            {
                this.textBox2.AppendText("未检测到有效url...." + "\r\n");
                return;
            }
            string host = match.Groups["host"].Value;
            spider.HostFilter = "^" + host + "$";
            spider.PageFilter = ".*(.html|.jsp|.aspx)?$";
            spider.Crawl();
        }
        /**
         * 爬虫停止事件处理
         */
        private void SpiderStopped(Crawler spider)
        {
            this.textBox2.AppendText("爬虫已停止...." + "\r\n");
        }
        /**
         * 当前页下载事件处理
         */
        private void CurPageDownloaded(Crawler spider, string url, string info)
        {
            string pageInfo = $"[网页={url}, 爬取状态={info}]";
            this.textBox2.AppendText(pageInfo + "\r\n");
        }
    }
}
