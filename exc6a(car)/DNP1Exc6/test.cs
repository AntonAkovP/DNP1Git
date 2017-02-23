using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNP1Exc6
{
    class test
    {
        static void Main(string[] args)
        {
            List<Car> car10 = new List<Car>();
            car10.Add(new Car(1600, 1, 160));
            car10.Add(new Car(2000, 2, 180));
            car10.Add(new Car(3000, 3, 160));
            car10.Add(new Car(4000, 4, 168));
            car10.Add(new Car(4535, 5, 169));
            car10.Add(new Car(2324, 6, 163));
            car10.Add(new Car(3467, 7, 164));
            car10.Add(new Car(2341, 8, 165));
            car10.Add(new Car(2356, 9, 176));
            car10.Add(new Car(1893, 10, 187));

            foreach(Car a in car10)
            {
                Console.WriteLine(a);
            }

            Console.WriteLine("\nSorting\n");

            car10.Sort();

            foreach (Car a in car10)
            {
                Console.WriteLine(a);
            }

            Console.ReadKey();
        }
    }
}
