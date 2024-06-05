using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fi
{
    class Program
    {
        static double f(double x)
        {
            return 4 * Math.Pow(3 - x, 2.0 / 3) + 2 * Math.Pow(x, 3);
        }

        static double FibonacciMinimization(double a, double b, double epsilon, out int Nk, out int Nf)
        {
            int n = (int)Math.Ceiling(Math.Log((b - a) / epsilon) / Math.Log((1 + Math.Sqrt(5)) / 2)); // Fibonacci number closest to (b - a) / epsilon
            double[] fib = new double[n + 1];
            fib[0] = 1;
            fib[1] = 1;
            for (int i = 2; i <= n; i++)
            {
                fib[i] = fib[i - 1] + fib[i - 2];
            }

            double x1 = a + (b - a) * fib[n - 2] / fib[n];
            double x2 = a + (b - a) * fib[n - 1] / fib[n];
            double f1 = f(x1);
            double f2 = f(x2);
            Nk = 0;
            Nf = 0;
            while (Math.Abs(b - a) > epsilon)
            {
                if (f1 < f2)
                {
                    b = x2;
                    x2 = x1;
                    f2 = f1;
                    x1 = a + (b - a) * fib[n - Nk - 2] / fib[n - Nk];
                    f1 = f(x1);
                }
                else
                {
                    a = x1;
                    x1 = x2;
                    f1 = f2;
                    x2 = a + (b - a) * fib[n - Nk - 1] / fib[n - Nk];
                    f2 = f(x2);
                }
                Nk++;
                Nf++;
            }
            return (a + b) / 2;
        }

        static void Main(string[] args)
        {
            double[] epsilons = { 1e-2, 1e-4, 1e-8 };

            foreach (var epsilon in epsilons)
            {
                int Nk, Nf;
                double minX = FibonacciMinimization(0, 2, epsilon, out Nk, out Nf);
                double minF = f(minX);
                Console.WriteLine($"Epsilon = {epsilon}");
                Console.WriteLine($"x* = {minX}, f* = {minF}");
                Console.WriteLine($"Nk: {Nk}");
                Console.WriteLine($"Nf: {Nf}");
                Console.WriteLine();
            }
        }
    }
}
