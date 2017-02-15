using LocalERP.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace LocalERP.DataAccess.DataDAO
{
    public abstract class ProductCirculationRecordDao
    {
        /*
        public int Insert(ProductClothesCirculationRecord info)
        {
            try
            {
                string commandText = string.Format("insert into ProductCirculationRecord(productID, totalNum, price, circulationID) values('{0}', '{1}', '{2}', '{3}')",
                    info.ProductID, info.TotalNum, info.Price, info.CirculationID);
                DbHelperAccess.executeNonQuery(commandText);
                int recordID = DbHelperAccess.executeLastID("ID", "ProductCirculationRecord");

                List<ProductClothesCirculationSKURecord> skuRecords = info.SkuRecords;
                ProductClothesCirculationSKURecordDao dao = new ProductClothesCirculationSKURecordDao();
                foreach (ProductClothesCirculationSKURecord record in skuRecords)
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
        }*/

        public abstract List<ProductCirculationRecord> FindList(int circulationID);
        /*
        public int FindCount(int productID)
        {
            string commandText = string.Format("select count(*) from ProductCirculationRecord where ProductCirculationRecord.productID = {0}", productID);
            int result = DbHelperAccess.executeQueryNum(commandText);
            return result;
        }


        public int Update(ProductSellRecord info)
        {
            string commandText = string.Format("update ProductSellRecord set productID={0}, num={1}, price={2} where ID={3}", 
                info.ProductID, info.Num, info.Price, info.ID);

            return DbHelperAccess.executeNonQuery(commandText);
        }

        public int DeleteByCirculationID(int circulationID)
        {
            string commandText = string.Format("delete from ProductCirculationRecord where circulationID={0}", circulationID);
            return DbHelperAccess.executeNonQuery(commandText);
        }*/
        
    }
}
