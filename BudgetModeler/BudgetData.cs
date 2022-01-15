using System.Collections.Generic;

namespace BudgetModeler
{
    class BudgetData
    {
        Dictionary<string, double> NormalizedInput;
        Dictionary<string, double> NormalizedOutput;
        public BudgetData()
        {
            NormalizedInput = new Dictionary<string, double>();
            NormalizedOutput = new Dictionary<string, double>();
        }

        public void AddInput(string Input_Name, double Input_Value)
        {
            NormalizedInput.Add(Input_Name, Input_Value);
        }
        public double GetInputValueOf(string Output_Name)
        {
            if (NormalizedInput.ContainsKey(Output_Name))
            {
                return NormalizedInput[Output_Name];
            }
            return 0.0D;
        }
        public List<double> GetListOfInput()
        {
            List<double> InputListData = new List<double>();
            foreach (KeyValuePair<string, double> Input in NormalizedInput)
            {
                InputListData.Add(Input.Value);
            }
            return InputListData;
        }
        public void UpdateInputValue(string Input_Name, double newInput_Value)
        {
            if (NormalizedInput.ContainsKey(Input_Name))
            {
                NormalizedInput[Input_Name] = newInput_Value;
            }
        }        
        
        public void AddOutput(string Output_Name, double Output_Value)
        {
            NormalizedOutput.Add(Output_Name, Output_Value);
        }
        public double GetOutputValueOf(string Output_Name)
        {
            if (NormalizedOutput.ContainsKey(Output_Name))
            {
                return NormalizedOutput[Output_Name];
            }
            return 0.0D;
        }
        public List<double> GetListOfOutput()
        {
            List<double> OutputListData = new List<double>();
            foreach (KeyValuePair<string, double> Input in NormalizedOutput)
            {
                OutputListData.Add(Input.Value);
            }
            return OutputListData;
        }
        public void UpdateOutputValue(string Output_Name, double newOutput_Value)
        {
            if (NormalizedOutput.ContainsKey(Output_Name))
            {
                NormalizedOutput[Output_Name] = newOutput_Value;
            }
        }

        public BudgetData GetCopy()
        {
            BudgetData BudgetDataBufor = new BudgetData();
            foreach (KeyValuePair<string,double> v in NormalizedInput)
            {
                BudgetDataBufor.AddInput(v.Key,v.Value);
            }
            //(NormalizedInput.Keys, 0.2D);
            //BudgetDataBufor.AddInput(Home, 0.2D);
            //BudgetDataBufor.AddInput(Health, 0);
            //BudgetDataBufor.AddInput(Family, 0);
            //BudgetDataBufor.AddInput(Car, 0.1D);
            //BudgetDataBufor.AddInput(Entertainment, 0);
            //BudgetDataBufor.AddInput(Stimulant, 0);
            //BudgetDataBufor.AddInput(Investment_S, 0);
            //BudgetDataBufor.AddInput(Investment_R, 0);
            //BudgetDataBufor.AddInput(Month_income, 0.2D);
            //BudgetDataBufor.AddInput(Budget, 0.05D);
            foreach (KeyValuePair<string, double> v in NormalizedOutput)
            {
                BudgetDataBufor.AddOutput(v.Key, v.Value);
            }
            // BudgetDataBufor.AddOutput(Calculation, 0.8D);
            
            return BudgetDataBufor;
        }
    }
}
