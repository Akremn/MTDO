using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Po_cord
{
    class CoordinateDescent
    {
        static double f(double x1, double x2) // Функція для обчислення значення функції f(x1, x2)
        {
            return 40 * Math.Pow(x1, 2) + 20 * x1 * x2 + 30 * Math.Pow(x2, 2) - 10 * x1 + x2;
        }

        static (double, double) Gradient(double x1, double x2) // Функція для обчислення градієнта функції f(x1, x2)
        {
            double df_dx1 = 80 * x1 + 20 * x2 - 10; // Парціальна похідна по x1
            double df_dx2 = 20 * x1 + 60 * x2 + 1;  // Парціальна похідна по x2
            return (df_dx1, df_dx2);
        }

        static (double[], double[], int, int) CoordinateDescentMethod(double[] x0, double epsilon) // Метод покоординатного спуску
        {
            double[] x = { x0[0], x0[1] }; // Початкова точка
            double[] x_prev = new double[2]; // Попередня точка
            int steps = 0; // Лічильник кроків
            int functionEvaluations = 0; // Лічильник обчислень значень функції

            while (true) // Цикл до досягнення умови зупинки
            {
                steps++; // Збільшення лічильника кроків
                x_prev[0] = x[0]; // Зберігання попередньої координати x1
                x_prev[1] = x[1]; // Зберігання попередньої координати x2
                (double df_dx1, double df_dx2) = Gradient(x[0], x[1]); // Обчислення градієнта
                functionEvaluations += 2; // Одне обчислення для кожної парціальної похідної

                // Оновлення x1 з урахуванням фіксованого x2
                x[0] = x[0] - 0.01 * df_dx1;

                // Оновлення x2 з урахуванням фіксованого x1
                (df_dx1, df_dx2) = Gradient(x[0], x[1]);
                functionEvaluations += 2;
                x[1] = x[1] - 0.01 * df_dx2;

                // Перевірка умови зупинки
                if (Math.Sqrt(df_dx1 * df_dx1 + df_dx2 * df_dx2) < epsilon)
                    break;
            }

            return (x, x_prev, steps, functionEvaluations); // Повернення результатів
        }

        static void Main() // Головний метод
        {
            double[] x0 = { 0, 2 }; // Початкова точка
            double epsilon1 = 1e-4; // Перша точність
            double epsilon2 = 1e-8; // Друга точність

            // Для точності 10^-4
            var result1 = CoordinateDescentMethod(x0, epsilon1);
            Console.WriteLine("Results for epsilon = 10^-4:"); // Виведення результатів для точності 10^-4
            Console.WriteLine($"Number of steps: {result1.Item3}"); // Кількість кроків
            Console.WriteLine($"Number of function evaluations: {result1.Item4}"); // Кількість обчислень функції
            Console.WriteLine($"Final point: ({result1.Item1[0]}, {result1.Item1[1]})"); // Остаточна точка мінімізації
            Console.WriteLine($"f* ≈ f(x) = {f(result1.Item1[0], result1.Item1[1])}"); // Значення функції в точці мінімізації
            Console.WriteLine();

            // Для точності 10^-8
            var result2 = CoordinateDescentMethod(x0, epsilon2);
            Console.WriteLine("Results for epsilon = 10^-8:"); // Виведення результатів для точності 10^-8
            Console.WriteLine($"Number of steps: {result2.Item3}"); // Кількість кроків
            Console.WriteLine($"Number of function evaluations: {result2.Item4}"); // Кількість обчислень функції
            Console.WriteLine($"Final point: ({result2.Item1[0]}, {result2.Item1[1]})"); // Остаточна точка мінімізації
            Console.WriteLine($"f* ≈ f(x) = {f(result2.Item1[0], result2.Item1[1])}"); // Значення функції в точці мінімізації
        }
    }
}
