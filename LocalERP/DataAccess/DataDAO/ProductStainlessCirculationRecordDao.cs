using LocalERP.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using LocalERP.DataAccess.Utility;

namespace LocalERP.DataAccess.DataDAO
{
    class ProductStainlessCirculationRecordDao : ProductCirculationRecordDao
    {
        public static ProductStainlessCirculationRecordDao dao;
        public static ProductStainlessCirculationRecordDao getInstance()
        {
            if (dao == null)
                dao = new ProductStainlessCirculationRecordDao();
            return dao;
        }

        public int Insert(ProductStainlessCirculationRecord info)
        {
            try
            {
                string commandText = string.Format("insert into ProductStainlessCirculationRecord(productID, quantityPerPiece, pieces, totalNum, unit, price, totalPrice, circulationID) values('{0}', {1}, {2}, '{3}','{4}', '{5}', '{6}', '{7}')",
                    info.ProductID, info.QuantityNull ? "NULL" : info.QuantityPerPiece.ToString(), info.PiecesNull ? "NULL" : info.Pieces.ToString(), info.TotalNum, info.Unit, info.Price, info.TotalPrice, info.CirculationID);
                DbHelperAccess.executeNonQuery(commandText);
                int recordID = DbHelperAccess.executeLastID("ID", "ProductStainlessCirculationRecord");
                return recordID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override List<ProductCirculationRecord> FindList(int circulationID)
        {
            List<ProductCirculationRecord> records = new List<ProductCirculationRecord>();

            string commandText = string.Format("select * from ProductStainlessCirculationRecord, ProductStainless where ProductStainlessCirculationRecord.productID = ProductStainless.ID and circulationID = {0} order by ProductStainlessCirculationRecord.ID", circulationID);
            DataTable dt = DbHelperAccess.executeQuery(commandText);
            foreach (DataRow dr in dt.Rows) {
                bool tempBool = false;

                ProductStainlessCirculationRecord record = new ProductStainlessCirculationRecord();
                record.CirculationID = circulationID;
                record.ID = (int)dr["ProductStainlessCirculationRecord.ID"];

                int quantityPerPiece, pieces;
                double price, totalPrice;

                double.TryParse(dr["price"].ToString(), out price);
                record.Price = price;

                record.Serial = dr["serial"].ToString();
                record.ProductID = (int)dr["ProductStainless.ID"];
                record.ProductName = dr["name"].ToString();

                ValidateUtility.getInt(dr, "ProductStainlessCirculationRecord.quantityPerPiece", out quantityPerPiece, out tempBool);
                record.QuantityPerPiece = quantityPerPiece;
                record.QuantityNull = tempBool;

                ValidateUtility.getInt(dr, "pieces", out pieces, out tempBool);
                record.Pieces = pieces;
                record.PiecesNull = tempBool;

                record.Unit = dr["ProductStainlessCirculationRecord.unit"].ToString();

                record.TotalNum = (int)dr["totalNum"];

                ValidateUtility.getDouble(dr, "totalPrice", out totalPrice, out tempBool);
                record.TotalPrice = totalPrice;

                records.Add(record);
            }
            return records;
        }

        public DataTable FindList(DateTime startTime, DateTime endTime, int type, string product, int productID, string customer, int customerID)
        {
            StringBuilder commandText = new StringBuilder();
            string temp = "select ProductStainlessCirculationRecord.*, ProductStainlessCirculation.code, ProductStainlessCirculation.circulationTime, ProductStainlessCirculation.status, ProductStainlessCirculation.type, ProductStainlessCirculation.flowType, ProductStainless.serial, ProductStainless.name, Customer.name"
                 + " from ProductStainlessCirculationRecord, (select * from ProductStainlessCirculation left join Customer on Customer.ID = ProductStainlessCirculation.customerID ) circulation, ProductStainless"
                 + " where ProductStainlessCirculationRecord.circulationID = ProductStainlessCirculation.ID"
                 + " and ProductStainlessCirculationRecord.productID = ProductStainless.ID"
                 + " and ProductStainlessCirculation.circulationTime between #{0}# and #{1}# and status = 4";

            commandText.Append(string.Format(temp, startTime.ToString("yyyy-MM-dd"), endTime.ToString("yyyy-MM-dd")));
            if (type > 0)
                commandText.Append(string.Format(" and ProductStainlessCirculation.type between {0} and {1}", type, type + 1));

            if (productID > 0)
                commandText.Append(string.Format(" and ProductStainless.ID={0}", productID));
            else if (!string.IsNullOrEmpty(product))
                commandText.Append(string.Format(" and ProductStainless.name like '%{0}%'", product));

            if (customerID > 0)
                commandText.Append(string.Format(" and Customer.ID={0}", customerID));
            else if (!string.IsNullOrEmpty(customer))
                commandText.Append(string.Format(" and Customer.name like '%{0}%'", customer));

            commandText.Append(" order by ProductStainlessCirculationRecord.ID desc");
            return DbHelperAccess.executeQuery(commandText.ToString());
        }

        public int FindCount(int productID)
        {
            string commandText = string.Format("select count(*) from ProductStainlessCirculationRecord where ProductStainlessCirculationRecord.productID = {0}", productID);
            int result = DbHelperAccess.executeQueryNum(commandText);
            return result;
        }

        public int DeleteByCirculationID(int circulationID)
        {
            string commandText = string.Format("delete from ProductStainlessCirculationRecord where circulationID={0}", circulationID);
            return DbHelperAccess.executeNonQuery(commandText);
        }
    }
}
