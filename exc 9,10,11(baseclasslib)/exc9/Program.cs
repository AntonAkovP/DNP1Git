using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace exc9
{
    class Program
    {
        static void Main(string[] args)
        {
            //asdf\n
            Reader txt1Reader = new Reader(@"D:\shoolshit\DNP1Git\exc 9,10,11(baseclasslib)\exc9\txt1.txt");
            //asdf\n
            Reader txt2Reader = new Reader(@"D:\shoolshit\DNP1Git\exc 9,10,11(baseclasslib)\exc9\txt2.txt");

            Thread t1 = new Thread(txt1Reader.Read);
            Thread t2 = new Thread(txt2Reader.Read);
            t1.Start();
            t2.Start();
            t1.Join();
            t2.Join();

            Console.WriteLine(txt1Reader.data.CompareTo(txt2Reader.data));
            Console.ReadKey();

        }
    }
}
