using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNP1Exc6
{
    class Car : IComparable
    {
        protected int engineSize;
        protected double weight;
        protected int topSpeed;

        public Car(int engSize, double weight, int topS)
        {
            if (engSize < 1600 || engSize > 5000) throw new ArgumentOutOfRangeException("Engine size must be between 1600 and 5000.");
            if (topS < 160 || topS >250) throw new ArgumentOutOfRangeException("Top speed must be between 160 and 250");
            engineSize = engSize;
            this.weight = weight;
            topSpeed = topS;
        }

        public override string ToString()
        {
            return "Engine size: " + engineSize + "\tWeight: " + weight + "\tTop speed: " + topSpeed;
        }

        public int CompareTo(Object obj)
        {
            if (obj == null) return 1;
            Car otherCar = obj as Car;

            if (otherCar != null)
                return engineSize.CompareTo(otherCar.engineSize);
            else
                throw new ArgumentException("Object is not a Car");
        }
    }
}
