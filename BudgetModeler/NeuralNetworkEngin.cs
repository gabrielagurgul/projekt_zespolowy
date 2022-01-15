using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetModeler
{
    class NeuralNetworkEngin
    {
        int dzielnikWejscia;

        public delegate double WywaloajFunkcje(double arg);
        WywaloajFunkcje F;

        List<int> MacierzLiczbNeuronow;         //          [iWarstwy]

        List<List<double>> MacierzWejsc;        //          [iWarstwy]  [iNeurona]
        List<double> MacierzWyjsc;              //                      [iNeurona]

        List<List<List<double>>> MacierzWag;    //          [iWarstwy]  [iNeurona]  [iPerceptrona]
        List<List<double>> MacierzBiasow;       //          [iWarstwy]  [iNeurona]

        public NeuralNetworkEngin(List<int> macierzLiczbNeuronow, string F, List<List<List<double>>> macierzWag, List<List<double>> macierzBiasow)
        {
            WybierzFunkcjeAktywacji(F);
            this.UstawMacierzLiczbNeuronow(macierzLiczbNeuronow);
            MacierzWag = macierzWag;
            MacierzBiasow = macierzBiasow;

            this.WygenerujMacierzWejsc();
            this.WygenerujMacierzWyjsc();
        }
            private void WybierzFunkcjeAktywacji(string nazwaFunkcji)
            {
                switch (nazwaFunkcji)
                {
                    case Global.Signum:
                        F = new WywaloajFunkcje(ActivationFunction.Signum);
                        break;
                    case Global.Sigmoidalna_Unipolarna:
                        F = new WywaloajFunkcje(ActivationFunction.SigmoidUnipolar);
                        break;
                    case Global.Sigmoidalna_Bipolarna:
                        F = new WywaloajFunkcje(ActivationFunction.SigmoidBipol);
                        break;
                    case Global.Liniowa:
                        F = new WywaloajFunkcje(ActivationFunction.Linear);
                        break;
                }
            }
            private void UstawMacierzLiczbNeuronow(List<int> LiczbaNeuronow)
            {
                MacierzLiczbNeuronow = new List<int>();
                MacierzLiczbNeuronow.AddRange(LiczbaNeuronow);
            }

            private void WygenerujMacierzWejsc()
            {
                MacierzWejsc = new List<List<double>>();

                for (int iWarstwy = 0; iWarstwy < MacierzLiczbNeuronow.Count - 1; iWarstwy++)
                {
                    List<double> buforWarstwa = new List<double>();

                    for (int iNeurona = 0; iNeurona < MacierzLiczbNeuronow[iWarstwy]; iNeurona++)
                    {
                        buforWarstwa.Add(0);
                    }

                    MacierzWejsc.Add(buforWarstwa);
                }
            }
            private void WygenerujMacierzWyjsc()
            {
                MacierzWyjsc = new List<double>();

                List<double> bufor = new List<double>();
                for (int i = 0; i < MacierzLiczbNeuronow[MacierzLiczbNeuronow.Count-1]; i++)
                {
                    bufor.Add(0);
                }
                MacierzWyjsc = bufor;
            }

        public List<double> WyliczOdpowiedz(List<double> DaneWejsciowe)
        {
            if (DaneWejsciowe.Count != MacierzWejsc[0].Count) return null;

            List<double> buforWektoraWyjscia= new List<double>();

            UstawMacierzJakoWarswaWejsiowa(DaneWejsciowe);
            WyliczWyjscie();

            foreach (double bufor in MacierzWyjsc) buforWektoraWyjscia.Add(bufor);

            return buforWektoraWyjscia;
        }

        private List<List<double>> NormalizujWzorceWejscia(List<List<double>> wzorceWejscia)
        {
            int dzielnik = WyliczDzielnikWejscia(wzorceWejscia);

            List<List<double>> bufor = new List<List<double>>();

            for (int i = 0; i < wzorceWejscia.Count; i++)
            {
                List<double> buforWzorca = new List<double>();
                for (int j = 0; j < wzorceWejscia[i].Count; j++)
                {
                    buforWzorca.Add(wzorceWejscia[i][j]);
                }
                bufor.Add(buforWzorca);
            }

            for (int iWzorca = 0; iWzorca < wzorceWejscia.Count; iWzorca++)
            {
                for (int iNeurona = 0; iNeurona < wzorceWejscia[iWzorca].Count; iNeurona++)
                {
                    bufor[iWzorca][iNeurona] /= dzielnik;
                }
            }
            return bufor;
        }
        private int WyliczDzielnikWejscia(List<List<double>> bufor)
        {
            int dzielnik;
            if (dzielnikWejscia == 0)
            {
                double max = WyliczMaxWartoscBezwzgledna(bufor);
                dzielnik = 1;

                while (dzielnik < max) dzielnik *= 2;
                this.dzielnikWejscia = dzielnik;
            }
            else
            {
                dzielnik = this.dzielnikWejscia;
            }

            return dzielnik;
        }
        private double WyliczMaxWartoscBezwzgledna(List<List<double>> macierz)
        {
            List<List<double>> bufor = new List<List<double>>();

            double max = 0;
            for (int iWzorca = 0; iWzorca < macierz.Count; iWzorca++)
            {
                List<double> buforWzorca = new List<double>();

                for (int iNeurona = 0; iNeurona < macierz[iWzorca].Count; iNeurona++)
                {
                    buforWzorca.Add(Math.Abs(macierz[iWzorca][iNeurona]));
                }

                bufor.Add(buforWzorca);
            }

            foreach (List<double> wzorzec in bufor)
            {
                foreach (double wejscie in wzorzec)
                {
                    if (max < wejscie) max = wejscie;
                }
            }

            return max;
        }

        private void UstawMacierzJakoWarswaWejsiowa(List<double> wzorzec)
        {
            for (int i = 0; i < wzorzec.Count; i++) MacierzWejsc[0][i] = wzorzec[i];
        }

        private void WyliczWyjscie()
        {
            bool WarstwaWyjsciowa = false;

            for (int iWarstwy = 0; iWarstwy < MacierzWag.Count; iWarstwy++)
            {
                if (iWarstwy == MacierzWag.Count - 1) WarstwaWyjsciowa = true;

                for (int iNeurona = 0; iNeurona < MacierzWag[iWarstwy].Count; iNeurona++)
                {
                    double bufor = 0;

                    for (int iPerceptronu = 0; iPerceptronu < MacierzWag[iWarstwy][iNeurona].Count; iPerceptronu++)
                    {
                        bufor += MacierzWag[iWarstwy][iNeurona][iPerceptronu] * MacierzWejsc[iWarstwy][iPerceptronu];
                    }

                    bufor += MacierzBiasow[iWarstwy][iNeurona];

                    if (WarstwaWyjsciowa) MacierzWyjsc[iNeurona] = F(bufor);//ActivationFunction.Linear(bufor);
                    else MacierzWejsc[iWarstwy + 1][iNeurona] = F(bufor);
                }
            }
        }
    }
}
