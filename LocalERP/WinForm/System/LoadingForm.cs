using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LocalERP.WinForm
{
    public partial class LoadingForm : Form
    {
        public LoadingForm()
        {
            InitializeComponent();
        }

        private delegate void SetTextHandler(string text);
        public void SetText(string text)
        {
            if (this.label2.InvokeRequired)
            {
                this.Invoke(new SetTextHandler(SetText), text);
            }
            else
            {
                if (text == "close")
                    this.Close();
                else
                    this.label2.Text = text;

                //this.label1.Refresh();
            }
        }
    }
}