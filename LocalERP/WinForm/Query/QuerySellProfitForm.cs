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

            double profit_margin = profit / cost * 100.0;
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

        private void initList()
        {
            // 获取审核通过的订单
            List<ProductCirculation> ls = ProductStainlessCirculationDao.getInstance().get_reviewed_bill();
            // 获取商品的平均成本价格
            Dictionary<int, double> product_average_price_map = 
                ProductStainlessCirculationRecordDao.getInstance().get_product_average_buy_cost(ls);
            // 获取审核通过的销售单的明细
            List<ProductCirculationRecord> reviewed_sell_record_ls =
                ProductStainlessCirculationRecordDao.getInstance().get_sell_records(ls);

            this.dataGridView1.Rows.Clear();
            foreach (ProductCirculationRecord record in reviewed_sell_record_ls)
            {
                int index = this.dataGridView1.Rows.Add();

                ProductCirculation cir = find_circulation_with_id(record.CirculationID, ls);
                ProductStainless product = ProductStainlessDao.getInstance().FindByID(record.ProductID);

                this.dataGridView1.Rows[index].Cells["serial"].Value = cir.Code;
                this.dataGridView1.Rows[index].Cells["sell_time"].Value = cir.CirculationTime;
                this.dataGridView1.Rows[index].Cells["customer"].Value = cir.CustomerName;
                this.dataGridView1.Rows[index].Cells["product"].Value = product.Name;
                this.dataGridView1.Rows[index].Cells["unit"].Value = product.Unit;
                this.dataGridView1.Rows[index].Cells["sell_cnt"].Value = record.TotalNum;
                this.dataGridView1.Rows[index].Cells["sell_price"].Value = record.Price;
                this.dataGridView1.Rows[index].Cells["sell_sum_price"].Value = record.TotalPrice;
                this.dataGridView1.Rows[index].Cells["cost"].Value = double_n(product_average_price_map[record.ProductID], 6);

                List<double> profit = get_profit(product_average_price_map[record.ProductID], record.Price);
                this.dataGridView1.Rows[index].Cells["profit"].Value = profit[0];
                this.dataGridView1.Rows[index].Cells["profit_margin"].Value = profit[1];

                this.dataGridView1.Rows[index].Cells["ope"].Value = cir.Oper;

                // 设置亏损，显示为红色
                if (profit[0] < 0.0000)
                {
                    //this.dataGridView1.Rows[index].Cells["profit"].FormattedValueTyp
                    //this.dataGridView1.Rows[index].DefaultCellStyle.BackColor = Color.Red;
                    //this.dataGridView1.Rows[index].DefaultCellStyle.ForeColor = Color.Red;
                    this.dataGridView1.Rows[index].Cells["profit"].Style.BackColor = Color.Red;
                    this.dataGridView1.Rows[index].Cells["profit_margin"].Style.BackColor = Color.Red;
                }
            }
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