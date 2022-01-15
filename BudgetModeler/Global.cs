using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetModeler
{
    class Global
    {
        const double e = 2.7182818284;
        static double beta = 1;
        static double ni;
        static double learningAccuracy;
        static int maxEpok;

        public static double E => e;

        public static double Beta { get => beta; set => beta = value; }

        public static double NI { get => ni; set => ni = value; }

        internal static double LearningAccuracy { get => learningAccuracy; set => learningAccuracy = value; }

        public static int MaxEpok { get => maxEpok; set => maxEpok = value; }

        public const string Signum = "Signum";
        public const string Sigmoidalna_Unipolarna = "Sigmoidalna Unipolarna";
        public const string Sigmoidalna_Bipolarna = "Sigmoidalna Bipolarna";
        public const string Liniowa = "Liniowa";

        public const string Summary = "Summary";
    }
}
