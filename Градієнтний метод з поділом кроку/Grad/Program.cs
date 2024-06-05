using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grad
{
    class Program
    {
        static double f(double x1, double x2)
        {
            return 40 * Math.Pow(x1, 2) + 20 * x1 * x2 + 30 * Math.Pow(x2, 2) - 10 * x1 + x2;
        }

        static double f1(double x1, double x2)
        {
            return 80 * x1 + 20 * x2 - 10;
        }

        static double f2(double x1, double x2)
        {
            return 20 * x1 + 60 * x2 + 1;
        }

        static void GradientDescent(double x1, double x2, double epsilon)
        {
            int iterations = 0;
            int functionCalls = 0;
            int maxIterations = 1000000; // максимальна кількість ітерацій

            while (iterations < maxIterations)
            {
                double gradient1 = f1(x1, x2);
                double gradient2 = f2(x1, x2);
                double gradientNorm = Math.Sqrt(Math.Pow(gradient1, 2) + Math.Pow(gradient2, 2));

                if (gradientNorm < epsilon)
                    break;

                iterations++;

                double stepSize = 1.0;
                double newX1, newX2;
                while (true)
                {
                    newX1 = x1 - stepSize * gradient1;
                    newX2 = x2 - stepSize * gradient2;
                    functionCalls++;

                    if (f(newX1, newX2) < f(x1, x2))
                        break;

                    stepSize /= 2.0;

                    if (stepSize < 1e-10) // Avoid too small step size
                        break;
                }

                x1 = newX1;
                x2 = newX2;

                // Проміжний вивід результатів
                if (iterations % 1000 == 0)
                {
                    Console.WriteLine($"Iteration: {iterations}, x1: {x1}, x2: {x2}, f(x): {f(x1, x2)}, Gradient Norm: {gradientNorm}");
                }
            }

            if (iterations == maxIterations)
            {
                Console.WriteLine("Reached maximum number of iterations without converging to the desired precision.");
            }

            Console.WriteLine("Iterations: " + iterations);
            Console.WriteLine("Function calls: " + functionCalls);
            Console.WriteLine("Minimum value found at: x1 = " + x1 + ", x2 = " + x2);
            Console.WriteLine("Minimum function value: " + f(x1, x2));
        }

        static void Main(string[] args)
        {
            double x1 = 0;
            double x2 = 2;
            double epsilon = 1e-4; // або 1e-4 для меншої точності

            GradientDescent(x1, x2, epsilon);
        }
    }
}
