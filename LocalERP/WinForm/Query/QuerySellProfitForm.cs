using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using LocalERP.DataAccess.DataDAO;
using LocalERP.DataAccess.Data;
using System.Collections;
using LocalERP.DataAccess.Utility;

namespace LocalERP.WinForm
{
    public partial class QuerySellProfitForm : MyDockContent
    {
        // ͳ�����ݣ�������ϸ��ʽ
        SellProfit statistic_record;

        public QuerySellProfitForm()
        {
            InitializeComponent();

            ArrayList typeList = new ArrayList();
            typeList.Add(new DictionaryEntry(1, "����ϸ"));
            //typeList.Add(new DictionaryEntry(3, "��������Ʒ"));
            this.comboBox1.DataSource = typeList;
            this.comboBox1.ValueMember = "Key";
            this.comboBox1.DisplayMember = "Value";
        }

        private void ProductSellForm_Load(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            this.start_time.Value = dateTime.AddMonths(-1);

            initList();
        }

        // ���� �����˻� �� �ɹ��˻�
        private void deal_with_return()
        {

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
        private List<double> get_profit(double cost, double price)
        {
            List<double> ret = new List<double>();

            double profit = price - cost;
            profit = double_n(profit, 6);

            // �����ʣ��������Ϊ����������Ӧ��Ҳ�Ǹ�����
            double profit_margin = profit / Math.Abs(cost) * 100.0;
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

        private void sellprofit_to_grid(SellProfit one, int index)
        {
            this.dataGridView1.Rows[index].Cells["serial"].Value = one.serial;
            this.dataGridView1.Rows[index].Cells["sell_time"].Value = one.sell_time;
            this.dataGridView1.Rows[index].Cells["customer"].Value = one.customer;
            this.dataGridView1.Rows[index].Cells["product"].Value = one.product;
            this.dataGridView1.Rows[index].Cells["unit"].Value = one.unit;
            this.dataGridView1.Rows[index].Cells["sell_cnt"].Value = one.cnt;
            this.dataGridView1.Rows[index].Cells["sell_price"].Value = one.price;
            this.dataGridView1.Rows[index].Cells["sell_sum_price"].Value = one.sum_price;
            this.dataGridView1.Rows[index].Cells["cost"].Value = one.cost;
            this.dataGridView1.Rows[index].Cells["profit"].Value = one.profit;
            this.dataGridView1.Rows[index].Cells["profit_margin"].Value = one.profit_margin;
            this.dataGridView1.Rows[index].Cells["ope"].Value = one.oper;
            this.dataGridView1.Rows[index].Cells["sum_cost"].Value = one.sum_cost;

            //this.dataGridView1.Rows[index].Cells["sell_cnt"].Style.BackColor = Color.Yellow;
            this.dataGridView1.Rows[index].Cells["sell_price"].Style.BackColor = Color.Yellow;
            this.dataGridView1.Rows[index].Cells["sell_sum_price"].Style.BackColor = Color.Yellow;
            this.dataGridView1.Rows[index].Cells["cost"].Style.BackColor = Color.YellowGreen;
            this.dataGridView1.Rows[index].Cells["sum_cost"].Style.BackColor = Color.YellowGreen;

            // ���ÿ�����ʾΪ��ɫ
            if (Convert.ToDouble(this.dataGridView1.Rows[index].Cells["profit"].Value) <= 0.0000)
            {
                this.dataGridView1.Rows[index].DefaultCellStyle.ForeColor = Color.Red;
                //this.dataGridView1.Rows[index].DefaultCellStyle.BackColor = Color.Red;
                //this.dataGridView1.Rows[index].DefaultCellStyle.ForeColor = Color.Red;
                //this.dataGridView1.Rows[index].Cells["profit_margin"].Style.BackColor = Color.Red;
            }

            // ��¼ͳ����Ϣ
            statistic_record.ID ++;
            statistic_record.cnt += one.cnt;
            statistic_record.sum_cost += one.sum_cost;
            statistic_record.sum_price += one.sum_price;
            statistic_record.profit += one.profit;
        }

        private SellProfit format_sellprofit(
            int index,
            ProductCirculation cir,
            ProductCirculationRecord record,
            double average_price)
        {
            ProductStainless product = ProductStainlessDao.getInstance().FindByID(record.ProductID);
            SellProfit one = new SellProfit();

            one.serial = cir.Code;
            one.sell_time = cir.CirculationTime;
            one.customer = cir.CustomerName;
            one.product = product.Name;
            one.unit = product.Unit;


            int sell_cnt = record.TotalNum;
            one.cnt = sell_cnt;
            one.price = (double)record.Price;

            double sum_price = record.Price * sell_cnt;
            one.sum_price = sum_price;

            one.cost = double_n(average_price, 6);

            double sum_cost = double_n(average_price, 6) * sell_cnt;
            one.sum_cost = sum_cost;

            List<double> profit = get_profit(sum_cost, sum_price);
            one.profit = profit[0];
            one.profit_margin = profit[1];

            one.oper = cir.Oper;
            one.record_id = record.ID;

            return one;
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

        // ֻ��Ҫ ����
        private DateTime get_date(DateTime x)
        {
            return new DateTime(x.Year, x.Month, x.Day);
        }

        // �Ƚ�����
        private bool if_in_time_range(DateTime start, DateTime end, DateTime x)
        {
            start = get_date(start);
            end = get_date(end);
            x = get_date(x);

            // �����ʱ�䷶Χ
            if (DateTime.Compare(start, end) > 0)
            {
                return false;
            }

            if (DateTime.Compare(start, x) <= 0 && DateTime.Compare(x, end) <= 0)
            {
                return true;
            }

            return false;
        }

        private bool if_ok(DateTime t)
        {
            DateTime start = start_time.Value;
            DateTime end = end_time.Value;

            if (!if_in_time_range(start, end, t))
            {
                return false;
            }

            return true;
        }

        private void initDoneListRecord(List<SellProfit> done_ls)
        {
            foreach (SellProfit one in done_ls)
            {
                // ��������
                if (!if_ok(one.sell_time))
                {
                    continue;
                }

                int index = this.dataGridView1.Rows.Add();

                sellprofit_to_grid(one, index);
            }
        }

        private void initUndoListRecord(
            List<ProductCirculation> ls,
            List<SellProfit> done_profit_ls,
            Dictionary<int, double> product_average_price_map)
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

                // ��������
                if (!if_ok(cir.CirculationTime))
                {
                    continue;
                }

                int index = this.dataGridView1.Rows.Add();

                // �����˻�
                if (cir.Type == 4)
                {
                    // ��������Ϊ����
                    record.TotalNum = -record.TotalNum;
                }

                SellProfit one =  format_sellprofit(index, cir, record, product_average_price_map[record.ProductID]);
                sellprofit_to_grid(one, index);

                // �洢
                SellProfitDao.getInstance().Insert(one);
            }
        }

        private void initRecordStatisticLine()
        {
            int index = this.dataGridView1.Rows.Add();
            index = this.dataGridView1.Rows.Add();
            index = this.dataGridView1.Rows.Add();
            index = this.dataGridView1.Rows.Add();
            index = this.dataGridView1.Rows.Add();

            this.dataGridView1.Rows[index].Cells["serial"].Value = statistic_record.ID;
            this.dataGridView1.Rows[index].Cells["sell_cnt"].Value = statistic_record.cnt;
            this.dataGridView1.Rows[index].Cells["sell_sum_price"].Value = statistic_record.sum_price;
            this.dataGridView1.Rows[index].Cells["sum_cost"].Value = statistic_record.sum_cost;
            this.dataGridView1.Rows[index].Cells["profit"].Value = statistic_record.profit;

            if (statistic_record.profit <= 0.0000)
            {
                this.dataGridView1.Rows[index].DefaultCellStyle.ForeColor = Color.Red;
            }
        }

        private void initListRecord()
        {
            statistic_record = new SellProfit();
            this.dataGridView1.Rows.Clear();

            // ��ȡ���ͨ���Ķ���
            List<ProductCirculation> reviewed_all_bill = ProductStainlessCirculationDao.getInstance().get_reviewed_bill();
            List<SellProfit> done_profit_ls = SellProfitDao.getInstance().FindList();
            // ��ȡ��Ʒ��ƽ���ɱ��۸�
            Dictionary<int, double> product_average_price_map = 
                ProductStainlessCirculationRecordDao.getInstance().get_product_average_buy_cost(reviewed_all_bill);

            initDoneListRecord(done_profit_ls);

            initUndoListRecord(reviewed_all_bill, done_profit_ls, product_average_price_map);

            initRecordStatisticLine();
        }

        private void initList()
        {
            // ������ϸ
            initListRecord();
        }
        /// <summary>
        /// event
        /// </summary>

        public override void refresh()
        {
            this.initList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            initList();
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            e.Handled = true;
            e.PaintBackground(e.CellBounds, true);
            e.PaintContent(e.CellBounds);
        }
    }
}