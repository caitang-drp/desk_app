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

        public ProductCirculationRecord find_record_by_id(int ID)
        {
            string commandText = string.Format("select * from ProductStainlessCirculationRecord where ID = " + ID.ToString());
            DataRow dr = DbHelperAccess.executeQueryGetOneRow(commandText);

            ProductCirculationRecord one = new ProductCirculationRecord();

            if (dr != null)
            {
                one.ID = (int)dr["ID"];
                one.ProductID = (int)dr["productID"];
            }

            return one;
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

        public List<ProductCirculationRecord> get_sell_records(List<ProductCirculation> ls)
        {
            // 只需要 销售的 单
            List<int> buy_circulation_id_ls = get_wanted_type_circulation_ls(3, ls);

            // 获取对应的 明细
            List<ProductCirculationRecord> records = get_records_from_cir_id_ls(buy_circulation_id_ls);

            return records;
        }

        public List<ProductCirculationRecord> get_sell_back_records(List<ProductCirculation> ls)
        {
            // 只需要 销售退货 的单
            List<int> buy_circulation_id_ls = get_wanted_type_circulation_ls(4, ls);

            // 获取对应的 明细
            List<ProductCirculationRecord> records = get_records_from_cir_id_ls(buy_circulation_id_ls);

            return records;
        }


        // 过滤 特定类型的 单
        public List<int>
        get_wanted_type_circulation_ls(
            int want_type,
            List<ProductCirculation> ls)
        {
            List<int> circulation_id_ls = new List<int>();
            foreach (ProductCirculation one in ls)
            {
                if (one.Type == want_type)
                {
                    circulation_id_ls.Add(one.ID);
                }
            }

            return circulation_id_ls; 
        }

        public List<ProductCirculationRecord> get_records_from_cir_id_ls(List<int> id_ls)
        {
            // 获取对应的 明细
            List<ProductCirculationRecord> records = new List<ProductCirculationRecord>();

            foreach (int ID in id_ls)
            {
                records.AddRange(ProductStainlessCirculationRecordDao.getInstance().FindList(ID));
            }

            return records;
        }

        // 将输入的单列表的明细，按照产品划分
        public Dictionary<int, List<ProductCirculationRecord>>
        get_record_group_by_product(
            List<int> id_ls)
        {
            // 获取对应的 明细
            List<ProductCirculationRecord> records = get_records_from_cir_id_ls(id_ls);
             
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

            return group;
        }

        // 获取 商品的 平均采购价格
        // input:
        //      ls      某段时间内的单
        // ret: [(商品id, 平均价格), ....]
        public Dictionary<int, double> get_product_average_buy_cost(List<ProductCirculation> ls)
        {
            // 采购
            Dictionary<int, List<ProductCirculationRecord>> purchase_group = get_record_group_by_product(get_wanted_type_circulation_ls(1, ls));
            // 采购退厂
            Dictionary<int, List<ProductCirculationRecord>> purchase_back_group = get_record_group_by_product(get_wanted_type_circulation_ls(2, ls));
            // 销售退货
            // NOTE: 销售退货相当于进了一件货
            Dictionary<int, List<ProductCirculationRecord>> sell_back_group = get_record_group_by_product(get_wanted_type_circulation_ls(4, ls));

            // 计算每组的 单价
            Dictionary<int, double> res = new Dictionary<int, double>();
            // 以采购为主
            foreach (KeyValuePair<int, List<ProductCirculationRecord>> product in purchase_group)
            {
                int key = product.Key;
                int cnt = 0;
                double sum = 0.0;

                // 采购
                foreach (ProductCirculationRecord record in purchase_group[key])
                {
                    cnt += record.TotalNum;
                    sum += record.TotalPrice;
                }

                // 采购退厂
                if (purchase_back_group.ContainsKey(key))
                {
                    foreach (ProductCirculationRecord record in purchase_back_group[key])
                    {
                        cnt -= record.TotalNum;
                        sum -= record.TotalPrice;
                    }
                }

                // 销售退货
                if (sell_back_group.ContainsKey(key))
                {
                    foreach (ProductCirculationRecord record in sell_back_group[key])
                    {
                        // 数据库中的cnt是正数的
                        cnt += record.TotalNum;
                        sum += record.TotalPrice;
                    }
                }

                res[product.Key] = sum / cnt;
            }

            return res;
        }  
    }
}
