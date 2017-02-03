using LocalERP.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace LocalERP.DataAccess.DataDAO
{
    class ProductJobRecordDao
    {
        public static ProductJobRecordDao dao;
        public static ProductJobRecordDao getInstance()
        {
            if (dao == null)
                dao = new ProductJobRecordDao();
            return dao;
        }

        public int Insert(ProductJobRecord info)
        {
            try
            {
                //string commandText = @"insert into Users(userName,userPassword,userLevel,userPhone,userAddress) values (
					                                     //?userName,?userPassword,?userLevel,?userPhone,?userAddress)";
                string commandText = string.Format("insert into ProductJobRecord(productID, num, jobID) values('{0}', '{1}', '{2}')",
                    info.ProductID, info.Number, info.JobID);

                return DbHelperAccess.executeNonQuery(commandText);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable FindList(int productJobID)
        {
            string commandText = string.Format("select * from ProductJobRecord where jobID = {0}", productJobID);
            return DbHelperAccess.executeQuery(commandText);
        }

        public int Update(ProductJobRecord info)
        {
            string commandText = string.Format("update ProductJobRecord set productID={0}, num={1}, arrivalNum={2}  where ID={3}", 
                info.ProductID, info.Number, info.ArrivalNum, info.ID);

            return DbHelperAccess.executeNonQuery(commandText);
        }

        public int UpdateArrivalNum(int ID, int arrivalNum)
        {
            string commandText = string.Format("update ProductJobRecord set arrivalNum={0}  where ID={1}",
                arrivalNum, ID);

            return DbHelperAccess.executeNonQuery(commandText);
        }

        public int DeleteByJobID(int jobID)
        {
            string commandText = string.Format("delete from ProductJobRecord where jobID={0}", jobID);

            return DbHelperAccess.executeNonQuery(commandText);
        }
    }
}
