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
        private List<PayReceipt> payReceiptList;
        private List<ProductCirculation> circulationList;

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
            //backgroundWorker_DoWork(null, null);
        }

        public override void refresh()
        {
            this.label_notice.Visible = true;
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            receipt = purchaseBack = otherReceipt = 0;
            payed = sellBack = freights = otherPay = 0;

            payReceiptList = PayReceiptDao.getInstance().FindPayReceiptList(null, null, 4, null, 1);
            circulationList = ProductStainlessCirculationDao.getInstance().FindProductCirculationList(1, 4, null, null, 4, null);

            foreach (ProductCirculation cir in circulationList) { 
                
                int type = cir.Type;
                if (type == 1)
                    payed += cir.ThisPayed;
                else if (type == 2)
                    purchaseBack += cir.ThisPayed;
                else if (type == 3)
                    receipt += cir.ThisPayed;
                else if (type == 4)
                    sellBack += cir.ThisPayed;

                freights += cir.Freight;
            }

            foreach (PayReceipt pr in payReceiptList) {
                PayReceipt.BillType type = pr.bill_type;
                switch (type)
                {
                    case PayReceipt.BillType.BuyPay:
                        payed += pr.thisPayed;
                        break;
                    case PayReceipt.BillType.BuyRefund:
                        purchaseBack += pr.thisPayed;
                        break;
                    case PayReceipt.BillType.SellReceipt:
                        receipt += pr.thisPayed;
                        break;
                    case PayReceipt.BillType.SellRefund:
                        sellBack += pr.thisPayed;
                        break;
                    case PayReceipt.BillType.OtherPay:
                        otherPay += pr.thisPayed;
                        break;
                    case PayReceipt.BillType.OtherReceipt:
                        otherReceipt += pr.thisPayed;
                        break;
                    default:
                        break;
                }
            }

            lib = 0;
            productDT = ProductStainlessDao.getInstance().FindList(null, null);
            foreach (DataRow dr in productDT.Rows) {
                double price;
                int num;
                bool positive;

                ValidateUtility.getInt(dr, "num", out num, out positive);
                ValidateUtility.getDouble(dr, "priceCost", out price);
                //这里如果直接取ProductStainless就不需要判断
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
            this.label_receipt.Text = string.Format("销售收入:{0,10:0.00}元", receipt);
            this.label_purchaseBack.Text = string.Format("采购退货收入:{0,6:0.00}元", purchaseBack);
            this.label_otherReceipt.Text = string.Format("其他收入:{0,10:0.00}元", otherReceipt);
            double sumReceipt = receipt + purchaseBack + otherReceipt;
            this.label_sumReceipt.Text = string.Format("合计:{0,9:0.00}元", sumReceipt);

            this.label_pay.Text = string.Format("采购支出:{0,10:0.00}元", payed);
            this.label_sellBack.Text = string.Format("销售退货支出:{0,6:0.00}元", sellBack);
            //this.label_freight.Text = string.Format("运费支出:{0,10}元", freights);
            this.label_otherPay.Text = string.Format("其他支出:{0,10:0.00}元", otherPay + freights);
            double sumPay = payed + freights + sellBack + otherPay;
            this.label_paySum.Text = string.Format("合计:{0,9:0.00}元", sumPay);

            this.label_sumCash.Text = string.Format("收支结存:{0,10:0.00}元", sumReceipt - sumPay);


            this.label_lib.Text = string.Format("库存成本:{0,10:0.00}元", lib);
            this.label_needReceipt.Text = string.Format("应收货款:{0,10:0.00}元", needReceipt);
            this.label_sumCash1.Text = string.Format("收支结存:{0,10:0.00}元", sumReceipt - sumPay);
            double assets = needReceipt + sumReceipt - sumPay + lib;
            this.label_assets.Text = string.Format("合计:{0,9:0.00}元", assets);

            this.label_needPay.Text = string.Format("应付货款:{0,10:0.00}元", needPay);
            this.label_debt.Text = string.Format("合计:{0,9:0.00}元", needPay);

            this.label_pureLib.Text = string.Format("净资产:{0,10:0.00}元", assets - needPay);

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