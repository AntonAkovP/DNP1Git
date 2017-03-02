using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace exc11Server
{
    class startS
    {
        static void Main(string[] args)
        {
            byte[] data;
            byte[] adr = { 127, 0, 0, 1 };
            IPAddress ipAdr = new IPAddress(adr);
            TcpListener newsock = new TcpListener(ipAdr, 12345);
            newsock.Start();
            Console.WriteLine("Waiting for a client...");
            TcpClient client = newsock.AcceptTcpClient();
            NetworkStream ns = client.GetStream();
            string welcome = "Welcome to the DNPI1 test server";
            data = Encoding.ASCII.GetBytes(welcome);
            ns.Write(data, 0, data.Length);
        }
    }
}
