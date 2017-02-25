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

        private int flowType;
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

            this.flowType = conf.flowType;

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

        private bool previousArrearNeedChanged = true;
        private bool thisPayedNeedChanged = true;

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
            /*
            circulation = null;//PayReceiptDao.getInstance().FindByID(circulationID);

            this.textBox_serial.Text = circulation.Code;
            this.dateTime_sellTime.Value = circulation.CirculationTime;
            this.textBox_comment.Text = circulation.Comment;
            this.lookupText1.LookupArg = new LookupArg(circulation.CustomerID, circulation.CustomerName);
            this.lookupText1.Text_Lookup = circulation.CustomerName;
            this.textBox_operator.Text = circulation.Oper;

            this.dataGridView2[1, 0].Value = circulation.Total;

            //textbox_cutoff是自动计算的，类似textbox_accumulative
            this.textBox_realTotal.Text = circulation.RealTotal.ToString();
            
            //this.textBox_cutoff.Text = circulation.Total == 0?"":string.Format("{0:F}", circulation.RealTotal / circulation.Total);
            
            this.textBox_previousArrears.Text = circulation.PreviousArrears.ToString();
            this.textBox_thisPayed.Text = circulation.ThisPayed.ToString();
            this.textBox_accumulative.Text = string.Format("{0}", circulation.PreviousArrears + circulation.RealTotal - circulation.ThisPayed);*/
        }
        //end init

        private void switchMode(int mode) { /*
            switch(mode){
                case 0:
                    this.label_status.Text = "新增";
                    this.initControlsEnable(true, false, false, false, true, true, true, false, false,true);
                    break;
                case 1:
                    this.label_status.Text = ProductCirculation.circulationStatusContext[0];
                    this.initControlsEnable(true, true, true, false, true, true, true, false, false,true);
                    break;
                case 2:
                    this.label_status.Text = ProductCirculation.circulationStatusContext[1];
                    this.initControlsEnable(false, false, true, true, false, false, false, true, true, true);
                    break;
                case 3:
                    this.label_status.Text = ProductCirculation.circulationStatusContext[2];
                    this.initControlsEnable(false, false, false, true, false, false, false, true, true, true);
                    break;
                case 4:
                    this.label_status.Text = ProductCirculation.circulationStatusContext[3];
                    this.initControlsEnable(false, false, false, true, false, false, false, false, true, true);
                    break;
                default:
                    break;
            }*/
        }


        private void initControlsEnable(bool save, bool approval, bool finish, bool print, bool basicInfo,
            bool add, bool del, bool saveArrival, bool elementReadonly, bool pay)
        {/*
            //this.toolStripButton_save.Enabled = save;
            this.toolStripButton_approval.Enabled = approval;
            this.toolStripButton_finish.Enabled = finish;
            this.toolStripButton_print.Enabled = print;

            this.textBox_serial.Enabled = basicInfo;
            this.dateTime_sellTime.Enabled = basicInfo;
            this.textBox_comment.Enabled = basicInfo;
            this.lookupText1.Enabled = basicInfo;
            this.textBox_operator.Enabled = basicInfo;

            this.button_add.Enabled = add;
            this.button_del.Enabled = del;
            //this.toolStripButton_saveArrival.Enabled = saveArrival;
            this.panel_pay.Enabled = pay;*/
        }

        /// <summary>
        /// for get value from controls
        /// </summary>

        protected bool getPayReceipt(out PayReceipt payReceipt)
        {
            payReceipt = new PayReceipt();
            payReceipt.bill_type = this.type;

            payReceipt.serial = textBox_serial.Text;
            ValidateUtility.getLookupValueID(this.lookupText1, this.errorProvider1, out payReceipt.customer_id);
            payReceipt.bill_time = this.dateTime_time.Value;
            payReceipt.handle_people = textBox_operator.Text;

            payReceipt.previousArrears = Convert.ToDouble(this.textBox_previousArrears.Text);
            payReceipt.amount = Convert.ToDouble(this.textBox_thisPayed.Text);

            payReceipt.comment = textBox_comment.Text;

            /*
            if (ValidateUtility.getName(this.textBox_serial, this.errorProvider1, out name) == false)
                return false;
            circulation.Code = name;

            int customerID=-1;
            if (this.lookupText1.Visible == true && ValidateUtility.getLookupValueID(this.lookupText1, this.errorProvider1, out customerID) == false)
                return false;

            circulation.CustomerID = customerID;

            circulation.CirculationTime = this.dateTime_time.Value;
            circulation.Comment = this.textBox_comment.Text;
            circulation.Oper = this.textBox_operator.Text;
            */
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
                {/*
                    ProductStainlessCirculationDao.getInstance().UpdateBaiscInfo(circulation);
                    if (recordChanged)
                        ProductStainlessCirculationDao.getInstance().updateRecords(circulation.ID, records);
                    MessageBox.Show(string.Format("保存{0}成功!", this.Text), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);*/
                }

                openMode = 1;
                //this.initPayReceipt();


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
        {/*
            if (MessageBox.Show("审核后，将修改库存数量，且该单据不能修改或删除，是否审核？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK)
                return;

            List<ProductCirculationRecord> records;
            this.getRecords(out records);

            ProductCirculation sell;
            this.getCirculation(out sell);

            foreach (ProductCirculationRecord record in records)
            {
                int leftNum = cirDao.getProductDao().FindNumByID(record.ProductID);
                int newLeftNum = leftNum + flowType * record.TotalNum;
                cirDao.getProductDao().UpdateNum(record.ProductID, newLeftNum);
            }

            cirDao.UpdateStatus(circulationID, 4);
 
            MessageBox.Show("审核成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            openMode = 4;
            this.switchMode(4);

            this.invokeUpdateNotify(this.finishNotifyType);
            */
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

        private void setAccumulative() {
            double arrear, pay;
            double.TryParse(this.textBox_previousArrears.Text, out arrear);
            double.TryParse(this.textBox_thisPayed.Text, out pay);
            double accumulative = arrear - pay;
            this.textBox_accumulative.Text = accumulative.ToString();
        }

        private void Controls_TextChanged(object sender, EventArgs e)
        {
            resetNeedSave(true);
        }

        private void lookupText1_valueSetted(object sender, LookupArg arg)
        {
            Customer customer = CustomerDao.getInstance().FindByID((int)arg.Value);
            this.textBox_previousArrears.Text = customer.arrear.ToString();
            resetNeedSave(true);
        }

        private void textBox_previousArrears_TextChanged(object sender, EventArgs e)
        {
            this.previousArrearNeedChanged = false;

            if (this.thisPayedNeedChanged == true)
            {
                this.setAccumulative();
            }

            this.previousArrearNeedChanged = true;
        }

        private void textBox_thisPayed_TextChanged(object sender, EventArgs e)
        {
            this.thisPayedNeedChanged = false;

            if (this.previousArrearNeedChanged == true)
            {
                this.setAccumulative();
            }

            this.thisPayedNeedChanged = true;
        }
    }
}