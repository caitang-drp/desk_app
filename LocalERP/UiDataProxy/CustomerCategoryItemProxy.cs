using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using LocalERP.DataAccess.DataDAO;
using LocalERP.DataAccess.Data;
using LocalERP.WinForm;
using LocalERP.WinForm.Utility;
using System.Drawing;
using LocalERP.DataAccess.Utility;

namespace LocalERP.UiDataProxy
{
    public class CustomerCategoryItemProxy : CategoryItemProxy
    {

        public CustomerCategoryItemProxy()
        {
            this.CategoryTableName = "CustomerCategory";
            this.ItemName = "往来单位";
            this.UpdateType_Category = UpdateType.CustomerCategoryUpdate;
            this.UpdateType_Item = UpdateType.CustomerUpdate;
        }

        public override void initColumns(DataGridView dgv)
        {
            DataGridViewCheckBoxColumn check = new DataGridViewCheckBoxColumn();
            check.HeaderText = "选择";
            check.Name = "check";
            check.Width = 60;

            string[] columnTexts = new string[] { "ID", "名称", "类别", "手机","我方欠款" };
            string[] columnNames = new string[] { "ID", "name", "category", "phone", "arrear" };
            int[] columnLengths = new int[] { 80, 120, 100, 100, 140 };

            ControlUtility.initColumns(dgv, columnNames, columnTexts, columnLengths);
            dgv.Columns.Insert(0, check);
        }

        public override void initTree(TreeView tv)
        {
            tv.Nodes.Clear();
            List<Category> categorys = CategoryDao.getInstance().FindByParentId(this.CategoryTableName, -1);
            foreach (Category category in categorys)
            {
                tv.Nodes.Add(this.getNodeById(category.Id));
            }
            tv.ExpandAll();
            
            if (tv.Nodes.Count > 0 && tv.Nodes[0].IsSelected == false)
                tv.SelectedNode = tv.Nodes[0];
        }

        private TreeNode getNodeById(int id)
        {

            Category category = CategoryDao.getInstance().FindById(this.CategoryTableName, id);

            List<Category> childrenCategory = CategoryDao.getInstance().FindByParentId(this.CategoryTableName, category.Id);
            TreeNode[] childrenNode = null;
            if (childrenCategory != null && childrenCategory.Count > 0)
            {
                childrenNode = new TreeNode[childrenCategory.Count];
                for (int i = 0; i < childrenNode.Length; i++)
                    childrenNode[i] = getNodeById(childrenCategory[i].Id);
            }

            TreeNode node = null;
            if (childrenNode != null)
            {
                node = new TreeNode(category.Name, childrenNode);
                node.Name = category.Id.ToString();
            }
            else
            {
                node = new TreeNode(category.Name);
                node.Name = category.Id.ToString();

            }
            return node;
        }

        /*public override void initItems(DataGridView dataGridView1, int parentId)
        {
            dataGridView1.Rows.Clear();
            Category parent = CategoryDao.getInstance().FindById(this.CategoryTableName, parentId);
            DataTable dataTable = CustomerDao.getInstance().FindList(parent, null);
            foreach (DataRow dr in dataTable.Rows)
            {
                int index = dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells["ID"].Value = dr["Customer.ID"];
                dataGridView1.Rows[index].Cells["name"].Value = dr["Customer.name"];
                dataGridView1.Rows[index].Cells["category"].Value = dr["CustomerCategory.name"];
                dataGridView1.Rows[index].Cells["tel"].Value = dr["tel"];
                dataGridView1.Rows[index].Cells["phone"].Value = dr["phone"];
                dataGridView1.Rows[index].Cells["address"].Value = dr["address"];
                dataGridView1.Rows[index].Cells["comment"].Value = dr["comment"];
            }
        }*/

        public override DataTable getRecordsTable(int parentId, string name)
        {
            Category parent = null;
            if(parentId > 0)
                parent = CategoryDao.getInstance().FindById(this.CategoryTableName, parentId);
            
            DataTable dataTable = CustomerDao.getInstance().FindList(parent, name);
            return dataTable;
        }

        public override void initRecords(DataGridView dataGridView1, DataTable dataTable)
        {
            dataGridView1.Rows.Clear();
            foreach (DataRow dr in dataTable.Rows)
            {
                int index = dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells["ID"].Value = dr["Customer.ID"];
                dataGridView1.Rows[index].Cells["name"].Value = dr["Customer.name"];
                dataGridView1.Rows[index].Cells["category"].Value = dr["CustomerCategory.name"];
                dataGridView1.Rows[index].Cells["phone"].Value = dr["phone"];
                double arrear = 0;
                bool temp;
                ValidateUtility.getDouble(dr, "arrear", out arrear, out temp);
                if (arrear > 0)
                {
                    dataGridView1.Rows[index].Cells["arrear"].Style.ForeColor = Color.Green;
                    dataGridView1.Rows[index].Cells["arrear"].Style.SelectionForeColor = Color.Green;
                    dataGridView1.Rows[index].Cells["arrear"].Value = string.Format("+{0:0.00}", arrear);
                }
                else if (arrear < 0)
                {
                    dataGridView1.Rows[index].Cells["arrear"].Style.ForeColor = Color.Red;
                    dataGridView1.Rows[index].Cells["arrear"].Style.SelectionForeColor = Color.Red;
                    dataGridView1.Rows[index].Cells["arrear"].Value = string.Format("{0:0.00}", arrear);
                }else
                    dataGridView1.Rows[index].Cells["arrear"].Value = string.Format("{0:0.00}", arrear);
            }
        }

        public override void delItems(int id)
        {
            CustomerDao.getInstance().Delete(id);
        }

        public override MyDockContent getItemForm(Form owner, int openMode, int ID)
        {
            CustomerForm form = FormSingletonFactory.getInstance().getCustomerForm();
            form.Owner = owner;
            form.reload(openMode, ID);
            return form;
        }
    }

}
