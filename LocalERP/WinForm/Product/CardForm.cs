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
    public partial class CardForm : MyDockContent
    {
        //open mode       | 0:add 1:edit | 2:approval  | 3:partArrival | 4:arrival
        //status          | 1:apply      | 2:approval  | 3:partArrival | 4:arrival  
        protected int openMode = 0;
        protected int cardID = 0;

        //protected CirculationTypeConf conf;

        private Card card = null;
        //public List<ProductCirculationRecord> records = null;

        protected bool needSave = false;

        //protected CardDao cirDao;

        public CardForm()
        {
            InitializeComponent();

            openMode = 0;
            cardID = 0;

            
            //this.cirDao = cirDao;

            //initDatagridview(this.dataGridView1);
        }

        private void ProductCirculationForm_Load(object sender, EventArgs e)
        {
            this.lookupText1.LookupForm = FormSingletonFactory.getInstance().getCustomerCIForm_Select();
            initCard();
        }

        public void reload(int mode, int id) {
            if (needSave && affirmQuit() != DialogResult.OK)
                return;
            
            openMode = mode;
            cardID = id;
            initCard();

            this.lookupText1.Focus();
        }

        /// <summary>
        /// for init card
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void initCard()
        {
            if (openMode == 0)
            {
                switchMode(openMode);
                this.dateTime_cardTime.Value = DateTime.Now;
                this.textBox_comment.Text = null;
                this.lookupText1.LookupArg = null;
                this.lookupText1.Text_Lookup = null;

                int max = 1;// CirDao.getMaxCode(string.Format("CARD-{0}-", DateTime.Now.ToString("yyyyMMdd")));
                this.textBox_serial.Text = string.Format("CARD-{0}-{1:0000}", DateTime.Now.ToString("yyyyMMdd"), max + 1);
                
                this.textBox_operator.Text = ConfDao.getInstance().Get(5).ToString();
                this.dataGridView1.Rows.Clear();
                //this.dataGridView2[1, 0].Value = null;

                //this.resetNeedSave(false);
                return;
            }
        }
        //end init

        private void switchMode(int mode) { 
            //0：保存可用，审核不可用。 1，保存不可用，审核可用。这两种状态可以互相转换。
            switch(mode){
                case 0:
                    this.label_status.Text = "新增";
                    this.initControlsEnable(true, false, false, true);
                    break;
                case 1:
                    //未审核，这种状态属于刚开始打开的编辑状态，以及弃核后的状态
                    this.label_status.Text = ProductCirculation.circulationStatusContext[0];
                    //this.initControlsEnable(false, true, true, false, false, true, true, true, false, false,true);
                    break;
                case 2:
                    //undefine
                    this.label_status.Text = ProductCirculation.circulationStatusContext[1];
                    //this.initControlsEnable(false, false, true, true, true, false, false, false, true, true, true);
                    break;
                case 3:
                    //undefine
                    this.label_status.Text = ProductCirculation.circulationStatusContext[2];
                    //this.initControlsEnable(false, false, false, true, true, false, false, false, true, true, true);
                    break;
                case 4:
                    //审核
                    this.label_status.Text = ProductCirculation.circulationStatusContext[3];
                    //this.initControlsEnable(false, false, false, true, true, false, false, false, false, true, false);
                    break;
                default:
                    break;
            }
        }

        protected virtual void initDatagridviewEnable(bool elementReadonly) {
        }

        private void initControlsEnable(bool save, bool finish, bool finishCancel, bool basicInfo)
        {
            this.toolStripButton_save.Enabled = save;
            this.toolStripButton_finish.Enabled = finish;
            this.toolStripButton_finishCancel.Enabled = finishCancel;
            this.panel_basic.Enabled = basicInfo;
        }

        protected bool getCard(out Card card)
        {
            card = new Card();
            card.ID = cardID;

            string code;
            if (ValidateUtility.getName(this.textBox_serial, this.errorProvider1, out code) == false)
                return false;
            card.Code = code;

            int customerID = -1;
            if (this.lookupText1.Visible == true && ValidateUtility.getLookupValueID(this.lookupText1, this.errorProvider1, out customerID) == false)
                return false;

            card.CustomerID = customerID;

            card.CardTime = this.dateTime_cardTime.Value;
            card.Comment = this.textBox_comment.Text;
            card.Oper = this.textBox_operator.Text;

           
            card.CustomerName = this.lookupText1.Text_Lookup;

            double total;
            int num;
            
            if (ValidateUtility.getDouble(this.textBox_realTotal, this.errorProvider1, true, true, out total)
                && ValidateUtility.getInt(this.textBox_num, this.errorProvider1, true, true, out num))
            {
                card.Total = total;
                card.Number = num;
                
            }
            else
                return false;
            return true;
        }

        
        /// <summary>
        /// for event
        /// </summary>
        ///
        protected void toolStripButton_save_Click(object sender, EventArgs e)
        {

            Card circulation;
            bool isSellCorrect = getCard(out card);
            if (isSellCorrect == false)
                return;

            //
            if (this.openMode == 1 /*&& CardDao.FindByID(card.ID) == null*/)
            {
                MessageBox.Show("该单据已经被删除了。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //this.Enabled = true;
                return;
            }

            try
            {
                if (openMode == 0)
                {
                    card.Status = 1;
                    
                    CardDao.getInstance().Insert(card, out cardID);
                    MessageBox.Show(string.Format("增加{0}成功!", this.Text), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (openMode == 1)
                {
                    //cirDao.UpdateBaiscInfo(circulation);
                    //if (recordChanged)
                        //cirDao.updateRecords(circulation.ID, records);
                    MessageBox.Show(string.Format("保存{0}成功!", this.Text), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                openMode = 1;
                //重新更新circulation和record，因为ID不一样
                this.initCard();
            }
            catch (Exception ex)
            {
                if (openMode == 0)
                    ProductStainlessCirculationDao.getInstance().DeleteByID(cardID);
                MessageBox.Show("保存有误,可能是往来单位或货品属性被修改过,请重新编辑!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            //so important: if edit ,it should be refresh also, because edit will del exist item and add new item
            //this.invokeUpdateNotify(conf.notifyType);
        }

        //审核
        private void toolStripButton_finish_Click(object sender, EventArgs e)
        {
            
        }

        //弃核
        private void toolStripButton_finishCancel_Click(object sender, EventArgs e)
        {
        }

        private DialogResult affirmQuit() {
            return MessageBox.Show("卡片尚未保存，是否放弃保存？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
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

        private void Controls_TextChanged(object sender, EventArgs e)
        {
            resetNeedSave(true);
        }

       

        //2017-11-20为了防止有多个窗口打开，同时审核出现的问题
        private void refreshArrears()
        {
            
        }

        private void lookupText1_valueSetted(object sender, LookupArg arg)
        {
            
        }

        private void setLastPayReceipt(int customerId) {
            
        }

    }
}