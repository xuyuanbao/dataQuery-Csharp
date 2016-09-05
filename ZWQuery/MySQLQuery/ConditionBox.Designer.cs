namespace ZWGPTS
{
    partial class ConditionBox
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblValue = new System.Windows.Forms.Label();
            this.cboValue = new System.Windows.Forms.ComboBox();
            this.lblValue2 = new System.Windows.Forms.Label();
            this.cboValue2 = new System.Windows.Forms.ComboBox();
            this.lblColumnName = new System.Windows.Forms.Label();
            this.lblColumn = new System.Windows.Forms.Label();
            this.lblTip1 = new System.Windows.Forms.Label();
            this.lblTip2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(252, 94);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(77, 30);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(335, 94);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(77, 30);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblValue
            // 
            this.lblValue.AutoSize = true;
            this.lblValue.Location = new System.Drawing.Point(12, 70);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(35, 12);
            this.lblValue.TabIndex = 7;
            this.lblValue.Text = "条件:";
            // 
            // cboValue
            // 
            this.cboValue.FormattingEnabled = true;
            this.cboValue.Location = new System.Drawing.Point(65, 67);
            this.cboValue.Name = "cboValue";
            this.cboValue.Size = new System.Drawing.Size(347, 20);
            this.cboValue.TabIndex = 6;
            // 
            // lblValue2
            // 
            this.lblValue2.AutoSize = true;
            this.lblValue2.Location = new System.Drawing.Point(230, 70);
            this.lblValue2.Name = "lblValue2";
            this.lblValue2.Size = new System.Drawing.Size(17, 12);
            this.lblValue2.TabIndex = 9;
            this.lblValue2.Text = "至";
            this.lblValue2.Visible = false;
            // 
            // cboValue2
            // 
            this.cboValue2.FormattingEnabled = true;
            this.cboValue2.Location = new System.Drawing.Point(252, 67);
            this.cboValue2.Name = "cboValue2";
            this.cboValue2.Size = new System.Drawing.Size(160, 20);
            this.cboValue2.TabIndex = 8;
            this.cboValue2.Visible = false;
            // 
            // lblColumnName
            // 
            this.lblColumnName.AutoSize = true;
            this.lblColumnName.Location = new System.Drawing.Point(12, 18);
            this.lblColumnName.Name = "lblColumnName";
            this.lblColumnName.Size = new System.Drawing.Size(47, 12);
            this.lblColumnName.TabIndex = 10;
            this.lblColumnName.Text = "筛选列:";
            // 
            // lblColumn
            // 
            this.lblColumn.AutoSize = true;
            this.lblColumn.Location = new System.Drawing.Point(65, 18);
            this.lblColumn.Name = "lblColumn";
            this.lblColumn.Size = new System.Drawing.Size(29, 12);
            this.lblColumn.TabIndex = 11;
            this.lblColumn.Text = "列名";
            // 
            // lblTip1
            // 
            this.lblTip1.AutoSize = true;
            this.lblTip1.Location = new System.Drawing.Point(138, 18);
            this.lblTip1.Name = "lblTip1";
            this.lblTip1.Size = new System.Drawing.Size(155, 12);
            this.lblTip1.TabIndex = 12;
            this.lblTip1.Text = "通配符 _ 代表任意单个字符";
            this.lblTip1.Visible = false;
            // 
            // lblTip2
            // 
            this.lblTip2.AutoSize = true;
            this.lblTip2.Location = new System.Drawing.Point(138, 32);
            this.lblTip2.Name = "lblTip2";
            this.lblTip2.Size = new System.Drawing.Size(179, 12);
            this.lblTip2.TabIndex = 13;
            this.lblTip2.Text = "通配符 % 代表任意长度的字符串";
            this.lblTip2.Visible = false;
            // 
            // ConditionBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 136);
            this.Controls.Add(this.lblTip2);
            this.Controls.Add(this.lblTip1);
            this.Controls.Add(this.cboValue);
            this.Controls.Add(this.lblColumn);
            this.Controls.Add(this.lblColumnName);
            this.Controls.Add(this.lblValue2);
            this.Controls.Add(this.cboValue2);
            this.Controls.Add(this.lblValue);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConditionBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "定义筛选条件";
            this.Load += new System.EventHandler(this.ConditionBox_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblValue;
        private System.Windows.Forms.ComboBox cboValue;
        private System.Windows.Forms.Label lblValue2;
        private System.Windows.Forms.ComboBox cboValue2;
        private System.Windows.Forms.Label lblColumnName;
        private System.Windows.Forms.Label lblColumn;
        private System.Windows.Forms.Label lblTip1;
        private System.Windows.Forms.Label lblTip2;
    }
}