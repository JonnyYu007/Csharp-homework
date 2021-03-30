using System;
using System.Threading;
namespace ConsoleApp1
{
    public delegate void ClockHandler(string t);
    public class Time
    {
        private int hour;
        private int minute;
        private int second;
        public Time(int hour,int minute,int second)
        {
            this.hour = hour;
            this.minute = minute;
            this.second = second;
           
        }
    }
    
    class AlarmClock
    {
       
        public event ClockHandler Alarm;
        public AlarmClock(string clock_time)
        {
            Alarm+=Func1;
        }
        public static void Func1(string clock_time)
        {
            string nowtime = DateTime.Now.ToString("hh:mm:ss");
            if (clock_time == nowtime)
            {
                Console.WriteLine("闹钟时间到！");
            }
        }
    }
    public class Tick
    {
        public event ClockHandler Tick1;
        public Tick(string nowtime)
        {

            Tick1 += Func2;
            
        }
        public static void Func2(string nowtime)
        {
            Console.WriteLine(DateTime.Now.ToString("hh:mm:ss"));
            Thread.Sleep(1000);
            Console.Clear();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string s = " ";
            Tick t = new Tick(s);
            Console.WriteLine("请输入闹钟时间:(以hh:mm:ss形式输入)");
            string alarm_time = Console.ReadLine();
            AlarmClock alarmClock = new AlarmClock(alarm_time);
            
        }
    }
}
