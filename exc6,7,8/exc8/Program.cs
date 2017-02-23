using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exc8
{
    class Program
    {
        delegate void GenAction<T>(T a);

        static void printGen<T>(T p)
        {
            Console.WriteLine(p);
        }

        static void Perform<T>(GenAction<T> act, T[] arr)
        {
            foreach (T i in arr)
                act(i);
        }

        static void Main(string[] args)
        {
            GenAction<int> del1;
            del1 = printGen<int>;

            del1(42);

            int[] temparr = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };

            Perform(del1, temparr);
            Console.ReadKey();

            Console.WriteLine('\n');
            GenAction<String> del2;
            del2 = printGen<String>;

            del2("Forty two");

            String[] temparr2 = { "Words", "used", "for", "Testing", "."};

            Perform(del2, temparr2);
            Console.ReadKey();


        }
    }
}
