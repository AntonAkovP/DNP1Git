using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNP1E3
{
    class Company
    {
        private List<Employee> employees;

        public Company()
        {
            employees = new List<Employee>();
        }
        public void employNewEmployee(Employee newEmployee)
        {
            employees.Add(newEmployee);
        }
        public double getMonthlySalaryTotal()
        {
            double result=0;
            foreach (Employee temp in employees)
            {
                result = result + temp.getMonthSalary();
            }
            return result;
        }
    }
}
