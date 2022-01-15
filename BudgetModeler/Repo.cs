using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetModeler
{
    class Repo
    {        
        //INPUT
        public const string Food = "Food";
        public const string Home = "Home";
        public const string Health = "Health";
        public const string Family = "Family";
        public const string Car = "Car";
        public const string Entertainment = "Entertainment";
        public const string Stimulant = "Stimulant";
        public const string Investment_S = "Investment S";
        public const string Investment_R = "Investment R";
        public const string Month_income = "Month income";
        public const string Budget = "Budget";
        //OUTPUT
        public const string Calculation = "Calculation";

        public const double Month_income_Value = 10000.0D;
        public const double Budget_Value = 200000.0D;

        List<BudgetData> ListBudgetData;
        BudgetData Bufor_BudgetData;

        List<int> NumberOfNeurons;
        string ActivationF;
        List<List<double>> InputTemplates;
        List<List<double>> OutputTemplates;
        NeuralNetwork neuralNetwork;
        NeuralNetwork.DrawChartFromLernResultOfNN drawchart_BP;
        List<double> error_Matrix;

        internal BudgetData BuforBudgetData { get => Bufor_BudgetData; set => Bufor_BudgetData = value; }
        public List<double> ErrorMatrix { get => error_Matrix; set => error_Matrix = value; }

        public Repo()
        {
            ListBudgetData = new List<BudgetData>();
            _ = ResetBuforBudgetData();
        }

        public bool UpdateInputValue(string Input_Name, double Input_Value)
        {
            try
            {
                Bufor_BudgetData.UpdateInputValue(Input_Name, Input_Value/100);
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public bool UpdateOutputValue(string Input_Name, double Input_Value)
        {
            try
            {
                Bufor_BudgetData.UpdateOutputValue(Input_Name, Input_Value / 100);
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public bool ResetBuforBudgetData()
        {
            Bufor_BudgetData = new BudgetData();
            Bufor_BudgetData.AddInput(Food, 0.2D);
            Bufor_BudgetData.AddInput(Home, 0.2D);
            Bufor_BudgetData.AddInput(Health, 0);
            Bufor_BudgetData.AddInput(Family, 0);
            Bufor_BudgetData.AddInput(Car, 0.1D);
            Bufor_BudgetData.AddInput(Entertainment, 0);
            Bufor_BudgetData.AddInput(Stimulant, 0);
            Bufor_BudgetData.AddInput(Investment_S, 0);
            Bufor_BudgetData.AddInput(Investment_R, 0);
            Bufor_BudgetData.AddInput(Month_income, 0.2D);
            Bufor_BudgetData.AddInput(Budget, 0.05D);

            Bufor_BudgetData.AddOutput(Calculation, 0.8D);
            return true;
        }

        public bool AddBuforToList()
        {
            if (Bufor_BudgetData is null) return false;

            try
            {
                ListBudgetData.Add(Bufor_BudgetData.GetCopy());
                //ResetBuforBudgetData();
            }
            catch (Exception)
            {
                return false;
            }         
            return true;
        }

        public List<string[]> GetDataToList()
        {
            List<string[]> MatrixData = new List<string[]>();

            int index = 1;
            foreach (BudgetData Data in ListBudgetData)
            {
                string[] RowData = {index.ToString(),           Data.GetInputValueOf(Food).ToString(),          Data.GetInputValueOf(Home).ToString(),
                Data.GetInputValueOf(Health).ToString(),        Data.GetInputValueOf(Family).ToString(),        Data.GetInputValueOf(Car).ToString(),
                Data.GetInputValueOf(Entertainment).ToString(), Data.GetInputValueOf(Stimulant).ToString(),     Data.GetInputValueOf(Investment_S).ToString(),
                Data.GetInputValueOf(Investment_R).ToString(),  Data.GetInputValueOf(Month_income).ToString(),  Data.GetInputValueOf(Budget).ToString(),
                Data.GetOutputValueOf(Calculation).ToString()};
                
                MatrixData.Add(RowData);
            }

            return MatrixData;
        }

        public void SetDataToList(List<double[]> MatrixData)
        {
            foreach (double[] RowData in MatrixData)
            {
                BudgetData Bufor = new BudgetData();
                Bufor.AddInput(Food, RowData[1]);
                Bufor.AddInput(Home, RowData[2]);
                Bufor.AddInput(Health, RowData[3]);
                Bufor.AddInput(Family, RowData[4]);
                Bufor.AddInput(Car, RowData[5]);
                Bufor.AddInput(Entertainment, RowData[6]);
                Bufor.AddInput(Stimulant, RowData[7]);
                Bufor.AddInput(Investment_S, RowData[8]);
                Bufor.AddInput(Investment_R, RowData[9]);
                Bufor.AddInput(Month_income, RowData[10]);
                Bufor.AddInput(Budget, RowData[11]);
                Bufor.AddOutput(Calculation, RowData[12]);
                ListBudgetData.Add(Bufor);
            }
        }

        public void SetFuncion(string activation)
        {
            ActivationF = activation;
        }

        public void SetNumberOfNeuronsInLayer(List<int> NoNs)
        {
            NumberOfNeurons = NoNs;
        }

        public async Task LearnNNAsync()
        {
            GenerateLearningTeplates();

            neuralNetwork = new NeuralNetwork(InputTemplates, OutputTemplates, NumberOfNeurons, ActivationF);
            if (drawchart_BP != null)
            {
                neuralNetwork.SetDelegatDrawChartFromErrorOfNN(drawchart_BP);
            }
            //if (FlagTestAutomatic)
            //{
            //    drawchart = DynamicAutoTestNN1;
            //    neuralNetwork.SetDelegatDrawChartFromLernResultOfNN(drawchart, PrzygotujMacierzWzorcowWejsciowych(), PrzygotujMacierzWzorcowWyjsciowych());
            //}
            //else neuralNetwork.ResetDelegatDrawChartFromLernResultOfNN();

            await Task.Run(async () =>
            {
                await neuralNetwork.UczSiec_BackPropagation();
            });
        }

        public void GenerateLearningTeplates()
        {
            InputTemplates = new List<List<double>>();
            foreach (BudgetData Data in ListBudgetData)
            {
                double[] RowData = {                Data.GetInputValueOf(Food),          Data.GetInputValueOf(Home),
                Data.GetInputValueOf(Health),        Data.GetInputValueOf(Family),        Data.GetInputValueOf(Car),
                Data.GetInputValueOf(Entertainment), Data.GetInputValueOf(Stimulant),     Data.GetInputValueOf(Investment_S),
                Data.GetInputValueOf(Investment_R),  Data.GetInputValueOf(Month_income),  Data.GetInputValueOf(Budget)};

                InputTemplates.Add(RowData.ToList());
            }

            OutputTemplates = new List<List<double>>();
            foreach (BudgetData Data in ListBudgetData)
            {
                double[] RowData = {Data.GetOutputValueOf(Calculation)};

                OutputTemplates.Add(RowData.ToList());
            }
        }

        public void SetChartDelegat(NeuralNetwork.DrawChartFromLernResultOfNN drawchart_BP )
        {
            this.drawchart_BP = drawchart_BP;
        }

        public string GetExportNNToCSV()
        {
            return neuralNetwork.ExportDataToCSV();
        }
    }
}
