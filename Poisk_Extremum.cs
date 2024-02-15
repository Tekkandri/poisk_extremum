using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace poisk_extremum
{
    public class Methods
    {
        private int N;
        private double epsilon;

        private string x_sec = "";
        private int parabola_n = 0;
        private double parabola_value = 0;

        private double gr_value = 0;
        private int gr_n = 0;

        private double coord_value = 0;
        private int coord_n = 0;

        private double grad_value = 0;
        private int grad_n = 0;

        public Methods(int n, double epsilon)
        {
            this.N = n;
            this.epsilon = epsilon;
        }
        public int get_n()
        {
            return this.N;
        }
        public string get_sec()
        {
            return this.x_sec;
        }
        public double get_parabola_value()
        {
            return Math.Round(this.parabola_value, 3);
        }
        public int get_parabola_n()
        {
            return this.parabola_n;
        }
        public double get_gr_value()
        {
            return Math.Round(this.gr_value, 3);
        }
        public int get_gr_n()
        {
            return this.gr_n;
        }
        public double get_coord_value()
        {
            return Math.Round(this.coord_value, 3);
        }
        public int get_coord_n()
        {
            return this.coord_n;
        }
        public double get_grad_value()
        {
            return Math.Round(this.grad_value, 3);
        }
        public int get_grad_n()
        {
            return this.grad_n;
        }

        private int get_fibbonachy(int n)
        {
            int fibbonachy_1 = 1;
            int fibbonachy_2 = 2;
            int fib_sum = 0;

            for (int i = 0; i < n-2; i++)
            {
                fib_sum = fibbonachy_1 + fibbonachy_2;
                fibbonachy_1 = fibbonachy_2;
                fibbonachy_2 = fib_sum;
            }
            return fibbonachy_2;
        }
        public double passive_method(int n)
        {
            return (1+this.epsilon)/(n/2 + 1);
        }
        public double dichotomy_method(int n)
        {
            return Math.Pow(2,(-n/2))+(1- Math.Pow(2, (-n / 2)))*this.epsilon;
        }

        public double fibb_method(int n)
        {
            double result = 0;
            if (n == 1)
                result = 1;
            else if (n == 2)
                result = 0.5 + this.epsilon / 2;
            else
                result = (1 / (double)get_fibbonachy(n)) + ((double)get_fibbonachy(n - 2) * this.epsilon / (double)get_fibbonachy(n));
            return result;
        }
        public double gold_method(int n)
        {
            return 1 /Math.Pow((1 + Math.Sqrt(5)) / 2, n-1);
        }
        public double parabola_method(double x, double eps)
        {
            double next_x = x - (Math.Exp(x) + 2 * x) / (Math.Exp(x) + 2);
            this.parabola_value = Math.Exp(next_x) + Math.Pow(next_x, 2);

            while (Math.Abs(this.parabola_value - (Math.Exp(x) + Math.Pow(x, 2))) > eps)
            {
                this.x_sec += $"x{this.parabola_n+1}: {Math.Round(next_x,3)}\r\n";
                x = next_x;
                next_x = x - (Math.Exp(x) + 2 * x) / (Math.Exp(x) + 2);
                this.parabola_n++;
                this.parabola_value = Math.Exp(next_x) + Math.Pow(next_x, 2);
            }

            return next_x;
        }
        public double gold_method_extr(double a, double b, double delta)
        {
            double t = (-1 + Math.Sqrt(5)) / 2;

            double x1 = a + t * (b - a);
            double x2 = b - t * (b - a);

            double fx1 = Math.Exp(x1) + Math.Pow(x1, 2);
            double fx2 = Math.Exp(x2) + Math.Pow(x2, 2);

            while (Math.Abs(b - a) >= delta)
            {
                if (fx1 > fx2)
                {
                    b = x2;
                    x2 = x1;
                    x1 = a + t* (b - a);
                    fx1 = fx2;
                    fx2 = Math.Exp(x2) + Math.Pow(x2, 2);
                }
                else if (fx2 > fx1)
                {
                    a = x1;
                    x1 = x2;
                    x2 = b - t * (b - a);
                    fx2 = fx1;
                    fx1 = Math.Exp(x1) + Math.Pow(x1, 2);
                }
                this.gr_n++;
            }

            this.gr_value = Math.Exp((a + b) / 2) + Math.Pow((a + b) / 2, 2);

            return (a + b) / 2;
        }
        public string grad_method(string x0_str, double lambda, double eps)
        {
            double[] x = new double[2];
            string[] values = x0_str.Split(' ');
            for (int i = 0; i < x.Length; i++)
                x[i] = double.Parse(values[i]);

            double dfdx1 = Math.Exp(Math.Pow(x[0], 2) + Math.Pow(x[1], 2)) * 2 * x[0] + 3.5;
            double dfdx2 = Math.Exp(Math.Pow(x[0], 2) + Math.Pow(x[1], 2)) * 2 * x[1] - 2;

            while (Math.Sqrt(Math.Pow(dfdx1,2) + Math.Pow(dfdx2,2)) > epsilon)
            {
                this.grad_n++;

                double temp = x[0];
                x[0] = x[0] - lambda * (Math.Exp(Math.Pow(x[0], 2) + Math.Pow(x[1], 2)) * 2 * x[0] + 3.5);
                x[1] = x[1] - lambda * (Math.Exp(Math.Pow(temp, 2) + Math.Pow(x[1], 2)) * 2 * x[1] - 2);

                dfdx1 = Math.Exp(Math.Pow(x[0], 2) + Math.Pow(x[1], 2)) * 2 * x[0] + 3.5;
                dfdx2 = Math.Exp(Math.Pow(x[0], 2) + Math.Pow(x[1], 2)) * 2 * x[1] - 2;
            }

            this.grad_value = Math.Exp(Math.Pow(x[0], 2) + Math.Pow(x[1], 2)) + 3.5 * x[0] - 2 * x[1];

            return $"{Math.Round(x[0],2)} {Math.Round(x[1], 2)}";
        }
        public string coord_method(string x0_str, double lambda, double eps)
        {
            double[] x = new double[2];
            string[] values = x0_str.Split(' ');
            for (int i = 0; i < x.Length; i++)
                x[i] = double.Parse(values[i]);

            double[] nextx = new double[2];

            nextx[0] = x[0] + lambda;
            nextx[1] = x[1];

            double fx = Math.Exp(Math.Pow(x[0], 2) + Math.Pow(x[1], 2)) + 3.5 * x[0] - 2 * x[1];
            double fnextx = Math.Exp(Math.Pow(nextx[0], 2) + Math.Pow(nextx[1], 2)) + 3.5 * nextx[0] - 2 * nextx[1];

            this.coord_n++;
            int j = 1;
            while (Math.Abs(fnextx - fx) >= eps)
            {
                this.coord_n++;
                if(fnextx >= fx)
                {
                    nextx[j] = x[j] - lambda;
                    fnextx = Math.Exp(Math.Pow(nextx[0], 2) + Math.Pow(nextx[1], 2)) + 3.5 * nextx[0] - 2 * nextx[1];
                    if(fnextx >= fx)
                    {
                        nextx[j] = x[j];
                    }
                    else
                    {
                        x[j] = nextx[j];
                        nextx[j] = x[j] + lambda;
                    }
                }
                else
                {
                    x[j] = nextx[j];
                    nextx[j] = x[j] + lambda;
                }
                if (j == 0)
                    j = 1;
                else
                    j = 0;


                fx = Math.Exp(Math.Pow(x[0], 2) + Math.Pow(x[1], 2)) + 3.5 * x[0] - 2 * x[1];
                fnextx = Math.Exp(Math.Pow(nextx[0], 2) + Math.Pow(nextx[1], 2)) + 3.5 * nextx[0] - 2 * nextx[1];
            }

            this.coord_value = fnextx;

            return $"{Math.Round(nextx[0], 2)} {Math.Round(nextx[1], 2)}";
        }
    }
}
