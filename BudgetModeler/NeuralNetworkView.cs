using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BudgetModeler
{
    class NeuralNetworkView : PictureBox
    {
        List<int> Matrix_NumberOfNeurons;
        Bitmap _bitmap;
        Graphics _graphics;
        Pen _pen_1;
        Pen _pen_2;

        List<Rectangle> SubworkSpace;
        List<List<Rectangle>> NeuralNetworkView2D;

        int WorkSpaceWidth, WorkSpaceHeight;
        int MarginX, MarginY;

        public NeuralNetworkView()
        {
            _pen_1 = new Pen(Color.Black, 1.0f);
            _pen_2 = new Pen(Color.Black, 2.0f);
            SubworkSpace = new List<Rectangle>();
            NeuralNetworkView2D = new List<List<Rectangle>>();
            //this.BorderStyle = BorderStyle.FixedSingle;
        }
        public NeuralNetworkView(List<int> macierzLiczbNeuronow)
        {
            Matrix_NumberOfNeurons = macierzLiczbNeuronow;
            _pen_1 = new Pen(Color.Black, 1.0f);
            _pen_2 = new Pen(Color.Black, 2.0f);
            SubworkSpace = new List<Rectangle>();
            NeuralNetworkView2D = new List<List<Rectangle>>();
        }

        public void DrawNNView(List<int> NewMacierzLiczbNeuronow)
        {
            this.Matrix_NumberOfNeurons = NewMacierzLiczbNeuronow;

            PrepareNewBitmap();
            FillBitmap();

            this.Image = _bitmap;
        }
        private void PrepareNewBitmap()
        {
            _bitmap = new Bitmap(this.Width, this.Height);
            _graphics = Graphics.FromImage(_bitmap);

            WorkSpaceWidth = _bitmap.Width;
            WorkSpaceHeight = _bitmap.Height;
            MarginX = WorkSpaceWidth / 20;
            MarginY = WorkSpaceHeight / 20;
            WorkSpaceWidth -= (MarginX * 2);
            WorkSpaceHeight -= (MarginY * 2);
        }
        private void FillBitmap()
        {
            SetSubworkSpaceOfLayer(Matrix_NumberOfNeurons.Count);
            SetSubworkSpaceOfElementsInLayer(Matrix_NumberOfNeurons.Count);
            DrawCircles();
            DrawRectangles();
            DrawLine();
            DrawTextOnNeuron();
            DrawTextOnInputAndOutput();
            DrawTextAsTitle();
        }
        private void SetSubworkSpaceOfLayer(int NumberOfLayers)
        {
            SubworkSpace = new List<Rectangle>();

            int buforUsedSpaceOfWidth = 0;
            for (int i = 0; i < NumberOfLayers; i++) // ile przestrzeni
            {
                int Width = buforUsedSpaceOfWidth;
                buforUsedSpaceOfWidth += WorkSpaceWidth / NumberOfLayers;
                Point startPoint = new Point(Width + MarginX, 0 + MarginY);
                Size size = new Size(WorkSpaceWidth / NumberOfLayers, WorkSpaceHeight);
                Rectangle buforRectangle = new Rectangle(startPoint, size);
                SubworkSpace.Add(buforRectangle);
            }
        }
        private void SetSubworkSpaceOfElementsInLayer(int NumberOfLayers)
        {
            NeuralNetworkView2D = new List<List<Rectangle>>();
            int LayerCounter = 0;
            int MaxNeuronInLayer = Matrix_NumberOfNeurons.Max();
            if (MaxNeuronInLayer > 10) MaxNeuronInLayer = 10;
            if (MaxNeuronInLayer < 5) MaxNeuronInLayer = 5;

            foreach (Rectangle rectangle in SubworkSpace)
            {
                //_graphics.DrawRectangle(_pen_1, rectangle);
                int elementInLayer = Matrix_NumberOfNeurons[LayerCounter];
                int margin = 2;
                int heighRectangle = (rectangle.Height / (MaxNeuronInLayer)) - 2 * margin;
                int widthRectangle = rectangle.Width - (2 * margin);
                int shiftY = 0;
                if (elementInLayer < 10) shiftY = (rectangle.Height - (elementInLayer * (heighRectangle + (2 * margin)))) / 2;
                else shiftY = (rectangle.Height - (MaxNeuronInLayer * (heighRectangle + (2 * margin)))) / 2;
                int startPontX = rectangle.X + margin;
                int startPointY = rectangle.Y + shiftY + margin;

                List<Rectangle> NeuronViewLayer = new List<Rectangle>();
                for (int i = 0; i < elementInLayer; i++)
                {
                    Rectangle newRectangle = new Rectangle(startPontX, startPointY + ((heighRectangle + (margin * 2)) * i), widthRectangle, heighRectangle);
                    //_graphics.DrawRectangle(_pen_1, newRectangle);
                    NeuronViewLayer.Add(newRectangle);
                    if (i == 9) break;
                }

                NeuralNetworkView2D.Add(NeuronViewLayer);
                LayerCounter++;
            }
        }
        private void DrawCircles()
        {
            List<List<Rectangle>> SubworkSpaceNeuralView = new List<List<Rectangle>>();
            SubworkSpaceNeuralView.AddRange(NeuralNetworkView2D.GetRange(1, NeuralNetworkView2D.Count - 2));

            for (int i = 0; i < SubworkSpaceNeuralView.Count; i++)
            {
                List<Rectangle> NeuronViewInLayer = new List<Rectangle>();
                for (int j = 0; j < SubworkSpaceNeuralView[i].Count; j++)
                {
                    Rectangle r = SubworkSpaceNeuralView[i][j];
                    Rectangle newRectangle = new Rectangle(r.X + ((r.Width - r.Height) / 2), r.Y, r.Height, r.Height);
                    _graphics.DrawEllipse(_pen_2, newRectangle);
                    NeuronViewInLayer.Add(newRectangle);
                    if (j == 9) break;
                }
                NeuralNetworkView2D[i + 1] = NeuronViewInLayer;
            }
        }
        private void DrawRectangles()
        {
            List<List<Rectangle>> SubworkSpaceNeuralView = new List<List<Rectangle>>();
            SubworkSpaceNeuralView.Add(NeuralNetworkView2D.First());
            SubworkSpaceNeuralView.Add(NeuralNetworkView2D.Last());

            for (int i = 0; i < 2; i++)
            {
                List<Rectangle> NeuronViewInLayer = new List<Rectangle>();
                for (int j = 0; j < SubworkSpaceNeuralView[i].Count; j++)
                {
                    Rectangle r = SubworkSpaceNeuralView[i][j];
                    Rectangle newRectangle;
                    if (i == 0) newRectangle = new Rectangle(r.X, r.Y + ((r.Height / 2) - 10), r.Width - ((r.Width - r.Height) / 2), 20);
                    else newRectangle = new Rectangle(r.X + ((r.Width - r.Height) / 2), r.Y + ((r.Height / 2) - 10), r.Width - ((r.Width - r.Height) / 2), 20);
                    _graphics.DrawRectangle(_pen_2, newRectangle);
                    NeuronViewInLayer.Add(newRectangle);
                    if (j == 9) break;
                }
                if (i == 0) NeuralNetworkView2D[i] = NeuronViewInLayer;
                else NeuralNetworkView2D[NeuralNetworkView2D.Count - 1] = NeuronViewInLayer;
            }
        }
        private void DrawLine()
        {
            List<Point> StartLinePoints = new List<Point>();
            List<Point> EndLinePoints = new List<Point>();

            for (int i = 0; i < Matrix_NumberOfNeurons.Count; i++)
            {
                if (i != 0)
                {
                    EndLinePoints = PrepareMatrixEndLinePoint(NeuralNetworkView2D[i]);

                    foreach (Point StartPoint in StartLinePoints)
                    {
                        if (EndLinePoints.Count < 1) break;
                        foreach (Point EndPoint in EndLinePoints)
                        {
                            _graphics.DrawLine(_pen_1, StartPoint, EndPoint);
                            if (i == Matrix_NumberOfNeurons.Count - 1) break;
                        }
                        if (i == Matrix_NumberOfNeurons.Count - 1) EndLinePoints.RemoveAt(0);
                    }
                }
                if (Matrix_NumberOfNeurons.Count != i + 1) StartLinePoints = PrepareMatrixStartLinePoint(NeuralNetworkView2D[i]);
            }
        }
        private List<Point> PrepareMatrixEndLinePoint(List<Rectangle> Rectangles)
        {
            List<Point> BuforPoint = new List<Point>();

            foreach (Rectangle rectangle in Rectangles)
            {
                int x = rectangle.Left;
                int y = rectangle.Top + (rectangle.Height / 2);
                Point point = new Point(x, y);
                BuforPoint.Add(point);
            }

            return BuforPoint;
        }
        private List<Point> PrepareMatrixStartLinePoint(List<Rectangle> Rectangles)
        {
            List<Point> BuforPoint = new List<Point>();

            foreach (Rectangle rectangle in Rectangles)
            {
                int x = rectangle.Left + rectangle.Width;
                int y = rectangle.Top + (rectangle.Height / 2);
                Point point = new Point(x, y);
                BuforPoint.Add(point);
            }

            return BuforPoint;
        }
        private void DrawTextOnNeuron()
        {
            int FontSize = 12;
            Font font = new Font("Arial", FontSize);
            SolidBrush solidBrush = new SolidBrush(Color.Black);

            int LayerCounter = 1;
            int NeuronCounter = 1;
            int FontModifier_x = FontSize / 2;
            int FontModifier_y = FontSize / 2;
            int FontModifierLimit = 10;

            List<List<Rectangle>> SubworkSpaceNeuralView = new List<List<Rectangle>>();
            SubworkSpaceNeuralView.AddRange(NeuralNetworkView2D.GetRange(1, NeuralNetworkView2D.Count - 2));

            foreach (List<Rectangle> LayerView in SubworkSpaceNeuralView)
            {
                string sNeuronCounter;
                int NeuronLayerCounter = 0;
                foreach (Rectangle neuron in LayerView)
                {
                    if (NeuronCounter >= FontModifierLimit)
                    {
                        FontModifierLimit *= 10;
                        FontModifier_x += FontSize / 2;
                    }
                    PointF pointNeuronName = new Point(neuron.X + (neuron.Width / 2) - FontModifier_x, neuron.Y + (neuron.Height / 2) - FontModifier_y);
                    if (++NeuronLayerCounter == 9 && Matrix_NumberOfNeurons[LayerCounter] > 10)
                    {
                        sNeuronCounter = "...";
                    }
                    else
                    {
                        if (NeuronLayerCounter == 10 && Matrix_NumberOfNeurons[LayerCounter] > 10) NeuronCounter += Matrix_NumberOfNeurons[LayerCounter] - NeuronLayerCounter;
                        sNeuronCounter = NeuronCounter.ToString();
                    }
                    _graphics.DrawString(sNeuronCounter, font, solidBrush, pointNeuronName);
                    NeuronCounter++;
                }
                LayerCounter++;
            }
        }
        private void DrawTextOnInputAndOutput()
        {
            int FontSize = 12;
            Font font = new Font("Arial", FontSize);
            SolidBrush solidBrush = new SolidBrush(Color.Black);

            int FontShiftXModifier = 0;
            int NeuronCounter = 0;
            string sInputCounter;

            foreach (Rectangle neuron in NeuralNetworkView2D[0])
            {
                sInputCounter = $"Input_{NeuronCounter + 1}/11";
                switch (NeuronCounter)
                {
                    case 0:
                        FontShiftXModifier = 30;
                        break;
                    case 8:
                        sInputCounter = "Input_n/11";
                        break;
                    case 9:
                        if (NeuralNetworkView2D[0].Count < 100)
                        {
                            FontShiftXModifier = 34;
                            sInputCounter = $"Input_11/11";
                        }
                        break;
                }
                PointF pointNeuronName = new Point(neuron.X + (neuron.Width / 2) - FontShiftXModifier, neuron.Y);
                _graphics.DrawString(sInputCounter, font, solidBrush, pointNeuronName);
                NeuronCounter++;
                if (NeuronCounter > 9) break;
            }
            NeuronCounter = 0;

            char ch = 'A';

            foreach (Rectangle neuron in NeuralNetworkView2D[NeuralNetworkView2D.Count - 1])
            {
                sInputCounter = $"Char: {ch++}";

                switch (NeuronCounter)
                {
                    case 0:
                        FontShiftXModifier = 37;
                        break;
                    case 8:
                        sInputCounter = "Char...";
                        break;
                    case 9:
                        sInputCounter = $"Char: Z";
                        break;
                }

                sInputCounter = "Quality";

                PointF pointNeuronName = new Point(neuron.X + (neuron.Width / 2) - FontShiftXModifier, neuron.Y);
                _graphics.DrawString(sInputCounter, font, solidBrush, pointNeuronName);
                NeuronCounter++;
                if (NeuronCounter > 9) break;
            }
        }
        private void DrawTextAsTitle()
        {
            int FontSize = 10;
            Font font = new Font("Arial", FontSize);
            SolidBrush solidBrush = new SolidBrush(Color.Black);
            int FontShiftXModifier = 0;

            int LayerCounter = 0;
            foreach (Rectangle r in SubworkSpace)
            {
                LayerCounter++;
                string title = "Warstwa ukryta"; // 45 / 53
                FontShiftXModifier = 45;
                if (LayerCounter == 1)
                {
                    title = "Sygnały wejściowe";//64 / 52
                    FontShiftXModifier = 52;
                }
                if (LayerCounter == SubworkSpace.Count - 1)
                {
                    title = "Warstwa wyjściowa";//68 / 56
                    FontShiftXModifier = 56;
                }
                if (LayerCounter == SubworkSpace.Count)
                {
                    title = "Sygnały wyjściowe";//64 / 52
                    FontShiftXModifier = 52;
                }

                PointF pointTitle = new Point(r.X + (r.Width / 2) - FontShiftXModifier);
                _graphics.DrawString(title, font, solidBrush, pointTitle);
            }
        }
    }
}
