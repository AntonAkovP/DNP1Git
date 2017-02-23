using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exc5
{
    class test
    {
        static void Main(string[] args)
        {
            ITaxable bus1 = new Bus(13, 101010, 120, 50000);
            ITaxable bus2 = new Bus(15, 101011, 160, 100000);

            ITaxable house1 = new House(110, "horsens", true, 4000000);
            ITaxable house2 = new House(300, "horsens", false, 10000000);

            Console.WriteLine("Bus1 tax:" + bus1.taxValue());
            Console.WriteLine("Bus2 tax:" + bus2.taxValue());
            Console.WriteLine("House1 tax:" + house1.taxValue());
            Console.WriteLine("House2 tax:" + house2.taxValue());

            Console.ReadKey();
        }
    }
}
