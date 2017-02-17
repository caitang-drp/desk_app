using LocalERP.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace LocalERP.DataAccess.DataDAO
{
    public class ProductStainlessCirculationDao : ProductCirculationDao
    {
        //singleton
        public static ProductStainlessCirculationDao dao;
        public static ProductStainlessCirculationDao getInstance()
        {
            if (dao == null)
            {
                dao = new ProductStainlessCirculationDao();
                dao.TableName = "ProductStainlessCirculation";
            }
            return dao;
        }

        public override ProductCirculationRecordDao getRecordDao()
        {
            return ProductStainlessCirculationRecordDao.getInstance();
        }

        public override ProductDao getProductDao()
        {
            return ProductStainlessDao.getInstance();
        }

        public override bool Insert(ProductCirculation info, List<ProductCirculationRecord> records, out int ProductCirculationID)
        {
            ProductCirculationID = 0;
            try
            {
                this.Insert(info, out ProductCirculationID);
                this.insertRecords(ProductCirculationID, records);
                return true;
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
                foreach (ProductStainlessCirculationRecord record in records)
                {
                    record.CirculationID = ID;
                    ProductStainlessCirculationRecordDao.getInstance().Insert(record);
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

        public void updateRecords(int ID, List<ProductCirculationRecord> records) {
            //try
            //{
                ProductStainlessCirculationRecordDao.getInstance().DeleteByCirculationID(ID);
                this.insertRecords(ID, records);
            //    return true;
            //}
            //catch {
            //    return false;
            //}
        }

        public int DeleteByID(int ID) {
            string commandText = string.Format("delete from ProductCirculation where ID={0}", ID);
            return DbHelperAccess.executeNonQuery(commandText);
        }
        
    }
}
