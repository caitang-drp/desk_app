using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using LocalERP.DataAccess.Data;
using LocalERP.DataAccess.DataDAO;
using System.IO;
using LocalERP.WinForm;

namespace LocalERP.UiDataProxy
{

    public class ProductCategoryItemProxy : CategoryItemProxy
    {
        public ProductCategoryItemProxy()
        {
            this.CategoryName = "Product";
            this.CategoryTableName = "ProductCategory";
            this.ItemName = "商品";
            this.UpdateType_Category = UpdateType.ProductCategoryUpdate;
            this.UpdateType_Item = UpdateType.ProductUpdate;
        }

        public override void initColumns(DataGridView dgv)
        {
            DataGridViewCheckBoxColumn check = new DataGridViewCheckBoxColumn();
            DataGridViewTextBoxColumn ID = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn name = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn category = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn price = new DataGridViewTextBoxColumn();

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
            name.HeaderText = "商品名称";
            name.Name = "name";
            name.ReadOnly = true;
            name.Width = 240;
            // 
            // category
            // 
            category.HeaderText = "类别";
            category.Name = "category";
            category.ReadOnly = true;
            category.Width = 120;
            // 
            // comment
            // 
            price.HeaderText = "价格/元";
            price.Name = "price";
            price.ReadOnly = true;
            price.Width = 100;

            dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                check,
                ID,
                name,
                category,
                price});
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

        public override DataTable getRecordsTable(int parentId, string name)
        {
            Category parent = null;
            if(parentId > 0)
                parent = CategoryDao.getInstance().FindById(this.CategoryTableName, parentId);
            
            DataTable dataTable = ProductClothesDao.getInstance().FindList(parent, name);
            return dataTable;
        }

        public override void initItems(DataGridView dataGridView1, int parentId)
        {
            dataGridView1.Rows.Clear();
            Category parent = CategoryDao.getInstance().FindById(this.CategoryTableName, parentId);
            DataTable dataTable = ProductDao.getInstance().FindList(parent);
            foreach (DataRow dr in dataTable.Rows)
            {
                int index = dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells["ID"].Value = dr["Product.ID"];
                dataGridView1.Rows[index].Cells["name"].Value = dr["Product.name"];
            }
        }

        public override void initRecords(DataGridView dataGridView1, DataTable dataTable)
        {
            dataGridView1.Rows.Clear();
            foreach (DataRow dr in dataTable.Rows)
            {
                int index = dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells["ID"].Value = dr["Product.ID"];
                dataGridView1.Rows[index].Cells["name"].Value = dr["Product.name"];
                dataGridView1.Rows[index].Cells["category"].Value = dr["ProductCategory.name"];
                dataGridView1.Rows[index].Cells["price"].Value = dr["price"];
            }
        }

        public override void delItems(int id)
        {
            ProductClothesDao.getInstance().Delete(id);
        }

        public override MyDockContent getItemForm(int openMode, int ID)
        {
            ProductClothesForm form = FormMgr.getInstance().getProductForm();
            form.reload(openMode, ID);
            return form;

        }
    }
}
