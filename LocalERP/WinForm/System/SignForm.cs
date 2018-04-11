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
    public partial class SignForm : Form
    {
        public SignForm()
        {
            InitializeComponent();
            this.textBox1.Text = AuthUtility.getCPU();
            this.toolStripStatusLabel1.Text = ConfUtility.GetSoftName();
            this.label4.Text = ConfUtility.GetProductName();
        }

        /// <summary>
        /// get value from control with valiadating
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private bool checkAndUpdateSN()
        {
            if (string.IsNullOrEmpty(this.textBox_ps.Text) || AuthUtility.checkSN(this.textBox_ps.Text) == false)
            {
                this.errorProvider1.SetError(this.textBox_ps, "ע����Ϊ�ջ����!");
                return false;
            }
            else
            {
                this.errorProvider1.SetError(this.textBox_ps, string.Empty);
                string newSn = this.textBox_ps.Text;

                string oldSn = ConfDao.getInstance().Get(2);
                if(oldSn != null && oldSn.Trim() != "")
                    newSn = oldSn.Trim() + "/" + newSn;
                ConfDao.getInstance().Update(2, newSn);
                return true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.checkAndUpdateSN())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}