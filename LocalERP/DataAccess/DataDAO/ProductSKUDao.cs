using LocalERP.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace LocalERP.DataAccess.DataDAO
{
    class ProductSKUDao
    {
        //singleton
        public static ProductSKUDao dao;
        public static ProductSKUDao getInstance()
        {
            if (dao == null)
                dao = new ProductSKUDao();
            return dao;
        }

        /*
        public int Insert(Product info, List<CharactorValue> colors, List<CharactorValue> sizes)
        {
            try
            {
                string commandText = string.Format("insert into Product(name, comment, parent) values('{0}', '{1}', 19)", info.Name, info.Comment);
                DbHelperAccess.executeNonQuery(commandText);
                int productID = DbHelperAccess.executeLastID("ID", "Product");
                foreach (CharactorValue color in colors)
                {
                    commandText = string.Format("insert into ProductAttribute(productID, charactorID, charactorValueID) values({0},{1},{2})", productID, color.CharactorId, color.Id);
                    DbHelperAccess.executeNonQuery(commandText);
                }
                foreach (CharactorValue size in sizes)
                {
                    commandText = string.Format("insert into ProductAttribute(productID, charactorID, charactorValueID) values({0},{1},{2})", productID, size.CharactorId, size.Id);
                    DbHelperAccess.executeNonQuery(commandText);
                }
                foreach (CharactorValue color in colors)
                {
                    foreach (CharactorValue size in sizes)
                    {
                        string composeID = string.Format("{0}:{1};{2}:{3}", color.CharactorId, color.Id, size.CharactorId, size.Id);
                        commandText = string.Format("insert into ProductSKU(productID, composeID, price) values({0},'{1}',{2})", 
                            productID, composeID, info.Price);
                        DbHelperAccess.executeNonQuery(commandText);
                    }
                }
                return productID;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void Update(Product info, List<int> elementIDs) {
            string commandText = string.Format("update Product set name='{0}', price='{1}', comment='{2}' where ID={3}", 
                info.Name, info.Price, info.Comment, info.ID);

            DbHelperAccess.executeNonQuery(commandText);

            commandText = string.Format("delete from ProductElement where productID={0}", info.ID);
            DbHelperAccess.executeNonQuery(commandText);

            foreach (int elementID in elementIDs)
            {
                commandText = string.Format("insert into ProductElement(productID, elementID) values('{0}', '{1}')", info.ID, elementID);
                DbHelperAccess.executeNonQuery(commandText);
            }
        }

        public DataTable FindList(Category parent)
        {
            string commandText = "select * from Product";
            if (parent != null)
                commandText = string.Format("select * from Product, ProductCategory where Product.parent=ProductCategory.ID and ProductCategory.lft>={0} and ProductCategory.rgt<={1}", parent.Left, parent.Right);
            return DbHelperAccess.executeQuery(commandText);
        }


        */

        public ProductSKU FindByComposeID(int productID, int colorID, int sizeID)
        {
            string composeID = string.Format("1:{0};2:{1}", colorID, sizeID);
            string commandText = string.Format("select * from ProductSKU where productID={0} and composeID='{1}'", productID, composeID);
            DataRow dr = DbHelperAccess.executeQueryGetOneRow(commandText);
            ProductSKU product = new ProductSKU();
            if (dr != null)
            {
                product.ID = (int)dr["ID"];
                product.Color = CharactorValueDao.getInstance().findById(colorID);// dr["name"] as string;
                product.Size = CharactorValueDao.getInstance().findById(sizeID);
                return product;
            }
            return null;
        }

        public ProductSKU FindByID(int ID)
        {
            string commandText = string.Format("select * from ProductSKU where ID={0}", ID);
            DataRow dr = DbHelperAccess.executeQueryGetOneRow(commandText);
            ProductSKU product = new ProductSKU();
            if (dr != null)
            {
                product.ID = (int)dr["ID"];
                string composeIDs = dr["composeID"] as string;
                string [] composeIDstrs = composeIDs.Split(new char [] {':',';'});
                int colorID = int.Parse(composeIDstrs[1]);
                int sizeID = int.Parse(composeIDstrs[3]);
                product.Color = CharactorValueDao.getInstance().findById(colorID);// dr["name"] as string;
                product.Size = CharactorValueDao.getInstance().findById(sizeID);
                return product;
            }
            return null;
        }

        public int FindNumByID(int ID)
        {
            string commandText = string.Format("select num from ProductSKU where ID={0}", ID);
            DataRow dr = DbHelperAccess.executeQueryGetOneRow(commandText);
            int result = 0;
            if (dr != null)
            {
                string str = dr[0].ToString();
                int.TryParse(str, out result);
            }
            return result;
        }

        public int FindNumByComposeID(int productID, int colorID, int sizeID)
        {
            string composeID = string.Format("1:{0};2:{1}", colorID, sizeID);
            string commandText = string.Format("select num from ProductSKU where productID={0} and composeID='{1}'", productID, composeID);
            DataRow dr = DbHelperAccess.executeQueryGetOneRow(commandText);
            int result = 0;
            if (dr != null)
            {
                string str = dr[0].ToString();
                int.TryParse(str, out result);
            }
            return result;
        }

        public int UpdateNum(int id, int num)
        {
            string commandText = string.Format("update ProductSKU set num={0} where ID={1}",
                num, id);

            return DbHelperAccess.executeNonQuery(commandText);
        }

        /*
        public int Delete(int id)
        {
            string commandText = string.Format("delete from Product where ID={0}", id);
            return DbHelperAccess.executeNonQuery(commandText);
        }

        public double FindPriceByID(int ID)
        {
            string commandText = string.Format("select price from Product where ID={0}", ID);
            DataRow dr = DbHelperAccess.executeQueryGetOneRow(commandText);
            double result = 0;
            if (dr != null)
            {
                string str = dr[0].ToString();
                double.TryParse(str, out result);
            }
            return result;
        }

        public List<ProductAttribute> findAttributes(int productID, int charactorID) {
            string commandText = string.Format("select * from ProductAttribute where productID={0} and charactorID={1}", productID, charactorID);
            DataTable dt = DbHelperAccess.executeQuery(commandText);
            List<ProductAttribute> attrs = new List<ProductAttribute>();
            foreach(DataRow dr in dt.Rows)
            {
                ProductAttribute attr = new ProductAttribute();
                attr.ID = (int)dr["ID"];
                attr.CharactorId = (int)dr["charactorID"];
                attr.CharactorValueId = (int)dr["charactorValueId"];
                //commented by stone: here can be improved for performance
                attr.CharactorValue = CharactorValueDao.getInstance().findById(attr.CharactorValueId);
                attrs.Add(attr);
            }
            return attrs;
        }*/
    }
}
