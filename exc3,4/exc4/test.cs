using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNP1E4
{
    class test
    {
        public static void Main(String[] args)
        {
            Company comp = new Company();
            comp.employNewEmployee(new PartTimeEmployee(5.5, 20));
            comp.employNewEmployee(new FullTimeEmployee(30000));
            PartTimeStudent john = new PartTimeStudent(10, 20);
            john.Register(2018);
            comp.employNewEmployee(john);
            Console.WriteLine("Expected: 30000+ 5.5*20 = 30110 Result: "+comp.getMonthlySalaryTotal());
            Console.WriteLine("John's year: " + john.getStartedEducation());
            Console.ReadKey();

        }
    }
}
