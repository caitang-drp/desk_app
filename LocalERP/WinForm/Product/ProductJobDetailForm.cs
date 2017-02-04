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
    public partial class ProductJobDetailForm : Form
    {
        //open mode       | 0:add 1:edit | 2:approval  | 3:partArrival | 4:finish
        //status          | 1:apply      | 2:approval  | 3:partArrival | 4:finish  
        private int openMode = 0;
        private int jobID = 0;
        private DataTable productTable = null;

        public delegate void ModifiedComplete();
        public event ModifiedComplete modifiedComplete;

        public ProductJobDetailForm(int mode, int id)
        {
            openMode = mode;
            jobID = id;
            if (openMode != 0)
            {
                openMode = ProductJobDao.getInstance().FindByID(jobID).Status;
            }
            InitializeComponent();
        }

        private void ElementOrderDetailForm_Load(object sender, EventArgs e)
        {
            DataGridViewComboBoxColumn productColumn = this.dataGridView1.Columns["product"] as DataGridViewComboBoxColumn;
            //this need test, if product is modified, this is not synchronise
            productTable = ProductDao.getInstance().FindList(null);
            productColumn.DataSource = productTable;
            productColumn.ValueMember = "ID";
            productColumn.DisplayMember = "name";

            initJobDetail();
        }
       
        private void initJobDetail()
        {
            if (openMode == 0)
            {
                switchMode(openMode);
                this.textBox_name.Text = "任务单" + DateTime.Now.ToShortDateString();
                return;
            }

            ProductJob job = ProductJobDao.getInstance().FindByID(jobID);

            this.textBox_name.Text = job.Name;
            this.dateTime_jobTime.Value = job.JobTime;
            this.textBox_comment.Text = job.Comment;

            this.dataGridView1.Rows.Clear();

            DataTable dataTable = ProductJobRecordDao.getInstance().FindList(jobID);
            foreach (DataRow dr in dataTable.Rows)
            {
                int index = this.dataGridView1.Rows.Add();
                this.dataGridView1.Rows[index].Cells["ID"].Value = dr["ID"];
                this.dataGridView1.Rows[index].Cells["product"].Value = (int)dr["productID"];
                this.dataGridView1.Rows[index].Cells["num"].Value = dr["num"];
                //this.dataGridView1.Rows[index].Cells["price"].Value = dr["price"];
                this.dataGridView1.Rows[index].Cells["arrivalNum"].Value = dr["arrivalNum"];
            }
            if (job != null)
                openMode = job.Status;
            switchMode(openMode);

            this.resetSaveButton(false);
        }

        private void switchMode(int mode)
        {
            switch (mode)
            {
                case 0:
                    this.label_status.Text = "新增";
                    this.initControlsEnable(true, false, false, true, true, true, true, true, false, false, false, false);
                    break;
                //apply
                case 1:
                    this.label_status.Text = ProductJob.statusEnum[0];
                    this.initControlsEnable(true, true, false, true, true, true, true, true, false, false, false, false);
                    break;
                //approval
                case 2:
                    this.label_status.Text = ProductJob.statusEnum[1];
                    this.initControlsEnable(false, false, true, false, false, false, false, false, true, true, true, true);
                    break;
                //part
                case 3:
                    this.label_status.Text = ProductJob.statusEnum[2];
                    this.initControlsEnable(false, false, true, false, false, false, false, false, true, true, true, true);
                    break;
                //finish
                case 4:
                    this.label_status.Text = ProductJob.statusEnum[3];
                    this.initControlsEnable(false, false, false, false, false, false, false, false, false, true, true, false);
                    break;
                default:
                    break;
            }
        }

        private void initControlsEnable(bool save, bool approval, bool finish, bool name, bool jobTime, bool comment,
            bool add, bool del, bool saveArrival, bool productReadonly, bool arrival, bool newArrival)
        {
            //this.toolStripButton_save.Enabled = save;
            this.toolStripButton_approval.Enabled = approval;
            this.toolStripButton_finish.Enabled = finish;
            this.textBox_name.Enabled = name;
            this.dateTime_jobTime.Enabled = jobTime;
            this.textBox_comment.Enabled = comment;
            this.toolStripButton_add.Enabled = add;
            this.toolStripButton_del.Enabled = del;
            this.toolStripButton_saveArrival.Enabled = saveArrival;

            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                if (productReadonly == true)
                {
                    setCellDisable(row.Cells["product"]);
                    setCellDisable(row.Cells["num"]);
                    //setCellDisable(row.Cells["price"]);
                }
                //setCellDisable(row.Cells["totalPrice"]);
                setCellDisable(row.Cells["arrivalNum"]);
            }
            this.dataGridView1.Columns["check"].Visible = !productReadonly;

            this.dataGridView1.Columns["arrivalNum"].Visible = arrival;
            this.dataGridView1.Columns["newArrivalNum"].Visible = newArrival;
        }

        private void setCellDisable(DataGridViewCell cell)
        {
            cell.ReadOnly = true;

            cell.Style.BackColor = System.Drawing.SystemColors.Control;
            cell.Style.ForeColor = System.Drawing.SystemColors.ControlDark;

            cell.Style.SelectionBackColor = System.Drawing.SystemColors.Control;
            cell.Style.SelectionForeColor = System.Drawing.SystemColors.ControlDark;
        }

        /// <summary>
        /// for get value from controls
        /// </summary>

        private bool getJob(out ProductJob job)
        {
            job = new ProductJob();
            job.ID = jobID;

            string name;
            if (this.getName(out name) == false)
                return false;

            job.Name = name;
            job.JobTime = this.dateTime_jobTime.Value;
            job.Comment = this.textBox_comment.Text;
            return true;
        }

        private bool getName(out string name)
        {
            if (string.IsNullOrEmpty(this.textBox_name.Text))
            {
                this.errorProvider1.SetError(this.textBox_name, "输入不能为空!");
                name = "";
                return false;
            }
            else
            {
                this.errorProvider1.SetError(this.textBox_name, string.Empty);
                name = this.textBox_name.Text;
                return true;
            }
        }

        private bool getRecords(out List<ProductJobRecord> records)
        {
            records = new List<ProductJobRecord>();

            int number = this.dataGridView1.RowCount;

            //double tempDouble;
            int tempInt;
            bool isInputCorrect = true;

            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                tempInt = Convert.ToInt32((row.Cells["product"] as DataGridViewComboBoxCell).Value);
                if (tempInt == 0)
                    continue;

                ProductJobRecord record = new ProductJobRecord();
                record.ProductID = tempInt;

                this.getInt(row.Cells["ID"], out tempInt);
                record.ID = tempInt;
                
                if (this.getInt(row.Cells["num"], out tempInt) == false)
                    isInputCorrect = false;
                record.Number = tempInt;

                this.getInt(row.Cells["arrivalNum"], out tempInt);
                record.ArrivalNum = tempInt;

                records.Add(record);
            }

            return isInputCorrect;
        }

        public bool getNewArrivals(out List<int> list)
        {
            list = new List<int>();

            bool isInputCorrect = true;

            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
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
            else
            {
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
        /// event
        /// </summary>
        
        //save
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            //for datagridview validate
            this.textBox_comment.Focus();

            List<ProductJobRecord> records;
            bool isRecordsCorrect = getRecords(out records);

            ProductJob job = null;
            bool isJobCorrect = getJob(out job);
            if (isRecordsCorrect == false || isJobCorrect == false)
                return;

            if (openMode == 0)
            {
                job.Status = 1;
                jobID = ProductJobDao.getInstance().Insert(job, records);
                MessageBox.Show("增加任务成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (openMode == 1)
            {
                ProductJobDao.getInstance().Update(job, records);
                MessageBox.Show("修改任务成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            //this is important
            openMode = 1;
            this.initJobDetail();

            if (modifiedComplete != null)
                modifiedComplete.Invoke();
        }
        
        //approval
        private void toolStripButton_approval_Click(object sender, EventArgs e)
        {
            if (this.toolStripButton_save.Enabled == true) {
                MessageBox.Show("请先保存任务单,再领用配件!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("领用配件后，将从配件库存里自动扣除配件，且该加工任务不能修改，是否设置已领配件？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK)
                return;

            this.Enabled = false;
            List<ProductJobRecord> records;
            this.getRecords(out records);

            ProductJob job;
            this.getJob(out job);

            //no reasonable
            for (int i = 0; i < records.Count; i++)
            {
                int productID = records[i].ProductID;
                List<Element> elements = ElementDao.getInstance().FindListByProductID(productID);
                foreach (Element element in elements)
                {
                    if (element.Num < records[i].Number) {
                        MessageBox.Show("配件库存不足, 领用配件失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Enabled = true;
                        return;
                    }
                }
            }

            for (int i = 0; i < records.Count; i++)
            {
                int productID = records[i].ProductID;
                List<Element> elements = ElementDao.getInstance().FindListByProductID(productID);
                foreach (Element element in elements) {
                    int totalNum = element.Num - records[i].Number;
                    ElementFlow flow = new ElementFlow(element.ID, element.Name, -1, records[i].Number, totalNum, string.Format("配件消耗-加工任务ID:{0}[{1}]", job.ID, job.Name), DateTime.Now, 4);
                    ElementFlowDao.getInstance().Insert(flow);
                    ElementDao.getInstance().UpdateNum(element.ID, totalNum);
                }   
            }

            this.Enabled = true;

            ProductJobDao.getInstance().UpdateStatus(jobID, 2);
            openMode = 2;
            this.switchMode(2);
            if (modifiedComplete != null)
                modifiedComplete.Invoke();
        }

        //finish
        private void toolStripButton_finish_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                if (!string.IsNullOrEmpty(row.Cells["newArrivalNum"].EditedFormattedValue as string))
                {
                    MessageBox.Show("你有未保存的新增到货数, 请先保存.", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (MessageBox.Show("任务结束后，将不能再添加完成数，是否结束任务?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK)
                return;

            ProductJobDao.getInstance().UpdateStatus(jobID, 4);
            openMode = 4;
            this.switchMode(4);
            if (modifiedComplete != null)
                modifiedComplete.Invoke();
        }

        //cancel
        private void toolStripButton_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //add record
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Rows.Add();

            DataGridViewRow row = this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1];
            
            if (productTable != null && productTable.Rows.Count > 0)
            {
                int productID = (int)(productTable.Rows[0]["ID"]);
                (row.Cells["product"] as DataGridViewComboBoxCell).Value = productID;
                row.Cells["num"].Value = 1000;
            }
            this.resetSaveButton(true);
        }

        //del record
        private void toolStripButton_del_Click(object sender, EventArgs e)
        {
            List<int> rowsIndex = this.getSelectRows();
            if (rowsIndex.Count <= 0)
            {
                MessageBox.Show("请选择项", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            for (int i = rowsIndex.Count - 1; i >= 0; i--)
            {
                this.dataGridView1.Rows.RemoveAt(rowsIndex[i]);
            }
            this.resetSaveButton(true);
        }

        //save arrival num
        private void toolStripButton_saveArrival_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
                dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells["arrivalNum"];

            List<ProductJobRecord> records;
            this.getRecords(out records);
            List<int> newArrivalNums;
            if (this.getNewArrivals(out newArrivalNums) == false)
                return;
            ProductJob job;
            this.getJob(out job);

            this.Enabled = false;

            for (int i = 0; i < records.Count; i++)
            {
                int newArrivalNum = records[i].ArrivalNum + newArrivalNums[i];
                if (records[i].Number < newArrivalNum)
                {
                    MessageBox.Show("新增完成数超过未完成数, 保存失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Enabled = true;
                    return;
                }
            }

            int zeroNum = 0;
            int finishNum = 0;

            for (int i = 0; i < records.Count; i++)
            {
                int recordArrivalNum = records[i].ArrivalNum + newArrivalNums[i];
                if (records[i].Number == recordArrivalNum)
                    finishNum++;

                if (newArrivalNums[i] == 0)
                {
                    zeroNum++;
                    continue;
                }

                Product product = ProductDao.getInstance().FindByID(records[i].ProductID);

                ProductFlow flow = new ProductFlow(records[i].ProductID, product.Name, 1, newArrivalNums[i], product.Num + newArrivalNums[i], string.Format("成品完成-加工任务ID:{0}[{1}]", job.ID, job.Name), DateTime.Now, 3);
                ProductFlowDao.getInstance().Insert(flow);

                ProductJobRecordDao.getInstance().UpdateArrivalNum(records[i].ID, recordArrivalNum);
                ProductDao.getInstance().UpdateNum(product.ID, product.Num + newArrivalNums[i]);
            }

            if (finishNum == records.Count)
            {
                ProductJobDao.getInstance().UpdateStatus(jobID, 4);
                openMode = 4;
            }
            else if (zeroNum < records.Count)
            {
                ProductJobDao.getInstance().UpdateStatus(jobID, 3);
                openMode = 3;
            }
            this.Enabled = true;

            this.initJobDetail();

            MessageBox.Show("保存新增到货成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (modifiedComplete != null)
                modifiedComplete.Invoke();
        }

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
        }

        void editingControl_TextChanged(object sender, EventArgs e)
        {
            DataGridViewCell cell = this.dataGridView1.CurrentCell;
            String columnName = cell.OwningColumn.Name;
            if (columnName == "num")
            {
                int temp;
                this.getInt(cell, out temp);
            }
        }

        private void textBox_serial_Validating(object sender, CancelEventArgs e)
        {
            string temp;
            getName(out temp);
        }


        private void Controls_Changed(object sender, EventArgs e)
        {
            resetSaveButton(true);
        }

        private void resetSaveButton(bool value){
            if (openMode < 2)
                this.toolStripButton_save.Enabled = value;
            else
                this.toolStripButton_save.Enabled = false;
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            e.Handled = true;
            e.PaintBackground(e.CellBounds, true);
            e.PaintContent(e.CellBounds);
        }

    }
}