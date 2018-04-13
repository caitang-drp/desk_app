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

            this.Text = conf.name + "��";
            this.label_title.Text = this.Text;
            this.label_customer.Text = conf.customer;

            this.label_date.Text = conf.business + "ʱ��:";

            if (conf.type == PayReceipt.BillType.BuyRefund || conf.type == PayReceipt.BillType.SellRefund)
                this.label_needPayed.Text = conf.cashDirection == -1 ? "�˵���(Ӧ��):" : "�˵���(Ӧ��):";
            this.label_thisPayed.Text = conf.cashDirection == -1 ? "�����Ѹ�:" : "��������:";
            this.label_arrears.Text = conf.arrearDirection == 1 ? "����Ƿ��(Ӧ��):" : "����Ƿ��(Ӧ��):";
            this.label_accumulative.Text = conf.arrearDirection == 1 ? "�ۼ�Ƿ��(Ӧ��):" : "�ۼ�Ƿ��(Ӧ��):";

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

            //�����δ���״̬����ǰǷ��Ӧ��������ɣ�
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
                    this.label_status.Text = "����";
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
                    this.initControlsEnable(false, false, true, false, false);
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
        //û�л�ȡstatus
        protected bool getPayReceipt(out PayReceipt payReceipt)
        {
            payReceipt = new PayReceipt();
            payReceipt.id = this.payReceiptID;
            payReceipt.bill_type = conf.type;
            payReceipt.bill_time = this.dateTime_time.Value;
            payReceipt.handle_people = textBox_operator.Text;
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
                //������˵㣬��������Ͳ��Ǳ������Ϊ����
                && (this.label_pay_need.Visible == true && ValidateUtility.getDouble(this.textBox_thisPayed, this.errorProvider1, true, true, out pay) || this.label_pay_need.Visible == false && ValidateUtility.getDouble(this.textBox_thisPayed, this.errorProvider1, false, true, out pay))
                && ValidateUtility.getDouble(this.textBox_previousArrears, this.errorProvider1, false, false, out previousArrears))
            {
                payReceipt.amount = sum;
                //Ƿ���õ��ǳ��ָ��û����ã�һ��Ϊ����
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
            //2018-3-30����ֹ���������и���
            this.refreshArrears();

            PayReceipt payReceipt;
            //get��ʱ��û��status
            bool isCorrect = getPayReceipt(out payReceipt);

            if (isCorrect == false)
                return;

            try
            {
                if (openMode == 0)
                {
                    payReceipt.status = 1;
                    PayReceiptDao.getInstance().Insert(payReceipt, out payReceiptID);
                    MessageBox.Show(string.Format("����{0}�ɹ�!", this.Text), "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (openMode == 1)
                {
                    payReceipt.status = 1;
                    PayReceiptDao.getInstance().Update(payReceipt);
                    MessageBox.Show(string.Format("����{0}�ɹ�!", this.Text), "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                openMode = 1;
                this.initPayReceipt();


            }
            catch (Exception ex)
            {
                if (openMode == 0)
                    PayReceiptDao.getInstance().Delete(payReceiptID);
                MessageBox.Show("��������,������������λ���޸Ĺ�,�����±༭!", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            //so important: if edit ,it should be refresh also, because edit will del exist item and add new item

            this.invokeUpdateNotify(conf.notifyType);
        }

        //���
        protected virtual void toolStripButton_finish_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("��˺󣬽��޸����ծ����Ϣ���Ƿ���ˣ�", "��ʾ", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK)
                return;

            //2017-11-20
            this.refreshArrears();

            //Ҫ����getһ�£���Ϊծ���п��ܸ��¡�
            PayReceipt payReceipt;
            bool isCorrect = getPayReceipt(out payReceipt);

            if (isCorrect == false)
                return;

            payReceipt.bill_time = DateTime.Now;
            this.dateTime_time.Value = payReceipt.bill_time;
            //������dateTime�ؼ���neeSave���Ϊtrue������������ϵͳ�Զ�����ʱ�䣬���Բ���Ҫ�û����棡��϶�̫����
            this.needSave = false;

            payReceipt.status = 4;
            PayReceiptDao.getInstance().Update(payReceipt);
            CustomerDao.getInstance().update_arrear(payReceipt.customer_id, conf.arrearDirection * Convert.ToDouble(this.textBox_accumulative.Text));
            this.initPayReceipt();

            MessageBox.Show("��˳ɹ�!", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);

            openMode = 4;
            this.switchMode(4);

            this.invokeUpdateNotify(conf.finishNotifyType);
        }

        //����
        protected virtual void toolStripButton_finishCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("�Ƿ����ˣ��˻ص�δ���״̬��", "��ʾ", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK)
                return;

            PayReceipt maxPr = PayReceiptDao.getInstance().FindLastestByCustomerID(this.payReceipt.customer_id, false);
            if (maxPr!=null && !maxPr.serial.Equals(this.payReceipt.serial))
            {
                MessageBox.Show(string.Format("����ʧ�ܣ��ڴ˵�֮���������˵ĵ��ݣ���������{0}", maxPr.serial), "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ProductCirculation pc = cirDao.FindLastestByCustomerID(this.payReceipt.customer_id, false);
            if (pc!=null && pc.CirculationTime > payReceipt.bill_time)
            {
                MessageBox.Show(string.Format("����ʧ�ܣ��ڴ˵�֮���������˵ĵ��ݣ���������{0}", pc.Code), "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            double arrear;
            double.TryParse(this.textBox_previousArrears.Text, out arrear);

            CustomerDao.getInstance().update_arrear(this.payReceipt.customer_id, this.conf.arrearDirection * arrear);
            PayReceiptDao.getInstance().UpdateStatus(this.payReceipt.id, 1);

            MessageBox.Show("���˳ɹ�!", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);

            openMode = 1;
            this.switchMode(1);

            this.invokeUpdateNotify(this.conf.finishNotifyType);
        }

        private void toolStripButton_print_Click(object sender, EventArgs e){}

        private DialogResult affirmQuit() {
            return MessageBox.Show(string.Format("{0}������δ���棬�Ƿ�������棿", conf.name), "��ʾ", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
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

            double accumulative = (conf.arrearDirection * arrear - conf.cashDirection * sum + conf.cashDirection * pay) * conf.arrearDirection;
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

        //2017-11-20Ϊ�˷�ֹ�ж�����ڴ򿪣�ͬʱ��˳��ֵ�����
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
    }
}