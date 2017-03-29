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
    public partial class ProductCirculationForm : MyDockContent
    {
        //open mode       | 0:add 1:edit | 2:approval  | 3:partArrival | 4:arrival
        //status          | 1:apply      | 2:approval  | 3:partArrival | 4:arrival  
        protected int openMode = 0;
        protected int circulationID = 0;

        protected CirculationTypeConf conf;

        private ProductCirculation circulation = null;
        public List<ProductCirculationRecord> records = null;

        protected bool needSave = false;
        protected bool recordChanged = false;

        protected ProductCirculationDao cirDao;

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

        public ProductCirculationForm(CirculationTypeConf c, ProductCirculationDao cirDao)
        {
            InitializeComponent();

            openMode = 0;
            circulationID = 0;

            this.conf = c;

            if ((int)conf.type > 4)
                this.toolStripButton_print.Visible = false;    

            this.Text = conf.name + "��";
            this.label_title.Text = this.Text;
            this.label2.Text = conf.business+"ʱ��:";
            this.label_customer.Text = conf.customer;

            this.label_sum.Text = conf.productDirection == 1 ? "����ʵ��Ӧ��:" : "����ʵ��Ӧ��:";
            this.label_thisPayed.Text = conf.productDirection == 1 ? "�����Ѹ�:" : "��������:";
            this.label_arrears.Text =  conf.arrearsDirection==1? "����Ƿ��(Ӧ��):":"����Ƿ��(Ӧ��):";
            this.label1_accumulative.Text = conf.arrearsDirection == 1 ? "�ۼ�Ƿ��(Ӧ��):" : "�ۼ�Ƿ��(Ӧ��):";

            this.cirDao = cirDao;

            initDatagridview(this.dataGridView1);
        }

        private void ProductCirculationForm_Load(object sender, EventArgs e)
        {
            this.lookupText1.LookupForm = FormSingletonFactory.getInstance().getCustomerCIForm_Select();
            
            dataGridView2.Rows.Add("�ܼۺϼ�/Ԫ:", "");
            dataGridView2[0, 0].Style.BackColor = Color.Yellow;
            dataGridView2[0, 0].Style.SelectionBackColor = Color.Yellow;
            dataGridView2[1, 0].Style.BackColor = Color.Yellow;
            this.backgroundWorker.DoWork += new DoWorkEventHandler(backgroundWorker_DoWork);
            this.backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker_RunWorkerCompleted);

            this.textBox_cutoff.TextChanged += new EventHandler(textBox_cutoff_TextChanged);
            this.textBox_realTotal.TextChanged += new EventHandler(textBox_realTotal_TextChanged);
            this.textBox_realTotal.TextChanged += new EventHandler(setAccumulative);

            this.textBox_previousArrears.TextChanged += new EventHandler(setAccumulative);
            this.textBox_thisPayed.TextChanged += new EventHandler(setAccumulative);

            initCirculation();
        }

        public virtual void initDatagridview(DataGridView dgv) { }

        public void reload(int mode, int id) {
            if (needSave && affirmQuit() != DialogResult.OK)
                return;
            
            openMode = mode;
            circulationID = id;
            initCirculation();

            this.lookupText1.Focus();
        }

        /// <summary>
        /// for init circulation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 

        //cutoff��realTotal�ụ��Ӱ�죬�Ӹ����Ʒ�ֹ��ѭ��
        private bool cutoffNeedReCaculate = true;
        private bool realTotalNeedRecaculate = true;

        private void initCirculation()
        {
            if (openMode == 0)
            {
                switchMode(openMode);
                int max = cirDao.getMaxCode(conf.code);
                this.textBox_serial.Text = string.Format("{0}-{1}-{2:0000}", conf.code, DateTime.Now.ToString("yyyyMMdd"), max + 1);
                this.dateTime_sellTime.Value = DateTime.Now;
                this.textBox_comment.Text = null;
                this.lookupText1.LookupArg = null;
                this.lookupText1.Text_Lookup = null;
                this.textBox_operator.Text = null;
                this.dataGridView1.Rows.Clear();
                this.dataGridView2[1, 0].Value = null;

                this.textBox_cutoff.Text = "100";
                this.textBox_realTotal.Text = "";

                this.textBox_previousArrears.Text = null;
                this.textBox_thisPayed.Text = null;
                this.textBox_accumulative.Text = null;
                this.textBox_freight.Text = null;

                this.resetNeedSave(false);
                this.recordChanged = false;
                
                return;
            }

            circulation = cirDao.FindByID(circulationID);

            this.textBox_serial.Text = circulation.Code;
            this.dateTime_sellTime.Value = circulation.CirculationTime;
            this.textBox_comment.Text = circulation.Comment;
            this.textBox_operator.Text = circulation.Oper;

            this.dataGridView2[1, 0].Value = circulation.Total;

            //textbox_cutoff���Զ�����ģ�����textbox_accumulative
            this.textBox_realTotal.Text = circulation.RealTotal.ToString();
            
            //���ʱ��������previousArrears
            this.lookupText1.LookupArg = new LookupArg(circulation.CustomerID, circulation.CustomerName);
            this.lookupText1.Text_Lookup = circulation.CustomerName;

            //���δ��ˣ�Ƿ���п��ܱ�
            if (circulation.Status > 1)
                this.textBox_previousArrears.Text = circulation.PreviousArrears.ToString();

            this.textBox_thisPayed.Text = circulation.ThisPayed.ToString();
            this.textBox_freight.Text = circulation.Freight.ToString();
            
            this.backgroundWorker.RunWorkerAsync(circulation.ID);
            this.invokeBeginLoadNotify();
        }

        //����Ҫʵ��refresh����Ϊ����Activedʱ���Զ����

        protected virtual void setRecord(DataGridViewRow row, ProductCirculationRecord record) { }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            int sellID = (int)e.Argument;
            records = cirDao.getRecordDao().FindList(sellID);
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.dataGridView1.Rows.Clear();
            foreach (ProductCirculationRecord record in records)
            {
                int index = this.dataGridView1.Rows.Add();
                this.setRecord(this.dataGridView1.Rows[index], record);
                //this.setSubTotalPrice(index);
            }

            //this.setTotalPrice();

            if (circulation != null)
                openMode = circulation.Status;
            switchMode(openMode);

            this.resetNeedSave(false);
            this.recordChanged = false;

            this.invokeEndLoadNotify();
        }
        //end init

        private void switchMode(int mode) { 
            switch(mode){
                case 0:
                    this.label_status.Text = "����";
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
                    this.initControlsEnable(false, false, false, true, false, false, false, false, true, false);
                    break;
                default:
                    break;
            }
        }

        protected virtual void initDatagridviewEnable(bool elementReadonly) {
        }

        private void initControlsEnable(bool save, bool approval, bool finish, bool print, bool basicInfo,
            bool add, bool del, bool saveArrival, bool elementReadonly, bool pay)
        {
            //this.toolStripButton_save.Enabled = save;
            this.toolStripButton_approval.Enabled = approval;
            this.toolStripButton_finish.Enabled = finish;
            this.toolStripButton_print.Enabled = print;

            this.panel_basic.Enabled = basicInfo;

            this.button_add.Enabled = add;
            this.button_del.Enabled = del;
            //this.toolStripButton_saveArrival.Enabled = saveArrival;

            initDatagridviewEnable(elementReadonly);

            this.panel_pay.Enabled = pay;
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

            //����������λ
            this.label9.Visible = false;
            this.label_customer.Visible = false;
            this.lookupText1.Visible = false;

            this.dataGridView2.Visible = false;
            this.panel_pay.Visible = false;
        }

        /// <summary>
        /// set datagridview value
        /// </summary>
        /// <param name="rowIndex"></param>
        protected virtual void setSubTotalPrice(int rowIndex)
        {
            DataGridViewRow row = this.dataGridView1.Rows[rowIndex];
            double price;
            int num=0;
            ValidateUtility.getDouble(row.Cells["price"], out price);
            ValidateUtility.getInt(row.Cells["num"], false, true, out num);
            row.Cells["totalPrice"].Value = num * price;
        }

        protected void setTotalPrice()
        {
            double total = 0;
            int number = this.dataGridView1.RowCount;
            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                double totalPrice = 0;
                ValidateUtility.getDouble(row.Cells["totalPrice"], out totalPrice);
                total += totalPrice;
            }

            this.dataGridView2[1, 0].Value = total;

            double cutoff = 100;
            double.TryParse(this.textBox_cutoff.Text, out cutoff);
            double realTotal = total * cutoff / 100;
            this.textBox_realTotal.Text =realTotal.ToString();
        }

        /// <summary>
        /// for get value from controls
        /// </summary>

        protected virtual bool getRecords(out List<ProductCirculationRecord> records) {
            records = null;
            return true;
        }

        protected bool getCirculation(out ProductCirculation circulation)
        {
            circulation = new ProductCirculation();
            circulation.ID = circulationID;
            circulation.Type = (int)conf.type;
            circulation.FlowType = conf.productDirection;
            circulation.ArrearDirection = conf.arrearsDirection;

            string name;
            if (ValidateUtility.getName(this.textBox_serial, this.errorProvider1, out name) == false)
                return false;
            circulation.Code = name;

            int customerID=-1;
            if (this.lookupText1.Visible == true && ValidateUtility.getLookupValueID(this.lookupText1, this.errorProvider1, out customerID) == false)
                return false;

            circulation.CustomerID = customerID;

            circulation.CirculationTime = this.dateTime_sellTime.Value;
            circulation.Comment = this.textBox_comment.Text;
            circulation.Oper = this.textBox_operator.Text;

            if (dataGridView2[1, 0].Value == null || dataGridView2[1, 0].Value.ToString()=="")
                circulation.Total = 0;
            else
                circulation.Total = (double)dataGridView2[1, 0].Value;

            circulation.CustomerName = this.lookupText1.Text_Lookup;

            double total, cutoff, realTotal, previousArrears, thisPayed, freight;

            if (ValidateUtility.getPrice(this.dataGridView2[1, 0], true, out total)
                && ValidateUtility.getDouble(this.textBox_cutoff, this.errorProvider1, false, true, out cutoff)
                && ValidateUtility.getPrice(this.textBox_realTotal, this.errorProvider1, true, true, out realTotal)
                && ValidateUtility.getPrice(this.textBox_previousArrears, this.errorProvider1, false, false, out previousArrears)
                && ValidateUtility.getPrice(this.textBox_thisPayed, this.errorProvider1, false, true, out thisPayed)
                && ValidateUtility.getPrice(this.textBox_freight, this.errorProvider1, false, true, out freight))
            {
                circulation.Total = total;
                circulation.RealTotal = realTotal;
                circulation.PreviousArrears = previousArrears;
                circulation.ThisPayed = thisPayed;
                circulation.Freight = freight;
            }
            else
                return false;

            return true;
        }

        private List<int> getSelectRows()
        {
            List<int> list = new List<int>();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                DataGridViewCheckBoxCell cell = row.Cells["check"] as DataGridViewCheckBoxCell;
                //commented by stone: this is not very reasonable
                if ((bool)cell.EditedFormattedValue == true)
                    list.Add(row.Index);
            }
            return list;
        }

        
        /// <summary>
        /// for event
        /// </summary>
        ///
        protected void toolStripButton_save_Click(object sender, EventArgs e)
        {
            //for datagridview validate
            if (dataGridView1.Rows.Count > 0 && dataGridView1.Columns["totalPrice"].Visible == true)
                dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells["totalPrice"];

            List<ProductCirculationRecord> records;
            bool isRecordsCorrect = getRecords(out records);

            ProductCirculation circulation;
            bool isSellCorrect = getCirculation(out circulation);
            if (isRecordsCorrect == false || isSellCorrect == false)
                return;

            try
            {
                if (openMode == 0)
                {
                    circulation.Status = 1;
                    cirDao.Insert(circulation, records, out circulationID);
                    MessageBox.Show(string.Format("����{0}�ɹ�!", this.Text), "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (openMode == 1)
                {
                    cirDao.UpdateBaiscInfo(circulation);
                    if (recordChanged)
                        cirDao.updateRecords(circulation.ID, records);
                    MessageBox.Show(string.Format("����{0}�ɹ�!", this.Text), "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                openMode = 1;
                //���¸���circulation��record����ΪID��һ��
                this.initCirculation();
            }
            catch (Exception ex)
            {
                if (openMode == 0)
                    ProductStainlessCirculationDao.getInstance().DeleteByID(circulationID);
                MessageBox.Show("��������,������������λ����Ʒ���Ա��޸Ĺ�,�����±༭!", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            //so important: if edit ,it should be refresh also, because edit will del exist item and add new item
            this.invokeUpdateNotify(conf.notifyType);
        }

        protected virtual void updateCostAndProfit(ProductCirculation cir, ProductCirculationRecord record) { 
        }

        //���
        private void toolStripButton_finish_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("��˺󣬽��޸Ŀ���������Ҹõ��ݲ����޸Ļ�ɾ�����Ƿ���ˣ�", "��ʾ", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK)
                return;

            List<ProductCirculationRecord> records;
            this.getRecords(out records);

            ProductCirculation sell;
            this.getCirculation(out sell);
          
            if(conf.productDirection == -1)
                foreach (ProductCirculationRecord record in records)
                {
                    int leftNum = cirDao.getProductDao().FindNumByID(record.ProductID);
                    if (record.TotalNum > leftNum)
                    {
                        MessageBox.Show(string.Format("{0} ��治��,����Ϊ{1},���ʧ��!", record.ProductName, leftNum), "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Enabled = true;
                        return;
                    }
                }

            //����ط���Ҫ������
            foreach (ProductCirculationRecord record in records)
            {
                //���²�Ʒ�ĳɱ��ܼۺ���������������
                this.updateCostAndProfit(sell, record);
            }
            
            cirDao.UpdateStatus(circulationID, 4);
            CustomerDao.getInstance().update_arrear(sell.CustomerID, conf.arrearsDirection * Convert.ToDouble(this.textBox_accumulative.Text));
            
            MessageBox.Show("��˳ɹ�!", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);

            openMode = 4;
            this.switchMode(4);

            this.invokeUpdateNotify(this.conf.finishNotifyType);
            
        }

        private void fill_records(List<ProductCirculationRecord> records)
        {
            // ���� ��ϸ
            foreach (ProductStainlessCirculationRecord record in records)
            {
                Report.DetailGrid.Recordset.Append();

                if (record.QuantityPerPiece > 0 && record.Pieces > 0)
                {
                    cnt_one_piece.AsInteger = record.QuantityPerPiece;
                    pieces.AsInteger = record.Pieces;
                }
                serial.AsString = record.Serial;
                product.AsString = record.ProductName;
                cnt.AsInteger = record.TotalNum;
                unit.AsString = record.Unit;
                price.AsFloat = record.Price;
                sum_price.AsFloat = record.TotalPrice;

                Report.DetailGrid.Recordset.Post();
            }
        }

        private void load_with_customer(ProductCirculation sell, List<ProductCirculationRecord> records)
        {
            // ��ȡ��Ӧ�̵���Ϣ
            Customer customer = CustomerDao.getInstance().FindByID(sell.CustomerID);
            string customer_tel = customer.Tel;
            string customer_addr = customer.Address;
            string contract = "�绰��" + customer_tel;

            DataTable dt = ConfDao.getInstance().GetAll();
            Report.ControlByName("title").AsStaticBox.Text = ConfDao.getInstance().Get(3).ToString() + conf.name + "��";

            Report.ControlByName("info").AsStaticBox.Text = string.Format("��    ַ : {0}\n�����˺� : {1}\n������Ϣ : {2}", dt.Rows[2]["conf"], dt.Rows[7]["conf"], dt.Rows[8]["conf"]);
            Report.ControlByName("contract").AsStaticBox.Text = string.Format("�� ϵ �� : {0}\n�绰���� : {1}\n�ֻ����� : {2}", dt.Rows[4]["conf"], dt.Rows[5]["conf"], dt.Rows[6]["conf"]); 

            // (�û�����Ӧ��)
            Report.ControlByName("customer").AsStaticBox.Text = conf.customer + sell.CustomerName;

            // (����)
            Report.ControlByName("date").AsStaticBox.Text = conf.business +"ʱ��: " + sell.CirculationTime.ToString("yyyy��MM��dd��");

            // ��(����)
            Report.ControlByName("serial").AsStaticBox.Text = "����: NO." + sell.Code;

            // ��ע
            Report.ControlByName("total").AsStaticBox.Text = string.Format("{0}Ԫ", sell.Total);
            Report.ControlByName("realTotal").AsStaticBox.Text = string.Format("{0}Ԫ", sell.RealTotal);

            fill_records(records);
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
            ProductCirculation sell;
            List<ProductCirculationRecord> records;
            this.getCirculation(out sell);
            this.getRecords(out records);

            if (sell.CustomerID == -1)
            {
                load_without_customer(sell, records);
            }
            else
            {
                load_with_customer(sell, records);
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
		}

        private void toolStripButton_print_Click(object sender, EventArgs e)
        {
            // ���뱨��ģ������
            string report_template_path = Application.StartupPath + "..\\..\\..\\grid++\\circulation_report.grf";
            Report.LoadFromFile(report_template_path);
            // ���ӱ����¼�
            Report.Initialize += new _IGridppReportEvents_InitializeEventHandler(ReportInitialize);
            Report.FetchRecord += new _IGridppReportEvents_FetchRecordEventHandler(ReportFetchRecord);
            // ��ӡԤ��
            Report.PrintPreview(true);
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
        //add detail
        private void button_add_Click(object sender, EventArgs e)
        {
            //File.AppendAllText("e:\\debug.txt", string.Format("add click, dataGridView hash code={0}, dataGridView name={1}\r\n", dataGridView1.GetHashCode(), dataGridView1.Name));

            this.dataGridView1.Rows.Add();
            
            DataGridViewRow row = this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1];
            //���ﲻ����,��initDatagridview����Ҳ������
            this.setCellEnable(row.Cells["totalPrice"], false);
            this.setCellEnable(row.Cells["serial"], false);
            
            //setSubTotalPrice(row.Index);
            //setTotalPrice();

            this.resetNeedSave(true);
            this.recordChanged = true;
        }

        //del
        private void button_del_Click(object sender, EventArgs e)
        {
            List<int> rowsIndex = this.getSelectRows();
            if (rowsIndex.Count <= 0) {
                MessageBox.Show("��ѡ����", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            for (int i = rowsIndex.Count - 1; i >= 0; i--)
            {
                this.dataGridView1.Rows.RemoveAt(rowsIndex[i]);
            }
            this.setTotalPrice();

            this.resetNeedSave(true);
            this.recordChanged = true;
        }

        //for event: caculate total price
        protected virtual void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e){}

        protected virtual void productEditingControl_valueSetted(object sender, LookupArg arg) { }

        protected virtual void editingControl_TextChanged(object sender, EventArgs e)
        {
            DataGridViewTextBoxEditingControl control = (sender as DataGridViewTextBoxEditingControl);
            //File.AppendAllText("e:\\debug.txt", string.Format("editingControl text changed, dataGridView hash code={0}, dataGridView name={1}\r\n", control.EditingControlDataGridView.GetHashCode(), control.EditingControlDataGridView.Name));

            DataGridViewCell cell = this.dataGridView1.CurrentCell;
            String columnName = cell.OwningColumn.Name;
            if (columnName == "price")
            {
                setSubTotalPrice(cell.RowIndex);
                setTotalPrice();
            }
            this.resetNeedSave(true);
            this.recordChanged = true;
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

        private void textBox_cutoff_TextChanged(object sender, EventArgs e)
        {
            this.cutoffNeedReCaculate = false;

            if (this.realTotalNeedRecaculate == true)
            {
                double cutoff = 100;
                double.TryParse(this.textBox_cutoff.Text, out cutoff);
                double realTotal = (double)this.dataGridView2[1, 0].Value * cutoff / 100;
                this.textBox_realTotal.Text = realTotal.ToString();
                //�����textBox_realTotal_TextChanged�ǲ����첽��
            }

            this.cutoffNeedReCaculate = true;
        }

        private void textBox_realTotal_TextChanged(object sender, EventArgs e)
        {
            this.realTotalNeedRecaculate = false;
            if (this.cutoffNeedReCaculate == true) {
                double realTotal = 0, total = 0;
                double.TryParse(this.textBox_realTotal.Text, out realTotal);
                ValidateUtility.getDouble(this.dataGridView2[1, 0], out total);
                if(total != 0)
                    this.textBox_cutoff.Text = string.Format("{0}", realTotal / total * 100);
            }
            this.realTotalNeedRecaculate = true;
        }


        private void lookupText1_valueSetted(object sender, LookupArg arg)
        {
            if (arg != null)
            {
                Customer customer = CustomerDao.getInstance().FindByID((int)arg.Value);
                this.textBox_previousArrears.Text = (this.conf.arrearsDirection * customer.arrear).ToString();
            }
            else
                this.textBox_previousArrears.Text = "";

            resetNeedSave(true);
        }

        private void setAccumulative(object sender, EventArgs e)
        {
            double arrear, pay, realTotal;
            double.TryParse(this.textBox_previousArrears.Text, out arrear);
            double.TryParse(this.textBox_thisPayed.Text, out pay);
            double.TryParse(this.textBox_realTotal.Text, out realTotal);
            //             ��ͳһ��ԭʼ��������ʾ           �»���պø������һ��            �����պø����﷽��һ��
            double temp1 = conf.arrearsDirection * arrear + conf.productDirection * realTotal - conf.productDirection * pay;
            temp1 = temp1 * conf.arrearsDirection;
            this.textBox_accumulative.Text = temp1.ToString("0.00");
        }
    }
}