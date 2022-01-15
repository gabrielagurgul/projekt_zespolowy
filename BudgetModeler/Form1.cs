using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace BudgetModeler
{
    public partial class Form1 : Form
    {
        Repo Repository;

        List<TabPages_LayerDetail> Layers;

        NeuralNetwork.DrawChartFromLernResultOfNN drawchart_BP;
        delegate void DelegateChart(string arg);
        DelegateChart delegateChart_BP;
        const string Error = "Error";

        bool IsAutoFillForm;
        public Form1()
        {
            InitializeComponent();
        }

        TabPages_LayerDetail.DrawNewNNView DrawNewNNView;
        private void Form1_Load(object sender, EventArgs e)
        {
            IsAutoFillForm = true;
            Repository = new Repo();
            LoadData();
            List<string[]> MatrixData = Repository.GetDataToList();
            FillListView(MatrixData);
            Repository.ResetBuforBudgetData();
            ResetControlData();
            Fill_TB_Summary();
            IsAutoFillForm = false;

            delegateChart_BP = new DelegateChart(RefreshNN_BP);
            drawchart_BP = DynamicAutoTestNN_BP;

            this.chartSignal1.AddSeries(Error, Color.DarkRed);

            InitializeCustomComponent();
        }
        private void InitializeCustomComponent()
        {
            this.CB_Funcion.Items.Add(Global.Signum);
            this.CB_Funcion.Items.Add(Global.Sigmoidalna_Unipolarna);
            this.CB_Funcion.Items.Add(Global.Sigmoidalna_Bipolarna);
            this.CB_Funcion.Items.Add(Global.Liniowa);
            this.CB_Funcion.SelectedIndex = 2;

            Layers = new List<TabPages_LayerDetail>();
            Layers.Add(new TabPages_LayerDetail());
            this.TC_Layers.TabPages.Add(Layers[0]);
            DrawNewNNView = DrawNeuralNetworkView;
            TabPages_LayerDetail.SetDelegateRedrawNNView(DrawNewNNView);
        }

        private List<double[]> LoadData()
        {
            string[] fileLines;
            bool iSFile = false;
            try
            {
                string path = @"Data.csv";
                fileLines = File.ReadAllLines(path);
                iSFile = true;
            }
            catch (Exception)
            {
                fileLines = null;
            }
            if (!iSFile) return null;

            List<double[]> ValidData = new List<double[]>();

            foreach (string singleLine in fileLines)
            {
                string[] tablica = singleLine.Split(';');
                List<double> ConvertedRecord = new List<double>();
                foreach (string t in tablica)
                {
                    try
                    {
                        ConvertedRecord.Add(Double.Parse(t));
                    }
                    catch (Exception)
                    {
                        ConvertedRecord.Add(0);
                    }
                }
                if(ConvertedRecord.Count > 1 ) ValidData.Add(ConvertedRecord.ToArray());
            }

            if (ValidData.Count > 1 ) Repository.SetDataToList(ValidData);
            return null;
        }

        private void NUD_1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (IsAutoFillForm) return;
                IsAutoFillForm = true;
                Repository.UpdateInputValue(Repo.Food, Double.Parse(NUD_1.Value.ToString()));
                TB_1.Text = Math.Round(Repo.Month_income_Value * Repository.BuforBudgetData.GetInputValueOf(Repo.Month_income) * Repository.BuforBudgetData.GetInputValueOf(Repo.Food), 2).ToString();
                Fill_TB_Summary();
            }
            catch (Exception)
            {
                MessageBox.Show("Error with update value");
            }
            finally
            {
                IsAutoFillForm = false;
            }
        }
        private void NUD_2_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (IsAutoFillForm) return;
                IsAutoFillForm = true;
                Repository.UpdateInputValue(Repo.Home, Double.Parse(NUD_2.Value.ToString()));
                TB_2.Text = Math.Round(Repo.Month_income_Value * Repository.BuforBudgetData.GetInputValueOf(Repo.Month_income) * Repository.BuforBudgetData.GetInputValueOf(Repo.Home), 2).ToString();
                Fill_TB_Summary();
            }
            catch (Exception)
            {
                MessageBox.Show("Error with update value");
            }
            finally
            {
                IsAutoFillForm = false;
            }
        }
        private void NUD_3_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (IsAutoFillForm) return;
                IsAutoFillForm = true;
                Repository.UpdateInputValue(Repo.Health, Double.Parse(NUD_3.Value.ToString()));
                TB_3.Text = Math.Round(Repo.Month_income_Value * Repository.BuforBudgetData.GetInputValueOf(Repo.Month_income) * Repository.BuforBudgetData.GetInputValueOf(Repo.Health), 2).ToString();
                Fill_TB_Summary();
            }
            catch (Exception)
            {
                MessageBox.Show("Error with update value");
            }
            finally
            {
                IsAutoFillForm = false;
            }
        }
        private void NUD_4_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (IsAutoFillForm) return;
                IsAutoFillForm = true;
                Repository.UpdateInputValue(Repo.Family, Double.Parse(NUD_4.Value.ToString()));
                TB_4.Text = Math.Round(Repo.Month_income_Value * Repository.BuforBudgetData.GetInputValueOf(Repo.Month_income) * Repository.BuforBudgetData.GetInputValueOf(Repo.Family), 2).ToString();
                Fill_TB_Summary();
            }
            catch (Exception)
            {
                MessageBox.Show("Error with update value");
            }
            finally
            {
                IsAutoFillForm = false;
            }
        }
        private void NUD_5_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (IsAutoFillForm) return;
                IsAutoFillForm = true;
                Repository.UpdateInputValue(Repo.Car, Double.Parse(NUD_5.Value.ToString()));
                TB_5.Text = Math.Round(Repo.Month_income_Value * Repository.BuforBudgetData.GetInputValueOf(Repo.Month_income) * Repository.BuforBudgetData.GetInputValueOf(Repo.Car), 2).ToString();
                Fill_TB_Summary();
            }
            catch (Exception)
            {
                MessageBox.Show("Error with update value");
            }
            finally
            {
                IsAutoFillForm = false;
            }
        }
        private void NUD_6_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (IsAutoFillForm) return;
                IsAutoFillForm = true;
                Repository.UpdateInputValue(Repo.Entertainment, Double.Parse(NUD_6.Value.ToString()));
                TB_6.Text = Math.Round(Repo.Month_income_Value * Repository.BuforBudgetData.GetInputValueOf(Repo.Month_income) * Repository.BuforBudgetData.GetInputValueOf(Repo.Entertainment), 2).ToString();
                Fill_TB_Summary();
            }
            catch (Exception)
            {
                MessageBox.Show("Error with update value");
            }
            finally
            {
                IsAutoFillForm = false;
            }
        }
        private void NUD_7_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (IsAutoFillForm) return;
                IsAutoFillForm = true;
                Repository.UpdateInputValue(Repo.Stimulant, Double.Parse(NUD_7.Value.ToString()));
                TB_7.Text = Math.Round(Repo.Month_income_Value * Repository.BuforBudgetData.GetInputValueOf(Repo.Month_income) * Repository.BuforBudgetData.GetInputValueOf(Repo.Stimulant), 2).ToString();
                Fill_TB_Summary();
            }
            catch (Exception)
            {
                MessageBox.Show("Error with update value");
            }
            finally
            {
                IsAutoFillForm = false;
            }
        }
        private void NUD_8_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (IsAutoFillForm) return;
                IsAutoFillForm = true;
                Repository.UpdateInputValue(Repo.Investment_S, Double.Parse(NUD_8.Value.ToString()));
                TB_8.Text = Math.Round(Repo.Month_income_Value * Repository.BuforBudgetData.GetInputValueOf(Repo.Month_income) * Repository.BuforBudgetData.GetInputValueOf(Repo.Investment_S), 2).ToString();
                Fill_TB_Summary();
            }
            catch (Exception)
            {
                MessageBox.Show("Error with update value");
            }
            finally
            {
                IsAutoFillForm = false;
            }
        }
        private void NUD_9_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (IsAutoFillForm) return;
                IsAutoFillForm = true;
                Repository.UpdateInputValue(Repo.Investment_R, Double.Parse(NUD_9.Value.ToString()));
                TB_9.Text = Math.Round(Repo.Month_income_Value * Repository.BuforBudgetData.GetInputValueOf(Repo.Month_income) * Repository.BuforBudgetData.GetInputValueOf(Repo.Investment_R), 2).ToString();
                Fill_TB_Summary();
            }
            catch (Exception)
            {
                MessageBox.Show("Error with update value");
            }
            finally
            {
                IsAutoFillForm = false;
            }
        }

        private void NUD_Incomes_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (IsAutoFillForm) return;
                IsAutoFillForm = true;
                Repository.UpdateInputValue(Repo.Month_income, Double.Parse(NUD_Incomes.Value.ToString()));
                TB_Incomes.Text = Math.Round(Repo.Month_income_Value * Repository.BuforBudgetData.GetInputValueOf(Repo.Month_income), 2).ToString();
                Fill_TB_Summary();
                Fill_TB_Data();
            }
            catch (Exception)
            {
                MessageBox.Show("Error with update value");
            }
            finally
            {
                IsAutoFillForm = false;
            }
        }
        private void NUD_Budget_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (IsAutoFillForm) return;
                IsAutoFillForm = true;
                Repository.UpdateInputValue(Repo.Budget, Double.Parse(NUD_Budget.Value.ToString()));
                TB_Budget.Text = Math.Round(Repo.Budget_Value * Repository.BuforBudgetData.GetInputValueOf(Repo.Budget), 2).ToString();
                Fill_TB_Summary();
                Fill_TB_Data();
            }
            catch (Exception)
            {
                MessageBox.Show("Error with update value");
            }
            finally
            {
                IsAutoFillForm = false;
            }
        }
        private void NUD_Calculation_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (IsAutoFillForm) return;
                IsAutoFillForm = true;
                Repository.UpdateOutputValue(Repo.Calculation, Double.Parse(NUD_Calculation.Value.ToString()));
            }
            catch (Exception)
            {
                MessageBox.Show("Error with update value");
            }
            finally
            {
                IsAutoFillForm = false;
            }
        }
        private void Fill_TB_Summary()
        {
            TB_UsedBudget.Text = $"{Repository.BuforBudgetData.GetListOfInput().GetRange(0, 9).Sum() * (Repo.Month_income_Value / Repo.Budget_Value) * 100} %";
            TB_UsedIncomes.Text = $"{Repository.BuforBudgetData.GetListOfInput().GetRange(0, 9).Sum() * 100} %";
        }

        private void TB_1_TextChanged(object sender, EventArgs e)
        {
            if (IsAutoFillForm) return;
            try
            {
                IsAutoFillForm = true;
                double Value = Double.Parse(TB_1.Text);
                double Incomoes = Repo.Month_income_Value * Repository.BuforBudgetData.GetInputValueOf(Repo.Month_income);
                double RelativValue = Value / Incomoes;
                Repository.BuforBudgetData.UpdateInputValue(Repo.Food, RelativValue);
                NUD_1.Value = Decimal.Parse(RelativValue.ToString()) * 100;
                Fill_TB_Summary();
            }
            catch (Exception)
            {
                MessageBox.Show("Error with update value");
            }
            finally
            {
                IsAutoFillForm = false;
            }
        }
        private void TB_2_TextChanged(object sender, EventArgs e)
        {
            if (IsAutoFillForm) return;
            try
            {
                IsAutoFillForm = true;
                double Value = Double.Parse(TB_2.Text);
                double Incomoes = Repo.Month_income_Value * Repository.BuforBudgetData.GetInputValueOf(Repo.Month_income);
                double RelativValue = Value / Incomoes;
                Repository.BuforBudgetData.UpdateInputValue(Repo.Home, RelativValue);
                NUD_2.Value = Decimal.Parse(RelativValue.ToString()) * 100;
                Fill_TB_Summary();
            }
            catch (Exception)
            {
                MessageBox.Show("Error with update value");
            }
            finally
            {
                IsAutoFillForm = false;
            }
        }
        private void TB_3_TextChanged(object sender, EventArgs e)
        {
            if (IsAutoFillForm) return;
            try
            {
                IsAutoFillForm = true;
                double Value = Double.Parse(TB_3.Text);
                double Incomoes = Repo.Month_income_Value * Repository.BuforBudgetData.GetInputValueOf(Repo.Month_income);
                double RelativValue = Value / Incomoes;
                Repository.BuforBudgetData.UpdateInputValue(Repo.Health, RelativValue);
                NUD_3.Value = Decimal.Parse(RelativValue.ToString()) * 100;
                Fill_TB_Summary();
            }
            catch (Exception)
            {
                MessageBox.Show("Error with update value");
            }
            finally
            {
                IsAutoFillForm = false;
            }
        }
        private void TB_4_TextChanged(object sender, EventArgs e)
        {
            if (IsAutoFillForm) return;
            try
            {
                IsAutoFillForm = true;
                double Value = Double.Parse(TB_4.Text);
                double Incomoes = Repo.Month_income_Value * Repository.BuforBudgetData.GetInputValueOf(Repo.Month_income);
                double RelativValue = Value / Incomoes;
                Repository.BuforBudgetData.UpdateInputValue(Repo.Family, RelativValue);
                NUD_4.Value = Decimal.Parse(RelativValue.ToString()) * 100;
                Fill_TB_Summary();
            }
            catch (Exception)
            {
                MessageBox.Show("Error with update value");
            }
            finally
            {
                IsAutoFillForm = false;
            }
        }
        private void TB_5_TextChanged(object sender, EventArgs e)
        {
            if (IsAutoFillForm) return;
            try
            {
                IsAutoFillForm = true;
                double Value = Double.Parse(TB_5.Text);
                double Incomoes = Repo.Month_income_Value * Repository.BuforBudgetData.GetInputValueOf(Repo.Month_income);
                double RelativValue = Value / Incomoes;
                Repository.BuforBudgetData.UpdateInputValue(Repo.Car, RelativValue);
                NUD_5.Value = Decimal.Parse(RelativValue.ToString()) * 100;
                Fill_TB_Summary();
            }
            catch (Exception)
            {
                MessageBox.Show("Error with update value");
            }
            finally
            {
                IsAutoFillForm = false;
            }
        }
        private void TB_6_TextChanged(object sender, EventArgs e)
        {
            if (IsAutoFillForm) return;
            try
            {
                IsAutoFillForm = true;
                double Value = Double.Parse(TB_6.Text);
                double Incomoes = Repo.Month_income_Value * Repository.BuforBudgetData.GetInputValueOf(Repo.Month_income);
                double RelativValue = Value / Incomoes;
                Repository.BuforBudgetData.UpdateInputValue(Repo.Entertainment, RelativValue);
                NUD_6.Value = Decimal.Parse(RelativValue.ToString()) * 100;
                Fill_TB_Summary();
            }
            catch (Exception)
            {
                MessageBox.Show("Error with update value");
            }
            finally
            {
                IsAutoFillForm = false;
            }
        }
        private void TB_7_TextChanged(object sender, EventArgs e)
        {
            if (IsAutoFillForm) return;
            try
            {
                IsAutoFillForm = true;
                double Value = Double.Parse(TB_7.Text);
                double Incomoes = Repo.Month_income_Value * Repository.BuforBudgetData.GetInputValueOf(Repo.Month_income);
                double RelativValue = Value / Incomoes;
                Repository.BuforBudgetData.UpdateInputValue(Repo.Stimulant, RelativValue);
                NUD_7.Value = Decimal.Parse(RelativValue.ToString()) * 100;
                Fill_TB_Summary();
            }
            catch (Exception)
            {
                MessageBox.Show("Error with update value");
            }
            finally
            {
                IsAutoFillForm = false;
            }
        }
        private void TB_8_TextChanged(object sender, EventArgs e)
        {
            if (IsAutoFillForm) return;
            try
            {
                IsAutoFillForm = true;
                double Value = Double.Parse(TB_8.Text);
                double Incomoes = Repo.Month_income_Value * Repository.BuforBudgetData.GetInputValueOf(Repo.Month_income);
                double RelativValue = Value / Incomoes;
                Repository.BuforBudgetData.UpdateInputValue(Repo.Investment_S, RelativValue);
                NUD_8.Value = Decimal.Parse(RelativValue.ToString()) * 100;
                Fill_TB_Summary();
            }
            catch (Exception)
            {
                MessageBox.Show("Error with update value");
            }
            finally
            {
                IsAutoFillForm = false;
            }
        }
        private void TB_9_TextChanged(object sender, EventArgs e)
        {
            if (IsAutoFillForm) return;
            try
            {
                IsAutoFillForm = true;
                double Value = Double.Parse(TB_9.Text);
                double Incomoes = Repo.Month_income_Value * Repository.BuforBudgetData.GetInputValueOf(Repo.Month_income);
                double RelativValue = Value / Incomoes;
                Repository.BuforBudgetData.UpdateInputValue(Repo.Investment_R, RelativValue);
                NUD_9.Value = Decimal.Parse(RelativValue.ToString()) * 100;
                Fill_TB_Summary();
            }
            catch (Exception)
            {
                MessageBox.Show("Error with update value");
            }
            finally
            {
                IsAutoFillForm = false;
            }
        }

        private void TB_Incomes_TextChanged(object sender, EventArgs e)
        {
            if (IsAutoFillForm) return;
            try
            {
                IsAutoFillForm = true;
                double Value = Double.Parse(TB_Incomes.Text);
                double Incomoes = Repo.Month_income_Value * Repository.BuforBudgetData.GetInputValueOf(Repo.Month_income);
                double RelativValue = Value / Incomoes;
                Repository.BuforBudgetData.UpdateOutputValue(Repo.Month_income, RelativValue);
                NUD_Incomes.Value = Decimal.Parse(RelativValue.ToString()) * 100;
                Fill_TB_Summary();
                Fill_TB_Data();
            }
            catch (Exception)
            {
                MessageBox.Show("Error with update value");
            }
            finally
            {
                IsAutoFillForm = false;
            }
        }
        private void TB_Budget_TextChanged(object sender, EventArgs e)
        {
            if (IsAutoFillForm) return;
            try
            {
                IsAutoFillForm = true;
                double Value = Double.Parse(TB_Budget.Text);
                double Incomoes = Repo.Budget_Value * Repository.BuforBudgetData.GetInputValueOf(Repo.Budget);
                double RelativValue = Value / Incomoes;
                Repository.BuforBudgetData.UpdateInputValue(Repo.Budget, RelativValue);
                NUD_Incomes.Value = Decimal.Parse(RelativValue.ToString()) * 100;
                Fill_TB_Summary();
            }
            catch (Exception)
            {
                MessageBox.Show("Error with update value");
            }
            finally
            {
                IsAutoFillForm = false;
            }
        }
        private void Fill_TB_Data()
        {
            double Bufor_value = Repo.Month_income_Value * Repository.BuforBudgetData.GetInputValueOf(Repo.Month_income);
            TB_1.Text = (Repository.BuforBudgetData.GetInputValueOf(Repo.Food) * Bufor_value).ToString();
            TB_2.Text = (Repository.BuforBudgetData.GetInputValueOf(Repo.Home) * Bufor_value).ToString();
            TB_3.Text = (Repository.BuforBudgetData.GetInputValueOf(Repo.Health) * Bufor_value).ToString();
            TB_4.Text = (Repository.BuforBudgetData.GetInputValueOf(Repo.Family) * Bufor_value).ToString();
            TB_5.Text = (Repository.BuforBudgetData.GetInputValueOf(Repo.Car) * Bufor_value).ToString();
            TB_6.Text = (Repository.BuforBudgetData.GetInputValueOf(Repo.Entertainment) * Bufor_value).ToString();
            TB_7.Text = (Repository.BuforBudgetData.GetInputValueOf(Repo.Stimulant) * Bufor_value).ToString();
            TB_8.Text = (Repository.BuforBudgetData.GetInputValueOf(Repo.Investment_S) * Bufor_value).ToString();
            TB_9.Text = (Repository.BuforBudgetData.GetInputValueOf(Repo.Investment_R) * Bufor_value).ToString();
            TB_Incomes.Text = Bufor_value.ToString();
            TB_Budget.Text = (Repo.Budget_Value * Repository.BuforBudgetData.GetInputValueOf(Repo.Budget)).ToString();
            
        }

        private void B_Clear_Click(object sender, EventArgs e)
        {
            Repository.ResetBuforBudgetData();
            ResetControlData();
            Fill_TB_Summary();
        }
        private void ResetControlData()
        {
            NUD_1.Value = Decimal.Parse(Repository.BuforBudgetData.GetInputValueOf(Repo.Food).ToString()) * 100;
            NUD_2.Value = Decimal.Parse(Repository.BuforBudgetData.GetInputValueOf(Repo.Home).ToString()) * 100;
            NUD_3.Value = Decimal.Parse(Repository.BuforBudgetData.GetInputValueOf(Repo.Health).ToString()) * 100;
            NUD_4.Value = Decimal.Parse(Repository.BuforBudgetData.GetInputValueOf(Repo.Family).ToString()) * 100;
            NUD_5.Value = Decimal.Parse(Repository.BuforBudgetData.GetInputValueOf(Repo.Car).ToString()) * 100;
            NUD_6.Value = Decimal.Parse(Repository.BuforBudgetData.GetInputValueOf(Repo.Entertainment).ToString()) * 100;
            NUD_7.Value = Decimal.Parse(Repository.BuforBudgetData.GetInputValueOf(Repo.Stimulant).ToString()) * 100;
            NUD_8.Value = Decimal.Parse(Repository.BuforBudgetData.GetInputValueOf(Repo.Investment_S).ToString()) * 100;
            NUD_9.Value = Decimal.Parse(Repository.BuforBudgetData.GetInputValueOf(Repo.Investment_R).ToString()) * 100;
            NUD_Incomes.Value = Decimal.Parse(Repository.BuforBudgetData.GetInputValueOf(Repo.Month_income).ToString()) * 100;
            NUD_Budget.Value = Decimal.Parse(Repository.BuforBudgetData.GetInputValueOf(Repo.Budget).ToString()) * 100;

            NUD_Calculation.Value = Decimal.Parse(Repository.BuforBudgetData.GetOutputValueOf(Repo.Calculation).ToString()) * 100;
        }

        private void B_Add_Click(object sender, EventArgs e)
        {
            try
            {
                Repository.AddBuforToList();
                List<string[]> MatrixData = Repository.GetDataToList();
                FillListView(MatrixData);
                SwitchView(1);
            }
            catch (Exception)
            {
            }
        }
        private void FillListView(List<string[]> MatrixData)
        {
            listView1.Items.Clear();

            foreach (string[] RowData in MatrixData)
            {
                ListViewItem ViewItem = new ListViewItem(RowData);
                this.listView1.Items.Add(ViewItem);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            string path = @"Data.csv";
            StreamWriter writer = new StreamWriter(path, false);
            if (writer != null)
            {
                foreach (string[] DataRow in Repository.GetDataToList())
                {
                    string singleLine = string.Empty;
                    foreach (string item in DataRow)
                    {
                        singleLine += $"{item}; ";
                    }
                        
                    writer.WriteLine(singleLine);
                }

                writer.Close();
            }
        }

        private void NUD_Layer_ValueChanged(object sender, EventArgs e)
        {
            int tabCount = Int32.Parse(this.NUD_Layer.Value.ToString());

            if (!IsChangeNumberOfLayersPermit(tabCount)) return;

            if (Layers.Count > tabCount)
            {
                this.TC_Layers.TabPages.Remove(Layers[tabCount]);

                TabPages_LayerDetail bufor = Layers[tabCount];
                Layers.RemoveAt(tabCount);
                bufor.Clear();
                GC.Collect();
            }

            if (Layers.Count < tabCount)
            {
                TabPages_LayerDetail buforPage = new TabPages_LayerDetail();

                Layers.Add(buforPage);
                this.TC_Layers.TabPages.Add(buforPage);
            }

            DrawNeuralNetworkView();
        }
            private bool IsChangeNumberOfLayersPermit(int tabCount)
            {
                if (tabCount < 1)
                {
                    this.NUD_Layer.Value = 1;
                    return false;
                }

                if (tabCount > 5)
                {
                    this.NUD_Layer.Value = 5;
                    return false;
                }
                return true;
            }
            private void DrawNeuralNetworkView()
            {
                SwitchView(2);

                List<int> Matrix_NumberOfNeurons = new List<int>();
                Matrix_NumberOfNeurons.Add(11);
                Matrix_NumberOfNeurons.AddRange(GetNumberOfNeuronInLayers());
                Matrix_NumberOfNeurons.Add(1);

                NNV.DrawNNView(Matrix_NumberOfNeurons);
            }
            private List<int> GetNumberOfNeuronInLayers()
            {
                List<int> bufor = new List<int>();

                foreach (TabPages_LayerDetail tab in Layers)
                {
                    bufor.Add(tab.LiczbaNeuronow);
                }

                return bufor;
            }

        private void SwitchView(int i)
        {
            this.TLP.Controls.Remove(this.listView1);
            this.TLP.Controls.Remove(this.NNV);
            this.TLP.Controls.Remove(this.chartSignal1);

            switch (i)
            {
                case 1:
                    this.TLP.Controls.Add(this.listView1, 1, 0);
                    this.listView1.Dock = DockStyle.Fill;
                    this.TLP.SetColumnSpan(this.listView1, 5);
                    break;
                case 2:
                    this.TLP.Controls.Add(this.NNV, 1, 0);
                    this.NNV.Dock = DockStyle.Fill;
                    this.TLP.SetColumnSpan(this.NNV, 5);
                    break;
                case 3:
                    this.TLP.Controls.Add(this.chartSignal1, 1, 0);
                    this.chartSignal1.Dock = DockStyle.Fill;
                    this.TLP.SetColumnSpan(this.chartSignal1, 5);
                    break;
                default:
                    break;
            }
        }

        private void DynamicAutoTestNN_BP(List<double> MatrixError)
        {
            Repository.ErrorMatrix = MatrixError;

            object[] obj = new object[1];
            obj[0] = "Wykres przedstawiający odpowiedź sieci na wybrany przykład." as object;

            this.chartSignal1.Invoke(delegateChart_BP, obj);
        }

        private void RefreshNN_BP(string Title)
        {
            if (Repository.ErrorMatrix == null) return;
            try
            {
                this.chartSignal1.HideAllSeries();
                this.chartSignal1.ClearAllSeries();
                this.chartSignal1.SetTitle(Title);
                
                List<double> BuforListY = new List<double>();
                foreach (var item in Repository.ErrorMatrix) 
                {
                    BuforListY.Add(BuforListY.Count);
                }

                this.chartSignal1.RedrawSeries(Error, Repository.ErrorMatrix, BuforListY);
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("ERROR...1");
            }
            catch (OverflowException)
            {
                Console.WriteLine("ERROR...2");
            }
        }

        private async void B_Learn_Click(object sender, EventArgs e)
        {
            try
            {
                B_Learn.Enabled = false;
                Repository.SetFuncion(this.CB_Funcion.Text);
                Repository.SetNumberOfNeuronsInLayer(GetNumberOfNeuronInLayers());
                Repository.SetChartDelegat(drawchart_BP);
                Global.NI = Double.Parse(TB_Ni.Text);
                Global.MaxEpok = Int32.Parse(TB_Epoch.Text);
                SwitchView(3);
                await Repository.LearnNNAsync();
            }
            catch (Exception)
            {
            }
            finally
            {
                B_Learn.Enabled = true;
            }
        }

        private void B_NNLearning_Click(object sender, EventArgs e)
        {
            SwitchView(3);
        }

        private void B_NNView_Click(object sender, EventArgs e)
        {
            SwitchView(2);
        }

        private void B_Templates_Click(object sender, EventArgs e)
        {
            SwitchView(1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string path = @"DataNN.csv";
            StreamWriter writer = new StreamWriter(path, false);
            if (writer != null)
            {
                writer.Write(Repository.GetExportNNToCSV());
                writer.Close();
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string[] fileLines;
            bool iSFile = false;
            try
            {
                string path = @"DataNN.csv";
                fileLines = File.ReadAllLines(path);
                iSFile = true;
            }
            catch (Exception)
            {
                fileLines = null;
            }
            if (!iSFile) return;

            int LineCounter = 0;
            string NameOfActivationF = fileLines.GetValue(LineCounter++).ToString();

            List<int> ListOfNumerOfNeurons;
            try
            {
                string LineWithSpecyficData = fileLines.GetValue(LineCounter++).ToString();
                string[] tablica = LineWithSpecyficData.Split(';');
                List<int> ConvertedRecord = new List<int>();
                foreach (string t in tablica)
                {
                    ConvertedRecord.Add(Int32.Parse(t));
                }
                ListOfNumerOfNeurons = ConvertedRecord;
            }
            catch (Exception)
            {
                return;
            }

            List<List<double>> ListOfBias = new List<List<double>>();
            for (int i = 0; i < ListOfNumerOfNeurons.Count - 1; i++)
            {
                string LineWithSpecyficData = fileLines.GetValue(LineCounter++).ToString();
                string[] tablica = LineWithSpecyficData.Split(';');
                List<double> ConvertedRecord = new List<double>();
                foreach (string t in tablica)
                {
                    ConvertedRecord.Add(Double.Parse(t));
                }
                ListOfBias.Add(ConvertedRecord);
            }

            List<List<List<double>>> ListOfWages = new List<List<List<double>>>();
            for (int i = 0; i < ListOfNumerOfNeurons.Count - 1; i++)
            {
                List<List<double>> BuforLayer = new List<List<double>>();
                for (int j = 0; j < ListOfNumerOfNeurons[i + 1]; j++)
                {  
                    string LineWithSpecyficData = fileLines.GetValue(LineCounter++).ToString();
                    string[] tablica = LineWithSpecyficData.Split(';');
                    List<double> ConvertedRecord = new List<double>();
                    foreach (string t in tablica)
                    {
                        ConvertedRecord.Add(Double.Parse(t));
                    }
                    BuforLayer.Add(ConvertedRecord);
                }
                ListOfWages.Add(BuforLayer);
            }

            NeuralNetworkEngin engin = new NeuralNetworkEngin(ListOfNumerOfNeurons, NameOfActivationF, ListOfWages, ListOfBias);
            List<double> input = new List<double> { 0,0,0,0,0,0,0,0,0,0.1D,0.1D};
            List<double> ans = engin.WyliczOdpowiedz(input);
            Console.WriteLine($"DATA[ 0 ] =  {ans[0]}");

            input = new List<double> { 1, 1, 1, 1, 1, 0, 0, 0, 0, 0.1D, 0.1D };
            ans = engin.WyliczOdpowiedz(input);
            Console.WriteLine($"DATA[ 1 ] =  {ans[0]}");

            input = new List<double> { 0, 0, 0, 0, 1, 1, 1, 1, 1, 0.1D, 0.1D };
            ans = engin.WyliczOdpowiedz(input);
            Console.WriteLine($"DATA[ 2 ] =  {ans[0]}");

            //DATA[0] = 0,83204878190169
            //DATA[1] = 0,935704943802168
            //DATA[2] = 0,604123514649405
        }
    }
}