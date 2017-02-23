using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exc5
{
    class FixedProperty
    {
        protected string location;
        protected bool inCity;
        protected decimal estimatedValue;

        public FixedProperty(string loc, bool iC, decimal eV)
        {
            location = loc;
            inCity = iC;
            estimatedValue = eV;
        }
    }
}
