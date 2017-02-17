using LocalERP.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace LocalERP.DataAccess.DataDAO
{
    class ProductStainlessDao : ProductDao
    {
        //singleton
        public static ProductStainlessDao dao;
        public static ProductStainlessDao getInstance()
        {
            if (dao == null)
                dao = new ProductStainlessDao();
            return dao;
        }

        public int Insert(ProductStainless info)
        {
            try
            {
                string commandText = string.Format(
                    "insert into ProductStainless(serial, name, comment, parent, pricePurchase, priceSell, unit, quantityPerPiece) values('{0}', '{1}', '{2}', {3}, {4}, {5}, '{6}', {7})",
                    info.Serial, info.Name, info.Comment, info.CategoryID, info.PricePurchase, info.PriceSell, info.Unit, info.QuantityPerPiece);
                DbHelperAccess.executeNonQuery(commandText);
                int productID = DbHelperAccess.executeLastID("ID", "ProductStainless");
                return productID;
            }
            catch (Exception ex)
            {
                throw ex;
            }

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

        public DataTable FindList(Category parent, string name)
        {
            StringBuilder commandText = new StringBuilder("select * from ProductStainless, ProductStainlessCategory where ProductStainless.parent=ProductStainlessCategory.ID");
            if (parent != null)
                commandText.AppendFormat("  and ProductStainlessCategory.lft>={0} and ProductStainlessCategory.rgt<={1}", parent.Left, parent.Right);

            if (!string.IsNullOrEmpty(name))
                commandText.AppendFormat(" and ProductStainless.name like '%{0}%'", name);
            return DbHelperAccess.executeQuery(commandText.ToString());
        }

        public override int FindNumByID(int ID)
        {
            string commandText = string.Format("select num from ProductStainless where ID={0}", ID);
            DataRow dr = DbHelperAccess.executeQueryGetOneRow(commandText);
            int result = 0;
            if (dr != null)
            {
                string str = dr[0].ToString();
                int.TryParse(str, out result);
            }
            return result;
        }

        public override int UpdateNum(int id, int num)
        {
            string commandText = string.Format("update ProductStainless set num={0} where ID={1}",
                num, id);

            return DbHelperAccess.executeNonQuery(commandText);
        }

        public ProductStainless FindByID(int ID)
        {
            string commandText = string.Format("select * from ProductStainless where ID={0}", ID);
            DataRow dr = DbHelperAccess.executeQueryGetOneRow(commandText);
            ProductStainless product = new ProductStainless();
            if (dr != null)
            {
                product.ID = (int)dr["ID"];
                product.Name = dr["name"] as string;
                product.CategoryID = (int)dr["parent"];

                product.QuantityPerPiece = (int)dr["quantityPerPiece"];
                product.Unit = dr["unit"] as string;
                product.Num = (int)dr["num"];

                product.Comment = dr["comment"] as string;

                double pricePurchase, priceSell;
                double.TryParse(dr["pricePurchase"].ToString(), out pricePurchase);
                product.PricePurchase = pricePurchase;

                double.TryParse(dr["priceSell"].ToString(), out priceSell);
                product.PriceSell = priceSell;

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
            string commandText = string.Format("select pricePurchase from ProductStainless where ID={0}", ID);
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
