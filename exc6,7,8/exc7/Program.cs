using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace exc7
{
    class Program
    {
        static int GreaterCount<T>(List<T> list, T min) where T:IComparable<T>
        {
            int res = 0;

            foreach (T t in list)
                if (t.CompareTo(min)>=0) res++;

            return res;
        }


        static void Main(string[] args)
        {

            List<double> temperatures = new List<double>();
            temperatures.Add(25);
            temperatures.Add(4.4);
            temperatures.Add(55);
            temperatures.Add(13);
            temperatures.Add(4.3);
            temperatures.Add(1);

            int res = 0;
            foreach(double t in temperatures)
                if (t >= 25) res++;


            Console.WriteLine("Alltemps");

            foreach(double t in temperatures)
                Console.WriteLine(t);
            
            Console.WriteLine("temps >= 25: " + res);

            Console.Write("Enter min temp ");
            double mintemp = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("temps >= "+mintemp+": " + GreaterCount(temperatures, mintemp));

            Console.ReadKey();
        }
    }
}
