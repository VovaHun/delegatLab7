using System;
using System.Collections.Generic;
using System.Threading;

namespace Lab7
{

    public class MassItem
    {
        public int Data;
        public long Fact;
    }

    public class MassData
    {
        private List<MassItem> massive;

        public MassData()
        {
            massive = new List<MassItem>(1000);
        }

        public void Add(MassItem newElement)
        {
            if (newElement.Data < 1) newElement.Data = 1;
            massive.Add(newElement);

            ThreadPool.QueueUserWorkItem(MethodForThread, newElement);
        }

        protected static void MethodForThread(object ob)
        {
            MassItem item = (MassItem)ob;
            int F = item.Data;
            Console.WriteLine("Поток {0}.Получен новый элемент {1}",
                Thread.CurrentThread.ManagedThreadId,
                item.Data);
            long Facrotial = 1;

            while (F > 1)
            {
                Facrotial *= F;
                Thread.Sleep(100);

              Console.WriteLine("Поток {0}.Вычесление! факториал от числа {1}  равен  {2}",
                Thread.CurrentThread.ManagedThreadId,
                item.Data,
                Facrotial);
                F--;
            }

            item.Fact = Facrotial;
            Console.WriteLine("Поток {0}.Вычесления завершины! Факториал от числа {1}  равен  {2}",
               Thread.CurrentThread.ManagedThreadId,
               item.Data,
               item.Fact);

        }
    }

    class Program
    {


        static void Main(string[] args)
        {
            int a;
            int b;
            ThreadPool.GetMaxThreads(out a, out b);
            Console.WriteLine("Максимальное кол-во раб. потоков: {0}" +
                " Максимальное кол-во потоков ввода-вывода {1}",
                a, b);
            MassData myData = new MassData();
            Random rand = new Random();
            for (int i =0; i<1000; i++)
            {
                myData.Add(new MassItem() { Data = rand.Next(10) });
            }
        }
    }
}
