using LocalERP.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace LocalERP.DataAccess.DataDAO
{
    class ProductSellRecordDao
    {
        public static ProductSellRecordDao dao;
        public static ProductSellRecordDao getInstance()
        {
            if (dao == null)
                dao = new ProductSellRecordDao();
            return dao;
        }

        public int Insert(ProductSellRecord info)
        {
            try
            {
                //string commandText = @"insert into Users(userName,userPassword,userLevel,userPhone,userAddress) values (
					                                     //?userName,?userPassword,?userLevel,?userPhone,?userAddress)";
                string commandText = string.Format("insert into ProductSellRecord(productID, num, price, sellID) values('{0}', '{1}', '{2}', '{3}')",
                    info.ProductID, info.Num, info.Price, info.SellID);

                return DbHelperAccess.executeNonQuery(commandText);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable FindList(int sellID)
        {
            string commandText = string.Format("select * from ProductSellRecord where sellID = {0}", sellID);
            return DbHelperAccess.executeQuery(commandText);
        }

        public int Update(ProductSellRecord info)
        {
            string commandText = string.Format("update ProductSellRecord set productID={0}, num={1}, price={2} where ID={3}", 
                info.ProductID, info.Num, info.Price, info.ID);

            return DbHelperAccess.executeNonQuery(commandText);
        }

        public int DeleteBySellID(int sellID)
        {
            string commandText = string.Format("delete from ProductSellRecord where sellID={0}", sellID);

            return DbHelperAccess.executeNonQuery(commandText);
        }

    }
}
