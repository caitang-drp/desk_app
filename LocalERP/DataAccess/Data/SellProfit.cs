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

        public int type;

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

            this.cnt = record.TotalNum * cir.FlowType;
            this.price = double.Parse((record.Price * cutoff).ToString("0.00"));
            this.sum_price = double.Parse((-cir.FlowType * record.TotalPrice * cutoff).ToString("0.00"));
            this.cost = double.Parse(average_price.ToString("0.00"));

            this.sum_cost = double.Parse((average_price * this.cnt).ToString("0.00"));

            this.profit = Math.Round(this.sum_price + this.sum_cost, 2);
            //2018-2-23:����Ҫ�ж��Ƿ�Ϊ��
            this.profit_margin = this.sum_cost == 0?100:Math.Round(this.profit / Math.Abs(this.sum_cost) * 100, 2);
            /*
            if (cir.Type == 4) {
                this.profit = -this.profit;
                this.profit_margin = -this.profit_margin;
            }*/
        }
    }
}
