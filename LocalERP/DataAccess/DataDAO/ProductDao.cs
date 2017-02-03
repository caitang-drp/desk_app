using LocalERP.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace LocalERP.DataAccess.DataDAO
{
    class ProductDao
    {
        //singleton
        public static ProductDao dao;
        public static ProductDao getInstance()
        {
            if (dao == null)
                dao = new ProductDao();
            return dao;
        }

        public int Insert(Product info, List<int> elementIDs)
        {
            try
            {
                //string commandText = @"insert into Users(userName,userPassword,userLevel,userPhone,userAddress) values (
					                                     //?userName,?userPassword,?userLevel,?userPhone,?userAddress)";

                string commandText = string.Format("insert into Product(name, comment) values('{0}', '{1}')", info.Name, info.Comment);
                DbHelperAccess.executeNonQuery(commandText);
                int productID = DbHelperAccess.executeLastID("ID", "Product");
                foreach (int elementID in elementIDs)
                {
                    commandText = string.Format("insert into ProductElement(productID, elementID) values('{0}', '{1}')", productID, elementID);
                    DbHelperAccess.executeNonQuery(commandText);
                }
                return productID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public int Insert_Clothes(Product info, List<CharactorValue> colors, List<CharactorValue> sizes)
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
                        commandText = string.Format("insert into ProductSKU(productID, composeID, price, colorID, colorName, sizeID, sizeName) values({0},'{1}',{2}, {3}, '{4}', {5}, '{6}')",
                            productID, composeID, info.Price, color.Id, color.Name, size.Id, size.Name);
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
                product.Price = price;

                return product;
            }
            return null;

        }

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
    }
}
