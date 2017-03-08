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

        private DataTable dataTable1;
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

            string [] columnTexts = new string[] { "往来单位", "时间", "支付方式", "总金额/元" };
            string [] columnNames = new string[] { "customer", "time", "type", "sum" };
            int [] columnLengths = new int[] { 100, 160, 120, 100 };

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

            dataTable1 = PayReceiptDao.getInstance().FindList(parent, this.textBox_search.Text);
            //这个地方需要再改成ProductCirculationDao
            dataTable2 = ProductStainlessCirculationDao.getInstance().FindList(parent, this.textBox_search.Text, true);
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            dataGridView1.Rows.Clear();
            foreach (DataRow dr in dataTable1.Rows)
            {
                int index = dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells["customer"].Value = dr["Customer.name"];
                dataGridView1.Rows[index].Cells["time"].Value = dr["bill_time"];// ((DateTime)dr["bill_time"]).ToShortDateString();
                int type = (int)(dr["bill_type"]);
                dataGridView1.Rows[index].Cells["type"].Value = PayReceipt.PayReceiptTypeConfs[type - 1].name; //dr["bill_type"];
                
                double sum=0;
                
                ValidateUtility.getDouble(dr, "amount", out sum);
                if(type==1 || type == 4)
                    ControlUtility.setCellWithColor(dataGridView1.Rows[index].Cells["sum"], Color.Green, string.Format("-{0:0.00}", sum));
                else if (type == 2 || type==3)
                    ControlUtility.setCellWithColor(dataGridView1.Rows[index].Cells["sum"], Color.Red, string.Format("+{0:0.00}", sum));
            }

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
            }

            this.dataGridView1.Sort(dataGridView1.Columns["time"], ListSortDirection.Descending);

            this.invokeEndLoadNotify();
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            if (this.treeView1.Nodes.Count > 0 && treeView1.SelectedNode != null && treeView1.Nodes[0].IsSelected == false)
                this.treeView1.SelectedNode = this.treeView1.Nodes[0];
            else
                treeView1_AfterSelect(null, null);
            this.label_notice.Visible = false;
        }
    }
}