using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using LocalERP.DataAccess.DataDAO;
using System.Collections;

namespace LocalERP.WinForm
{
    public partial class ProductLibForm : DockContent
    {
        public ProductLibForm()
        {
            InitializeComponent();
        }

        private void ProductLibForm_Load(object sender, EventArgs e)
        {
            DateTime endTime = DateTime.Now;
            DateTime startTime = endTime.AddMonths(-1);

            initYear(this.comboBox_startYear, startTime.Year);
            initYear(this.comboBox_endYear, endTime.Year);
            initMonth(this.comboBox_startMon, startTime.Month);
            initMonth(this.comboBox_endMon, endTime.Month);
            this.initList(false);
        }

        private void initYear(ComboBox combox, int value) {
            DateTime now = DateTime.Now;
            ArrayList arrayList = new ArrayList();
            for (int i = 2010; i <= now.Year; i++)
                arrayList.Add(new DictionaryEntry(i, i+"年"));

            combox.DataSource = arrayList;
            combox.ValueMember = "Key";
            combox.DisplayMember = "Value";

            combox.SelectedValue = value;
        }

        private void initMonth(ComboBox combox, int value) {
            ArrayList arrayList = new ArrayList();
            for (int i = 1; i <= 12; i++)
                arrayList.Add(new DictionaryEntry(i, i + "月"));

            combox.DataSource = arrayList;
            combox.ValueMember = "Key";
            combox.DisplayMember = "Value";

            combox.SelectedValue = value;
        }

        public void initList(bool isStatistics)
        {
            this.rowMergeView1.Rows.Clear();
            int columnNums = this.rowMergeView1.Columns.Count;
            for (int ci = columnNums - 1; ci >= 3; ci--)
                rowMergeView1.Columns.RemoveAt(ci);
            this.rowMergeView1.MergeColumnNames.Remove("Column1");

            DateTime sTime = new DateTime((int)(comboBox_startYear.SelectedValue), (int)(comboBox_startMon.SelectedValue), 1);
            DateTime eTime = new DateTime((int)(comboBox_endYear.SelectedValue), (int)(comboBox_endMon.SelectedValue), 1);

            DateTime tempTime = sTime;
            int i = 0;
            while (eTime.CompareTo(tempTime) >= 0 || i< 2) {
                DataGridViewCell cell = new DataGridViewTextBoxCell();
                DataGridViewColumn column = new DataGridViewColumn(cell);
                column.HeaderText = tempTime.ToString("yyyy年MM月");
                column.Name = "flow"+i;
                this.rowMergeView1.Columns.Add(column);

                tempTime = tempTime.AddMonths(1);
                i++;
            }
            
            this.rowMergeView1.ColumnHeadersHeight = 50;
            this.rowMergeView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            this.rowMergeView1.MergeColumnNames.Add("Column1");
            this.rowMergeView1.AddSpanHeader(3, i, "入库数/出库数");

            DataTable dataTable = ProductDao.getInstance().FindList(null);

            foreach (DataRow dr in dataTable.Rows)
            {
                int index = this.rowMergeView1.Rows.Add();
                this.rowMergeView1.Rows[index].Cells["ID"].Value = dr["ID"];
                this.rowMergeView1.Rows[index].Cells["name"].Value = dr["name"];
                this.rowMergeView1.Rows[index].Cells["num"].Value = dr["num"];


                tempTime = sTime;
                i = 0;
                while (eTime.CompareTo(tempTime) >= 0 || i < 2)
                {
                    if (isStatistics)
                    {
                        int flowIn = this.getFlow((int)(dr["ID"]), tempTime, tempTime.AddMonths(1), 1);
                        int flowOut = this.getFlow((int)(dr["ID"]), tempTime, tempTime.AddMonths(1), -1);
                        this.rowMergeView1.Rows[index].Cells["flow" + i].Value = flowIn + "/" + flowOut;
                    }else
                        this.rowMergeView1.Rows[index].Cells["flow" + i].Value = "-";

                    tempTime = tempTime.AddMonths(1);
                    i++;
                }
            }
        }


        /// <summary>
        /// get value
        /// </summary>
        
        private int getFlow(int productID, DateTime startTime, DateTime endTime, int flowType) {
            DataTable dt = ProductFlowDao.getInstance().FindList(productID, startTime, endTime, flowType);
            int totalNum = 0;
            foreach (DataRow dr in dt.Rows) {
                totalNum += (int)dr["num"];
            }
            return totalNum;
        }

        /// <summary>
        /// event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            initList(true);
        }
    }
}