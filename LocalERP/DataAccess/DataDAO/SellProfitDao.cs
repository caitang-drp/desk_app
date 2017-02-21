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
                string commandText = string.Format("insert into SellProfit(serial, sell_time, customer, product, unit, cnt, price, sum_price, cost, profit, profit_margin, oper, sum_cost, record_id) values('{0}', '{1}', '{2}', '{3}', '{4}', {5}, {6}, {7}, {8}, {9}, {10}, '{11}', {12}, {13})", 
                    one.serial, one.sell_time, one.customer, one.product, one.unit, one.cnt, one.price, one.sum_price, one.cost, one.profit, one.profit_margin, one.oper, one.sum_cost, one.record_id);
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

        public List<SellProfit> FindList()
        {
            string commandText = string.Format("select * from SellProfit");
            DataTable data = DbHelperAccess.executeQuery(commandText);

            List<SellProfit> ls = new List<SellProfit>();
            foreach (DataRow dr in data.Rows)
            { 
                SellProfit sell = new SellProfit(); 
                if (dr != null) {
                    sell.ID = (int)dr["ID"];
                    sell.serial = dr["serial"] as string;
                    sell.sell_time = (DateTime)dr["sell_time"];
                    sell.customer = dr["customer"] as string;
                    sell.product = dr["product"] as string;
                    sell.unit = dr["unit"] as string;
                    sell.cnt = (int)dr["cnt"];
                    sell.price = (double)dr["price"];
                    sell.sum_price = (double)dr["sum_price"];
                    sell.cost = (double)dr["cost"];
                    sell.profit = (double)dr["profit"];
                    sell.profit_margin = (double)dr["profit_margin"];
                    sell.oper = dr["oper"] as string;
                    sell.sum_cost = (double)dr["sum_cost"];
                    sell.record_id = (int)dr["record_id"];

                    ls.Add(sell);
                }
            }

            return ls;
        }
    }

}