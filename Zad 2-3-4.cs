using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;
using System.Net;

namespace ConsoleApp1
{
    class Program
    {
        

        static void Main(string[] args)
        {

        

            ThreadPool.QueueUserWorkItem(Server);

            Thread.Sleep(1000);

            ThreadPool.QueueUserWorkItem(Client);

            Thread.Sleep(500);

            ThreadPool.QueueUserWorkItem(Client);

            Thread.Sleep(500);



        }

        static void WC(byte[] message, ConsoleColor color)
        {
            for (int i = 0; i < message.Length; i++)
            {
                Console.ForegroundColor = color;
                Console.Write(Convert.ToChar(message[i]));
                Console.ResetColor();

            }
            Console.WriteLine();
        }

        static void Client(Object info)
        {
            Object thisLock = new Object();
            Thread.Sleep(100);

            TcpClient client = new TcpClient();
            client.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 2048));
            var message = new ASCIIEncoding().GetBytes("text");
            
                client.GetStream().Write(message, 0, message.Length);
            // Thread.Sleep(300);
            lock (thisLock)
            {
                WC(message, ConsoleColor.Green);
            }

            


        }

        static void Server(Object info)
        {
            Thread.Sleep(100);
            TcpListener server = new TcpListener(IPAddress.Any, 2048);
            ThreadPool.QueueUserWorkItem(Client);
            server.Start();

            while (true)
            {
                TcpClient client = server.AcceptTcpClient();

               // ThreadPool.QueueUserWorkItem(Client);


                var buffer = new byte[1024];
                client.GetStream().Read(buffer, 0, 1024);
                client.GetStream().Write(buffer, 0, buffer.Length);
                Thread.Sleep(100);
                WC(buffer, ConsoleColor.Red);
                client.Close();

            }
        }



    }


}