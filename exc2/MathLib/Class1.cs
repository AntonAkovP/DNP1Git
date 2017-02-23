using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNP
{
    public class MathLib
    {
        public static int Sum(int a, int b)
        {
            return a + b;
        }
        public static int Sub(int a, int b)
        {
            return a - b;
        }
        public static int Mul(int a, int b)
        {
            return a * b;
        }
        public static int Div(int a, int b)
        {
            if (b == 0) throw new DivideByZeroException("You cannot devide by zero");
            else return a / b;
        }

        public static double Sum(double a, double b)
        {
            return a + b;
        }
        public static double Sub(double a, double b)
        {
            return a - b;
        }
        public static double Mul(double a, double b)
        {
            return a * b;
        }
        public static double Div(double a, double b)
        {
            if (b == 0) throw new DivideByZeroException("You cannot devide by zero");
            else return a / b;
        }
    }
}
