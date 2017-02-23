using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exc5
{
    class Bus : Vehicle, ITaxable
    {
        protected int numberOfSeats;

        public Bus(int seats, int rN, double mV, decimal v) : base(rN, mV, v)
        {
            numberOfSeats = seats;
        }

        //20% of value +1 for each seat
        public decimal taxValue()
        {
            return value * (decimal)0.20 + (decimal)numberOfSeats;
        }
    }
}
