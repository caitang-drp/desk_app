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
using System.Collections;

namespace LocalERP.WinForm
{
    public partial class StatisticProductForm : MyDockContent
    {
        protected string[] statisticTypeContext = new string[] { "按商品", "按来往单位", "按月份", "按类别", "按品牌" };
        protected enum statisticTypeEnum { product = 0, customer = 1, month = 2, type = 3, brand = 4 };
        protected string[][] columnNames, columnTexts;
        protected int[][] columnLengths;

        protected DataTable itemsTable;
        
        public StatisticProductForm()
        {
            InitializeComponent();

            DateTime dateTime = DateTime.Now;
            this.dateTimePicker3.Value = dateTime.AddMonths(-1);

            this.backgroundWorker.DoWork += new DoWorkEventHandler(backgroundWorker_DoWork);
            this.backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker_RunWorkerCompleted);

            this.initControls();
            this.initConfArray();

            this.label_title.Text = string.Format("统计类型:{0,-12}统计方式:{0,-12}统计时间:{0,-12}", "");
        }

        private void initConfArray(){

            columnNames = new string[5][];
            columnTexts = new string[5][];
            columnLengths = new int[5][];

            columnTexts[0] = new string[]{"货号", "货品名称", "总数量", "总金额"};
            columnNames[0] = new string[] { "serial", "product", "staticNum", "sum"};
            columnLengths[0] = new int[] {80, 240, 100, 100};

            columnTexts[1] = new string[] { "ID", "来往单位", "总数量", "总金额" };
            columnNames[1] = new string[] { "ID", "name", "num", "sum" };
            columnLengths[1] = new int[] { 80, 200, 100, 100 };

            columnTexts[2] = new string[] { "月份", "时间段", "总数量", "总金额" };
            columnNames[2] = new string[] { "month", "timeSpan", "num", "sum" };
            columnLengths[2] = new int[] { 100, 220, 100, 100 };

            columnTexts[3] = new string[] { "ID", "类别", "总数量", "金额" };
            columnNames[3] = new string[] { "ID", "type", "num", "price" };
            columnLengths[3] = new int[] { 40, 160, 80, 100 };

            columnTexts[4] = new string[] { "ID", "品牌", "总数量", "金额" };
            columnNames[4] = new string[] { "ID", "branch", "num", "price" };
            columnLengths[4] = new int[] { 40, 160, 80, 100 };
        }

        private void initControls() {
            ArrayList arrayList = new ArrayList();

            for (int i = 0; i <= 2; i++)
                arrayList.Add(new DictionaryEntry(i, statisticTypeContext[i]));

            this.comboBox1.DataSource = arrayList;
            this.comboBox1.ValueMember = "Key";
            this.comboBox1.DisplayMember = "Value";

            ArrayList typeList = new ArrayList();
            typeList.Add(new DictionaryEntry(1, "采购统计"));
            typeList.Add(new DictionaryEntry(3, "销售统计"));
            this.comboBox2.DataSource = typeList;
            this.comboBox2.ValueMember = "Key";
            this.comboBox2.DisplayMember = "Value";
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            statisticTypeEnum type = (statisticTypeEnum)this.comboBox1.SelectedValue;
            this.label_title.Text = string.Format("统计类型:{0,-12}统计方式:{1,-12}统计时间:{2}至{3}",
                this.comboBox2.Text, this.comboBox1.Text, this.dateTimePicker3.Value.ToShortDateString(), this.dateTimePicker4.Value.ToShortDateString());
            switch (type)
            {
                case statisticTypeEnum.product:
                    statisticProduct();
                    break;
                case statisticTypeEnum.customer:
                    statisticCustomer();
                    break;
                case statisticTypeEnum.month:
                    statisticMonth();
                    break;
                default:
                    break;
            }
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            statisticTypeEnum type = (statisticTypeEnum)this.comboBox1.SelectedValue;
            switch (type)
            {
                case statisticTypeEnum.product:
                    initProductItems();
                    break;
                case statisticTypeEnum.customer:
                    initCustomerItems();
                    break;
                case statisticTypeEnum.month:
                    initMonthItems();
                    break;
                default:
                    break;
            }
            this.invokeEndLoadNotify();
            this.label_notice.Visible = false;
        }

        protected virtual void statisticProduct()
        {}

        protected virtual void statisticCustomer() {
            itemsTable = CustomerDao.getInstance().FindListForStatistic(null);
            itemsTable.Columns.Add("num");
            itemsTable.Columns.Add("sum");
            
            foreach(DataRow dr in itemsTable.Rows){
                
                int customerID = (int)dr["ID"];
                int totalNum = 0;
                double sum;
                this.getSum(this.dateTimePicker3.Value, this.dateTimePicker4.Value,(int)this.comboBox2.SelectedValue, 0, customerID, out totalNum, out sum);
                dr["num"] = totalNum;
                dr["sum"] = sum;
            }  
        }

        protected virtual void statisticMonth()
        {
            itemsTable = new DataTable();
            itemsTable.Columns.Add("month");
            itemsTable.Columns.Add("timeSpan");
            itemsTable.Columns.Add("num");
            itemsTable.Columns.Add("sum");

            DateTime startDate = this.dateTimePicker3.Value;
            DateTime currentDate = (new DateTime(this.dateTimePicker3.Value.Year, this.dateTimePicker3.Value.Month, 1)).AddMonths(1).AddDays(-1);

            int totalNum;
            double sum;

            while (currentDate < this.dateTimePicker4.Value) {

                this.getSum(startDate, currentDate, (int)this.comboBox2.SelectedValue, 0, 0, out totalNum, out sum);
                itemsTable.Rows.Add(startDate.ToString("yyyy年MM月"), startDate.ToShortDateString() + "至" + currentDate.ToShortDateString(), totalNum, sum);
                
                startDate = currentDate.AddDays(1);
                currentDate = (new DateTime(currentDate.Year, currentDate.Month, 1)).AddMonths(2).AddDays(-1);
            }
            if (startDate <= this.dateTimePicker4.Value)
            {
                this.getSum(startDate, this.dateTimePicker4.Value, (int)this.comboBox2.SelectedValue, 0, 0, out totalNum, out sum);
                itemsTable.Rows.Add(startDate.ToString("yyyy年MM月"), startDate.ToShortDateString() + "至" + this.dateTimePicker4.Value.ToShortDateString(), totalNum, sum);
            }
        }

        protected virtual void getSum(DateTime start, DateTime end, int type, int productID, int customerID, out int totalNum, out double sum) {
            totalNum = 0;
            sum = 0;
        }

        //写入统计结果
        protected virtual void initProductItems() { }

        private void initCustomerItems()
        {
            this.dataGridView1.Rows.Clear();
            foreach (DataRow dr in itemsTable.Rows)
            {
                this.dataGridView1.Rows.Add(dr["ID"], dr["name"], dr["num"], dr["sum"]);
            }
        }

        private void initMonthItems()
        {
            this.dataGridView1.Rows.Clear();
            foreach (DataRow dr in itemsTable.Rows)
            {
                this.dataGridView1.Rows.Add(dr["month"], dr["timeSpan"], dr["num"], dr["sum"]);
            }
        }

        public override void refresh()
        {
            this.label_notice.Visible = true;
        }

        private void button_search_Click(object sender, EventArgs e)
        {
            ControlUtility.initColumns(this.dataGridView1, 
                columnNames[(int)(this.comboBox1.SelectedValue)],
                columnTexts[(int)(this.comboBox1.SelectedValue)],
                columnLengths[(int)(this.comboBox1.SelectedValue)]);
            backgroundWorker.RunWorkerAsync();
            this.invokeBeginLoadNotify();
        }
    }
}