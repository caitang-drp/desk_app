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
using LocalERP.DataAccess;

namespace LocalERP.WinForm
{
    public partial class QuerySellProfitForm : MyDockContent
    {
        // 统计数据，按照明细方式
        SellProfit statistic_record;

        public QuerySellProfitForm()
        {
            InitializeComponent();

            ArrayList typeList = new ArrayList();
            typeList.Add(new DictionaryEntry(1, "按明细"));
            typeList.Add(new DictionaryEntry(2, "按销售商品"));
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

        private void sellprofit_to_grid(SellProfit one, int index)
        {
            this.dataGridView1.Rows[index].Cells["serial"].Value = one.serial;
            this.dataGridView1.Rows[index].Cells["sell_time"].Value = one.sell_time.ToShortDateString();
            this.dataGridView1.Rows[index].Cells["customer"].Value = one.customer;
            this.dataGridView1.Rows[index].Cells["product"].Value = one.product;
            this.dataGridView1.Rows[index].Cells["unit"].Value = one.unit;
            this.dataGridView1.Rows[index].Cells["sell_cnt"].Value = one.cnt;
            this.dataGridView1.Rows[index].Cells["sell_price"].Value = one.price;
            this.dataGridView1.Rows[index].Cells["sell_sum_price"].Value = one.sum_price;
            this.dataGridView1.Rows[index].Cells["cost"].Value = one.cost;
            this.dataGridView1.Rows[index].Cells["profit"].Value = one.profit;
            this.dataGridView1.Rows[index].Cells["profit_margin"].Value = one.profit_margin;
            this.dataGridView1.Rows[index].Cells["sum_cost"].Value = one.sum_cost;

            this.dataGridView1.Rows[index].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            // 设置亏损，显示为红色
            if (Convert.ToDouble(this.dataGridView1.Rows[index].Cells["profit"].Value) <= 0.0000)
            {
                this.dataGridView1.Rows[index].DefaultCellStyle.ForeColor = Color.Red;
            }

            // 记录统计信息
            statistic_record.ID ++;
            statistic_record.cnt += one.cnt;
            statistic_record.sum_cost += one.sum_cost;
            statistic_record.sum_price += one.sum_price;
            statistic_record.profit += one.profit;
        }

        private void initDoneListRecord(List<SellProfit> done_ls)
        {
            foreach (SellProfit one in done_ls)
            {
                int index = this.dataGridView1.Rows.Add();
                sellprofit_to_grid(one, index);
            }
        }

        private void initRecordStatisticLine()
        {
            int index = this.dataGridView1.Rows.Add();

            this.dataGridView1.Rows[index].Cells["serial"].Value = "合计";
            this.dataGridView1.Rows[index].Cells["sell_cnt"].Value = statistic_record.cnt;
            this.dataGridView1.Rows[index].Cells["sell_sum_price"].Value = statistic_record.sum_price;
            this.dataGridView1.Rows[index].Cells["sum_cost"].Value = statistic_record.sum_cost;
            this.dataGridView1.Rows[index].Cells["profit"].Value = statistic_record.profit;

            if (statistic_record.profit <= 0.0000)
            {
                this.dataGridView1.Rows[index].DefaultCellStyle.ForeColor = Color.Red;
            }

            this.dataGridView1.Rows[index].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataGridView1.Rows[index].DefaultCellStyle.Font = new Font("宋体", 10F, FontStyle.Bold);
        }


        private void initListRecord()
        {
            statistic_record = new SellProfit();
            this.dataGridView1.Rows.Clear();

            List<SellProfit> done_profit_ls = SellProfitDao.getInstance().FindList(this.start_time.Value, this.end_time.Value.AddDays(1), this.textBox_product.Text, this.textBox_customer.Text);

            initDoneListRecord(done_profit_ls);

            initRecordStatisticLine();
        }

        /// <summary>
        /// 按产品统计
        /// </summary>
        private void do_init_product_list()
        {
            List<SellProfit> done_profit_ls = SellProfitDao.getInstance().FindList(this.start_time.Value, this.end_time.Value.AddDays(1), this.textBox_product.Text, this.textBox_customer.Text);
            SellProfit sell_profit_obj = new SellProfit();
            
            // 根据商品进行分组
            Dictionary<int, List<SellProfit>> group = SellProfitDao.getInstance().group_by_product_id(done_profit_ls);
            foreach (KeyValuePair<int, List<SellProfit>> one in group)
            {
                int product_id = one.Key;
                List<SellProfit> sell_profit_ls = one.Value;

                SellProfit merge = new SellProfit();
                foreach (SellProfit sell in sell_profit_ls)
                {
                    merge.product = sell.product;
                    merge.unit = sell.unit;

                    merge.cnt += sell.cnt;
                    merge.sum_price += sell.sum_price;
                    merge.sum_cost += sell.sum_cost;
                    merge.profit += sell.profit;
                }

                //List<double> profit = sell_profit_obj.get_profit(merge.sum_cost, merge.sum_price);
                //这个地方没有考虑退货
                merge.profit_margin = (merge.sum_price - merge.sum_cost) / merge.sum_cost * 100;

                int index = this.dataGridView1.Rows.Add();
                sellprofit_to_grid(merge, index);
            }
        }

        private void initProductStatisticLine()
        {
            int index = this.dataGridView1.Rows.Add();

            this.dataGridView1.Rows[index].Cells["product"].Value = "合计";
            this.dataGridView1.Rows[index].Cells["sell_cnt"].Value = statistic_record.cnt;
            this.dataGridView1.Rows[index].Cells["sell_sum_price"].Value = statistic_record.sum_price;
            this.dataGridView1.Rows[index].Cells["sum_cost"].Value = statistic_record.sum_cost;
            this.dataGridView1.Rows[index].Cells["profit"].Value = statistic_record.profit;

            if (statistic_record.profit <= 0.0000)
            {
                this.dataGridView1.Rows[index].DefaultCellStyle.ForeColor = Color.Red;
            }

            this.dataGridView1.Rows[index].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataGridView1.Rows[index].DefaultCellStyle.Font = new Font("宋体", 10F, FontStyle.Bold);
        }

        private void initListProduct()
        {
            statistic_record = new SellProfit();
            this.dataGridView1.Rows.Clear();

            // 使用已经计算好的SellProfit表来统计就可以了
            do_init_product_list();

            initProductStatisticLine();
        }

        private void chg_visible(bool x)
        {
            serial.Visible = x;
            sell_time.Visible = x;
            customer.Visible = x;
            sell_price.Visible = x;
            cost.Visible = x;
        }

        private void initList()
        {
            // 按照明细
            if ((int)comboBox1.SelectedValue == 1)
            {
                chg_visible(true);
                initListRecord();
            }

            // 按照产品
            if ((int)comboBox1.SelectedValue == 2)
            {
                chg_visible(false);
                initListProduct();
            }
        }
        /// <summary>
        /// event
        /// </summary>

        public override void refresh()
        {
            this.label_notice.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            initList();
            this.label_notice.Visible = false;
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            e.Handled = true;
            e.PaintBackground(e.CellBounds, true);
            e.PaintContent(e.CellBounds);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string commandText = "";
            commandText = string.Format("delete * from ProductStainlessCirculationRecord;");
            DbHelperAccess.executeNonQuery(commandText);
            commandText = string.Format("delete * from ProductStainlessCirculation;");
            DbHelperAccess.executeNonQuery(commandText);
            commandText = string.Format("delete * from ProductStainless;");
            DbHelperAccess.executeNonQuery(commandText);
            commandText = string.Format("delete * from SellProfit;");
            DbHelperAccess.executeNonQuery(commandText);
        }
    }
}