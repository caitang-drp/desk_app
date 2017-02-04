using LocalERP.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace LocalERP.DataAccess.DataDAO
{
    class ProductCirculationRecordDao
    {
        public static ProductCirculationRecordDao dao;
        public static ProductCirculationRecordDao getInstance()
        {
            if (dao == null)
                dao = new ProductCirculationRecordDao();
            return dao;
        }

        public int Insert(ProductCirculationRecord info)
        {
            try
            {
                string commandText = string.Format("insert into ProductCirculationRecord(productID, totalNum, price, circulationID) values('{0}', '{1}', '{2}', '{3}')",
                    info.ProductID, info.TotalNum, info.Price, info.CirculationID);
                DbHelperAccess.executeNonQuery(commandText);
                int recordID = DbHelperAccess.executeLastID("ID", "ProductCirculationRecord");

                List<ProductCirculationSKURecord> skuRecords = info.SkuRecords;
                ProductCirculationSKURecordDao dao = new ProductCirculationSKURecordDao();
                foreach (ProductCirculationSKURecord record in skuRecords)
                {
                    record.RecordID = recordID;
                    dao.Insert(record);
                }
                return recordID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProductCirculationRecord> FindList(int circulationID)
        {
            List<ProductCirculationRecord> records = new List<ProductCirculationRecord>();

            string commandText = string.Format("select * from ProductCirculationRecord, Product where ProductCirculationRecord.productID = Product.ID and circulationID = {0} order by ProductCirculationRecord.ID", circulationID);
            DataTable dt = DbHelperAccess.executeQuery(commandText);
            foreach (DataRow dr in dt.Rows) {
                ProductCirculationRecord record = new ProductCirculationRecord();
                record.CirculationID = circulationID;
                record.ID = (int)dr["ProductCirculationRecord.ID"];

                double price;
                double.TryParse(dr["ProductCirculationRecord.price"].ToString(), out price);
                record.Price = price;

                record.ProductID = (int)dr["Product.ID"];
                record.ProductName = dr["name"].ToString();

                record.TotalNum = (int)dr["totalNum"];
                records.Add(record);
            }
            return records;
        }

        public int FindCount(int productID)
        {
            string commandText = string.Format("select count(*) from ProductCirculationRecord where ProductCirculationRecord.productID = {0}", productID);
            int result = DbHelperAccess.executeQueryNum(commandText);
            return result;
        }

        /*
        public int Update(ProductSellRecord info)
        {
            string commandText = string.Format("update ProductSellRecord set productID={0}, num={1}, price={2} where ID={3}", 
                info.ProductID, info.Num, info.Price, info.ID);

            return DbHelperAccess.executeNonQuery(commandText);
        }*/

        public int DeleteByCirculationID(int circulationID)
        {
            string commandText = string.Format("delete from ProductCirculationRecord where circulationID={0}", circulationID);
            return DbHelperAccess.executeNonQuery(commandText);
        }
        
    }
}
