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

        public override bool updateRecords(int ID, List<ProductCirculationRecord> records) {
            try
            {
                ProductStainlessCirculationRecordDao.getInstance().DeleteByCirculationID(ID);
                this.insertRecords(ID, records);
                return true;
            }
            catch {
                return false;
            }
        }

        //这个要不要考虑重构，把record都放在ProductCirculation里
        public override List<ProductCirculation> FindProductCirculationList(int typeStart, int typeEnd, DateTime? startTime, DateTime? endTime, int status, string name, int parent)
        {
            List<ProductCirculation> list = base.FindProductCirculationList(typeStart, typeEnd, startTime, endTime, status, name, parent);
            foreach (ProductCirculation cir in list)
            {
                cir.Records = this.getRecordDao().FindList(cir.ID);
            }
            return list;
        }
        
        public override ProductCirculation FindByID(int ID)
        {
            ProductCirculation pc = base.FindByID(ID);
            pc.Records = this.getRecordDao().FindList(ID);
            return pc;
        }
    }
}
