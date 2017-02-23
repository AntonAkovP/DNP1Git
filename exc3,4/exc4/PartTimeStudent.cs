using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNP1E4
{
    class PartTimeStudent : PartTimeEmployee, IStudent
    {
        private int year;

        public PartTimeStudent(double hW, int hpm) : base(hW, hpm)
        {
            year = 0;
        }

        public void Register(int y)
        {
            year = y;
        }

        public int getStartedEducation()
        {
            return year;
        }
    }
}
