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
    public partial class ReportInfoForm : Form
    {
        public ReportInfoForm()
        {
            InitializeComponent();
        }


        private void ReportInfoForm_Load(object sender, EventArgs e)
        {
            DataTable dt = ConfDao.getInstance().GetAll();
            this.textBox_company.Text = dt.Rows[2]["conf"].ToString();
            this.textBox_address.Text = dt.Rows[3]["conf"].ToString();
            this.textBox_contact.Text = dt.Rows[4]["conf"].ToString();
            this.textBox_phone.Text = dt.Rows[5]["conf"].ToString();
            this.textBox_mobile.Text = dt.Rows[6]["conf"].ToString();
            this.textBox_bank.Text = dt.Rows[7]["conf"].ToString();
            this.textBox_other.Text = dt.Rows[8]["conf"].ToString();           
        }

        /// <summary>
        /// get value from control with valiadating
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            ConfDao.getInstance().UpdateCompanyInfo(this.textBox_company.Text, this.textBox_address.Text, this.textBox_contact.Text,
                this.textBox_phone.Text, this.textBox_mobile.Text, this.textBox_bank.Text, this.textBox_other.Text);
                MessageBox.Show("保存信息成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}