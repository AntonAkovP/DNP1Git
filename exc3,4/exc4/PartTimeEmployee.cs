using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNP1E4
{
    class PartTimeEmployee : Employee
    {
        private double hourlyWage;
        private int hoursPerMonth;

        public PartTimeEmployee(double hW, int hpm)
        {
            hourlyWage = hW;
            hoursPerMonth = hpm;
        }
        public override double getMonthSalary()
        {
            return hoursPerMonth * hourlyWage;
        }
    }
}
