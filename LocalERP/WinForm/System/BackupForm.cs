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
            MessageBox.Show("保存路径成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
            
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择备份路径";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    MessageBox.Show(this, "路径不能为空", "提示");
                    return;
                }
                else {
                    this.textBox_oldPs.Text = dialog.SelectedPath;
                }
            }
        }
    }
}