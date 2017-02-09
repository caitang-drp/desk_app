using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using LocalERP.DataAccess.DataDAO;
using LocalERP.DataAccess.Data;
using LocalERP.WinForm;

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
            DataGridViewTextBoxColumn ID = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn name = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn category = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn tel = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn phone = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn address = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn comment = new DataGridViewTextBoxColumn();

            check.HeaderText = "选择";
            check.Name = "check";
            check.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            check.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            check.Width = 60;
            // 
            // ID
            // 
            ID.HeaderText = "ID";
            ID.Name = "ID";
            ID.ReadOnly = true;
            ID.Width = 60;
            // 
            // name
            // 
            name.HeaderText = "名称";
            name.Name = "name";
            name.ReadOnly = true;
            // 
            // category
            // 
            category.HeaderText = "类别";
            category.Name = "category";
            category.ReadOnly = true;
            category.Width = 120;
            // 
            // tel
            // 
            tel.HeaderText = "电话";
            tel.Name = "tel";
            tel.ReadOnly = true;
            tel.Width = 120;
            // 
            // phone
            // 
            phone.HeaderText = "手机";
            phone.Name = "phone";
            phone.Width = 120;
            // 
            // address
            // 
            address.HeaderText = "地址";
            address.Name = "address";
            address.Width = 160;
            // 
            // comment
            // 
            comment.HeaderText = "备注";
            comment.Name = "comment";
            comment.ReadOnly = true;

            dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                check, ID, name, category, tel, phone, address, comment});
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
                dataGridView1.Rows[index].Cells["tel"].Value = dr["tel"];
                dataGridView1.Rows[index].Cells["phone"].Value = dr["phone"];
                dataGridView1.Rows[index].Cells["address"].Value = dr["address"];
                dataGridView1.Rows[index].Cells["comment"].Value = dr["comment"];
            }
        }

        public override void delItems(int id)
        {
            CustomerDao.getInstance().Delete(id);
        }

        public override MyDockContent getItemForm(int openMode, int ID)
        {
            CustomerForm form = FormMgr.getInstance().getCustomerForm();
            form.reload(openMode, ID);
            return form;
        }
    }

}
