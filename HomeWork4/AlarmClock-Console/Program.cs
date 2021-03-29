using System;

namespace Alarm
{
    class Clock
    {
        private int hour;
        private int minute;
        private int second;
        public Clock(int hour = 0, int minute = 0, int second = 0)
        {
            this.hour = hour;
            this.minute = minute;
            this.second = second;
        }
        public override bool Equals(object obj)
        {
            var time = obj as Clock;
            return time != null;
        }
    }
    class AlarmClock
    {
        public Clock CurrentTime { get; set; }
        public Clock AlarmTime { get; set; }
        public event Action<AlarmClock> AlarmEvent;
        public AlarmClock{
            CurrentTime=new Clock();
            }
        void run(int a,int b,int c)
    {
        AlarmClock.hour = CurrentTime.hour + a;
        AlarmClock.minute = CurrentTime.minute + b;
        AlarmClock.second = CurrentTime.second + c;
        if (AlarmTime.Equals(CurrentTime))
            AlarmEvent(this);
        Thread.Sleep(1000);
    }
    }
    class Program
    {
        static void Main(string[] args)
        {
           
        Console.WriteLine("实时时间："+DateTime.Now.ToString());
        private static void ShowTime(AlarmClock sender)
        {
            ClockTime time = sender.CurrentTime;
            Console.WriteLine($"AlarmEvent Event: " +
              $"{time.hour}:{time.minute}:{time.second}");
        }
        
        Console.WriteLine("请输入闹钟的时间:");
        Console.Write("时:");
        int a = Console.Read();
        Console.Write("分:");
        int b= Console.Read();
        Console.Write("秒:");
        int c = Console.Read();

        AlarmClock alarm = new AlarmClock();
        alarm.AlarmTime = new Clock();
        clock.AlarmEvent += ShowTime;
        alarm.run(a, b, c);
    }
    }
}
