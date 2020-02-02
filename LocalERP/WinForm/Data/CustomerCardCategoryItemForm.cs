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
    public class CustomerCardCategoryItemForm : CustomerCategoryItemForm
    {
        public CustomerCardCategoryItemForm(int openMode, CategoryItemTypeConf conf, string title, Form parentForm)
            :base(openMode, conf, title, parentForm)
        {
        }

        protected override void initColumns()
        {
            DataGridViewCheckBoxColumn check = new DataGridViewCheckBoxColumn();
            check.HeaderText = "选择";
            check.Name = "check";
            check.Width = 80;

            string[] columnTexts = new string[] { "ID", "名称", "类别"};
            string[] columnNames = new string[] { "ID", "name", "category"};
            int[] columnLengths = new int[] { 120, 140, 120};

            ControlUtility.initColumns(this.dataGridView1, columnNames, columnTexts, columnLengths);
            this.dataGridView1.Columns.Insert(0, check);
        }

        protected override void initRecords(DataTable dataTable)
        {
            dataGridView1.Rows.Clear();
            int index = 0;
            foreach (DataRow dr in dataTable.Rows)
            {
                index = dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells["ID"].Value = dr["Customer.ID"];
                dataGridView1.Rows[index].Cells["name"].Value = dr["Customer.name"];
                dataGridView1.Rows[index].Cells["category"].Value = dr["CustomerCategory.name"];
                
            }

        }
    }
}
