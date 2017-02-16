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

        private void ProductSellReportForm_Load(object sender, EventArgs e)
        {
            SellDataSet sellDS = new SellDataSet();
            /*
            TODO
            foreach(ProductCirculationRecord record in records)
                sellDS.DataTable1.AddDataTable1Row(record.ProductName, record.Num.ToString(), record.Price.ToString(), record.TotalPrice.ToString());
                */
            
            SellCrystalReport sellCR = new SellCrystalReport();
            TextObject to = sellCR.ReportDefinition.ReportObjects["sellTitle"] as TextObject;
            to.Text = "�������ڣ�" + sell.CirculationTime.ToString("yyyy��MM��dd��");

            TextObject customerTO = sellCR.ReportDefinition.ReportObjects["customer"] as TextObject;
            customerTO.Text = "�ͻ���"+sell.CustomerName;

            TextObject allTotalPriceTO = sellCR.ReportDefinition.ReportObjects["allTotalPrice"] as TextObject;
            allTotalPriceTO.Text = "�ܼۺϼ�/Ԫ��" + sell.RealTotal.ToString();

            // ���½ǵ� ��ϵ��ʽ �� ��ַ
            Customer customer = CustomerDao.getInstance().FindByID(sell.CustomerID);
            string customer_tel = customer.Tel;
            string customer_addr = customer.Address;
            string contract = "��ַ��" + customer_addr + "\n�绰��" + customer_tel;
            TextObject contract_obj = sellCR.ReportDefinition.ReportObjects["contract"] as TextObject;
            contract_obj.Text = contract;

            // ����
            TextObject title_obj = sellCR.ReportDefinition.ReportObjects["Text6"] as TextObject;
            title_obj.Text = "�ɹ���ⵥ";

            
            sellCR.SetDataSource(sellDS);

            this.crystalReportViewer1.ReportSource = sellCR;
        }
    }
}