using LocalERP.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace LocalERP.DataAccess.DataDAO
{
    class ProductClothesDao
    {
        //singleton
        public static ProductClothesDao dao;
        public static ProductClothesDao getInstance()
        {
            if (dao == null)
                dao = new ProductClothesDao();
            return dao;
        }

        public int Insert(Product info, List<CharactorValue> colors, List<CharactorValue> sizes)
        {
            try
            {
                string commandText = string.Format("insert into Product(name, comment, parent, price) values('{0}', '{1}', {2}, {3})", info.Name, info.Comment, info.CategoryID, info.PricePurchase);
                DbHelperAccess.executeNonQuery(commandText);
                int productID = DbHelperAccess.executeMax("ID", "Product");
                setAttribute(productID, info, colors, sizes);
                return productID;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void setAttribute(int productID, Product info, List<CharactorValue> colors, List<CharactorValue> sizes)
        {
            string commandText = "";
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
                    commandText = string.Format("insert into ProductSKU(productID, composeID, price, colorID, colorName, sizeID, sizeName) values({0},'{1}',{2}, {3}, '{4}', {5}, '{6}')",
                        productID, composeID, info.PricePurchase, color.Id, color.Name, size.Id, size.Name);
                    DbHelperAccess.executeNonQuery(commandText);
                }
            }
        }

        private void delAttribute(int productID)
        {
            string commandText = string.Format("delete from ProductAttribute where productID = {0}", productID);
            DbHelperAccess.executeNonQuery(commandText);
            
            commandText = string.Format("delete from ProductSKU where productID = {0}", productID);
            DbHelperAccess.executeNonQuery(commandText);
        }

        public bool UpdateBasicInfo(Product info) {
            try
            {
                string commandText = string.Format("update Product set name='{0}', price={1}, comment='{2}', parent={3} where ID={4}",
                    info.Name, info.PricePurchase, info.Comment, info.CategoryID, info.ID);

                DbHelperAccess.executeNonQuery(commandText);
                return true;
            }
            catch (Exception ex) {
                return false;
            }
        }

        public bool UpdateAttributes(int productID, Product info, List<CharactorValue> colors, List<CharactorValue> sizes) {
            try
            {
                delAttribute(productID);
                setAttribute(productID, info, colors, sizes);
                return true;
            }
            catch(Exception ex) {
                return false;
            }
        }

        public DataTable FindList(Category parent, string name)
        {
            StringBuilder commandText = new StringBuilder("select * from Product, ProductCategory where Product.parent=ProductCategory.ID");
            if (parent != null)
                commandText.AppendFormat("  and ProductCategory.lft>={0} and ProductCategory.rgt<={1}", parent.Left, parent.Right);

            if (!string.IsNullOrEmpty(name))
                commandText.AppendFormat(" and Product.name like '%{0}%'", name);
            return DbHelperAccess.executeQuery(commandText.ToString());
        }

        public int UpdateNum(int id, int num)
        {
            string commandText = string.Format("update Product set num={0} where ID={1}",
                num, id);

            return DbHelperAccess.executeNonQuery(commandText);
        }

        public Product FindByID(int ID)
        {
            string commandText = string.Format("select * from Product where ID={0}", ID);
            DataRow dr = DbHelperAccess.executeQueryGetOneRow(commandText);
            Product product = new Product();
            if (dr != null)
            {
                product.ID = (int)dr["ID"];
                product.Name = dr["name"] as string;
                product.CategoryID = (int)dr["parent"];

                product.Comment = dr["comment"] as string;
                product.Num = (int)dr["num"];

                double price;
                double.TryParse(dr["price"].ToString(), out price);
                product.PricePurchase = price;

                return product;
            }
            return null;

        }

        public bool Delete(int id)
        {
            try
            {
                string commandText = string.Format("delete from Product where ID={0}", id);
                DbHelperAccess.executeNonQuery(commandText);
                return true;
            }
            catch(Exception ex) {
                throw ex;
                return false;
            }
        
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
        }

        //commented by stone
        public string getNumString(int productId) {
            List<ProductAttribute> colorAttrs = findAttributes(productId, 1);
            List<ProductAttribute> sizeAttrs = findAttributes(productId, 2);

            StringBuilder sb = new StringBuilder();
            string line = "";
            foreach (ProductAttribute color in colorAttrs)
            {
                line = "";
                foreach (ProductAttribute size in sizeAttrs)
                {
                    line += string.Format(" {0}/{1}:{2,-5}", color.CharactorValue.Name, size.CharactorValue.Name, ProductSKUDao.getInstance().FindNumByComposeID(productId, color.CharactorValueId, size.CharactorValueId));
                }
                sb.AppendLine(line);
            }
            return sb.ToString();
        }
    }
}
