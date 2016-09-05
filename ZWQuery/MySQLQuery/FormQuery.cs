
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ZedGraph;
using System.Threading;
using System.Configuration;
using System.Diagnostics;
using System.IO;


namespace ZWGPTS
{
    public partial class FormQuery : Form
    {
        private MyDB.Database DB = new MyDB.Database();
        private string m_strRunMode;

        public struct stQueryColumn
        {
            public string strColumn;
            public string strNickName;
            public string strDataType; //数据库中存储的数据类型；
            
            public string strOutput;
            public string strOperator;
            public string strConditions;
            public string strSortType;
            public string strChart;

        }

        public struct stQuery
        {
            public string strTableID;
            public string strTable;
            public string strTableNickName;
            public string strQueryID;
            public string strQueryName;
            public stQueryColumn[] QueryCol;
            public int intTopCount;
        }

        public static stQuery g_stQuery;  //查询快照
        private Dictionary<string, string> m_dictCols;  //保存数据库的data type.

        public FormQuery(string strRunMode)
        {
            InitializeComponent();
            if (!DB.InitDB())
            {
                MessageBox.Show("DB Initial Failed!");

                return;
            }
            if (!DB.Open())
                MessageBox.Show("DB open failed, please check the settings.");
            //如果是ADMIN则显示全部设置，如果不是，则以user状态运行；不可以添加快照等功能；
            m_strRunMode = strRunMode;
            m_dictCols = new Dictionary<string, string>();
        }

        private void FormQuery_Load(object sender, EventArgs e)
        {
            g_stQuery.intTopCount = 15000;  //默认5000
            g_stQuery.strTableID = "";
            g_stQuery.strTable = "";
            g_stQuery.strTableNickName = "";
            g_stQuery.strQueryID = "";
            g_stQuery.strQueryName = "";
            g_stQuery.QueryCol = null;
            txtSQL.Text = "";
            InitGridConditions();
            InitGridResults();
            InitTreeQuery();
            if (m_strRunMode != "ADMIN")
            {
                //txtSQL.Visible = false;
                tbSave.Visible = false;
                ts4.Visible = false;
                //tvQuery.ContextMenuStrip = null;
               // tabControl1.Dock = DockStyle.Fill;

                if (dgvConditions.Rows.Count > 1)
                {
                    dgvConditions.Rows[0].Visible = false;
                }
                dgvConditions.AllowUserToAddRows=false;
            }
        }

        private void InitTreeQuery()
        {
            Int32 intTableID = 0;
            string strTable = "";
            string strNickName = "";
            Int32 intQueryID = 0;
            string strQueryName = "";
            string strColColumn = "";
            string strColNickName = "";
            string strColOutput = "0";
            string strColConditions = "";
            string strColSortType = "";
            string strChart = "";

            tvQuery.Nodes.Clear();

            try
            {
                //从表QueryHead中获取数据表
                string strSQL = "select TableID,tbTable,tbNickName from QueryHead order by TableID";

                if (!DB.ExecuteReader(strSQL))
                {
                    MessageBox.Show("请检查数据库和网络连接！");
                    return;
                }
                tvQuery.BeginUpdate();

                while (DB.dataReader.Read())
                {
                    intTableID = DB.dataReader.GetInt32(0);
                    strTable = DB.dataReader.GetString(1).Trim();
                    strNickName = DB.dataReader.GetString(2).Trim();

                    TreeNode tnTable = new TreeNode();
                    tnTable.ImageIndex = 0;
                    tnTable.SelectedImageIndex = 0;
                    tnTable.Tag = intTableID.ToString();
                    tnTable.Name = strTable;
                    if (strNickName.Equals(""))
                    {
                        tnTable.Text = strTable;
                    }
                    else
                    {
                        tnTable.Text = strNickName;
                        tnTable.ToolTipText = strNickName + " (" + strTable + ")";
                    }
                    tvQuery.Nodes.Add(tnTable);
                }
                tvQuery.EndUpdate();

                //从表QueryDetail中获取查询快照
                strSQL = "select TableID,tbTable,QueryID,QueryName,colColumn,colNickName,colOutput,colConditions,colSortType,colChart,colOperator " +
                    "from v_QueryDetail order by TableID, QueryID, ColumnID";

                if (!DB.ExecuteReader(strSQL))
                {
                    return;
                }
                tvQuery.BeginUpdate();

                while (DB.dataReader.Read())
                {
                    intTableID = DB.dataReader.GetInt32(0);
                    strTable = DB.dataReader.GetString(1).Trim();
                    intQueryID = DB.dataReader.GetInt32(2);
                    strQueryName = DB.dataReader.GetString(3).Trim();
                    strColColumn = DB.dataReader.GetString(4).Trim();
                    strColNickName = DB.dataReader.GetString(5).Trim();
                    strColOutput = DB.dataReader.GetString(6).Trim();
                    strColConditions = DB.dataReader.GetString(7).Trim();
                    strColSortType = DB.dataReader.GetString(8).Trim();
                    strChart = DB.dataReader.GetString(9).Trim();

                    try
                    {
                        TreeNode tnTable = tvQuery.Nodes.Find(strTable, false)[0];
                        if (tnTable.Nodes.Find(intQueryID.ToString(), false).Length == 0)
                        {
                            TreeNode tnTableQuery = new TreeNode();
                            tnTableQuery.ImageIndex = 1;
                            tnTableQuery.SelectedImageIndex = 1;
                            tnTableQuery.Tag = intTableID.ToString();
                            tnTableQuery.Name = intQueryID.ToString();
                            tnTableQuery.Text = strQueryName;
                            tnTable.Nodes.Add(tnTableQuery);
                        }
                    }
                    catch
                    { }
                }
                tvQuery.EndUpdate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "InitTreeQuery()");
            }

            tvQuery.ExpandAll();
        }

        private void InitGridConditions()
        {
            dgvConditions.ReadOnly = false;
            dgvConditions.EditMode = DataGridViewEditMode.EditOnEnter;
            dgvConditions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvConditions.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            dgvConditions.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            dgvConditions.Rows.Clear();
            //dgvConditions.Columns的样式模板已在设计视图中编辑好
        }

        private void InitGridResults()
        {
            dgvResults.ReadOnly = false;
            dgvResults.EditMode = DataGridViewEditMode.EditOnEnter;
            dgvResults.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvResults.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            dgvResults.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            dgvResults.Rows.Clear();
            dgvResults.Columns.Clear();
        }

        private void dgvResults_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            e.Cancel = true;
        }

        /// <summary>
        ///  加载查询快照到条件窗格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvQuery_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                if (tvQuery.SelectedNode == e.Node && e.Button == MouseButtons.Right) return;
                
                tvQuery.SelectedNode = e.Node;
                dgvResults.DataSource = null;
                m_dictCols.Clear();

                if (e.Node.Level == 0)  //数据表
                {
                    g_stQuery.strTableID = e.Node.Tag.ToString();
                    g_stQuery.strTable = e.Node.Name;
                    g_stQuery.strTableNickName = e.Node.Text;
                }
                else if (e.Node.Level == 1)  //查询快照
                {
                    g_stQuery.strTableID = e.Node.Parent.Tag.ToString();
                    g_stQuery.strTable = e.Node.Parent.Name;
                    g_stQuery.strTableNickName = e.Node.Parent.Text;
                }
                else
                {
                    return;
                }

                //加载数据表的字段，初始化条件窗格
                DataGridViewComboBoxCell cboColumn = new DataGridViewComboBoxCell();
                cboColumn.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                cboColumn.DisplayStyleForCurrentCellOnly = false;
                cboColumn.Items.Add("*");

               
                //string strSQL = "select name from syscolumns where id=object_id('" + g_stQuery.strTable + "')";
                string strSQL = " select column_name,DATA_TYPE from information_schema.columns where table_name='"
                    + g_stQuery.strTable + "' order by ordinal_position;";
                if (!DB.ExecuteReader(strSQL))
                {
                    MessageBox.Show("请检查数据库和网络连接！");
                    return;
                }

                while (DB.dataReader.Read())
                {
                    cboColumn.Items.Add(DB.dataReader.GetString(0));
                    m_dictCols.Add(DB.dataReader.GetString(0).Trim(), DB.dataReader.GetString(1).Trim());
                }
                cboColumn.Items.Add("");
                dgvConditions.Columns[0].CellTemplate = cboColumn;
                dgvConditions.Rows.Clear();


                if (e.Node.Level == 0 && false)
                {
                    //点击数据表时，加载数据表的默认查询快照
                    g_stQuery.strQueryName = "";

                    g_stQuery.QueryCol = new stQueryColumn[1];
                    int n = 0;
                    g_stQuery.QueryCol[n].strColumn = "*";
                    g_stQuery.QueryCol[n].strNickName = "";
                    g_stQuery.QueryCol[n].strOutput = "1";
                    g_stQuery.QueryCol[n].strOperator = "";
                    g_stQuery.QueryCol[n].strConditions = "";
                    g_stQuery.QueryCol[n].strSortType = "";
                    g_stQuery.QueryCol[n].strChart = "";

                    dgvConditions.Rows.Add();
                    dgvConditions.Rows[n].Cells[0].Value = g_stQuery.QueryCol[n].strColumn;
                    dgvConditions.Rows[n].Cells[1].Value = g_stQuery.QueryCol[n].strNickName;
                    dgvConditions.Rows[n].Cells[2].Value = g_stQuery.QueryCol[n].strOutput;
                    dgvConditions.Rows[n].Cells[3].Value = g_stQuery.QueryCol[n].strOperator;
                    dgvConditions.Rows[n].Cells[4].Value = g_stQuery.QueryCol[n].strConditions;
                    dgvConditions.Rows[n].Cells[5].Value = g_stQuery.QueryCol[n].strSortType;
                    dgvConditions.Rows[n].Cells[6].Value = g_stQuery.QueryCol[n].strChart;
                }
                else if (e.Node.Level == 1)
                {
                    //点击查询快照时，加载查询快照到条件窗格
                    g_stQuery.strQueryID = e.Node.Name;

                    strSQL = "select count(*) " +
                   "from v_QueryDetail where TableID='" + g_stQuery.strTableID + "' and  QueryID='" + g_stQuery.strQueryID + "'";
                    if (!DB.ExecuteReader(strSQL))
                    {
                        return;
                    }
                    int n = 0;
                    if (DB.dataReader.Read())
                    {
                        n = DB.dataReader.GetInt32(0);
                    }
                    if (n <= 0)
                    {
                        return;
                    }

                    g_stQuery.QueryCol = new stQueryColumn[n];

                    for (int i = 0; i < n; i++)
                    {
                        dgvConditions.Rows.Add();
                    }

                    strSQL = "select tbTable,QueryID,QueryName,colColumn,colNickName,colOutput,colConditions,colSortType,colChart,colOperator " +
                    " from v_QueryDetail where TableID='" + g_stQuery.strTableID + "' and QueryID='" + g_stQuery.strQueryID + "' order by TableID, QueryID, ColumnID";
                    if (!DB.ExecuteReader(strSQL))
                    {
                        return;
                    }
                    n = 0;
                    
                    while (DB.dataReader.Read())
                    {
                        g_stQuery.strTable = DB.dataReader.GetString(0).Trim();
                        g_stQuery.strQueryID = DB.dataReader.GetInt32(1).ToString();
                        g_stQuery.strQueryName = DB.dataReader.GetString(2).Trim();

                        g_stQuery.QueryCol[n].strColumn = DB.dataReader.GetString(3).Trim();
                        g_stQuery.QueryCol[n].strNickName = DB.dataReader.GetString(4).Trim();
                        g_stQuery.QueryCol[n].strOutput = DB.dataReader.GetString(5).Trim();
                        g_stQuery.QueryCol[n].strConditions = DB.dataReader.GetString(6).Trim();
                        g_stQuery.QueryCol[n].strSortType = DB.dataReader.GetString(7).Trim();
                        g_stQuery.QueryCol[n].strChart = DB.dataReader.GetString(8).Trim();
                        g_stQuery.QueryCol[n].strOperator = DB.dataReader.GetString(9).Trim();

                        dgvConditions.Rows[n].Cells[0].Value = g_stQuery.QueryCol[n].strColumn;
                        dgvConditions.Rows[n].Cells[1].Value = g_stQuery.QueryCol[n].strNickName;
                        dgvConditions.Rows[n].Cells[2].Value = (g_stQuery.QueryCol[n].strOutput=="1" ? "1":"0");
                        dgvConditions.Rows[n].Cells[3].Value = g_stQuery.QueryCol[n].strOperator;
                        dgvConditions.Rows[n].Cells[4].Value = g_stQuery.QueryCol[n].strConditions;
                        dgvConditions.Rows[n].Cells[5].Value = g_stQuery.QueryCol[n].strSortType;
                        dgvConditions.Rows[n].Cells[6].Value = g_stQuery.QueryCol[n].strChart;
                        
                        n++;
                    }
                }

                txtSQL.Text = GetSQL();
                if (m_strRunMode != "ADMIN")
                {
                    if(dgvConditions.Rows.Count >1)
                        dgvConditions.Rows[0].Visible = false;
                    dgvConditions.Columns[2].Visible = false;
                    dgvConditions.Columns[0].Visible = false;
                    dgvConditions.Columns[3].Visible = false;
                    dgvConditions.Columns[5].Visible = false;
                    dgvConditions.Columns[6].Visible = false;
                    dgvConditions.Columns[1].ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "tvQuery_NodeMouseClick()");
            }
        }

        /// <summary>
        /// 执行条件窗格中的查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbQuery_Click(object sender, EventArgs e)
        {
            try
            {
                txtSQL.Focus();  //用来确认使“查询设计器dgvConditions”结束编辑状态并激活事件dgvConditions_CellEndEdit()生成SQL语句

                dgvResults.DataSource = null;
                Application.DoEvents();

                if (g_stQuery.strTable == null || g_stQuery.QueryCol == null)
                {
                    MessageBox.Show("请选择数据表或查询快照！", "查询", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (txtSQL.Text.Trim().Equals(""))
                {
                    MessageBox.Show("请设置查询条件！", "查询", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (!DB.ExecuteDataSet(txtSQL.Text)) 
                {
                    MessageBox.Show("查询执行失败或结果为空！", "查询", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                dgvResults.DataSource = DB.dataSet.Tables[0];
                //格式化显示日期时间
                for (int i = 0; i < DB.dataSet.Tables[0].Columns.Count; i++)
                {  
                    if (DB.dataSet.Tables[0].Columns[i].DataType.Name.ToLower() == "datetime")
                    {
                        dgvResults.Columns[i].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
                    }
                }
                CreateChart(zgcChart, DB.dataSet.Tables[0]);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString(), "tbQuery_Click()");
            }
        }

        private string Utf8_2_GBK(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return "";
            }
            Encoding encodingGB = System.Text.Encoding.GetEncoding("GB2312");
            Encoding encodingUTF8 = System.Text.Encoding.UTF8;
            byte[] bytes = encodingUTF8.GetBytes(input);
            
            byte[] newBytes = Encoding.Convert(encodingUTF8, encodingGB, bytes);
            string newOldHtml = encodingGB.GetString(newBytes);
            return newOldHtml;
        }


        private string Gbk2Utf8(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return "";
            }
            Encoding encodingGB = System.Text.Encoding.GetEncoding("GB2312");
            byte[] bytes = encodingGB.GetBytes(input);
            Encoding encodingUTF8 = System.Text.Encoding.UTF8;
            byte[] newBytes = Encoding.Convert(encodingGB, encodingUTF8, bytes);
            string newOldHtml = encodingUTF8.GetString(newBytes);
            return newOldHtml;
        }

        /// <summary>
        /// 生成柱形图、折线图、饼图
        /// </summary>
        /// <param name="zgc"></param>
        public void CreateChart(ZedGraphControl zgc, DataTable dt)
        {
            string strChartType = "";
            string strChartTitle = "图表";
            string strXAxisTitle = "分类(X)轴标志";
            string strYAxisTitle = "系列值";
            string[] XAxisLabel = null;
            double[] YAxisValue = null;

            try
            {
                GraphPane myPane = zgc.GraphPane;

                myPane.CurveList.Clear();
                myPane.GraphObjList.Clear();

                // 设置图表区域的背景色
                myPane.Chart.Fill = new Fill(Color.White, Color.LightGoldenrodYellow, 45F);
                myPane.Fill = new Fill(Color.FromArgb(250, 250, 255));

                // 获取分类(X)轴标志和系列值
                int iRowCount = dt.Rows.Count;
                if (iRowCount > 0)
                {
                    for (int n = 0; n < g_stQuery.QueryCol.Length - 1; n++)
                    {
                        if (XAxisLabel == null &&
                            (g_stQuery.QueryCol[n].strChart.Equals("柱形图-分类(X)轴标志") || g_stQuery.QueryCol[n].strChart.Equals("折线图-分类(X)轴标志") || g_stQuery.QueryCol[n].strChart.Equals("饼图-分类(X)轴标志")))
                        {
                            XAxisLabel = new string[iRowCount];
                            for (int i = 0; i < iRowCount; i++)
                            {
                                XAxisLabel[i] = dt.Rows[i][g_stQuery.QueryCol[n].strColumn].ToString().Trim();
                            }
                            strXAxisTitle = g_stQuery.QueryCol[n].strNickName.Equals("") ? g_stQuery.QueryCol[n].strColumn : g_stQuery.QueryCol[n].strNickName;
                            strChartType = g_stQuery.QueryCol[n].strChart.Split('-')[0];
                            strChartTitle = g_stQuery.strQueryName.Equals("") ? strChartType : g_stQuery.strQueryName;
                        }
                        else if (YAxisValue == null && g_stQuery.QueryCol[n].strChart.Equals("系列值"))
                        {
                            YAxisValue = new double[iRowCount];
                            for (int i = 0; i < iRowCount; i++)
                            {
                                YAxisValue[i] = Convert.ToDouble(dt.Rows[i][g_stQuery.QueryCol[n].strColumn]);
                            }
                            strYAxisTitle = g_stQuery.QueryCol[n].strNickName.Equals("") ? g_stQuery.QueryCol[n].strColumn : g_stQuery.QueryCol[n].strNickName;
                        }
                    }
                }

                // 设置图表标题
                myPane.Title.Text = strChartTitle;
                myPane.XAxis.Title.Text = strXAxisTitle;  // 设置X轴的说明文字
                myPane.YAxis.Title.Text = strYAxisTitle;  // 设置Y轴的说明文字

                // 设置分类(X)轴标志
                myPane.XAxis.Scale.TextLabels = XAxisLabel;
                myPane.XAxis.Type = AxisType.Text;

                // 设置系列值
                switch (strChartType)
                {
                    case "柱形图":
                        // 生成柱状图
                        BarItem myBar = myPane.AddBar(strXAxisTitle, null, YAxisValue, Color.Blue);
                        myBar.Bar.Fill = new Fill(Color.Blue, Color.White, Color.Blue);
                        // 数值标注
                        //BarItem.CreateBarLabels(myPane, false, "");
                        myPane.XAxis.IsVisible = true;  // 显示坐标轴
                        myPane.YAxis.IsVisible = true;
                        myPane.XAxis.MajorGrid.IsVisible = false; // 隐藏X轴网格线
                        myPane.YAxis.MajorGrid.IsVisible = true;  // 显示Y轴网格线
                        break;
                    case "折线图":
                        // 生成折线图
                        LineItem myLine = myPane.AddCurve(strXAxisTitle, null, YAxisValue, Color.Blue, SymbolType.None);
                        myLine.Line.Width = 2;
                        myLine.Symbol.Fill = new Fill(Color.White);
                        myPane.XAxis.IsVisible = true;  // 显示坐标轴
                        myPane.YAxis.IsVisible = true;
                        myPane.XAxis.MajorGrid.IsVisible = false; // 隐藏X轴网格线
                        myPane.YAxis.MajorGrid.IsVisible = true;  // 显示Y轴网格线
                        // 数值标注
                        //for (int i = 0; i < myLine.Points.Count; i++)
                        //{
                        //    PointPair pt = myLine.Points[i];
                        //    TextObj txt = new TextObj(pt.Y.ToString(), pt.X, pt.Y, CoordType.AxisXYScale, AlignH.Left, AlignV.Center);
                        //    txt.ZOrder = ZOrder.A_InFront;
                        //    txt.FontSpec.Border.IsVisible = false;
                        //    txt.FontSpec.Fill.IsVisible = false;
                        //    txt.FontSpec.Angle = 0;
                        //    myPane.GraphObjList.Add(txt);
                        //}
                        break;
                    case "饼图":
                        for (int i = 0; i < iRowCount; i++)
                        {
                            PieItem myPie = myPane.AddPieSlice(YAxisValue[i], Color.Blue, Color.White, 45F, 0, XAxisLabel[i]);
                            myPie.LabelType = PieLabelType.Name_Value_Percent;
                        }
                        myPane.XAxis.IsVisible = false;  // 隐藏坐标轴
                        myPane.YAxis.IsVisible = false;
                        break;
                    default:
                        break;
                }

                zgc.AxisChange();   // 刷新数据
                zgc.Refresh();      // 刷新图表
                zgc.IsShowPointValues = true;  // 设置显示节点值
                zgc.RestoreScale(myPane);  // 重置图表默认大小
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "CreateChart()");
            }
        }

        /// <summary>
        /// 保存条件窗格中的查询快照
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSQL.Text.Trim().Equals(""))
                {
                    MessageBox.Show("请先设置查询条件！", "保存查询", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                int intQueryID;
                string strSQL = "select max(QueryID) from QueryDetail where TableID='" + g_stQuery.strTableID + "'";

                if (!DB.ExecuteReader(strSQL))
                {
                    return;
                }
                if (DB.dataReader.Read())
                {
                    intQueryID = DB.dataReader.IsDBNull(0) ? 0 : DB.dataReader.GetInt32(0);
                }
                else
                {
                    return;
                }

                g_stQuery.strQueryID = (intQueryID + 1).ToString();  //查询快照的ID
                g_stQuery.strQueryName = g_stQuery.strTableID + "-" + g_stQuery.strQueryID;  //查询快照的默认名称

                strSQL = "";
                for (int i = 0; i < g_stQuery.QueryCol.Length; i++)
                {
                    if (g_stQuery.QueryCol[i].strColumn == null || g_stQuery.QueryCol[i].strColumn.Equals(""))
                    {
                        continue;
                    }
                    if ((g_stQuery.QueryCol[i].strOutput == null || g_stQuery.QueryCol[i].strColumn.Equals("")) &&
                        (g_stQuery.QueryCol[i].strConditions == null || g_stQuery.QueryCol[i].strConditions.Equals("")) &&
                        (g_stQuery.QueryCol[i].strSortType == null || g_stQuery.QueryCol[i].strSortType.Equals("")) &&
                        (g_stQuery.QueryCol[i].strChart == null || g_stQuery.QueryCol[i].strChart.Equals("")) &&
                        (g_stQuery.QueryCol[i].strOperator == null || g_stQuery.QueryCol[i].strOperator.Equals("")))
                    {
                        continue;
                    }
                    g_stQuery.QueryCol[i].strConditions = g_stQuery.QueryCol[i].strConditions.Replace("'", "''");

                    strSQL += "insert into QueryDetail (TableID,QueryID,QueryName,ColumnID,colColumn,colNickName,colOutput,colConditions,colSortType,colChart,colOperator) " +
                        "values('" + g_stQuery.strTableID + "','" + g_stQuery.strQueryID + "','" + g_stQuery.strQueryName + "','" + i + "','" +
                        g_stQuery.QueryCol[i].strColumn + "','" + g_stQuery.QueryCol[i].strNickName + "','" + g_stQuery.QueryCol[i].strOutput + "','" +
                        g_stQuery.QueryCol[i].strConditions + "','" + g_stQuery.QueryCol[i].strSortType + "','" + g_stQuery.QueryCol[i].strChart + "','"+g_stQuery.QueryCol[i].strOperator + "');";
                }

                if (DB.ExecuteNonQuery(strSQL) > 0)
                {
                    InitTreeQuery();
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString(), "tbSave_Click()");
            }
        }

        /// <summary>
        /// 导出数据到Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbExport_Click(object sender, EventArgs e)
        {
            if (dgvResults.Rows.Count <= 0)
            {
                MessageBox.Show("没有数据可供导出！", "导出数据", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            OutputToFile(dgvResults);
            return;

            SaveFileDialog SaveFileDialog1 = new SaveFileDialog();
            SaveFileDialog1.DefaultExt = "xls";
            SaveFileDialog1.Filter = "Excel文件(*.xls)|*.xls;*.xlsx";
            SaveFileDialog1.FileName = "Export" + DateTime.Now.ToString("yyyyMMddHHmmss");
            if (SaveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string strFilePathAndName = SaveFileDialog1.FileName.Trim();

                if (MyFiles.ClassExcel.DatasetToExcel(DB.dataSet, strFilePathAndName))
                {
                    if (MessageBox.Show("已成功导出数据到 " + strFilePathAndName + "\n是否打开文件？", "导出数据",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(strFilePathAndName);
                    }
                }
            }
            SaveFileDialog1.Dispose();
        }

        public void OutputToFile(DataGridView dvGrid)
        {
            FileStream fs = new FileStream("C:\\A.csv", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            int nRows = dvGrid.Rows.Count;
            int nCols = dvGrid.Columns.Count;

            string strRow = "";

            for (int ii = 0; ii < nCols; ii++)
            {
                strRow = strRow + "\"" + dvGrid.Columns[ii].HeaderText + "\"";
                if (ii < nCols - 1) strRow = strRow + ",";
            }
            sw.WriteLine(strRow);
            for (int iR = 0; iR < nRows - 1; iR++)
            {
                strRow = "";
                for (int iC = 0; iC < nCols; iC++)
                {
                    strRow = strRow + "\"" + dvGrid.Rows[iR].Cells[iC].Value.ToString().Trim() + "\"";
                    if (iC != nCols - 1) strRow = strRow + ",";
                }
                sw.WriteLine(strRow);
            }
            sw.Close();
            fs.Close();
            System.Diagnostics.Process.Start("C:\\a.csv");
        }


        private void tbExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private string GetConditionFilterSQL(int nRowIndex)
        {
            string strSQL = "";
            for (int n = 0; n < nRowIndex; n++)
            {
                if ((!g_stQuery.QueryCol[n].strConditions.Equals("")) && (!g_stQuery.QueryCol[n].strOperator.Equals("")))
                {
                    strSQL += "(" + g_stQuery.QueryCol[n].strColumn + " " + g_stQuery.QueryCol[n].strOperator + g_stQuery.QueryCol[n].strConditions + ") " + "and ";
                }
            }
            strSQL += " 1=1 "; //否则多一个and 或者是where。
            return strSQL;
        }


        private void dgvConditions_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4 && (!(dgvConditions[0, e.RowIndex].Value == null || dgvConditions[0, e.RowIndex].Value.ToString() == "*" || dgvConditions[3, e.RowIndex].Value.ToString()=="")))  //colConditions
            {
                string[] strValues = new string[100];  //列出20个示例数据供用户选择
                 string strDataType = "";

                 if (m_dictCols.ContainsKey(dgvConditions[0, e.RowIndex].Value.ToString()))
                     strDataType = m_dictCols[dgvConditions[0, e.RowIndex].Value.ToString()].ToUpper();
                 if (strDataType.Contains("DATE") || strDataType.Contains("TIME"))
                 {
                     DateBox frmDate = new DateBox();
                     if (frmDate.ShowDialog() == DialogResult.OK)
                     {
                         dgvConditions[e.ColumnIndex, e.RowIndex].Value = frmDate.DateString;
                     }
                     frmDate.Dispose();
                     txtSQL.Focus();
                     return;
                 }

                string strSQL = "select distinct " + dgvConditions[0, e.RowIndex].Value.ToString() +
                    " from " + g_stQuery.strTable +
                    " where " + dgvConditions[0, e.RowIndex].Value.ToString() + " is not null and "+ GetConditionFilterSQL(e.RowIndex) +
                    " order by " + dgvConditions[0, e.RowIndex].Value.ToString();
                if (DB.ExecuteDataSet(strSQL))
                {
                    for (int i = 0; i < DB.dataSet.Tables[0].Rows.Count; i++)
                    {
                        if (i >= 100) break;
                        strValues[i] = DB.dataSet.Tables[0].Rows[i][0].ToString().Trim();
                    }
                }

                ConditionBox frmConditionBox = new ConditionBox(dgvConditions[0, e.RowIndex].Value.ToString(), strValues);
                frmConditionBox.InputDataType = m_dictCols[dgvConditions[0, e.RowIndex].Value.ToString()].ToUpper();
                if (frmConditionBox.ShowDialog() == DialogResult.OK)
                {
                    dgvConditions[e.ColumnIndex, e.RowIndex].Value = frmConditionBox.Condition;
                }
                frmConditionBox.Dispose();
                txtSQL.Focus();
            }
            else if (e.ColumnIndex == 3 && (!(dgvConditions[0, e.RowIndex].Value == null || dgvConditions[0, e.RowIndex].Value.ToString() == "*")))  //colConditions
            {
                OperatorBox frmOperatorBox = new OperatorBox();
                if (frmOperatorBox.ShowDialog() == DialogResult.OK)
                {
                    dgvConditions[e.ColumnIndex, e.RowIndex].Value = frmOperatorBox.Operator;
                }
                frmOperatorBox.Dispose();
                txtSQL.Focus();
            }

        }

        private void dgvConditions_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (g_stQuery.strTable == null || g_stQuery.QueryCol == null)
            {
                e.Cancel = true;
                return;
            }
            int intColumnIndex = 0; //colColumn
            switch (e.ColumnIndex)  //(dgvConditions.Columns[e.ColumnIndex].Name)
            {
                case 2:  //"colOutput":
                    if (dgvConditions[intColumnIndex, e.RowIndex].Value == null)
                    {
                        e.Cancel = true;
                    }
                    break;
                case 1:  //"colNickName":
                    if (dgvConditions[intColumnIndex, e.RowIndex].Value == null || dgvConditions[intColumnIndex, e.RowIndex].Value.ToString() == "*")
                    {
                        e.Cancel = true;
                    }
                    break;
                case 3:  //"colConditions":
                    if (dgvConditions[intColumnIndex, e.RowIndex].Value == null || dgvConditions[intColumnIndex, e.RowIndex].Value.ToString() == "*")
                    {
                        e.Cancel = true;
                    }
                    break;
                case 4:  //"colSortType":
                    if (dgvConditions[intColumnIndex, e.RowIndex].Value == null || dgvConditions[intColumnIndex, e.RowIndex].Value.ToString() == "*")
                    {
                        e.Cancel = true;
                    }
                    break;
                case 5:  //"colChart":
                    if (dgvConditions[intColumnIndex, e.RowIndex].Value == null || dgvConditions[intColumnIndex, e.RowIndex].Value.ToString() == "*")
                    {
                        e.Cancel = true;
                    }
                    break;
            }

        }

        private void dgvConditions_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //输入处理
            if (e.ColumnIndex == 0)  //colColumn
            {
                if (dgvConditions[e.ColumnIndex, e.RowIndex].Value == null || dgvConditions[e.ColumnIndex, e.RowIndex].Value.Equals("") || dgvConditions[e.ColumnIndex, e.RowIndex].Value.ToString() == "*")
                {
                    dgvConditions["colNickName", e.RowIndex].Value = "";
                    dgvConditions["colOutput", e.RowIndex].Value = (dgvConditions["colColumn", e.RowIndex].Value == null || dgvConditions["colColumn", e.RowIndex].Value.Equals("")) ? "0" : dgvConditions["colOutput", e.RowIndex].Value;
                    dgvConditions["colConditions", e.RowIndex].Value = "";
                    dgvConditions["colSortType", e.RowIndex].Value = "";
                    dgvConditions["colChart", e.RowIndex].Value = "";
                }
            }
            else if (e.ColumnIndex == 5)  //colSortType
            {
                if (dgvConditions[e.ColumnIndex, e.RowIndex].Value == null || dgvConditions[e.ColumnIndex, e.RowIndex].Value.Equals("") || dgvConditions[e.ColumnIndex, e.RowIndex].Value.ToString() == "未排序")
                {
                    dgvConditions[e.ColumnIndex, e.RowIndex].Value = "";
                    dgvConditions[e.ColumnIndex + 1, e.RowIndex].Value = "";
                }
            }

            //验证：至少要有一个输出列
            bool bOutput = false;
            for (int i = 0; i < dgvConditions.Rows.Count; i++)
            {
                if (dgvConditions["colOutput", i].Value != null)
                {
                    if (dgvConditions["colOutput", i].Value.ToString() == "1")
                    {
                        bOutput = true;
                        break;
                    }
                }
            }
            if (bOutput == false)
            {
                txtSQL.Text = "";
                MessageBox.Show("至少要有一个输出列！", "查询条件", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            txtSQL.Text = GetSQL();  //所见即所得
        }

        /// <summary>
        /// 条件窗格转换为SQL语句
        /// </summary>
        /// <returns></returns>
        private string GetSQL()
        {
            g_stQuery.QueryCol = new stQueryColumn[dgvConditions.Rows.Count];

            for (int n = 0; n < dgvConditions.Rows.Count; n++)
            {
                g_stQuery.QueryCol[n].strColumn = dgvConditions.Rows[n].Cells[0].Value == null ? "" : dgvConditions.Rows[n].Cells[0].Value.ToString();
                g_stQuery.QueryCol[n].strNickName = dgvConditions.Rows[n].Cells[1].Value == null ? "" : dgvConditions.Rows[n].Cells[1].Value.ToString();
                g_stQuery.QueryCol[n].strOutput = dgvConditions.Rows[n].Cells[2].Value == null ? "" : dgvConditions.Rows[n].Cells[2].Value.ToString();
                g_stQuery.QueryCol[n].strOperator = dgvConditions.Rows[n].Cells[3].Value == null ? "" : dgvConditions.Rows[n].Cells[3].Value.ToString();
                g_stQuery.QueryCol[n].strConditions = dgvConditions.Rows[n].Cells[4].Value == null ? "" : dgvConditions.Rows[n].Cells[4].Value.ToString();
                g_stQuery.QueryCol[n].strSortType = dgvConditions.Rows[n].Cells[5].Value == null ? "" : dgvConditions.Rows[n].Cells[5].Value.ToString();
                g_stQuery.QueryCol[n].strChart = dgvConditions.Rows[n].Cells[6].Value == null ? "" : dgvConditions.Rows[n].Cells[6].Value.ToString();
            }
            
            string strSQL = " select ";
            if((DB.Config.ServerType == MyDB.DbServerType.SqlServer)|| (DB.Config.ServerType== MyDB.DbServerType.SqlServerCe))
                strSQL += "top " + g_stQuery.intTopCount + " ";

            for (int n = 0; n < g_stQuery.QueryCol.Length; n++)
            {
                if (!g_stQuery.QueryCol[n].strColumn.Equals("") && g_stQuery.QueryCol[n].strOutput.Equals("1"))
                {
                    strSQL += g_stQuery.QueryCol[n].strColumn;
                    if (!g_stQuery.QueryCol[n].strNickName.Equals(""))
                    {
                        strSQL += " as " + g_stQuery.QueryCol[n].strNickName;
                    }
                    strSQL += ",";
                }
            }
            strSQL = strSQL.Remove(strSQL.Length - 1, 1);

            strSQL += "\r\n from " + g_stQuery.strTable;

            bool bWhere = false;
            for (int n = 0; n < g_stQuery.QueryCol.Length; n++)
            {
                if (!g_stQuery.QueryCol[n].strConditions.Equals(""))
                {
                    strSQL += "\r\n where ";
                    bWhere = true;
                    break;
                }
            }
            string sTemp = "";
            for (int n = 0; n < g_stQuery.QueryCol.Length; n++)
            {
                sTemp = g_stQuery.QueryCol[n].strConditions.ToLower();
                if (sTemp.Contains("insert ") || sTemp.Contains("delete ") || sTemp.Contains("update ")
                    || sTemp.Contains("create ") || sTemp.Contains("drop "))
                {
                    MessageBox.Show("筛选条件中不允许出现insert,delete,update,create,drop等关键字！");
                    return "";
                }
                if (!g_stQuery.QueryCol[n].strConditions.Equals(""))
                {
                    strSQL += "(" + g_stQuery.QueryCol[n].strColumn + " " + g_stQuery.QueryCol[n].strOperator + g_stQuery.QueryCol[n].strConditions + ") " + "and ";
                }
            }
            if (bWhere)
            {
                strSQL = strSQL.Remove(strSQL.Length - 4, 4);
            }

            bool bOrderBy = false;
            for (int n = 0; n < g_stQuery.QueryCol.Length; n++)
            {
                if (!g_stQuery.QueryCol[n].strSortType.Equals(""))
                {
                    strSQL += "\r\n order by ";
                    bOrderBy = true;
                    break;
                }
            }
            for (int n = 0; n < g_stQuery.QueryCol.Length; n++)
            {
                if (!g_stQuery.QueryCol[n].strSortType.Equals(""))
                {
                    strSQL += g_stQuery.QueryCol[n].strColumn + " " + (g_stQuery.QueryCol[n].strSortType.Equals("升序") ? "ASC" : "DESC") + ",";
                }
            }
            if (bOrderBy)
            {
                strSQL = strSQL.Remove(strSQL.Length - 1, 1);
            }
            if(DB.Config.ServerType == MyDB.DbServerType.MySql)
                strSQL += "\r\n limit " + g_stQuery.intTopCount;

            return (strSQL);
        }

        /// <summary>
        /// 行自动编号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvResults_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X,
                e.RowBounds.Location.Y,
                dgvResults.RowHeadersWidth - 4,
                e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                dgvResults.RowHeadersDefaultCellStyle.Font,
                rectangle,
                dgvResults.RowHeadersDefaultCellStyle.ForeColor,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        private void tsMenuDelete_Click(object sender, EventArgs e)
        {
            if (tvQuery.SelectedNode != null)
            {
                if (tvQuery.SelectedNode.Level == 1)
                {
                    string strSQL = "delete from QueryDetail where TableID='" + tvQuery.SelectedNode.Tag.ToString() + "' and QueryID='" + tvQuery.SelectedNode.Name + "'";
                    if (DB.ExecuteNonQuery(strSQL) > 0)
                    {
                        tvQuery.SelectedNode.Remove();
                    }
                }
            }
        }

        private void tsMenuRename_Click(object sender, EventArgs e)
        {
            if (tvQuery.SelectedNode != null)
            {
                tvQuery.LabelEdit = true;
                tvQuery.SelectedNode.BeginEdit();
            }
        }

        /// <summary>
        /// 保存查询快照的重命名
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvQuery_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            tvQuery.LabelEdit = false;
            if (e.Label == null || e.Label.Trim().Equals(""))
            {
                e.CancelEdit = true;
                return;
            }
            if (tvQuery.SelectedNode.Level == 0)
            {
                string strSQL = "update QueryHead set tbNickName='" + e.Label + "' where TableID='" + tvQuery.SelectedNode.Tag.ToString() + "'";
                if (DB.ExecuteNonQuery(strSQL) <=0)
                {
                    e.CancelEdit = true;
                }
            }
            else if (tvQuery.SelectedNode.Level == 1)
            {
                string strSQL = "update QueryDetail set QueryName='" + e.Label + "' where TableID='" + tvQuery.SelectedNode.Tag.ToString() + "' and QueryID='" + tvQuery.SelectedNode.Name + "'";
                if (DB.ExecuteNonQuery(strSQL) <= 0)
                {
                    e.CancelEdit = true;
                }
            }
        }

        private void 生成报表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(dgvResults.SelectedRows.Count<=0) return ;
            string strID = dgvResults.SelectedRows[0].Cells[0].Value.ToString().Trim();
            if(strID=="") return ;

            Process inner_proc = new Process();
            inner_proc.StartInfo.Arguments = "-AUTO TEMPLATE_ID=1;FILE=XDS_MLTR.xls;REPORT_ID=1;电机序号=" + strID;
            inner_proc.StartInfo.FileName = "ZWReport.exe";
            inner_proc.Start();
        }

        private void contextMenuStrip2_Opening(object sender, CancelEventArgs e)
        {
            if (dgvResults.SelectedRows.Count <= 0 || tvQuery.SelectedNode.Parent.Index != 0)
                contextMenuStrip2.Enabled = false;
            else
                contextMenuStrip2.Enabled = true;
        }

    }//End of Class.
}
