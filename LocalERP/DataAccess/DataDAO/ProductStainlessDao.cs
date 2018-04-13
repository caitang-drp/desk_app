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
                    "insert into ProductStainless(serial, name, comment, parent, pricePurchase, priceCost, priceSell, unit, quantityPerPiece, disable) values('{0}', '{1}', '{2}', {3}, {4}, {5}, {6}, '{7}', {8}, {9})",
                    info.Serial, info.Name, info.Comment, info.CategoryID, info.PricePurchase, info.PriceCost, info.PriceSell, info.Unit, info.QuantityPerPiece, info.Disable);
                DbHelperAccess.executeNonQuery(commandText);
                int productID = DbHelperAccess.executeMax("ID", "ProductStainless");
                return productID;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool Update(ProductStainless info) {
            try
            {
                string commandText = string.Format("update ProductStainless set serial='{0}', name='{1}', comment='{2}', parent={3}, pricePurchase={4}, priceCost = {5}, priceSell={6}, unit='{7}', quantityPerPiece={8}, num={9}, disable={10} where ID={11}",
                    info.Serial, info.Name, info.Comment, info.CategoryID, info.PricePurchase, info.PriceCost, info.PriceSell, info.Unit, info.QuantityPerPiece, info.Num, info.Disable, info.ID);

                DbHelperAccess.executeNonQuery(commandText);
                return true;
            }
            catch (Exception ex) {
                return false;
            }
        }

        public DataTable FindList(Category parent, string name, bool notShowDisable)
        {
            StringBuilder commandText = new StringBuilder("select * from ProductStainless, ProductStainlessCategory where ProductStainless.parent=ProductStainlessCategory.ID");
            if (parent != null)
                commandText.AppendFormat("  and ProductStainlessCategory.lft>={0} and ProductStainlessCategory.rgt<={1}", parent.Left, parent.Right);

            if (!string.IsNullOrEmpty(name))
                commandText.AppendFormat(" and ( ProductStainless.name like '%{0}%' or ProductStainless.serial like '%{0}%')", name);
            if (notShowDisable == true)
                commandText.AppendFormat(" and ProductStainless.disable = false");

            commandText.Append(" order by ProductStainless.ID");
            
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
                product.Serial = dr["serial"] as string;
                product.CategoryID = (int)dr["parent"];

                product.QuantityPerPiece = (int)dr["quantityPerPiece"];
                product.Unit = dr["unit"] as string;

                double num;
                double.TryParse(dr["num"].ToString(), out num);
                product.Num = num;

                product.Comment = dr["comment"] as string;

                double pricePurchase, priceSell, priceCost;
                double.TryParse(dr["pricePurchase"].ToString(), out pricePurchase);
                product.PricePurchase = pricePurchase;

                double.TryParse(dr["priceSell"].ToString(), out priceSell);
                product.PriceSell = priceSell;

                double.TryParse(dr["priceCost"].ToString(), out priceCost);
                product.PriceCost = priceCost;

                bool disable;
                bool.TryParse(dr["disable"].ToString(), out disable);
                product.Disable = disable;

                return product;
            }
            return null;

        }

        public bool Delete(int id)
        {
            try
            {
                string commandText = string.Format("delete from ProductStainless where ID={0}", id);
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

        public double find_purchase_price_by_id(int ID)
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
        public int update_purchase_price_by_id(int id, double purchase_price)
        {
            string commandText = string.Format("update ProductStainless set pricePurchase={0} where ID={1}",
                purchase_price, id);

            return DbHelperAccess.executeNonQuery(commandText);
        }

        public void ClearAllNumAndCost()
        {
            string commandText = string.Format("update ProductStainless set num=0,  priceCost = pricePurchase");
            DbHelperAccess.executeNonQuery(commandText);
        }
    }
}
