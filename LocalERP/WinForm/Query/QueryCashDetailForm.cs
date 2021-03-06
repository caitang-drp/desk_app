using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using WeifenLuo.WinFormsUI.Docking;
using LocalERP.DataAccess.Data;
using LocalERP.UiDataProxy;
using LocalERP.DataAccess.DataDAO;
using LocalERP.WinForm.Utility;
using LocalERP.DataAccess.Utility;
using gregn6Lib;

namespace LocalERP.WinForm
{
    public partial class QueryCashDetailForm : MyDockContent
    {
        private List<PayReceipt> payReceiptList;
        private List<ProductCirculation> productCirculationList;

        private int customerId = 0;

        public QueryCashDetailForm(Form parentForm, string title)
            : base()
        {
            this.Owner = parentForm;
            InitializeComponent();
            this.Text = title;

            this.backgroundWorker.DoWork += new DoWorkEventHandler(backgroundWorker_DoWork);
            this.backgroundWorker.RunWorkerCompleted +=new RunWorkerCompletedEventHandler(backgroundWorker_RunWorkerCompleted);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            string[] columnTexts = new string[] {"排序时间", "时间", "业务单号", "业务类型", "明细(产品/单价/数量)", /*"本单应付/已付",*/ "本单应收", "本单已收", /*"累计欠款\r(应付)",*/ "累计欠款\r(应收)", "备注" };
            string[] columnNames = new string[] {"sortTime", "time", "serial", "type", "detail", /*"needPay",*/ "needReceipt", "thisReceipt", /*"accNeedPay",*/ "accNeedReceipt", "comment" };
            int[] columnLengths = new int[] {0, 90, 140, 90, 260, 60, 60, 90, 120};
            bool[] columnNums = new bool[] { false, false, false, false, false, true, true, true, false };

            ControlUtility.initColumns(this.dataGridView1, columnNames, columnTexts, columnLengths, null);
            
            //categoryDao.initTree与ControlUtility.initTree重复了，可以删掉一个
            CategoryDao.getInstance().initTreeView("CustomerCategory", this.treeView1);

            DateTime dateTime = DateTime.Now;
            this.dateTimePicker_start.Value = dateTime.AddMonths(-12);
            //this.backgroundWorker.RunWorkerAsync();
            //this.invokeBeginLoadNotify();
        }
        
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            int parent = -1;
            if (treeView1.SelectedNode != null)
            {
                if (treeView1.SelectedNode.ImageIndex != 2)
                    return;
                parent = int.Parse(treeView1.SelectedNode.Name);
            }
            else
                return;

            this.textBox_search.Text = this.treeView1.SelectedNode.Text;
            this.customerId = int.Parse(this.treeView1.SelectedNode.Name);

            this.backgroundWorker.RunWorkerAsync(parent);
            this.invokeBeginLoadNotify();
        }

        public override void refresh()
        {
            this.label_notice.Visible = true;
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            int parentId = (int)e.Argument;          
            payReceiptList = PayReceiptDao.getInstance().FindPayReceiptList(this.dateTimePicker_start.Value, this.dateTimePicker_end.Value.AddDays(1), 4, this.textBox_search.Text, parentId, 1);
            //这个地方需要再改成ProductCirculationDao
            productCirculationList = ProductStainlessCirculationDao.getInstance().FindProductCirculationList(1, 4, this.dateTimePicker_start.Value, this.dateTimePicker_end.Value.AddDays(1), 4, this.textBox_search.Text, parentId);
        }

        private void formatRow(DataGridViewRow row, string customer, DateTime time, string serial, string type, string detail, /*string needPay, string thisPayed, string accNeedPay,*/ string needReceipt, string thisReceipted, string accNeedReceipt, string comment) {
            //row.Cells["customer"].Value = customer;
            if (time != DateTime.MinValue)
                row.Cells["sortTime"].Value = time;
            if(time != DateTime.MinValue)
                row.Cells["time"].Value = time.ToShortDateString();
            row.Cells["serial"].Value = serial;
            row.Cells["type"].Value = type;

            row.Cells["detail"].Value = detail;
            
            //row.Cells["needPay"].Value = string.IsNullOrEmpty(needPay) && string.IsNullOrEmpty(thisPayed)?"":string.Format("{0,8}/{1}", needPay, thisPayed);
            //row.Cells["accNeedPay"].Value = accNeedPay;

            row.Cells["needReceipt"].Value = needReceipt.Equals("0")?"":needReceipt;
            row.Cells["thisReceipt"].Value = thisReceipted.Equals("0")?"":thisReceipted;
            
            row.Cells["accNeedReceipt"].Value = accNeedReceipt;
            row.Cells["comment"].Value = comment;
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            dataGridView1.Rows.Clear();
            double sum_needPay = 0, sum_thisPayed = 0, sum_needReceipt = 0, sum_thisReceipted = 0;
            foreach (PayReceipt pr in payReceiptList)
            {
                int index = dataGridView1.Rows.Add();
                string /*needPay, thisPayed, accPay,*/ needReceipt, thisReceipted, accReceipt;
                /*needPay = thisPayed = accPay = */needReceipt = thisReceipted = accReceipt = "";

                //处理this pay or receive
                if (pr.cashDirection == -1)
                {
                    //thisPayed = pr.thisPayed.ToString();
                    //sum_thisPayed += pr.thisPayed;

                    //stone
                    thisReceipted = (-pr.thisPayed).ToString();
                    sum_thisReceipted -= pr.thisPayed;
                    //stone
                }
                else
                {
                    thisReceipted = pr.thisPayed.ToString();
                    sum_thisReceipted += pr.thisPayed;
                }

                //2018-11-1这个应该可以不要
                if (pr.bill_type == PayReceipt.BillType.ChangeArrear)
                    /*thisPayed = */thisReceipted = "";
                
                //处理acc
                if (pr.arrearDirection == 1)
                {
                    double tempAccPay = (pr.arrearDirection * pr.previousArrears - pr.cashDirection * (pr.amount - pr.thisPayed)) * pr.arrearDirection;
                    //accPay = tempAccPay.ToString();

                    //stone
                    accReceipt = (-tempAccPay).ToString();
                }
                else
                {
                    double tempAccReceipt = (pr.arrearDirection * pr.previousArrears - pr.cashDirection * (pr.amount - pr.thisPayed)) * pr.arrearDirection;
                    accReceipt = tempAccReceipt.ToString();
                }

                if (pr.bill_type == PayReceipt.BillType.OtherPay || pr.bill_type == PayReceipt.BillType.OtherReceipt)
                    /*accPay = */accReceipt = "";

                //处理need
                if (pr.bill_type == PayReceipt.BillType.SellRefund || pr.bill_type == PayReceipt.BillType.ChangeArrear && pr.cashDirection == -1)
                {
                    //needPay = pr.amount.ToString();
                    //sum_needPay += pr.amount;

                    //stone
                    needReceipt = (-pr.amount).ToString();
                    sum_needReceipt -= pr.amount;
                    //stone
                }

                if (pr.bill_type == PayReceipt.BillType.BuyRefund || pr.bill_type == PayReceipt.BillType.ChangeArrear && pr.cashDirection == 1)
                {
                    needReceipt = pr.amount.ToString();
                    sum_needReceipt += pr.amount;
                }
                
                formatRow(dataGridView1.Rows[index], pr.customerName, pr.bill_time, pr.serial, PayReceipt.PayReceiptTypeConfs[(int)pr.bill_type - 1].name, "", /*needPay, thisPayed, accPay, */needReceipt, thisReceipted, accReceipt, pr.comment);
            }

            foreach (ProductCirculation cir in this.productCirculationList)
            {
                int index = dataGridView1.Rows.Add();
                string /*needPay, thisPayed, accPay, */needReceipt, thisReceipted, accReceipt;
                /*needPay = thisPayed = accPay = */needReceipt = thisReceipted = accReceipt = "";

                //处理this, need
                if (cir.FlowType == 1){
                    //thisPayed = cir.ThisPayed.ToString();
                    //needPay = cir.RealTotal.ToString();

                    //sum_thisPayed += cir.ThisPayed;
                    //sum_needPay += cir.RealTotal;

                    //stone
                    thisReceipted = (-cir.ThisPayed).ToString();
                    needReceipt = (-cir.RealTotal).ToString();
                    sum_thisReceipted -= cir.ThisPayed;
                    sum_needReceipt -= cir.RealTotal;
                    //stone
                }
                else{
                    thisReceipted = cir.ThisPayed.ToString();
                    needReceipt = cir.RealTotal.ToString();

                    sum_thisReceipted += cir.ThisPayed;
                    sum_needReceipt += cir.RealTotal;
                }
                //处理acc
                if (cir.ArrearDirection == 1){
                    double tempAccPay = (cir.ArrearDirection * cir.PreviousArrears + cir.FlowType * (cir.RealTotal - cir.ThisPayed)) * cir.ArrearDirection;
                    //accPay = tempAccPay.ToString();
                    
                    //stone
                    accReceipt = (-tempAccPay).ToString();
                    //stone
                }
                else{
                    double tempAccReceipt = (cir.ArrearDirection * cir.PreviousArrears + cir.FlowType * (cir.RealTotal - cir.ThisPayed)) * cir.ArrearDirection;
                    accReceipt = tempAccReceipt.ToString();
                }

                formatRow(dataGridView1.Rows[index], cir.CustomerName, cir.CirculationTime, cir.Code, ProductCirculation.CirculationTypeConfs[(int)cir.Type - 1].name, cir.getRecordsTxt(), /*needPay, thisPayed, accPay,*/ needReceipt, thisReceipted, accReceipt, cir.Comment+"(货单自动生成)");

                //判断是否有运费
                if (cir.Freight > 0) {
                    index = dataGridView1.Rows.Add();
                    formatRow(dataGridView1.Rows[index], "", cir.CirculationTime, cir.Code, LabelUtility.CASH_OTHER_PAY, "", /*"", cir.Freight.ToString(), "",*/ "", (-cir.Freight).ToString(), "", "运费(货单自动生成)");
                }
            }
            this.dataGridView1.Sort(dataGridView1.Columns["sortTime"], ListSortDirection.Descending);
            
            int sumIndex = dataGridView1.Rows.Add();
            this.dataGridView1.Rows[sumIndex].DefaultCellStyle.ForeColor = Color.Red;
            this.dataGridView1.Rows[sumIndex].DefaultCellStyle.Font = new Font("宋体", 10F, FontStyle.Bold);
            formatRow(dataGridView1.Rows[sumIndex], "合计", DateTime.MinValue, null, null, "合计", /*sum_needPay.ToString(), sum_thisPayed.ToString(), null,*/ sum_needReceipt.ToString(), sum_thisReceipted.ToString(), null, null);

            dataGridView1.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);

            this.invokeEndLoadNotify();
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            if (this.label_notice.Visible == true)
                CategoryDao.getInstance().initTreeView("CustomerCategory", this.treeView1);

            if (this.customerId > 0)
            {
                this.backgroundWorker.RunWorkerAsync(this.customerId);
                this.invokeBeginLoadNotify();
            }

            this.label_notice.Visible = false;
        }

        private GridppReport Report = new GridppReport();

        private void button_print_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count <= 1)
            {
                MessageBox.Show("没有记录，请重新查询", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 载入报表模板数据
            
            string report_template_path = ConfUtility.check_report_path;
            Report.LoadFromFile(report_template_path);
            // 连接报表事件
            Report.Initialize -= new _IGridppReportEvents_InitializeEventHandler(ReportInitialize);
            Report.Initialize += new _IGridppReportEvents_InitializeEventHandler(ReportInitialize);
            //一定要先-=，要不会重复数据
            Report.FetchRecord -= new _IGridppReportEvents_FetchRecordEventHandler(ReportFetchRecord);
            Report.FetchRecord += new _IGridppReportEvents_FetchRecordEventHandler(ReportFetchRecord);
            // 打印预览
            Report.PrintPreview(true);
        }

        private void ReportInitialize()
        {
        }

        private void ReportFetchRecord() {

            Report.ControlByName("title").AsStaticBox.Text = ConfDao.getInstance().Get(3).ToString() + " 客户对账单";
            Report.ControlByName("customer").AsStaticBox.Text = "客户："+ this.textBox_search.Text;
            Report.ControlByName("time").AsStaticBox.Text = string.Format("对账时间: {0}至{1}", this.dateTimePicker_start.Value.ToShortDateString(), this.dateTimePicker_end.Value.ToShortDateString());

            // 处理 明细
            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                Report.DetailGrid.Recordset.Append();
                Report.FieldByDBName("date").AsString = row.Cells["time"].Value.ToString();
                Report.FieldByDBName("type").AsString = row.Cells["type"].Value.ToString() +"\n" + row.Cells["serial"].Value.ToString();
                Report.FieldByDBName("product").AsString = row.Cells["detail"].Value.ToString();
                Report.FieldByDBName("needReceipt").AsString = row.Cells["needReceipt"].Value.ToString();
                Report.FieldByDBName("thisReceipt").AsString = row.Cells["thisReceipt"].Value.ToString();
                Report.FieldByDBName("accTotal").AsString = row.Cells["accNeedReceipt"].Value.ToString();
                Report.DetailGrid.Recordset.Post();
            }
        }
    }
}