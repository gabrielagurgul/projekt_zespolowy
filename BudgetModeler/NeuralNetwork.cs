using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace BudgetModeler
{
    public class NeuralNetwork
    {
        static int seed = 0;

        int dzielnikWejscia;
        int dzielnikWyjscia;

        public delegate double WywaloajFunkcje(double arg);
        WywaloajFunkcje F;
        string F_Name;
        public delegate double WywaloajFunkcjePrim(double arg);
        WywaloajFunkcjePrim Fprim;

        List<int> MacierzLiczbNeuronow;         //          [iWarstwy]

        List<List<double>> WzorceWejscia;       //[iWzorca]             [iNeurona]
        List<List<double>> WzorceWyjscia;       //[iWzorca]             [iNeurona]

        List<List<double>> MacierzWejsc;        //          [iWarstwy]  [iNeurona]
        List<List<double>> MacierzWyjsc;        //[iWzorca]             [iNeurona]

        List<List<List<double>>> MacierzWag;    //          [iWarstwy]  [iNeurona]  [iPerceptrona]
        List<List<double>> MacierzBiasow;       //          [iWarstwy]  [iNeurona]

        List<List<List<double>>> MacierzBledow; //[iWzorca] [iWarstwy]  [iNeurona]

        List<List<List<double>>> MacierzWPB;    //[iWzorca] [iWarstwy]  [iNeurona]

        List<List<double>> WzorceWejscioweTestow;//[iWzorca]             [iNeurona]
        List<List<double>> WzorceWyjscioweTestow;//[iWzorca]             [iNeurona]

        List<double> BuforSrednigoBleduAbsolutnego;// [iEpoch]

        public delegate void DrawChartFromLernResultOfNN(List<double> arg);
        DrawChartFromLernResultOfNN drawChartFromLernResultOfNN;

        public delegate void FillLabelWithInformationAboutLerningProgres(string arg);
        static FillLabelWithInformationAboutLerningProgres fillLabelWithInformationAboutLerningProgres;

        private Timer czasomierz;
        private bool FlagCzasAkcji;
        private bool FlagOdswiezanieZakonczone;

        public NeuralNetwork(List<List<double>> WzorceWejscia, List<List<double>> WzorceWyjscia, List<int> LiczbaNeuronow, string FunkcjaAktywacji)
        {
            FlagCzasAkcji = false;
            FlagOdswiezanieZakonczone = true;
            dzielnikWejscia = 0;
            dzielnikWyjscia = 0;
            this.UstawWzorce(WzorceWejscia, WzorceWyjscia);
            this.UstawMacierzLiczbNeuronow(LiczbaNeuronow);

            this.MacierzWejsc = new List<List<double>>();
            this.MacierzWag = new List<List<List<double>>>();
            this.MacierzBiasow = new List<List<double>>();
            this.MacierzWyjsc = new List<List<double>>();
            this.MacierzBledow = new List<List<List<double>>>();
            this.MacierzWPB = new List<List<List<double>>>();

            this.WygenerujMacierze();

            this.WybierzFunkcjeAktywacji(FunkcjaAktywacji);
        }

        public NeuralNetwork()
        {
            dzielnikWejscia = 0;
            dzielnikWyjscia = 0;

            this.MacierzWejsc = new List<List<double>>();
            this.MacierzWag = new List<List<List<double>>>();
            this.MacierzBiasow = new List<List<double>>();
            this.MacierzWyjsc = new List<List<double>>();
            this.MacierzBledow = new List<List<List<double>>>();
            this.MacierzWPB = new List<List<List<double>>>();
        }

        public void SetDelegatDrawChartFromLernResultOfNN(DrawChartFromLernResultOfNN delegat, List<List<double>> WzorceWejscioweTestow, List<List<double>> WzorceWyjscioweTestow)
        {
            this.WzorceWejscioweTestow = WzorceWejscioweTestow;
            this.WzorceWyjscioweTestow = WzorceWyjscioweTestow;
            drawChartFromLernResultOfNN = delegat;
        }

        public void SetDelegatDrawChartFromErrorOfNN(DrawChartFromLernResultOfNN delegat)
        {
            drawChartFromLernResultOfNN = delegat;
        }

        public void ResetDelegatDrawChartFromLernResultOfNN()
        {
            this.WzorceWejscioweTestow = null;
            this.WzorceWyjscioweTestow = null;

            drawChartFromLernResultOfNN = null;
        }

        public static void SetDelegateFillLabelWithInformationAboutLerningProgres(FillLabelWithInformationAboutLerningProgres delegat)
        {
            fillLabelWithInformationAboutLerningProgres = delegat;
        }

        public static void ResetDelegateFillLabelWithInformationAboutLerningProgres()
        {
            fillLabelWithInformationAboutLerningProgres = null;
        }


        private void UstawTimer()
        {
            czasomierz = new Timer(100);
            // Hook up the Elapsed event for the timer. 
            czasomierz.Elapsed += OnTimedEvent;
            czasomierz.AutoReset = true;
            czasomierz.Enabled = true;
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            FlagCzasAkcji = true;
        }

        private void StartCzasomierz()
        {
            if (drawChartFromLernResultOfNN == null) return;//|| WzorceWejscioweTestow == null) return;
            UstawTimer();
            this.czasomierz.Start();
        }

        private void StopCzasomierz()
        {
            //if (drawChartFromLernResultOfNN == null || WzorceWejscioweTestow == null) return;
            this.czasomierz.Stop();
            this.czasomierz.Dispose();
        }
        // ===================================== Generowanie Macierzy =====================================
        public void WygenerujMacierze()
        {
            this.WygenerujMacierzWejsc();
            this.WygenerujMacierzWag();
            this.WygenerujMacierzBiasow();
            this.WygenerujMacierzWyjsc();
            this.WygenerujMacierzBledow();
            this.WygenerujMacierzWPB();
        }

        public void UstawWzorce(List<List<double>> WzorceWejscia, List<List<double>> WzorceWyjscia)
        {
            WzorceWejscia = NormalizujWzorceWejscia(WzorceWejscia);
            WzorceWyjscia = NormalizujWzorceWyjscia(WzorceWyjscia);

            this.WzorceWejscia = WzorceWejscia;
            this.WzorceWyjscia = WzorceWyjscia;

            ShowMatrixCountig(WzorceWejscia, "WzorceWejscia");
            ShowMatrixCountig(WzorceWyjscia, "WzorceWyjscia");
        }
        public List<List<double>> NormalizujWzorceWejscia(List<List<double>> wzorceWejscia)
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
        public int WyliczDzielnikWejscia(List<List<double>> bufor)
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
        public double WyliczMaxWartoscBezwzgledna(List<List<double>> macierz)
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
        public List<List<double>> NormalizujWzorceWyjscia(List<List<double>> wzorceWejscia)
        {
            int dzielnik = WyliczDzielnikWyjscia(wzorceWejscia);

            List<List<double>> bufor = wzorceWejscia;

            for (int iWzorca = 0; iWzorca < wzorceWejscia.Count; iWzorca++)
            {
                for (int iNeurona = 0; iNeurona < wzorceWejscia[iWzorca].Count; iNeurona++)
                {
                    bufor[iWzorca][iNeurona] /= dzielnik;
                }
            }
            return bufor;
        }
        public int WyliczDzielnikWyjscia(List<List<double>> bufor)
        {
            int dzielnik;
            if (dzielnikWyjscia == 0)
            {
                double max = WyliczMaxWartoscBezwzgledna(bufor);
                dzielnik = 1;

                while (dzielnik < max) dzielnik *= 2;
                this.dzielnikWyjscia = dzielnik;
            }
            else
            {
                dzielnik = this.dzielnikWyjscia;
            }

            return dzielnik;
        }

        public void UstawMacierzLiczbNeuronow(List<int> LiczbaNeuronow)
        {
            MacierzLiczbNeuronow = new List<int>();
            MacierzLiczbNeuronow.Add(WzorceWejscia[0].Count);
            MacierzLiczbNeuronow.AddRange(LiczbaNeuronow);
        }

        private void WygenerujMacierzWejsc()
        {
            for (int iWarstwy = 0; iWarstwy < MacierzLiczbNeuronow.Count - 1; iWarstwy++)
            {
                List<double> buforWarstwa = new List<double>();

                for (int iNeurona = 0; iNeurona < MacierzLiczbNeuronow[iWarstwy]; iNeurona++)
                {
                    buforWarstwa.Add(0);
                }

                MacierzWejsc.Add(buforWarstwa);
            }

            ShowMatrixCountig(MacierzWejsc, "MacierzWejsc");
        }

        private void WygenerujMacierzWag()
        {
            ZwymiarujMacierzWag();
            LosujMacierzWag();

            ShowMatrixCountig(MacierzWag, "MacierzWag");
        }
        private void ZwymiarujMacierzWag()
        {
            for (int iWarstwa = 0; iWarstwa < MacierzLiczbNeuronow.Count - 1; iWarstwa++)
            {
                List<List<double>> buforWarstwa = new List<List<double>>();
                for (int iNeuronow = 0; iNeuronow < MacierzLiczbNeuronow[iWarstwa + 1]; iNeuronow++)
                {
                    List<double> buforNeuron = new List<double>();
                    for (int iPerceptronow = 0; iPerceptronow < MacierzLiczbNeuronow[iWarstwa]; iPerceptronow++)
                    {
                        buforNeuron.Add(0);
                    }

                    buforWarstwa.Add(buforNeuron);
                }

                MacierzWag.Add(buforWarstwa);
            }
        }
        private void LosujMacierzWag()
        {
            Random rand = new Random(seed++);

            for (int iWarstwa = 0; iWarstwa < MacierzWag.Count; iWarstwa++)
            {
                for (int iNeuronow = 0; iNeuronow < MacierzWag[iWarstwa].Count; iNeuronow++)
                {
                    for (int iPerceptronow = 0; iPerceptronow < MacierzWag[iWarstwa][iNeuronow].Count; iPerceptronow++)
                    {
                        MacierzWag[iWarstwa][iNeuronow][iPerceptronow] = (rand.NextDouble() * 0.2) - 0.1;
                    }
                }
            }
        }

        private void WygenerujMacierzBiasow()
        {
            ZwymiarujMacierzBiasow();
            LosujMacierzBiasow();

            ShowMatrixCountig(MacierzBiasow, "MacierzBiasow");
        }
        private void ZwymiarujMacierzBiasow()
        {
            for (int iWarstwy = 1; iWarstwy < MacierzLiczbNeuronow.Count; iWarstwy++)
            {
                List<double> buforWarstwa = new List<double>();

                for (int iNeuronow = 0; iNeuronow < MacierzLiczbNeuronow[iWarstwy]; iNeuronow++)
                {
                    buforWarstwa.Add(0);
                }

                MacierzBiasow.Add(buforWarstwa);
            }
        }
        private void LosujMacierzBiasow()
        {
            Random rand = new Random(seed++);

            for (int iWarstwy = 0; iWarstwy < MacierzBiasow.Count; iWarstwy++)
            {
                for (int iNeuronow = 0; iNeuronow < MacierzBiasow[iWarstwy].Count; iNeuronow++)
                {
                    MacierzBiasow[iWarstwy][iNeuronow] = (rand.NextDouble() * 0.2) - 1;
                }
            }
        }

        private void WygenerujMacierzWyjsc()
        {
            foreach (List<double> wzorzec in WzorceWyjscia)
            {
                List<double> bufor = new List<double>();
                for (int i = 0; i < wzorzec.Count; i++)
                {
                    bufor.Add(0);
                }
                MacierzWyjsc.Add(bufor);
            }

            ShowMatrixCountig(MacierzWyjsc, "MacierzWyjsc");
        }

        private void WygenerujMacierzBledow()
        {
            for (int iWzorca = 0; iWzorca < WzorceWejscia.Count; iWzorca++)
            {
                List<List<double>> buforWzorca = new List<List<double>>();

                for (int iWarstw = 1; iWarstw < MacierzLiczbNeuronow.Count; iWarstw++)
                {
                    List<double> buforWarstwa = new List<double>();

                    for (int iNeuronow = 0; iNeuronow < MacierzLiczbNeuronow[iWarstw]; iNeuronow++)
                    {
                        buforWarstwa.Add(0);
                    }

                    buforWzorca.Add(buforWarstwa);
                }

                MacierzBledow.Add(buforWzorca);
            }

            WyzerujMacierzBledow();

            ShowMatrixCountig(MacierzBledow, "MacierzBledow");
        }
        public void WyzerujMacierzBledow()
        {
            for (int iWzorca = 0; iWzorca < WzorceWejscia.Count; iWzorca++)
            {
                for (int iWarstwy = 0; iWarstwy < MacierzBledow[iWzorca].Count; iWarstwy++)
                {
                    for (int iNeurona = 0; iNeurona < MacierzBledow[iWzorca][iWarstwy].Count; iNeurona++)
                    {
                        MacierzBledow[iWzorca][iWarstwy][iNeurona] = 0;
                    }
                }
            }
        }
        public void WyzerujMacierzBledow(int iWzorca)
        {
            for (int iWarstwy = 0; iWarstwy < MacierzBledow[iWzorca].Count; iWarstwy++)
            {
                for (int iNeurona = 0; iNeurona < MacierzBledow[iWzorca][iWarstwy].Count; iNeurona++)
                {
                    MacierzBledow[iWzorca][iWarstwy][iNeurona] = 0;
                }
            }
        }

        private void WygenerujMacierzWPB()
        {
            for (int iWzorca = 0; iWzorca < WzorceWejscia.Count; iWzorca++)
            {
                List<List<double>> buforWzorca = new List<List<double>>();

                for (int iWarstw = 1; iWarstw < MacierzLiczbNeuronow.Count - 1; iWarstw++)
                {
                    List<double> buforWarstwa = new List<double>();

                    for (int iNeuronow = 0; iNeuronow < MacierzLiczbNeuronow[iWarstw]; iNeuronow++)
                    {
                        buforWarstwa.Add(0);
                    }

                    buforWzorca.Add(buforWarstwa);
                }
                MacierzWPB.Add(buforWzorca);
            }

            WyzerujMacierzWPB();

            ShowMatrixCountig(MacierzWPB, "MacierzWPB");
        }
        public void WyzerujMacierzWPB()
        {
            for (int iWzorca = 0; iWzorca < WzorceWejscia.Count; iWzorca++)
            {
                for (int iWarstwy = 0; iWarstwy < MacierzWPB[iWzorca].Count; iWarstwy++)
                {
                    for (int iNeurona = 0; iNeurona < MacierzWPB[iWzorca][iWarstwy].Count; iNeurona++)
                    {
                        MacierzWPB[iWzorca][iWarstwy][iNeurona] = 0;
                    }
                }
            }
        }
        public void WyzerujMacierzWPB(int iWzorca)
        {
            for (int iWarstwy = 0; iWarstwy < MacierzWPB[iWzorca].Count; iWarstwy++)
            {
                for (int iNeurona = 0; iNeurona < MacierzWPB[iWzorca][iWarstwy].Count; iNeurona++)
                {
                    MacierzWPB[iWzorca][iWarstwy][iNeurona] = 0;
                }
            }
        }

        public void WybierzFunkcjeAktywacji(System.Windows.Forms.ComboBox vFunkcje)
        {
            vFunkcje.BackColor = System.Drawing.Color.White;

            string nazwaFunkcji = vFunkcje.Text;

            if (nazwaFunkcji == null || nazwaFunkcji == "")
            {
                vFunkcje.BackColor = System.Drawing.Color.Red;
                return;
            }

            this.WybierzFunkcjeAktywacji(nazwaFunkcji);
        }

        public void WybierzFunkcjeAktywacji(string nazwaFunkcji)
        {
            F_Name = nazwaFunkcji;
            switch (nazwaFunkcji)
            {
                case Global.Signum:
                    F = new WywaloajFunkcje(ActivationFunction.Signum);
                    Fprim = new WywaloajFunkcjePrim(ActivationFunction.SignumPrim);
                    break;
                case Global.Sigmoidalna_Unipolarna:
                    F = new WywaloajFunkcje(ActivationFunction.SigmoidUnipolar);
                    Fprim = new WywaloajFunkcjePrim(ActivationFunction.SigmoidUnipolPrim);
                    break;
                case Global.Sigmoidalna_Bipolarna:
                    F = new WywaloajFunkcje(ActivationFunction.SigmoidBipol);
                    Fprim = new WywaloajFunkcjePrim(ActivationFunction.SigmoidBipolarPrim);
                    break;
                case Global.Liniowa:
                    F = new WywaloajFunkcje(ActivationFunction.Linear);
                    Fprim = new WywaloajFunkcjePrim(ActivationFunction.LinearPrim);
                    break;
            }
        }

        // ===================================== Obliczenia na Macierzy =====================================

        public void WyliczWyjscie(int indexWzorca)
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

                    if (WarstwaWyjsciowa) MacierzWyjsc[indexWzorca][iNeurona] = F(bufor);//ActivationFunction.Linear(bufor);
                    else MacierzWejsc[iWarstwy + 1][iNeurona] = F(bufor);
                }
            }
        }

        public void UstawMacierzJakoWarswaWejsiowa(List<double> wzorzec)
        {
            for (int i = 0; i < wzorzec.Count; i++) MacierzWejsc[0][i] = wzorzec[i];
        }

        public void WyliczBlad(int indexWzorca)
        {
            WyzerujMacierzBledow(indexWzorca);
            WyzerujMacierzWPB(indexWzorca);

            WyliczBladNaWyjsciu(indexWzorca);
            WyliczMacierzeWPB_DlaWarstwyPoprzedniej(indexWzorca);
            WypelnijMacierzBledow(indexWzorca);
        }

        public void WyliczMacierzeWPB_DlaWarstwyPoprzedniej(int indexWzorca)
        {
            for (int iWarstwy = MacierzWPB[indexWzorca].Count - 1; iWarstwy >= 0; iWarstwy--)
            {
                for (int iNeurona = 0; iNeurona < MacierzWPB[indexWzorca][iWarstwy].Count; iNeurona++)
                {
                    for (int iNeuronaWarstwyNastepnej = 0; iNeuronaWarstwyNastepnej < MacierzWag[iWarstwy + 1].Count; iNeuronaWarstwyNastepnej++)
                    {
                        MacierzWPB[indexWzorca][iWarstwy][iNeurona] +=
                            MacierzBledow[indexWzorca][iWarstwy + 1][iNeuronaWarstwyNastepnej] *
                                    MacierzWag[iWarstwy + 1][iNeuronaWarstwyNastepnej][iNeurona];
                    }
                }
            }
        }

        public void WyliczBladNaWyjsciu(int indexWzorca)
        {
            int iWarstwy = MacierzBledow[indexWzorca].Count - 1;

            for (int iNeurona = 0; iNeurona < MacierzBledow[indexWzorca][iWarstwy].Count; iNeurona++)
            {
                MacierzBledow[indexWzorca][iWarstwy][iNeurona] = WzorceWyjscia[indexWzorca][iNeurona] - MacierzWyjsc[indexWzorca][iNeurona];
            }
        }

        public void WypelnijMacierzBledow(int indexWzorca)
        {
            for (int iWarstwy = 0; iWarstwy < MacierzWPB[indexWzorca].Count; iWarstwy++)
            {
                for (int iNeurona = 0; iNeurona < MacierzWPB[indexWzorca][iWarstwy].Count; iNeurona++)
                {
                    MacierzBledow[indexWzorca][iWarstwy][iNeurona] = MacierzWPB[indexWzorca][iWarstwy][iNeurona];
                }
            }
        }

        public void Koryguj(int indexWzorca)
        {
            KorektaWag(indexWzorca);
            KorektaBiasow(indexWzorca);
        }

        public void KorektaWag(int indexWzorca)
        {
            for (int iWarstwy = 0; iWarstwy < MacierzWag.Count; iWarstwy++)
            {
                for (int iNeurona = 0; iNeurona < MacierzWag[iWarstwy].Count; iNeurona++)
                {
                    for (int iPerceptronu = 0; iPerceptronu < MacierzWag[iWarstwy][iNeurona].Count; iPerceptronu++)
                    {
                        if (iWarstwy < MacierzWag.Count - 1)
                            MacierzWag[iWarstwy][iNeurona][iPerceptronu] += Global.NI * MacierzBledow[indexWzorca][iWarstwy][iNeurona] * MacierzWejsc[iWarstwy][iPerceptronu] * Fprim(MacierzWejsc[iWarstwy + 1][iNeurona]);
                        else
                            MacierzWag[iWarstwy][iNeurona][iPerceptronu] += Global.NI * MacierzBledow[indexWzorca][iWarstwy][iNeurona] * MacierzWejsc[iWarstwy][iPerceptronu] * Fprim(MacierzWyjsc[indexWzorca][iNeurona]);
                    }
                }
            }
        }

        public void KorektaBiasow(int indexWzorca)
        {
            for (int iWarstwy = 0; iWarstwy < MacierzBiasow.Count; iWarstwy++)
            {
                for (int iNeurona = 0; iNeurona < MacierzBiasow[iWarstwy].Count; iNeurona++)
                {
                    MacierzBiasow[iWarstwy][iNeurona] += Global.NI * MacierzBledow[indexWzorca][iWarstwy][iNeurona];
                }
            }
        }

        // ===================================== SIEC =====================================

        public async Task UczSiec_BackPropagation()
        {
            DateTime startTime = DateTime.Now;
            TimeSpan learningTime = new TimeSpan();

            List<List<double>> Bufor_AnswerNN = new List<List<double>>();
            //List<List<double>> Bufor_ErrorsNN = new List<List<double>>();
            BuforSrednigoBleduAbsolutnego = new List<double>();

            int Epoka = 0;
            this.StartCzasomierz();
            await Task.Run(async () =>
            {
                do
                {
                    int indexWzorca = 0;
                    foreach (List<double> wzorzezWejscia in WzorceWejscia)
                    {
                        UstawMacierzJakoWarswaWejsiowa(wzorzezWejscia);
                        WyliczWyjscie(indexWzorca);
                        WyliczBlad(indexWzorca);
                        Koryguj(indexWzorca);
                        indexWzorca++;
                    }

                    double sredniBladAbsolutny = WyliczSredniBladAbsulutny();
                    BuforSrednigoBleduAbsolutnego.Add(sredniBladAbsolutny);
                    double maksymalnyBlad = WyliczMaxWartoscBezwzgledna(ZwrucMacierzBledowWyjsciowych()) * dzielnikWyjscia;
                    if (FlagCzasAkcji)
                    {
                        Console.WriteLine("INTERRUPT TIME on lap :" + Epoka.ToString());
                        if (FlagOdswiezanieZakonczone)
                        {
                            //Bufor_AnswerNN = WyliczOdpowiedz(WzorceWejscioweTestow);
                            learningTime = startTime - DateTime.Now;
                            //fillLabelWithInformationAboutLerningProgres("Czas uczenia [ " + learningTime.ToString(@"mm' : 'ss\.ff") + " ], aktualna epoka: " + Epoka + ", aktualny średni błąd absolutny: " + Math.Round(sredniBladAbsolutny, 5).ToString() + ", aktualny maksymalny błąd: " + Math.Round(maksymalnyBlad, 5).ToString());
                            Console.WriteLine("   START ACTION");
                            FlagOdswiezanieZakonczone = false;
                            FlagCzasAkcji = !FlagCzasAkcji;
                            RefreshSeriesBlad(BuforSrednigoBleduAbsolutnego);
                        }
                    }

                    //if (SpelnionyWarunekKoncaSrednigoBleduAbsolutnego((sredniBladAbsolutny)) && SpelnionyWarunekKoncaMaxymalnegoBledu(maksymalnyBlad)) break;

                } while (++Epoka < Global.MaxEpok);
            });
            this.StopCzasomierz();

            if (WzorceWejscioweTestow == null) return;
            Bufor_AnswerNN = WyliczOdpowiedz(WzorceWejscioweTestow);
            await RefreshSeriesBlad(BuforSrednigoBleduAbsolutnego);
            learningTime = startTime - DateTime.Now;
            if (dzielnikWyjscia == 0) dzielnikWyjscia = 1;
            double FinalError = WyliczSredniBladAbsulutny() / dzielnikWyjscia;
            double FinalMaxError = WyliczMaxWartoscBezwzgledna(ZwrucMacierzBledowWyjsciowych()) * dzielnikWyjscia;
            fillLabelWithInformationAboutLerningProgres("Całkowity czas uczenia [ " + learningTime.ToString(@"mm' : 'ss\.ff") + "], ilość epok: " + Epoka + ", średni błąd absolutny: " + Math.Round(FinalError, 5).ToString() + ", maksymalny błąd: " + Math.Round(FinalMaxError, 5).ToString());
        }

        public void Koryguj_Hebian(int indexWzorca)
        {
            KorektaWag_Hebian(indexWzorca);
            KorektaBiasow_Hebian(indexWzorca);
        }
        public void KorektaWag_Hebian(int indexWzorca)
        {
            for (int iWarstwy = 0; iWarstwy < MacierzWag.Count; iWarstwy++)
            {
                for (int iNeurona = 0; iNeurona < MacierzWag[iWarstwy].Count; iNeurona++)
                {
                    for (int iPerceptronu = 0; iPerceptronu < MacierzWag[iWarstwy][iNeurona].Count; iPerceptronu++)
                    {
                        if (iWarstwy < MacierzWag.Count - 1)
                            MacierzWag[iWarstwy][iNeurona][iPerceptronu] += Global.NI * MacierzWejsc[iWarstwy][iPerceptronu] * MacierzWejsc[iWarstwy + 1][iNeurona];
                        else
                            MacierzWag[iWarstwy][iNeurona][iPerceptronu] += Global.NI * MacierzWejsc[iWarstwy][iPerceptronu] * WzorceWyjscia[indexWzorca][iNeurona];
                    }
                }
            }
        }
        public void KorektaBiasow_Hebian(int indexWzorca)
        {
            for (int iWarstwy = 0; iWarstwy < MacierzBiasow.Count; iWarstwy++)
            {
                for (int iNeurona = 0; iNeurona < MacierzBiasow[iWarstwy].Count; iNeurona++)
                {
                    if (iWarstwy < MacierzWag.Count - 1)
                        MacierzBiasow[iWarstwy][iNeurona] += Global.NI * MacierzBiasow[iWarstwy][iNeurona] * MacierzWejsc[iWarstwy + 1][iNeurona];
                    else
                        MacierzBiasow[iWarstwy][iNeurona] += Global.NI * MacierzBiasow[iWarstwy][iNeurona] * WzorceWyjscia[indexWzorca][iNeurona];
                }
            }
        }
        public async Task UczSiec_Hebian()
        {
            DateTime startTime = DateTime.Now;
            TimeSpan learningTime = new TimeSpan();

            List<List<double>> Bufor_AnswerNN = new List<List<double>>();
            //List<List<double>> Bufor_ErrorsNN = new List<List<double>>();
            BuforSrednigoBleduAbsolutnego = new List<double>();

            int Epoka = 0;
            this.StartCzasomierz();
            await Task.Run(async () =>
            {
                do
                {
                    int indexWzorca = 0;
                    foreach (List<double> wzorzezWejscia in WzorceWejscia)
                    {
                        UstawMacierzJakoWarswaWejsiowa(wzorzezWejscia);
                        Koryguj_Hebian(indexWzorca);
                        indexWzorca++;
                    }

                    double sredniBladAbsolutny = WyliczSredniBladAbsulutny();
                    BuforSrednigoBleduAbsolutnego.Add(sredniBladAbsolutny);
                    double maksymalnyBlad = WyliczMaxWartoscBezwzgledna(ZwrucMacierzBledowWyjsciowych()) * dzielnikWyjscia;
                    if (FlagCzasAkcji)
                    {
                        Console.WriteLine("INTERRUPT TIME on lap :" + Epoka.ToString());
                        if (FlagOdswiezanieZakonczone)
                        {
                            Bufor_AnswerNN = WyliczOdpowiedz(WzorceWejscioweTestow);
                            learningTime = startTime - DateTime.Now;
                            fillLabelWithInformationAboutLerningProgres("Czas uczenia [ " + learningTime.ToString(@"mm' : 'ss\.ff") + " ], aktualna epoka: " + Epoka + ", aktualny średni błąd absolutny: " + Math.Round(sredniBladAbsolutny, 5).ToString() + ", aktualny maksymalny błąd: " + Math.Round(maksymalnyBlad, 5).ToString());
                            Console.WriteLine("   START ACTION");
                            FlagOdswiezanieZakonczone = false;
                            FlagCzasAkcji = !FlagCzasAkcji;
                            RefreshSeriesBlad(BuforSrednigoBleduAbsolutnego);
                        }
                    }

                    //if (SpelnionyWarunekKoncaSrednigoBleduAbsolutnego((sredniBladAbsolutny)) && SpelnionyWarunekKoncaMaxymalnegoBledu(maksymalnyBlad)) break;

                } while (++Epoka < Global.MaxEpok);
            });
            this.StopCzasomierz();

            if (WzorceWejscioweTestow == null) return;
            Bufor_AnswerNN = WyliczOdpowiedz(WzorceWejscioweTestow);
            await RefreshSeriesBlad(BuforSrednigoBleduAbsolutnego);
            learningTime = startTime - DateTime.Now;
            for (int i = 0; i < WzorceWejscia.Count; i++) WyliczWyjscie(i);
            if (dzielnikWyjscia == 0) dzielnikWyjscia = 1;
            double FinalError = WyliczSredniBladAbsulutny() / dzielnikWyjscia;
            //double FinalMaxError = WyliczMaxWartoscBezwzgledna(ZwrucMacierzBledowWyjsciowych()) * dzielnikWyjscia;
            fillLabelWithInformationAboutLerningProgres("Całkowity czas uczenia [ " + learningTime.ToString(@"mm' : 'ss\.ff") + "], ilość epok: " + Epoka + ", średni błąd absolutny: " + Math.Round(FinalError, 5).ToString());
        }
        private double WyliczSredniBladAbsulutny()
        {
            double SumErrors = 0;
            int counter = 0;

            for (int i = 0; i < MacierzWyjsc.Count; i++)
            {
                //Console.WriteLine($"Tamplate {i}");
                for (int j = 0; j < MacierzWyjsc[i].Count; j++)
                {
                    SumErrors += Math.Abs(WzorceWyjscia[i][j] - MacierzWyjsc[i][j]);
                    Console.WriteLine($"{SumErrors} += {WzorceWyjscia[i][j]} - {MacierzWyjsc[i][j]}");
                    counter++;
                }
            }
            SumErrors /= counter;
            return SumErrors * dzielnikWyjscia;
        }

        private List<List<double>> ZwrucMacierzBledowWyjsciowych()
        {
            List<List<double>> bufor = new List<List<double>>();
            int iWarstwy = MacierzLiczbNeuronow.Count - 2;

            for (int iWzorca = 0; iWzorca < WzorceWejscia.Count; iWzorca++)
            {
                List<double> buforWzorca = new List<double>(0);

                for (int iNeurona = 0; iNeurona < MacierzLiczbNeuronow[MacierzLiczbNeuronow.Count - 1]; iNeurona++)
                {
                    buforWzorca.Add(MacierzBledow[iWzorca][iWarstwy][iNeurona]);
                }

                bufor.Add(buforWzorca);
            }
            return bufor;
        }
        public List<List<double>> WyliczOdpowiedz(List<List<double>> DaneWejsciowe)
        {
            if (DaneWejsciowe[0].Count != MacierzWejsc[0].Count) return null;

            List<List<double>> DaneWejscioweZnormalizowane = NormalizujWzorceWejscia(DaneWejsciowe);

            List<List<double>> buforMacierzyWyjscia = new List<List<double>>();
            List<double> buforWektoraWyjscia;

            int i = 0;

            foreach (List<double> wzorzec in DaneWejscioweZnormalizowane)
            {
                UstawMacierzJakoWarswaWejsiowa(wzorzec);
                WyliczWyjscie(i);

                buforWektoraWyjscia = new List<double>();
                foreach (double bufor in MacierzWyjsc[i]) buforWektoraWyjscia.Add(bufor * dzielnikWyjscia);

                buforMacierzyWyjscia.Add(buforWektoraWyjscia);
            }

            return buforMacierzyWyjscia;
        }
        private async Task RefreshSeriesBlad(List<double> bufor)
        {
            await Task.Run(() => {
                try
                {
                    drawChartFromLernResultOfNN(bufor.ToList());
                    FlagOdswiezanieZakonczone = true;
                }
                catch (InvalidOperationException e)
                {
                    Console.WriteLine("      ERROR...1");
                    FlagOdswiezanieZakonczone = true;
                }
                catch (OverflowException e)
                {
                    Console.WriteLine("      ERROR...2");
                    FlagOdswiezanieZakonczone = true;
                }
                catch (Exception)
                {
                    Console.WriteLine("      ERROR...?");
                }
                Console.WriteLine("      RENDERING -- END ---");
            });
        }

        // ===================================== TESTY =====================================

        public void ShowMatrixCountig(List<List<List<double>>> Data, string Nazwa)
        {
            Console.WriteLine("Macierz " + Nazwa);
            string Format = String.Format("F3");

            for (int iWarstwy = 0; iWarstwy < Data.Count; iWarstwy++)
            {
                Console.WriteLine("    DATA[ " + iWarstwy + " ] count : " + Data[iWarstwy].Count);

                for (int iNeuronu = 0; iNeuronu < Data[iWarstwy].Count; iNeuronu++)
                {
                    Console.WriteLine("        DATA[ " + iWarstwy + " ][ " + iNeuronu +
                                      " ] count : " + Data[iWarstwy][iNeuronu].Count);

                    for (int iPerceptronu = 0; iPerceptronu < Data[iWarstwy][iNeuronu].Count; iPerceptronu++)
                    {
                        Console.WriteLine("            DATA[ " + iWarstwy + " ][ " + iNeuronu + " ][ " + iPerceptronu +
                                          " ] count : " + Data[iWarstwy][iNeuronu][iPerceptronu].ToString(Format));
                    }
                }
            }
        }

        public void ShowMatrixCountig(List<List<double>> Data, string Nazwa)
        {
            Console.WriteLine("Macierz " + Nazwa);
            string Format = String.Format("F3");

            for (int iWarstwy = 0; iWarstwy < Data.Count; iWarstwy++)
            {
                Console.WriteLine("    DATA[ " + iWarstwy + " ] count : " + Data[iWarstwy].Count.ToString());

                for (int iNeuronu = 0; iNeuronu < Data[iWarstwy].Count; iNeuronu++)
                {
                    Console.WriteLine("        DATA[ " + iWarstwy + " ][ " + iNeuronu +
                                      " ] : " + Data[iWarstwy][iNeuronu].ToString(Format));
                }
            }
        }

        public void ShowMatrixCountigOnOutput(List<List<List<double>>> Data, string Nazwa)
        {
            Console.WriteLine("Macierz " + Nazwa);
            string Format = String.Format("F3");

            int iWarstwy = Data[0].Count - 1;

            for (int iWzorca = 0; iWzorca < Data.Count; iWzorca++)
            {
                Console.WriteLine("        DATA[ " + iWzorca + " ][ " + iWarstwy + " ] count : " + Data[iWzorca][iWarstwy].Count);

                for (int iNeuronu = 0; iNeuronu < Data[iWzorca][iWarstwy].Count; iNeuronu++)
                {
                    Console.WriteLine("            DATA[ " + iWzorca + " ][ " + iWarstwy + " ][ " + iNeuronu + " ] = " + Data[iWzorca][iWarstwy][iNeuronu].ToString(Format));
                }
            }
        }

        public void ShowLog(string Bufor)
        {
            Console.WriteLine("Log " + Bufor);
        }

        public List<int> getMacierzLiczbNeuronow()
        {
            return MacierzLiczbNeuronow;
        }

        public List<List<double>> getWzorceWejscia()
        {
            return WzorceWejscia;
        }

        public List<List<double>> getWzorceWyjscia()
        {
            return WzorceWyjscia;
        }

        public List<List<double>> getMacierzWejsc()
        {
            return MacierzWejsc;
        }

        public List<List<double>> getMacierzWyjsc()
        {
            return MacierzWyjsc;
        }

        public List<List<List<double>>> getMacierzWag()
        {
            return MacierzWag;
        }

        public List<List<double>> getMacierzBiasow()
        {
            return MacierzBiasow;
        }

        public List<List<List<double>>> getMacierzBledow()
        {
            return MacierzBledow;
        }

        public List<List<List<double>>> getMacierzWPB()
        {
            return MacierzWPB;
        }

        public void setMacierzBiasow(List<List<double>> l)
        {
            MacierzBiasow = l;
        }

        public void setMacierzWag(List<List<List<double>>> l)
        {
            MacierzWag = l;
        }

        /*###*/// ShowMatrixCountig(MacierzWejsc, "MacierzWejsc");
        /*###*/// ShowMatrixCountig(MacierzWag, "MacierzWag");
        /*###*/// ShowMatrixCountig(MacierzBiasow, "MacierzBiasow");
        /*###*/// ShowMatrixCountig(MacierzWyjsc, "MacierzWyjsc");
        /*###*/// ShowMatrixCountigOnOutput(MacierzBledow, "MacierzBledow");
        /*###*/// ShowMatrixCountig(MacierzWPB, "MacierzWPB");
        /*###*/// ShowMatrixCountig(WzorceWyjscia, "WzorceWyjscia");
    
        public string ExportDataToCSV()
        {
            string Bufor = String.Empty;

            Bufor += $"{F_Name}";
            Bufor += $"{System.Environment.NewLine}";
            foreach (int value in MacierzLiczbNeuronow)
            {
                Bufor += $"{value}; ";
            }
            Bufor = Bufor.Remove(Bufor.Length - 2);
            Bufor += $"{System.Environment.NewLine}";
            foreach (List<double> List_value in MacierzBiasow)
            {
                foreach (double value in List_value)
                {
                    Bufor += $"{value}; ";
                }
                Bufor = Bufor.Remove(Bufor.Length - 2);
                Bufor += $"{System.Environment.NewLine}";
            }
            foreach (List<List<double>> List_List_value in MacierzWag)
            {
                foreach (List<double> List_value in List_List_value)
                {
                    foreach (double value in List_value)
                    {
                        Bufor += $"{value}; ";
                    }
                    Bufor = Bufor.Remove(Bufor.Length - 2);
                    Bufor += $"{System.Environment.NewLine}";
                }
            }
            return Bufor;
        }
    }
}
