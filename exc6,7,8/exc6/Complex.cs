using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exc6
{
    class Complex
    {
        protected double a;
        protected double b;

        public Complex(double r, double i)
        {
            a = r;
            b = i;
        }

        public static Complex operator+ (Complex left, Complex right)
        {

            return new Complex(left.a + right.a, left.b + right.b);
        }

        public static Complex operator -(Complex left, Complex right)
        {

            return new Complex(left.a - right.a, left.b - right.b);
        }

        public static Complex operator *(Complex left, Complex right)
        {

            return new Complex(left.a*right.a - left.b*right.b, left.a*right.b + left.b*right.a);
        }

        public static Complex operator /(Complex left, Complex right)
        {

            return new Complex(((left.a*right.a + left.b*right.b) / (Math.Pow(right.a,2) + Math.Pow(right.b,2))), ((left.b*right.a - left.a*right.b) / (Math.Pow(right.a,2) + Math.Pow(right.b,2))));
        }

        public static Boolean operator ==(Complex left, Complex right)
        {
            return left.a == right.a && left.b == right.b;
        }

        public static Boolean operator !=(Complex left, Complex right)
        {
            return !(left==right);
        }

        public static explicit operator Complex(double d)
        {
            return new Complex(d, 0);
        }

        public static implicit operator Double(Complex c)
        {
            return c.a;
        }

    }
}
