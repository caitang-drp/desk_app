using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using LocalERP.DataAccess.DataDAO;
using LocalERP.DataAccess.Data;
using LocalERP.DataAccess.Utility;

namespace LocalERP.WinForm
{
    public partial class WelcomeForm : DockContent
    {
        private MainForm mainForm;
        public WelcomeForm(MainForm form)
        {
            this.mainForm = form;
            InitializeComponent();
        }

        private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel linkLabel = sender as LinkLabel;
            mainForm.setForm(linkLabel.Text);
        }

        private void linkLabel28_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
    }
}