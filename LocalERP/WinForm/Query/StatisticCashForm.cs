using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using WeifenLuo.WinFormsUI.Docking;
using LocalERP.DataAccess.Data;
using LocalERP.UiDataProxy;
using LocalERP.DataAccess.DataDAO;
using LocalERP.WinForm.Utility;
using LocalERP.DataAccess.Utility;

namespace LocalERP.WinForm
{
    public partial class StatisticCashForm : MyDockContent
    {
        private DataTable payReceiptDT;
        private DataTable circulationDT;


        double sumPayed, sumFreight;

        public StatisticCashForm(Form parentForm, string title)
            : base()
        {
            this.Owner = parentForm;
            InitializeComponent();

            this.backgroundWorker.DoWork += new DoWorkEventHandler(backgroundWorker_DoWork);
            this.backgroundWorker.RunWorkerCompleted +=new RunWorkerCompletedEventHandler(backgroundWorker_RunWorkerCompleted);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
        }

        public override void refresh()
        {
            this.label_notice.Visible = true;
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            payReceiptDT = PayReceiptDao.getInstance().FindList(null, null);
            circulationDT = ProductStainlessCirculationDao.getInstance().FindList(null, null, false, true);

            sumPayed = 0;
            sumFreight = 0;

            foreach (DataRow dr in circulationDT.Rows) { 
                double payed, freight;
                double.TryParse(dr["thisPayed"].ToString(), out payed);
                double.TryParse(dr["freight"].ToString(), out freight);
                sumPayed += payed;
                sumFreight += freight;
            }

        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.label_pay.Text = string.Format("采购支出:      {0}元", sumPayed);
            this.label_freight.Text = string.Format("运费支出:      {0}元", sumFreight);
            this.label_paySum.Text = string.Format("合计:      {0}元", sumFreight + sumPayed);
            
            this.invokeEndLoadNotify();
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            this.backgroundWorker.RunWorkerAsync();
            this.invokeBeginLoadNotify();
        }
    }
}