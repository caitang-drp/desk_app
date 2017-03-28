using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LocalERP.DataAccess.DataDAO;
using LocalERP.DataAccess.Data;
using LocalERP.UiDataProxy;
using LocalERP.DataAccess.Utility;
using LocalERP.WinForm.Utility;
using System.IO;

namespace LocalERP.WinForm
{
    public partial class PayReceiptOtherForm : PayReceiptForm
    {
        public PayReceiptOtherForm(PayReceiptTypeConf conf):base(conf)
        {
            this.panel_history.Visible = false;
            this.label_tip.Text = "*该单不会自动核销债务,如需核销,请采用采购或销售收付款单";

            this.panel_customer.Visible = false;
            this.panel_sum.Visible = false;

            this.panel1.Location = new Point(this.panel1.Location.X, this.panel_sum.Location.Y);
        }

        //审核
        protected override void toolStripButton_finish_Click(object sender, EventArgs e)
        {
            PayReceipt payReceipt;
            bool isCorrect = getPayReceipt(out payReceipt);

            if (isCorrect == false)
                return;

            payReceipt.status = 4;
            PayReceiptDao.getInstance().Update(payReceipt);
            //CustomerDao.getInstance().update_arrear(payReceipt.customer_id, this.arrearDirection * Convert.ToDouble(this.textBox_accumulative.Text));
            MessageBox.Show("审核成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            openMode = 4;
            this.switchMode(4);

            this.invokeUpdateNotify(conf.finishNotifyType);
        }
    }
}