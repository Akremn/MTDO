using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spusk
{
    class GradientDescent
    {
        static double epsilon1 = 1e-4;
        static double epsilon2 = 1e-8;

        static double[] Gradient(double x1, double x2)
        {
            double df_dx1 = 80 * x1 + 20 * x2 - 10;
            double df_dx2 = 20 * x1 + 60 * x2 + 1;
            return new double[] { df_dx1, df_dx2 };
        }

        static double Function(double x1, double x2)
        {
            return 40 * x1 * x1 + 20 * x1 * x2 + 30 * x2 * x2 - 10 * x1 + x2;
        }

        static double Norm(double[] vector)
        {
            double sum = 0;
            foreach (var v in vector)
            {
                sum += v * v;
            }
            return Math.Sqrt(sum);
        }

        static double LineSearch(double[] x, double[] direction)
        {
            double alpha = 1;
            double beta = 0.5;
            double c = 1e-4;
            double[] gradient = Gradient(x[0], x[1]);
            double f0 = Function(x[0], x[1]);
            double f1;
            double[] xNew = new double[2];

            while (true)
            {
                xNew[0] = x[0] + alpha * direction[0];
                xNew[1] = x[1] + alpha * direction[1];
                f1 = Function(xNew[0], xNew[1]);

                if (f1 <= f0 + c * alpha * (gradient[0] * direction[0] + gradient[1] * direction[1]))
                {
                    break;
                }

                alpha *= beta;
            }

            return alpha;
        }

        static void GradientDescentMethod(double epsilon)
        {
            double[] x = new double[] { 0, 2 };
            int steps = 0;
            int functionCalculations = 0;

            while (true)
            {
                double[] gradient = Gradient(x[0], x[1]);
                functionCalculations += 1;

                if (Norm(gradient) < epsilon)
                {
                    break;
                }

                double[] direction = new double[] { -gradient[0], -gradient[1] };
                double alpha = LineSearch(x, direction);
                functionCalculations += 1;

                x[0] += alpha * direction[0];
                x[1] += alpha * direction[1];
                steps++;
            }

            Console.WriteLine($"Solution with epsilon = {epsilon}");
            Console.WriteLine($"x* = ({x[0]}, {x[1]})");
            Console.WriteLine($"f* = {Function(x[0], x[1])}");
            Console.WriteLine($"Number of steps: {steps}");
            Console.WriteLine($"Number of function calculations: {functionCalculations}");
            Console.WriteLine();
        }

        static void Main()
        {
            GradientDescentMethod(epsilon1);
            GradientDescentMethod(epsilon2);
        }
    }
}
