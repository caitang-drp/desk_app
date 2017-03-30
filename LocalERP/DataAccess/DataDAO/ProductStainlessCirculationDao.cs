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

        /*
        public DataTable FindList(Category parent, string name, bool thisPayed, bool finish)
        {
            StringBuilder commandText = new StringBuilder("select * from ProductStainlessCirculation, Customer, CustomerCategory where ProductStainlessCirculation.customerID = Customer.ID and Customer.parent = CustomerCategory.ID");
            if (thisPayed == true)
                commandText.AppendFormat(" and thisPayed > 0");
            if (finish == true)
                commandText.AppendFormat(" and status = 4 ");
            if (parent != null)
                commandText.AppendFormat(" and CustomerCategory.lft>={0} and CustomerCategory.rgt<={1}", parent.Left, parent.Right);

            if (!string.IsNullOrEmpty(name))
                commandText.AppendFormat(" and Customer.name like '%{0}%'", name);
            return DbHelperAccess.executeQuery(commandText.ToString());
        }*/
    }
}
