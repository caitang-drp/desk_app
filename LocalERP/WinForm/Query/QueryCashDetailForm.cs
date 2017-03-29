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

namespace LocalERP.WinForm
{
    public partial class QueryCashDetailForm : MyDockContent
    {
        private string searchName = null;

        private List<PayReceipt> payReceiptList;
        private DataTable dataTable2;

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
            //categoryItemProxy.initColumns(this.dataGridView1);

            string[] columnTexts = new string[] { "往来单位", "时间", "业务单号", "业务类型", "本单应付", "本单已付", "累计欠款\r(应付)", "本单应收", "本单已收", "累计欠款\r(应收)" };
            string[] columnNames = new string[] { "customer", "time", "serial", "type", "needPay", "thisPayed", "accNeedPay", "needReceipt", "thisReceipted", "accNeedReceipt"};
            int [] columnLengths = new int[] { 90, 120, 120, 90,     90, 90, 90,    90, 90, 90};

            ControlUtility.initColumns(this.dataGridView1, columnNames, columnTexts, columnLengths);
            CategoryDao.getInstance().initTreeView("CustomerCategory", this.treeView1);
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            int parent = -1;
            if (treeView1.SelectedNode != null)
                parent = int.Parse(treeView1.SelectedNode.Name);

            searchName = null;
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
            Category parent = null;
            if(parentId > 0)
                parent = CategoryDao.getInstance().FindById("CustomerCategory", parentId);

            payReceiptList = PayReceiptDao.getInstance().FindPayReceiptList(parent, this.textBox_search.Text);
            //这个地方需要再改成ProductCirculationDao
            //dataTable2 = ProductStainlessCirculationDao.getInstance().FindList(parent, this.textBox_search.Text, true, true);
        }

        private void formatRow(DataGridViewRow row, string customer, DateTime time, string serial, string type, string needPay, string thisPayed, string accNeedPay, string needReceipt, string thisPeceipted, string accNeedReceipt) {
            row.Cells["customer"].Value = customer;
            row.Cells["time"].Value = time;
            row.Cells["serial"].Value = serial;
            row.Cells["type"].Value = type;
            row.Cells["needPay"].Value = needPay;
            row.Cells["thisPayed"].Value = thisPayed;
            row.Cells["accNeedPay"].Value = accNeedPay;
            row.Cells["needReceipt"].Value = needReceipt;
            row.Cells["thisReceipted"].Value = thisPeceipted;
            row.Cells["accNeedReceipt"].Value = accNeedReceipt;
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            dataGridView1.Rows.Clear();
            foreach (PayReceipt pr in payReceiptList)
            {
                int index = dataGridView1.Rows.Add();
                string needPay = "-", thisPayed = "-", accPay = "-", needReceipt = "-", thisReceipted = "-", accReceipt = "-";

                if (pr.cashDirection == -1)
                    thisPayed = pr.thisPayed.ToString();
                else
                    thisReceipted = pr.thisPayed.ToString();
                
                if (pr.arrearDirection == 1)
                    accPay = ((pr.arrearDirection * pr.previousArrears - pr.cashDirection * (pr.amount - pr.thisPayed)) * pr.arrearDirection).ToString();
                else
                    accReceipt = ((pr.arrearDirection * pr.previousArrears - pr.cashDirection * (pr.amount - pr.thisPayed)) * pr.arrearDirection).ToString();

                if (pr.bill_type == PayReceipt.BillType.BuyRefund)
                    needReceipt = pr.amount.ToString();

                if (pr.bill_type == PayReceipt.BillType.SellRefund)
                    needPay = pr.amount.ToString();
                
                formatRow(dataGridView1.Rows[index], pr.customerName, pr.bill_time, pr.serial, PayReceipt.PayReceiptTypeConfs[(int)pr.bill_type - 1].name, needPay, thisPayed, accPay, needReceipt, thisReceipted, accReceipt);
            }
            /*
            foreach (DataRow dr in dataTable2.Rows)
            {
                int index = dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells["customer"].Value = dr["Customer.name"];
                dataGridView1.Rows[index].Cells["time"].Value = dr["circulationTime"];
                int type = (int)(dr["type"]);
                dataGridView1.Rows[index].Cells["type"].Value = PayReceipt.PayReceiptTypeConfs[type - 1].name; //dr["bill_type"];
                
                double sum = 0;
                ValidateUtility.getDouble(dr, "thisPayed", out sum);
                if (type == 1 || type == 4)
                    ControlUtility.setCellWithColor(dataGridView1.Rows[index].Cells["sum"], Color.Green, string.Format("-{0:0.00}", sum));
                else if (type == 2 || type == 3)
                    ControlUtility.setCellWithColor(dataGridView1.Rows[index].Cells["sum"], Color.Red, string.Format("+{0:0.00}", sum));
            }*/

            this.dataGridView1.Sort(dataGridView1.Columns["time"], ListSortDirection.Descending);

            this.invokeEndLoadNotify();
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            //click相当于刷新，类别有可能变化，所以重新加载
            CategoryDao.getInstance().initTreeView("CustomerCategory", this.treeView1);

            if (this.treeView1.Nodes.Count > 0 && treeView1.SelectedNode != null && treeView1.Nodes[0].IsSelected == false)
                this.treeView1.SelectedNode = this.treeView1.Nodes[0];
            else
                treeView1_AfterSelect(null, null);
            this.label_notice.Visible = false;
        }
    }
}