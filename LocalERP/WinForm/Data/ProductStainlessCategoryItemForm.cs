using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using LocalERP.DataAccess.DataDAO;
using LocalERP.DataAccess.Data;
using LocalERP.WinForm.Utility;
using LocalERP.UiDataProxy;

namespace LocalERP.WinForm.Data
{
    public class ProductStainlessCategoryItemForm : CategoryItemForm
    {
        public ProductStainlessCategoryItemForm(int openMode, CategoryItemTypeConf conf, string title, Form parentForm)
            :base(openMode, conf, title, parentForm)
        {
        }

        protected override void initColumns()
        {
            DataGridViewCheckBoxColumn check = new DataGridViewCheckBoxColumn();
            check.HeaderText = "选择";
            check.Name = "check";
            check.Width = 60;

            DataGridViewTextBoxColumn ID = new DataGridViewTextBoxColumn();
            ID.HeaderText = "ID";
            ID.Name = "ID";
            ID.Visible = false;

            string[] columnTexts = new string[] { "货号", "名称", "类别", "停用", "采购价\r/元", "销售价\r/元", "成本价\r/元", "库存数量" };
            string[] columnNames = new string[] { "serial", "name", "category", "disable", "pricePurchase", "priceSell", "priceCost", "libNum" };
            int[] columnLengths = new int[] { 70, 120, 65, 50, 80, 80, 80, 100 };

            ControlUtility.initColumns(this.dataGridView1, columnNames, columnTexts, columnLengths, null);
            this.dataGridView1.Columns.Insert(0, check);
            this.dataGridView1.Columns.Insert(0, ID);
        }

        protected override void hideSomeColumnsForSelectMode()
        {
            dataGridView1.Columns["pricePurchase"].Visible = false;
            //dgv.Columns["priceSell"].Visible = false;
            dataGridView1.Columns["priceCost"].Visible = false;
            //因为在CategoryItemForm构造函数里，已经判断openMode为0时，才调用hideSomeColumns
            dataGridView1.Columns["disable"].Visible = false;
        }

        protected override DataTable getRecordsTable(int parentId, string searchName)
        {
            Category parent = null;
            if(parentId > 0)
                parent = CategoryDao.getInstance().FindById(conf.CategoryTableName, parentId);
            
            DataTable dataTable;
            if(this.openMode == 0)
                dataTable = ProductStainlessDao.getInstance().FindList(parent, searchName, true);
            else
                dataTable = ProductStainlessDao.getInstance().FindList(parent, searchName, false);
            return dataTable;
        }

        protected override void initRecords(DataTable dataTable)
        {
            dataGridView1.Rows.Clear();
            foreach (DataRow dr in dataTable.Rows)
            {
                int index = dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells["ID"].Value = dr["ProductStainless.ID"];
                dataGridView1.Rows[index].Cells["serial"].Value = dr["serial"];
                dataGridView1.Rows[index].Cells["name"].Value = dr["ProductStainless.name"];
                dataGridView1.Rows[index].Cells["category"].Value = dr["ProductStainlessCategory.name"];
                bool disable;
                bool.TryParse(dr["disable"].ToString(), out disable);
                dataGridView1.Rows[index].Cells["disable"].Value = disable ? "是" : "";
                dataGridView1.Rows[index].Cells["pricePurchase"].Value = dr["pricePurchase"];
                dataGridView1.Rows[index].Cells["priceCost"].Value = dr["priceCost"];
                dataGridView1.Rows[index].Cells["priceSell"].Value = dr["priceSell"];
                dataGridView1.Rows[index].Cells["libNum"].Value = dr["num"];
            }
        }

        protected override MyDockContent getItemForm(Form owner, int openMode, int ID)
        {
            MyDockContent form = FormSingletonFactory.getInstance().getProductForm();
            form.Owner = owner;
            (form as ProductStainlessForm).reload(openMode, ID);
            return form;
        }

        protected override void delItem(int id)
        {
            ProductStainlessDao.getInstance().Delete(id);
        }
    }
}
