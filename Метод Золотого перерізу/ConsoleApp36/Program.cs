using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp36
{
    class GoldenSectionSearch
    {
        static double Function(double x)
        {
            return 4 * Math.Pow(3 - x, 2.0 / 3) + 2 * Math.Pow(x, 2);
        }

        static (double x, double fx, int Nk, int Nf) GoldenSection(double a, double b, double epsilon)
        {
            double gr = (Math.Sqrt(5) + 1) / 2; // Золоте відношення
            double c = b - (b - a) / gr;
            double d = a + (b - a) / gr;
            int Nk = 0; // Кількість ітерацій
            int Nf = 0; // Кількість обчислень функції

            double fc = Function(c);
            double fd = Function(d);

            while (Math.Abs(b - a) > epsilon)
            {
                Nk++;
                if (fc < fd)
                {
                    b = d;
                    d = c;
                    fd = fc;
                    c = b - (b - a) / gr;
                    fc = Function(c);
                }
                else
                {
                    a = c;
                    c = d;
                    fc = fd;
                    d = a + (b - a) / gr;
                    fd = Function(d);
                }
                Nf++;
            }

            double x = (a + b) / 2;
            double fx = Function(x);
            return (x, fx, Nk, Nf);
        }

        static void Main()
        {
            double a = 0;
            double b = 2;

            double[] epsilons = { 1e-2, 1e-4, 1e-8 };

            foreach (double epsilon in epsilons)
            {
                var result = GoldenSection(a, b, epsilon);
                Console.WriteLine($"Epsilon = {epsilon}");
                Console.WriteLine($"Nk = {result.Nk}");
                Console.WriteLine($"Nf = {result.Nf}");
                Console.WriteLine($"x* = {result.x}");
                Console.WriteLine($"f* = {result.fx}");
                Console.WriteLine();
            }
        }
    }
}
