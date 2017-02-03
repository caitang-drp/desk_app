using LocalERP.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace LocalERP.DataAccess.DataDAO
{
    class ProductCirculationDao
    {
        //singleton
        public static ProductCirculationDao dao;
        public static ProductCirculationDao getInstance()
        {
            if (dao == null)
                dao = new ProductCirculationDao();
            return dao;
        }

        public int Insert(ProductCirculation info, List<ProductCirculationRecord> records)
        {
            try
            {
                string commandText = string.Format("insert into ProductCirculation(code, circulationTime, comment, status, customerID, type, flowType, operator) values('{0}', '{1}', '{2}', '{3}', {4}, {5}, {6}, '{7}')", 
                    info.Code, info.CirculationTime, info.Comment, info.Status, info.CustomerID<=0?"null":info.CustomerID.ToString(), info.Type, info.FlowType, info.Oper);
                DbHelperAccess.executeNonQuery(commandText);
                int ProductCirculationID = DbHelperAccess.executeLastID("ID", "ProductCirculation");
                this.insertRecords(ProductCirculationID, records);
                return ProductCirculationID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void insertRecords(int ID, List<ProductCirculationRecord> records)
        {
            try
            {
                foreach (ProductCirculationRecord record in records)
                {
                    record.CirculationID = ID;
                    ProductCirculationRecordDao.getInstance().Insert(record);
                }
            }
            catch (Exception ex){
                throw ex;
            }
        }


        public int UpdateStatus(int ID, int status)
        {
            string commandText = string.Format("update ProductCirculation set status = {0} where ID={1}",
                    status, ID);

            return DbHelperAccess.executeNonQuery(commandText);
        }

        public int UpdatePay(int ID, double pay, double payed)
        {
            string commandText = string.Format("update ProductCirculation set pay = {0}, payed={1} where ID={2}",
                    pay, payed, ID);

            return DbHelperAccess.executeNonQuery(commandText);
        }

        public void UpdateBaiscInfo(ProductCirculation info)
        {
            string commandText = string.Format("update ProductCirculation set code='{0}', circulationTime='{1}', comment='{2}', customerID={3}, operator='{4}' where ID={5}",
                info.Code, info.CirculationTime, info.Comment, info.CustomerID <= 0 ? "null" : info.CustomerID.ToString(), info.Oper, info.ID);

            DbHelperAccess.executeNonQuery(commandText);
        }

        public bool updateRecords(int ID, List<ProductCirculationRecord> records) {
            try
            {
                ProductCirculationRecordDao.getInstance().DeleteByCirculationID(ID);
                this.insertRecords(ID, records);
                return true;
            }
            catch {
                return false;
            }
        }

        public DataTable FindList(int type, DateTime startTime, DateTime endTime, int status, string customerName)
        {
            StringBuilder commandText = null;
            if(type < 3)
                commandText = new StringBuilder(string.Format("select ProductCirculation.*, Customer.name from ProductCirculation, Customer where Customer.ID = ProductCirculation.customerID and circulationTime between #{0}# and #{1}# ", startTime.ToString("yyyy-MM-dd"), endTime.ToString("yyyy-MM-dd")));
            else if(type == 3)
                commandText = new StringBuilder(string.Format("select ProductCirculation.* from ProductCirculation where circulationTime between #{0}# and #{1}# ", startTime.ToString("yyyy-MM-dd"), endTime.ToString("yyyy-MM-dd")));
            if(type > 0)
                commandText.Append(string.Format(" and type between {0} and {1}", type*2-1, type*2));
            if (status > 0)
                commandText.Append(string.Format(" and status = {0}", status));
            if (!string.IsNullOrEmpty(customerName))
                commandText.Append(string.Format(" and Customer.name like '%{0}%'", customerName));

            commandText.Append(" order by ProductCirculation.ID desc");
            return DbHelperAccess.executeQuery(commandText.ToString());
        }

        public ProductCirculation FindByID(int ID)
        {
            string commandText = string.Format("select * from ProductCirculation where ID={0}", ID);
            DataRow dr = DbHelperAccess.executeQueryGetOneRow(commandText);
            ProductCirculation sell = new ProductCirculation(); 
            if (dr != null) {
                sell.ID = (int)dr["ID"];
                sell.Code = dr["code"] as string;
                sell.CirculationTime = (DateTime)dr["circulationTime"];
                sell.Comment = dr["comment"] as string;
                sell.Status = (int)dr["status"];
                
                int customerID = 0;
                if(int.TryParse(dr["customerID"].ToString(), out customerID))
                    sell.CustomerID = customerID;
                
                sell.Oper = dr["operator"] as string;
                double pay = 0, payed;
                if (double.TryParse(dr["pay"].ToString(), out pay))
                    sell.Pay = pay;
                if (double.TryParse(dr["payed"].ToString(), out payed))
                    sell.Payed = payed;
                //not reasonal
                if(customerID > 0)
                    sell.CustomerName = CustomerDao.getInstance().FindByID(sell.CustomerID).Name;
                return sell;
            }
            return null;
            
        }

        public int getMaxCode(string code)
        {
            string commandText = string.Format("select max(code) from ProductCirculation where code like '{0}-{1}-%'", code, DateTime.Now.ToString("yyyyMMdd"));
            DataRow dr = DbHelperAccess.executeQueryGetOneRow(commandText);
            string result = dr[0] as string;
            if (string.IsNullOrEmpty(result))
                return 0;
            else
            {
                int max = 0;
                if (int.TryParse(result.Substring(result.LastIndexOf('-')), out max))
                    return Math.Abs(max);
                else
                    return 0;
            }
        }

        public int DeleteByID(int ID) {
            string commandText = string.Format("delete from ProductCirculation where ID={0}", ID);
            return DbHelperAccess.executeNonQuery(commandText);
        }
        
    }
}
