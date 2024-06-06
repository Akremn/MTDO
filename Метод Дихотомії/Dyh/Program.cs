using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dyh
{
    class DichotomyMethod
    {
        static double Function(double x)
        {
            return 4 * Math.Pow(3 - x, 2.0 / 3.0) + 2 * Math.Pow(x, 2);
        }

        static (double xStar, double fStar, int Nk, int Nf) Dichotomy(double a, double b, double epsilon)
        {
            double delta = epsilon / 2.0;
            int Nk = 0;
            int Nf = 0;

            while ((b - a) / 2.0 > epsilon)
            {
                double x1 = (a + b) / 2.0 - delta;
                double x2 = (a + b) / 2.0 + delta;
                double f1 = Function(x1);
                double f2 = Function(x2);
                Nf += 2;

                if (f1 < f2)
                {
                    b = x2;
                }
                else
                {
                    a = x1;
                }

                Nk++;
            }

            double xStar = (a + b) / 2.0;
            double fStar = Function(xStar);
            

            return (xStar, fStar, Nk, Nf);
        }

        static void Main(string[] args)
        {
            double a = 0;
            double b = 2;
            double[] epsilons = { 1e-2, 1e-4, 1e-8 };

            foreach (var epsilon in epsilons)
            {
                var result = Dichotomy(a, b, epsilon);
                Console.WriteLine($"Epsilon: {epsilon}");
                Console.WriteLine($"Nk = {result.Nk}");
                Console.WriteLine($"Nf = {result.Nf}");
                Console.WriteLine($"x* = {result.xStar}");
                Console.WriteLine($"f* = {result.fStar}");
                Console.WriteLine();
            }
        }
    }

}
