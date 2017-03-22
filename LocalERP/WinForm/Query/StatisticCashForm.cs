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
            circulationDT = ProductStainlessCirculationDao.getInstance().FindList(null, null, true);
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            foreach (DataRow dr in payReceiptDT.Rows)
            {

            }

            foreach (DataRow dr in circulationDT.Rows)
            {
            }
           
        }

        private void button_add_Click(object sender, EventArgs e)
        {
        }
    }
}