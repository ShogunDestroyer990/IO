using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication19
{



    public class TaskInfo
    {
        
       
        public int Value;
        public AutoResetEvent arvent;

        public TaskInfo( int Value, AutoResetEvent arvent)
        {
           
           this.Value = Value;
            this.arvent = arvent;
        }
    }





    class Program
    {

       public static int N = 10;

        public static int suma;

        static int[] tab = new int[] { 3, 4, 5, 6, 1, 4, 9, 12, 1, 7 };






        static void Main(string[] args)
        {
            Object thisLock = new Object();


            AutoResetEvent[] arvents = new AutoResetEvent[N];

            //  Thread[] Tablica_wątków = new Thread[N];
            for (int i = 0; i < N; i++)
            {
               
                Random rnd = new Random();

                int chosen = rnd.Next(0, N);


                arvents[i] = new AutoResetEvent(false);

                TaskInfo ti = new TaskInfo(tab[chosen], arvents[i]);


               

                ThreadPool.QueueUserWorkItem(new WaitCallback(wybierz_losowyelemnt),ti);
                //  Tablica_wątków[i] = new Thread(wybierz_losowyelemnt);
                //Tablica_wątków[i].Start();
                Thread.Sleep(100);
            }

            WaitHandle.WaitAll(arvents);


            lock (thisLock)
            {
                
                System.Console.WriteLine(" wynik to " + suma);
            }
         


        }



        static void wybierz_losowyelemnt(object info)
        {

            TaskInfo ti = (TaskInfo)info;

            System.Console.WriteLine(ti.Value);

            suma += ti.Value;


            ti.arvent.Set();


        }
    }
}


/*
 
   bool notchosen = true;
            int[] itwas=new int [N];
            int size = itwas.Length;


            while (notchosen)
            {
                

                for (int i = 0; i < size; i++)
                {

                    if (itwas[i] == chosen)
                    {
                        notchosen = false;
                    }
                    
                }

                if (notchosen == false)
                {
                    notchosen = true;
                }
                else
                {
                    notchosen = false;
                    itwas[size - 1] = chosen;
                    System.Console.WriteLine(chosen);
                }

                 
            }
 

    */