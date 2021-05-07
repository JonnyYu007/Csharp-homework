using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace HomeWork9
{
    public class Crawler
    {
        /**
       * 解析网站的内容
       */
        public static readonly string urlParseRegex = @"^(?<url>https?://(?<host>(www.)?[-a-zA-Z0-9@:%._+~#=]{1,256}.[a-zA-Z0-9()]{1,6})/?)(.*/)*(?<page>(.*(.html|.jsp|.aspx))?)$";
        /**
         * 检测href中的url的正则表达式
         */
        public static readonly string urlDetectRegex = @"<a.+?(href|HREF)=[""'](?<url>[^""'#>]+)[""'].*>";
        /**
         * 两个爬虫事件
         */
        public event Action<Crawler> SpiderStopped;
        public event Action<Crawler, string, string> CurPageDownloaded;

        /**
         * url下载等待队列
         */
        private Queue<string> waiting = new Queue<string>();
        public string PageFilter { get; set; }
        public string HostFilter { get; set; }
        /**
         * 最大爬取页数
         */
        public int MaxPage { get; set; }
        /**
         * 开始网页
         */
        public string StartURL { get; set; }
        /**
         * 网页编码
         */
        public Encoding Encoding { get; set; }
        public Dictionary<string, bool> Urls { get; set; }
        public Crawler()
        {
            MaxPage = 50;
            Encoding = Encoding.UTF8;
        }
        public void Crawl()
        {
            Urls = new Dictionary<string, bool>();
            waiting.Clear();
            waiting.Enqueue(StartURL);
            while (Urls.Count < MaxPage && waiting.Count > 0)
            {
                string current = waiting.Dequeue();
                try
                {
                    string html = DownLoad(current);
                    Urls[current] = true;
                    //解析
                    Parse(html, current);
                    //触发当前页下载成功事件
                    CurPageDownloaded(this, current, "SUCCESS：爬取成功");
                }
                catch (Exception e)
                {
                    //触发当前页下载失败事件
                    CurPageDownloaded(this, current, "ERROR：" + e.Message);
                }
            }
            //触发爬虫完成事件
            SpiderStopped(this);
        }
        /**
         * 下载html
         */
        public string DownLoad(string url)
        {
            try
            {
                WebClient webClient = new WebClient();
                webClient.Encoding = Encoding.UTF8;
                string html = webClient.DownloadString(url);
                TimeSpan ts = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0);
                Random rd = new Random();
                string fileName = "";
                if (Regex.IsMatch(url, @".*.html?$"))
                {
                    fileName = Convert.ToInt64(ts.TotalSeconds).ToString() + rd.Next(10, 99).ToString() + ".html";
                }
                else if (Regex.IsMatch(url, @".*.jsp?$"))
                {
                    fileName = Convert.ToInt64(ts.TotalSeconds).ToString() + rd.Next(10, 99).ToString() + ".jsp";
                }
                else if (Regex.IsMatch(url, @".*.aspx?$"))
                {
                    fileName = Convert.ToInt64(ts.TotalSeconds).ToString() + rd.Next(10, 99).ToString() + ".aspx";
                }
                else
                {
                    fileName = Convert.ToInt64(ts.TotalSeconds).ToString() + rd.Next(10, 99).ToString();
                }
                if (!Directory.Exists("D://spiderData"))
                {
                    Directory.CreateDirectory("D://spiderData");
                }
                File.WriteAllText("D://spiderData//" + fileName, html, Encoding.UTF8);
                return html;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /**
         * 解析html
         */
        private void Parse(string html, string pageUrl)
        {
            //获取html中的url集合
            MatchCollection matchUrls = new Regex(urlDetectRegex).Matches(html);
            foreach (Match matchUrl in matchUrls)
            {
                string linkUrl = matchUrl.Groups["url"].Value;
                if (linkUrl == null || linkUrl.Equals(""))
                {
                    continue;
                }
                //转换为绝对路径   pageUrl当前页url
                linkUrl = ConvertUrl(linkUrl, pageUrl);
                //解析出host和file两个部分
                Match linkUrlMatch = Regex.Match(linkUrl, urlParseRegex);
                string host = linkUrlMatch.Groups["host"].Value;
                string page = linkUrlMatch.Groups["page"].Value;
                if (page.Equals(""))
                {
                    page = "index.html";
                }
                //过滤重复页面 过滤非html/aspx/jsp网页
                if (Regex.IsMatch(host, HostFilter) && Regex.IsMatch(page, PageFilter) && !Urls.ContainsKey(linkUrl))
                {
                    //将转换后的url加入等待队列
                    waiting.Enqueue(linkUrl);
                    //将转换后的url加入字典   并设为false未爬取
                    Urls.Add(linkUrl, false);
                }
            }
        }
        /**
         * 将相对路径转为绝对路径
         */
        static private string ConvertUrl(string url, string baseUrl)
        {
            //绝对路径
            if (url.Contains("://"))
            {
                return url;
            }
            if (url.StartsWith("//"))
            {
                return "http:" + url;
            }
            if (url.StartsWith("/"))
            {
                String temp = Regex.Match(baseUrl, urlParseRegex).Groups["url"].Value;
                return temp.EndsWith("/") ? temp + url.Substring(1) : temp + url;
            }
            if (url.StartsWith("./"))
            {
                return ConvertUrl(url.Substring(2), baseUrl);
            }
            if (url.StartsWith("../"))
            {
                int idx = baseUrl.LastIndexOf('/');
                return ConvertUrl(url.Substring(3), baseUrl.Substring(0, idx));
            }
            int end = baseUrl.LastIndexOf("/");
            return baseUrl.Substring(0, end) + "/" + url;
        }
    }
}
