using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace HomeWork10
{
    public class Crawler
    {
        public Crawler()
        {
            MaxPage = 50;
            Encoding = Encoding.UTF8;
        }

        //两个爬虫事件
        public event Action<Crawler> SpiderStopped;
        public event Action<Crawler, string, string> CurPageDownloaded;

        //url等待下载队列
        private ConcurrentQueue<string> waiting;
        public string FileFilter { get; set; }
        public string HostFilter { get; set; }

        //最大爬取页数
        public int MaxPage { get; set; }

        //开始网页
        public string StartURL { get; set; }

        //编码
        public Encoding Encoding { get; set; }
        public Dictionary<string, bool> Urls { get; set; }

        //检测href中的正则表达式
        public static readonly string urlDetectRegex = @"<a.+?(href|HREF)=[""'](?<url>[^""'#>]+)[""'].*>";

        //解析网站的内容
        public static readonly string urlParseRegex = @"^(?<url>https?://(?<host>(www.)?[-a-zA-Z0-9@:%._+~#=]{1,256}.[a-zA-Z0-9()]{1,6})/?)(.*/)*(?<page>(.*(.html|.jsp|.aspx))?)$";

        //已经爬取的页面
        public int doneCount;

        //并行任务列表
        List<Task> tasks;

        //下载html
        public string DownLoad(string url)
        {
            try
            {
                WebClient webClient = new WebClient
                {
                    Encoding = Encoding.UTF8
                };
            string html = webClient.DownloadString(url);
            //防止出现多任务并行导致的文件IO异常
            lock (this)
            {
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
                if (!Directory.Exists("D://CrawlerData"))
                {
                    Directory.CreateDirectory("D://CrawlerData");
                }
                File.WriteAllText("D://CrawlerData//" + fileName, html, Encoding.UTF8);

            }
            return html;
        }
    
            catch (Exception e)
            {
                throw e;
            }
        }

        //解析
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
                if (Regex.IsMatch(host, HostFilter) && Regex.IsMatch(page, FileFilter) && !Urls.ContainsKey(linkUrl))
                {
                    //将转换后的url加入等待队列
                    waiting.Enqueue(linkUrl);
                    //将转换后的url加入字典
                    Urls.Add(linkUrl, false);
                }
            }
        }
        //相对路径转绝对路径
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

        public void Crawl()
        {
            Urls = new Dictionary<string, bool>();
            tasks = new List<Task>();//并行任务列表
            doneCount = 0;
            waiting = new ConcurrentQueue<string>();
           
            waiting.Enqueue(StartURL);
            //循环爬取
            while (tasks.Count < MaxPage)
            {
                if(waiting.TryDequeue(out string current))
                {
                    //如果有任务未完成，先完成
                    if (doneCount<tasks.Count)
                    {
                        continue;
                    }
                    else break;
                }
                Task task = Task.Run(() =>
                 {
                     try
                     {
                         string html = null;
                        //下载页面
                        html = DownLoad(current);
                         Urls[current] = true;
                        //解析
                        Parse(html, current);
                        //当前页爬取成功事件
                        ++doneCount;
                         CurPageDownloaded(this, current, "爬取成功");
                     }
                     catch (Exception e)
                     {
                        //当前页爬取失败事件
                        ++doneCount;
                         CurPageDownloaded(this, current, "爬取失败" + e.Message);
                     }
                 });
                tasks.Add(task);

            }
            //完成剩余任务
            Task.WaitAll(tasks.ToArray());
            //爬虫完成
            SpiderStopped(this);
        }
    }
}
