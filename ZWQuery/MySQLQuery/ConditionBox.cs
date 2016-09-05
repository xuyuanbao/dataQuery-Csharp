/******************************************
 *  junjie
 *  2014-10-20
******************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ZWGPTS
{
    public partial class ConditionBox : Form
    {
        private string _condition = "";
        public string Condition 
        {
            get 
            {
                if (_condition == " '' ")
                    _condition = "";
                return _condition;
            }
            set
            {
                _condition = value;
                if (_condition == " '' ")
                    _condition = "";
            }
        }

        public string InputOperator
        {
            get;
            set;
        }

        public string InputDataType
        {
            get;
            set;
        }

        public ConditionBox(string column)
        {
            InitializeComponent();
            this.DialogResult = DialogResult.None;
            lblColumn.Text = column;
            SetValueComboBox(false);
        }

        public ConditionBox(string column, string[] values)
        {
            InitializeComponent();
            this.DialogResult = DialogResult.None;
            lblColumn.Text = column;
            SetValueComboBox(false);

            cboValue.MaxDropDownItems = 10;
            cboValue2.MaxDropDownItems = 10;
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] != null)
                {
                    cboValue.Items.Add(values[i]);
                    cboValue2.Items.Add(values[i]);
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Condition = GetCondition(InputOperator);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }



        private void SetValueComboBox(bool showValue2)
        {
            cboValue.Visible = true;
            lblValue2.Visible = showValue2;
            cboValue2.Visible = showValue2;

            if (showValue2)
            {
                cboValue.Width = cboValue2.Width;
            }
            else
            {
                cboValue.Width = 347;
            }
        }

        private string GetCondition(string strOperator)
        {
            switch (strOperator)
            {
                case "between":
                case "not between":
                    return " '" + cboValue.Text.Trim() + "' and '" + cboValue2.Text.Trim() + "' ";
                case "like":
                case "not like":
                    return  " '" + cboValue.Text.Trim() + "' ";
                default:
                    return  " '" + cboValue.Text.Trim() + "' ";
            }
        }

        private void ConditionBox_Load(object sender, EventArgs e)
        {
            if (InputDataType == "DATETIME")
            {
                SetValueComboBox(true);
            }
        }

    }
}
