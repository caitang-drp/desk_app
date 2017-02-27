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
    public partial class ProductCirculationForm : MyDockContent
    {
        //open mode       | 0:add 1:edit | 2:approval  | 3:partArrival | 4:arrival
        //status          | 1:apply      | 2:approval  | 3:partArrival | 4:arrival  
        protected int openMode = 0;
        protected int circulationID = 0;
        protected ProductCirculation.CirculationType type;
        protected UpdateType notifyType;
        protected UpdateType finishNotifyType;

        private int flowType;
        private string code;

        private ProductCirculation circulation = null;
        public List<ProductCirculationRecord> records = null;

        protected bool needSave = false;
        protected bool recordChanged = false;

        private ProductCirculationDao cirDao;
        public ProductCirculationForm(CirculationTypeConf conf, ProductCirculationDao cirDao)
        {
            InitializeComponent();

            openMode = 0;
            circulationID = 0;

            this.type = conf.type;
            this.notifyType = conf.notifyType;
            this.finishNotifyType = conf.finishNotifyType;

            this.flowType = conf.flowType;

            this.Text = conf.name + "单";
            this.label_title.Text = this.Text;
            this.code = conf.code;
            this.label2.Text = conf.date;
            this.label_customer.Text = conf.customer;

            this.cirDao = cirDao;

            initDatagridview(this.dataGridView1);

        }

        private void ProductCirculationForm_Load(object sender, EventArgs e)
        {
            this.lookupText1.LookupForm = FormSingletonFactory.getInstance().getCustomerCIForm_Select();
            
            dataGridView2.Rows.Add("总价合计/元:", "");
            dataGridView2[0, 0].Style.BackColor = Color.Yellow;
            dataGridView2[0, 0].Style.SelectionBackColor = Color.Yellow;
            dataGridView2[1, 0].Style.BackColor = Color.Yellow;
            this.backgroundWorker.DoWork += new DoWorkEventHandler(backgroundWorker_DoWork);
            this.backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker_RunWorkerCompleted);

            initCirculation();
        }

        public virtual void initDatagridview(DataGridView dgv) { }

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
        /// 

        //cutoff和realTotal会互相影响，加个控制防止死循环
        private bool cutoffNeedReCaculate = true;
        private bool realTotalNeedRecaculate = true;

        private void initCirculation()
        {
            if (openMode == 0)
            {
                switchMode(openMode);
                int max = cirDao.getMaxCode(code);
                this.textBox_serial.Text = string.Format("{0}-{1}-{2:0000}", code, DateTime.Now.ToString("yyyyMMdd"), max+1);
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
                this.textBox_accumulative.Text = null;
                this.textBox_thisPayed.Text = null;


                this.resetNeedSave(false);
                this.recordChanged = false;
                
                return;
            }

            circulation = cirDao.FindByID(circulationID);

            this.textBox_serial.Text = circulation.Code;
            this.dateTime_sellTime.Value = circulation.CirculationTime;
            this.textBox_comment.Text = circulation.Comment;
            this.lookupText1.LookupArg = new LookupArg(circulation.CustomerID, circulation.CustomerName);
            this.lookupText1.Text_Lookup = circulation.CustomerName;
            this.textBox_operator.Text = circulation.Oper;

            this.dataGridView2[1, 0].Value = circulation.Total;

            //textbox_cutoff是自动计算的，类似textbox_accumulative
            this.textBox_realTotal.Text = circulation.RealTotal.ToString();
            
            //this.textBox_cutoff.Text = circulation.Total == 0?"":string.Format("{0:F}", circulation.RealTotal / circulation.Total);
            
            this.textBox_previousArrears.Text = circulation.PreviousArrears.ToString();
            this.textBox_thisPayed.Text = circulation.ThisPayed.ToString();
            this.textBox_accumulative.Text = string.Format("{0}", circulation.PreviousArrears + circulation.RealTotal - circulation.ThisPayed);
            this.backgroundWorker.RunWorkerAsync(circulation.ID);
            this.invokeBeginLoadNotify();
        }

        public override void refresh()
        {
            ((this.dataGridView1.Columns["product"] as DataGridViewLookupColumn).LookupForm as CategoryItemForm).initTree();
            //not reasonal
            (this.lookupText1.LookupForm as CategoryItemForm).initTree();
        }

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
                    this.label_status.Text = "新增";
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
                    this.initControlsEnable(false, false, false, true, false, false, false, false, true, true);
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

            this.textBox_serial.Enabled = basicInfo;
            this.dateTime_sellTime.Enabled = basicInfo;
            this.textBox_comment.Enabled = basicInfo;
            this.lookupText1.Enabled = basicInfo;
            this.textBox_operator.Enabled = basicInfo;

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
                //cell.Style.ForeColor = System.Drawing.SystemColors.ControlDark;
                cell.Style.BackColor = System.Drawing.SystemColors.Control;
                cell.Style.SelectionBackColor = System.Drawing.SystemColors.Control;
                //cell.Style.SelectionForeColor = System.Drawing.SystemColors.ControlDark;
            }
            else {
                cell.ReadOnly = false;

                cell.Style.BackColor = Color.White;
                //cell.Style.ForeColor = Color.Black;

                cell.Style.SelectionBackColor = Color.White;
                //cell.Style.SelectionForeColor = Color.Black;
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
                total += (double)row.Cells["totalPrice"].Value;

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
            circulation.Type = (int)type;
            circulation.FlowType = flowType;

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

            double total, realTotal, previousArrears, thisPayed;

            if (ValidateUtility.getDouble(this.dataGridView2[1, 0], out total)
                && ValidateUtility.getDouble(this.textBox_realTotal, this.errorProvider1, false, out realTotal)
                && ValidateUtility.getDouble(this.textBox_previousArrears, this.errorProvider1, false, out previousArrears)
                && ValidateUtility.getDouble(this.textBox_thisPayed, this.errorProvider1, false, out thisPayed))
            {
                circulation.Total = total;
                circulation.RealTotal = realTotal;
                circulation.PreviousArrears = previousArrears;
                circulation.ThisPayed = thisPayed;
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
                    MessageBox.Show(string.Format("增加{0}成功!", this.Text), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (openMode == 1)
                {/*
                    ProductStainlessCirculationDao.getInstance().UpdateBaiscInfo(circulation);
                    if (recordChanged)
                        ProductStainlessCirculationDao.getInstance().updateRecords(circulation.ID, records);
                    MessageBox.Show(string.Format("保存{0}成功!", this.Text), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);*/
                }

                openMode = 1;
                this.initCirculation();


            }
            catch (Exception ex)
            {
                if (openMode == 0)
                    ProductStainlessCirculationDao.getInstance().DeleteByID(circulationID);
                MessageBox.Show("保存有误,可能是往来单位或商品属性被修改过,请重新编辑!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            //so important: if edit ,it should be refresh also, because edit will del exist item and add new item

            this.invokeUpdateNotify(notifyType);
        }

        //下单后不能修改，现在不用这个功能
        private void toolStripButton_approval_Click(object sender, EventArgs e)
        {/*
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

            this.invokeUpdateNotify(notifyType);*/
        }

        //审核
        private void toolStripButton_finish_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("审核后，将修改库存数量，且该单据不能修改或删除，是否审核？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK)
                return;

            List<ProductCirculationRecord> records;
            this.getRecords(out records);

            ProductCirculation sell;
            this.getCirculation(out sell);

            /*
            if(flowType == -1)
                foreach (ProductCirculationRecord record in records) {
                    foreach (ProductClothesCirculationSKURecord skuRecord in record.SkuRecords) { 
                        int leftNum = ProductSKUDao.getInstance().FindNumByID(skuRecord.ProductSKUID);
                        if (skuRecord.Num > leftNum)
                        {
                            MessageBox.Show(string.Format("{0} ({1}) 库存不足, 审核失败!", record.ProductName, skuRecord.ProductSKU.getName()), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.Enabled = true;
                            return;
                        }
                    }
                }
            */

            foreach (ProductCirculationRecord record in records)
            {
                int leftNum = cirDao.getProductDao().FindNumByID(record.ProductID);
                int newLeftNum = leftNum + flowType * record.TotalNum;
                cirDao.getProductDao().UpdateNum(record.ProductID, newLeftNum);
            }

            cirDao.UpdateStatus(circulationID, 4);


            ////////////////////////////////////////////////////////////////////////
            // 先计算利润
            SellProfit sell_profit_obj = new SellProfit();
            sell_profit_obj.update_sell_profit();

            // 使用“移动加权平均法”，只有采购才需要重新计算
            // http://blog.sina.com.cn/s/blog_552cccd7010002rt.html
            if (this.type == ProductCirculation.CirculationTypeConf_Purchase.type)
            {
                // 重新计算产品的平均成本，并更新到数据库
                ProductStainless product_stainless_obj = new ProductStainless();
                product_stainless_obj.recal_product_stainless_purchase_price();
            }
            ////////////////////////////////////////////////////////////////////////


            MessageBox.Show("审核成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            openMode = 4;
            this.switchMode(4);

            this.invokeUpdateNotify(this.finishNotifyType);
            
        }


        private void toolStripButton_print_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("系统暂未开放打印功能.", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            ProductCirculation sell;
            List<ProductCirculationRecord> records;
            this.getCirculation(out sell);
            this.getRecords(out records);
            ProductSellReportForm form = new ProductSellReportForm(sell, records);
            form.ShowDialog();
        }

        private DialogResult affirmQuit() {
            return MessageBox.Show("单据尚未保存，是否放弃保存？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
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
            this.setCellEnable(row.Cells["totalPrice"], false);
            //this.setCellEnable(row.Cells["num"], false);
            
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

        private void lookupText1_valueSetted(object sender, LookupArg arg)
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
                //这里的textBox_realTotal_TextChanged是不是异步？
            }

            this.cutoffNeedReCaculate = true;
        }

        private void textBox_realTotal_TextChanged(object sender, EventArgs e)
        {
            this.realTotalNeedRecaculate = false;
            if (this.cutoffNeedReCaculate == true) {
                double realTotal = 0;
                double.TryParse(this.textBox_realTotal.Text, out realTotal);
                double total = (double)this.dataGridView2[1, 0].Value;
                if(total != 0)
                    this.textBox_cutoff.Text = string.Format("{0}", realTotal / total * 100);
            }
            this.realTotalNeedRecaculate = true;
        }

        private void textBox_previousArrears_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox_thisPayed_TextChanged(object sender, EventArgs e)
        {

        }
    }
}