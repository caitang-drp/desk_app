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

            addRow();
        }

        private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel linkLabel = sender as LinkLabel;
            mainForm.setForm(linkLabel.Text);
        }

        private void addRow() {
            Label label = new System.Windows.Forms.Label();
            
            label.Font = new System.Drawing.Font("宋体", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            label.Image = global::LocalERP.Properties.Resources.Yes;
            label.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            label.Location = new System.Drawing.Point(74, 55);
            label.Name = "label1";
            label.Size = new System.Drawing.Size(100, 18);
            label.TabIndex = 58;
            label.Text = "采购管理";
            label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            this.Controls.Add(label);
        }
    }
}