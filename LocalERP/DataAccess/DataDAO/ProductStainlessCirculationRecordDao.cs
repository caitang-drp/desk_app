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
                    info.ProductID, info.QuantityNull?"NULL":info.QuantityPerPiece.ToString(), info.PiecesNull?"NULL":info.Pieces.ToString(), info.TotalNum, info.Unit, info.Price, info.TotalPrice, info.CirculationID);
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

        // ������۵��б� ���ڵ���ϸ�б�
        public List<ProductCirculationRecord> get_sell_records(List<ProductCirculation> ls)
        {
            // ֻ��Ҫ ���۵� ��
            List<int> buy_circulation_id_ls = new List<int>();
            foreach (ProductCirculation one in ls)
            {
                // �²�һ�£�enum��ȻҪʵ���������ã�Ӧ�÷ŵ�ȫ�ֱ�������Ȼÿ�ζ�Ҫnewһ�����������
                if (one.Type == 3)
                {
                    buy_circulation_id_ls.Add(one.ID);
                }
            }

            // ��ȡ��Ӧ�� ��ϸ
            List<ProductCirculationRecord> records = new List<ProductCirculationRecord>();
            foreach (int ID in buy_circulation_id_ls)
            {
                records.AddRange(ProductStainlessCirculationRecordDao.getInstance().FindList(ID));
            }

            return records;
        }

        // ��ȡ ��Ʒ�� ƽ���ɹ��۸�
        // ret: [(��Ʒid, ƽ���۸�), ....]
        public Dictionary<int, double> get_product_average_buy_cost(List<ProductCirculation> ls)
        {
            // ֻ��Ҫ �ɹ��� ��
            List<int> buy_circulation_id_ls = new List<int>();
            foreach (ProductCirculation one in ls)
            {
                if (one.Type == 1)
                {
                    buy_circulation_id_ls.Add(one.ID);
                }
            }

            // ��ȡ��Ӧ�� ��ϸ
            List<ProductCirculationRecord> records = new List<ProductCirculationRecord>();
            foreach (int ID in buy_circulation_id_ls)
            {
                records.AddRange(ProductStainlessCirculationRecordDao.getInstance().FindList(ID));
            }

            // ������Ʒ���з���
            Dictionary<int, List<ProductCirculationRecord>> group = new Dictionary<int, List<ProductCirculationRecord>>();
            foreach (ProductCirculationRecord record in records)
            {
                int key = record.ProductID;

                if (!group.ContainsKey(key))
                {
                    group.Add(key, new List<ProductCirculationRecord>());
                }

                List<ProductCirculationRecord> val = group[key];
                val.Add(record);

                group[key] = val;
            }

            // ����ÿ��� ����
            Dictionary<int, double> res = new Dictionary<int, double>();
            foreach (KeyValuePair<int,List<ProductCirculationRecord>> product in group)
            {
                List<ProductCirculationRecord> product_record_ls = product.Value;

                int cnt = 0;
                double sum = 0.0;
                foreach (ProductCirculationRecord record in product_record_ls)
                {
                    cnt += record.TotalNum;
                    sum += record.TotalPrice;
                }

                res[product.Key] = sum / cnt;
            }

            return res;
        }  
    }
}
