using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BudgetModeler
{
    class TabPages_LayerDetail : TabPage
    {
        static int licznik = 0;
        int id;

        public delegate void DrawNewNNView();
        static DrawNewNNView drawNewNNView;

        NumericUpDown nudNumberOfNeuronsValue;
        private int liczbaNeuronow;
        Label lTypeLayerValue;

        public int LiczbaNeuronow { get => liczbaNeuronow; set => liczbaNeuronow = value; }

        public TabPages_LayerDetail()
        {
            id = licznik++;
            LiczbaNeuronow = 1;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(addTableLayoutPanel());
            this.Location = new System.Drawing.Point(4, 25);
            this.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.Name = "warstwa " + licznik.ToString();
            this.Padding = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.Size = new System.Drawing.Size(192, 59);
            this.TabIndex = (licznik - 1);
            this.Text = "Warstwa " + licznik.ToString();
        }

        public void Clear()
        {
            licznik--;
        }

        private TableLayoutPanel addTableLayoutPanel()
        {
            TableLayoutPanel bufor = new TableLayoutPanel();

            bufor.ColumnCount = 2;
            bufor.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            bufor.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            bufor.Controls.Add(AddLabelNumberOfNeuronsName(), 0, 0);
            bufor.Controls.Add(AddNumericUpDownNumberOfNeuronsValue(), 1, 0);
            bufor.Dock = System.Windows.Forms.DockStyle.Fill;
            bufor.Location = new System.Drawing.Point(3, 3);
            bufor.Name = $"TLP_Layer{licznik.ToString()}";
            bufor.RowCount = 1;
            bufor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            bufor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 53F));
            bufor.Size = new System.Drawing.Size(186, 53);
            bufor.TabIndex = 0;

            return bufor;
        }

        private Label AddLabelNumberOfNeuronsName()
        {
            Label bufor = new Label();

            bufor.AutoSize = true;
            bufor.Dock = System.Windows.Forms.DockStyle.Fill;
            bufor.Location = new System.Drawing.Point(3, 0);
            bufor.Name = "label19";
            bufor.Size = new System.Drawing.Size(87, 53);
            bufor.TabIndex = 0;
            bufor.Text = "Num. of neurons";
            bufor.TextAlign = System.Drawing.ContentAlignment.TopCenter;

            return bufor;
        }

        private NumericUpDown AddNumericUpDownNumberOfNeuronsValue()
        {
            NumericUpDown bufor = new NumericUpDown();

            bufor.Dock = System.Windows.Forms.DockStyle.Fill;
            bufor.Location = new System.Drawing.Point(96, 6);
            bufor.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            bufor.Name = "numericUpDown2";
            bufor.Size = new System.Drawing.Size(87, 22);
            bufor.TabIndex = 1;
            bufor.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);

            nudNumberOfNeuronsValue = bufor;
            return bufor;
        }
        private void numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            LiczbaNeuronow = Int32.Parse(nudNumberOfNeuronsValue.Value.ToString());
            try
            {
                drawNewNNView();
            }
            catch (Exception)
            {
                Console.WriteLine("      ERROR...?");
            }
        }

        public static void SetDelegateRedrawNNView(DrawNewNNView drawNewNNView)
        {
            TabPages_LayerDetail.drawNewNNView = drawNewNNView;
        }
    }
}
