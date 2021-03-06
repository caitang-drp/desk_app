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
using gregn6Lib;

namespace LocalERP.WinForm
{
    public partial class PayReceiptForm : MyDockContent
    {
        //open mode       | 0:add 1:edit | 2:pending  | 3:pending | 4:finish
        //status          | 1:apply      | 2:pending  | 3:pending | 4:finish  
        protected int openMode = 0;
        protected int payReceiptID = 0;

        protected PayReceiptTypeConf conf;
        protected PayReceipt payReceipt = null;
        
        protected bool needSave = false;

        protected ProductCirculationDao cirDao;

        public PayReceiptForm(PayReceiptTypeConf conf, ProductCirculationDao cd)
        {
            InitializeComponent();

            openMode = 0;
            payReceiptID = 0;

            this.conf = conf;

            if (conf.type == PayReceipt.BillType.BuyPay || conf.type == PayReceipt.BillType.SellReceipt)
                this.panel_sum.Visible = false;
            else if (conf.type == PayReceipt.BillType.BuyRefund || conf.type == PayReceipt.BillType.SellRefund)
                this.label_pay_need.Visible = false;

            this.Text = conf.name + "单";
            this.label_title.Text = this.Text;
            this.label_customer.Text = conf.customer;

            this.label_date.Text = "开单时间:";

            if (conf.type == PayReceipt.BillType.BuyRefund || conf.type == PayReceipt.BillType.SellRefund)
                this.label_needPayed.Text = conf.cashDirection == -1 ? "退点金额(应付):" : "退点金额(应收):";
            this.label_thisPayed.Text = conf.cashDirection == -1 ? "本单已付:" : "本单已收:";
            this.label_arrears.Text = conf.arrearDirection == 1 ? "以上欠款(应付):" : "以上欠款(应收):";
            this.label_accumulative.Text = conf.arrearDirection == 1 ? "累计欠款(应付):" : "累计欠款(应收):";

            this.cirDao = cd;
        }

        private void PayReceiptForm_Load(object sender, EventArgs e)
        {
            this.lookupText1.LookupForm = FormSingletonFactory.getInstance().getCustomerCIForm_Select();
            this.lookupText1.valueSetted += new LookupText.ValueSetted(lookupText1_valueSetted);

            this.textBox_sum.TextChanged += new EventHandler(setAccumulative);
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
                int max = PayReceiptDao.getInstance().getMaxCode(conf.serial);
                this.textBox_serial.Text = string.Format("{0}-{1}-{2:0000}", conf.serial, DateTime.Now.ToString("yyyyMMdd"), max+1);
                this.lookupText1.LookupArg = null;
                this.lookupText1.Text_Lookup = null;
                this.dateTime_time.Value = DateTime.Now;
                this.textBox_operator.Text = null;

                this.textBox_sum.Text = null;
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
            this.textBox_operator.Text = payReceipt.handle_people;

            this.textBox_sum.Text = payReceipt.amount.ToString();

            this.lookupText1.LookupArg = new LookupArg(payReceipt.customer_id, payReceipt.customerName);
            this.lookupText1.Text_Lookup = payReceipt.customerName;

            //如果是未审核状态，以前欠款应该如何生成？
            if(payReceipt.status > 1)
                this.textBox_previousArrears.Text = payReceipt.previousArrears.ToString();

            this.textBox_thisPayed.Text = payReceipt.thisPayed.ToString();

            openMode = payReceipt.status;

            this.resetNeedSave(false);
            this.switchMode(payReceipt.status);
        }
        //end init

        protected void switchMode(int mode) {
            switch(mode){
                case 0:
                    this.label_status.Text = "新增";
                    this.initControlsEnable(true, false, false, false, true);
                    break;
                case 1:
                    this.label_status.Text = PayReceipt.statusContext[0];
                    this.initControlsEnable(false, true, false, false, true);
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
                    this.initControlsEnable(false, false, true, true, false);
                    break;
                default:
                    break;
            }
        }


        private void initControlsEnable(bool save, bool finish, bool finishCancel, bool print, bool basicInfo)
        {
            this.toolStripButton_save.Enabled = save;
            this.toolStripButton_finish.Enabled = finish;
            this.toolStripButton_finishCancel.Enabled = finishCancel;

            this.toolStripButton_print.Enabled = print;

            this.textBox_serial.Enabled = basicInfo;
            //this.dateTime_time.Enabled = basicInfo;
            this.lookupText1.Enabled = basicInfo;
            this.textBox_operator.Enabled = basicInfo;

            this.textBox_sum.Enabled = basicInfo;
            this.textBox_thisPayed.Enabled = basicInfo;

            this.textBox_comment.Enabled = basicInfo;
        }

        /// <summary>
        /// for get value from controls
        /// </summary>
        //没有获取status
        protected bool getPayReceipt(out PayReceipt payReceipt)
        {
            payReceipt = new PayReceipt();
            payReceipt.id = this.payReceiptID;
            payReceipt.bill_type = conf.type;
            payReceipt.bill_time = this.dateTime_time.Value;
            payReceipt.handle_people = textBox_operator.Text;
            payReceipt.comment = textBox_comment.Text;

            payReceipt.cashDirection = conf.cashDirection;
            payReceipt.arrearDirection = conf.arrearDirection;

            string name;
            if (ValidateUtility.getName(this.textBox_serial, this.errorProvider1, out name) == false)
                return false;
            payReceipt.serial = name;

            if (this.lookupText1.Visible == true && ValidateUtility.getLookupValueID(this.lookupText1, this.errorProvider1, out payReceipt.customer_id) == false)
                return false;
            
            double sum=0, pay, previousArrears;
            if ((this.panel_sum.Visible == false || ValidateUtility.getDouble(this.textBox_sum, this.errorProvider1, true, true, out sum))
                //如果是退点，本单付款就不是必填，否则为必填
                && (this.label_pay_need.Visible == true && ValidateUtility.getDouble(this.textBox_thisPayed, this.errorProvider1, true, true, out pay) || this.label_pay_need.Visible == false && ValidateUtility.getDouble(this.textBox_thisPayed, this.errorProvider1, false, true, out pay))
                && ValidateUtility.getDouble(this.textBox_previousArrears, this.errorProvider1, false, false, out previousArrears))
            {
                payReceipt.amount = sum;
                //欠款用的是呈现给用户看得，一般为正数
                payReceipt.previousArrears = previousArrears;
                payReceipt.thisPayed = pay;
            }
            else
                return false;

            return true;
        }

        /// <summary>
        /// for event
        /// </summary>
        ///
        protected virtual void toolStripButton_save_Click(object sender, EventArgs e)
        {
            //2018-3-30：防止其他窗口有更新
            this.refreshArrears();

            PayReceipt payReceipt;
            //get的时候没有status
            bool isCorrect = getPayReceipt(out payReceipt);

            if (isCorrect == false)
                return;

            if (this.openMode == 1 && PayReceiptDao.getInstance().FindByID(payReceipt.id) == null)
            {
                MessageBox.Show("该单据已经被删除了。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

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
                    payReceipt.status = 1;
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

            this.invokeUpdateNotify(conf.notifyType);
        }

        //审核
        protected virtual void toolStripButton_finish_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("审核后，将修改相关债务信息，是否审核？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK)
                return;

            //2017-11-20
            this.refreshArrears();

            //要重新get一下，因为债务有可能更新。
            PayReceipt payReceipt;
            bool isCorrect = getPayReceipt(out payReceipt);
            if (isCorrect == false)
                return;

            if (PayReceiptDao.getInstance().FindByID(payReceipt.id) == null)
            {
                MessageBox.Show("该单据已经被删除了。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            payReceipt.bill_time = DateTime.Now;
            this.dateTime_time.Value = payReceipt.bill_time;
            //重置了dateTime控件后，neeSave会变为true，但是由于是系统自动更新时间，所以不需要用户保存！耦合度太高了
            this.needSave = false;

            payReceipt.status = 4;
            PayReceiptDao.getInstance().Update(payReceipt);
            CustomerDao.getInstance().update_arrear(payReceipt.customer_id, conf.arrearDirection * Convert.ToDouble(this.textBox_accumulative.Text));
            this.initPayReceipt();

            MessageBox.Show("审核成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            openMode = 4;
            this.switchMode(4);

            this.invokeUpdateNotify(conf.finishNotifyType);
        }

        //弃核
        protected virtual void toolStripButton_finishCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否弃核，退回到未审核状态？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK)
                return;

            PayReceipt maxPr = PayReceiptDao.getInstance().FindLastestByCustomerID(this.payReceipt.customer_id, false);
            if (maxPr!=null && !maxPr.serial.Equals(this.payReceipt.serial))
            {
                MessageBox.Show(string.Format("弃核失败，在此单之后存在已审核的单据，请先弃核{0}", maxPr.serial), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ProductCirculation pc = cirDao.FindLastestByCustomerID(this.payReceipt.customer_id, false);
            if (pc!=null && pc.CirculationTime > payReceipt.bill_time)
            {
                MessageBox.Show(string.Format("弃核失败，在此单之后存在已审核的单据，请先弃核{0}", pc.Code), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //直接取以前的欠款，简单
            double arrear;
            double.TryParse(this.textBox_previousArrears.Text, out arrear);

            CustomerDao.getInstance().update_arrear(this.payReceipt.customer_id, this.conf.arrearDirection * arrear);
            PayReceiptDao.getInstance().UpdateStatus(this.payReceipt.id, 1);

            MessageBox.Show("弃核成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            openMode = 1;
            this.switchMode(1);

            this.invokeUpdateNotify(this.conf.finishNotifyType);
        }

        private DialogResult affirmQuit() {
            return MessageBox.Show(string.Format("{0}单据尚未保存，是否放弃保存？", conf.name), "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
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
            double arrear, pay, sum;
            double.TryParse(this.textBox_previousArrears.Text, out arrear);
            double.TryParse(this.textBox_thisPayed.Text, out pay);
            double.TryParse(this.textBox_sum.Text, out sum);

            double accumulative = (conf.arrearDirection * arrear - conf.arrearDirection * sum + conf.cashDirection * pay) * conf.arrearDirection;
            this.textBox_accumulative.Text = accumulative.ToString("0.00");//("N2");
        }

        private void Controls_TextChanged(object sender, EventArgs e)
        {
            resetNeedSave(true);
        }

        private void lookupText1_valueSetted(object sender, LookupArg arg)
        {
            Customer customer = CustomerDao.getInstance().FindByID((int)arg.Value);
            this.textBox_previousArrears.Text = (customer.arrear*conf.arrearDirection).ToString();
            resetNeedSave(true);
        }

        //2017-11-20为了防止有多个窗口打开，同时审核出现的问题
        private void refreshArrears()
        {
            if (!string.IsNullOrEmpty(this.lookupText1.Text_Lookup))
            {
                int cID = 0;
                int.TryParse(this.lookupText1.LookupArg.Value.ToString(), out cID);
                Customer customer = CustomerDao.getInstance().FindByID(cID);
                this.textBox_previousArrears.Text = (customer.arrear * conf.arrearDirection).ToString();
            }
            else
                this.textBox_previousArrears.Text = "";
        }


        private void toolStripButton_print_Click(object sender, EventArgs e)
        {
            // 载入报表模板数据
            string report_template_path = ConfUtility.payReceipt_report_path;
            Report.LoadFromFile(report_template_path);
            //一定要先-=，要不会重复数据
            Report.FetchRecord -= new _IGridppReportEvents_FetchRecordEventHandler(load_print_info);
            Report.FetchRecord += new _IGridppReportEvents_FetchRecordEventHandler(load_print_info);
            // 打印预览
            Report.PrintPreview(true);
        }

        //定义Grid++Report报表主对象
        private GridppReport Report = new GridppReport();

        private void load_print_info()
        {
            Customer customer = CustomerDao.getInstance().FindByID(payReceipt.customer_id);
            DataTable dt = ConfDao.getInstance().GetAll();

            Report.ControlByName("title").AsStaticBox.Text = ConfDao.getInstance().Get(3).ToString() + conf.name + "单";
            
            if (Report.ControlByName("addressValue") != null)
                Report.ControlByName("addressValue").AsStaticBox.Text = dt.Rows[3]["conf"].ToString();

            if (Report.ControlByName("bankValue") != null)
                Report.ControlByName("bankValue").AsStaticBox.Text = dt.Rows[7]["conf"].ToString();

            if (Report.ControlByName("commentValue") != null)
                Report.ControlByName("commentValue").AsStaticBox.Text = dt.Rows[8]["conf"].ToString();

            if (Report.ControlByName("contractorValue") != null)
                Report.ControlByName("contractorValue").AsStaticBox.Text = dt.Rows[4]["conf"].ToString();

            if (Report.ControlByName("telValue") != null)
                Report.ControlByName("telValue").AsStaticBox.Text = dt.Rows[5]["conf"].ToString();

            if (Report.ControlByName("phoneValue") != null)
                Report.ControlByName("phoneValue").AsStaticBox.Text = dt.Rows[6]["conf"].ToString();

            // (用户，供应商)
            Report.ControlByName("customer").AsStaticBox.Text = string.Format("{0}{1}{2}", conf.customer, payReceipt.customerName, String.IsNullOrEmpty(customer.Phone) ? "" : "(手机:" + customer.Phone + ")");

            if (Report.ControlByName("customerAddr") != null)
                Report.ControlByName("customerAddr").AsStaticBox.Text = "客户地址: " + customer.Address;

            // (日期)
            Report.ControlByName("date").AsStaticBox.Text = "开单时间: " + payReceipt.bill_time.ToString("yyyy年MM月dd日");

            // 右(单号)
            if (Report.ControlByName("serial") != null)
                Report.ControlByName("serial").AsStaticBox.Text = "单号: NO." + payReceipt.serial;

            
            // 备注
            if (Report.ControlByName("text_comment") != null)
                Report.ControlByName("text_comment").AsStaticBox.Text = payReceipt.comment;

            if ((conf.type == PayReceipt.BillType.BuyRefund || conf.type == PayReceipt.BillType.SellRefund) && Report.ControlByName("amount") != null)
                Report.ControlByName("amount").AsStaticBox.Text = string.Format("{0}    {1:0.00}元", this.label_needPayed.Text, double.Parse(this.textBox_sum.Text));

            Report.ControlByName("label_pay").AsStaticBox.Text = this.label_thisPayed.Text;
            Report.ControlByName("text_pay").AsStaticBox.Text = string.Format("{0:0.00}元", double.Parse(this.textBox_thisPayed.Text));

            Report.ControlByName("label_arr").AsStaticBox.Text = this.label_arrears.Text;
            Report.ControlByName("text_arr").AsStaticBox.Text = string.Format("{0:0.00}元", double.Parse(this.textBox_previousArrears.Text));

            Report.ControlByName("label_acc").AsStaticBox.Text = this.label_accumulative.Text;
            decimal temp1;
            decimal.TryParse(this.textBox_accumulative.Text, out temp1);
            Report.ControlByName("text_acc").AsStaticBox.Text = string.Format("{0:0.00}元 ({1})", this.textBox_accumulative.Text, DataUtility.CmycurD(temp1));
            //因为有些单据是没有的
            if (Report.ControlByName("oper") != null)
                Report.ControlByName("oper").AsStaticBox.Text = payReceipt.handle_people;
            
        }
    }
}