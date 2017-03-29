using LocalERP.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using LocalERP.DataAccess.Utility;

namespace LocalERP.DataAccess.DataDAO
{
    public abstract class ProductCirculationDao
    {
        private string tableName;

        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        public abstract ProductCirculationRecordDao getRecordDao();
        public abstract ProductDao getProductDao();

        public abstract bool Insert(ProductCirculation info, List<ProductCirculationRecord> records, out int ProductCirculationID);
        public abstract bool updateRecords(int ID, List<ProductCirculationRecord> records); 

        public bool Insert(ProductCirculation info, out int ProductCirculationID)
        {
            ProductCirculationID = 0;
            try
            {
                string commandText = string.Format("insert into {0}(code, circulationTime, comment, status, customerID, type, flowType, arrearDirection, total, realTotal, previousArrears, thisPayed, freight, operator) values('{1}','{2}', '{3}', '{4}', {5}, {6}, {7}, {8}, {9}, {10},{11},{12},{13},'{14}')",
                    tableName, info.Code, info.CirculationTime, info.Comment, info.Status, info.CustomerID <= 0 ? "null" : info.CustomerID.ToString(), info.Type, info.FlowType, info.ArrearDirection, info.Total, info.RealTotal, info.PreviousArrears, info.ThisPayed, info.Freight, info.Oper);
                DbHelperAccess.executeNonQuery(commandText);
                ProductCirculationID = DbHelperAccess.executeLastID("ID", tableName);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public int UpdateStatus(int ID, int status)
        {
            string commandText = string.Format("update {0} set status = {1} where ID={2}",
                    tableName, status, ID);

            return DbHelperAccess.executeNonQuery(commandText);
        }

        public void UpdateBaiscInfo(ProductCirculation info)
        {
            string commandText = string.Format("update {0} set code='{1}', circulationTime='{2}', comment='{3}', customerID={4}, total={5}, realTotal={6}, previousArrears={7}, thisPayed ={8}, freight={9}, operator='{10}' where ID={11}",
                tableName, info.Code, info.CirculationTime, info.Comment, info.CustomerID <= 0 ? "null" : info.CustomerID.ToString(), info.Total, info.RealTotal, info.PreviousArrears, info.ThisPayed, info.Freight, info.Oper, info.ID);

            DbHelperAccess.executeNonQuery(commandText);
        }

        public DataTable FindList(int type, DateTime startTime, DateTime endTime, int status, string customerName)
        {
            StringBuilder commandText = null;
            if(type < 3)
                commandText = new StringBuilder(string.Format("select {0}.*, Customer.name from {0}, Customer where Customer.ID = {0}.customerID and circulationTime between #{1}# and #{2}# ",tableName, startTime.ToString("yyyy-MM-dd"), endTime.ToString("yyyy-MM-dd")));
            else if(type >= 3)
                commandText = new StringBuilder(string.Format("select {0}.* from {0} where circulationTime between #{1}# and #{2}# ", tableName, startTime.ToString("yyyy-MM-dd"), endTime.ToString("yyyy-MM-dd")));
            if(type > 0)
                commandText.Append(string.Format(" and type between {0} and {1}", type*2-1, type*2));
            if (status > 0)
                commandText.Append(string.Format(" and status = {0}", status));
            if (!string.IsNullOrEmpty(customerName))
                commandText.Append(string.Format(" and Customer.name like '%{0}%'", customerName));

            commandText.Append(string.Format(" order by {0}.ID desc", tableName));
            return DbHelperAccess.executeQuery(commandText.ToString());
        }

        public ProductCirculation FindByID(int ID)
        {
            string commandText = string.Format("select {0}.*, Customer.name from {0} left join Customer on Customer.ID = {0}.customerID where {0}.ID={1}", tableName, ID);
            DataRow dr = DbHelperAccess.executeQueryGetOneRow(commandText);
            ProductCirculation circulation = new ProductCirculation(); 
            if (dr != null) {
                circulation.ID = (int)dr["ID"];
                circulation.Code = dr["code"] as string;
                circulation.CirculationTime = (DateTime)dr["circulationTime"];
                circulation.Comment = dr["comment"] as string;
                circulation.Status = (int)dr["status"];
                circulation.Type = (int)dr["type"];

                circulation.FlowType = (int)dr["flowType"];
                circulation.ArrearDirection = (int)dr["arrearDirection"];

                int customerID = 0;
                if(int.TryParse(dr["customerID"].ToString(), out customerID))
                    circulation.CustomerID = customerID;
                
                circulation.Oper = dr["operator"] as string;

                double total, realTotal, previousArrears, thisPayed, freight;

                if (double.TryParse(dr["total"].ToString(), out total))
                    circulation.Total = total;
                if (double.TryParse(dr["realTotal"].ToString(), out realTotal))
                    circulation.RealTotal = realTotal;
                if (double.TryParse(dr["previousArrears"].ToString(), out previousArrears))
                    circulation.PreviousArrears = previousArrears;
                if (double.TryParse(dr["thisPayed"].ToString(), out thisPayed))
                    circulation.ThisPayed = thisPayed;
                if (double.TryParse(dr["freight"].ToString(), out freight))
                    circulation.Freight = freight;

                circulation.CustomerName = dr["name"] as string;
                return circulation;
            }
            return null;
            
        }

        public int getMaxCode(string code)
        {
            string commandText = string.Format("select max(code) from {0} where code like '{1}-{2}-%'", tableName, code, DateTime.Now.ToString("yyyyMMdd"));
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
            string commandText = string.Format("delete from {0} where ID={1}", tableName, ID);
            return DbHelperAccess.executeNonQuery(commandText);
        }

    }
}
