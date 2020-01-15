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

        protected CirculationTypeConf conf;

        private Card card = null;
        public List<ProductCirculationRecord> records = null;

        protected bool needSave = false;
        protected bool recordChanged = false;

        protected ProductCirculationDao cirDao;

        public CardForm(CirculationTypeConf c, ProductCirculationDao cirDao)
        {
            InitializeComponent();

            openMode = 0;
            cardID = 0;

            this.conf = c;

            
            this.cirDao = cirDao;

            initDatagridview(this.dataGridView1);
        }

        private void ProductCirculationForm_Load(object sender, EventArgs e)
        {
            this.lookupText1.LookupForm = FormSingletonFactory.getInstance().getCustomerCIForm_Select();
            initCirculation();
        }

        public virtual void initDatagridview(DataGridView dgv) { }

        public void reload(int mode, int id) {
            if (needSave && affirmQuit() != DialogResult.OK)
                return;
            
            openMode = mode;
            cardID = id;
            initCirculation();

            this.lookupText1.Focus();
        }

        /// <summary>
        /// for init card
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void initCirculation()
        {
            if (openMode == 0)
            {
                switchMode(openMode);
                this.dateTime_cardTime.Value = DateTime.Now;
                this.textBox_comment.Text = null;
                this.lookupText1.LookupArg = null;
                this.lookupText1.Text_Lookup = null;

                int max = cirDao.getMaxCode(string.Format("CARD-{0}-", DateTime.Now.ToString("yyyyMMdd")));
                this.textBox_serial.Text = string.Format("CARD-{0}-{1:0000}", DateTime.Now.ToString("yyyyMMdd"), max + 1);
                
                this.textBox_operator.Text = ConfDao.getInstance().Get(5).ToString();
                this.dataGridView1.Rows.Clear();
                this.dataGridView2[1, 0].Value = null;

                this.resetNeedSave(false);
                return;
            }
        }

        //����Ҫʵ��refresh����Ϊ����Activedʱ���Զ����

        protected virtual void setRecord(DataGridViewRow row, ProductCirculationRecord record) { }

        
        //end init

        private void switchMode(int mode) { 
            //0��������ã���˲����á� 1�����治���ã���˿��á�������״̬���Ի���ת����
            switch(mode){
                case 0:
                    this.label_status.Text = "����";
                    this.initControlsEnable(true, false, false, false, false, true, true, true, false, false,true);
                    break;
                case 1:
                    //δ��ˣ�����״̬���ڸտ�ʼ�򿪵ı༭״̬���Լ����˺��״̬
                    this.label_status.Text = ProductCirculation.circulationStatusContext[0];
                    this.initControlsEnable(false, true, true, false, false, true, true, true, false, false,true);
                    break;
                case 2:
                    //undefine
                    this.label_status.Text = ProductCirculation.circulationStatusContext[1];
                    this.initControlsEnable(false, false, true, true, true, false, false, false, true, true, true);
                    break;
                case 3:
                    //undefine
                    this.label_status.Text = ProductCirculation.circulationStatusContext[2];
                    this.initControlsEnable(false, false, false, true, true, false, false, false, true, true, true);
                    break;
                case 4:
                    //���
                    this.label_status.Text = ProductCirculation.circulationStatusContext[3];
                    this.initControlsEnable(false, false, false, true, true, false, false, false, false, true, false);
                    break;
                default:
                    break;
            }
        }

        protected virtual void initDatagridviewEnable(bool elementReadonly) {
        }

        private void initControlsEnable(bool save, bool approval, bool finish, bool print, bool finishCancel, bool basicInfo,
            bool add, bool del, bool saveArrival, bool elementReadonly, bool pay)
        {
           
        }

        protected void setCellEnable(DataGridViewCell cell, bool enable)
        {
            if (enable == false)
            {
                cell.ReadOnly = true;
                cell.Style.BackColor = System.Drawing.SystemColors.Control;
                cell.Style.SelectionBackColor = System.Drawing.SystemColors.Control;
            }
            else {
                cell.ReadOnly = false;
                cell.Style.BackColor = Color.White;
                cell.Style.SelectionBackColor = Color.White;
            }
        }

        public virtual void hideSomeControls() {

           
        }

        /// <summary>
        /// for get value from controls
        /// </summary>

        protected virtual bool getRecords(out List<ProductCirculationRecord> records) {
            records = null;
            return true;
        }

        protected bool getCard(out Card card)
        {
            card = new Card();
            card.ID = cardID;

            string name;
            if (ValidateUtility.getName(this.textBox_serial, this.errorProvider1, out name) == false)
                return false;
            card.Code = name;

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
            
            if (ValidateUtility.getPrice(this.dataGridView2[1, 0], true, out total)
                && ValidateUtility.getInt(this.textBox1, this.errorProvider1, true, true, out num))
            {
                card.Total = total;
                
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
            if (dataGridView1.Rows.Count <= 0) {
                MessageBox.Show("��ϸ����Ϊ�գ��������ϸ!", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //added 2018-3-30
            this.refreshArrears();

            //for datagridview validate
            if (dataGridView1.Rows.Count > 0 && dataGridView1.Columns["totalPrice"].Visible == true)
                dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells["totalPrice"];

            List<ProductCirculationRecord> records;
            bool isRecordsCorrect = getRecords(out records);

            ProductCirculation circulation;
            bool isSellCorrect = getCard(out card);
            if (isRecordsCorrect == false || isSellCorrect == false)
                return;

            //
            if (this.openMode == 1 && cirDao.FindByID(card.ID) == null)
            {
                MessageBox.Show("�õ����Ѿ���ɾ���ˡ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //this.Enabled = true;
                return;
            }

            try
            {
                if (openMode == 0)
                {
                    card.Status = 1;
                    //cirDao.Insert(card, records, out cardID);
                    MessageBox.Show(string.Format("����{0}�ɹ�!", this.Text), "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (openMode == 1)
                {
                    //cirDao.UpdateBaiscInfo(circulation);
                    if (recordChanged)
                        //cirDao.updateRecords(circulation.ID, records);
                    MessageBox.Show(string.Format("����{0}�ɹ�!", this.Text), "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                openMode = 1;
                //���¸���circulation��record����ΪID��һ��
                this.initCirculation();
            }
            catch (Exception ex)
            {
                if (openMode == 0)
                    ProductStainlessCirculationDao.getInstance().DeleteByID(cardID);
                MessageBox.Show("��������,������������λ���Ʒ���Ա��޸Ĺ�,�����±༭!", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            //so important: if edit ,it should be refresh also, because edit will del exist item and add new item
            this.invokeUpdateNotify(conf.notifyType);
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

        private void Controls_TextChanged(object sender, EventArgs e)
        {
            resetNeedSave(true);
        }

       

        //2017-11-20Ϊ�˷�ֹ�ж�����ڴ򿪣�ͬʱ��˳��ֵ�����
        private void refreshArrears()
        {
            
        }

        private void lookupText1_valueSetted(object sender, LookupArg arg)
        {
            
        }

        private void setLastPayReceipt(int customerId) {
            
        }

        /// <summary>
        /// ��ӡ����
        /// </summary>
        /// <param name="records"></param>
        /// 

        //����Grid++Report����������
        private GridppReport Report = new GridppReport();
        // ��ϸ�������
        private IGRField serial;
        private IGRField product;
        private IGRField cnt_one_piece;
        private IGRField pieces;
        private IGRField unit;
        private IGRField cnt;
        private IGRField price;
        private IGRField sum_price;
        private IGRField comment;

        private void fill_records(List<ProductCirculationRecord> records)
        {
            // ���� ��ϸ
            foreach (ProductStainlessCirculationRecord record in records)
            {
                Report.DetailGrid.Recordset.Append();

                if (cnt_one_piece != null && record.QuantityPerPiece > 0)
                {
                    cnt_one_piece.AsFloat = record.QuantityPerPiece;
                }

                if (pieces != null && record.Pieces > 0)
                {
                    pieces.AsInteger = record.Pieces;
                }

                serial.AsString = record.Serial;
                product.AsString = record.ProductName;
                if (cnt != null)
                    cnt.AsFloat = record.TotalNum;
                unit.AsString = record.Unit;
                price.AsFloat = record.Price;
                sum_price.AsFloat = record.TotalPrice;
                if (comment != null)
                    comment.AsString = record.Comment;

                Report.DetailGrid.Recordset.Post();
            }
        }

        private void load_with_customer(ProductCirculation sell, List<ProductCirculationRecord> records)
        {
            
        }

        private void load_without_customer(ProductCirculation sell, List<ProductCirculationRecord> records)
        {
            // (����)
            Report.ControlByName("date").AsStaticBox.Text = sell.CirculationTime.ToString("yyyy��MM��dd��");

            // ��(����)
            Report.ControlByName("serial").AsStaticBox.Text = "NO." + sell.Code;

            // ��ע
            Report.ControlByName("comment_text").AsStaticBox.Text = sell.Comment;

            // ���� ��ϸ
            fill_records(records);
        }

        //��C#��һ������һ����¼���ܳɹ���ֻ��ʹ��һ�ν���¼ȫ�������ķ�ʽ
        private void ReportFetchRecord()
        {
            Card sell;
            List<ProductCirculationRecord> records;
            this.getCard(out sell);
            this.getRecords(out records);

            if (sell.CustomerID == -1)
            {
                //load_without_customer(sell, records);
            }
            else
            {
                //load_with_customer(sell, records);
            }
        }

        private void ReportInitialize()
        {
            //�ڴ˼�¼��ÿ���ֶεĽӿ�ָ��
            serial = Report.FieldByName("serial");
            product = Report.FieldByName("product");
            cnt_one_piece = Report.FieldByName("cnt_one_piece");
            pieces = Report.FieldByName("pieces");
            unit = Report.FieldByName("unit");
            cnt = Report.FieldByName("cnt");
            price = Report.FieldByName("price");
            sum_price = Report.FieldByName("sum_price");
            comment = Report.FieldByName("comment");
        }

        private void toolStripButton_print_Click(object sender, EventArgs e)
        {
            // ���뱨��ģ������
            string report_template_path = ConfUtility.cir_report_path;
            Report.LoadFromFile(report_template_path);
            // ���ӱ����¼�
            Report.Initialize -= new _IGridppReportEvents_InitializeEventHandler(ReportInitialize);
            Report.Initialize += new _IGridppReportEvents_InitializeEventHandler(ReportInitialize);
            //һ��Ҫ��-=��Ҫ�����ظ�����
            Report.FetchRecord -= new _IGridppReportEvents_FetchRecordEventHandler(ReportFetchRecord);
            Report.FetchRecord += new _IGridppReportEvents_FetchRecordEventHandler(ReportFetchRecord);
            // ��ӡԤ��
            Report.PrintPreview(true);
        }


        /// <summary>
        /// ��ӡ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton_printLetter_Click(object sender, EventArgs e)
        {
           
        }


    }
}