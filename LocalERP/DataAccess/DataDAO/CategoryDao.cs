using LocalERP.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

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

                int categoryID = DbHelperAccess.executeLastID("ID", tableName);

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
                string parent = dr["parent"] as string;
                if (parent != null && parent != "")
                    category.Parent = int.Parse(parent);
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
            string commandText = string.Format("select * from {0} where parent{1}",tableName, idString);
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
        
    }
}
