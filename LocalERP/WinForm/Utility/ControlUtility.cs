using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using LocalERP.WinForm;
using LocalERP.WinForm.Utility;
using System.Data;
using System.Drawing;
using LocalERP.DataAccess.Data;
using LocalERP.DataAccess.DataDAO;

namespace LocalERP.WinForm.Utility
{
    public static class ControlUtility
    {
        public static void initColumns(DataGridView dgv, string[] columnNames, string [] columnTexts, int [] columnLengths)
        {
            DataGridViewColumn[] columns = new DataGridViewColumn[columnNames.Length];
            for (int i = 0; i < columnNames.Length; i++)
            {
                columns[i] = new DataGridViewTextBoxColumn();
                columns[i].HeaderText = columnTexts[i];
                columns[i].Name = columnNames[i];
                columns[i].ReadOnly = true;
                columns[i].Width = columnLengths[i];
                if(columnLengths[i] == 0)
                    columns[i].Visible = false;
            }
            dgv.Columns.Clear();
            dgv.Columns.AddRange(columns);
        }

        public static void setCellWithColor(DataGridViewCell cell, Color color, string text) {
            cell.Style.ForeColor = color;
            cell.Style.SelectionForeColor = color;
            cell.Value = text;
        }

        public static void initTree(TreeView tv, string categoryTableName)
        {
            tv.Nodes.Clear();
            List<Category> categorys = CategoryDao.getInstance().FindByParentId(categoryTableName, -1);
            foreach (Category category in categorys)
            {
                tv.Nodes.Add(getNodeById(category.Id, categoryTableName));
            }
            tv.ExpandAll();

            if (tv.Nodes.Count > 0 && tv.Nodes[0].IsSelected == false)
                tv.SelectedNode = tv.Nodes[0];
        }

        private static TreeNode getNodeById(int id, string categoryTableName)
        {

            Category category = CategoryDao.getInstance().FindById(categoryTableName, id);

            List<Category> childrenCategory = CategoryDao.getInstance().FindByParentId(categoryTableName, category.Id);
            TreeNode[] childrenNode = null;
            if (childrenCategory != null && childrenCategory.Count > 0)
            {
                childrenNode = new TreeNode[childrenCategory.Count];
                for (int i = 0; i < childrenNode.Length; i++)
                    childrenNode[i] = getNodeById(childrenCategory[i].Id, categoryTableName);
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
    }
}
