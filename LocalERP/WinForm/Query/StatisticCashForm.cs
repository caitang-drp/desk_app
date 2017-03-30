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
        //private DataTable payReceiptDT;
        private DataTable circulationDT;

        private DataTable productDT;
        private DataTable customerDT;

        double receipt, purchaseBack, otherReceipt;
        double payed, sellBack, freights, otherPay;

        double lib;
        double needPay, needReceipt;

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
            receipt = purchaseBack = otherReceipt = 0;
            payed = sellBack = freights = otherPay = 0;

            //payReceiptDT = PayReceiptDao.getInstance().FindList(null, null);
            //circulationDT = ProductStainlessCirculationDao.getInstance().FindList(null, null, false, true);

            payed = 0;
            freights = 0;

            foreach (DataRow dr in circulationDT.Rows) { 
                double pay, freight;
                ValidateUtility.getDouble(dr, "thisPayed", out pay);
                ValidateUtility.getDouble(dr, "freight", out freight);
                
                int type = 1;
                int.TryParse(dr["type"].ToString(), out type);
                if (type == 1)
                    payed += pay;
                else if (type == 2)
                    purchaseBack += pay;
                else if (type == 3)
                    receipt += pay;
                else if (type == 4)
                    sellBack += pay;

                freights += freight;
            }

            lib = 0;
            productDT = ProductStainlessDao.getInstance().FindList(null, null);
            foreach (DataRow dr in productDT.Rows) {
                double price;
                int num;
                bool positive;

                ValidateUtility.getInt(dr, "num", out num, out positive);
                ValidateUtility.getDouble(dr, "priceCost", out price);
                if (price <= 0)
                    ValidateUtility.getDouble(dr, "pricePurchase", out price);

                lib = lib + price * num;
            }

            needPay = needReceipt = 0;
            customerDT = CustomerDao.getInstance().FindList(null, null);

            foreach (DataRow dr in customerDT.Rows) {
                double arrear = 0;
                ValidateUtility.getDouble(dr, "arrear", out arrear);
                if (arrear > 0)
                    needPay += arrear;
                else
                    needReceipt -= arrear;
            }
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.label_receipt.Text = string.Format("销售收入:{0,10}元", receipt);
            this.label_purchaseBack.Text = string.Format("采购退货收入:{0,6}元", purchaseBack);
            this.label_otherReceipt.Text = string.Format("其他收入:{0,10}元", otherReceipt);
            double sumReceipt = receipt + purchaseBack + otherReceipt;
            this.label_sumReceipt.Text = string.Format("合计:{0,9}元", sumReceipt);

            this.label_pay.Text = string.Format("采购支出:{0,10}元", payed);
            this.label_sellBack.Text = string.Format("销售退货支出:{0,6}元", sellBack);
            this.label_freight.Text = string.Format("运费支出:{0,10}元", freights);
            this.label_otherPay.Text = string.Format("其他支出:{0,10}元", otherPay);
            double sumPay = payed + freights + sellBack + otherPay;
            this.label_paySum.Text = string.Format("合计:{0,9}元", sumPay);

            this.label_sumCash.Text = string.Format("结存金额:{0,10}元", sumReceipt - sumPay);


            this.label_lib.Text = string.Format("库存成本:{0,10}元", lib);
            this.label_needReceipt.Text = string.Format("应收货款:{0,10}元", needReceipt);
            this.label_sumCash1.Text = string.Format("结存金额:{0,10}元", sumReceipt - sumPay);
            this.label_assets.Text = string.Format("合计:{0,9}元", needReceipt + sumReceipt - sumPay + lib);

            this.label_needPay.Text = string.Format("应付货款:{0,10}元", needPay);
            this.label_debt.Text = string.Format("合计:{0,9}元", needPay);

            this.invokeEndLoadNotify();
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            this.label_notice.Visible = false;
            this.backgroundWorker.RunWorkerAsync();
            this.invokeBeginLoadNotify();
        }
    }
}