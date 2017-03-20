using System;
using System.Collections.Generic;
using System.Text;
using LocalERP.DataAccess.DataDAO;

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
        
        //这些字段在数据库表可以不用，但是类里可以有，以方便使用
        public int customerID;
        public string customer;

        public int productID;
        public string product;

        public string unit;
        public string oper;
        public DateTime sell_time;
        public int record_id;

        public SellProfit() { }

        public SellProfit(
           ProductCirculation cir,
           ProductCirculationRecord record,
           double average_price) {

               this.format_sellprofit(cir, record, average_price);

        }

        private double double_n(double x, int n)
        {
            return Convert.ToDouble(decimal.Round(decimal.Parse(x.ToString()), n).ToString());
        }

        // 计算利润和利润率
        // input: 
        //      cost        成本
        //      price       售价 
        // output:
        //      {利润，利润率%}
        public List<double> get_profit(double cost, double price)
        {
            List<double> ret = new List<double>();

            double profit = price - cost;
            profit = double_n(profit, 1);

            // 利润率，如果利润为负，利润率应该也是负数的
            double profit_margin = profit / Math.Abs(price) * 100.0;
            profit_margin = double_n(profit_margin, 2);

            ret.Add(profit);
            ret.Add(profit_margin);
            return ret;
        }

        private ProductCirculation find_circulation_with_id(int ID, List<ProductCirculation> ls)
        {
            foreach (ProductCirculation cir in ls)
            {
                if (ID == cir.ID)
                {
                    return cir;
                }
            }

            return null;
        }

        private void format_sellprofit(
            ProductCirculation cir,
            ProductCirculationRecord record,
            double average_price)
        {
            if (cir != null)
            {
                this.serial = cir.Code;
                this.sell_time = cir.CirculationTime;
                this.oper = cir.Oper;
            }
            //这种情况应该不会发生
            else {
                this.serial = "";
                this.sell_time = new DateTime();
                //this.customer = "";
                this.oper = "";
            }
            this.unit = record.Unit;

            int sell_cnt = record.TotalNum;
            this.cnt = sell_cnt;
            this.price = (double)record.Price;

            double sum_price = record.Price * sell_cnt;
            if (cir == null)
            {
                sum_price = record.TotalPrice;
            }
            this.sum_price = sum_price;

            this.cost = double_n(average_price, 1);

            double sum_cost = double_n(average_price, 1) * sell_cnt;
            this.sum_cost = sum_cost;

            List<double> profit = get_profit(sum_cost, sum_price);
            this.profit = profit[0];

            this.profit_margin = profit[1];
            if (sell_cnt < 0 && this.profit_margin > 0)
            {
                this.profit_margin = -this.profit_margin;
            }

            this.record_id = record.ID;
        }
    }
}
