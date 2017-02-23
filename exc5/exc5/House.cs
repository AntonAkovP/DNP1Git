using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exc5
{
    class House : FixedProperty, ITaxable
    {
        protected double area;
        public House(double a, string loc, bool iC, decimal eV) : base(loc, iC, eV)
        {
            area = a;
        }
        //20% of value + area
        public decimal taxValue()
        {
            return estimatedValue * new decimal(0.20) + (decimal)area;
        }
    }
}
