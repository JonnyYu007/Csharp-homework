using System;

namespace homework4
{
    public class Node<T>
    {
        public Node<T> Next;
        public T Data;
        public Node(T t)
        {
            Next = null;
            Data = t;
        }
    }
    public class Genericlist<T>
    {
        private Node<T> head;
        private Node<T> tail;
        public Genericlist()
        {
            head = tail = null;
        }
        public Node<T> Head//属性
        {
            get => head;
        }
        public void Add(T t)
        {
            Node <T>n = new Node<T>(t);
            if (tail == null)
            {
                head = tail = n;
            }
            else
            {
                tail.Next = n;
                tail = n;
            }
        }
        public void ForEach(Action<T> action)
        {
            for (Node<T> n = head; n != null; n = n.Next)
            {
                action(n.Data);
            }
        }
    }
    class Program
    {
        
        static void Main(string[] args)
        {
            Genericlist<int> list = new Genericlist<int>();
            Random r = new Random();
            for(int i = 0; i < 20; i++)
            {
                list.Add(r.Next(100));
            }
            Console.Write("遍历泛型链表的结果是:");
            list.ForEach(n => Console.Write(n+" "));
            double min = 100;
            double max = 0;
            double sum = 0;
            list.ForEach(n => min = min > n ? n : min);
            list.ForEach(n => max = max > n ? max : n);
            list.ForEach(n => sum += n);
            
            Console.WriteLine("\n"+$"min={min},max={max},sum={sum}");
        }
    }
}

