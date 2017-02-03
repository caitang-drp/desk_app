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
    public partial class ElementOrderReportForm : Form
    {
        private ElementOrder order;
        private List<ElementOrderRecord> records;

        public ElementOrderReportForm(ElementOrder order, List<ElementOrderRecord> records)
        {
            InitializeComponent();

            this.order = order;
            this.records = records;
        }

        private void ElementOrderReportForm_Load(object sender, EventArgs e)
        {
            OrderDataSet orderDS = new OrderDataSet();
            foreach(ElementOrderRecord record in records)
                orderDS.DataTable1.AddDataTable1Row(record.Name, record.Number.ToString());
            
            OrderCrystalReport orderCR = new OrderCrystalReport();
            TextObject to = orderCR.ReportDefinition.ReportObjects["orderTitle"] as TextObject;
            to.Text = "订单日期:" + order.OrderTime.ToString("yyyy年MM月dd日");

            orderCR.SetDataSource(orderDS);

            this.crystalReportViewer1.ReportSource = orderCR;

            //this.reportViewer1.RefreshReport();
        }
    }
}