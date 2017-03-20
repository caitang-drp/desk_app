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
        //Ӧ��ΪID��Ҫ��Ȼ��Ʒ���Ըı��ˣ������޷����Ÿı�
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

        // ���������������
        // input: 
        //      cost        �ɱ�
        //      price       �ۼ� 
        // output:
        //      {����������%}
        public List<double> get_profit(double cost, double price)
        {
            List<double> ret = new List<double>();

            double profit = price - cost;
            profit = double_n(profit, 1);

            // �����ʣ��������Ϊ����������Ӧ��Ҳ�Ǹ�����
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
                this.customer = cir.CustomerName;
                this.oper = cir.Oper;
            }
            else {
                this.serial = "";
                this.sell_time = new DateTime();
                this.customer = "";
                this.oper = "";
            }

            this.product = record.ProductName;// product.Name;
            this.unit = record.Unit;// product.Unit;


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

        // �����Ѿ��������
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
            // ��ȡ���ͨ�������۵�����ϸ
            List<ProductCirculationRecord> reviewed_sell_record_ls =
                ProductStainlessCirculationRecordDao.getInstance().get_sell_records(ls);
            // �����˻���ϸ
            List<ProductCirculationRecord> reviewed_sell_back_record_ls =
                ProductStainlessCirculationRecordDao.getInstance().get_sell_back_records(ls);

            // �����Ѿ��������
            List<ProductCirculationRecord> reviewed_undo_sell_record = filter_done_record(reviewed_sell_record_ls, done_profit_ls);
            List<ProductCirculationRecord> reviewed_undo_sell_back_record = filter_done_record(reviewed_sell_back_record_ls, done_profit_ls);

            // ���е���ϸ
            List<ProductCirculationRecord> record_ls = reviewed_undo_sell_record;
            record_ls.AddRange(reviewed_undo_sell_back_record);
            // ����ID����
            record_ls.Sort(delegate(ProductCirculationRecord x, ProductCirculationRecord y)
            {
                return x.ID.CompareTo(y.ID);
            });


            foreach (ProductCirculationRecord record in record_ls)
            {
                ProductCirculation cir = find_circulation_with_id(record.CirculationID, ls);

                // �����˻�
                if (cir.Type == 4)
                {
                    // ��������Ϊ����
                    record.TotalNum = -record.TotalNum;
                }

                double product_purchase_average_price = ProductStainlessDao.getInstance().find_purchase_price_by_id(record.ProductID);
                SellProfit one = new SellProfit(cir, record, product_purchase_average_price);

                // �洢
                SellProfitDao.getInstance().Insert(one);
            }
        }

        public void update_sell_profit()
        {
            // ��ȡ���ͨ���Ķ���
            List<ProductCirculation> reviewed_all_bill = ProductStainlessCirculationDao.getInstance().get_reviewed_bill();
            List<SellProfit> done_profit_ls = SellProfitDao.getInstance().FindList();
            initUndoListRecord(reviewed_all_bill, done_profit_ls);
        }
    }
}
