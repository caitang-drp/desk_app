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
    public partial class CirSettingForm : Form
    {
        public CirSettingForm()
        {
            InitializeComponent();

            string serialType = ConfUtility.GetSerialType();
            //string可以用==，String就不行？
            if (serialType == "serialType2")
                this.radioButton2.Checked = true;
            else
                this.radioButton1.Checked = true;

            string backFreight = ConfUtility.GetBackFreightOpen();
            if (backFreight == "backFreightOpen")
                this.checkBox_backFreight.Checked = true;

            string printLetter = ConfUtility.GetPrintLetterOpen();
            if (printLetter == "printLetterOpen")
                this.checkBox_printLetter.Checked = true;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            ConfDao.getInstance().Update(19, this.radioButton2.Checked == true?"serialType2":"serialType1");
            ConfDao.getInstance().Update(18, this.checkBox_backFreight.Checked == true ? "backFreightOpen" : "backFreightClose");
            ConfDao.getInstance().Update(17, this.checkBox_printLetter.Checked == true ? "printLetterOpen" : "printLetterClose");

            MessageBox.Show("保存成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}