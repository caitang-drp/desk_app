using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LocalERP.CrystalReport;
using LocalERP.DataAccess.Data;
using CrystalDecisions.CrystalReports.Engine;
using LocalERP.DataAccess.DataDAO;

namespace LocalERP.WinForm
{
    public partial class ProductSellReportForm : Form
    {
        private ProductCirculation sell;
        private List<ProductCirculationRecord> records;

        public ProductSellReportForm(ProductCirculation sell, List<ProductCirculationRecord> records)
        {
            InitializeComponent();

            this.sell = sell;
            this.records = records;
        }

        private string get_cus_type()
        {
            string cus_type = "客户";

            // 采购
            if (sell.Type == 1 || sell.Type == 2)
            {
                cus_type = "供应商";
            }
            // 销售
            else if (sell.Type == 2 || sell.Type == 3)
            {
                cus_type = "客户";
            }

            return cus_type;
        }

        private void load_with_customer()
        {
            SellDataSet sellDS = new SellDataSet();

            SellCrystalReport sellCR = new SellCrystalReport();

            // 获取供应商的信息
            Customer customer = CustomerDao.getInstance().FindByID(sell.CustomerID);
            string customer_tel = customer.Tel;
            string customer_addr = customer.Address;
            string contract = "电话：" + customer_tel;

            // 左上角(供应商，联系地址)
            TextObject top_left = sellCR.ReportDefinition.ReportObjects["Text8"] as TextObject;
            top_left.Text = get_cus_type() + "：" + sell.CustomerName + "\n联系地址：" + customer_addr;

            // 中间上面(联系电话，日期)
            TextObject top_middle = sellCR.ReportDefinition.ReportObjects["Text10"] as TextObject;
            top_middle.Text = "联系电话："+ customer_tel + "\n日期：" + sell.CirculationTime.ToString("yyyy年MM月dd日");
            // 右上面(日期，单号)
            TextObject top_right = sellCR.ReportDefinition.ReportObjects["Text11"] as TextObject;
            top_right.Text = "经手人："+ sell.Oper + "\n单号：" + sell.Code;

            // 处理 明细
            foreach(ProductCirculationRecord record in records)
                sellDS.DataTable1.AddDataTable1Row(record.ProductName, record.TotalNum.ToString(), record.Price.ToString(), record.TotalPrice.ToString());

            // 合计
            TextObject allTotalPriceTO = sellCR.ReportDefinition.ReportObjects["allTotalPrice"] as TextObject;
            allTotalPriceTO.Text = "总价合计/元：" + sell.RealTotal.ToString();
            // 备注
            TextObject beizhu = sellCR.ReportDefinition.ReportObjects["Text4"] as TextObject;
            beizhu.Text = "备注：" + sell.Comment;
            // 标题
            TextObject ti = sellCR.ReportDefinition.ReportObjects["Text6"] as TextObject;
            //string title = new ProductCirculation().get_circulation_type_string((ProductCirculation.CirculationType)this.sell.Type) + "单";
            ti.Text = "title";

            sellCR.SetDataSource(sellDS);

            this.crystalReportViewer1.ReportSource = sellCR;
        }

        private void load_without_customer()
        {
            SellDataSet sellDS = new SellDataSet();

            SellCrystalReport sellCR = new SellCrystalReport();

            // 左上角(经手人, 单号)
            TextObject top_left = sellCR.ReportDefinition.ReportObjects["Text8"] as TextObject;
            top_left.Text = "经手人："+ sell.Oper + "\n单号：" + sell.Code;

            // 中间上角(经手人, 单号)
            TextObject top_mid = sellCR.ReportDefinition.ReportObjects["Text10"] as TextObject;
            top_mid.Text = "";

            // 右上面(日期)
            TextObject top_right = sellCR.ReportDefinition.ReportObjects["Text11"] as TextObject;
            top_right.Text = "日期：" + sell.CirculationTime.ToString("yyyy年MM月dd日");

            // 处理 明细
            foreach(ProductCirculationRecord record in records)
                sellDS.DataTable1.AddDataTable1Row(record.ProductName, record.TotalNum.ToString(), record.Price.ToString(), record.TotalPrice.ToString());

            // 合计
            TextObject allTotalPriceTO = sellCR.ReportDefinition.ReportObjects["allTotalPrice"] as TextObject;
            allTotalPriceTO.Text = "总价合计/元：" + sell.RealTotal.ToString();
            // 备注
            TextObject beizhu = sellCR.ReportDefinition.ReportObjects["Text4"] as TextObject;
            beizhu.Text = "备注：" + sell.Comment;
            // 标题
            TextObject ti = sellCR.ReportDefinition.ReportObjects["Text6"] as TextObject;
            //string title = new ProductCirculation().get_circulation_type_string((ProductCirculation.CirculationType)this.sell.Type) + "单";
            ti.Text = "title";

            sellCR.SetDataSource(sellDS);

            this.crystalReportViewer1.ReportSource = sellCR;
        }

        private void ProductSellReportForm_Load(object sender, EventArgs e)
        {
            if (sell.CustomerID == -1)
            {
                load_without_customer();
            }
            else
            {
                load_with_customer();
            }
        }
    }
}