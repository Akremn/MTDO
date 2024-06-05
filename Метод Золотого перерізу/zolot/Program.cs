using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zolot
{
    public class GoldenRatio
    {
        private double e, a, b;
        public GoldenRatio(double e, double a, double b)
        {
            this.e = e;
            this.a = a;
            this.b = b;
        }
        private double Func(double x) => 4 * Math.Pow(3 - x, 2 / 3) + 2 * Math.Pow(x, 3);
        public void Evalf()
        {
            double nk = 0;
            double nf = 0;
        start:
            double u = a + ((3 - Math.Sqrt(5)) / 2) * (b - a);
            double v = a + b - u;
            double fu = Func(u);
            double fv = Func(v);
            double x = 0;
            double f = 0;
            while (true)
            {
                if (fu <= fv)
                {
                    a = a;
                    b = v;
                    x = u;
                    f = fu;
                    v = u;
                    fv = fu;
                    u = a + b - v;
                    if (u < v)
                    {
                        fu = Func(u);
                    }
                }
                else
                {
                    a = u;
                    b = b;
                    x = v;
                    f = Func(v);
                    u = v;
                    fu = fv;
                    v = a + b - u;
                    if (u < v)
                    {
                        fv = Func(v);
                    }
                }
                nk++;
                if (!Double.IsNaN(f))
                {
                    nf++;
                }
                if (b - a < e)
                {
                    break;
                }
                if (u < v)
                {
                    continue;
                }
                else
                {
                    goto start;
                }
            }
            Console.WriteLine($"Nk = {nk}");
            Console.WriteLine($"Nf = {nf}");
            Console.WriteLine($"x = {x}");
            Console.WriteLine($"F(x) = {f}");
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("e=0.01");
            GoldenRatio gr = new GoldenRatio(0.01, 0, 2);
            gr.Evalf();
            Console.WriteLine("e=0.0001");
            GoldenRatio gr1 = new GoldenRatio(0.0001, 0, 2);
            gr1.Evalf();
            Console.WriteLine("e=0.00000001");
            GoldenRatio gr2 = new GoldenRatio(0.00000001, 0, 2);
            gr2.Evalf();
        }
    }

}
