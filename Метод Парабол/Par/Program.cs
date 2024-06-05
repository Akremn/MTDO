using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Par
{
    class Program
    {
        static double f(double x)
        {
            return 4 * Math.Pow(3 - x, 2.0 / 3) + 2 * Math.Pow(x, 3);
        }

        static double ParabolicMinimization(double a, double b, double E, out int Nk, out int Nf)
        {
            double x1, x2, x3, f1, f2, f3, x_min, f_min;
            double eps = E;
            Nk = 0;
            Nf = 0;

            do
            {
                x1 = a;
                x3 = b;
                x2 = (x1 + x3) / 2;
                f1 = f(x1);
                f3 = f(x3);
                f2 = f(x2);
                Nf += 3;

                x_min = 0.5 * ((x2 * x2 - x3 * x3) * f1 + (x3 * x3 - x1 * x1) * f2 + (x1 * x1 - x2 * x2) * f3)
                    / ((x2 - x3) * f1 + (x3 - x1) * f2 + (x1 - x2) * f3);
                f_min = f(x_min);
                Nf++;

                if (x_min < x2)
                {
                    if (f_min < f2)
                    {
                        b = x2;
                        x2 = x_min;
                    }
                    else
                    {
                        a = x_min;
                    }
                }
                else
                {
                    if (f_min < f2)
                    {
                        a = x2;
                        x2 = x_min;
                    }
                    else
                    {
                        b = x_min;
                    }
                }

                Nk++;
            } while (Math.Abs(x3 - x1) > eps && Math.Abs(f3 - f1) > eps);

            return x_min;
        }

        static void Main()
        {
            double a = 0;
            double b = 2;
            double E1 = 1e-2;
            double E2 = 1e-4;
            double E3 = 1e-8;

            int Nk1, Nk2, Nk3, Nf1, Nf2, Nf3;
            double x_min1 = ParabolicMinimization(a, b, E1, out Nk1, out Nf1);
            double x_min2 = ParabolicMinimization(a, b, E2, out Nk2, out Nf2);
            double x_min3 = ParabolicMinimization(a, b, E3, out Nk3, out Nf3);

            Console.WriteLine($"E=1e-2:Nk={Nk1}, Nf={Nf1}, x*={x_min1}, f*={f(x_min1)}");
            Console.WriteLine($"E=1e-4:Nk={Nk1}, Nf={Nf1}, x*={x_min2}, f*={f(x_min2)}");
            Console.WriteLine($"E=1e-8:Nk={Nk1}, Nf={Nf1}, x*={x_min3}, f*={f(x_min3)}");
        }
    }
}
