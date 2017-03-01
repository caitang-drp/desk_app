//create by stone 2017-02-07: 用于将ProductStainless应用于CategoryItem窗口

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

    public class ProductStainlessCategoryItemProxy : CategoryItemProxy
    {
        public ProductStainlessCategoryItemProxy()
        {
            this.CategoryName = "ProductStainless";
            this.CategoryTableName = "ProductStainlessCategory";
            this.ItemName = "货品名称";
            this.UpdateType_Category = UpdateType.ProductCategoryUpdate;
            this.UpdateType_Item = UpdateType.ProductUpdate;
        }

        public override void initColumns(DataGridView dgv)
        {
            DataGridViewCheckBoxColumn check = new DataGridViewCheckBoxColumn();
            DataGridViewTextBoxColumn ID = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn serial = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn name = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn category = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn pricePurchase = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn priceSell = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn libNum = new DataGridViewTextBoxColumn();

            check.HeaderText = "选择";
            check.Name = "check";
            check.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            check.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            check.Width = 70;
            // 
            // ID
            // 
            ID.HeaderText = "ID";
            ID.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ID.Name = "ID";
            ID.ReadOnly = true;
            ID.Width = 40;

            serial.HeaderText = "货号";
            serial.Name = "serial";
            serial.ReadOnly = true;
            serial.Width = 100;
            // 
            // name
            // 
            name.HeaderText = "货品名称";
            name.Name = "name";
            name.ReadOnly = true;
            name.Width = 160;
            // 
            // category
            // 
            category.HeaderText = "类别";
            category.Name = "category";
            category.ReadOnly = true;
            category.Width = 90;
            // 
            // comment
            // 
            pricePurchase.HeaderText = "采购价格/元";
            pricePurchase.Name = "pricePurchase";
            pricePurchase.ReadOnly = true;
            pricePurchase.Width = 115;

            priceSell.HeaderText = "销售价格/元";
            priceSell.Name = "priceSell";
            priceSell.ReadOnly = true;
            priceSell.Width = 115;

            libNum.HeaderText = "库存数量";
            libNum.Name = "libNum";
            libNum.ReadOnly = true;
            libNum.Width = 90;

            dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                check,
                ID,
                serial,
                name,
                category,
                pricePurchase,
                priceSell,
                libNum});
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
            
            DataTable dataTable = ProductStainlessDao.getInstance().FindList(parent, name);
            return dataTable;
        }

        /*
        public override void initItems(DataGridView dataGridView1, int parentId)
        {
            dataGridView1.Rows.Clear();
            Category parent = CategoryDao.getInstance().FindById(this.CategoryTableName, parentId);
            DataTable dataTable = ProductStainlessDao.getInstance().FindList(parent, null);
            foreach (DataRow dr in dataTable.Rows)
            {
                int index = dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells["ID"].Value = dr["ProductStainless.ID"];
                dataGridView1.Rows[index].Cells["name"].Value = dr["ProductStainless.name"];
            }
        }*/

        //初始化列表
        public override void initRecords(DataGridView dataGridView1, DataTable dataTable)
        {
            dataGridView1.Rows.Clear();
            foreach (DataRow dr in dataTable.Rows)
            {
                int index = dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells["ID"].Value = dr["ProductStainless.ID"];
                dataGridView1.Rows[index].Cells["serial"].Value = dr["serial"];
                dataGridView1.Rows[index].Cells["name"].Value = dr["ProductStainless.name"];
                dataGridView1.Rows[index].Cells["category"].Value = dr["ProductStainlessCategory.name"];
                dataGridView1.Rows[index].Cells["pricePurchase"].Value = dr["pricePurchase"];
                dataGridView1.Rows[index].Cells["priceSell"].Value = dr["priceSell"];
                dataGridView1.Rows[index].Cells["libNum"].Value = dr["num"];
            }
        }

        public override void delItems(int id)
        {
            ProductStainlessDao.getInstance().Delete(id);
        }

        public override MyDockContent getItemForm(Form owner, int openMode, int ID)
        {
            MyDockContent form = FormSingletonFactory.getInstance().getProductForm();
            form.Owner = owner;
            (form as ProductStainlessForm).reload(openMode, ID);
            return form;

        }
    }
}
