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

namespace LocalERP.WinForm
{
    public partial class ProductSellDetailForm : MyDockContent
    {
        //open mode       | 0:add 1:edit | 2:approval  | 3:partArrival | 4:arrival
        //status          | 1:apply      | 2:approval  | 3:partArrival | 4:arrival  
        private int openMode = 0;
        private int sellID = 0;
        private DataTable productTable = null;

        public ProductSellDetailForm(int mode, int id)
        {
            sellID = id;
            openMode = mode;
            if (openMode != 0) {
                openMode = ProductCirculationDao.getInstance().FindByID(sellID).Status;
            }
            InitializeComponent();

            this.lookupText1.LookupForm = new CategoryItemForm(0, new CustomerCategoryItemProxy(), DataUtility.DATA_CUSTOMER, this);
        }

        /// <summary>
        /// for init
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProductSellDetailForm_Load(object sender, EventArgs e)
        {
            dataGridView2.Rows.Add("总价合计/元:", "");

            DataGridViewComboBoxColumn productColumn = this.dataGridView1.Columns["product"] as DataGridViewComboBoxColumn;
            productTable = ProductDao.getInstance().FindList(null);
            productColumn.DataSource = productTable;
            productColumn.ValueMember = "ID";
            productColumn.DisplayMember = "name";

            initSellDetail();
        }

        private void initSellDetail()
        {
            if (openMode == 0)
            {
                switchMode(openMode);
                this.textBox_serial.Text = "售单" + DateTime.Now.ToShortDateString();
                return;
            }

            ProductSell sell = ProductSellDao.getInstance().FindByID(sellID);

            this.textBox_serial.Text = sell.Name;
            this.dateTime_sellTime.Value = sell.SellTime;
            this.textBox_comment.Text = sell.Comment;
            //this.comboBox_customer.SelectedValue = sell.CustomerID;
            this.lookupText1.Value_Lookup = sell.CustomerID.ToString();
            this.lookupText1.Text_Lookup = sell.CustomerName;

            this.dataGridView1.Rows.Clear();

            DataTable dataTable = ProductSellRecordDao.getInstance().FindList(sell.ID);
            foreach (DataRow dr in dataTable.Rows)
            {
                int index = this.dataGridView1.Rows.Add();
                this.dataGridView1.Rows[index].Cells["ID"].Value = dr["ID"];
                this.dataGridView1.Rows[index].Cells["product"].Value = (int)dr["productID"];
                this.dataGridView1.Rows[index].Cells["num"].Value = dr["num"];
                this.dataGridView1.Rows[index].Cells["price"].Value = dr["price"];

                this.setSubTotalPrice(index);
            }

            this.setTotalPrice();

            if (sell != null)
                openMode = sell.Status;
            switchMode(openMode);

            this.resetSaveButton(false);
        }

        private void switchMode(int mode) { 
            switch(mode){
                case 0:
                    this.label_status.Text = "新增";
                    this.initControlsEnable(true, false, false, false, true, true, true, false, false);
                    break;
                case 1:
                    this.label_status.Text = ProductSell.statusEnum[0];
                    this.initControlsEnable(true, true, false, false, true, true, true, false, false);
                    break;
                case 2:
                    this.label_status.Text = ProductSell.statusEnum[1];
                    this.initControlsEnable(false, false, true, true, false, false, false, true, true);
                    break;
                case 3:
                    this.label_status.Text = ProductSell.statusEnum[2];
                    this.initControlsEnable(false, false, false, true, false, false, false, true, true);
                    break;
                case 4:
                    this.label_status.Text = ProductSell.statusEnum[3];
                    this.initControlsEnable(false, false, false, true, false, false, false, false, true);
                    break;
                default:
                    break;
            }
        }

        private void initControlsEnable(bool save, bool approval, bool finish, bool print, bool basicInfo,
            bool add, bool del, bool saveArrival, bool elementReadonly)
        {
            //this.toolStripButton_save.Enabled = save;
            this.toolStripButton_approval.Enabled = approval;
            this.toolStripButton_finish.Enabled = finish;
            this.toolStripButton_print.Enabled = print;

            this.textBox_serial.Enabled = basicInfo;
            this.dateTime_sellTime.Enabled = basicInfo;
            this.textBox_comment.Enabled = basicInfo;
            //this.comboBox_customer.Enabled = basicInfo;
            this.lookupText1.Enabled = basicInfo;

            this.button_add.Enabled = add;
            this.button_del.Enabled = del;
            //this.toolStripButton_saveArrival.Enabled = saveArrival;

            foreach (DataGridViewRow row in this.dataGridView1.Rows) {
                if (elementReadonly == true) {
                    setCellDisable(row.Cells["product"]);
                    setCellDisable(row.Cells["num"]);
                    setCellDisable(row.Cells["price"]);
                    //setCellDisable(row.Cells["check"]);
                }
                setCellDisable(row.Cells["totalPrice"]);
            }

            this.dataGridView1.Columns["check"].Visible = !elementReadonly;
        }

        private void setCellDisable(DataGridViewCell cell) {
            cell.ReadOnly = true;

            cell.Style.BackColor = System.Drawing.SystemColors.Control;
            cell.Style.ForeColor = System.Drawing.SystemColors.ControlDark;

            cell.Style.SelectionBackColor = System.Drawing.SystemColors.Control;
            cell.Style.SelectionForeColor = System.Drawing.SystemColors.ControlDark;            
        }

        /// <summary>
        /// set datagridview value
        /// </summary>
        /// <param name="rowIndex"></param>
        private void setSubTotalPrice(int rowIndex)
        {
            DataGridViewRow row = this.dataGridView1.Rows[rowIndex];
            double price;
            int num;
            this.getDouble(row.Cells["price"], out price);
            this.getInt(row.Cells["num"], out num);
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

        private bool getSell(out ProductSell sell)
        {
            sell = new ProductSell();
            sell.ID = sellID;
            
            string name;
            if (this.getName(out name) == false)
                return false;
            sell.Name = name;

            int customerID;
            if (this.getCustomerID(out customerID) == false)
                return false;
            sell.CustomerID = customerID;

            sell.SellTime = this.dateTime_sellTime.Value;
            sell.Comment = this.textBox_comment.Text;
            
            string tempValue = dataGridView2[1, 0].Value.ToString();
            if (string.IsNullOrEmpty(tempValue))
                sell.TotalPrice = 0;
            else
                sell.TotalPrice = (double)dataGridView2[1, 0].Value;

            sell.CustomerName = this.lookupText1.Text_Lookup;
            return true;
        }

        private bool getName(out string name)
        {
            if (string.IsNullOrEmpty(this.textBox_serial.Text))
            {
                this.errorProvider1.SetError(this.textBox_serial, "输入不能为空!");
                name = "";
                return false;
            }
            else
            {
                this.errorProvider1.SetError(this.textBox_serial, string.Empty);
                name = this.textBox_serial.Text;
                return true;
            }
        }

        private bool getCustomerID(out int id)
        {
            if (this.lookupText1.Value_Lookup == null)
            {
                this.errorProvider1.SetError(this.lookupText1, "客户不能为空,请在[数据配置]里增加客户!");
                id = 0;
                return false;
            }
            else
            {
                this.errorProvider1.SetError(this.lookupText1, string.Empty);
                id = int.Parse(this.lookupText1.Value_Lookup);
                return true;
            }
        }

        private bool getRecords(out List<ProductSellRecord> records)
        {
            records = new List<ProductSellRecord>();

            int number = this.dataGridView1.RowCount;

            double tempDouble;
            int tempInt;
            bool isInputCorrect = true;

            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                DataGridViewComboBoxCell cell = row.Cells["product"] as DataGridViewComboBoxCell;
                tempInt = Convert.ToInt32(cell.Value);
                if (tempInt == 0)
                    continue;

                ProductSellRecord record = new ProductSellRecord();
                record.ProductID = tempInt;

                this.getInt(row.Cells["ID"], out tempInt);
                record.ID = tempInt;

                //stone: this should be tested
                record.ProductName = cell.FormattedValue as string;

                if (this.getInt(row.Cells["num"], out tempInt) == false)
                    isInputCorrect = false;
                record.Num = tempInt;

                if (this.getDouble(row.Cells["price"], out tempDouble) == false)
                    isInputCorrect = false;
                record.Price = tempDouble;

                this.getDouble(row.Cells["totalPrice"], out tempDouble);
                record.TotalPrice = tempDouble;

                records.Add(record);
            }

            return isInputCorrect;
        }

        public bool getDouble(DataGridViewCell cell, out double result)
        {
            result = 0;
            string temp = cell.EditedFormattedValue.ToString();
            if (temp == null || temp == "" || double.TryParse(temp, out result))
            {
                cell.ErrorText = string.Empty;
                return true;
            }
            else {
                cell.ErrorText = "请输入数字!";
                return false;
            }
        }

        private bool getInt(DataGridViewCell cell, out int result)
        {
            result = 0;
            string temp = cell.EditedFormattedValue.ToString();
            if (temp == null || temp == "" || int.TryParse(temp, out result))
            {
                cell.ErrorText = string.Empty;
                return true;
            }
            else
            {
                cell.ErrorText = "请输入整数!";
                return false;
            }
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
            if (dataGridView1.Rows.Count > 0)
                dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells["totalPrice"];


            List<ProductSellRecord> records;
            bool isRecordsCorrect = getRecords(out records);

            ProductSell sell;
            bool isSellCorrect = getSell(out sell);
            if (isRecordsCorrect == false || isSellCorrect == false)
                return;
           
            if (openMode == 0)
            {
                sell.Status = 1;
                sellID = ProductSellDao.getInstance().Insert(sell, records);
                MessageBox.Show("增加订单成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (openMode == 1)
            {
                ProductSellDao.getInstance().Update(sell, records);
                MessageBox.Show("保存订单成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            //so important: if edit ,it should be refresh also, because edit will del exist item and add new item
            openMode = 1;
            this.initSellDetail();

            this.invokeChangeNotify(NotifyType.PurchaseUpdate);
        }

        private void toolStripButton_approval_Click(object sender, EventArgs e)
        {
            if (this.toolStripButton_save.Enabled == true)
            {
                MessageBox.Show("请先保存售单,再下单!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("下单后，该售单不能修改，是否下单？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK)
                return;

            List<ProductSellRecord> records;
            this.getRecords(out records);

            bool isLeftEnought = true;

            //no reasonable
            for (int i = 0; i < records.Count; i++)
            {
                int productID = records[i].ProductID;
                int leftNum = ProductDao.getInstance().FindByID(productID).Num;
                if (records[i].Num > leftNum)
                    isLeftEnought = false;
            }

            if(isLeftEnought == false)
                if (MessageBox.Show("库存不足，是否继续下单？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK)
                    return;

            ProductSellDao.getInstance().UpdateStatus(sellID, 2);
            openMode = 2;
            this.switchMode(2);

            this.invokeChangeNotify(NotifyType.PurchaseUpdate);
        }

        private void toolStripButton_finish_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("发货后，将从成品库存里自动扣除成品，且该售单不能修改，是否发货？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK)
                return;

            List<ProductSellRecord> records;
            this.getRecords(out records);

            ProductSell sell;
            this.getSell(out sell);

            //no reasonable
            for (int i = 0; i < records.Count; i++)
            {
                int productID = records[i].ProductID;
                int leftNum = ProductDao.getInstance().FindByID(productID).Num;
                if (records[i].Num > leftNum)
                {
                    MessageBox.Show("成品库存不足, 发货失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Enabled = true;
                    return;
                }
            }

            for (int i = 0; i < records.Count; i++)
            {
                int productID = records[i].ProductID;
                Product product = ProductDao.getInstance().FindByID(productID);
                
                int leftNum = product.Num - records[i].Num;
                ProductFlow flow = new ProductFlow(product.ID, product.Name, -1, records[i].Num, leftNum, string.Format("成品销售-订单ID:{0}[{1}]", sell.ID, sell.Name), DateTime.Now, 4);
                ProductFlowDao.getInstance().Insert(flow);
                ProductDao.getInstance().UpdateNum(product.ID, leftNum);
                
            }

            ProductSellDao.getInstance().UpdateStatus(sellID, 3);

            MessageBox.Show("发货成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            openMode = 3;
            this.switchMode(3);

            this.invokeChangeNotify(NotifyType.PurchaseUpdate);
        }


        private void toolStripButton_print_Click(object sender, EventArgs e)
        {
            ProductSell sell;
            List<ProductSellRecord> records;
            this.getSell(out sell);
            this.getRecords(out records);
            ProductSellReportForm form = new ProductSellReportForm(sell, records);
            form.ShowDialog();
        }

        private void toolStripButton_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //add detail
        private void button_add_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Rows.Add();
            
            DataGridViewRow row = this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1];
            DataGridViewCell cell = row.Cells["totalPrice"];
            this.setCellDisable(cell);

            if (productTable != null && productTable.Rows.Count > 0)
            {
                int productID = (int)(productTable.Rows[0]["ID"]);
                (row.Cells["product"] as DataGridViewComboBoxCell).Value = productID;
                row.Cells["price"].Value = ProductDao.getInstance().FindPriceByID(productID);
                row.Cells["num"].Value = 1000;
            }
            else {
                row.Cells["price"].Value = 0;
                row.Cells["num"].Value = 0;
            }

            setSubTotalPrice(row.Index);
            setTotalPrice();
            this.resetSaveButton(true);
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
            this.resetSaveButton(true);
        }

        //for event: caculate total price
        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            this.resetSaveButton(true);
            if (e.Control.GetType().Equals(typeof(DataGridViewTextBoxEditingControl)))//cell为类TextBox时
            {
                e.CellStyle.BackColor = Color.FromName("window");
                DataGridViewTextBoxEditingControl editingControl = e.Control as DataGridViewTextBoxEditingControl;
                editingControl.TextChanged -= new EventHandler(editingControl_TextChanged);
                editingControl.TextChanged += new EventHandler(editingControl_TextChanged);
            }
            else if (e.Control.GetType().Equals(typeof(DataGridViewComboBoxEditingControl))) {
                e.CellStyle.BackColor = Color.FromName("window");
                ComboBox editingControl = e.Control as ComboBox;
                editingControl.SelectedIndexChanged -= new EventHandler(editingControl_SelectedIndexChanged);
                editingControl.SelectedIndexChanged += new EventHandler(editingControl_SelectedIndexChanged);
            } 
        }

        void editingControl_TextChanged(object sender, EventArgs e)
        {
            DataGridViewCell cell = this.dataGridView1.CurrentCell;
            String columnName = cell.OwningColumn.Name;
            if (columnName == "num" || columnName == "price")
            {
                setSubTotalPrice(cell.RowIndex);
                setTotalPrice();
            }
        }

        void editingControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox combox = sender as ComboBox;

            int selectIndex = combox.SelectedIndex;
            string text = combox.Text.ToString();
            if (selectIndex < 0 || text.Equals("System.Data.DataRowView"))
                return;

            DataGridViewComboBoxCell cell = this.dataGridView1.CurrentCell as DataGridViewComboBoxCell;
            
            //stone: not reasonable: this should be added
            //if (cell.Value == null)
            //    return;

            String columnName = cell.OwningColumn.Name;
            if (columnName == "product")
            {
                int productID = (int)(productTable.Rows[selectIndex]["ID"]);
                if (productID != (int)(cell.Value))
                {
                    this.dataGridView1.Rows[cell.RowIndex].Cells["price"].Value = ProductDao.getInstance().FindPriceByID(productID);
                    cell.Value = productID;

                    setSubTotalPrice(cell.RowIndex);
                    setTotalPrice();
                }
            }
        }

        private void textBox_serial_Validating(object sender, CancelEventArgs e)
        {
            string temp;
            getName(out temp);
        }

        private void resetSaveButton(bool value)
        {
            if (openMode < 2)
                this.toolStripButton_save.Enabled = value;
            else
                this.toolStripButton_save.Enabled = false;
        }

        private void Controls_TextChanged(object sender, EventArgs e)
        {
            resetSaveButton(true);
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            e.Handled = true;
            e.PaintBackground(e.CellBounds, true);
            e.PaintContent(e.CellBounds);
        }
    }
}