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
            
            SellCrystalReport sellCR = new SellCrystalReport();

            // ��ȡ��Ӧ�̵���Ϣ
            Customer customer = CustomerDao.getInstance().FindByID(sell.CustomerID);
            string customer_tel = customer.Tel;
            string customer_addr = customer.Address;
            string contract = "�绰��" + customer_tel;

            // ���Ͻ�(��Ӧ�̣���ϵ��ַ)
            TextObject top_left = sellCR.ReportDefinition.ReportObjects["Text8"] as TextObject;
            top_left.Text = "��Ӧ�̣�"+ sell.CustomerName + "\n��ϵ��ַ��" + customer_addr;
            // �м�����(��ϵ�绰������)
            TextObject top_middle = sellCR.ReportDefinition.ReportObjects["Text10"] as TextObject;
            top_middle.Text = "��ϵ�绰��"+ customer_tel + "\n���ڣ�" + sell.CirculationTime.ToString("yyyy��MM��dd��");
            // ������(���ڣ�����)
            TextObject top_right = sellCR.ReportDefinition.ReportObjects["Text11"] as TextObject;
            top_right.Text = "�����ˣ�"+ sell.Oper + "\n���ţ�" + sell.Code;

            // ���� ��ϸ
            foreach(ProductCirculationRecord record in records)
                sellDS.DataTable1.AddDataTable1Row(record.ProductName, record.TotalNum.ToString(), record.Price.ToString(), record.TotalPrice.ToString());

            // �ϼ�
            TextObject allTotalPriceTO = sellCR.ReportDefinition.ReportObjects["allTotalPrice"] as TextObject;
            allTotalPriceTO.Text = "�ܼۺϼ�/Ԫ��" + sell.RealTotal.ToString();
            // ��ע
            TextObject beizhu = sellCR.ReportDefinition.ReportObjects["Text4"] as TextObject;
            beizhu.Text = "��ע��" + sell.Comment;


            sellCR.SetDataSource(sellDS);

            this.crystalReportViewer1.ReportSource = sellCR;
        }
    }
}