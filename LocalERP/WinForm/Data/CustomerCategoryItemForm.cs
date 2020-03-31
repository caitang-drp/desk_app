using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using LocalERP.DataAccess.DataDAO;
using LocalERP.DataAccess.Data;
using LocalERP.WinForm.Utility;
using LocalERP.DataAccess.Utility;
using System.Drawing;
using LocalERP.UiDataProxy;

namespace LocalERP.WinForm.Data
{
    public class CustomerCategoryItemForm : CategoryItemForm
    {
        public CustomerCategoryItemForm(int openMode, CategoryItemTypeConf conf, string title, Form parentForm)
            :base(openMode, conf, title, parentForm)
        {
            this.dataGridView1.IsLastRowSort = false;
        }

        protected override void initColumns()
        {
            DataGridViewCheckBoxColumn check = new DataGridViewCheckBoxColumn();
            check.HeaderText = "选择";
            check.Name = "check";
            check.Width = 60;

            string[] columnTexts = new string[] { "ID", "名称", "类别", "我方欠款/元\r(应付)", "对方欠款/元\r(应收)" };
            string[] columnNames = new string[] { "ID", "name", "category", "myArrear", "hisArrear" };
            bool[] columnNums = new bool[] { false, false, false, true, true };
            int[] columnLengths = new int[] { 80, 120, 80, 120, 120 };

            ControlUtility.initColumns(this.dataGridView1, columnNames, columnTexts, columnLengths, columnNums);
            this.dataGridView1.Columns.Insert(0, check);
        }        

        protected override DataTable getRecordsTable(int parentId, string name)
        {
            Category parent = null;
            if (parentId > 0)
                parent = CategoryDao.getInstance().FindById(conf.CategoryTableName, parentId);

            DataTable dataTable = CustomerDao.getInstance().FindList(parent, name);
            return dataTable;
        }

        protected override void initRecords(DataTable dataTable)
        {
            dataGridView1.Rows.Clear();
            double sumToPay = 0, sumToRecepit = 0;
            int index = 0;
            foreach (DataRow dr in dataTable.Rows)
            {
                index = dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells["ID"].Value = dr["Customer.ID"];
                dataGridView1.Rows[index].Cells["name"].Value = dr["Customer.name"];
                dataGridView1.Rows[index].Cells["category"].Value = dr["CustomerCategory.name"];
                double arrear = 0;
                bool temp;
                ValidateUtility.getDouble(dr, "arrear", out arrear, out temp);
                if (arrear > 0)
                {
                    dataGridView1.Rows[index].Cells["myArrear"].Style.ForeColor = Color.Green;
                    dataGridView1.Rows[index].Cells["myArrear"].Style.SelectionForeColor = Color.Green;
                    dataGridView1.Rows[index].Cells["myArrear"].Value = arrear;
                    sumToPay += arrear;
                }
                else if (arrear < 0)
                {
                    dataGridView1.Rows[index].Cells["hisArrear"].Style.ForeColor = Color.Red;
                    dataGridView1.Rows[index].Cells["hisArrear"].Style.SelectionForeColor = Color.Red;
                    dataGridView1.Rows[index].Cells["hisArrear"].Value = -arrear;
                    sumToRecepit += (-arrear);
                }
            }
            
            index = dataGridView1.Rows.Add();

            dataGridView1.Rows[index].Cells["name"].Value = "合计";
            dataGridView1.Rows[index].Cells["myArrear"].Style.ForeColor = Color.Green;
            dataGridView1.Rows[index].Cells["myArrear"].Style.SelectionForeColor = Color.Green;
            //四舍五入，取最后两位
            dataGridView1.Rows[index].Cells["myArrear"].Value = Math.Round(sumToPay, 2);

            dataGridView1.Rows[index].Cells["hisArrear"].Style.ForeColor = Color.Red;
            dataGridView1.Rows[index].Cells["hisArrear"].Style.SelectionForeColor = Color.Red;
            dataGridView1.Rows[index].Cells["hisArrear"].Value = Math.Round(sumToRecepit, 2);

            dataGridView1.Rows[index].DefaultCellStyle.ForeColor = Color.Red;
            dataGridView1.Rows[index].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Rows[index].DefaultCellStyle.Font = new Font("宋体", 10F, FontStyle.Bold);
        }

        /*protected override void initRecords(DataTable dataTable)
        {
            DataTable dt = new DataTable();
            DataColumn dc_name = new DataColumn("名称");
            DataColumn dc_myArrear = new DataColumn("myArrear", typeof(double));
            dt.Columns.Add(dc_name);
            dt.Columns.Add(dc_myArrear);

            int index = 0;
            foreach (DataRow dataRow in dataTable.Rows)
            {
                DataRow dr = dt.NewRow();
                dr["名称"] = dataRow["Customer.name"];
                
                double arrear = 0;
                bool temp;
                ValidateUtility.getDouble(dataRow, "arrear", out arrear, out temp);
                dr["myArrear"] = arrear;

                dt.Rows.Add(dr);
            }

            this.dataGridView1.DataSource = dt;
        }*/

        protected override MyDockContent getItemForm(Form owner, int openMode, int ID)
        {
            CustomerForm form = FormSingletonFactory.getInstance().getCustomerForm();
            form.Owner = owner;
            form.reload(openMode, ID);
            return form;
        }

        protected override void delItem(int id)
        {
            CustomerDao.getInstance().Delete(id);
        }
    }
}
