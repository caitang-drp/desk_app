using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LocalERP.DataAccess.DataDAO;
using LocalERP.DataAccess.Data;

namespace LocalERP.WinForm
{
    public partial class ElementOrderDetailForm : Form
    {
        //open mode       | 0:add 1:edit | 2:approval  | 3:partArrival | 4:arrival
        //status          | 1:apply      | 2:approval  | 3:partArrival | 4:arrival  
        private int openMode = 0;
        private int orderID = 0;
        private DataTable elementTable = null;

        public delegate void ModifiedComplete();
        public event ModifiedComplete modifiedComplete;

        public ElementOrderDetailForm(int mode, int id)
        {
            orderID = id;
            openMode = mode;
            if (openMode != 0) {
                openMode = ElementOrderDao.getInstance().FindByID(orderID).Status;
            }
            InitializeComponent();
        }

        /// <summary>
        /// for init
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ElementOrderDetailForm_Load(object sender, EventArgs e)
        {
            dataGridView2.Rows.Add("总价合计/元:", "");

            DataGridViewComboBoxColumn elementColumn = this.dataGridView1.Columns["element"] as DataGridViewComboBoxColumn;
            elementTable = ElementDao.getInstance().FindList();
            elementColumn.DataSource = elementTable;
            elementColumn.ValueMember = "ID";
            elementColumn.DisplayMember = "name";

            initOrderDetail();
        }

        private void initOrderDetail()
        {
            if (openMode == 0)
            {
                switchMode(openMode);
                this.textBox_serial.Text = "订单" + DateTime.Now.ToShortDateString();
                return;
            }

            ElementOrder order = ElementOrderDao.getInstance().FindByID(orderID);

            this.textBox_serial.Text = order.Serial;
            this.dateTime_orderTime.Value = order.OrderTime;
            this.textBox_comment.Text = order.Comment;

            this.dataGridView1.Rows.Clear();

            DataTable dataTable = ElementOrderRecordDao.getInstance().FindList(order.ID);
            foreach (DataRow dr in dataTable.Rows)
            {
                int index = this.dataGridView1.Rows.Add();
                this.dataGridView1.Rows[index].Cells["ID"].Value = dr["ID"];
                this.dataGridView1.Rows[index].Cells["element"].Value = (int)dr["elementID"];
                this.dataGridView1.Rows[index].Cells["num"].Value = dr["num"];
                this.dataGridView1.Rows[index].Cells["price"].Value = dr["price"];
                this.dataGridView1.Rows[index].Cells["arrivalNum"].Value = dr["arrivalNum"];

                this.setSubTotalPrice(index);
            }

            this.setTotalPrice();

            if (order != null)
                openMode = order.Status;
            switchMode(openMode);

            this.resetSaveButton(false);
        }

        private void switchMode(int mode) { 
            switch(mode){
                case 0:
                    this.label_status.Text = "新增";
                    this.initControlsEnable(true, false, false, false, true, true, true, false, false, false, false);
                    break;
                case 1:
                    this.label_status.Text = ElementOrder.statusEnum[0];
                    this.initControlsEnable(true, true, false, false, true, true, true, false, false, false, false);
                    break;
                case 2:
                    this.label_status.Text = ElementOrder.statusEnum[1];
                    this.initControlsEnable(false, false, true, true, false, false, false, true, true, true, true);
                    break;
                case 3:
                    this.label_status.Text = ElementOrder.statusEnum[2];
                    this.initControlsEnable(false, false, true, true, false, false, false, true, true, true, true);
                    break;
                case 4:
                    this.label_status.Text = ElementOrder.statusEnum[3];
                    this.initControlsEnable(false, false, false, true, false, false, false, false, true, true, false);
                    break;
                default:
                    break;
            }
        }

        private void initControlsEnable(bool save, bool approval, bool finish, bool print, bool basicInfo,
            bool add, bool del, bool saveArrival, bool elementReadonly, bool arrival, bool newArrival)
        {
            //this.toolStripButton_save.Enabled = save;
            this.toolStripButton_approval.Enabled = approval;
            this.toolStripButton_finish.Enabled = finish;

            this.toolStripButton_print.Enabled = print;

            this.textBox_serial.Enabled = basicInfo;
            this.dateTime_orderTime.Enabled = basicInfo;
            this.textBox_comment.Enabled = basicInfo;
            this.toolStripButton_add.Enabled = add;
            this.toolStripButton_del.Enabled = del;
            this.toolStripButton_saveArrival.Enabled = saveArrival;

            foreach (DataGridViewRow row in this.dataGridView1.Rows) {
                if (elementReadonly == true) {
                    setCellDisable(row.Cells["element"]);
                    setCellDisable(row.Cells["num"]);
                    setCellDisable(row.Cells["price"]);
                    //setCellDisable(row.Cells["check"]);
                }
                setCellDisable(row.Cells["totalPrice"]);
                setCellDisable(row.Cells["arrivalNum"]);
            }

            this.dataGridView1.Columns["check"].Visible = !elementReadonly;

            this.dataGridView1.Columns["arrivalNum"].Visible = arrival;
            this.dataGridView1.Columns["newArrivalNum"].Visible = newArrival;
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

        private bool getOrder(out ElementOrder order) {
            order = new ElementOrder();
            order.ID = orderID;
            
            string name;
            if (this.getName(out name) == false)
                return false;
            
            order.Serial = name;
            order.OrderTime = this.dateTime_orderTime.Value;
            order.Comment = this.textBox_comment.Text;
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

        private bool getRecords(out List<ElementOrderRecord> records)
        {
            records = new List<ElementOrderRecord>();

            int number = this.dataGridView1.RowCount;

            double tempDouble;
            int tempInt;
            bool isInputCorrect = true;

            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                DataGridViewComboBoxCell cell = row.Cells["element"] as DataGridViewComboBoxCell;
                tempInt = Convert.ToInt32(cell.Value);
                if (tempInt == 0)
                    continue;

                ElementOrderRecord record = new ElementOrderRecord();
                record.ElementID = tempInt;

                this.getInt(row.Cells["ID"], out tempInt);
                record.ID = tempInt;

                record.Name = cell.FormattedValue.ToString();

                if (this.getInt(row.Cells["num"], out tempInt) == false)
                    isInputCorrect = false;
                record.Number = tempInt;

                this.getInt(row.Cells["arrivalNum"], out tempInt);
                record.ArrivalNum = tempInt;

                if (this.getDouble(row.Cells["price"], out tempDouble) == false)
                    isInputCorrect = false;
                record.Price = tempDouble;

                records.Add(record);
            }

            return isInputCorrect;
        }

        public bool getNewArrivals(out List<int> list) {
            list = new List<int>();

            bool isInputCorrect = true;

            foreach (DataGridViewRow row in this.dataGridView1.Rows) {
                int num;
                if (this.getInt(row.Cells["newArrivalNum"], out num) == false)
                    isInputCorrect = false;
                list.Add(num);
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

            List<ElementOrderRecord> records;
            bool isRecordsCorrect = getRecords(out records);

            ElementOrder order;
            bool isOrderCorrect = getOrder(out order);
            if( isRecordsCorrect == false || isOrderCorrect == false)
                return;
            
            if (openMode == 0)
            {
                order.Status = 1;
                orderID = ElementOrderDao.getInstance().Insert(order, records);
                MessageBox.Show("增加订单成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (openMode == 1)
            {
                ElementOrderDao.getInstance().Update(order, records);
                MessageBox.Show("保存订单成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            //so important: if edit ,it should be refresh also, because edit will del exist item and add new item
            openMode = 1;
            this.initOrderDetail();

            if (modifiedComplete != null)
                modifiedComplete.Invoke();
        }

        private void toolStripButton_approval_Click(object sender, EventArgs e)
        {
            if (this.toolStripButton_save.Enabled == true)
            {
                MessageBox.Show("请先保存订单,再下单!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("下单后，该订单不能修改，是否下单？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK)
                return;

            ElementOrderDao.getInstance().UpdateStatus(orderID, 2);
            openMode = 2;
            this.switchMode(2);
            if (modifiedComplete != null)
                modifiedComplete.Invoke();
        }


        private void toolStripButton_finish_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.dataGridView1.Rows) {
                if (!string.IsNullOrEmpty(row.Cells["newArrivalNum"].EditedFormattedValue as string))
                {
                    MessageBox.Show("你有未保存的新增到货数, 请先保存.", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    return;
                }
            }
            if (MessageBox.Show("订单结束后，将不能再添加到货数, 是否结束订单?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK)
                return;

            ElementOrderDao.getInstance().UpdateStatus(orderID, 4);
            openMode = 4;
            this.switchMode(4);
            if (modifiedComplete != null)
                modifiedComplete.Invoke();
        }

        private void toolStripButton_print_Click(object sender, EventArgs e)
        {
            ElementOrder order;
            this.getOrder(out order);
            List<ElementOrderRecord> records;
            this.getRecords(out records);

            ElementOrderReportForm form = new ElementOrderReportForm(order, records);
            form.ShowDialog();
        }

        private void toolStripButton_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton_add_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Rows.Add();
            
            DataGridViewRow row = this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1];
            DataGridViewCell cell = row.Cells["totalPrice"];
            this.setCellDisable(cell);

            if (elementTable != null && elementTable.Rows.Count > 0)
            {
                int elementID = (int)(elementTable.Rows[0]["ID"]);
                (row.Cells["element"] as DataGridViewComboBoxCell).Value = elementID;
                row.Cells["price"].Value = ElementDao.getInstance().FindPriceByID(elementID);
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

        private void toolStripButton_saveArrival_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
                dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells["totalPrice"];

            List<ElementOrderRecord> records;
            this.getRecords(out records);
            List<int> newArrivalNums;
            if (this.getNewArrivals(out newArrivalNums) == false)
                return;
            ElementOrder order;
            this.getOrder(out order);

            this.Enabled = false;

            for (int i = 0; i < records.Count; i++)
            {
                int newArrivalNum = records[i].ArrivalNum + newArrivalNums[i];
                if (records[i].Number < newArrivalNum)
                {
                    MessageBox.Show("新增到货数超过未到货数, 保存失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Enabled = true;
                    return;
                }
            }

            int zeroNum = 0;
            int finishNum = 0;

            for (int i = 0; i < records.Count; i++)
            {
                int recordArrNum = records[i].ArrivalNum + newArrivalNums[i];
                if (records[i].Number == recordArrNum)
                    finishNum++;
                
                if (newArrivalNums[i] == 0){
                    zeroNum ++;
                    continue;
                }

                Element element = ElementDao.getInstance().FindByID(records[i].ElementID);

                ElementFlow flow = new ElementFlow(records[i].ElementID, element.Name,  1, newArrivalNums[i], element.Num + newArrivalNums[i], string.Format("配件到货-订单ID:{0}[{1}]", order.ID, order.Serial), DateTime.Now, 3);
                ElementFlowDao.getInstance().Insert(flow);

                ElementOrderRecordDao.getInstance().UpdateArrivalNum(records[i].ID, recordArrNum);
                ElementDao.getInstance().UpdateNum(element.ID, element.Num + newArrivalNums[i]);
            }


            if (finishNum == records.Count)
            {
                ElementOrderDao.getInstance().UpdateStatus(orderID, 4);
                this.openMode = 4;
            }
            else if (zeroNum < records.Count)
            {
                ElementOrderDao.getInstance().UpdateStatus(orderID, 3);
                this.openMode = 3;
            }
            this.Enabled = true;

            initOrderDetail();

            MessageBox.Show("保存新增到货成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (modifiedComplete != null)
                modifiedComplete.Invoke();
        }

        //del
        private void toolStripButton_del_Click(object sender, EventArgs e)
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
            if (columnName == "element")
            {
                int elementID = (int)(elementTable.Rows[selectIndex]["ID"]);
                if (elementID != (int)(cell.Value))
                {
                    this.dataGridView1.Rows[cell.RowIndex].Cells["price"].Value = ElementDao.getInstance().FindPriceByID(elementID);
                    cell.Value = elementID;

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