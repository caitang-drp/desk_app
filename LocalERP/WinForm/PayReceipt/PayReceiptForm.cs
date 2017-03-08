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
    public partial class PayReceiptForm : MyDockContent
    {
        //open mode       | 0:add 1:edit | 2:pending  | 3:pending | 4:finish
        //status          | 1:apply      | 2:pending  | 3:pending | 4:finish  
        protected int openMode = 0;
        protected int payReceiptID = 0;
        protected PayReceipt.BillType type;
        protected UpdateType notifyType;
        protected UpdateType finishNotifyType;

        private int arrearDirection;
        private string code;

        private PayReceipt payReceipt = null;
        
        protected bool needSave = false;

        public PayReceiptForm(PayReceiptTypeConf conf)
        {
            InitializeComponent();

            openMode = 0;
            payReceiptID = 0;

            this.type = conf.type;
            this.notifyType = conf.notifyType;
            this.finishNotifyType = conf.finishNotifyType;

            this.arrearDirection = conf.arrearDirection;

            this.Text = conf.name + "单";
            this.label_title.Text = this.Text;
            this.code = conf.code;
            this.label_customer.Text = conf.customer;

            this.label_date.Text = conf.business + "时间:";
            this.label_thisPayed.Text = conf.business + "金额:";
        }

        private void PayReceiptForm_Load(object sender, EventArgs e)
        {
            this.lookupText1.LookupForm = FormSingletonFactory.getInstance().getCustomerCIForm_Select();
            this.lookupText1.valueSetted += new LookupText.ValueSetted(lookupText1_valueSetted);

            this.textBox_previousArrears.TextChanged += new EventHandler(setAccumulative);
            this.textBox_thisPayed.TextChanged += new EventHandler(setAccumulative);

            initPayReceipt();
        }

        public void reload(int mode, int id) {
            if (needSave && affirmQuit() != DialogResult.OK)
                return;
            
            openMode = mode;
            payReceiptID = id;
            initPayReceipt();
        }

        public override void refresh()
        {
            //not reasonal
            (this.lookupText1.LookupForm as CategoryItemForm).initTree();
        }

        /// <summary>
        /// for init circulation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void initPayReceipt()
        {
            if (openMode == 0)
            {
                switchMode(openMode);
                int max = PayReceiptDao.getInstance().getMaxCode(code);
                this.textBox_serial.Text = string.Format("{0}-{1}-{2:0000}", code, DateTime.Now.ToString("yyyyMMdd"), max+1);
                this.lookupText1.LookupArg = null;
                this.lookupText1.Text_Lookup = null;
                this.dateTime_time.Value = DateTime.Now;
                this.textBox_operator.Text = null;

                this.textBox_previousArrears.Text = null;
                this.textBox_thisPayed.Text = null;
                this.textBox_accumulative.Text = null;

                this.textBox_comment.Text = null;

                this.resetNeedSave(false);
                
                return;
            }
            
            payReceipt = PayReceiptDao.getInstance().FindByID(payReceiptID);

            this.textBox_serial.Text = payReceipt.serial;
            this.dateTime_time.Value = payReceipt.bill_time;
            this.textBox_comment.Text = payReceipt.comment;
            this.lookupText1.LookupArg = new LookupArg(payReceipt.customer_id, payReceipt.customerName);
            this.lookupText1.Text_Lookup = payReceipt.customerName;
            this.textBox_operator.Text = payReceipt.handle_people;

            //如果是未审核状态，以前欠款应该如何生成？
            this.textBox_previousArrears.Text = payReceipt.previousArrears.ToString();
            this.textBox_thisPayed.Text = payReceipt.amount.ToString();

            openMode = payReceipt.status;

            this.resetNeedSave(false);
            this.switchMode(payReceipt.status);
        }
        //end init

        private void switchMode(int mode) {
            switch(mode){
                case 0:
                    this.label_status.Text = "新增";
                    this.initControlsEnable(true, false, false, true);
                    break;
                case 1:
                    this.label_status.Text = PayReceipt.statusContext[0];
                    this.initControlsEnable(false, true, false, true);
                    break;
                /*case 2:
                    this.label_status.Text = ProductCirculation.circulationStatusContext[1];
                    this.initControlsEnable(false, false, true, true, false, false, false, true, true, true);
                    break;
                case 3:
                    this.label_status.Text = ProductCirculation.circulationStatusContext[2];
                    this.initControlsEnable(false, false, false, true, false, false, false, true, true, true);
                    break;*/
                case 4:
                    this.label_status.Text = ProductCirculation.circulationStatusContext[3];
                    this.initControlsEnable(false, false, false, false);
                    break;
                default:
                    break;
            }
        }


        private void initControlsEnable(bool save, bool finish, bool print, bool basicInfo)
        {
            this.toolStripButton_save.Enabled = save;
            this.toolStripButton_finish.Enabled = finish;
            this.toolStripButton_print.Enabled = print;

            this.textBox_serial.Enabled = basicInfo;
            this.dateTime_time.Enabled = basicInfo;
            this.lookupText1.Enabled = basicInfo;
            this.textBox_operator.Enabled = basicInfo;

            this.textBox_thisPayed.Enabled = basicInfo;

            this.textBox_comment.Enabled = basicInfo;
        }

        /// <summary>
        /// for get value from controls
        /// </summary>

        protected bool getPayReceipt(out PayReceipt payReceipt)
        {
            payReceipt = new PayReceipt();
            payReceipt.id = this.payReceiptID;
            payReceipt.bill_type = this.type;

            string name;
            if (ValidateUtility.getName(this.textBox_serial, this.errorProvider1, out name) == false)
                return false;
            payReceipt.serial = name;

            if (ValidateUtility.getLookupValueID(this.lookupText1, this.errorProvider1, out payReceipt.customer_id) == false)
                return false;

            payReceipt.bill_time = this.dateTime_time.Value;
            payReceipt.handle_people = textBox_operator.Text;

            payReceipt.previousArrears = Convert.ToDouble(this.textBox_previousArrears.Text);
            
            double pay;
            if (ValidateUtility.getDouble(this.textBox_thisPayed, this.errorProvider1, true, true, out pay) == false)
                return false;
            payReceipt.amount = pay;

            payReceipt.comment = textBox_comment.Text;

            return true;
        }

        /// <summary>
        /// for event
        /// </summary>
        ///
        protected void toolStripButton_save_Click(object sender, EventArgs e)
        {
            PayReceipt payReceipt;
            bool isCorrect = getPayReceipt(out payReceipt);

            if (isCorrect == false)
                return;

            try
            {
                if (openMode == 0)
                {
                    payReceipt.status = 1;
                    PayReceiptDao.getInstance().Insert(payReceipt, out payReceiptID);
                    MessageBox.Show(string.Format("增加{0}成功!", this.Text), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (openMode == 1)
                {
                    PayReceiptDao.getInstance().Update(payReceipt);
                    MessageBox.Show(string.Format("保存{0}成功!", this.Text), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                openMode = 1;
                this.initPayReceipt();


            }
            catch (Exception ex)
            {
                if (openMode == 0)
                    PayReceiptDao.getInstance().Delete(payReceiptID);
                MessageBox.Show("保存有误,可能是往来单位被修改过,请重新编辑!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            //so important: if edit ,it should be refresh also, because edit will del exist item and add new item

            this.invokeUpdateNotify(notifyType);
        }

        //审核
        private void toolStripButton_finish_Click(object sender, EventArgs e)
        {
            PayReceipt payReceipt;
            bool isCorrect = getPayReceipt(out payReceipt);

            if (isCorrect == false)
                return;
            
            payReceipt.status = 4;
            PayReceiptDao.getInstance().Update(payReceipt);
            CustomerDao.getInstance().update_arrear(payReceipt.customer_id, this.arrearDirection * Convert.ToDouble(this.textBox_accumulative.Text));
            MessageBox.Show("审核成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            openMode = 4;
            this.switchMode(4);

            this.invokeUpdateNotify(this.finishNotifyType);
        }


        private void toolStripButton_print_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("系统暂未开放打印功能.", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            PayReceipt sell;
            List<ProductCirculationRecord> records;
            this.getPayReceipt(out sell);
            //ProductSellReportForm form = new ProductSellReportForm(sell, records);
            //form.ShowDialog();
        }

        private DialogResult affirmQuit() {
            return MessageBox.Show("单据尚未保存，是否放弃保存？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }

        private void toolStripButton_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            if (this.needSave && affirmQuit() != DialogResult.OK)
                e.Cancel = true;
        }
       

        private void textBox_serial_Validating(object sender, CancelEventArgs e)
        {
            string temp;
            ValidateUtility.getName(this.textBox_serial, this.errorProvider1, out temp);
        }

        protected void resetNeedSave(bool value)
        {
            if (openMode < 2)
                this.toolStripButton_save.Enabled = value;
            else
                this.toolStripButton_save.Enabled = false;

            if (value == true)
                this.toolStripButton_finish.Enabled = false;

            needSave = value;
        }

        private void setAccumulative(object sender, EventArgs e)
        {
            double arrear, pay;
            double.TryParse(this.textBox_previousArrears.Text, out arrear);
            double.TryParse(this.textBox_thisPayed.Text, out pay);
            double accumulative = arrear - pay;
            this.textBox_accumulative.Text = accumulative.ToString("N2");
        }

        private void Controls_TextChanged(object sender, EventArgs e)
        {
            resetNeedSave(true);
        }

        private void lookupText1_valueSetted(object sender, LookupArg arg)
        {
            Customer customer = CustomerDao.getInstance().FindByID((int)arg.Value);
            this.textBox_previousArrears.Text = (customer.arrear*arrearDirection).ToString();
            resetNeedSave(true);
        }
    }
}