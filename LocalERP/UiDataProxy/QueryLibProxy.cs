using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using LocalERP.DataAccess.Data;
using LocalERP.DataAccess.DataDAO;
using System.Data;
using LocalERP.WinForm;
using LocalERP.WinForm.Utility;

namespace LocalERP.UiDataProxy
{
    public class QueryLibProxy : ProductCategoryItemProxy
    {
        public QueryLibProxy()
        {
            this.CategoryTableName = "ProductCategory";
        }

        public override void initColumns(DataGridView dgv)
        {
            DataGridViewCheckBoxColumn check = new DataGridViewCheckBoxColumn();
            DataGridViewTextBoxColumn ID = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn name = new DataGridViewTextBoxColumn();
            DataGridViewResizeTextBoxColumn num = new DataGridViewResizeTextBoxColumn();
            DataGridViewTextBoxColumn totalNum = new DataGridViewTextBoxColumn();

            check.HeaderText = "选择";
            check.Name = "check";
            check.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            check.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            check.Width = 60;
            check.Visible = false;
            // 
            // ID
            // 
            ID.HeaderText = "商品ID";
            ID.Name = "ID";
            ID.ReadOnly = true;
            ID.Width = 60;
            // 
            // name
            // 
            name.HeaderText = "商品名称";
            name.Name = "name";
            name.ReadOnly = true;
            name.Width = 140;
            // 
            // comment
            // 
            num.HeaderText = "库存数量";
            num.Name = "num";
            num.ReadOnly = true;
            num.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                check,
                ID,
                name,
                num});
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
            DataTable dataTable = ProductClothesDao.getInstance().FindList(parent, null);
            foreach (DataRow dr in dataTable.Rows)
            {
                int index = dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells["ID"].Value = dr["Product.ID"];
                dataGridView1.Rows[index].Cells["name"].Value = dr["Product.name"];
                DataGridViewResizeTextBoxCell numCell = dataGridView1.Rows[index].Cells["num"] as DataGridViewResizeTextBoxCell;
                numCell.Value = ProductClothesDao.getInstance().getNumString((int)(dr["Product.ID"]));
                numCell.resetSize(dataGridView1.Rows[index].Cells["num"].Value.ToString());
            }
        }*/

        public override DataTable getRecordsTable(int parentId, string name)
        {
            Category parent = CategoryDao.getInstance().FindById(this.CategoryTableName, parentId);
            DataTable dataTable = ProductClothesDao.getInstance().FindList(parent, name);
            dataTable.Columns.Add("numText");
            foreach (DataRow dr in dataTable.Rows)
                dr["numText"] = ProductClothesDao.getInstance().getNumString((int)(dr["Product.ID"]));
            return dataTable;
        }

        public override void initRecords(DataGridView dataGridView1, DataTable dataTable)
        {
            dataGridView1.Rows.Clear();
            foreach (DataRow dr in dataTable.Rows)
            {
                int index = dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells["ID"].Value = dr["Product.ID"];
                dataGridView1.Rows[index].Cells["name"].Value = dr["Product.name"];
                DataGridViewResizeTextBoxCell numCell = dataGridView1.Rows[index].Cells["num"] as DataGridViewResizeTextBoxCell;
                numCell.Value = dr["numText"].ToString();
                numCell.resetSize(numCell.Value.ToString());
            }
        }

        public override MyDockContent getItemForm(int openMode, int ID)
        {
            return null;
        }
    }
}
