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
            string[] columnTexts = new string[] { "往来单位", "时间", "业务单号", "业务类型", "本单应付/已付", "本单应收/已收", "累计欠款\r(应付)", "累计欠款\r(应收)", "备注" };
            string[] columnNames = new string[] { "customer", "time", "serial", "type", "needPay", "needReceipt", "accNeedPay", "accNeedReceipt", "comment" };
            int[] columnLengths = new int[] { 100, 140, 140, 90, 125, 125, 90, 90, 120};

            ControlUtility.initColumns(this.dataGridView1, columnNames, columnTexts, columnLengths);
            //CategoryDao.getInstance().initTreeView("CustomerCategory", this.treeView1);

            DateTime dateTime = DateTime.Now;
            this.dateTimePicker_start.Value = dateTime.AddMonths(-1);
            this.backgroundWorker.RunWorkerAsync();
            this.invokeBeginLoadNotify();
        }
        /*
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            int parent = -1;
            if (treeView1.SelectedNode != null)
                parent = int.Parse(treeView1.SelectedNode.Name);

            searchName = null;
            this.backgroundWorker.RunWorkerAsync(parent);
            this.invokeBeginLoadNotify();
        }*/

        public override void refresh()
        {
            this.label_notice.Visible = true;
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            /*int parentId = (int)e.Argument;
            Category parent = null;
            if(parentId > 0)
                parent = CategoryDao.getInstance().FindById("CustomerCategory", parentId);
            */
            payReceiptList = PayReceiptDao.getInstance().FindPayReceiptList(this.dateTimePicker_start.Value, this.dateTimePicker_end.Value.AddDays(1), 4, this.textBox_search.Text, 1);
            //这个地方需要再改成ProductCirculationDao
            productCirculationList = ProductStainlessCirculationDao.getInstance().FindProductCirculationList(1, 4, this.dateTimePicker_start.Value, this.dateTimePicker_end.Value.AddDays(1), 4, this.textBox_search.Text);
        }

        private void formatRow(DataGridViewRow row, string customer, DateTime time, string serial, string type, string needPay, string thisPayed, string accNeedPay, string needReceipt, string thisReceipted, string accNeedReceipt, string comment) {
            row.Cells["customer"].Value = customer;
            if(time != DateTime.MinValue)
                row.Cells["time"].Value = time;
            row.Cells["serial"].Value = serial;
            row.Cells["type"].Value = type;
            row.Cells["needPay"].Value = string.IsNullOrEmpty(needPay) && string.IsNullOrEmpty(thisPayed)?"":string.Format("{0,8}/{1}", needPay, thisPayed);
            //row.Cells["thisPayed"].Value = thisPayed;
            row.Cells["accNeedPay"].Value = accNeedPay;
            row.Cells["needReceipt"].Value = string.IsNullOrEmpty(needReceipt)&&string.IsNullOrEmpty(thisReceipted)?"": string.Format("{0,8}/{1}", needReceipt, thisReceipted);
            //row.Cells["thisReceipted"].Value = thisPeceipted;
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
                string needPay, thisPayed, accPay, needReceipt, thisReceipted, accReceipt;
                needPay = thisPayed = accPay = needReceipt = thisReceipted = accReceipt = "";

                //处理this
                if (pr.cashDirection == -1)
                {
                    thisPayed = pr.thisPayed.ToString();
                    sum_thisPayed += pr.thisPayed;
                }
                else
                {
                    thisReceipted = pr.thisPayed.ToString();
                    sum_thisReceipted += pr.thisPayed;
                }

                if (pr.bill_type == PayReceipt.BillType.ChangeArrear)
                    thisPayed = thisReceipted = "";
                
                //处理acc
                if (pr.arrearDirection == 1)
                {
                    double tempAccPay = (pr.arrearDirection * pr.previousArrears - pr.cashDirection * (pr.amount - pr.thisPayed)) * pr.arrearDirection;
                    accPay = tempAccPay.ToString();
                    //sum_accPay += tempAccPay;
                }
                else
                {
                    double tempAccReceipt = (pr.arrearDirection * pr.previousArrears - pr.cashDirection * (pr.amount - pr.thisPayed)) * pr.arrearDirection;
                    accReceipt = tempAccReceipt.ToString();
                    //sum_accReceipt += tempAccReceipt;
                }

                if (pr.bill_type == PayReceipt.BillType.OtherPay || pr.bill_type == PayReceipt.BillType.OtherReceipt)
                    accPay = accReceipt = "";

                //处理need
                if (pr.bill_type == PayReceipt.BillType.BuyRefund || pr.bill_type == PayReceipt.BillType.ChangeArrear && pr.cashDirection == 1)
                {
                    needReceipt = pr.amount.ToString();
                    sum_needReceipt += pr.amount;
                }

                if (pr.bill_type == PayReceipt.BillType.SellRefund || pr.bill_type == PayReceipt.BillType.ChangeArrear && pr.cashDirection == -1)
                {
                    needPay = pr.amount.ToString();
                    sum_needPay += pr.amount;
                }
                
                formatRow(dataGridView1.Rows[index], pr.customerName, pr.bill_time, pr.serial, PayReceipt.PayReceiptTypeConfs[(int)pr.bill_type - 1].name, needPay, thisPayed, accPay, needReceipt, thisReceipted, accReceipt, pr.comment);
            }

            foreach (ProductCirculation cir in this.productCirculationList)
            {
                int index = dataGridView1.Rows.Add();
                string needPay, thisPayed, accPay, needReceipt, thisReceipted, accReceipt;
                needPay = thisPayed = accPay = needReceipt = thisReceipted = accReceipt = "";

                //处理this, need
                if (cir.FlowType == 1)
                {
                    thisPayed = cir.ThisPayed.ToString();
                    needPay = cir.RealTotal.ToString();

                    sum_thisPayed += cir.ThisPayed;
                    sum_needPay += cir.RealTotal;
                }
                else
                {
                    thisReceipted = cir.ThisPayed.ToString();
                    needReceipt = cir.RealTotal.ToString();

                    sum_thisReceipted += cir.ThisPayed;
                    sum_needReceipt += cir.RealTotal;
                }
                //处理acc
                if (cir.ArrearDirection == 1)
                {
                    double tempAccPay = (cir.ArrearDirection * cir.PreviousArrears + cir.FlowType * (cir.RealTotal - cir.ThisPayed)) * cir.ArrearDirection;
                    accPay = tempAccPay.ToString();
                    //sum_accPay += tempAccPay;
                }
                else
                {
                    double tempAccReceipt = (cir.ArrearDirection * cir.PreviousArrears + cir.FlowType * (cir.RealTotal - cir.ThisPayed)) * cir.ArrearDirection;
                    accReceipt = tempAccReceipt.ToString();
                    //sum_accReceipt += tempAccReceipt;
                }


                formatRow(dataGridView1.Rows[index], cir.CustomerName, cir.CirculationTime, cir.Code, ProductCirculation.CirculationTypeConfs[(int)cir.Type - 1].name, needPay, thisPayed, accPay, needReceipt, thisReceipted, accReceipt, cir.Comment+"(货单自动生成)");

                //判断是否有运费
                if (cir.Freight > 0) {
                    index = dataGridView1.Rows.Add();
                    formatRow(dataGridView1.Rows[index], "", cir.CirculationTime, cir.Code, LabelUtility.CASH_OTHER_PAY, "", cir.Freight.ToString(), "", "", "", "", "运费(货单自动生成)");
                }
            }
            this.dataGridView1.Sort(dataGridView1.Columns["time"], ListSortDirection.Descending);
            
            int sumIndex = dataGridView1.Rows.Add();
            this.dataGridView1.Rows[sumIndex].DefaultCellStyle.ForeColor = Color.Red;
            this.dataGridView1.Rows[sumIndex].DefaultCellStyle.Font = new Font("宋体", 10F, FontStyle.Bold);
            formatRow(dataGridView1.Rows[sumIndex], "合计", DateTime.MinValue, null, null, sum_needPay.ToString(), sum_thisPayed.ToString(), null, sum_needReceipt.ToString(), sum_thisReceipted.ToString(), null, null);

            this.invokeEndLoadNotify();
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            /*
            //click相当于刷新，类别有可能变化，所以重新加载
            CategoryDao.getInstance().initTreeView("CustomerCategory", this.treeView1);

            if (this.treeView1.Nodes.Count > 0 && treeView1.SelectedNode != null && treeView1.Nodes[0].IsSelected == false)
                this.treeView1.SelectedNode = this.treeView1.Nodes[0];
            else
                treeView1_AfterSelect(null, null);*/
            this.backgroundWorker.RunWorkerAsync();
            this.invokeBeginLoadNotify();
            this.label_notice.Visible = false;
        }

        private GridppReport Report = new GridppReport();

        private void button_print_Click(object sender, EventArgs e)
        {
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
            // 处理 明细
            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                Report.DetailGrid.Recordset.Append();
                Report.FieldByDBName("date").AsString = row.Cells["customer"].Value.ToString();
                Report.FieldByDBName("type").AsString = row.Cells["type"].Value.ToString();
                Report.FieldByDBName("total").AsString = row.Cells["needReceipt"].Value.ToString();
                Report.FieldByDBName("accTotal").AsString = row.Cells["accNeedReceipt"].Value.ToString();
                Report.DetailGrid.Recordset.Post();
            }
        }
    }
}