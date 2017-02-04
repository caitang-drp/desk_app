using LocalERP.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace LocalERP.DataAccess.DataDAO
{
    class ProductSellDao
    {
        //singleton
        public static ProductSellDao dao;
        public static ProductSellDao getInstance()
        {
            if (dao == null)
                dao = new ProductSellDao();
            return dao;
        }

        public int Insert(ProductSell info, List<ProductSellRecord> records)
        {
            try
            {
                //string commandText = @"insert into Users(userName,userPassword,userLevel,userPhone,userAddress) values (
                //?userName,?userPassword,?userLevel,?userPhone,?userAddress)";

                string commandText = string.Format("insert into ProductSell(name, sellTime, comment, status, customerID) values('{0}', '{1}', '{2}', '{3}', {4})", info.Name, info.SellTime, info.Comment, info.Status, info.CustomerID);

                DbHelperAccess.executeNonQuery(commandText);

                int ProductSellID = DbHelperAccess.executeLastID("ID", "ProductSell");

                ProductSellRecordDao dao = new ProductSellRecordDao();
                foreach (ProductSellRecord record in records)
                {
                    record.SellID = ProductSellID;
                    dao.Insert(record);
                }
                return ProductSellID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int UpdateStatus(int sellID, int status)
        {
            string commandText = string.Format("update ProductSell set status = {0} where ID={1}",
                    status, sellID);

            return DbHelperAccess.executeNonQuery(commandText);
        }

        public void Update(ProductSell info, List<ProductSellRecord> records)
        {
            string commandText = string.Format("update ProductSell set name='{0}', sellTime='{1}', comment='{2}', customerID='{3}' where ID={4}",
                info.Name, info.SellTime, info.Comment, info.CustomerID, info.ID);

            DbHelperAccess.executeNonQuery(commandText);

            ProductSellRecordDao.getInstance().DeleteBySellID(info.ID);

            foreach (ProductSellRecord record in records)
            {
                record.SellID = info.ID;
                ProductSellRecordDao.getInstance().Insert(record);
            }
        }

        public DataTable FindList(DateTime startTime, DateTime endTime, int status, int customerID)
        {
            StringBuilder commandText = new StringBuilder(string.Format("select ProductSell.*, Customer.name from ProductSell, Customer where Customer.ID = ProductSell.customerID and sellTime between #{0}# and #{1}# ", startTime.ToString("yyyy-MM-dd"), endTime.ToString("yyyy-MM-dd")));
            if (status > 0 && status <= 2)
                commandText.Append(string.Format(" and status = {0}", status));
            if (customerID > 0)
                commandText.Append(string.Format(" and customerID={0}", customerID));

            commandText.Append(" order by ProductSell.ID desc");
            return DbHelperAccess.executeQuery(commandText.ToString());
        }


        public ProductSell FindByID(int ID)
        {
            string commandText = string.Format("select * from ProductSell where ID={0}", ID);
            DataRow dr = DbHelperAccess.executeQueryGetOneRow(commandText);
            ProductSell sell = new ProductSell(); 
            if (dr != null) {
                sell.ID = (int)dr["ID"];
                sell.Name = dr["name"] as string;
                sell.SellTime = (DateTime)dr["sellTime"];
                sell.Comment = dr["comment"] as string;
                sell.Status = (int)dr["status"];
                sell.CustomerID = (int)dr["customerID"];
                return sell;
            }
            return null;
            
        }

        public int DeleteByID(int ID) {
            string commandText = string.Format("delete from ProductSell where ID={0}", ID);
            return DbHelperAccess.executeNonQuery(commandText);
        }

    }
}
