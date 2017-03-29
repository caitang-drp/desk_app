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
            string cus_type = "�ͻ�";

            // �ɹ�
            if (sell.Type == 1 || sell.Type == 2)
            {
                cus_type = "��Ӧ��";
            }
            // ����
            else if (sell.Type == 2 || sell.Type == 3)
            {
                cus_type = "�ͻ�";
            }

            return cus_type;
        }

        private void load_with_customer()
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
            top_left.Text = get_cus_type() + "��" + sell.CustomerName + "\n��ϵ��ַ��" + customer_addr;

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
            // ����
            TextObject ti = sellCR.ReportDefinition.ReportObjects["Text6"] as TextObject;
            //string title = new ProductCirculation().get_circulation_type_string((ProductCirculation.CirculationType)this.sell.Type) + "��";
            ti.Text = "title";

            sellCR.SetDataSource(sellDS);

            this.crystalReportViewer1.ReportSource = sellCR;
        }

        private void load_without_customer()
        {
            SellDataSet sellDS = new SellDataSet();

            SellCrystalReport sellCR = new SellCrystalReport();

            // ���Ͻ�(������, ����)
            TextObject top_left = sellCR.ReportDefinition.ReportObjects["Text8"] as TextObject;
            top_left.Text = "�����ˣ�"+ sell.Oper + "\n���ţ�" + sell.Code;

            // �м��Ͻ�(������, ����)
            TextObject top_mid = sellCR.ReportDefinition.ReportObjects["Text10"] as TextObject;
            top_mid.Text = "";

            // ������(����)
            TextObject top_right = sellCR.ReportDefinition.ReportObjects["Text11"] as TextObject;
            top_right.Text = "���ڣ�" + sell.CirculationTime.ToString("yyyy��MM��dd��");

            // ���� ��ϸ
            foreach(ProductCirculationRecord record in records)
                sellDS.DataTable1.AddDataTable1Row(record.ProductName, record.TotalNum.ToString(), record.Price.ToString(), record.TotalPrice.ToString());

            // �ϼ�
            TextObject allTotalPriceTO = sellCR.ReportDefinition.ReportObjects["allTotalPrice"] as TextObject;
            allTotalPriceTO.Text = "�ܼۺϼ�/Ԫ��" + sell.RealTotal.ToString();
            // ��ע
            TextObject beizhu = sellCR.ReportDefinition.ReportObjects["Text4"] as TextObject;
            beizhu.Text = "��ע��" + sell.Comment;
            // ����
            TextObject ti = sellCR.ReportDefinition.ReportObjects["Text6"] as TextObject;
            //string title = new ProductCirculation().get_circulation_type_string((ProductCirculation.CirculationType)this.sell.Type) + "��";
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