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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();

            this.label_name.Text = ConfDao.getInstance().Get(3);

            this.toolStripStatusLabel1.Text = ConfUtility.GetProductNameWithCopyRight();
        }

        /// <summary>
        /// get value from control with valiadating
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private bool getPs(out string ps)
        {
            if (string.IsNullOrEmpty(this.textBox_ps.Text) || 
                !this.textBox_ps.Text.Equals(ConfDao.getInstance().Get(1)))
            {
                this.errorProvider1.SetError(this.textBox_ps, " ‰»ÎŒ™ø’ªÚ¥ÌŒÛ!");
                ps = "";
                return false;
            }
            else
            {
                this.errorProvider1.SetError(this.textBox_ps, string.Empty);
                ps = this.textBox_ps.Text;
                return true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ps;
            if (this.getPs(out ps))
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}