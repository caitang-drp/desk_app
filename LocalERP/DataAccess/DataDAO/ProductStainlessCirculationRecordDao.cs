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

        // 将输入的单列表的明细，按照产品划分
        public Dictionary<int, List<ProductCirculationRecord>>
        get_record_group_by_product_sorted_by_id(
            List<int> id_ls)
        {
            Dictionary<int, List<ProductCirculationRecord>> ret = get_record_group_by_product(id_ls);

            foreach (KeyValuePair<int, List<ProductCirculationRecord>> item in ret)
            {
                item.Value.Sort(delegate(ProductCirculationRecord x, ProductCirculationRecord y)
                {
                    return x.ID.CompareTo(y.ID);
                });
            }

            return ret;
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

                /*
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
                */

                res[product.Key] = sum / cnt;
            }

            return res;
        }  

        // 获取 商品的 成本价格（移动加权平均法）
        // input:
        //      ls      某段时间内的单
        // ret: [(商品id, 平均价格), ....]
        public Dictionary<int, double> get_product_moving_weighted_average_method_cost(List<ProductCirculation> ls)
        {
            // 采购
            Dictionary<int, List<ProductCirculationRecord>> purchase_group = get_record_group_by_product_sorted_by_id(get_wanted_type_circulation_ls(1, ls));
            // 采购退厂
            Dictionary<int, List<ProductCirculationRecord>> purchase_back_group = get_record_group_by_product_sorted_by_id(get_wanted_type_circulation_ls(2, ls));
            // 销售
            Dictionary<int, List<ProductCirculationRecord>> sell_group = get_record_group_by_product_sorted_by_id(get_wanted_type_circulation_ls(3, ls));
            // 销售退货
            // NOTE: 销售退货相当于进了一件货
            Dictionary<int, List<ProductCirculationRecord>> sell_back_group = get_record_group_by_product_sorted_by_id(get_wanted_type_circulation_ls(4, ls));

            // 计算每组的 单价
            Dictionary<int, double> res = new Dictionary<int, double>();
            // 以采购为主
            foreach (KeyValuePair<int, List<ProductCirculationRecord>> product in purchase_group)
            {
                int key = product.Key;

                // 采购
                List<ProductCirculationRecord> purchase_ls = new List<ProductCirculationRecord>();
                // 采购退厂
                List<ProductCirculationRecord> purchase_back_ls = new List<ProductCirculationRecord>();
                // 销售
                List<ProductCirculationRecord> sell_ls = new List<ProductCirculationRecord>();
                // 销售退货
                List<ProductCirculationRecord> sell_back_ls = new List<ProductCirculationRecord>();

                if (purchase_group.ContainsKey(key))
                {
                    purchase_ls = purchase_group[key];
                }
                if (purchase_back_group.ContainsKey(key))
                {
                    purchase_back_ls = purchase_back_group[key];
                }
                if (sell_group.ContainsKey(key))
                {
                    sell_ls = sell_group[key];
                }
                if (sell_back_group.ContainsKey(key))
                {
                    sell_back_ls = sell_back_group[key];
                }

                // 成本
                double product_cost = 0.0;
                // 上一次采购时候的序号
                int min_id = 0;

                double last_left_sum_cost = 0.0;
                int last_left_cnt = 0;

                if (purchase_ls.Count > 0)
                {
                    try
                    {
                        min_id = purchase_ls[0].ID;
                        last_left_cnt = purchase_ls[0].TotalNum;
                        last_left_sum_cost = purchase_ls[0].TotalPrice;
                        product_cost = purchase_ls[0].TotalPrice / purchase_ls[0].TotalNum;

                        purchase_ls = purchase_ls.GetRange(1, purchase_ls.Count - 1);
                    }
                    catch
                    {

                    }
                }

                foreach (ProductCirculationRecord purchase in purchase_ls)
                {
                    foreach (ProductCirculationRecord sell in sell_ls)
                    {
                        if (sell.ID < min_id)
                        {
                            continue;
                        }

                        if (sell.ID > purchase.ID)
                        {
                            break;
                        }

                        last_left_sum_cost -= (product_cost * sell.TotalNum);
                        last_left_cnt -= sell.TotalNum;
                    }

                    foreach (ProductCirculationRecord sell_back in sell_back_ls)
                    {
                        if (sell_back.ID < min_id)
                        {
                            continue;
                        }

                        if (sell_back.ID > purchase.ID)
                        {
                            break;
                        }

                        last_left_sum_cost += (product_cost * sell_back.TotalNum);
                        last_left_cnt += sell_back.TotalNum;
                    }

                    foreach (ProductCirculationRecord purchase_back in purchase_back_ls)
                    {
                        if (purchase_back.ID < min_id)
                        {
                            continue;
                        }

                        if (purchase_back.ID > purchase.ID)
                        {
                            break;
                        }

                        last_left_sum_cost -= purchase_back.TotalPrice;
                        last_left_cnt -= purchase_back.TotalNum;
                    }

                    last_left_sum_cost += purchase.TotalPrice;
                    last_left_cnt += purchase.TotalNum;

                    min_id = purchase.ID;
                    product_cost = last_left_sum_cost / last_left_cnt;
                }
                res[key] = product_cost;
            }

            return res;
        }  
    }
}
