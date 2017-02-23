using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace excercises1._4
{
    class Program
    {
        static void Main(string[] args)
        {
            int i = 1;
            while(i<256)
            {
                Console.WriteLine(i.ToString("X"));
                i++;
            }
            Console.ReadKey();
        }
    }
}
