
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ZWGPTS
{
    public partial class OperatorBox : Form
    {
        public string Operator = "";

        public OperatorBox()
        {
            InitializeComponent();
            this.DialogResult = DialogResult.None;
            SetOperatorComboBox();
        }
       
        private void btnOK_Click(object sender, EventArgs e)
        {
            Operator = GetOperator();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void cboOperator_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void SetOperatorComboBox()
        {
            cboOperator .Items.Clear ();
            cboOperator.Items.Add("=");
            cboOperator.Items.Add("!=");
            cboOperator.Items.Add(">");
            cboOperator.Items.Add(">=");
            cboOperator.Items.Add("<");
            cboOperator.Items.Add("<=");
            cboOperator.Items.Add("between");
            cboOperator.Items.Add("not between");
            cboOperator.Items.Add("like");
            cboOperator.Items.Add("not like");
            cboOperator.SelectedIndex = 0;
        }

        private string GetOperator()
        {
            return cboOperator.Text.Trim();
        }

    }
}
