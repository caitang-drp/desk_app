using LocalERP.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

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
                    info.ProductID, info.QuantityPerPiece, info.Pieces, info.TotalNum, info.Unit, info.Price, info.TotalPrice, info.CirculationID);
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
                ProductStainlessCirculationRecord record = new ProductStainlessCirculationRecord();
                record.CirculationID = circulationID;
                record.ID = (int)dr["ProductStainlessCirculationRecord.ID"];

                double quantityPerPiece, pieces, price;
                double.TryParse(dr["price"].ToString(), out price);
                record.Price = price;

                record.ProductID = (int)dr["ProductStainless.ID"];
                record.ProductName = dr["name"].ToString();

                double.TryParse(dr["ProductStainlessCirculationRecord.quantityPerPiece"].ToString(), out quantityPerPiece);
                record.QuantityPerPiece = quantityPerPiece;

                double.TryParse(dr["pieces"].ToString(), out pieces);
                record.Pieces = pieces;

                record.Unit = dr["ProductStainlessCirculationRecord.unit"].ToString();

                record.TotalNum = (int)dr["totalNum"];
                record.TotalPrice = Convert.ToDouble(dr["totalPrice"]);
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

        // 获得销售单列表 对于的明细列表
        public List<ProductCirculationRecord> get_sell_records(List<ProductCirculation> ls)
        {
            // 只需要 销售的 单
            List<int> buy_circulation_id_ls = new List<int>();
            foreach (ProductCirculation one in ls)
            {
                // 吐槽一下，enum居然要实例化才能用，应该放到全局变量，不然每次都要new一个对象才能用
                if (one.Type == 3)
                {
                    buy_circulation_id_ls.Add(one.ID);
                }
            }

            // 获取对应的 明细
            List<ProductCirculationRecord> records = new List<ProductCirculationRecord>();
            foreach (int ID in buy_circulation_id_ls)
            {
                records.AddRange(ProductStainlessCirculationRecordDao.getInstance().FindList(ID));
            }

            return records;
        }

        // 获取 商品的 平均采购价格
        // ret: [(商品id, 平均价格), ....]
        public Dictionary<int, double> get_product_average_buy_cost(List<ProductCirculation> ls)
        {
            // 只需要 采购的 单
            List<int> buy_circulation_id_ls = new List<int>();
            foreach (ProductCirculation one in ls)
            {
                if (one.Type == 1)
                {
                    buy_circulation_id_ls.Add(one.ID);
                }
            }

            // 获取对应的 明细
            List<ProductCirculationRecord> records = new List<ProductCirculationRecord>();
            foreach (int ID in buy_circulation_id_ls)
            {
                records.AddRange(ProductStainlessCirculationRecordDao.getInstance().FindList(ID));
            }

            // 根据商品进行分组
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

            // 计算每组的 单价
            Dictionary<int, double> res = new Dictionary<int, double>();
            foreach (var product in group)
            {
                List<ProductCirculationRecord> product_record_ls = product.Value;

                int cnt = 0;
                double sum = 0.0;
                foreach (var record in product_record_ls)
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
