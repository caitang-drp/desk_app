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
    public partial class BackupForm : Form
    {
        public BackupForm()
        {
            InitializeComponent();
            this.textBox_oldPs.Text = ConfUtility.GetBackupPath();
        }

        /// <summary>
        /// event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            string path = null;
            if (ValidateUtility.getName(this.textBox_oldPs, this.errorProvider1, out path) == false)
                return;
            ConfDao.getInstance().Update(13, path);
            MessageBox.Show("����·���ɹ�!", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
            
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "��ѡ�񱸷�·��";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    MessageBox.Show(this, "·������Ϊ��", "��ʾ");
                    return;
                }
                else {
                    this.textBox_oldPs.Text = dialog.SelectedPath;
                }
            }
        }
    }
}