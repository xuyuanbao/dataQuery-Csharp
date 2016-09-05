namespace ZWGPTS
{
    partial class FormQuery
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormQuery));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tvQuery = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsMenuRename = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dgvConditions = new System.Windows.Forms.DataGridView();
            this.colColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colNickName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOutput = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colOperator = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colConditions = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSortType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colChart = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.txtSQL = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgvResults = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.生成报表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.zgcChart = new ZedGraph.ZedGraphControl();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tbQuery = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tbSave = new System.Windows.Forms.ToolStripButton();
            this.ts4 = new System.Windows.Forms.ToolStripSeparator();
            this.tbExport = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tbExit = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConditions)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).BeginInit();
            this.contextMenuStrip2.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 58);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tvQuery);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(872, 504);
            this.splitContainer1.SplitterDistance = 141;
            this.splitContainer1.TabIndex = 0;
            // 
            // tvQuery
            // 
            this.tvQuery.ContextMenuStrip = this.contextMenuStrip1;
            this.tvQuery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvQuery.FullRowSelect = true;
            this.tvQuery.HideSelection = false;
            this.tvQuery.ImageIndex = 0;
            this.tvQuery.ImageList = this.imageList1;
            this.tvQuery.ItemHeight = 20;
            this.tvQuery.Location = new System.Drawing.Point(0, 0);
            this.tvQuery.Name = "tvQuery";
            this.tvQuery.SelectedImageIndex = 0;
            this.tvQuery.ShowNodeToolTips = true;
            this.tvQuery.Size = new System.Drawing.Size(141, 504);
            this.tvQuery.TabIndex = 0;
            this.tvQuery.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.tvQuery_AfterLabelEdit);
            this.tvQuery.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvQuery_NodeMouseClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsMenuRename,
            this.tsMenuDelete});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(107, 48);
            // 
            // tsMenuRename
            // 
            this.tsMenuRename.Name = "tsMenuRename";
            this.tsMenuRename.Size = new System.Drawing.Size(106, 22);
            this.tsMenuRename.Text = "重命名";
            this.tsMenuRename.Click += new System.EventHandler(this.tsMenuRename_Click);
            // 
            // tsMenuDelete
            // 
            this.tsMenuDelete.Name = "tsMenuDelete";
            this.tsMenuDelete.Size = new System.Drawing.Size(106, 22);
            this.tsMenuDelete.Text = "删除";
            this.tsMenuDelete.Click += new System.EventHandler(this.tsMenuDelete_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Icos_Table.ico");
            this.imageList1.Images.SetKeyName(1, "Icos_Query.ico");
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.dgvConditions);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.txtSQL);
            this.splitContainer2.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer2.Size = new System.Drawing.Size(727, 504);
            this.splitContainer2.SplitterDistance = 116;
            this.splitContainer2.TabIndex = 0;
            // 
            // dgvConditions
            // 
            this.dgvConditions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvConditions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colColumn,
            this.colNickName,
            this.colOutput,
            this.colOperator,
            this.colConditions,
            this.colSortType,
            this.colChart});
            this.dgvConditions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvConditions.Location = new System.Drawing.Point(0, 0);
            this.dgvConditions.Name = "dgvConditions";
            this.dgvConditions.RowTemplate.Height = 23;
            this.dgvConditions.Size = new System.Drawing.Size(727, 116);
            this.dgvConditions.TabIndex = 1;
            this.dgvConditions.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvConditions_CellBeginEdit);
            this.dgvConditions.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvConditions_CellClick);
            this.dgvConditions.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvConditions_CellEndEdit);
            // 
            // colColumn
            // 
            this.colColumn.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.colColumn.HeaderText = "列";
            this.colColumn.Items.AddRange(new object[] {
            "*"});
            this.colColumn.Name = "colColumn";
            this.colColumn.Width = 87;
            // 
            // colNickName
            // 
            this.colNickName.HeaderText = "别名";
            this.colNickName.Name = "colNickName";
            // 
            // colOutput
            // 
            this.colOutput.FalseValue = "0";
            this.colOutput.HeaderText = "输出";
            this.colOutput.IndeterminateValue = "2";
            this.colOutput.Name = "colOutput";
            this.colOutput.TrueValue = "1";
            // 
            // colOperator
            // 
            this.colOperator.HeaderText = "运算符";
            this.colOperator.Name = "colOperator";
            // 
            // colConditions
            // 
            this.colConditions.HeaderText = "筛选条件";
            this.colConditions.Name = "colConditions";
            this.colConditions.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colConditions.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colSortType
            // 
            this.colSortType.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.colSortType.HeaderText = "排序类型";
            this.colSortType.Items.AddRange(new object[] {
            "升序",
            "降序",
            "未排序"});
            this.colSortType.Name = "colSortType";
            // 
            // colChart
            // 
            this.colChart.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.colChart.HeaderText = "图表";
            this.colChart.Items.AddRange(new object[] {
            "柱形图-分类(X)轴标志",
            "折线图-分类(X)轴标志",
            "饼图-分类(X)轴标志",
            "系列值"});
            this.colChart.Name = "colChart";
            // 
            // txtSQL
            // 
            this.txtSQL.BackColor = System.Drawing.SystemColors.Window;
            this.txtSQL.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtSQL.ForeColor = System.Drawing.Color.DimGray;
            this.txtSQL.Location = new System.Drawing.Point(0, 0);
            this.txtSQL.Multiline = true;
            this.txtSQL.Name = "txtSQL";
            this.txtSQL.ReadOnly = true;
            this.txtSQL.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSQL.Size = new System.Drawing.Size(727, 54);
            this.txtSQL.TabIndex = 2;
            this.txtSQL.Text = "1\r\n2\r\n3\r\n4";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(0, 56);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(727, 328);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvResults);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(719, 302);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "数据";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgvResults
            // 
            this.dgvResults.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgvResults.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResults.ContextMenuStrip = this.contextMenuStrip2;
            this.dgvResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvResults.Location = new System.Drawing.Point(3, 3);
            this.dgvResults.Name = "dgvResults";
            this.dgvResults.RowTemplate.Height = 23;
            this.dgvResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvResults.Size = new System.Drawing.Size(713, 296);
            this.dgvResults.TabIndex = 0;
            this.dgvResults.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvResults_CellBeginEdit);
            this.dgvResults.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvResults_RowPostPaint);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.生成报表ToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(153, 48);
            this.contextMenuStrip2.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip2_Opening);
            // 
            // 生成报表ToolStripMenuItem
            // 
            this.生成报表ToolStripMenuItem.Name = "生成报表ToolStripMenuItem";
            this.生成报表ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.生成报表ToolStripMenuItem.Text = "生成报表";
            this.生成报表ToolStripMenuItem.Click += new System.EventHandler(this.生成报表ToolStripMenuItem_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.zgcChart);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(719, 302);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "图表";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // zgcChart
            // 
            this.zgcChart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.zgcChart.Location = new System.Drawing.Point(3, 52);
            this.zgcChart.Name = "zgcChart";
            this.zgcChart.ScrollGrace = 0D;
            this.zgcChart.ScrollMaxX = 0D;
            this.zgcChart.ScrollMaxY = 0D;
            this.zgcChart.ScrollMaxY2 = 0D;
            this.zgcChart.ScrollMinX = 0D;
            this.zgcChart.ScrollMinY = 0D;
            this.zgcChart.ScrollMinY2 = 0D;
            this.zgcChart.Size = new System.Drawing.Size(713, 285);
            this.zgcChart.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbQuery,
            this.toolStripSeparator2,
            this.tbSave,
            this.ts4,
            this.tbExport,
            this.toolStripSeparator3,
            this.tbExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(872, 55);
            this.toolStrip1.Stretch = true;
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tbQuery
            // 
            this.tbQuery.AutoSize = false;
            this.tbQuery.Image = ((System.Drawing.Image)(resources.GetObject("tbQuery.Image")));
            this.tbQuery.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.tbQuery.ImageTransparentColor = System.Drawing.Color.Fuchsia;
            this.tbQuery.Name = "tbQuery";
            this.tbQuery.Size = new System.Drawing.Size(60, 52);
            this.tbQuery.Text = "查询";
            this.tbQuery.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.tbQuery.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.tbQuery.Click += new System.EventHandler(this.tbQuery_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 55);
            // 
            // tbSave
            // 
            this.tbSave.AutoSize = false;
            this.tbSave.Image = ((System.Drawing.Image)(resources.GetObject("tbSave.Image")));
            this.tbSave.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.tbSave.ImageTransparentColor = System.Drawing.Color.Fuchsia;
            this.tbSave.Name = "tbSave";
            this.tbSave.Size = new System.Drawing.Size(60, 52);
            this.tbSave.Text = "保存快照";
            this.tbSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.tbSave.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.tbSave.Click += new System.EventHandler(this.tbSave_Click);
            // 
            // ts4
            // 
            this.ts4.Name = "ts4";
            this.ts4.Size = new System.Drawing.Size(6, 55);
            // 
            // tbExport
            // 
            this.tbExport.AutoSize = false;
            this.tbExport.Image = ((System.Drawing.Image)(resources.GetObject("tbExport.Image")));
            this.tbExport.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.tbExport.ImageTransparentColor = System.Drawing.Color.Fuchsia;
            this.tbExport.Name = "tbExport";
            this.tbExport.Size = new System.Drawing.Size(60, 52);
            this.tbExport.Text = "导出数据";
            this.tbExport.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.tbExport.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.tbExport.Click += new System.EventHandler(this.tbExport_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 55);
            // 
            // tbExit
            // 
            this.tbExit.AutoSize = false;
            this.tbExit.Image = ((System.Drawing.Image)(resources.GetObject("tbExit.Image")));
            this.tbExit.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.tbExit.ImageTransparentColor = System.Drawing.Color.Fuchsia;
            this.tbExit.Name = "tbExit";
            this.tbExit.Size = new System.Drawing.Size(60, 52);
            this.tbExit.Text = "退出";
            this.tbExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.tbExit.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.tbExit.Click += new System.EventHandler(this.tbExit_Click);
            // 
            // FormQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(872, 562);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormQuery";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据查询系统";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormQuery_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvConditions)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).EndInit();
            this.contextMenuStrip2.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridView dgvResults;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tbQuery;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tbSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tbExit;
        private System.Windows.Forms.ToolStripButton tbExport;
        private System.Windows.Forms.DataGridView dgvConditions;
        private System.Windows.Forms.TreeView tvQuery;
        private System.Windows.Forms.ToolStripSeparator ts4;
        private System.Windows.Forms.TextBox txtSQL;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsMenuRename;
        private System.Windows.Forms.ToolStripMenuItem tsMenuDelete;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private ZedGraph.ZedGraphControl zgcChart;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.DataGridViewComboBoxColumn colColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNickName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colOutput;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOperator;
        private System.Windows.Forms.DataGridViewTextBoxColumn colConditions;
        private System.Windows.Forms.DataGridViewComboBoxColumn colSortType;
        private System.Windows.Forms.DataGridViewComboBoxColumn colChart;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem 生成报表ToolStripMenuItem;
    }
}