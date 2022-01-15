using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetModeler
{
    class ActivationFunction
    {
        public static double Signum(double arg)
        {
            if (arg > 0) return 1;
            if (arg < 0) return -1;
            else return 0;
        }

        public static double SignumPrim(double arg)
        {
            return 1; // nie istnieje;
        }

        public static double SigmoidUnipolar(double arg)
        {
            double bufor = 1 / (1 + Math.Pow(Global.E, ((-1) * Global.Beta * arg)));
            return bufor;
        }

        public static double SigmoidUnipolPrim(double arg)
        {
            double bufor = Global.Beta * SigmoidUnipolar(arg) * (1 - SigmoidUnipolar(arg));
            return bufor;
        }

        public static double SigmoidBipol(double arg)
        {
            double bufor = (1 - Math.Pow(Global.E, ((-1) * Global.Beta * arg))) / (1 + Math.Pow(Global.E, ((-1) * Global.Beta * arg)));
            return bufor;
        }

        public static double SigmoidBipolarPrim(double arg)
        {
            double bufor = 2 * Global.Beta * (1 - Math.Pow(SigmoidBipol(arg), 2));
            return bufor;
        }

        public static double Linear(double arg)
        {
            double a = 1;
            double b = 0;
            double bufor = a * arg + b;
            return bufor;
        }

        public static double LinearPrim(double arg)
        {
            double a = 1;
            double bufor = a;
            return bufor;
        }
    }
}
