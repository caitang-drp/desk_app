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

namespace LocalERP.WinForm
{
    public partial class ProductSellReportForm : Form
    {
        private ProductSell sell;
        private List<ProductSellRecord> records;

        public ProductSellReportForm(/*ProductSell sell, List<ProductSellRecord> records*/)
        {
            InitializeComponent();

            this.sell = sell;
            this.records = records;
        }

        private void ProductSellReportForm_Load(object sender, EventArgs e)
        {
            SellDataSet sellDS = new SellDataSet();
            foreach(ProductSellRecord record in records)
                sellDS.DataTable1.AddDataTable1Row(record.ProductName, record.Num.ToString(), record.Price.ToString(), record.TotalPrice.ToString());
            
            SellCrystalReport sellCR = new SellCrystalReport();
            TextObject to = sellCR.ReportDefinition.ReportObjects["sellTitle"] as TextObject;
            to.Text = "�������ڣ�" + sell.SellTime.ToString("yyyy��MM��dd��");

            TextObject customerTO = sellCR.ReportDefinition.ReportObjects["customer"] as TextObject;
            customerTO.Text = "�ͻ���"+sell.CustomerName;

            TextObject allTotalPriceTO = sellCR.ReportDefinition.ReportObjects["allTotalPrice"] as TextObject;
            allTotalPriceTO.Text = "�ܼۺϼ�/Ԫ��" + sell.TotalPrice.ToString();

            sellCR.SetDataSource(sellDS);

            this.crystalReportViewer1.ReportSource = sellCR;
        }
    }
}