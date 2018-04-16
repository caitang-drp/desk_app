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
                //注意这里：quantity不用''，而totalNum用
                string commandText = string.Format("insert into ProductStainlessCirculationRecord(productID, quantityPerPiece, pieces, totalNum, unit, price, totalPrice, circulationID, comment) values('{0}', {1}, {2}, '{3}','{4}', '{5}', '{6}', '{7}', '{8}')",
                    info.ProductID, info.QuantityNull ? "NULL" : info.QuantityPerPiece.ToString(), info.PiecesNull ? "NULL" : info.Pieces.ToString(), info.TotalNum, info.Unit, info.Price, info.TotalPrice, info.CirculationID, info.Comment);
                DbHelperAccess.executeNonQuery(commandText);
                int recordID = DbHelperAccess.executeMax("ID", "ProductStainlessCirculationRecord");
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

                int pieces;
                double price, totalPrice, quantityPerPiece;

                double.TryParse(dr["price"].ToString(), out price);
                record.Price = price;

                record.Serial = dr["serial"].ToString();
                record.ProductID = (int)dr["ProductStainless.ID"];
                record.ProductName = dr["name"].ToString();

                ValidateUtility.getDouble(dr, "ProductStainlessCirculationRecord.quantityPerPiece", out quantityPerPiece, out tempBool);
                record.QuantityPerPiece = quantityPerPiece;
                record.QuantityNull = tempBool;

                ValidateUtility.getInt(dr, "pieces", out pieces, out tempBool);
                record.Pieces = pieces;
                record.PiecesNull = tempBool;

                record.Unit = dr["ProductStainlessCirculationRecord.unit"].ToString();

                double num;
                ValidateUtility.getDouble(dr, "totalNum", out num, out tempBool);
                record.TotalNum = num;

                ValidateUtility.getDouble(dr, "totalPrice", out totalPrice, out tempBool);
                record.TotalPrice = totalPrice;

                record.Comment = dr["ProductStainlessCirculationRecord.comment"].ToString();

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
                commandText.Append(string.Format(" and ProductStainlessCirculation.type= {0}", type));

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

        public List<string> FindPriceList(int type, int productID, int customerID)
        {
            //退货的价格以进货的价格为主
            if (type == 2 || type == 4)
                type--;

            StringBuilder commandText = new StringBuilder();
            string temp = "select distinct(ProductStainlessCirculationRecord.price) from ProductStainlessCirculationRecord, (select * from ProductStainlessCirculation left join Customer on Customer.ID = ProductStainlessCirculation.customerID ) circulation, ProductStainless"
                 + " where ProductStainlessCirculationRecord.circulationID = ProductStainlessCirculation.ID and ProductStainlessCirculationRecord.productID = ProductStainless.ID and status = 4";
            commandText.Append(temp);
            if (type > 0)
                commandText.Append(string.Format(" and ProductStainlessCirculation.type= {0}", type));

            if (productID > 0)
                commandText.Append(string.Format(" and ProductStainless.ID={0}", productID));

            if (customerID > 0)
                commandText.Append(string.Format(" and Customer.ID={0}", customerID));
        
            //commandText.Append(" order by ProductStainlessCirculationRecord.ID desc");

            DataTable dt = DbHelperAccess.executeQuery(commandText.ToString());

            List<string> prices = new List<string>();
            foreach (DataRow dr in dt.Rows)
            {
                prices.Add(dr["price"].ToString());
            }
            return prices;
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
