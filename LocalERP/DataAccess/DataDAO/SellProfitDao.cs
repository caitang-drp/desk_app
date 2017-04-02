using LocalERP.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace LocalERP.DataAccess.DataDAO
{
    public class SellProfitDao
    {
        //singleton
        public static SellProfitDao dao;
        public static SellProfitDao getInstance()
        {
            if (dao == null)
            {
                dao = new SellProfitDao();
            }
            return dao;
        }

        public int Insert(SellProfit one)
        {
            try
            {
                string commandText = string.Format("insert into SellProfit(cnt, price, sum_price, cost, profit, profit_margin, sum_cost, record_id) values({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7})", 
                    one.cnt, one.price, one.sum_price, one.cost, one.profit, one.profit_margin, one.sum_cost, one.record_id);
                return DbHelperAccess.executeNonQuery(commandText);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertList(List<SellProfit> ls)
        {
            foreach (SellProfit one in ls)
            {
                Insert(one);
            }
        }

        public int DeleteAll()
        {
            string commandText = string.Format("delete from SellProfit");
            return DbHelperAccess.executeNonQuery(commandText);
        }

        // 把SellProfit列表，按照产品id分组
        public Dictionary<int, List<SellProfit>> group_by_product_id(List<SellProfit> ls)
        {
            Dictionary<int, List<SellProfit>> d = new Dictionary<int, List<SellProfit>>();

            foreach (SellProfit one in ls)
            {
                int product_id = one.productID;

                if (!d.ContainsKey(product_id))
                {
                    d[product_id] = new List<SellProfit>();
                }

                d[product_id].Add(one);
            }

            return d;
        }

        public List<SellProfit> FindList(DateTime startTime, DateTime endTime, string product, string customer)
        {
            StringBuilder commandText = new StringBuilder();
            //查找的字段名可以都使用<原始表名>.<字段>，至于结果，可以参考access查找出来的记
            string temp = "select SellProfit.*, ProductStainlessCirculationRecord.unit, ProductStainlessCirculation.code, ProductStainlessCirculation.type, ProductStainlessCirculation.circulationTime, ProductStainless.serial, ProductStainless.name, ProductStainless.ID, Customer.name, Customer.ID"
                 + " from SellProfit, ProductStainlessCirculationRecord, (select * from ProductStainlessCirculation left join Customer on Customer.ID = ProductStainlessCirculation.customerID ) circulation, ProductStainless"
                 + " where SellProfit.record_id = ProductStainlessCirculationRecord.ID and"
                 + " ProductStainlessCirculationRecord.circulationID = ProductStainlessCirculation.ID"
                 + " and ProductStainlessCirculationRecord.productID = ProductStainless.ID"
                 + " and ProductStainlessCirculation.circulationTime between #{0}# and #{1}#";

            commandText.Append(string.Format(temp, startTime.ToString("yyyy-MM-dd"), endTime.ToString("yyyy-MM-dd")));

            if (!string.IsNullOrEmpty(product))
                commandText.Append(string.Format(" and ProductStainless.name like '%{0}%'", product));

            else if (!string.IsNullOrEmpty(customer))
                commandText.Append(string.Format(" and Customer.name like '%{0}%'", customer));

            commandText.Append(" order by SellProfit.ID desc");

            DataTable data = DbHelperAccess.executeQuery(commandText.ToString());

            List<SellProfit> ls = new List<SellProfit>();
            foreach (DataRow dr in data.Rows)
            { 
                SellProfit sell = new SellProfit(); 
                if (dr != null) {
                    sell.ID = (int)dr["SellProfit.ID"];
                    sell.serial = dr["code"] as string;
                    sell.type = (int)dr["type"];
                    sell.sell_time = (DateTime)dr["circulationTime"];
                    sell.customerID = (int)dr["Customer.ID"];
                    sell.customer = dr["circulation.name"] as string;
                    sell.productID = (int)dr["ProductStainless.ID"];
                    sell.product = dr["ProductStainless.name"] as string;
                    sell.unit = dr["unit"] as string;
                    sell.cnt = (int)dr["cnt"];
                    sell.price = (double)dr["price"];
                    sell.sum_price = (double)dr["sum_price"];
                    sell.cost = (double)dr["cost"];
                    sell.profit = (double)dr["profit"];
                    sell.profit_margin = (double)dr["profit_margin"];
                    sell.sum_cost = (double)dr["sum_cost"];
                    sell.record_id = (int)dr["record_id"];

                    ls.Add(sell);
                }
            }

            return ls;
        }
    }

}