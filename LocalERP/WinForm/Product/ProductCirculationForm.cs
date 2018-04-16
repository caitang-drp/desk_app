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

        public ProductCirculationForm(CirculationTypeConf c, ProductCirculationDao cirDao)
        {
            InitializeComponent();

            openMode = 0;
            circulationID = 0;

            this.conf = c;

            //有些控件的显示控制，下面还有一个virtual的hideSomeControls
            //除了采购销售，要隐藏一些东西
            if ((int)conf.type > 4)
            {
                this.toolStripButton_print.Visible = false;
                this.toolStripButton_printLetter.Visible = false;
            }

            int close = 0;
            if (ConfUtility.GetBackFreightOpen() == "backFreightClose") {
                this.panel_payBackFreight.Visible = false;
                close++;
            }

            if (ConfUtility.GetLastPayReceiptOpen() == "lastPayReceiptClose") {
                this.panel_lastPayReceipt.Visible = false;
                close++;
            }

            if(close == 2)
                this.panel_payBasic.Location = new Point(this.panel_payBasic.Location.X, this.panel_payBasic.Location.Y - 20);

            if (ConfUtility.GetPrintLetterOpen() == "printLetterClose")
                this.toolStripButton_printLetter.Visible = false;

            this.Text = conf.name + "单";
            this.label_title.Text = this.Text;
            //this.label2.Text = conf.business+"时间:";
            this.label2.Text = "开单时间:";
            this.label_customer.Text = conf.customer;

            this.label_sum.Text = conf.productDirection == 1 ? "本单实计应付:" : "本单实计应收:";
            this.label_thisPayed.Text = conf.productDirection == 1 ? LabelUtility.THIS_PAY : LabelUtility.THIS_RECEIPT;
            this.label_arrears.Text =  conf.arrearsDirection==1? "以上欠款(应付):":"以上欠款(应收):";
            this.label1_accumulative.Text = conf.arrearsDirection == 1 ? "累计欠款(应付):" : "累计欠款(应收):";

            this.cirDao = cirDao;

            initDatagridview(this.dataGridView1);
        }

        private void ProductCirculationForm_Load(object sender, EventArgs e)
        {
            this.lookupText1.LookupForm = FormSingletonFactory.getInstance().getCustomerCIForm_Select();
            
            dataGridView2.Rows.Add("总价合计/元:", "", "累计件数", "");
            dataGridView2[0, 0].Style.BackColor = Color.Yellow;
            dataGridView2[0, 0].Style.SelectionBackColor = Color.Yellow;
            dataGridView2[1, 0].Style.BackColor = Color.Yellow;
            this.backgroundWorker.DoWork += new DoWorkEventHandler(backgroundWorker_DoWork);
            this.backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker_RunWorkerCompleted);

            this.textBox_cutoff.TextChanged += new EventHandler(textBox_cutoff_TextChanged);
            this.textBox_backFreightPerPiece.TextChanged += new EventHandler(textBox_backFreightPerPiece_TextChanged);
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

        //cutoff和realTotal会互相影响，加个控制防止死循环
        private bool cutoffNeedReCaculate = true;
        private bool realTotalNeedRecaculate = true;

        private void initCirculation()
        {
            if (openMode == 0)
            {
                switchMode(openMode);
                if(ConfUtility.GetSerialType() == "serialType1" || (int)conf.type > 4){
                    int max = cirDao.getMaxCode(string.Format("{0}-{1}-", conf.code, DateTime.Now.ToString("yyyyMMdd")));
                    this.textBox_serial.Text = string.Format("{0}-{1}-{2:0000}", conf.code, DateTime.Now.ToString("yyyyMMdd"), max + 1);
                }
                this.dateTime_sellTime.Value = DateTime.Now;
                this.textBox_comment.Text = null;
                this.lookupText1.LookupArg = null;
                this.lookupText1.Text_Lookup = null;
                this.textBox_operator.Text = ConfDao.getInstance().Get(5).ToString();
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
            
            this.backgroundWorker.RunWorkerAsync(circulation.ID);
            this.invokeBeginLoadNotify();
        }

        //不需要实现refresh，因为窗口Actived时会自动检测

        protected virtual void setRecord(DataGridViewRow row, ProductCirculationRecord record) { }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            int sellID = (int)e.Argument;
            records = cirDao.getRecordDao().FindList(sellID);
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.dataGridView1.Rows.Clear();
            int totalPieces = 0;

            foreach (ProductCirculationRecord record in records)
            {
                int index = this.dataGridView1.Rows.Add();
                this.setRecord(this.dataGridView1.Rows[index], record);
                totalPieces += record.Pieces;
            }

            //不是很合理!!!!
            this.label_totalPieces.Text = totalPieces == 0 ? "" : totalPieces.ToString();
            this.realTotalNeedRecaculate = false;
            this.textBox_backFreightPerPiece.Text = circulation.BackFreightPerPiece.ToString();
            this.realTotalNeedRecaculate = true;
            this.label_totalBackFreight.Text = (circulation.BackFreightPerPiece * totalPieces).ToString();

            //控件previousArrears, thisPayed, realTotal的变化，引起accumulative的自动计算
            //这几个值，只有accumulative是自动计算的

            ///******从initCirculation那里搬过来
            //textbox_cutoff是自动计算的，类似textbox_accumulative
            this.textBox_realTotal.Text = circulation.RealTotal.ToString();

            //这个时候有重置previousArrears
            this.lookupText1.LookupArg = new LookupArg(circulation.CustomerID, circulation.CustomerName);
            this.lookupText1.Text_Lookup = circulation.CustomerName;

            //serial previousArrears lastPayReceipt跟customer的变化相关
            //不合理，因为lookupText1改变会导致这个值改变
            this.textBox_serial.Text = circulation.Code;
            //如果未审核，欠款有可能变，所以以重置后的值为准，不用数据库里的值
            //如果已审核，使用数据库里的值
            if (circulation.Status > 1){
                this.textBox_previousArrears.Text = circulation.PreviousArrears.ToString();
                this.label_lastPayReceipt.Text = circulation.LastPayReceipt;
            }

            this.textBox_thisPayed.Text = circulation.ThisPayed.ToString();
            this.textBox_freight.Text = circulation.Freight.ToString();
            ///搬过来

            if (circulation != null)
                openMode = circulation.Status;
            switchMode(openMode);

            this.resetNeedSave(false);
            this.recordChanged = false;

            this.invokeEndLoadNotify();
        }
        //end init

        private void switchMode(int mode) { 
            //0：保存可用，审核不可用。 1，保存不可用，审核可用。这两种状态可以互相转换。
            switch(mode){
                case 0:
                    this.label_status.Text = "新增";
                    this.initControlsEnable(true, false, false, false, false, true, true, true, false, false,true);
                    break;
                case 1:
                    //未审核，这种状态属于刚开始打开的编辑状态，以及弃核后的状态
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
                    //审核
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
            //原来save这一行被取消了
            this.toolStripButton_save.Enabled = save;
            
            this.toolStripButton_approval.Enabled = approval;
            this.toolStripButton_finish.Enabled = finish;
            this.toolStripButton_print.Enabled = print;
            this.toolStripButton_printLetter.Enabled = print;

            this.toolStripButton_finishCancel.Enabled = finishCancel;

            this.panel_basic.Enabled = basicInfo;

            this.button_add.Enabled = add;
            this.button_del.Enabled = del;

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

            //隐藏来往单位
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
            double price, num;
            ValidateUtility.getDouble(row.Cells["price"], out price);
            ValidateUtility.getDouble(row.Cells["num"], false, true, out num);
            row.Cells["totalPrice"].Value = num * price;
        }

        //总价、折扣、退运费和实价的关系，有三种情况：
        //1，总价有变化，联合折扣、退运费计算出实价
        //2，折扣或退运费有变化，计算出实价
        //3，实价有变化，反推出折扣
        //1和2其实属于一种情况， 可以理解为正方向的计算，3可以理解为反方向的计算

        //这是明细改动时，更新总价，也是第1种情况
        protected void setTotalPrice()
        {
            double total = 0;
            
            double totalPieces = 0;
            
            int number = this.dataGridView1.RowCount;
            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                double totalPrice = 0;
                ValidateUtility.getDouble(row.Cells["totalPrice"], out totalPrice);
                total += totalPrice;

                double pieces = 0;
                ValidateUtility.getDouble(row.Cells["pieces"], out pieces);
                totalPieces += pieces;
            }

            this.dataGridView2[1, 0].Value = total;
            this.label_totalPieces.Text = totalPieces == 0 ? "" : totalPieces.ToString();
            /*
            double cutoff = 100;
            double.TryParse(this.textBox_cutoff.Text, out cutoff);

            double backFreightPerPiece = 0;
            double.TryParse(this.textBox_backFreightPerPiece.Text, out backFreightPerPiece);*/

            //added on 2018-3-16，这个是明细修改引起的总价重新计算
            //不需要通过realTotal引发重新计算cutoff，也不需要通过backFreight引发重新计算realTotal
            this.cutoffNeedReCaculate = false;
            //this.realTotalNeedRecaculate = false;

            /*this.label_totalBackFreight.Text = backFreightPerPiece * totalPieces;
            double realTotal = total * cutoff / 100 - backFreightPerPiece * totalPieces;
            this.textBox_realTotal.Text =realTotal.ToString();*/
            this.calTotalBackAndRealTotal();

            //added on 2018-3-16
            this.cutoffNeedReCaculate = true;
            //this.realTotalNeedRecaculate = true;
        }

        //通过total，cutoff，backFreight计算realTotal
        private void calTotalBackAndRealTotal(){
            double total = 0;
            ValidateUtility.getPrice(this.dataGridView2[1, 0], false, out total);

            double totalPieces = 0;
            double.TryParse(this.label_totalPieces.Text, out totalPieces);

            double cutoff = 100;
            double.TryParse(this.textBox_cutoff.Text, out cutoff);

            double backFreightPerPiece = 0;
            double.TryParse(this.textBox_backFreightPerPiece.Text, out backFreightPerPiece);

            this.label_totalBackFreight.Text = (totalPieces * backFreightPerPiece).ToString();

            double realTotal = total * cutoff / 100 - backFreightPerPiece * totalPieces;
            this.textBox_realTotal.Text = realTotal.ToString();
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
            circulation.LastPayReceipt = this.label_lastPayReceipt.Text;

            if (dataGridView2[1, 0].Value == null || dataGridView2[1, 0].Value.ToString()=="")
                circulation.Total = 0;
            else
                circulation.Total = (double)dataGridView2[1, 0].Value;

            circulation.CustomerName = this.lookupText1.Text_Lookup;

            double total, backFreightPerPiece, cutoff, realTotal, previousArrears, thisPayed, freight;

            if (ValidateUtility.getPrice(this.dataGridView2[1, 0], true, out total)
                && ValidateUtility.getDouble(this.textBox_cutoff, this.errorProvider1, false, true, out cutoff)
                && ValidateUtility.getPrice(this.textBox_backFreightPerPiece, this.errorProvider1, false, true, out backFreightPerPiece)
                && ValidateUtility.getPrice(this.textBox_realTotal, this.errorProvider1, true, true, out realTotal)
                && ValidateUtility.getPrice(this.textBox_previousArrears, this.errorProvider1, false, false, out previousArrears)
                && ValidateUtility.getPrice(this.textBox_thisPayed, this.errorProvider1, false, true, out thisPayed)
                && ValidateUtility.getPrice(this.textBox_freight, this.errorProvider1, false, true, out freight))
            {
                circulation.Total = total;
                circulation.BackFreightPerPiece = backFreightPerPiece;
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
            if (dataGridView1.Rows.Count <= 0) {
                MessageBox.Show("明细不能为空，请添加明细!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            bool isSellCorrect = getCirculation(out circulation);
            if (isRecordsCorrect == false || isSellCorrect == false)
                return;

            try
            {
                if (openMode == 0)
                {
                    circulation.Status = 1;
                    cirDao.Insert(circulation, records, out circulationID);
                    MessageBox.Show(string.Format("增加{0}成功!", this.Text), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (openMode == 1)
                {
                    cirDao.UpdateBaiscInfo(circulation);
                    if (recordChanged)
                        cirDao.updateRecords(circulation.ID, records);
                    MessageBox.Show(string.Format("保存{0}成功!", this.Text), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                openMode = 1;
                //重新更新circulation和record，因为ID不一样
                this.initCirculation();
            }
            catch (Exception ex)
            {
                if (openMode == 0)
                    ProductStainlessCirculationDao.getInstance().DeleteByID(circulationID);
                MessageBox.Show("保存有误,可能是往来单位或货品属性被修改过,请重新编辑!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            //so important: if edit ,it should be refresh also, because edit will del exist item and add new item
            this.invokeUpdateNotify(conf.notifyType);
        }

        protected virtual void updateCostAndProfit(ProductCirculation cir, ProductCirculationRecord record) { 
        }

        protected virtual void cancelUpdateCostAndProfit(ProductCirculation cir, ProductCirculationRecord record)
        {
        }

        //审核
        private void toolStripButton_finish_Click(object sender, EventArgs e)
        {
            string tips = "审核后，将修改库存数量和债务信息，是否审核？";
            if((int)conf.type > 4)
                tips = "审核后，将修改库存数量，是否审核？";
            if (MessageBox.Show(tips, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK)
                return;

            //2017-11-20防止多个窗口打开，并同时审核
            this.refreshArrears();

            //2018-3-29：其实审核的时候不需要重新获取 cir 和 record，考虑把这几行去掉？不行！！！因为债务有可能更新
            List<ProductCirculationRecord> records;
            this.getRecords(out records);

            ProductCirculation sell;
            this.getCirculation(out sell);

            //判断是否支持负库存
            string negative = ConfDao.getInstance().Get(20);
            if((string.IsNullOrEmpty(negative) || negative != "1") && conf.productDirection == -1)
                foreach (ProductCirculationRecord record in records)
                {
                    int leftNum = cirDao.getProductDao().FindNumByID(record.ProductID);
                    if (record.TotalNum > leftNum)
                    {
                        MessageBox.Show(string.Format("{0} 库存不足,数量为{1},审核失败!", record.ProductName, leftNum), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Enabled = true;
                        return;
                    }
                }

            //2017-11-20，相应的信息也要更新
            sell.CirculationTime = DateTime.Now;
            this.dateTime_sellTime.Value = sell.CirculationTime;
            //重置了dateTime控件后，neeSave会变为true，但是由于是系统自动更新时间，所以不需要用户保存！耦合度太高了
            this.needSave = false;
            cirDao.UpdateBaiscInfo(sell);

            //这个地方需要事务处理
            foreach (ProductCirculationRecord record in records)
            {
                //更新产品的成本总价和数量，新增利润
                this.updateCostAndProfit(sell, record);
            }
            
            cirDao.UpdateStatus(circulationID, 4);
            CustomerDao.getInstance().update_arrear(sell.CustomerID, conf.arrearsDirection * Convert.ToDouble(this.textBox_accumulative.Text));
            
            //2018-3-30
            this.initCirculation();

            MessageBox.Show("审核成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            openMode = 4;
            this.switchMode(4);

            this.invokeUpdateNotify(this.conf.finishNotifyType);
            
        }

        //弃核
        private void toolStripButton_finishCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否弃核，退回到未审核状态？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK)
                return;

            //判断是否支持负库存
            string negative = ConfDao.getInstance().Get(20);
            if ((string.IsNullOrEmpty(negative) || negative != "1") && conf.productDirection == 1)
                foreach (ProductCirculationRecord record in records)
                {
                    int leftNum = cirDao.getProductDao().FindNumByID(record.ProductID);
                    if (record.TotalNum > leftNum)
                    {
                        MessageBox.Show(string.Format("{0} 库存不足,数量为{1},弃核失败!", record.ProductName, leftNum), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Enabled = true;
                        return;
                    }
                }

            //circulation不用重新get，因为考虑到审核过的有重新init，而且其值不能改变
            ProductCirculation maxCir = cirDao.FindLastestByCustomerID(this.circulation.CustomerID, false);
            if (maxCir != null && !maxCir.Code.Equals(circulation.Code))
            {
                MessageBox.Show(string.Format("弃核失败，在此单之后存在已审核的单据，请先弃核{0}", maxCir.Code), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            PayReceipt payReceipt = PayReceiptDao.getInstance().FindLastestByCustomerID(circulation.CustomerID, false);
            if (payReceipt !=null && circulation.CirculationTime < payReceipt.bill_time) {
                MessageBox.Show(string.Format("弃核失败，在此单之后存在已审核的单据，请先弃核{0}", payReceipt.serial), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            foreach (ProductCirculationRecord record in records)
                //更新产品数量
                this.cancelUpdateCostAndProfit(circulation, record);

            double arrear;
            double.TryParse(this.textBox_previousArrears.Text, out arrear);

            CustomerDao.getInstance().update_arrear(circulation.CustomerID, conf.arrearsDirection * arrear);
            cirDao.UpdateStatus(circulationID, 1);

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
        //add detail
        private void button_add_Click(object sender, EventArgs e)
        {
            //File.AppendAllText("e:\\debug.txt", string.Format("add click, dataGridView hash code={0}, dataGridView name={1}\r\n", dataGridView1.GetHashCode(), dataGridView1.Name));

            this.dataGridView1.Rows.Add();
            
            DataGridViewRow row = this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1];
            //这里不合理,在initDatagridview那里也有设置
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
                MessageBox.Show("请选择项", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                /*
                double cutoff = 100;
                double.TryParse(this.textBox_cutoff.Text, out cutoff);
                double realTotal = (double)this.dataGridView2[1, 0].Value * cutoff / 100;
                this.textBox_realTotal.Text = realTotal.ToString();
                //这里的textBox_realTotal_TextChanged是不是异步？*/
                this.calTotalBackAndRealTotal();
            }

            this.cutoffNeedReCaculate = true;
        }

        //
        void textBox_backFreightPerPiece_TextChanged(object sender, EventArgs e)
        {
            //主要是初始化backFreight时，不需要重新计算realTotal
            if (this.realTotalNeedRecaculate == true)
            {
                this.cutoffNeedReCaculate = false;
                this.calTotalBackAndRealTotal();
                this.cutoffNeedReCaculate = true;
            }
        }

        //这个函数是更新下cutoff
        private void textBox_realTotal_TextChanged(object sender, EventArgs e)
        {
            this.realTotalNeedRecaculate = false;
            if (this.cutoffNeedReCaculate == true) {
                double realTotal = 0, total = 0, totalBackFreight = 0;
                double.TryParse(this.textBox_realTotal.Text, out realTotal);
                double.TryParse(this.label_totalBackFreight.Text, out totalBackFreight);

                ValidateUtility.getDouble(this.dataGridView2[1, 0], out total);
                if(total != 0)
                    this.textBox_cutoff.Text = string.Format("{0}", (realTotal+totalBackFreight) / total * 100);
            }
            this.realTotalNeedRecaculate = true;
        }

        //2017-11-20为了防止有多个窗口打开，同时审核出现的问题
        private void refreshArrears()
        {
            if (!string.IsNullOrEmpty(this.lookupText1.Text_Lookup))
            {
                int cID = 0;
                int.TryParse(this.lookupText1.LookupArg.Value.ToString(), out cID);
                Customer customer = CustomerDao.getInstance().FindByID(cID);
                //以上欠款就通过arrearDirection（应收应付）来表示，本单应收应付跟货物的direction相关
                this.textBox_previousArrears.Text = (this.conf.arrearsDirection * customer.arrear).ToString();

                setLastPayReceipt(customer.ID);
            }
            else
                this.textBox_previousArrears.Text = "";
        }

        private void lookupText1_valueSetted(object sender, LookupArg arg)
        {
            //转移控件焦点，使得能够重新生成历史price
            if (dataGridView1.Rows.Count > 0 && dataGridView1.Columns["totalPrice"].Visible == true)
                dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells["totalPrice"];

            string serialType = ConfUtility.GetSerialType();

            //add 2018-4-13如果为已审核，就不需要重新计算previousArrear、serial、lastPayReceipt
            if (arg != null)
            {
                //新增、编辑（未审核）
                //编辑初始化时serial是不用重新计算的，但是要判断编辑初始化比较困难，为了代码方便，一刀切，在initCirculation那里会重新设回serial
                if (circulation == null || circulation.Status <= 1)
                {
                    Customer customer = CustomerDao.getInstance().FindByID((int)arg.Value);
                    this.textBox_previousArrears.Text = (this.conf.arrearsDirection * customer.arrear).ToString();
                    //2018-3-28修复
                    if (serialType == "serialType2")
                    {
                        int max = cirDao.getMaxCode(string.Format("{0}-ID{1}-{2}-", conf.code, customer.ID, DateTime.Now.ToString("yyyyMM")));
                        this.textBox_serial.Text = string.Format("{0}-ID{1}-{2}-{3:000}", conf.code, customer.ID, DateTime.Now.ToString("yyyyMM"), max + 1);
                    }

                    if (this.panel_lastPayReceipt.Visible)
                        setLastPayReceipt(customer.ID);
                }
                //编辑（已审核）不需要重新计算
            }
            //新增初始化
            else
            {
                this.textBox_serial.Text = "";
                this.textBox_previousArrears.Text = "";
                this.label_lastPayReceipt.Text = "";
            }

            resetNeedSave(true);
        }

        private void setLastPayReceipt(int customerId) {
            ProductCirculation cir = cirDao.FindLastestByCustomerID(customerId, true);
            PayReceipt payReceipt = PayReceiptDao.getInstance().FindLastestByCustomerID(customerId, true);
            double lastPayReceipt = 0;
            int cashDirection = 0;
            DateTime dt = DateTime.Now;

            if (cir != null && payReceipt != null)
            {
                if (cir.CirculationTime < payReceipt.bill_time)
                {
                    lastPayReceipt = payReceipt.thisPayed;
                    cashDirection = payReceipt.cashDirection;
                    dt = payReceipt.bill_time;
                }
                else
                {
                    lastPayReceipt = cir.ThisPayed;
                    cashDirection = cir.FlowType * -1;
                    dt = cir.CirculationTime;
                }
            }
            else if (cir != null && payReceipt == null)
            {
                lastPayReceipt = cir.ThisPayed;
                cashDirection = cir.FlowType * -1;
                dt = cir.CirculationTime;
            }
            else if (cir == null && payReceipt != null)
            {
                lastPayReceipt = payReceipt.thisPayed;
                cashDirection = payReceipt.cashDirection;
                dt = payReceipt.bill_time;
            }

            if (lastPayReceipt == 0 || cashDirection == 0)
                label_lastPayReceipt.Text = "";
            else
                label_lastPayReceipt.Text = string.Format("{0} {1:0.00}元({2:yyyy年MM月dd日})", cashDirection == -1 ? LabelUtility.LAST_PAY : LabelUtility.LAST_RECEIPT, lastPayReceipt, dt);
        
        }

        //获取控件上的previousArrears, thisPayed, realTotal，然后计算出accumulative
        private void setAccumulative(object sender, EventArgs e)
        {
            double arrear, pay, realTotal;
            double.TryParse(this.textBox_previousArrears.Text, out arrear);
            double.TryParse(this.textBox_thisPayed.Text, out pay);
            double.TryParse(this.textBox_realTotal.Text, out realTotal);
            //             先统一用原始数据来表示           新货款刚好跟货款方向一样            付款方向刚好跟货物方向一样
            double temp1 = conf.arrearsDirection * arrear + conf.productDirection * realTotal - conf.productDirection * pay;
            temp1 = temp1 * conf.arrearsDirection;
            this.textBox_accumulative.Text = temp1.ToString("0.00");

            this.label_accCap.Text = DataUtility.CmycurD((decimal)temp1);
        }

        /// <summary>
        /// 打印单据
        /// </summary>
        /// <param name="records"></param>
        /// 

        //定义Grid++Report报表主对象
        private GridppReport Report = new GridppReport();
        // 明细报表的列
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
            // 处理 明细
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
            // 获取供应商的信息
            Customer customer = CustomerDao.getInstance().FindByID(sell.CustomerID);

            DataTable dt = ConfDao.getInstance().GetAll();
            Report.ControlByName("title").AsStaticBox.Text = ConfDao.getInstance().Get(3).ToString() + conf.name + "单";

            //Report.ControlByName("info").AsStaticBox.Text = string.Format("地    址 : {0}\n银行账号 : {1}\n其他信息 : {2}", dt.Rows[3]["conf"], dt.Rows[7]["conf"], dt.Rows[8]["conf"]);
            //Report.ControlByName("contract").AsStaticBox.Text = string.Format("联 系 人 : {0}\n电话号码 : {1}\n手机号码 : {2}", dt.Rows[4]["conf"], dt.Rows[5]["conf"], dt.Rows[6]["conf"]); 

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
            Report.ControlByName("customer").AsStaticBox.Text = string.Format("{0}{1}{2}", conf.customer, sell.CustomerName, String.IsNullOrEmpty(customer.Phone) ? "" : "(手机:" + customer.Phone + ")");

            if (Report.ControlByName("customerAddr") != null)
                Report.ControlByName("customerAddr").AsStaticBox.Text = "客户地址: " + customer.Address;

            // (日期)
            Report.ControlByName("date").AsStaticBox.Text = conf.business + "时间: " + sell.CirculationTime.ToString("yyyy年MM月dd日");

            // 右(单号)
            if (Report.ControlByName("serial") != null)
                Report.ControlByName("serial").AsStaticBox.Text = "单号: NO." + sell.Code;

            // 备注
            //Report.ControlByName("total").AsStaticBox.Text = string.Format("{0:0.00}元", sell.Total);

            Report.ControlByName("realTotal").AsStaticBox.Text = string.Format("{0:0.00}元", sell.RealTotal);
            if (Report.ControlByName("text_backFreight") != null)
                Report.ControlByName("text_backFreight").AsStaticBox.Text = string.Format("{0}件×{1}元 = {2}元", this.label_totalPieces.Text, this.textBox_backFreightPerPiece.Text, this.label_totalBackFreight.Text);

            if (Report.ControlByName("lastPayReceipt") != null)
                Report.ControlByName("lastPayReceipt").AsStaticBox.Text = this.label_lastPayReceipt.Text;

            Report.ControlByName("label_pay").AsStaticBox.Text = this.label_thisPayed.Text;

            Report.ControlByName("text_pay").AsStaticBox.Text = string.Format("{0:0.00}元", double.Parse(this.textBox_thisPayed.Text));
            Report.ControlByName("text_arr").AsStaticBox.Text = string.Format("{0:0.00}元", double.Parse(this.textBox_previousArrears.Text));
            Report.ControlByName("text_acc").AsStaticBox.Text = string.Format("{0:0.00}元 ({1})", this.textBox_accumulative.Text, this.label_accCap.Text);
            //因为有些单据是没有的
            if (Report.ControlByName("oper") != null)
                Report.ControlByName("oper").AsStaticBox.Text = sell.Oper;

            fill_records(records);
        }

        private void load_without_customer(ProductCirculation sell, List<ProductCirculationRecord> records)
        {
            // (日期)
            Report.ControlByName("date").AsStaticBox.Text = sell.CirculationTime.ToString("yyyy年MM月dd日");

            // 右(单号)
            Report.ControlByName("serial").AsStaticBox.Text = "NO." + sell.Code;

            // 备注
            Report.ControlByName("comment_text").AsStaticBox.Text = sell.Comment;

            // 处理 明细
            fill_records(records);
        }

        //在C#中一次填入一条记录不能成功，只能使用一次将记录全部填充完的方式
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
            //在此记录下每个字段的接口指针
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
            // 载入报表模板数据
            string report_template_path = ConfUtility.cir_report_path;
            Report.LoadFromFile(report_template_path);
            // 连接报表事件
            Report.Initialize -= new _IGridppReportEvents_InitializeEventHandler(ReportInitialize);
            Report.Initialize += new _IGridppReportEvents_InitializeEventHandler(ReportInitialize);
            //一定要先-=，要不会重复数据
            Report.FetchRecord -= new _IGridppReportEvents_FetchRecordEventHandler(ReportFetchRecord);
            Report.FetchRecord += new _IGridppReportEvents_FetchRecordEventHandler(ReportFetchRecord);
            // 打印预览
            Report.PrintPreview(true);
        }


        /// <summary>
        /// 打印封面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton_printLetter_Click(object sender, EventArgs e)
        {
            LetterSettingForm letterSettingForm = new LetterSettingForm(circulation, this.label_totalPieces.Text);
            letterSettingForm.ShowDialog();
        }


    }
}