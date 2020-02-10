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

            this.lookupText1.LookupForm = FormSingletonFactory.getInstance().getCustomerCIForm_Select();
            this.lookupText2.LookupForm = FormSingletonFactory.getInstance().getCardListForm_select();
        }

        public void reload(int mode, int id) {
            //��Ϊ���ֻ��һ��card���ڣ��������������card��Ҫ�򿪣���Ҫ�����ԭcard�Ļ�����Ҫ�ж���
            if (needSave && affirmQuit() != DialogResult.OK)
                return;
            
            openMode = mode;
            consumeID = id;

            initConsume();
        }

        //�����ṩ����ĺ����������edit�����Զ�����openMode��������switch mode
        private void initConsume()
        {
            //2020-1-18 ����ֻ���������������0֮�⣬���������Ҫ����card��status������openMode
            if (openMode == 0)
            {
                int max = 1;// CirDao.getMaxCode(string.Format("CARD-{0}-", DateTime.Now.ToString("yyyyMMdd")));
                this.textBox_code.Text = string.Format("XF-{0}-{1:0000}", DateTime.Now.ToString("yyyyMMdd"), max + 1);

                this.dateTime_consumeTime.Value = DateTime.Now;
                this.textBox_comment.Text = null;
                this.lookupText1.LookupArg = null;
                this.lookupText1.Text_Lookup = null;

                this.textBox_operator.Text = ConfDao.getInstance().Get(5).ToString();
                //this.dataGridView1.Rows.Clear();
                //this.dataGridView2[1, 0].Value = null;
            }
            else {
                consume = ConsumeDao.getInstance().FindByID(consumeID);

                this.textBox_code.Text = consume.Code;
                this.dateTime_consumeTime.Value = consume.ConsumeTime;
               
                this.textBox_comment.Text = consume.Comment;
                this.textBox_operator.Text = consume.Oper;

               
                int leftNumber = 0;
                //�����Ϊ��ˣ���ô��Ҫ��ʱ����
                if(openMode == 1)
                    leftNumber = consume.Card.LeftNumber;
                else
                    leftNumber = consume.LeftNumber; 
                string cardInfo = consume.Card.getInfo(leftNumber);
                
                this.lookupText2.LookupArg = new LookupArg(consume.CardID, cardInfo);
                //��������leftNumber;
                this.lookupText2.LookupArg.ArgName = leftNumber.ToString();
                this.lookupText2.Text_Lookup = cardInfo;
                

                openMode = consume.Status;
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
        protected bool getConsume(out Consume consume)
        {
            consume = new Consume();
            consume.ID = consumeID;

            string code;
            if (ValidateUtility.getName(this.textBox_code, this.errorProvider1, out code) == false)
                return false;
            consume.Code = code;

            int num = 1;
            if (ValidateUtility.getInt(this.textBox_num, this.errorProvider1, true, true, out num) == false)
                return false;
            consume.Number = num;


            int cardID = -1;
            if (this.lookupText2.Visible == true && ValidateUtility.getLookupValueID(this.lookupText2, this.errorProvider1, out cardID) == false)
                return false;

            consume.CardID = cardID;
            int leftNum = 0;
            int.TryParse(this.lookupText2.LookupArg.ArgName, out leftNum);
            consume.LeftNumber = leftNum;

            consume.ConsumeTime = this.dateTime_consumeTime.Value;
            consume.Comment = this.textBox_comment.Text;
            consume.Oper = this.textBox_operator.Text;
           
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

            consume.Status = 1;

            if (this.openMode == 1 && ConsumeDao.getInstance().FindByID(consumeID) == null)
            {
                MessageBox.Show("�������Ѿ���ɾ���ˡ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                if (openMode == 0)
                {   
                    ConsumeDao.getInstance().Insert(consume, out consumeID);
                    MessageBox.Show(string.Format("����{0}�ɹ�!", this.Text), "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    openMode = 1;
                }
                else if (openMode == 1)
                {
                    ConsumeDao.getInstance().Update(consume);
                    MessageBox.Show(string.Format("����{0}�ɹ�!", this.Text), "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                //��Ҫ�Ǳ��水ť�ص�ԭ����״̬
                this.initConsume();

            }
            catch (Exception ex)
            {
                /*if (openMode == 0)
                    ConsumeDao.getInstance().DeleteByID(consumeID);*/
                MessageBox.Show("��������,������������λ��Ƭ���޸Ĺ�,�����±༭!", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            this.invokeUpdateNotify(UpdateType.ConsumeUpdate);
        }

        //���
        private void toolStripButton_finish_Click(object sender, EventArgs e)
        {
            string tips = "��˺󽫴ӿ���۳��������Ƿ������";
            if (MessageBox.Show(tips, "��ʾ", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK)
                return;

            this.getConsume(out consume);
            consume = ConsumeDao.getInstance().FindByID(consumeID);
            //2018-4-20�޸���bug
            if (consume == null)
            {
                MessageBox.Show("�������Ѿ���ɾ���ˡ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Card card = consume.Card;
            if (consume.Number > card.LeftNumber) {
                MessageBox.Show("�����Ѵ�������ʣ�������", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            consume.ConsumeTime = DateTime.Now;
            consume.LeftNumber = card.LeftNumber;
            consume.Status = 4;

            ConsumeDao.getInstance().Update(consume);
            card.LeftNumber = card.LeftNumber - consume.Number;
            if (card.LeftNumber == 0)
                card.Status = 4;

            CardDao.getInstance().Update(card);

            MessageBox.Show("��˳ɹ�!", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            this.initConsume();
            this.invokeUpdateNotify(UpdateType.ConsumeUpdate);
            this.invokeUpdateNotify(UpdateType.CardUpdate);
        }

        //����
        private void toolStripButton_finishCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("�Ƿ����ˣ��˻ص�δ���״̬��", "��ʾ", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK)
                return;

            List<Consume> cons = ConsumeDao.getInstance().FindList(consume.CardID, 0);
            
            if (cons[cons.Count - 1].ID > consume.ID)
            {
                MessageBox.Show("����ʧ�ܣ��ÿ�Ƭ���滹�����ѣ�����ɾ����Ӧ�����ѡ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            consume.Status = 1;
            consume.ConsumeTime = DateTime.Now;
            ConsumeDao.getInstance().Update(consume);

            //ע����������ϲ��������ͨ��dao find������û���⣬�����this.getConsume�Ͳ��У�getConsume�󣬻�������findһ�£�������û����
            Card card = consume.Card;
            if (card.LeftNumber == 0)
                card.Status = 3;
            card.LeftNumber += consume.Number;
            
            CardDao.getInstance().Update(card);

            MessageBox.Show("���˳ɹ�!", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.initConsume();

            this.invokeUpdateNotify(UpdateType.CardUpdate);
            this.invokeUpdateNotify(UpdateType.ConsumeUpdate);
        }

        private DialogResult affirmQuit() {
            return MessageBox.Show("������δ���棬�Ƿ�������棿", "��ʾ", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
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

        private void lookupText2_valueSetted(object sender, LookupArg arg)
        {
            resetNeedSave(true);
        }

    }
}