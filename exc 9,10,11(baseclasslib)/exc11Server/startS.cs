using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace exc11Server
{
    class startS
    {


        private static void ListenerM()
        {
            byte[] adr = { 127, 0, 0, 1 };
            IPAddress ipAdr = new IPAddress(adr);
            TcpListener newsock = new TcpListener(ipAdr, 12345);
            newsock.Start();
            while(true)
            {
                Console.WriteLine("Waiting for a client...");
                TcpClient client = newsock.AcceptTcpClient();
                Thread handler = new Thread(handlerM);
                handler.Start(client);

                Console.WriteLine(client.ToString() + " has connected.");
            }
            
        }

        private static void handlerM(object clientO)
        {
            TcpClient client = clientO as TcpClient;
            if (client==null) return;


            NetworkStream ns = client.GetStream();

            byte[] buff;
            byte[] writeBuff;
            StringBuilder message = new StringBuilder("Message not read");
            int bread;

            while (true)
            {
                try
                {


                    if (ns.CanRead)
                    {
                        buff = new byte[128];
                        message = new StringBuilder();

                        do
                        {
                            bread = ns.Read(buff, 0, buff.Length);

                            message.AppendFormat("{0}", Encoding.ASCII.GetString(buff, 0, bread));

                        }
                        while (ns.DataAvailable);
                    }
                    if (ns.CanWrite)
                    {
                        writeBuff = Encoding.ASCII.GetBytes("ECHO: " + message);
                        ns.Write(writeBuff, 0, writeBuff.Length);
                    }
                }
                catch (System.IO.IOException e) { Console.WriteLine(client.ToString() + " disconnected."); break; }
            }
        }
        static void Main(string[] args)
        {
            Thread listener = new Thread(ListenerM);
            listener.IsBackground = true;
            listener.Start();

            Console.ReadKey();

        }
    }
}
