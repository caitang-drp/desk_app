using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LocalERP.DataAccess.DataDAO;
using LocalERP.DataAccess.Data;

namespace LocalERP.WinForm
{
    public partial class PasswordForm : Form
    {
        public PasswordForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// get value from control with valiadating
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private bool getOldPs(out string oldPs)
        {
            if (string.IsNullOrEmpty(this.textBox_oldPs.Text) || 
                !this.textBox_oldPs.Text.Equals(ConfDao.getInstance().Get(1)))
            {
                this.errorProvider1.SetError(this.textBox_oldPs, "����Ϊ�ջ����!");
                oldPs = "";
                return false;
            }
            else
            {
                this.errorProvider1.SetError(this.textBox_oldPs, string.Empty);
                oldPs = this.textBox_oldPs.Text;
                return true;
            }
        }

        private bool getNewPs(out string newPs) {
            if (string.IsNullOrEmpty(this.textBox_newPs.Text))
            {
                this.errorProvider1.SetError(this.textBox_newPs, "���벻��Ϊ��!");
                newPs = "";
                return false;
            }
            else
            {
                this.errorProvider1.SetError(this.textBox_newPs, string.Empty);
                newPs = this.textBox_newPs.Text;
                return true;
            }
        }

        private bool getNewPsAgain(out string newPs)
        {
            if (string.IsNullOrEmpty(this.textBox_newPsAgain.Text) || !this.textBox_newPs.Text.Equals(this.textBox_newPsAgain.Text))
            {
                this.errorProvider1.SetError(this.textBox_newPsAgain, "���벻һ��!");
                newPs = "";
                return false;
            }
            else
            {
                this.errorProvider1.SetError(this.textBox_newPsAgain, string.Empty);
                newPs = this.textBox_newPsAgain.Text;
                return true;
            }
        }

        /// <summary>
        /// event for validating
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_oldPs_Validating(object sender, CancelEventArgs e)
        {
            string name;
            this.getOldPs(out name);
        }

        /// <summary>
        /// event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            string ps;
            if (this.getOldPs(out ps)
            && this.getNewPs(out ps)
            && this.getNewPsAgain(out ps)) {
                ConfDao.getInstance().Update(1, ps);
                MessageBox.Show("��������ɹ�!", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}