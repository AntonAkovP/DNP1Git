using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DNP
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("2+3=" + MathLib.Sum(2, 3));
            Console.WriteLine("2-3=" + MathLib.Sub(2, 3));
            Console.WriteLine("2*3=" + MathLib.Mul(2, 3));
            Console.WriteLine("2/3=" + MathLib.Div(2, 3));

            //ideally every time you wanted to devide you'd run a piece of code with the surround/catch block
            Console.Write("2/0=");
            try { Console.Write(MathLib.Div(2, 0)); } catch (DivideByZeroException) { Console.WriteLine("Cannot divide by 0"); }

            Console.WriteLine("2.1+3.3=" + MathLib.Sum(2.1, 3.3));
            Console.WriteLine("2.2-3.3=" + MathLib.Sub(2.2, 3.3));
            Console.WriteLine("2.4*3=" + MathLib.Mul(2.4, 3));
            Console.WriteLine("2.4/3=" + MathLib.Div(2.4, 3));

            Console.Write("2.5/0=");
            try { Console.Write(MathLib.Div(2.5, 0)); } catch (DivideByZeroException) { Console.Write("Cannot divide by 0"); }

            Console.ReadKey();

        }
    }
}
