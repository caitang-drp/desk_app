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
                string commandText = string.Format("insert into {0}(code, circulationTime, comment, status, customerID, type, flowType, arrearDirection, total, backFreightPerPiece, realTotal, previousArrears, thisPayed, freight, operator, lastPayReceipt) values('{1}','{2}', '{3}', '{4}', {5}, {6}, {7}, {8}, {9},{10},{11},{12},{13},{14},'{15}', '{16}')",
                    tableName, info.Code, info.CirculationTime, info.Comment, info.Status, info.CustomerID <= 0 ? "null" : info.CustomerID.ToString(), info.Type, info.FlowType, info.ArrearDirection, info.Total, info.BackFreightPerPiece, info.RealTotal, info.PreviousArrears, info.ThisPayed, info.Freight, info.Oper, info.LastPayReceipt);
                DbHelperAccess.executeNonQuery(commandText);
                ProductCirculationID = DbHelperAccess.executeMax("ID", tableName);
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
            string commandText = string.Format("update {0} set code='{1}', circulationTime='{2}', comment='{3}', customerID={4}, total={5}, backFreightPerPiece={6}, realTotal={7}, previousArrears={8}, thisPayed ={9}, freight={10}, operator='{11}', lastPayReceipt='{12}' where ID={13}",
                tableName, info.Code, info.CirculationTime, info.Comment, info.CustomerID <= 0 ? "null" : info.CustomerID.ToString(), info.Total, info.BackFreightPerPiece, info.RealTotal, info.PreviousArrears, info.ThisPayed, info.Freight, info.Oper, info.LastPayReceipt, info.ID);

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

            commandText.Append(string.Format(" order by {0}.circulationTime desc", tableName));
            return DbHelperAccess.executeQuery(commandText.ToString());
        }

        private ProductCirculation formatProductCirculation(DataRow dr) {
            ProductCirculation circulation = new ProductCirculation();
            if (dr != null)
            {
                circulation.ID = (int)dr["ID"];
                circulation.Code = dr["code"] as string;
                circulation.CirculationTime = (DateTime)dr["circulationTime"];
                circulation.Comment = dr["comment"] as string;
                circulation.Status = (int)dr["status"];
                circulation.Type = (int)dr["type"];

                circulation.FlowType = (int)dr["flowType"];
                circulation.ArrearDirection = (int)dr["arrearDirection"];

                int customerID = 0;
                if (int.TryParse(dr["customerID"].ToString(), out customerID))
                    circulation.CustomerID = customerID;

                circulation.Oper = dr["operator"] as string;
                circulation.LastPayReceipt = dr["lastPayReceipt"] as string;

                double total, backFreightPerPiece, realTotal, previousArrears, thisPayed, freight;

                if (double.TryParse(dr["total"].ToString(), out total))
                    circulation.Total = total;
                if (double.TryParse(dr["backFreightPerPiece"].ToString(), out backFreightPerPiece))
                    circulation.BackFreightPerPiece = backFreightPerPiece;
                if (double.TryParse(dr["realTotal"].ToString(), out realTotal))
                    circulation.RealTotal = realTotal;
                if (double.TryParse(dr["previousArrears"].ToString(), out previousArrears))
                    circulation.PreviousArrears = previousArrears;
                if (double.TryParse(dr["thisPayed"].ToString(), out thisPayed))
                    circulation.ThisPayed = thisPayed;
                if (double.TryParse(dr["freight"].ToString(), out freight))
                    circulation.Freight = freight;
                try
                {
                    circulation.CustomerName = dr["name"] as string;
                }
                catch { }
                return circulation;
            }
            return null;
        }

        public virtual ProductCirculation FindByID(int ID)
        {
            string commandText = string.Format("select {0}.*, Customer.name from {0} left join Customer on Customer.ID = {0}.customerID where {0}.ID={1}", tableName, ID);
            DataRow dr = DbHelperAccess.executeQueryGetOneRow(commandText);
            return this.formatProductCirculation(dr);
        }

        public ProductCirculation FindLastestByCustomerID(int customerID, bool thisPayNotNull)
        {
            ////模仿FindByID，所以left join customer，其实可以不要
            string commandText = string.Format("select * from {0} where circulationTime = (SELECT max(circulationTime) from {0} where customerID={1} and status=4)", tableName, customerID);
            if(thisPayNotNull)
                commandText = string.Format("select * from {0} where circulationTime = (SELECT max(circulationTime) from {0} where customerID={1} and status=4 and thisPayed <>0 )", tableName, customerID);
            DataRow dr = DbHelperAccess.executeQueryGetOneRow(commandText);
            return this.formatProductCirculation(dr);
        }

        public ProductCirculation FindLastestAccReceiptZero(int customerID)
        {
            string commandText = string.Format("select {0}.*, Customer.name from {0} left join Customer on Customer.ID = {0}.customerID where Customer.ID={1} and (arrearDirection * previousArrears + flowType * (realTotal - thisPayed)) * arrearDirection = 0", tableName, customerID);
            DataRow dr = DbHelperAccess.executeQueryGetOneRow(commandText);
            return this.formatProductCirculation(dr);
        }

        public virtual List<ProductCirculation> FindProductCirculationList(int typeStart, int typeEnd, DateTime? startTime, DateTime? endTime, int status, string name, int parent)
        {
            //要注意，这个语句会筛选掉没有Customer信息的
            StringBuilder commandText = new StringBuilder(string.Format("select {0}.*, Customer.name from {0} left join Customer on {0}.customerID = Customer.ID where 1=1 ", tableName));
            if(typeStart > 0 && typeEnd >0 && typeStart <= typeEnd)
                commandText.Append(string.Format(" and type between {0} and {1}", typeStart, typeEnd));
            if(startTime!=null && endTime!=null)
                commandText.Append(string.Format(" and circulationTime between #{0}# and #{1}# ", startTime.Value.ToString("yyyy-MM-dd"), endTime.Value.ToString("yyyy-MM-dd")));
            if (status > 0)
                commandText.Append(string.Format(" and status = {0}", status));
            if (!string.IsNullOrEmpty(name))
                commandText.AppendFormat(" and Customer.name like '%{0}%'", name);

            if (parent > 0)
                commandText.Append(string.Format(" and Customer.ID={0}", parent));

            DataTable dt = DbHelperAccess.executeQuery(commandText.ToString());
            List<ProductCirculation> list = new List<ProductCirculation>();
            foreach (DataRow pr in dt.Rows)
            {
                list.Add(this.formatProductCirculation(pr));
            }
            return list;
        }

        public int getMaxCode(string code)
        {
            string commandText = string.Format("select max(code) from {0} where code like '{1}%'", tableName, code);
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

        public int DeleteAll()
        {
            string commandText = string.Format("delete from {0}", tableName);
            return DbHelperAccess.executeNonQuery(commandText);
        }
    }
}
