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
        public QuerySellProfitForm()
        {
            InitializeComponent();
        }

        private void ProductSellForm_Load(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            this.dateTimePicker3.Value = dateTime.AddMonths(-1);

            initList();
        }

        // 处理 销售退货 和 采购退货
        private void deal_with_return()
        {

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
        private List<double> get_profit(double cost, double price)
        {
            List<double> ret = new List<double>();

            double profit = price - cost;
            profit = double_n(profit, 6);

            // 利润率，如果利润为负，利润率应该也是负数的
            double profit_margin = profit / Math.Abs(cost) * 100.0;
            profit_margin = double_n(profit_margin, 2);

            ret.Add(profit);
            ret.Add(profit_margin);
            return ret;
        }

        private ProductCirculation find_circulation_with_id(int ID, List<ProductCirculation> ls)
        {
            foreach (var cir in ls)
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

            this.dataGridView1.Rows[index].Cells["sell_cnt"].Style.BackColor = Color.Yellow;
            this.dataGridView1.Rows[index].Cells["sell_price"].Style.BackColor = Color.Yellow;
            this.dataGridView1.Rows[index].Cells["sell_sum_price"].Style.BackColor = Color.Yellow;
            this.dataGridView1.Rows[index].Cells["cost"].Style.BackColor = Color.YellowGreen;
            this.dataGridView1.Rows[index].Cells["sum_cost"].Style.BackColor = Color.YellowGreen;

            // 设置亏损，显示为红色
            if (Convert.ToDouble(this.dataGridView1.Rows[index].Cells["profit"].Value) <= 0.0000)
            {
                this.dataGridView1.Rows[index].DefaultCellStyle.ForeColor = Color.Red;
                //this.dataGridView1.Rows[index].DefaultCellStyle.BackColor = Color.Red;
                //this.dataGridView1.Rows[index].DefaultCellStyle.ForeColor = Color.Red;
                //this.dataGridView1.Rows[index].Cells["profit_margin"].Style.BackColor = Color.Red;
            }
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

        private void initDoneList(List<SellProfit> done_ls)
        {
            foreach (SellProfit one in done_ls)
            {
                int index = this.dataGridView1.Rows.Add();

                sellprofit_to_grid(one, index);
            }
        }

        private void initUndoList(
            List<ProductCirculation> ls,
            List<SellProfit> done_profit_ls,
            Dictionary<int, double> product_average_price_map)
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
                int index = this.dataGridView1.Rows.Add();

                ProductCirculation cir = find_circulation_with_id(record.CirculationID, ls);

                // 销售退货
                if (cir.Type == 4)
                {
                    // 设置数量为负数
                    record.TotalNum = -record.TotalNum;
                }

                SellProfit one =  format_sellprofit(index, cir, record, product_average_price_map[record.ProductID]);
                sellprofit_to_grid(one, index);

                // 存储
                SellProfitDao.getInstance().Insert(one);
            }
        }

        private void initList()
        {
            this.dataGridView1.Rows.Clear();

            // 获取审核通过的订单
            List<ProductCirculation> reviewed_all_bill = ProductStainlessCirculationDao.getInstance().get_reviewed_bill();
            List<SellProfit> done_profit_ls = SellProfitDao.getInstance().FindList();
            // 获取商品的平均成本价格
            Dictionary<int, double> product_average_price_map = 
                ProductStainlessCirculationRecordDao.getInstance().get_product_average_buy_cost(reviewed_all_bill);

            initDoneList(done_profit_ls);

            initUndoList(reviewed_all_bill, done_profit_ls, product_average_price_map);
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