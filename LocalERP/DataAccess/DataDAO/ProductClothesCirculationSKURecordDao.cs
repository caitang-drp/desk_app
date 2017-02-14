using LocalERP.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace LocalERP.DataAccess.DataDAO
{
    class ProductClothesCirculationSKURecordDao
    {
        public static ProductClothesCirculationSKURecordDao dao;
        private string temp;
        public static ProductClothesCirculationSKURecordDao getInstance()
        {
            if (dao == null)
                dao = new ProductClothesCirculationSKURecordDao();
            return dao;
        }

        public int Insert(ProductClothesCirculationSKURecord info)
        {
            try
            {
                string commandText = string.Format("insert into ProductCirculationSKURecord(productSKUID, num, price, recordID) values('{0}', '{1}', '{2}', '{3}')",
                    info.ProductSKUID, info.Num, info.Price, info.RecordID);

                return DbHelperAccess.executeNonQuery(commandText);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProductClothesCirculationSKURecord> FindList(int recordID)
        {
            List < ProductClothesCirculationSKURecord> records = new List<ProductClothesCirculationSKURecord>();

            string commandText = string.Format("select * from ProductCirculationSKURecord where recordID = {0}", recordID);
            DataTable dt = DbHelperAccess.executeQuery(commandText);
            foreach (DataRow dr in dt.Rows) {
                ProductClothesCirculationSKURecord record = new ProductClothesCirculationSKURecord();
                record.RecordID = recordID;
                record.ID = (int)dr["ID"];
                record.Num = (int)dr["num"];
                record.ProductSKUID = (int)dr["productSKUID"];
                record.ProductSKU = ProductSKUDao.getInstance().FindByID(record.ProductSKUID);

                records.Add(record);
            }
            return records;
        }

        public DataTable FindList(DateTime startTime, DateTime endTime, int type, string product, int productID, string customer, int customerID)
        {
            StringBuilder commandText = new StringBuilder();
            string temp = "select ProductCirculationSKURecord.*, ProductCirculationRecord.*, ProductCirculation.code, ProductCirculation.circulationTime, ProductCirculation.status, ProductCirculation.type, ProductCirculation.flowType, ProductSKU.colorName, ProductSKU.sizeName, Product.name, Customer.name"
                 + " from ProductCirculationSKURecord, ProductCirculationRecord, (select * from ProductCirculation left join Customer on Customer.ID = ProductCirculation.customerID ) circulation, ProductSKU, Product"
                 +" where ProductCirculationSKURecord.recordID = ProductCirculationRecord.ID"
                 +" and ProductCirculationRecord.circulationID = ProductCirculation.ID"
                 +" and ProductCirculationSKURecord.productSKUID = ProductSKU.ID"
                 +" and ProductSKU.productID = Product.ID"
                 +" and ProductCirculation.circulationTime between #{0}# and #{1}# and status = 4";
                
            commandText.Append(string.Format(temp, startTime.ToString("yyyy-MM-dd"), endTime.ToString("yyyy-MM-dd")));
            if (type > 0)
                commandText.Append(string.Format(" and ProductCirculation.type between {0} and {1}", type, type+1));
            
            if (productID > 0)
                commandText.Append(string.Format(" and Product.ID={0}", productID));
            else if(!string.IsNullOrEmpty(product))
                commandText.Append(string.Format(" and Product.name like '%{0}%'", product));

            if (customerID > 0)
                commandText.Append(string.Format(" and Customer.ID={0}", customerID));
            else if (!string.IsNullOrEmpty(customer))
                commandText.Append(string.Format(" and Customer.name like '%{0}%'", customer));
            
            commandText.Append(" order by ProductCirculationSKURecord.ID desc");
            return DbHelperAccess.executeQuery(commandText.ToString());
        }

        /*
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
        */
    }
}
