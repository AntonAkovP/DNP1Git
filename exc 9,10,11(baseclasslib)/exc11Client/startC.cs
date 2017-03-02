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
            NetworkStream networkStream = client.GetStream();
            byte[] abyString = Encoding.ASCII.GetBytes("HEJ");
            networkStream.Write(abyString, 0, 3);
            Console.WriteLine(networkStream.Read());
        }
    }
}
