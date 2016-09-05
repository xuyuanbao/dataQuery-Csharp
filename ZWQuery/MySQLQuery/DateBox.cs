
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ZWGPTS
{
    public partial class DateBox : Form
    {
        public string DateString = "";

        public DateBox()
        {
            InitializeComponent();
            this.DialogResult = DialogResult.None;
            DateString = "";
        }
       
        private void btnOK_Click(object sender, EventArgs e)
        {
            DateString = " '"+ mcSelect.SelectionStart.ToString("yyyy-MM-dd")+ "' ";
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
