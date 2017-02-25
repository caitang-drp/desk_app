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
        public string customer;
        public string product;
        public string unit;
        public string oper;
        public DateTime sell_time;
        public int record_id;

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

        private SellProfit format_sellprofit(
            ProductCirculation cir,
            ProductCirculationRecord record,
            double average_price)
        {
            ProductStainless product = ProductStainlessDao.getInstance().FindByID(record.ProductID);
            SellProfit one = new SellProfit();

            if (cir != null)
            {
                one.serial = cir.Code;
                one.sell_time = cir.CirculationTime;
                one.customer = cir.CustomerName;
                one.oper = cir.Oper;
            }
            else {
                one.serial = "";
                one.sell_time = new DateTime();
                one.customer = "";
                one.oper = "";
            }

            one.product = product.Name;
            one.unit = product.Unit;


            int sell_cnt = record.TotalNum;
            one.cnt = sell_cnt;
            one.price = (double)record.Price;

            double sum_price = record.Price * sell_cnt;
            if (cir == null)
            {
                sum_price = record.TotalPrice;
            }
            one.sum_price = sum_price;

            one.cost = double_n(average_price, 1);

            double sum_cost = double_n(average_price, 1) * sell_cnt;
            one.sum_cost = sum_cost;

            List<double> profit = get_profit(sum_cost, sum_price);
            one.profit = profit[0];

            one.profit_margin = profit[1];
            if (sell_cnt < 0 && one.profit_margin > 0)
            {
                one.profit_margin = -one.profit_margin;
            }

            one.record_id = record.ID;

            return one;
        }

        // 过滤已经计算过的
        private List<ProductCirculationRecord> filter_done_record(List<ProductCirculationRecord> ls, List<SellProfit> done_profit_ls)
        {
            List<ProductCirculationRecord> undo_ls = new List<ProductCirculationRecord>();

            foreach (ProductCirculationRecord one in ls)
            {
                bool ok = false;
                foreach (SellProfit done in done_profit_ls)
                {
                    if (done.record_id == one.ID)
                    {
                        ok = true;
                        break;
                    }
                }

                if (!ok)
                {
                    undo_ls.Add(one);
                }
            }

            return undo_ls;
        }

        private void initUndoListRecord(
            List<ProductCirculation> ls,
            List<SellProfit> done_profit_ls)
        {
            // 获取审核通过的销售单的明细
            List<ProductCirculationRecord> reviewed_sell_record_ls =
                ProductStainlessCirculationRecordDao.getInstance().get_sell_records(ls);
            // 销售退货明细
            List<ProductCirculationRecord> reviewed_sell_back_record_ls =
                ProductStainlessCirculationRecordDao.getInstance().get_sell_back_records(ls);

            // 过滤已经计算过的
            List<ProductCirculationRecord> reviewed_undo_sell_record = filter_done_record(reviewed_sell_record_ls, done_profit_ls);
            List<ProductCirculationRecord> reviewed_undo_sell_back_record = filter_done_record(reviewed_sell_back_record_ls, done_profit_ls);

            // 所有的明细
            List<ProductCirculationRecord> record_ls = reviewed_undo_sell_record;
            record_ls.AddRange(reviewed_undo_sell_back_record);
            // 按照ID排序
            record_ls.Sort(delegate(ProductCirculationRecord x, ProductCirculationRecord y)
            {
                return x.ID.CompareTo(y.ID);
            });


            foreach (ProductCirculationRecord record in record_ls)
            {
                ProductCirculation cir = find_circulation_with_id(record.CirculationID, ls);

                // 销售退货
                if (cir.Type == 4)
                {
                    // 设置数量为负数
                    record.TotalNum = -record.TotalNum;
                }

                double product_purchase_average_price = ProductStainlessDao.getInstance().find_purchase_price_by_id(record.ProductID);
                SellProfit one =  format_sellprofit(cir, record, product_purchase_average_price);

                // 存储
                SellProfitDao.getInstance().Insert(one);
            }
        }

        public void update_sell_profit()
        {
            // 获取审核通过的订单
            List<ProductCirculation> reviewed_all_bill = ProductStainlessCirculationDao.getInstance().get_reviewed_bill();
            List<SellProfit> done_profit_ls = SellProfitDao.getInstance().FindList();
            initUndoListRecord(reviewed_all_bill, done_profit_ls);
        }
    }
}
