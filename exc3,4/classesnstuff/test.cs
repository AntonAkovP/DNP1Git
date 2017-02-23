using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNP1E3
{
    class test
    {
        public static void Main(String[] args)
        {
            Company comp = new Company();
            comp.employNewEmployee(new PartTimeEmployee(5.5, 20));
            comp.employNewEmployee(new FullTimeEmployee(30000));

            Console.WriteLine("Expected: 30000+ 5.5*20 = 30110 Result: "+comp.getMonthlySalaryTotal());
            Console.ReadKey();

        }
    }
}
