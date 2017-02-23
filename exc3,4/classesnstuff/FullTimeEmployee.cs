using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNP1E3
{
    class FullTimeEmployee : Employee
    {
        private double monthlySalary;

        public FullTimeEmployee(double mS)
        {
            monthlySalary = mS;
        }

        public override double getMonthSalary()
        {
            return monthlySalary;
        }
    }
}
