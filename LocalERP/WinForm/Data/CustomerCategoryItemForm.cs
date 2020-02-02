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
        }

        protected override void initColumns()
        {
            DataGridViewCheckBoxColumn check = new DataGridViewCheckBoxColumn();
            check.HeaderText = "ѡ��";
            check.Name = "check";
            check.Width = 60;

            string[] columnTexts = new string[] { "ID", "����", "���", "�ҷ�Ƿ��/Ԫ\r(Ӧ��)", "�Է�Ƿ��/Ԫ\r(Ӧ��)" };
            string[] columnNames = new string[] { "ID", "name", "category", "myArrear", "hisArrear" };
            int[] columnLengths = new int[] { 80, 120, 80, 120, 120 };

            ControlUtility.initColumns(this.dataGridView1, columnNames, columnTexts, columnLengths);
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
                    dataGridView1.Rows[index].Cells["myArrear"].Value = string.Format("{0:0.00}", arrear);
                    sumToPay += arrear;
                }
                else if (arrear < 0)
                {
                    dataGridView1.Rows[index].Cells["hisArrear"].Style.ForeColor = Color.Red;
                    dataGridView1.Rows[index].Cells["hisArrear"].Style.SelectionForeColor = Color.Red;
                    dataGridView1.Rows[index].Cells["hisArrear"].Value = string.Format("{0:0.00}", -arrear);
                    sumToRecepit += (-arrear);
                }
            }

            index = dataGridView1.Rows.Add();

            dataGridView1.Rows[index].Cells["name"].Value = "�ϼ�";

            dataGridView1.Rows[index].Cells["myArrear"].Style.ForeColor = Color.Green;
            dataGridView1.Rows[index].Cells["myArrear"].Style.SelectionForeColor = Color.Green;
            dataGridView1.Rows[index].Cells["myArrear"].Value = string.Format("{0:0.00}", sumToPay);

            dataGridView1.Rows[index].Cells["hisArrear"].Style.ForeColor = Color.Red;
            dataGridView1.Rows[index].Cells["hisArrear"].Style.SelectionForeColor = Color.Red;
            dataGridView1.Rows[index].Cells["hisArrear"].Value = string.Format("{0:0.00}", sumToRecepit);

            dataGridView1.Rows[index].DefaultCellStyle.ForeColor = Color.Red;
            dataGridView1.Rows[index].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Rows[index].DefaultCellStyle.Font = new Font("����", 10F, FontStyle.Bold);
        }

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