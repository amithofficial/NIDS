using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

using ZedGraph;

using Accord.Statistics.Analysis;
using Accord.Statistics.Controls;
using System.Collections;
using System.Drawing.Drawing2D;
using System.Collections;
using System.Diagnostics;
using CenterSpace.Free;






namespace Regression.Linear
{

    public partial class MainForm : System.Windows.Forms.Form
    {
        Stopwatch stp = new Stopwatch();
        Stopwatch stp1 = new Stopwatch();
        // Colors used in the pie graphics
        private readonly Color[] colors = { Color.YellowGreen, Color.DarkOliveGreen, Color.DarkKhaki, Color.Olive,
            Color.Honeydew, Color.PaleGoldenrod, Color.Indigo, Color.Olive, Color.SeaGreen };
        private ArtificialImmuneSystemProgram obj = new ArtificialImmuneSystemProgram();
        String f1 = "Mzc4";
        String f2 = "NjE4";
        String f3 = "NQ==";
        private PrincipalComponentAnalysis pca;
        private DescriptiveAnalysis sda;
        ArrayList al = new ArrayList();
        ArrayList al1 = new ArrayList();
        ArrayList m_arrayX = new ArrayList();
        ArrayList m_arrayY = new ArrayList();
        ArrayList m_arrayZ = new ArrayList();
        ArrayList m_arrayX1 = new ArrayList();
        ArrayList m_arrayY1 = new ArrayList();
        ArrayList m_arrayX2 = new ArrayList();
        ArrayList m_arrayY2 = new ArrayList();
        ArrayList m_arrayZ1 = new ArrayList();
        ArrayList m1_arrayX1 = new ArrayList();
        ArrayList m1_arrayY1 = new ArrayList();
        ArrayList m1_arrayZ1 = new ArrayList();

        
        // Axis points
        static int offsetX = 40;
        static int offsetY = 440;

        int m_xAxisStart = offsetX;
        int m_xAxisEnd = 800;
        int m_yAxisStart = offsetY;
        int m_yAxisEnd = offsetY - 400;
         public static  int c = 1;
         public static int x11 = 240;
         public static int y22 = 256;

         public static int x22 = 340;
         public static int y33 = 456;

         private static int brushTransparency = 128;
        //imported data
        float[,] m_XYdata;
        float m_xMin;
        float m_yMin;
        float m_xMax;
        float m_yMax;
        float m_xDataAxisStart;
        float m_xDataAxisEnd;
        float m_yDataAxisStart;
        float m_yDataAxisEnd;

        // Set default color for data points
        Color m_DataPtColor = Color.Red;
        Color m_DataPtColor1 = Color.Blue;
        private static Color backColor = Color.White;

        string[] sourceColumns;


        public FitnessFunction ff = new FitnessFunction();
        public MainForm()
        {
            InitializeComponent();

            dgvAnalysisSource.AutoGenerateColumns = true;
            dgvDistributionMeasures.AutoGenerateColumns = false;
            dgvFeatureVectors.AutoGenerateColumns = true;
            dgvPrincipalComponents.AutoGenerateColumns = false;
          //  dgvProjectionComponents.AutoGenerateColumns = false;
            dgvProjectionResult.AutoGenerateColumns = true;
        }

        private void DataAnalyzer_Load(object sender, EventArgs e)
        {
            Array methods = Enum.GetValues(typeof(PrincipalComponentAnalysis.AnalysisMethod));
            //  this.tscbMethod.ComboBox.DataSource = methods;
            this.cbMethod.DataSource = methods;


        }


        #region Buttons
        private void btnRunAnalysis_Click(object sender, EventArgs e)
        {
            // Finishes and save any pending changes to the given data
            dgvAnalysisSource.EndEdit();

            // Creates a matrix from the source data table
            double[,] sourceMatrix = ToMatrix(dgvAnalysisSource.DataSource as DataTable, out sourceColumns);

            // Creates the Simple Descriptive Analysis of the given source
            sda = new DescriptiveAnalysis(sourceMatrix, sourceColumns);

            // Populates statistics overview tab with analysis data
          //  dgvStatisticCenter.DataSource = new ArrayDataView(sda.DeviationScores, sourceColumns);
            dgvStatisticStandard.DataSource = new ArrayDataView(sda.StandardScores, sourceColumns);

            dgvStatisticCovariance.DataSource = new ArrayDataView(sda.CovarianceMatrix, sourceColumns);
          //  dgvStatisticCorrelation.DataSource = new ArrayDataView(sda.CorrelationMatrix, sourceColumns);
            dgvDistributionMeasures.DataSource = sda.Measures;

            // string sss = "Covariance";
            // Creates the Principal Component Analysis of the given source
            pca = new PrincipalComponentAnalysis(sda.Source,
                (PrincipalComponentAnalysis.AnalysisMethod)cbMethod.SelectedValue);


            // Compute the Principal Component Analysis
            pca.Compute();

            // Populates components overview with analysis data
            dgvFeatureVectors.DataSource = new ArrayDataView(pca.ComponentMatrix);
            dgvPrincipalComponents.DataSource = pca.Components;

           // dgvProjectionComponents.DataSource = pca.Components;
            //numComponents.Maximum = pca.Components.Count;
            // numComponents.Value = 1;
            numThreshold.Value = (decimal)pca.Components[0].CumulativeProportion * 100;

        //    CreateComponentCumulativeDistributionGraph(graphCurve);
          //  CreateComponentDistributionGraph(graphShare);

            btnShift.Enabled = true;

        }

        private void btnProject_Click(object sender, EventArgs e)
        {
            ArtificialImmuneSystemProgram obj=new ArtificialImmuneSystemProgram();
            string[] colNames;
            //  int components = (int)numComponents.Value;
            int components = 7;
            
            double[,] projectionSource = ToMatrix(dgvProjectionSource.DataSource as DataTable, out colNames);
            double[,] m = pca.Transform(projectionSource, components);
            obj.Detector(components.ToString());
            dgvProjectionResult.DataSource = new ArrayDataView(m, GenerateComponentNames(components));
           // dgvReversionSource.DataSource = dgvProjectionResult.DataSource;
            btnRevert.Enabled = true;

        }

        private void btnRevert_Click(object sender, EventArgs e)
        {
            var lstm = new LSTMCell(inputSize: 3, hiddenSize: 5);

            // Example sequence of inputs
            float[][] inputSequence = new float[][]
            {
                    new float[] { 0.1f, 0.2f, 0.3f },
                    new float[] { 0.2f, 0.3f, 0.4f },
                    new float[] { 0.3f, 0.4f, 0.5f }
            };

            foreach (var input in inputSequence)
            {
                float[] output = lstm.Forward(input);
               
            }

            string fname = Application.StartupPath + "\\" + Program.f1;
            ExcelReader db = new ExcelReader(fname, true, false);
            TableSelectDialog t = new TableSelectDialog(db.GetWorksheetList());
            //    MessageBox.Show(t.ToString());
            this.dataGridView1.DataSource = db.GetWorksheet(t.Selection);

            string fname1 = Application.StartupPath + "\\" + Program.f2;
            ExcelReader db1 = new ExcelReader(fname1, true, false);
            TableSelectDialog t1 = new TableSelectDialog(db1.GetWorksheetList());
            //    MessageBox.Show(t.ToString());
            this.dataGridView2.DataSource = db1.GetWorksheet(t1.Selection);
            string fname2 = Application.StartupPath + "\\" + Program.f3;
            ExcelReader db2 = new ExcelReader(fname2, true, false);
            TableSelectDialog t2 = new TableSelectDialog(db2.GetWorksheetList());
            //    MessageBox.Show(t.ToString());
            this.dataGridView3.DataSource = db2.GetWorksheet(t2.Selection);
            string fname3 = Application.StartupPath + "\\" + Program.f2;
            ExcelReader db3 = new ExcelReader(fname3, true, false);
            TableSelectDialog t3 = new TableSelectDialog(db3.GetWorksheetList());
            //    MessageBox.Show(t.ToString());
            this.dataGridView5.DataSource = db3.GetWorksheet(t3.Selection);
            string fname4 = Application.StartupPath + "\\" + Program.f2;
            ExcelReader db4 = new ExcelReader(fname4, true, false);
            TableSelectDialog t4 = new TableSelectDialog(db4.GetWorksheetList());
            //    MessageBox.Show(t.ToString());
            this.dataGridView4.DataSource = db4.GetWorksheet(t4.Selection);
            //double[,] reversionSource = (double[,])(dgvReversionSource.DataSource as ArrayDataView).Data;
            //double[,] m = pca.Revert(reversionSource);

        
            textBox1.Text = ff.evaluatetp().ToString();
            textBox2.Text = ff.evaluatetn().ToString();
            textBox3.Text = ff.evaluatefp().ToString();
            textBox4.Text = ff.evaluatefn().ToString();
            //Form3 obj = new Form3(al, al1);
            //obj.Show();
            button1.Enabled = true;
            button2.Enabled = true;
           // btnDrawViz.Enabled = true;


        }
        public void pushgraph(string c, string count, string node)
        {

            // chart1.Series[c].Points.AddXY(count, node);
        }
        #endregion


        #region Menus
        private void MenuFileOpen_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string filename = openFileDialog.FileName;
                string extension = Path.GetExtension(filename);
                if (extension == ".xls" || extension == ".xlsx")
                {
                    ExcelReader db = new ExcelReader(filename, true, false);
                    TableSelectDialog t = new TableSelectDialog(db.GetWorksheetList());

                    if (t.ShowDialog(this) == DialogResult.OK)
                    {
                        this.dgvAnalysisSource.DataSource = db.GetWorksheet(t.Selection);
                        this.dgvProjectionSource.DataSource = db.GetWorksheet(t.Selection);
                    }
                }
                else if (extension == ".xml")
                {
                    DataTable dataTableAnalysisSource = new DataTable();
                    dataTableAnalysisSource.ReadXml(openFileDialog.FileName);

                    this.dgvAnalysisSource.DataSource = dataTableAnalysisSource;
                    this.dgvProjectionSource.DataSource = dataTableAnalysisSource.Clone();
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                (dgvAnalysisSource.DataSource as DataTable).WriteXml(saveFileDialog1.FileName, XmlWriteMode.WriteSchema);
            }
        }
        #endregion


        #region Graphs
        public void CreateComponentCumulativeDistributionGraph(ZedGraphControl zgc)
        {
            GraphPane myPane = zgc.GraphPane;

            myPane.CurveList.Clear();

            // Set the titles and axis labels
            myPane.Title.Text = "Component Distribution";
            myPane.Title.FontSpec.Size = 24f;
            myPane.Title.FontSpec.Family = "Tahoma";
            myPane.XAxis.Title.Text = "Components";
            myPane.YAxis.Title.Text = "Percentage";

            PointPairList list = new PointPairList();
            for (int i = 0; i < pca.Components.Count; i++)
            {
                list.Add(pca.Components[i].Index, pca.Components[i].CumulativeProportion);
            }

            // Hide the legend
            myPane.Legend.IsVisible = false;

            // Add a curve
            LineItem curve = myPane.AddCurve("label", list, Color.Red, SymbolType.Circle);
            curve.Line.Width = 2.0F;
            curve.Line.IsAntiAlias = true;
            curve.Symbol.Fill = new Fill(Color.White);
            curve.Symbol.Size = 7;

            myPane.XAxis.Scale.MinAuto = true;
            myPane.XAxis.Scale.MaxAuto = true;
            myPane.YAxis.Scale.MinAuto = true;
            myPane.YAxis.Scale.MaxAuto = true;
            myPane.XAxis.Scale.MagAuto = true;
            myPane.YAxis.Scale.MagAuto = true;


            // Calculate the Axis Scale Ranges
            zgc.AxisChange();
            zgc.Invalidate();
        }

        public void CreateComponentDistributionGraph(ZedGraphControl zgc)
        {
            GraphPane myPane = zgc.GraphPane;
            myPane.CurveList.Clear();

            // Set the GraphPane title
            myPane.Title.Text = "Component Proportion";
            myPane.Title.FontSpec.Size = 24f;
            myPane.Title.FontSpec.Family = "Tahoma";

            // Fill the pane background with a color gradient
            //  myPane.Fill = new Fill(Color.White, Color.WhiteSmoke, 45.0f);
            // No fill for the chart background
            myPane.Chart.Fill.Type = FillType.None;

            myPane.Legend.IsVisible = false;

            // Add some pie slices
            for (int i = 0; i < pca.Components.Count; i++)
            {
                myPane.AddPieSlice(pca.Components[i].Proportion, colors[i % colors.Length], 0.1, pca.Components[i].Index.ToString());
            }

            myPane.XAxis.Scale.MinAuto = true;
            myPane.XAxis.Scale.MaxAuto = true;
            myPane.YAxis.Scale.MinAuto = true;
            myPane.YAxis.Scale.MaxAuto = true;
            myPane.XAxis.Scale.MagAuto = true;
            myPane.YAxis.Scale.MagAuto = true;


            // Calculate the Axis Scale Ranges
            zgc.AxisChange();

            zgc.Invalidate();
        }
        #endregion


        #region Events
        private void numComponents_ValueChanged(object sender, EventArgs e)
        {
            //if (rbComponents.Checked)
            //{
            // int num = (int)numComponents.Value;
            int num = 7;
            numThreshold.Value = (decimal)pca.CumulativeProportions[num - 1] * 100;

            //dgvProjectionComponents.ClearSelection();
            //for (int i = 0; i < num && i < dgvProjectionComponents.Rows.Count; i++)
            //    dgvProjectionComponents.Rows[i].Selected = true;
            // }
        }

        private void numThreshold_ValueChanged(object sender, EventArgs e)
        {
            if (rbThreshold.Checked)
            {
                int num = pca.GetNumberOfComponents((float)numThreshold.Value / 100);
                // numComponents.Value = num;

                //dgvProjectionComponents.ClearSelection();
                //for (int i = 0; i < num && i < dgvProjectionComponents.Rows.Count; i++)
                //    dgvProjectionComponents.Rows[i].Selected = true;
            }
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {
            if (dgvDistributionMeasures.CurrentRow != null)
            {
                DataGridViewRow row = (DataGridViewRow)dgvDistributionMeasures.CurrentRow;
               // dataHistogramView1.DataSource =
                 //   ((DescriptiveMeasures)row.DataBoundItem).Samples;
            }
        }
        #endregion



        private double[,] ToMatrix(DataTable table, out string[] columnNames)
        {

            //double[,] m = new double[table.Rows.Count, table.Columns.Count-5];
            double[,] m = new double[table.Rows.Count, 7];
            //columnNames = new string[table.Columns.Count];
            columnNames = new string[7];
            int k = 0;

            //  for (int j = 4; j < table.Columns.Count-10; j++)
            for (int j = 0; j < 40; j++)
            {
                if (j == 0)
                {
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        if (table.Columns[j].DataType == typeof(System.String))
                        {
                            m[i, k] = Double.Parse((String)table.Rows[i][j]);
                        }
                        else if (table.Columns[j].DataType == typeof(System.Boolean))
                        {
                            m[i, k] = (Boolean)table.Rows[i][j] ? 1.0 : 0.0;
                        }
                        else
                        {
                            m[i, k] = (Double)table.Rows[i][j];
                            // MessageBox.Show(i+":"+k+":"+m[i, k].ToString());
                        }
                    }
                   // columnNames[k] = table.Columns[j].Caption;
                    columnNames[k] = Program.type1;
                  //  MessageBox.Show(columnNames[k].ToString());

                    k++;

                }
                if (j == 11)
                {
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        if (table.Columns[j].DataType == typeof(System.String))
                        {
                            m[i, k] = Double.Parse((String)table.Rows[i][j]);
                        }
                        else if (table.Columns[j].DataType == typeof(System.Boolean))
                        {
                            m[i, k] = (Boolean)table.Rows[i][j] ? 1.0 : 0.0;
                        }
                        else
                        {
                            m[i, k] = (Double)table.Rows[i][j];
                            // MessageBox.Show(i+":"+k+":"+m[i, k].ToString());
                        }
                    }
                    columnNames[k] = Program.type2;
                  //  MessageBox.Show(columnNames[k].ToString());

                    k++;
                }
                if (j == 20)
                {
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        if (table.Columns[j].DataType == typeof(System.String))
                        {
                            m[i, k] = Double.Parse((String)table.Rows[i][j]);
                        }
                        else if (table.Columns[j].DataType == typeof(System.Boolean))
                        {
                            m[i, k] = (Boolean)table.Rows[i][j] ? 1.0 : 0.0;
                        }
                        else
                        {
                            m[i, k] = (Double)table.Rows[i][j];
                            // MessageBox.Show(i+":"+k+":"+m[i, k].ToString());
                        }
                    }
                    columnNames[k] = Program.type3;
                  //  MessageBox.Show(columnNames[k].ToString());

                    k++;
                }
                if (j == 29)
                {
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        if (table.Columns[j].DataType == typeof(System.String))
                        {
                            m[i, k] = Double.Parse((String)table.Rows[i][j]);
                        }
                        else if (table.Columns[j].DataType == typeof(System.Boolean))
                        {
                            m[i, k] = (Boolean)table.Rows[i][j] ? 1.0 : 0.0;
                        }
                        else
                        {
                            m[i, k] = (Double)table.Rows[i][j];
                            // MessageBox.Show(i+":"+k+":"+m[i, k].ToString());
                        }
                    }
                    columnNames[k] = Program.type4;
                  //  MessageBox.Show(columnNames[k].ToString());

                    k++;
                }
                if (j == 26)
                {
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        if (table.Columns[j].DataType == typeof(System.String))
                        {
                            m[i, k] = Double.Parse((String)table.Rows[i][j]);
                        }
                        else if (table.Columns[j].DataType == typeof(System.Boolean))
                        {
                            m[i, k] = (Boolean)table.Rows[i][j] ? 1.0 : 0.0;
                        }
                        else
                        {
                            m[i, k] = (Double)table.Rows[i][j];
                            // MessageBox.Show(i+":"+k+":"+m[i, k].ToString());
                        }
                    }
                    columnNames[k] = Program.type5;
                  //  MessageBox.Show(columnNames[k].ToString());

                    k++;
                }
                if (j == 27)
                {
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        if (table.Columns[j].DataType == typeof(System.String))
                        {
                            m[i, k] = Double.Parse((String)table.Rows[i][j]);
                        }
                        else if (table.Columns[j].DataType == typeof(System.Boolean))
                        {
                            m[i, k] = (Boolean)table.Rows[i][j] ? 1.0 : 0.0;
                        }
                        else
                        {
                            m[i, k] = (Double)table.Rows[i][j];
                            // MessageBox.Show(i+":"+k+":"+m[i, k].ToString());
                        }
                    }
                    columnNames[k] = Program.type6;
                 //   MessageBox.Show(columnNames[k].ToString());

                    k++;
                }
                if (j == 33)
                {
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        if (table.Columns[j].DataType == typeof(System.String))
                        {
                            m[i, k] = Double.Parse((String)table.Rows[i][j]);
                        }
                        else if (table.Columns[j].DataType == typeof(System.Boolean))
                        {
                            m[i, k] = (Boolean)table.Rows[i][j] ? 1.0 : 0.0;
                        }
                        else
                        {
                            m[i, k] = (Double)table.Rows[i][j];
                            // MessageBox.Show(i+":"+k+":"+m[i, k].ToString());
                        }
                    }
                    columnNames[k] = Program.type7;
                  //  MessageBox.Show(columnNames[k].ToString());

                    k++;
                }


                  
                
            }
            return m;
        }

        private string[] GenerateComponentNames(int number)
        {
            string[] names = new string[number];
            for (int i = 0; i < names.Length; i++)
            {
                names[i] = "Component " + (i + 1);
            }
            return names;
        }

        private void rbComponents_CheckedChanged(object sender, EventArgs e)
        {
            // numComponents.Enabled = true;
            numThreshold.Enabled = false;
        }

        private void rbThreshold_CheckedChanged(object sender, EventArgs e)
        {
            // numComponents.Enabled = false;
            numThreshold.Enabled = true;
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {


            double tp = Convert.ToDouble(textBox1.Text);
            double tn = Convert.ToDouble(textBox2.Text);
            double fp = Convert.ToDouble(textBox3.Text);
            double fn = Convert.ToDouble(textBox4.Text);

            double tpr = tp / (tp + fn);
            textBox8.Text = (tpr * 100).ToString();

            double tnr = (tn / (tn + fp));
            textBox7.Text = (tnr * 100).ToString();

            double fnr = (1 - tpr);
            textBox6.Text = (fnr * 100).ToString();
            double fpr = (1 - tnr);
            textBox5.Text = (fpr * 100).ToString();

            double acc = ((tp + tn) / (tp + tn + fp + fn)) * 100;
            textBox9.Text = acc.ToString();
           // button5.Enabled = true;

        }

        private void btnDrawViz_Click(object sender, EventArgs e)
        {
            

        }

        private void pnlViz_Paint(object sender, PaintEventArgs e)
        {
           
        }
        
       
        
        
        
        private void btnDataPtColor_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string filename = Application.StartupPath + "//data//abc.xls";
            ExcelReader db = new ExcelReader(filename, true, false);
            TableSelectDialog t = new TableSelectDialog(db.GetWorksheetList());

            if (t.ShowDialog(this) == DialogResult.OK)
            {
                this.dgvAnalysisSource.DataSource = db.GetWorksheet(t.Selection);
                this.dgvProjectionSource.DataSource = db.GetWorksheet(t.Selection);
            }
            btnSampleRunAnalysis.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                (dgvAnalysisSource.DataSource as DataTable).WriteXml(saveFileDialog1.FileName, XmlWriteMode.WriteSchema);
            }
        }

        private void pnlViz_MouseMove(object sender, MouseEventArgs e)
        {
            //Text = e.Location.X + "," + e.Location.Y;
            //label3.Text = Text;
           
        }

        private void pnlViz_Click(object sender, EventArgs e)
        {
            //Point point = pnlViz.PointToClient(Cursor.Position);
            //label3.Text = point.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            StreamWriter SaveFile = new StreamWriter("E:\\test5.txt");

           

        }
        private Point[] StarPoints1(int num_points, Rectangle bounds)
        {
            // Make room for the points.
            Point[] pts = new Point[num_points];

            double rx = ((bounds.Width) / 4);
            double ry = ((bounds.Height) / 4);
            double cx = bounds.X + rx;
            double cy = bounds.Y + ry;

            // Start at the top.
            double theta = -Math.PI / 2;
            double dtheta = 4 * Math.PI / num_points;
            //for (int i = 0; i < num_points; i++)
            //{
            //    pts[i] = new PointF(
            //        (float)(cx + rx * Math.Cos(theta)),
            //        (float)(cy + ry * Math.Sin(theta)));
            //    theta += dtheta;
            //}
            pts[0] = new Point(487, 116);
            pts[1] = new Point(604, 406);
            pts[2] = new Point(322, 238);
            pts[3] = new Point(675, 229);
            pts[4] = new Point(374, 428);
            return pts;
        }
        
        private Point[] StarPoints(int num_points, Rectangle bounds)
        {
            // Make room for the points.
            Point[] pts = new Point[num_points];

            double rx = ((bounds.Width) / 4);
            double ry = ((bounds.Height) / 4);
            double cx = bounds.X + rx;
            double cy = bounds.Y + ry;
          
            // Start at the top.
            double theta = -Math.PI / 2;
            double dtheta = 4 * Math.PI / num_points;
            //for (int i = 0; i < num_points; i++)
            //{
            //    pts[i] = new PointF(
            //        (float)(cx + rx * Math.Cos(theta)),
            //        (float)(cy + ry * Math.Sin(theta)));
            //    theta += dtheta;
            //}
            pts[0] = new Point(487, 116);
            pts[1] = new Point(604, 406);
            pts[2] = new Point(322, 238);
            pts[3] = new Point(675, 229);
            pts[4] = new Point(374, 428);
            return pts;
        }
     //   List<Point> poly
        public bool PointInPolygon(Point p, Point[] poly)
        {
            Point p1, p2;

            bool inside = false;

            if (poly.Length < 3)
            {
                return inside;
            }

            Point oldPoint = new Point(poly[poly.Length - 1].X, poly[poly.Length - 1].Y);

            for (int i = 0; i < poly.Length; i++)
            {
                Point newPoint = new Point(poly[i].X, poly[i].Y);

                if (newPoint.X > oldPoint.X)
                {
                    p1 = oldPoint;
                    p2 = newPoint;
                }
                else
                {
                    p1 = newPoint;
                    p2 = oldPoint;
                }

                if ((newPoint.X < p.X) == (p.X <= oldPoint.X)
                && ((long)p.Y - (long)p1.Y) * (long)(p2.X - p1.X)
                 < ((long)p2.Y - (long)p1.Y) * (long)(p.X - p1.X))
                {
                    inside = !inside;
                }

                oldPoint = newPoint;
            }

            return inside;
        }
        private void button5_Click_1(object sender, EventArgs e)
        {



            //stp.Start();

            //int count = 1;
            //int ptSize1 = Int16.Parse("4");
            //float fntSizef = 12;
            //Graphics g = panel1.CreateGraphics();

            ////g.Clear(panel1.BackColor);

            //// PointF[] pts = StarPoints(5, panel1.ClientRectangle);
            //Point[] pts = StarPoints(5, panel1.ClientRectangle);
            //using (Pen big_pen = new Pen(Color.White, 0))
            //{
            //    g.DrawPolygon(big_pen, pts);
            //}
            //Point[] pts3 = new Point[5];
            //pts3[0] = new Point(449, 241);
            //pts3[1] = new Point(532, 240);
            //pts3[2] = new Point(558, 303);
            //pts3[3] = new Point(503, 336);
            //pts3[4] = new Point(431, 294);
            //using (Pen big_pen = new Pen(Color.White, 0))
            //{
            //    g.DrawPolygon(big_pen, pts3);
            //}
            //for (int i = 1; i < panel1.Width; i++)
            //{
            //    for (int j = 1; j < panel1.Height; j++)
            //    {

            //        Point p = new Point(i, j);
            //        bool nn = PointInPolygon(p, pts);
            //        bool nn1 = PointInPolygon(p, pts3);
            //        if (nn == true)
            //        {
            //            count = count + 1;
            //            // MessageBox.Show(count.ToString());
            //            if (count % 25 == 0)
            //            {
            //                System.Threading.Thread.Sleep(5);
            //                Pen myPen1 = new Pen(m_DataPtColor, 1);

            //                System.Drawing.SolidBrush myBrush1 = new System.Drawing.SolidBrush(System.Drawing.Color.Red);

            //                //  g.DrawEllipse(myPen1, new Rectangle(i, j, ptSize1, ptSize1));
            //                g.FillEllipse(myBrush1, new Rectangle(i, j, ptSize1, ptSize1));
            //            }
            //        }
            //        if (nn1 == true)
            //        {
            //            count = count + 1;
            //            // MessageBox.Show(count.ToString());
            //            if (count % 25 == 0)
            //            {
            //                System.Threading.Thread.Sleep(5);
            //                Pen myPen1 = new Pen(m_DataPtColor, 1);

            //                System.Drawing.SolidBrush myBrush1 = new System.Drawing.SolidBrush(System.Drawing.Color.Red);

            //                //  g.DrawEllipse(myPen1, new Rectangle(i, j, ptSize1, ptSize1));
            //                g.FillEllipse(myBrush1, new Rectangle(i, j, ptSize1, ptSize1));
            //            }
            //        }

            //    }
            //}


            //int ptSize5 = Int16.Parse("75");

            //FileStream aFile = new FileStream(Program.f7, FileMode.Open, FileAccess.Read);
            //StreamReader sr = new StreamReader(aFile);

            //string strLine;
            //strLine = sr.ReadLine();
            //string[] strData;
            //char[] chrDelimeter = new char[] { ',' };

            //// Read line from file and split into x, y, z dimensions
            //// price, HP, # cylinders
            //while (strLine != null)
            //{
            //    if (strLine == "")
            //    {

            //    }
            //    else
            //    {
            //        strData = strLine.Split(chrDelimeter, 10);

            //        m_arrayX1.Add(strData[0]);
            //        m_arrayY1.Add(strData[1]);
            //        m_arrayZ1.Add(strData[2]);
            //        //m_arrayZ.Add (strData[2]);
            //    }



            //    strLine = sr.ReadLine();
            //}

            //Pen blackPen = new Pen(Color.White, 2);
            //for (int m = 0; m < m_arrayX1.Count; m++)
            //{
            //    // SolidBrush backBrush = new SolidBrush(backColor);
            //    Color bRed = Color.FromArgb(128, 255, 255, 0);

            //    Pen myPen1 = new Pen(m_DataPtColor1, 1);
              

            //    //  System.Drawing.SolidBrush myBrush1 = new System.Drawing.SolidBrush(System.Drawing.Color.Yellow);
            //    System.Drawing.SolidBrush myBrush1 = new System.Drawing.SolidBrush(bRed);

            //    System.Threading.Thread.Sleep(500);

            //    g.DrawEllipse(myPen1, new Rectangle(Convert.ToInt32(m_arrayX1[m]), Convert.ToInt32(m_arrayY1[m]), Convert.ToInt32(m_arrayZ1[m]), Convert.ToInt32(m_arrayZ1[m])));

            //    g.FillEllipse(myBrush1, new Rectangle(Convert.ToInt32(m_arrayX1[m]), Convert.ToInt32(m_arrayY1[m]), Convert.ToInt32(m_arrayZ1[m]), Convert.ToInt32(m_arrayZ1[m])));

            //    myBrush1.Dispose();
               


            //}
            //g.DrawRectangle(blackPen, 290, 80, 400, 410);

            //stp.Stop();
            //Program.vtime = stp.ElapsedMilliseconds.ToString();
          //  MessageBox.Show(Program.vtime);

            //button6.Enabled = true;
          
        }
       
       


        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            //Point point = panel1.PointToClient(Cursor.Position);
            //label11.Text = point.ToString();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            //Point point = panel1.PointToClient(Cursor.Position);
            //label11.Text = point.ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           
        }

        private void button6_Click_2(object sender, EventArgs e)
        {
            //stp1.Start();
            //int count = 1;
            //int ptSize11 = Int16.Parse("4");
            //float fntSizef = 12;
            //Graphics g1 = panel2.CreateGraphics();

            ////g.Clear(panel1.BackColor);

            //// PointF[] pts = StarPoints(5, panel1.ClientRectangle);
            //Point[] ptss = StarPoints1(5, panel2.ClientRectangle);
            //using (Pen big_pen1 = new Pen(Color.White, 0))
            //{
            //    g1.DrawPolygon(big_pen1, ptss);
            //}
            //Point[] pts31 = new Point[5];
            //pts31[0] = new Point(449, 241);
            //pts31[1] = new Point(532, 240);
            //pts31[2] = new Point(558, 303);
            //pts31[3] = new Point(503, 336);
            //pts31[4] = new Point(431, 294);
            //using (Pen big_pen1 = new Pen(Color.White, 0))
            //{
            //    g1.DrawPolygon(big_pen1, pts31);
            //}
            //for (int i = 1; i < panel2.Width; i++)
            //{
            //    for (int j = 1; j < panel2.Height; j++)
            //    {

            //        Point p1 = new Point(i, j);
            //        bool nnn = PointInPolygon(p1, ptss);
            //        bool nnn1 = PointInPolygon(p1, pts31);
            //        if (nnn == true)
            //        {
            //            count = count + 1;
            //            // MessageBox.Show(count.ToString());
            //            if (count % 25 == 0)
            //            {
            //              //  System.Threading.Thread.Sleep(2);
            //                Pen myPen11 = new Pen(m_DataPtColor, 1);

            //                System.Drawing.SolidBrush myBrush11 = new System.Drawing.SolidBrush(System.Drawing.Color.Red);

            //                //  g.DrawEllipse(myPen1, new Rectangle(i, j, ptSize1, ptSize1));
            //                g1.FillEllipse(myBrush11, new Rectangle(i, j, ptSize11, ptSize11));
            //            }
            //        }
            //        if (nnn1 == true)
            //        {
            //            count = count + 1;
            //            // MessageBox.Show(count.ToString());
            //            if (count % 25 == 0)
            //            {
            //              //  System.Threading.Thread.Sleep(2);
            //                Pen myPen11 = new Pen(m_DataPtColor, 1);

            //                System.Drawing.SolidBrush myBrush11 = new System.Drawing.SolidBrush(System.Drawing.Color.Red);

            //                //  g.DrawEllipse(myPen1, new Rectangle(i, j, ptSize1, ptSize1));
            //                g1.FillEllipse(myBrush11, new Rectangle(i, j, ptSize11, ptSize11));
            //            }
            //        }

            //    }
            //}


            //int ptSize51 = Int16.Parse("75");

            //FileStream aFile1 = new FileStream(Program.f9, FileMode.Open, FileAccess.Read);
            //StreamReader sr1 = new StreamReader(aFile1);

            //string strLine1;
            //strLine1 = sr1.ReadLine();
            //string[] strData1;
            //char[] chrDelimeter1 = new char[] { ',' };

            //// Read line from file and split into x, y, z dimensions
            //// price, HP, # cylinders
            //while (strLine1 != null)
            //{
            //    if (strLine1 == "")
            //    {

            //    }
            //    else
            //    {
            //        strData1 = strLine1.Split(chrDelimeter1, 10);

            //        m1_arrayX1.Add(strData1[0]);
            //        m1_arrayY1.Add(strData1[1]);
            //        m1_arrayZ1.Add(strData1[2]);
            //        //m_arrayZ.Add (strData[2]);
            //    }



            //    strLine1 = sr1.ReadLine();
            //}

            //Pen blackPen1 = new Pen(Color.White, 2);
            //for (int m = 0; m < m1_arrayX1.Count; m++)
            //{
            //    // SolidBrush backBrush = new SolidBrush(backColor);
            //    Color bRed1 = Color.FromArgb(128, 255, 255, 0);

            //    Pen myPen11 = new Pen(m_DataPtColor1, 1);


            //    //  System.Drawing.SolidBrush myBrush1 = new System.Drawing.SolidBrush(System.Drawing.Color.Yellow);
            //    System.Drawing.SolidBrush myBrush11 = new System.Drawing.SolidBrush(bRed1);

            //    System.Threading.Thread.Sleep(200);

            //    g1.DrawEllipse(myPen11, new Rectangle(Convert.ToInt32(m1_arrayX1[m]), Convert.ToInt32(m1_arrayY1[m]), Convert.ToInt32(m1_arrayZ1[m]), Convert.ToInt32(m1_arrayZ1[m])));

            //    g1.FillEllipse(myBrush11, new Rectangle(Convert.ToInt32(m1_arrayX1[m]), Convert.ToInt32(m1_arrayY1[m]), Convert.ToInt32(m1_arrayZ1[m]), Convert.ToInt32(m1_arrayZ1[m])));

            //    myBrush11.Dispose();



            //}
            //g1.DrawRectangle(blackPen1, 290, 80, 400, 410);

            //stp1.Stop();
            //Program.otime = stp1.ElapsedMilliseconds.ToString();
           // MessageBox.Show(Program.otime);
            button8.Enabled = true;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            NormalDist obj = new NormalDist();
            obj.CDF();
            textBox13.Text = ff.evaluatetp1(f1).ToString();
            textBox12.Text = ff.evaluatetp1(f2).ToString();
            textBox11.Text = ff.evaluatetp1(f3).ToString();
            textBox10.Text = ff.evaluatefn().ToString();
            button7.Enabled = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            double tp = Convert.ToDouble(textBox13.Text);
            double tn = Convert.ToDouble(textBox12.Text);
            double fp = Convert.ToDouble(textBox11.Text);
            double fn = Convert.ToDouble(textBox10.Text);

            double tpr = tp / (tp + fn);
            textBox18.Text = (tpr * 100).ToString();

            double tnr = (tn / (tn + fp));
            textBox17.Text = (tnr * 100).ToString();

            double fnr = (1 - tpr);
            textBox16.Text = (fnr * 100).ToString();
            double fpr = (1 - tnr);
            textBox15.Text = (fpr * 100).ToString();

            double acc = ((tp + tn) / (tp + tn + fp + fn)) * 100;
            textBox14.Text = acc.ToString();
           // button5.Enabled = true;
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            fillChart();
            fillChart1();
        }
        private void fillChart()
        {
            //double x1 = Convert.ToDouble(textBox9.Text) * 100;
            //double y1 = Convert.ToDouble(textBox14.Text) * 100;

            chart1.Series["Series1"].Points.AddXY("LSTM", textBox9.Text);
            chart1.Series["Series1"].Points.AddXY("G-LSTM", textBox14.Text);
            
            chart1.Titles.Add("Accuracy Analysis");
        }
        private void fillChart1()
        {
          

            //chart2.Series["Series1"].Points.AddXY("IF Detector", Program.vtime);
            //chart2.Series["Series1"].Points.AddXY("Optimized IFDetector", Program.otime);

            //chart2.Titles.Add("Time Analysis");
        }  

    }
    public class ArtificialImmuneSystemProgram
    {
        static Random random;
        static void VDetector()
        {

            random = new Random(1);
            int numPatternBits = 12;
            int numAntibodyBits = 4;
            int numLymphocytes = 3;
         //   int stimulationThreshold = 3;

            List<BitArray> selfSet = LoadSelfSet(null);
            ShowSelfSet(selfSet);

            List<Lymphocyte> lymphocyteSet = CreateLymphocyteSet(selfSet, numAntibodyBits,numLymphocytes);
            ShowLymphocyteSet(lymphocyteSet);

            int time = 0;
            int maxTime = 6;
            while (time < maxTime)
            {

                BitArray incoming = RandomBitArray(numPatternBits);

                BitArrayAsString(incoming);
                for (int i = 0; i < lymphocyteSet.Count; ++i)
                {
                    if (lymphocyteSet[i].Detects(incoming) == true)
                    {

                        ++lymphocyteSet[i].stimulation;



                    }
                    ++time;

                } 

            }
            
                
        }
        public static List<BitArray> LoadSelfSet(string dataSource)
        {
            List<BitArray> result = new List<BitArray>();
            bool[] self0 = new bool[] { true, false, false, true, false, true,
                              true, false, true, false, false, true };
            // Etc.
            bool[] self5 = new bool[] { false, false, true, false, true, false,
                              true, false, false, true, false, false };
            result.Add(new BitArray(self0));
            // Etc.
            result.Add(new BitArray(self5));
            return result;
        }
        public void Detector(string a)
        {
            int numBits = Convert.ToInt32(a);
            bool[] bools = new bool[numBits];
            for (int i = 0; i < numBits; ++i)
            {
                //int b = random.Next(0, 2);  // between [0,1] inclusive
               // bools[i] = (b == 0) ? false : true;
            }
            new BitArray(bools);
        }
        public static BitArray RandomBitArray(int numBits)
        {
            bool[] bools = new bool[numBits];
            for (int i = 0; i < numBits; ++i)
            {
                int b = random.Next(0, 2);  // between [0,1] inclusive
                bools[i] = (b == 0) ? false : true;
            }
            return new BitArray(bools);
        }
        private static bool DetectsAny(List<BitArray> selfSet,Lymphocyte lymphocyte)
        {
            for (int i = 0; i < selfSet.Count; ++i)
                if (lymphocyte.Detects(selfSet[i]) == true) return true;
            return false;
        }
        public static void ShowLymphocyteSet(List<Lymphocyte> lymphocyteySet)
        {
            for (int i = 0; i < lymphocyteySet.Count; ++i)
                Console.WriteLine(i + ": " + lymphocyteySet[i].ToString());
        }
        public static void ShowSelfSet(List<BitArray> selfSet)
        {
            for (int i = 0; i < selfSet.Count; ++i)
                Console.WriteLine(i + ": " + BitArrayAsString(selfSet[i]));
        }
        public static string BitArrayAsString(BitArray ba)
        {
            string s = "";
            for (int i = 0; i < ba.Length; ++i)
                s += (ba[i] == true) ? "1 " : "0 ";
            return s;
        }
        public static List<Lymphocyte> CreateLymphocyteSet(List<BitArray> selfSet,int numAntibodyBits, int numLymphocytes)
        {
            List<Lymphocyte> result = new List<Lymphocyte>();
            Dictionary<int, bool> contents = new Dictionary<int, bool>();
            while (result.Count < numLymphocytes)
            {
                BitArray antibody = RandomBitArray(numAntibodyBits);
                Lymphocyte lymphocyte = new Lymphocyte(antibody);
                int hash = lymphocyte.GetHashCode();
                if (DetectsAny(selfSet, lymphocyte) == false &&
                  contents.ContainsKey(hash) == false)
                {
                    result.Add(lymphocyte);
                    contents.Add(hash, true);
                }
            }
            return result;
        }
    }
        public class Lymphocyte
        {
            public BitArray antibody;  // Detector
            public int[] searchTable;  // For fast detection
            public int age;            // Not used; could determine death
            public int stimulation;    // Controls triggering

            public Lymphocyte(BitArray antibody)
            {
                this.antibody = new BitArray(antibody);
                this.searchTable = BuildTable();
                this.age = 0;
                this.stimulation = 0;
            }
            public bool Detects(BitArray pattern)  // Adapted KMP algorithm
            {
                int m = 0;
                int i = 0;
                while (m + i < pattern.Length)
                {
                    if (this.antibody[i] == pattern[m + i])
                    {
                        if (i == antibody.Length - 1)
                            return true;
                        ++i;
                    }
                    else
                    {
                        m = m + i - this.searchTable[i];
                        if (searchTable[i] > -1)
                            i = searchTable[i];
                        else
                            i = 0;
                    }
                }
                return false;  // Not found
            }
            private int[] BuildTable()
            {
                int[] result = new int[antibody.Length];
                int pos = 2;
                int cnd = 0;
                result[0] = -1;
                result[1] = 0;
                while (pos < antibody.Length)
                {
                    if (antibody[pos - 1] == antibody[cnd])
                    {
                        ++cnd; result[pos] = cnd; ++pos;
                    }
                    else if (cnd > 0)
                        cnd = result[cnd];
                    else
                    {
                        result[pos] = 0; ++pos;
                    }
                }
                return result;
            }
            public override int GetHashCode()
            {
                int[] singleInt = new int[1];
                antibody.CopyTo(singleInt, 0);
                return singleInt[0];
            }
            public override string ToString()
            {
                string s = "a = ";
                for (int i = 0; i < antibody.Length; ++i)
                    s += (antibody[i] == true) ? "1 " : "0 ";
                s += + age;
                s += + stimulation;
                return s;
            }
        }
    public class LSTMCell
    {
        private int inputSize;
        private int hiddenSize;

        private float[] h, c; // Hidden state and cell state

        // Weights (randomly initialized for example)
        private float[,] Wf, Wi, Wo, Wc;
        private float[] bf, bi, bo, bc;

        public LSTMCell(int inputSize, int hiddenSize)
        {
            this.inputSize = inputSize;
            this.hiddenSize = hiddenSize;

            h = new float[hiddenSize];
            c = new float[hiddenSize];

            Wf = RandomMatrix(hiddenSize, inputSize);
            Wi = RandomMatrix(hiddenSize, inputSize);
            Wo = RandomMatrix(hiddenSize, inputSize);
            Wc = RandomMatrix(hiddenSize, inputSize);

            bf = new float[hiddenSize];
            bi = new float[hiddenSize];
            bo = new float[hiddenSize];
            bc = new float[hiddenSize];
        }

        public float[] Forward(float[] x)
        {
            float[] f = Sigmoid(Add(Dot(Wf, x), bf));
            float[] i = Sigmoid(Add(Dot(Wi, x), bi));
            float[] o = Sigmoid(Add(Dot(Wo, x), bo));
            float[] cBar = Tanh(Add(Dot(Wc, x), bc));

            for (int j = 0; j < hiddenSize; j++)
            {
                c[j] = f[j] * c[j] + i[j] * cBar[j];
                h[j] = o[j] * (float)Math.Tanh(c[j]);
            }

            return h;
        }

        // Utilities

        private float[] Dot(float[,] matrix, float[] vector)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            float[] result = new float[rows];

            for (int i = 0; i < rows; i++)
            {
                float sum = 0;
                for (int j = 0; j < cols; j++)
                {
                    sum += matrix[i, j] * vector[j];
                }
                result[i] = sum;
            }
            return result;
        }

        private float[] Add(float[] a, float[] b)
        {
            float[] result = new float[a.Length];
            for (int i = 0; i < a.Length; i++) result[i] = a[i] + b[i];
            return result;
        }

        private float[] Sigmoid(float[] x)
        {
            float[] result = new float[x.Length];
            for (int i = 0; i < x.Length; i++)
            {
                result[i] = 1f / (1f + (float)Math.Exp(-x[i]));
            }
            return result;
        }


        private float[] Tanh(float[] x)
        {
            float[] result = new float[x.Length];
            for (int i = 0; i < x.Length; i++)
            {
                result[i] = (float)Math.Tanh(x[i]);
            }
            return result;
        }

        private float[,] RandomMatrix(int rows, int cols)
        {
            var rnd = new Random();
            float[,] result = new float[rows, cols];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    result[i, j] = (float)(rnd.NextDouble() - 0.5);
            return result;
        }
    }

}
