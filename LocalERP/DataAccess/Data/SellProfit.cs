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
        
        //��Щ�ֶ������ݿ����Բ��ã�������������У��Է���ʹ��
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

        // ���������������
        // input: 
        //      cost        �ɱ�
        //      price       �ۼ� 
        // output:
        //      {����������%}
        /*public List<double> get_profit(double cost, double price)
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
        }*/

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
            this.record_id = record.ID;
            this.serial = cir.Code;
            this.sell_time = cir.CirculationTime;
            this.oper = cir.Oper;
            this.unit = record.Unit;

            //����������۵Ļ���Ҫ������ۿ�
            double cutoff = cir.Total == 0?1:cir.RealTotal / cir.Total;

            this.cnt = record.TotalNum;
            this.price = record.Price * cutoff;
            this.sum_price = record.TotalPrice * cutoff;
            this.cost = average_price;

            this.sum_cost = average_price * this.cnt;

            this.profit = this.sum_price - this.sum_cost;
            this.profit_margin = this.profit / this.sum_cost * 100;

            if (cir.Type == 4) {
                this.profit = -this.profit;
                this.profit_margin = -this.profit_margin;
            }
        }
    }
}
