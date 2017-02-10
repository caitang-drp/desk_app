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
    public partial class SellReceiptBillForm: MyDockContent
    {
        //open mode       | 0:add 1:edit | 2:approval  | 3:partArrival | 4:arrival
        //status          | 1:apply      | 2:approval  | 3:partArrival | 4:arrival  
        private int openMode = 0;
        private int circulationID = 0;
        private ProductCirculation.CirculationType type;
        private UpdateType notifyType;
        private UpdateType finishNotifyType;

        private int flowType;
        private string code;

        private ProductCirculation sell = null;
        public List<ProductCirculationRecord> records = null;

        private bool needSave = false;
        private bool recordChanged = false;

        public SellReceiptBillForm(CirculationTypeConf conf)
        {
            InitializeComponent();

            openMode = 0;
            circulationID = 0;

            this.type = conf.type;
            this.notifyType = conf.notifyType;
            this.finishNotifyType = conf.finishNotifyType;

            this.flowType = conf.flowType;

            this.label_title.Text = this.Text;
            this.code = conf.code;
            this.label2.Text = conf.date;
            this.label_customer.Text = conf.customer;


            // 设置标题
            this.Text = "销售收款单";
            this.label_title.Text = "销售收款单";

            // 点击，选择客户
            this.lookuptext_customer.LookupForm = FormSingletonFactory.getInstance().getCustomerCIForm_Select();

            // 设置单据编号
            int max = ProductCirculationDao.getInstance().getMaxCode(code);
            this.textBox_serial.Text = string.Format("{0}-{1}-{2:0000}", code, DateTime.Now.ToString("yyyyMMdd"), max+1);

            // 设置以上欠款
            this.textBox_last_arrear.Text = this.get_customer_last_receipt().ToString();
            // 设置累计欠款
            this.textBox_now_arrear.Text = "0.0";
        }
        
        // 
        private void textBox_receipt_amount_textchanged(object sender, EventArgs e)
        {
            double last_receipt = this.get_customer_last_receipt();
            double now_arrear;

            try {
                now_arrear = last_receipt - Convert.ToDouble(this.textBox_receipt_amount.Text);
            }
            catch {
                now_arrear = last_receipt;
            }

            this.textBox_now_arrear.Text = now_arrear.ToString();
        }

        // 获取 以上欠款
        private double get_customer_last_receipt()
        {
            // TODO
            return 99999999999.9;
        }

        // 更新客户欠款
        private void update_customer_arrear()
        {
            try {
                double now_customer_arrear = Convert.ToDouble(this.textBox_now_arrear.Text);
                // TODO,写入数据库
            }
            catch {

            }
        }
        
        // 把“销售收款单”写入数据库
        private void write_bill_to_db()
        {
            PayReceipt tmp = new PayReceipt();
            tmp.bill_type = PayReceipt.BillType.SellReceipt;
            tmp.comment = textBox_comment.Text;
            tmp.bill_time = dateTime_pay_time.Value;
            ValidateUtility.getLookupValueID(lookuptext_customer, this.errorProvider1, out tmp.customer_id);
            tmp.serial = textBox_serial.Text;
            tmp.handle_people = textBox_operator.Text;
            tmp.amount = Convert.ToDouble(textBox_receipt_amount.Text);

            PayReceiptDao.getInstance().Insert(tmp);
        }

        // 点击 保存 响应事件
        private void toolStripButton_save_Click(object sender, EventArgs e)
        {
            update_customer_arrear();
            write_bill_to_db();

            // 关闭窗口
            this.Close();
        }


        /*
        private void ProductCirculationForm_Load(object sender, EventArgs e)
        {
            (this.dataGridView1.Columns["product"] as DataGridViewLookupColumn).LookupForm = FormMgr.getInstance().getProductCIForm_select();  
            (this.dataGridView1.Columns["num"] as DataGridViewLookupColumn).LookupForm = new ProductClothesInputNumForm(this);

            this.lookuptext_customer.LookupForm = FormMgr.getInstance().getCustomerCIForm_Select();
            
            dataGridView2.Rows.Add("总价合计/元:", "");

            this.backgroundWorker.DoWork += new DoWorkEventHandler(backgroundWorker_DoWork);
            this.backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker_RunWorkerCompleted);

            initCirculation();
        }

        public void reload(int mode, int id) {
            if (needSave && affirmQuit() != DialogResult.OK)
                return;
            
            openMode = mode;
            circulationID = id;
            initCirculation();
        }

        /// <summary>
        /// for init circulation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void initCirculation()
        {
            if (openMode == 0)
            {
                switchMode(openMode);
                int max = ProductCirculationDao.getInstance().getMaxCode(code);
                this.textBox_serial.Text = string.Format("{0}-{1}-{2:0000}", code, DateTime.Now.ToString("yyyyMMdd"), max+1);
                this.dateTime_pay_time.Value = DateTime.Now;
                this.textBox_comment.Text = null;
                this.lookuptext_customer.LookupArg = null;
                this.lookuptext_customer.Text_Lookup = null;
                this.textBox_operator.Text = null;
                this.dataGridView1.Rows.Clear();
                this.dataGridView2[1, 0].Value = null;

                this.textBox_now_arrear.Text = null;
                this.textBox_pay.Text = null;

                this.resetNeedSave(false);
                this.recordChanged = false;
                
                return;
            }

            sell = ProductCirculationDao.getInstance().FindByID(circulationID);

            this.textBox_serial.Text = sell.Code;
            this.dateTime_pay_time.Value = sell.CirculationTime;
            this.textBox_comment.Text = sell.Comment;
            this.lookuptext_customer.LookupArg = new LookupArg(sell.CustomerID, sell.CustomerName);
            this.lookuptext_customer.Text_Lookup = sell.CustomerName;
            this.textBox_operator.Text = sell.Oper;

            this.textBox_now_arrear.Text = sell.Payed.ToString();
            this.textBox_pay.Text = sell.Pay.ToString();

            this.backgroundWorker.RunWorkerAsync(sell.ID);
            this.invokeBeginLoadNotify();
        }

        public override void refresh()
        {
            ((this.dataGridView1.Columns["product"] as DataGridViewLookupColumn).LookupForm as CategoryItemForm).initTree();
            //not reasonal
            (this.lookuptext_customer.LookupForm as CategoryItemForm).initTree();
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            int sellID = (int)e.Argument;
            records = ProductCirculationRecordDao.getInstance().FindList(sellID);
            foreach (ProductCirculationRecord record in records)
            {
                record.SkuRecords = ProductCirculationSKURecordDao.getInstance().FindList(record.ID);
                record.NumText = record.getTxt();            
            }
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            
            this.dataGridView1.Rows.Clear();
            foreach (ProductCirculationRecord record in records)
            {
                
                int index = this.dataGridView1.Rows.Add();
                this.dataGridView1.Rows[index].Cells["ID"].Value = record.ID;
                this.dataGridView1.Rows[index].Cells["product"].Value = new LookupArg(record.ProductID, record.ProductName);
                this.dataGridView1.Rows[index].Cells["price"].Value = record.Price;
                DataGridViewLookupCell numCell = this.dataGridView1.Rows[index].Cells["num"] as DataGridViewLookupCell;
                numCell.resetSize(record.NumText);
                numCell.Value = new LookupArg(record, record.NumText);

                this.setSubTotalPrice(index);
            }

            this.setTotalPrice();

            if (sell != null)
                openMode = sell.Status;
            switchMode(openMode);

            this.resetNeedSave(false);
            this.recordChanged = false;

            this.invokeEndLoadNotify();
        }
        //end init

        private void switchMode(int mode) { 
            switch(mode){
                case 0:
                    this.label_status.Text = "新增";
                    this.initControlsEnable(true, false, false, false, true, true, true, false, false,false);
                    break;
                case 1:
                    this.label_status.Text = ProductCirculation.circulationStatusContext[0];
                    this.initControlsEnable(true, true, true, false, true, true, true, false, false,false);
                    break;
                case 2:
                    this.label_status.Text = ProductCirculation.circulationStatusContext[1];
                    this.initControlsEnable(false, false, true, true, false, false, false, true, true, false);
                    break;
                case 3:
                    this.label_status.Text = ProductCirculation.circulationStatusContext[2];
                    this.initControlsEnable(false, false, false, true, false, false, false, true, true, false);
                    break;
                case 4:
                    this.label_status.Text = ProductCirculation.circulationStatusContext[3];
                    this.initControlsEnable(false, false, false, true, false, false, false, false, true, true);
                    break;
                default:
                    break;
            }
        }

        private void initControlsEnable(bool save, bool approval, bool finish, bool print, bool basicInfo,
            bool add, bool del, bool saveArrival, bool elementReadonly, bool pay)
        {
            //this.toolStripButton_save.Enabled = save;
            this.toolStripButton_approval.Enabled = approval;
            this.toolStripButton_finish.Enabled = finish;
            this.toolStripButton_print.Enabled = print;

            this.textBox_serial.Enabled = basicInfo;
            this.dateTime_pay_time.Enabled = basicInfo;
            this.textBox_comment.Enabled = basicInfo;
            this.lookuptext_customer.Enabled = basicInfo;
            this.textBox_operator.Enabled = basicInfo;

            this.button_add.Enabled = add;
            this.button_del.Enabled = del;
            //this.toolStripButton_saveArrival.Enabled = saveArrival;

            foreach (DataGridViewRow row in this.dataGridView1.Rows) {
                if (elementReadonly == true) {
                    setCellEnable(row.Cells["product"], false);
                    setCellEnable(row.Cells["num"], false);
                    setCellEnable(row.Cells["price"], false);
                    //setCellDisable(row.Cells["check"]);
                }
                setCellEnable(row.Cells["totalPrice"], false);
            }

            this.dataGridView1.Columns["check"].Visible = !elementReadonly;

            this.panel_pay.Enabled = pay;
        }

        private void setCellEnable(DataGridViewCell cell, bool enable) {
            if (enable == false)
            {
                cell.ReadOnly = true;

                cell.Style.BackColor = System.Drawing.SystemColors.Control;
                cell.Style.ForeColor = System.Drawing.SystemColors.ControlDark;

                cell.Style.SelectionBackColor = System.Drawing.SystemColors.Control;
                cell.Style.SelectionForeColor = System.Drawing.SystemColors.ControlDark;
            }
            else {
                cell.ReadOnly = false;

                cell.Style.BackColor = Color.White;
                cell.Style.ForeColor = Color.Black;

                cell.Style.SelectionBackColor = Color.White;
                cell.Style.SelectionForeColor = Color.Black;
            }
        }

        public void hideSomeControls() {
            this.label9.Visible = false;
            this.label_customer.Visible = false;
            this.lookuptext_customer.Visible = false;

            //this.label_operator.Visible = false;
            //this.textBox_operator.Visible = false;

            this.dataGridView1.Columns["price"].Visible = false;
            this.dataGridView1.Columns["totalPrice"].Visible = false;
            this.dataGridView2.Visible = false;
            this.panel_pay.Visible = false;
        }

        /// <summary>
        /// set datagridview value
        /// </summary>
        /// <param name="rowIndex"></param>
        private void setSubTotalPrice(int rowIndex)
        {
            DataGridViewRow row = this.dataGridView1.Rows[rowIndex];
            double price;
            int num=0;
            ValidateUtility.getDouble(row.Cells["price"], out price);
            try
            {
                object temp = (row.Cells["num"] as DataGridViewLookupCell).EditedValue;
                num = ((temp as LookupArg).Value as ProductCirculationRecord).TotalNum;
            }
            catch(Exception ex) {
                ex.Message.ToString();
            }
            row.Cells["totalPrice"].Value = num * price;
        }

        private void setTotalPrice()
        {
            double total = 0;
            int number = this.dataGridView1.RowCount;
            foreach (DataGridViewRow row in this.dataGridView1.Rows)
                total += (double)row.Cells["totalPrice"].Value;

            this.dataGridView2[1, 0].Value = total;
        }

        /// <summary>
        /// for get value from controls
        /// </summary>

        private bool getCirculation(out ProductCirculation sell)
        {
            sell = new ProductCirculation();
            sell.ID = circulationID;
            sell.Type = (int)type;
            sell.FlowType = flowType;

            string name;
            if (ValidateUtility.getName(this.textBox_serial, this.errorProvider1, out name) == false)
                return false;
            sell.Code = name;

            int customerID=-1;
            if (this.lookuptext_customer.Visible == true && ValidateUtility.getLookupValueID(this.lookuptext_customer, this.errorProvider1, out customerID) == false)
                return false;

            sell.CustomerID = customerID;

            sell.CirculationTime = this.dateTime_pay_time.Value;
            sell.Comment = this.textBox_comment.Text;
            sell.Oper = this.textBox_operator.Text;

            if (dataGridView2[1, 0].Value == null || dataGridView2[1, 0].Value.ToString()=="")
                sell.TotalPrice = 0;
            else
                sell.TotalPrice = (double)dataGridView2[1, 0].Value;

            sell.CustomerName = this.lookuptext_customer.Text_Lookup;
            return true;
        }

        private bool getRecords(out List<ProductCirculationRecord> records)
        {
            records = new List<ProductCirculationRecord>();

            int number = this.dataGridView1.RowCount;

            double tempDouble;
            //int tempInt;
            bool isInputCorrect = true;

            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                object productID = null;
                object tempRecord = null;

                if (ValidateUtility.getLookupValue(row.Cells["product"], out productID) == false || ValidateUtility.getDouble(row.Cells["price"], out tempDouble) == false || ValidateUtility.getLookupValue(row.Cells["num"], out tempRecord) == false)
                    return false;
                ProductCirculationRecord record = tempRecord as ProductCirculationRecord;
                
                record.ProductName = ((row.Cells["product"] as DataGridViewLookupCell).EditedValue as LookupArg).Text;
                record.Price = tempDouble;
                records.Add(record);
            }

            return isInputCorrect;
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
        private void toolStripButton_save_Click(object sender, EventArgs e)
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

            for(int i=0;i<records.Count;i++){
                ProductCirculationRecord record = records[i];
                for (int j = 0; j < records.Count; j++)
                {
                    ProductCirculationRecord compare = records[j];
                    if (compare.ProductID == record.ProductID && i != j) {
                        MessageBox.Show(string.Format("商品{0}有多条记录,请在同一记录里输入该商品的所有数量!", compare.ProductName), "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }

            try
            {
                if (openMode == 0)
                {
                    circulation.Status = 1;
                    ProductCirculationDao.getInstance().Insert(circulation, records, out circulationID);
                    MessageBox.Show(string.Format("增加{0}成功!", this.Text), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (openMode == 1)
                {
                    ProductCirculationDao.getInstance().UpdateBaiscInfo(circulation);
                    if (recordChanged)
                        ProductCirculationDao.getInstance().updateRecords(circulation.ID, records);
                    MessageBox.Show(string.Format("保存{0}成功!", this.Text), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                openMode = 1;
                this.initCirculation();


            }catch(Exception ex){
                if (openMode == 0)
                    ProductCirculationDao.getInstance().DeleteByID(circulationID);
                MessageBox.Show("保存有误,可能是往来单位或商品属性被修改过,请重新编辑!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            //so important: if edit ,it should be refresh also, because edit will del exist item and add new item
            
            this.invokeUpdateNotify(notifyType);
        }

        private void toolStripButton_approval_Click(object sender, EventArgs e)
        {
            if (this.toolStripButton_save.Enabled == true)
            {
                MessageBox.Show("请先保存单据,再下单!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("下单后，该单据不能修改，是否下单？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK)
                return;

            List<ProductCirculationRecord> records;
            this.getRecords(out records);

            bool isLeftEnought = true;

            //no reasonable
            for (int i = 0; i < records.Count; i++)
            {
                int productID = records[i].ProductID;
                int leftNum = ProductClothesDao.getInstance().FindByID(productID).Num;
                if (records[i].TotalNum > leftNum)
                    isLeftEnought = false;
            }

            if(isLeftEnought == false)
                if (MessageBox.Show("库存不足，是否继续下单？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK)
                    return;

            ProductSellDao.getInstance().UpdateStatus(circulationID, 2);
            openMode = 2;
            this.switchMode(2);

            this.invokeUpdateNotify(notifyType);
        }

        private void toolStripButton_finish_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("审核后，将修改库存数量，且该单据不能修改或删除，是否审核？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK)
                return;

            List<ProductCirculationRecord> records;
            this.getRecords(out records);

            ProductCirculation sell;
            this.getCirculation(out sell);

            if(flowType == -1)
                foreach (ProductCirculationRecord record in records) {
                    foreach (ProductCirculationSKURecord skuRecord in record.SkuRecords) { 
                        int leftNum = ProductSKUDao.getInstance().FindNumByID(skuRecord.ProductSKUID);
                        if (skuRecord.Num > leftNum)
                        {
                            MessageBox.Show(string.Format("{0} ({1}) 库存不足, 审核失败!", record.ProductName, skuRecord.ProductSKU.getName()), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.Enabled = true;
                            return;
                        }
                    }
                }

            foreach (ProductCirculationRecord record in records)
            {
                foreach (ProductCirculationSKURecord skuRecord in record.SkuRecords)
                {
                    int leftNum = ProductSKUDao.getInstance().FindNumByID(skuRecord.ProductSKUID);
                    int newLeftNum = leftNum + flowType * skuRecord.Num;
                    ProductSKUDao.getInstance().UpdateNum(skuRecord.ProductSKUID, newLeftNum);
                }
            }

            ProductCirculationDao.getInstance().UpdateStatus(circulationID, 4);

            //commented by stone:very not reasonable
            ProductCirculationDao.getInstance().UpdatePay(circulationID, double.Parse(dataGridView2[1, 0].Value.ToString()), 0);
            this.textBox_pay.Text = dataGridView2[1, 0].Value.ToString();
            this.textBox_now_arrear.Text = "0";
            
            MessageBox.Show("审核成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            openMode = 4;
            this.switchMode(4);

            this.invokeUpdateNotify(this.finishNotifyType);

        }

        */
        private void toolStripButton_print_Click(object sender, EventArgs e)
        {
            MessageBox.Show("系统暂未开放打印功能.", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            /*
            ProductCirculation sell;
            List<ProductCirculationRecord> records;
            this.getCirculation(out sell);
            this.getRecords(out records);
            ProductSellReportForm form = new ProductSellReportForm(sell, records);
            form.ShowDialog();*/
        }

        private DialogResult affirmQuit() {
            return MessageBox.Show("单据尚未保存，是否放弃保存？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }

        private void toolStripButton_cancel_Click(object sender, EventArgs e)
        {
            if (this.needSave && affirmQuit() != DialogResult.OK)
                return;

            this.Close();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            if (this.needSave && affirmQuit() != DialogResult.OK)
                e.Cancel = true;
        }

        /*
        //add detail
        private void button_add_Click(object sender, EventArgs e)
        {
            //File.AppendAllText("e:\\debug.txt", string.Format("add click, dataGridView hash code={0}, dataGridView name={1}\r\n", dataGridView1.GetHashCode(), dataGridView1.Name));

            this.dataGridView1.Rows.Add();
            
            DataGridViewRow row = this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1];
            this.setCellEnable(row.Cells["totalPrice"], false);
            this.setCellEnable(row.Cells["num"], false);
            
            setSubTotalPrice(row.Index);
            setTotalPrice();

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
        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control.GetType().Equals(typeof(DataGridViewTextBoxEditingControl)))//cell为类TextBox时
            {
                e.CellStyle.BackColor = Color.FromName("window");
                DataGridViewTextBoxEditingControl editingControl = e.Control as DataGridViewTextBoxEditingControl;
                
                editingControl.TextChanged -= new EventHandler(editingControl_TextChanged);
                editingControl.TextChanged += new EventHandler(editingControl_TextChanged);
            }
            else if (e.Control.GetType().Equals(typeof(DataGridViewLookupEditingControl)))
            {
                DataGridViewLookupEditingControl editingControl = e.Control as DataGridViewLookupEditingControl;
                string columnName = this.dataGridView1.CurrentCell.OwningColumn.Name;
                editingControl.valueSetted -= new LookupText.ValueSetted(productEditingControl_valueSetted);
                editingControl.valueSetted += new LookupText.ValueSetted(productEditingControl_valueSetted);
                if(columnName == "num"){
                    ProductClothesInputNumForm form = editingControl.LookupForm as ProductClothesInputNumForm;
                    LookupArg arg = this.dataGridView1.CurrentCell.OwningRow.Cells["product"].Value as LookupArg;
                    form.setTitle(arg.Text);
                    form.ProductId = (int)(arg.Value);

                    editingControl.LookupArg = (this.dataGridView1.CurrentCell.OwningRow.Cells["num"] as DataGridViewLookupCell).Value as LookupArg;
                }
            }
 
        }
        */

        /*
        void productEditingControl_valueSetted(object sender, LookupArg arg)
        {
            //File.AppendAllText("e:\\debug.txt", string.Format("value changed, thread:{0}\r\n", System.Threading.Thread.CurrentThread.ManagedThreadId));

            DataGridViewLookupEditingControl control = (sender as DataGridViewLookupEditingControl);
            */

            /*if (dataGridView1.Columns.Count <= control.ColumnIndex)
            {
                System.Threading.Thread.Sleep(0);
            }*/
            
            //DataGridViewCell cell = this.dataGridView1[control.ColumnIndex, control.EditingControlRowIndex];
            
            //if (cell.OwningColumn.Name == "product")

            /*
            try
            {
                //File.AppendAllText("e:\\debug.txt", string.Format("value changed, dataGridView hash code={0}, dataGridView name={1}\r\n", control.EditingControlDataGridView.GetHashCode(), control.EditingControlDataGridView.Name));
                if (control.EditingControlDataGridView.Rows.Count == 0 || control.EditingControlDataGridView.CurrentCell == null)
                    throw new Exception();

                //if(control.EditingControlDataGridView.CurrentCell.OwningColumn.Name == "product")
                if (!string.IsNullOrEmpty(arg.ArgName) && arg.ArgName == "Product")
                {
                    int productID = (int)(arg.Value);
                    int oldID = -1;
                    int.TryParse((control.EditingControlDataGridView.Rows[control.EditingControlRowIndex].Cells["product"].Value).ToString(), out oldID);
                    if (productID != oldID)
                    {
                        control.EditingControlDataGridView.Rows[control.EditingControlRowIndex].Cells["price"].Value = ProductClothesDao.getInstance().FindPriceByID(productID);
                        DataGridViewLookupEditingControl lookup = sender as DataGridViewLookupEditingControl;
                        this.setCellEnable(control.EditingControlDataGridView.Rows[control.EditingControlRowIndex].Cells["num"], true);
                        control.EditingControlDataGridView.Rows[control.EditingControlRowIndex].Cells["num"].Value = new LookupArg("", "");
                    }
                }
                //not reasonal
                setSubTotalPrice(control.EditingControlRowIndex);
                setTotalPrice();
            }
            catch(Exception ex) {
                //File.AppendAllText("e:\\debug.txt",string.Format("exception, dataGridView.Rows.Count={0}\r\n", this.dataGridView1.Rows.Count));
                //File.AppendAllText("e:\\debug.txt", string.Format("exception, dataGridView hash code={0}, dataGridView name={1}\r\n", control.EditingControlDataGridView.GetHashCode(), control.EditingControlDataGridView.Name));
                System.Threading.Thread.Sleep(0);
            }

            this.resetNeedSave(true);
            this.recordChanged = true;
        }

        void editingControl_TextChanged(object sender, EventArgs e)
        {
            DataGridViewTextBoxEditingControl control = (sender as DataGridViewTextBoxEditingControl);
            File.AppendAllText("e:\\debug.txt", string.Format("editingControl text changed, dataGridView hash code={0}, dataGridView name={1}\r\n", control.EditingControlDataGridView.GetHashCode(), control.EditingControlDataGridView.Name));

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
        */

        private void resetNeedSave(bool value)
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

        private void lookupText_customer_valueSetted(object sender, LookupArg arg)
        {
            resetNeedSave(true);
        }

        /*
        private void button_savePay_Click(object sender, EventArgs e)
        {
            double pay = 0;
            double payed = 0;
            if (ValidateUtility.getDouble(this.textBox_now_arrear, this.errorProvider1, false, out payed) &&
                ValidateUtility.getDouble(this.textBox_pay, this.errorProvider1,false, out pay))
            {
                ProductCirculationDao.getInstance().UpdatePay(circulationID, pay, payed);
                MessageBox.Show("保存货款信息成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.invokeUpdateNotify(notifyType);
            }
        }
        */
    }
}