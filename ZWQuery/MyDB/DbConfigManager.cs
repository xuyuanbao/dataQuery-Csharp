using System;
using System.IO;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
//using System.Data.SqlServerCe;
//using MySql.Data;
//using MySql.Data.MySqlClient;

namespace MyDB
{
    public partial class DbConfigManager : Form
    {
        private DbConfig Config = new DbConfig();

        public DbConfigManager()
        {
            InitializeComponent();
            this.DialogResult = DialogResult.None;
        }

        private void DbConfigManager_Load(object sender, EventArgs e)
        {
            LoadConfig();
        }

        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            try
            {
                Config.ServerType = GetSelectedServerType();
                Config.Server = txtServer.Text.Trim();
                Config.Port =txtPort.Text.Trim();
                Config.WindowsAuthentication = chkWindowsAuthentication.Checked;
                Config.UserId = txtUserId.Text.Trim();
                Config.Password = txtPassword.Text;
                Config.Database = cboDatabase.Text.Trim();

                if (Config.VerifyConnection())
                {
                    MessageBox.Show("Connect database successfully.");
                }
                else
                {
                    MessageBox.Show("Connect database fail.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtPort_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar !='\b')
            {
                e.KeyChar = (char)0;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (SaveConfig())
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void rbSqlServer_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSqlServer.Checked)
            {
                lblServer.Text = "Server:";
                txtServer.Text = "";
                txtPort.Text = "";
                chkWindowsAuthentication.Checked = false;
                txtUserId.Text = "";
                txtPassword.Text = "";
                cboDatabase.Text = "";
                cboDatabase.Items.Clear();

                txtPort.Enabled = false;
                chkWindowsAuthentication.Enabled = true;
                txtUserId.Enabled = true;
                cboDatabase.Enabled = true;
            }
        }

        private void rbSqlServerCe_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSqlServerCe.Checked)
            {
                lblServer.Text = "SDF file:";
                txtServer.Text = "";
                txtPort.Text = "";
                chkWindowsAuthentication.Checked = false;
                txtUserId.Text = "";
                txtPassword.Text = "";
                cboDatabase.Text = "";
                cboDatabase.Items.Clear();

                txtPort.Enabled = false;
                chkWindowsAuthentication.Enabled = false;
                txtUserId.Enabled = false;
                cboDatabase.Enabled = false;
            }
        }

        private void rbMySql_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMySql.Checked)
            {
                lblServer.Text = "Server:";
                txtServer.Text = "";
                txtPort.Text = "3306";
                chkWindowsAuthentication.Checked = false;
                txtUserId.Text = "";
                txtPassword.Text = "";
                cboDatabase.Text = "";
                cboDatabase.Items.Clear();

                txtPort.Enabled = true;
                chkWindowsAuthentication.Enabled = false;
                txtUserId.Enabled = true;
                cboDatabase.Enabled = true;
            }
        }

        private void rbOracle_CheckedChanged(object sender, EventArgs e)
        {
            if (rbOracle.Checked)
            {

            }
        }

        private void chkWindowsAuthentication_CheckedChanged(object sender, EventArgs e)
        {
            txtUserId.Enabled = !chkWindowsAuthentication.Checked;
            txtPassword.Enabled = !chkWindowsAuthentication.Checked;
        }

        private void lnkDefault_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoadConfig();
        }

        private void LoadConfig()
        {
            try
            {
                if (Config.LoadSettings())
                {
                    if (Config.ServerType == DbServerType.SqlServer) rbSqlServer.Checked = true;
                    if (Config.ServerType == DbServerType.SqlServerCe) rbSqlServerCe.Checked = true;
                    if (Config.ServerType == DbServerType.MySql) rbMySql.Checked = true;
                    if (Config.ServerType == DbServerType.Oracle) rbOracle.Checked = true;
                    txtServer.Text = Config.Server;
                    txtPort.Text = Config.Port;
                    chkWindowsAuthentication.Checked = Config.WindowsAuthentication;
                    txtUserId.Text = Config.UserId;
                    txtPassword.Text = Config.Password;
                    cboDatabase.Text = Config.Database;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool SaveConfig()
        {
            bool bSuccessSaved = false;
            try
            {
                Config.ServerType = GetSelectedServerType();
                Config.Server = txtServer.Text.Trim();
                Config.Port = txtPort.Text.Trim();
                Config.WindowsAuthentication = chkWindowsAuthentication.Checked;
                Config.UserId = txtUserId.Text.Trim();
                Config.Password = txtPassword.Text;
                Config.Database = cboDatabase.Text.Trim();

                bSuccessSaved = Config.SaveSettings();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return bSuccessSaved;
        }

        private DbServerType GetSelectedServerType()
        {
            if (rbSqlServer.Checked) return DbServerType.SqlServer;
            if (rbSqlServerCe.Checked) return DbServerType.SqlServerCe;
            if (rbMySql.Checked) return DbServerType.MySql;
            if (rbOracle.Checked) return DbServerType.Oracle;
            rbSqlServer.Checked = true;
            return DbServerType.SqlServer;
        }

    }
}
