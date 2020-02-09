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

        private Card card = null;
        //public List<ProductCirculationRecord> records = null;

        protected bool needSave = false;

        public CardForm()
        {
            InitializeComponent();

            openMode = 0;
            cardID = 0;

            this.lookupText1.LookupForm = FormSingletonFactory.getInstance().getCustomerCIForm_Select();
        }

        public void reload(int mode, int id) {
            //因为设计只有一个card窗口，所以如果有其他card需要打开，需要代替掉原card的话，就要判断下
            if (needSave && affirmQuit() != DialogResult.OK)
                return;
            
            openMode = mode;
            cardID = id;
            initCard();

        }

        //对内提供服务的函数，调用switch mode，其实可以把openMode作为参数
        private void initCard()
        {
            //2020-1-18 这里只分两种情况，除了0之外，其他情况还要根据card的status来重设openMode
            if (openMode == 0)
            {
                int max = 1;// CirDao.getMaxCode(string.Format("CARD-{0}-", DateTime.Now.ToString("yyyyMMdd")));
                this.textBox_serial.Text = string.Format("CARD-{0}-{1:0000}", DateTime.Now.ToString("yyyyMMdd"), max + 1);
                this.dateTime_cardTime.Value = DateTime.Now;
                this.textBox_num.Text = "";
                this.textBox_realTotal.Text = "";

                this.textBox_comment.Text = null;
                this.textBox_operator.Text = ConfDao.getInstance().Get(5).ToString();
                this.lookupText1.LookupArg = null;
                this.lookupText1.Text_Lookup = null;

                this.dataGridView1.Rows.Clear();
                
            }
            else {
                card = CardDao.getInstance().FindByID(cardID);

                this.textBox_serial.Text = card.Code;
                this.dateTime_cardTime.Value = card.CardTime;
                this.textBox_realTotal.Text = card.Total.ToString();
                this.textBox_num.Text = card.Number.ToString();
                this.textBox_leftNum.Text = card.LeftNumber.ToString();
                this.textBox_comment.Text = card.Comment;
                this.textBox_operator.Text = card.Oper;
                this.lookupText1.LookupArg = new LookupArg(card.CustomerID, card.CustomerName);
                this.lookupText1.Text_Lookup = card.CustomerName;

                openMode = card.Status;
            }
            switchMode(openMode);
            resetNeedSave(false);
        }
        //end init

        private void switchMode(int mode) { 
            //0：保存可用，审核不可用。 1，保存不可用，审核可用。这两种状态可以互相转换。
            switch(mode){
                case 0:
                    this.label_status.Text = "新增";
                    this.initControlsEnable(false, false, false, true);
                    break;
                case 1:
                    //未审核，这种状态属于刚开始打开的编辑状态，以及弃核后的状态
                    this.label_status.Text = Card.cardStatusContext[0];
                    this.initControlsEnable(false, true, false, true);
                    break;
                case 2:
                    //undefine
                    this.label_status.Text = Card.cardStatusContext[1];
                    //this.initControlsEnable(false, false, true, true, true, false, false, false, true, true, true);
                    break;
                case 3:
                    //审核
                    this.label_status.Text = Card.cardStatusContext[2];
                    this.initControlsEnable(false, false, true, false);
                    break;
                case 4:
                    //消费完
                    this.label_status.Text = Card.cardStatusContext[3];
                    this.initControlsEnable(false, false, true, false);
                    break;
                default:
                    break;
            }
        }

        private void initControlsEnable(bool save, bool finish, bool finishCancel, bool basicInfo)
        {
            this.toolStripButton_save.Enabled = save;
            this.toolStripButton_finish.Enabled = finish;
            this.toolStripButton_finishCancel.Enabled = finishCancel;
            this.panel_basic.Enabled = basicInfo;
        }


        //get and set api
        protected bool getCard(out Card c)
        {
            //先获取status
            int tempStatus = 1;
            if (card != null)
                tempStatus = card.Status;
            
            c = new Card();

            c.Status = tempStatus;

            c.ID = cardID;

            string code;
            if (ValidateUtility.getName(this.textBox_serial, this.errorProvider1, out code) == false)
                return false;
            c.Code = code;

            int customerID = -1;
            if (this.lookupText1.Visible == true && ValidateUtility.getLookupValueID(this.lookupText1, this.errorProvider1, out customerID) == false)
                return false;

            c.CustomerID = customerID;

            c.CardTime = this.dateTime_cardTime.Value;
            c.Comment = this.textBox_comment.Text;
            c.Oper = this.textBox_operator.Text;

           
            c.CustomerName = this.lookupText1.Text_Lookup;

            double total;
            int num, leftNum;
            
            if (ValidateUtility.getDouble(this.textBox_realTotal, this.errorProvider1, true, true, out total)
                && ValidateUtility.getInt(this.textBox_num, this.errorProvider1, true, true, out num)
                && ValidateUtility.getInt(this.textBox_leftNum, this.errorProvider1, true, true, out leftNum))
            {
                c.Total = total;
                c.Number = num;
                c.LeftNumber = leftNum;
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
            bool isSellCorrect = getCard(out card);
            if (isSellCorrect == false)
                return;

            card.Status = 1;
            //
            if (this.openMode == 1 && CardDao.getInstance().FindByID(card.ID) == null)
            {
                MessageBox.Show("该单据已经被删除了。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //this.Enabled = true;
                return;
            }

            try
            {
                if (openMode == 0)
                {   
                    CardDao.getInstance().Insert(card, out cardID);
                    MessageBox.Show(string.Format("增加{0}成功!", this.Text), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    openMode = 1;
                }
                else if (openMode == 1)
                {
                    CardDao.getInstance().Update(card);
                    MessageBox.Show(string.Format("保存{0}成功!", this.Text), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                //主要是保存按钮恢复到初始状态
                this.initCard();
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存有误,可能是往来单位或货品属性被修改过,请重新编辑!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            //so important: if edit ,it should be refresh also, because edit will del exist item and add new item
            this.invokeUpdateNotify(UpdateType.CardUpdate);
        }

        //审核
        private void toolStripButton_finish_Click(object sender, EventArgs e)
        {
            string tips = "是否审核？";
            if (MessageBox.Show(tips, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK)
                return;

            this.getCard(out card);

            //2018-4-20修复的bug
            if (CardDao.getInstance().FindByID(card.ID) == null)
            {
                MessageBox.Show("该卡片已经被删除了。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            card.CardTime = DateTime.Now;
            card.Status = 3;

            CardDao.getInstance().Update(card);

            this.initCard();
            
            MessageBox.Show("审核成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.invokeUpdateNotify(UpdateType.CardUpdate);
        }

        //弃核
        private void toolStripButton_finishCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否弃核，退回到未审核状态？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK)
                return;

            if (ConsumeDao.getInstance().FindList(cardID).Count > 0) {
                MessageBox.Show("弃核失败，该卡片已经消费过，请先删除相应的消费。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            card.Status = 1;
            card.CardTime = DateTime.Now;

            CardDao.getInstance().Update(card);

            MessageBox.Show("弃核成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.initCard();

            this.invokeUpdateNotify(UpdateType.CardUpdate);
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

        private void Textbox_num_TextChanged(object sender, EventArgs e)
        {
            this.textBox_leftNum.Text = this.textBox_num.Text;
        }

    }
}