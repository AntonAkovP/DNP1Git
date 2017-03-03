using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace exc11Client
{
    class startC
    {
        static void Main(string[] args)
        {
            TcpClient client = new TcpClient("127.0.0.1", 12345);
            NetworkStream ns = client.GetStream();


            byte[] buff = new byte[256];
            int bread;
            byte[] writeBuff;
            StringBuilder message = new StringBuilder("Cannot read from server");

            while (true)
            {
                if (ns.CanWrite)
                {
                    writeBuff = Encoding.ASCII.GetBytes(Console.ReadLine());
                    ns.Write(writeBuff, 0, writeBuff.Length);
                }
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

                    Console.WriteLine(message);
                }
            }
        }
    }
}
