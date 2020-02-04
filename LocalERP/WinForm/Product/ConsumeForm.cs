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
    public partial class ConsumeForm : MyDockContent
    {
        //open mode       | 0:add 1:edit | 2:approval  | 3:partArrival | 4:arrival
        //status          | 1:apply      | 2:approval  | 3:partArrival | 4:arrival  
        protected int openMode = 0;
        protected int consumeID = 0;

        private Consume consume = null;

        protected bool needSave = false;

        //protected CardDao cirDao;

        public ConsumeForm()
        {
            InitializeComponent();

            openMode = 0;
            consumeID = 0;
        }

        //load �� reload���Ƕ����ṩ�ӿ�
        private void ConsumeForm_Load(object sender, EventArgs e)
        {
            this.lookupText1.LookupForm = FormSingletonFactory.getInstance().getCustomerCIForm_Select();
            this.lookupText2.LookupForm = FormSingletonFactory.getInstance().getCardListForm_select();

            initConsume();
        }

        public void reload(int mode, int id) {
            //��Ϊ���ֻ��һ��card���ڣ��������������card��Ҫ�򿪣���Ҫ�����ԭcard�Ļ�����Ҫ�ж���
            if (needSave && affirmQuit() != DialogResult.OK)
                return;
            
            openMode = mode;
            consumeID = id;
            initConsume();

            this.lookupText1.Focus();
        }

        //�����ṩ����ĺ���������switch mode
        private void initConsume()
        {
            //2020-1-18 ����ֻ���������������0֮�⣬���������Ҫ����card��status������openMode
            if (openMode == 0)
            {

                this.dateTime_consumeTime.Value = DateTime.Now;
                this.textBox_comment.Text = null;
                this.lookupText1.LookupArg = null;
                this.lookupText1.Text_Lookup = null;

                int max = 1;// CirDao.getMaxCode(string.Format("CARD-{0}-", DateTime.Now.ToString("yyyyMMdd")));
                this.textBox_code.Text = string.Format("CARD-{0}-{1:0000}", DateTime.Now.ToString("yyyyMMdd"), max + 1);

                this.textBox_operator.Text = ConfDao.getInstance().Get(5).ToString();
                //this.dataGridView1.Rows.Clear();
                //this.dataGridView2[1, 0].Value = null;
            }
            else {/*
                card = CardDao.getInstance().FindByID(cardID);

                this.textBox_code.Text = card.Code;
                this.dateTime_consumeTime.Value = card.CardTime;
               
                this.textBox_comment.Text = card.Comment;
                this.textBox_operator.Text = card.Oper;
                this.lookupText1.LookupArg = new LookupArg(card.CustomerID, card.CustomerName);
                this.lookupText1.Text_Lookup = card.CustomerName;

                openMode = card.Status;*/
            }
            switchMode(openMode);
            resetNeedSave(false);
        }
        //end init

        private void switchMode(int mode) { 
            //0��������ã���˲����á� 1�����治���ã���˿��á�������״̬���Ի���ת����
            switch(mode){
                case 0:
                    this.label_status.Text = "����";
                    this.initControlsEnable(false, false, false, true);
                    break;
                case 1:
                    //δ��ˣ�����״̬���ڸտ�ʼ�򿪵ı༭״̬���Լ����˺��״̬
                    this.label_status.Text = ProductCirculation.circulationStatusContext[0];
                    this.initControlsEnable(false, true, false, true);
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
                    //���
                    this.label_status.Text = ProductCirculation.circulationStatusContext[3];
                    //this.initControlsEnable(false, false, false, true, true, false, false, false, false, true, false);
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
        protected bool getConsume(out Consume consume)
        {
            consume = new Consume();
            consume.ID = consumeID;

            string code;
            if (ValidateUtility.getName(this.textBox_code, this.errorProvider1, out code) == false)
                return false;
            consume.Code = code;

            /*
            int customerID = -1;
            if (this.lookupText1.Visible == true && ValidateUtility.getLookupValueID(this.lookupText1, this.errorProvider1, out customerID) == false)
                return false;
            */

            int cardID = -1;
            if (this.lookupText2.Visible == true && ValidateUtility.getLookupValueID(this.lookupText1, this.errorProvider1, out cardID) == false)
                return false;

            consume.CardID = cardID;

            consume.ConsumeTime = this.dateTime_consumeTime.Value;
            consume.Comment = this.textBox_comment.Text;
            consume.Oper = this.textBox_operator.Text;

            //consume.CustomerName = this.lookupText1.Text_Lookup;
           
            return true;
        }

        
        /// <summary>
        /// for event
        /// </summary>
        ///
        protected void toolStripButton_save_Click(object sender, EventArgs e)
        {
            bool isSellCorrect = getConsume(out consume);
            if (isSellCorrect == false)
                return;

            //
            if (this.openMode == 1 /*&& CardDao.FindByID(card.ID) == null*/)
            {
                MessageBox.Show("�õ����Ѿ���ɾ���ˡ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //this.Enabled = true;
                return;
            }

            try
            {
                if (openMode == 0)
                {
                    consume.Status = 1;
                    
                    ConsumeDao.getInstance().Insert(consume, out consumeID);
                    MessageBox.Show(string.Format("����{0}�ɹ�!", this.Text), "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (openMode == 1)
                {
                    //cirDao.UpdateBaiscInfo(circulation);
                    //if (recordChanged)
                        //cirDao.updateRecords(circulation.ID, records);
                    MessageBox.Show(string.Format("����{0}�ɹ�!", this.Text), "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                openMode = 1;
                //���¸���circulation��record����ΪID��һ��
                this.initConsume();
            }
            catch (Exception ex)
            {/*
                if (openMode == 0)
                    ProductStainlessCirculationDao.getInstance().DeleteByID(cardID);
                MessageBox.Show("��������,������������λ���Ʒ���Ա��޸Ĺ�,�����±༭!", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);*/
            }

            //so important: if edit ,it should be refresh also, because edit will del exist item and add new item
            //this.invokeUpdateNotify(conf.notifyType);
        }

        //���
        private void toolStripButton_finish_Click(object sender, EventArgs e)
        {
            
        }

        //����
        private void toolStripButton_finishCancel_Click(object sender, EventArgs e)
        {
        }

        private DialogResult affirmQuit() {
            return MessageBox.Show("��Ƭ��δ���棬�Ƿ�������棿", "��ʾ", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
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
            ValidateUtility.getName(this.textBox_code, this.errorProvider1, out temp);
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

        private void lookupText1_valueSetted(object sender, LookupArg arg)
        {
            
        }

    }
}