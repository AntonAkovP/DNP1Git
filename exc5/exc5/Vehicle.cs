using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exc5
{
    class Vehicle
    {
        protected int registrationNumber;
        protected double maxVelocity;
        protected decimal value;

        public Vehicle(int rN, double mV, decimal v)
        {
            registrationNumber = rN;
            maxVelocity = mV;
            value = v;

        }
    }
}
