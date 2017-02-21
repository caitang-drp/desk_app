using System;
using System.Collections.Generic;
using System.Text;

namespace LocalERP.DataAccess.Data
{
    public class SellProfit
    {
        public int ID;
        public int cnt;
        public double price;
        public double sum_price;
        public double cost;
        public double sum_cost;
        public double profit;
        public double profit_margin;
        public string serial;
        public string customer;
        public string product;
        public string unit;
        public string oper;
        public DateTime sell_time;
        public int record_id;
    }
}
