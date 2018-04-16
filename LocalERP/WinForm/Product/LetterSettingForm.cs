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
using gregn6Lib;

namespace LocalERP.WinForm
{
    public partial class LetterSettingForm : Form
    {
        private GridppReport Report = new GridppReport();
        private ProductCirculation cir;
        private string pieces;
        public LetterSettingForm(ProductCirculation c, string p)
        {
            InitializeComponent();
            cir = c;
            pieces = p;
            this.textBox_timeFormat.Text = ConfDao.getInstance().Get(12);
            if (string.IsNullOrEmpty(this.textBox_timeFormat.Text) || this.textBox_timeFormat.Text == "0")
                resetTime();

            if (cir.CustomerID != 0)
            {
                Customer customer = CustomerDao.getInstance().FindByID(cir.CustomerID);
                this.textBox_name.Text = customer.Name;
                this.textBox_address.Text = customer.Address;
                this.textBox_tel.Text = customer.Contract;

                this.textBox_contractor.Text = ConfDao.getInstance().Get(5);
                this.textBox_contractorPhone.Text = ConfUtility.getContract();
            }
            this.textBox_pieces.Text = pieces;
            
            try
            {
                this.textBox_time.Text = cir.CirculationTime.ToString(this.textBox_timeFormat.Text);
            }
            catch {
                resetTime();
                this.textBox_time.Text = cir.CirculationTime.ToString(this.textBox_timeFormat.Text);
            }
            this.textBox_comment.Text = cir.Comment;
        }

        private void resetTime() {
            this.textBox_timeFormat.Text = "yyyy MM dd";
            ConfDao.getInstance().Update(12, this.textBox_timeFormat.Text);
        }

        private void ReportFetchData()
        {
            if (Report.ControlByName("StaticBox_address") != null)
                Report.ControlByName("StaticBox_address").AsStaticBox.Text = this.textBox_address.Text;

            if (Report.ControlByName("StaticBox_name") != null)
                Report.ControlByName("StaticBox_name").AsStaticBox.Text = this.textBox_name.Text;
            
            if (Report.ControlByName("StaticBox_tel") != null)
                Report.ControlByName("StaticBox_tel").AsStaticBox.Text = this.textBox_tel.Text;
            
            if (Report.ControlByName("StaticBox_comment") != null)
                Report.ControlByName("StaticBox_comment").AsStaticBox.Text = this.textBox_comment.Text;

            if (Report.ControlByName("StaticBox_pieces") != null)
                Report.ControlByName("StaticBox_pieces").AsStaticBox.Text = this.textBox_pieces.Text;

            if (Report.ControlByName("StaticBox_contractor") != null)
                Report.ControlByName("StaticBox_contractor").AsStaticBox.Text = this.textBox_contractor.Text;

            if (Report.ControlByName("StaticBox_contractorPhone") != null)
                Report.ControlByName("StaticBox_contractorPhone").AsStaticBox.Text = this.textBox_contractorPhone.Text;

            if (Report.ControlByName("StaticBox_time") != null)
                Report.ControlByName("StaticBox_time").AsStaticBox.Text = this.textBox_time.Text;

        }
        private void button3_Click(object sender, EventArgs e)
        {
            // 载入报表模板数据
            string report_template_path = ConfUtility.letter_report_path;
            Report.LoadFromFile(report_template_path);
            //一定要先-=，要不会重复数据
            Report.FetchRecord -= new _IGridppReportEvents_FetchRecordEventHandler(ReportFetchData);
            Report.FetchRecord += new _IGridppReportEvents_FetchRecordEventHandler(ReportFetchData);
            // 打印预览
            Report.PrintPreview(true);
        }

        //打开表格
        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(ConfUtility.letter_report_path);
        }

        //保存字段设置
        private void button2_Click(object sender, EventArgs e)
        {
            ConfDao.getInstance().Update(12, this.textBox_timeFormat.Text);
            this.textBox_time.Text = cir.CirculationTime.ToString(this.textBox_timeFormat.Text);
            MessageBox.Show("保存成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }
}