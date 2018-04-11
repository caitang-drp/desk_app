using LocalERP.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace LocalERP.DataAccess.DataDAO
{
    class CategoryDao
    {
        //singleton
        public static CategoryDao dao;
        public static CategoryDao getInstance()
        {
            if (dao == null)
                dao = new CategoryDao();
            return dao;
        }

        public int Insert(string tableName, Category parent, Category right, Category info)
        {
            try
            {
                int point=0;
                string parentId=null;
                if (right != null){
                    point = right.Right;
                    parentId = right.Parent.ToString();
                }
                else if (parent != null){
                    point = parent.Right - 1; // parent.right is changed to right+1
                    parentId = parent.Id.ToString();
                }

                if(parentId == null || parentId == "" || parentId == "0")
                    parentId = "null";

                string commandText = string.Format("update {0} set {0}.lft={0}.lft+2 where lft>{1}", tableName, point);
                DbHelperAccess.executeNonQuery(commandText);

                commandText = string.Format("update {0} set {0}.rgt={0}.rgt+2 where rgt>{1}", tableName, point);
                DbHelperAccess.executeNonQuery(commandText);

                commandText = string.Format("insert into {0}(name, parent, lft, rgt) values('{1}', {2}, {3}, {4})", 
                    tableName, info.Name, parentId, point+1, point+2);
                DbHelperAccess.executeNonQuery(commandText);

                int categoryID = DbHelperAccess.executeMax("ID", tableName);

                return categoryID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int UpdateName(string tableName, int ID, string name)
        {
            string commandText = string.Format("update {0} set name='{1}' where ID={2}",tableName, name, ID);
            return DbHelperAccess.executeNonQuery(commandText);
        }

        public bool UpdateParent(string tableName, Category node, Category preParent, Category newParent) {
            if (preParent == null && newParent == null || preParent!=null && newParent!=null && preParent.Id == newParent.Id)
                return false;

            string commandText = string.Format("update {0} set parent={1} where ID={2}", tableName, newParent==null?"null":newParent.Id.ToString(), node.Id);
            DbHelperAccess.executeNonQuery(commandText);

            int newParentRight = 0;
            int nodeSpan = node.Right - node.Left + 1;
            if (newParent != null)
            {
                newParentRight = newParent.Right;

                //先腾出位置，包括newParent的right，也包括node
                commandText = string.Format("update {0} set {0}.lft={0}.lft+{1} where lft>={2}", tableName, nodeSpan, newParentRight);
                DbHelperAccess.executeNonQuery(commandText);

                commandText = string.Format("update {0} set {0}.rgt={0}.rgt+{1} where rgt>={2}", tableName, nodeSpan, newParentRight);
                DbHelperAccess.executeNonQuery(commandText);
            }
            else {
                int maxRight = DbHelperAccess.executeMax("rgt", tableName);
                newParentRight = maxRight + 1;
            }

            //刷新下node1
            node = this.FindById(tableName, node.Id);
            //node的left从newParentRight开始
            int offset = newParentRight - node.Left;
            //更新下node2
            commandText = string.Format("update {0} set {0}.rgt={0}.rgt+{1}, {0}.lft={0}.lft+{1} where lft>={2} and lft<={3}", tableName, offset, node.Left, node.Right);
            DbHelperAccess.executeNonQuery(commandText);

            //用node1来判断，右边的才需要删除位置
            commandText = string.Format("update {0} set {0}.lft={0}.lft-{1} where lft>{2}", tableName, nodeSpan, node.Right);
            DbHelperAccess.executeNonQuery(commandText);

            commandText = string.Format("update {0} set {0}.rgt={0}.rgt-{1} where rgt>{2}", tableName, nodeSpan, node.Right);
            DbHelperAccess.executeNonQuery(commandText);

            return true;

        }

        //仅限于同级相邻两个节点，而且node比preNode大
        public bool nodeUp(string tableName, Category node, Category preNode) {
            //先把node挪开，要不等下会重叠
            int maxRight = DbHelperAccess.executeMax("rgt", tableName);
            maxRight++;
            int offset = maxRight - node.Left;
            string commandText = string.Format("update {0} set {0}.rgt={0}.rgt+{1}, {0}.lft={0}.lft+{1} where lft>={2} and lft<={3}", tableName, offset, node.Left, node.Right);
            DbHelperAccess.executeNonQuery(commandText);


            //把preNode移过去新位置
            int span = node.Right - node.Left + 1;
            commandText = string.Format("update {0} set {0}.rgt={0}.rgt+{1}, {0}.lft={0}.lft+{1} where lft>={2} and lft<={3}", tableName, span, preNode.Left, preNode.Right);
            DbHelperAccess.executeNonQuery(commandText);

            node = this.FindById(tableName, node.Id);
            span = preNode.Right - preNode.Left + 1 + offset;
            commandText = string.Format("update {0} set {0}.rgt={0}.rgt-{1}, {0}.lft={0}.lft-{1} where lft>={2} and lft<={3}", tableName, span, node.Left, node.Right);
            DbHelperAccess.executeNonQuery(commandText);

            return true;
        }

        //
        public bool DeleteDeaf(string tableName, int id)
        {
            Category category = this.FindById(tableName, id);

            //not leaf node
            if (category.Right > category.Left + 1)
                return false;

            int point = category.Right;
            try
            {
                string commandText = string.Format("delete from {0} where ID={1}", tableName, id);
                DbHelperAccess.executeNonQuery(commandText);

                commandText = string.Format("update {0} set {0}.lft={0}.lft-2 where lft>{1}", tableName, point);
                DbHelperAccess.executeNonQuery(commandText);

                commandText = string.Format("update {0} set {0}.rgt={0}.rgt-2 where rgt>{1}", tableName, point);
                DbHelperAccess.executeNonQuery(commandText);
            }
            catch {
                return false;
            }
            return true;
        }
 /*
        public DataTable FindList()
        {
            string commandText = "select * from Customer";
            return DbHelperAccess.executeQuery(commandText);
        }
        */

        public Category FindById(string tableName, int ID) {
            string commandText = string.Format("select * from {0} where ID = {1}", tableName, ID);
            DataRow dr = DbHelperAccess.executeQueryGetOneRow(commandText);

            if (dr != null)
            {
                Category category = new Category();
                category.Id = (int)dr["ID"];
                category.Name = dr["name"] as string;
                try { category.Parent = (int)dr["parent"]; }
                catch { }
                category.Left = (int)dr["lft"];
                category.Right = (int)dr["rgt"];
                return category;
            }
            return null;

        }

        public List<Category> FindByParentId(string tableName, int ID)
        {
            string idString = "="+ID.ToString();
            if (ID < 0)
                idString = " is null";
            string commandText = string.Format("select * from {0} where parent{1} order by lft",tableName, idString);
            DataTable dt = DbHelperAccess.executeQuery(commandText);
            
            List<Category> categorys = new List<Category>();

            foreach (DataRow dr in dt.Rows) {
                Category category = new Category();
                category.Id = (int)dr["ID"];
                category.Name = dr["name"] as string;
                string parent = dr["parent"] as string;
                if (parent != null && parent != "")
                    category.Parent = int.Parse(parent);
                category.Left = (int)dr["lft"];
                category.Right = (int)dr["rgt"];
                categorys.Add(category);
            }

            return categorys;
        }

        public void initTreeView(string tableName, TreeView tv)
        {
            tv.Nodes.Clear();
            List<Category> categorys = CategoryDao.getInstance().FindByParentId(tableName, -1);
            foreach (Category category in categorys)
            {
                tv.Nodes.Add(this.getNodeById(tableName, category.Id));
            }
            tv.ExpandAll();

            if (tv.Nodes.Count > 0 && tv.Nodes[0].IsSelected == false)
                tv.SelectedNode = tv.Nodes[0];
        }

        private TreeNode getNodeById(string tableName, int id)
        {

            Category category = CategoryDao.getInstance().FindById(tableName, id);

            List<Category> childrenCategory = CategoryDao.getInstance().FindByParentId(tableName, category.Id);
            TreeNode[] childrenNode = null;
            if (childrenCategory != null && childrenCategory.Count > 0)
            {
                childrenNode = new TreeNode[childrenCategory.Count];
                for (int i = 0; i < childrenNode.Length; i++)
                    childrenNode[i] = getNodeById(tableName, childrenCategory[i].Id);
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
