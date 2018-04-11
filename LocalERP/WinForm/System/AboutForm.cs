using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LocalERP.DataAccess.DataDAO;
using LocalERP.DataAccess.Data;
using LocalERP.DataAccess.Utility;

namespace LocalERP.WinForm
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();

            this.label1.Text = ConfUtility.GetProductName();
            this.label2.Text = ConfUtility.GetSoftName();
        }
    }
}