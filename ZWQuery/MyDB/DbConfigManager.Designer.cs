namespace MyDB
{
    partial class DbConfigManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DbConfigManager));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbOracle = new System.Windows.Forms.RadioButton();
            this.rbMySql = new System.Windows.Forms.RadioButton();
            this.rbSqlServerCe = new System.Windows.Forms.RadioButton();
            this.rbSqlServer = new System.Windows.Forms.RadioButton();
            this.lblServer = new System.Windows.Forms.Label();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnTestConnection = new System.Windows.Forms.Button();
            this.cboDatabase = new System.Windows.Forms.ComboBox();
            this.lblDatabase = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtUserId = new System.Windows.Forms.TextBox();
            this.lblUserId = new System.Windows.Forms.Label();
            this.chkWindowsAuthentication = new System.Windows.Forms.CheckBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.lblPort = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.lnkDefault = new System.Windows.Forms.LinkLabel();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbOracle);
            this.groupBox1.Controls.Add(this.rbMySql);
            this.groupBox1.Controls.Add(this.rbSqlServerCe);
            this.groupBox1.Controls.Add(this.rbSqlServer);
            this.groupBox1.Location = new System.Drawing.Point(143, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(379, 57);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Database Type";
            // 
            // rbOracle
            // 
            this.rbOracle.AutoSize = true;
            this.rbOracle.Enabled = false;
            this.rbOracle.Location = new System.Drawing.Point(277, 25);
            this.rbOracle.Name = "rbOracle";
            this.rbOracle.Size = new System.Drawing.Size(59, 16);
            this.rbOracle.TabIndex = 3;
            this.rbOracle.Text = "Oracle";
            this.rbOracle.UseVisualStyleBackColor = true;
            this.rbOracle.CheckedChanged += new System.EventHandler(this.rbOracle_CheckedChanged);
            // 
            // rbMySql
            // 
            this.rbMySql.AutoSize = true;
            this.rbMySql.Location = new System.Drawing.Point(218, 25);
            this.rbMySql.Name = "rbMySql";
            this.rbMySql.Size = new System.Drawing.Size(53, 16);
            this.rbMySql.TabIndex = 2;
            this.rbMySql.Text = "MySQL";
            this.rbMySql.UseVisualStyleBackColor = true;
            this.rbMySql.CheckedChanged += new System.EventHandler(this.rbMySql_CheckedChanged);
            // 
            // rbSqlServerCe
            // 
            this.rbSqlServerCe.AutoSize = true;
            this.rbSqlServerCe.Location = new System.Drawing.Point(108, 25);
            this.rbSqlServerCe.Name = "rbSqlServerCe";
            this.rbSqlServerCe.Size = new System.Drawing.Size(101, 16);
            this.rbSqlServerCe.TabIndex = 1;
            this.rbSqlServerCe.Text = "SQL Server CE";
            this.rbSqlServerCe.UseVisualStyleBackColor = true;
            this.rbSqlServerCe.CheckedChanged += new System.EventHandler(this.rbSqlServerCe_CheckedChanged);
            // 
            // rbSqlServer
            // 
            this.rbSqlServer.AutoSize = true;
            this.rbSqlServer.Checked = true;
            this.rbSqlServer.Location = new System.Drawing.Point(19, 25);
            this.rbSqlServer.Name = "rbSqlServer";
            this.rbSqlServer.Size = new System.Drawing.Size(83, 16);
            this.rbSqlServer.TabIndex = 0;
            this.rbSqlServer.TabStop = true;
            this.rbSqlServer.Text = "SQL Server";
            this.rbSqlServer.UseVisualStyleBackColor = true;
            this.rbSqlServer.CheckedChanged += new System.EventHandler(this.rbSqlServer_CheckedChanged);
            // 
            // lblServer
            // 
            this.lblServer.AutoSize = true;
            this.lblServer.Location = new System.Drawing.Point(17, 32);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(47, 12);
            this.lblServer.TabIndex = 0;
            this.lblServer.Text = "Server:";
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(82, 29);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(189, 21);
            this.txtServer.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(125, 279);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnTestConnection);
            this.groupBox2.Controls.Add(this.cboDatabase);
            this.groupBox2.Controls.Add(this.lblDatabase);
            this.groupBox2.Controls.Add(this.txtPassword);
            this.groupBox2.Controls.Add(this.lblPassword);
            this.groupBox2.Controls.Add(this.txtUserId);
            this.groupBox2.Controls.Add(this.lblUserId);
            this.groupBox2.Controls.Add(this.chkWindowsAuthentication);
            this.groupBox2.Controls.Add(this.txtPort);
            this.groupBox2.Controls.Add(this.lblPort);
            this.groupBox2.Controls.Add(this.txtServer);
            this.groupBox2.Controls.Add(this.lblServer);
            this.groupBox2.Location = new System.Drawing.Point(143, 84);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(379, 207);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Connection Parameters";
            // 
            // btnTestConnection
            // 
            this.btnTestConnection.Location = new System.Drawing.Point(286, 165);
            this.btnTestConnection.Name = "btnTestConnection";
            this.btnTestConnection.Size = new System.Drawing.Size(78, 25);
            this.btnTestConnection.TabIndex = 11;
            this.btnTestConnection.Text = "Test";
            this.btnTestConnection.UseVisualStyleBackColor = true;
            this.btnTestConnection.Click += new System.EventHandler(this.btnTestConnection_Click);
            // 
            // cboDatabase
            // 
            this.cboDatabase.FormattingEnabled = true;
            this.cboDatabase.Location = new System.Drawing.Point(82, 137);
            this.cboDatabase.Name = "cboDatabase";
            this.cboDatabase.Size = new System.Drawing.Size(189, 20);
            this.cboDatabase.TabIndex = 10;
            // 
            // lblDatabase
            // 
            this.lblDatabase.AutoSize = true;
            this.lblDatabase.Location = new System.Drawing.Point(17, 140);
            this.lblDatabase.Name = "lblDatabase";
            this.lblDatabase.Size = new System.Drawing.Size(59, 12);
            this.lblDatabase.TabIndex = 9;
            this.lblDatabase.Text = "Database:";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(82, 110);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(189, 21);
            this.txtPassword.TabIndex = 8;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(17, 113);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(59, 12);
            this.lblPassword.TabIndex = 7;
            this.lblPassword.Text = "Password:";
            // 
            // txtUserId
            // 
            this.txtUserId.Location = new System.Drawing.Point(82, 83);
            this.txtUserId.Name = "txtUserId";
            this.txtUserId.Size = new System.Drawing.Size(189, 21);
            this.txtUserId.TabIndex = 6;
            // 
            // lblUserId
            // 
            this.lblUserId.AutoSize = true;
            this.lblUserId.Location = new System.Drawing.Point(17, 86);
            this.lblUserId.Name = "lblUserId";
            this.lblUserId.Size = new System.Drawing.Size(53, 12);
            this.lblUserId.TabIndex = 5;
            this.lblUserId.Text = "User ID:";
            // 
            // chkWindowsAuthentication
            // 
            this.chkWindowsAuthentication.AutoSize = true;
            this.chkWindowsAuthentication.Location = new System.Drawing.Point(82, 61);
            this.chkWindowsAuthentication.Name = "chkWindowsAuthentication";
            this.chkWindowsAuthentication.Size = new System.Drawing.Size(156, 16);
            this.chkWindowsAuthentication.TabIndex = 4;
            this.chkWindowsAuthentication.Text = "Windows Authentication";
            this.chkWindowsAuthentication.UseVisualStyleBackColor = true;
            this.chkWindowsAuthentication.CheckedChanged += new System.EventHandler(this.chkWindowsAuthentication_CheckedChanged);
            // 
            // txtPort
            // 
            this.txtPort.Enabled = false;
            this.txtPort.Location = new System.Drawing.Point(318, 29);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(46, 21);
            this.txtPort.TabIndex = 3;
            this.txtPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPort_KeyPress);
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(277, 32);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(35, 12);
            this.lblPort.TabIndex = 2;
            this.lblPort.Text = "Port:";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(343, 315);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(78, 25);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(429, 315);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(78, 25);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lnkDefault
            // 
            this.lnkDefault.AutoSize = true;
            this.lnkDefault.Location = new System.Drawing.Point(49, 321);
            this.lnkDefault.Name = "lnkDefault";
            this.lnkDefault.Size = new System.Drawing.Size(47, 12);
            this.lnkDefault.TabIndex = 6;
            this.lnkDefault.TabStop = true;
            this.lnkDefault.Text = "Default";
            this.lnkDefault.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkDefault_LinkClicked);
            // 
            // DbConfigManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 352);
            this.Controls.Add(this.lnkDefault);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "DbConfigManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Database Configuration";
            this.Load += new System.EventHandler(this.DbConfigManager_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbSqlServer;
        private System.Windows.Forms.RadioButton rbOracle;
        private System.Windows.Forms.RadioButton rbMySql;
        private System.Windows.Forms.RadioButton rbSqlServerCe;
        private System.Windows.Forms.Label lblServer;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.CheckBox chkWindowsAuthentication;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtUserId;
        private System.Windows.Forms.Label lblUserId;
        private System.Windows.Forms.ComboBox cboDatabase;
        private System.Windows.Forms.Label lblDatabase;
        private System.Windows.Forms.Button btnTestConnection;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.LinkLabel lnkDefault;
    }
}