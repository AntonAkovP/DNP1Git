using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace excercises1._3
{
    class Program
    {
        static void Main(string[] args)
        {
            int i = 50;
            int temp;
            while(i<=101)
            {
                temp = i * 2;
                switch(temp)
                {
                    case 110 : Console.WriteLine(temp + " this is a funny number"); break;
                    case 130 : Console.WriteLine(temp + " this is a funny number"); break;
                    case 140 : Console.WriteLine(temp + " this is a funny number"); break;

                    case 122 : Console.WriteLine(temp + " this is my lucky number"); break;

                    case 198 : break;

                    default : Console.WriteLine(temp); break;
                }
                i++;
            }
            Console.ReadKey();
        }
    }
}
