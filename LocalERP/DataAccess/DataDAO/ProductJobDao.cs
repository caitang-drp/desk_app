using LocalERP.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace LocalERP.DataAccess.DataDAO
{
    class ProductJobDao
    {
        //singleton
        public static ProductJobDao dao;
        public static ProductJobDao getInstance()
        {
            if (dao == null)
                dao = new ProductJobDao();
            return dao;
        }

        public int Insert(ProductJob info, List<ProductJobRecord> records)
        {
            try
            {
                //string commandText = @"insert into Users(userName,userPassword,userLevel,userPhone,userAddress) values (
					                                     //?userName,?userPassword,?userLevel,?userPhone,?userAddress)";

                string commandText = string.Format("insert into ProductJob(name, jobTime, comment, status) values('{0}', '{1}', '{2}', '{3}')", info.Name, info.JobTime, info.Comment, info.Status);

                DbHelperAccess.executeNonQuery(commandText);

                int ProductJobID = DbHelperAccess.executeLastID("ID", "ProductJob");

                ProductJobRecordDao dao = new ProductJobRecordDao();
                foreach (ProductJobRecord record in records){
                    record.JobID = ProductJobID;
                    dao.Insert(record);
                }
                return ProductJobID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int UpdateStatus(int jobID, int status)
        {
            string commandText = string.Format("update ProductJob set status = {0} where ID={1}",
                    status, jobID);

            return DbHelperAccess.executeNonQuery(commandText);
        }

        public void Update(ProductJob info, List<ProductJobRecord> records)
        {
            string commandText = string.Format("update ProductJob set name='{0}', jobTime='{1}', comment='{2}' where ID={3}",
                info.Name, info.JobTime, info.Comment, info.ID);

            DbHelperAccess.executeNonQuery(commandText);

            ProductJobRecordDao.getInstance().DeleteByJobID(info.ID);

            foreach (ProductJobRecord record in records)
            {
                record.JobID = info.ID;
                ProductJobRecordDao.getInstance().Insert(record);
            }
        }

        public DataTable FindList(DateTime startTime, DateTime endTime, int status)
        {
            StringBuilder commandText = new StringBuilder(string.Format("select * from ProductJob where jobTime between #{0}# and #{1}# ", startTime.ToString("yyyy-MM-dd"), endTime.ToString("yyyy-MM-dd")));
            if (status > 0 && status <= 4)
                commandText.Append(string.Format(" and status = {0}", status));
            commandText.Append(" order by ID desc");
            return DbHelperAccess.executeQuery(commandText.ToString());
        }


        public ProductJob FindByID(int ID)
        {
            string commandText = string.Format("select * from ProductJob where ID={0}", ID);
            DataRow dr = DbHelperAccess.executeQueryGetOneRow(commandText);
            ProductJob job = new ProductJob(); 
            if (dr != null) {
                job.ID = (int)dr["ID"];
                job.Name = dr["name"] as string;
                job.JobTime = (DateTime)dr["jobTime"];
                job.Status = (int)dr["status"];
                job.Comment = dr["comment"] as string;
                return job;
            }
            return null;
            
        }

        public int DeleteByID(int ID) {
            string commandText = string.Format("delete from ProductJob where ID={0}", ID);
            return DbHelperAccess.executeNonQuery(commandText);
        }

    }
}
