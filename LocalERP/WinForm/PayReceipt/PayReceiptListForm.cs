using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using LocalERP.DataAccess.DataDAO;
using LocalERP.DataAccess.Data;
using System.Collections;
using LocalERP.DataAccess.Utility;

namespace LocalERP.WinForm
{
    public partial class PayReceiptListForm : MyDockContent
    {
        private MainForm mainForm;
        private int circulationType;

        public PayReceiptListForm(MainForm mf, int type, string title)
        {
            InitializeComponent();

            this.mainForm = mf;
            this.circulationType = type;
            this.Text = title;

            DateTime dateTime = DateTime.Now;
            this.dateTimePicker3.Value = dateTime.AddMonths(-1);
        }

        private void ProductCirculationListForm_Load(object sender, EventArgs e)
        {
            initCustomerCombox();
            initList();
            this.dataGridView1.Columns["check"].ReadOnly = false;
        }

        private void initCustomerCombox() {

            ArrayList arrayList = new ArrayList();

            arrayList.Add(new DictionaryEntry(0, "-全部-"));
            arrayList.Add(new DictionaryEntry(1, ProductCirculation.circulationStatusContext[0]));
            arrayList.Add(new DictionaryEntry(4, ProductCirculation.circulationStatusContext[3]));

            this.comboBox1.DataSource = arrayList;
            this.comboBox1.ValueMember = "Key";
            this.comboBox1.DisplayMember = "Value";
        }

        public void hideControls() {
            this.label8.Visible = false;
            this.dataGridView1.Columns["customer"].Visible = false;
            this.dataGridView1.Columns["pay"].Visible = false;
            this.textBox_customer.Visible = false;
        }

        private void initList()
        {
            try
            {
                DataTable dataTable = PayReceiptDao.getInstance().FindList();
                this.dataGridView1.Rows.Clear();
                foreach (DataRow dr in dataTable.Rows)
                {
                    int index = this.dataGridView1.Rows.Add();
                    this.dataGridView1.Rows[index].Cells["ID"].Value = dr["ID"];
                    this.dataGridView1.Rows[index].Cells["serial"].Value = dr["serial"];
                    this.dataGridView1.Rows[index].Cells["bill_time"].Value = dr["bill_time"];                    
                    this.dataGridView1.Rows[index].Cells["comment"].Value = dr["comment"];                    
                    this.dataGridView1.Rows[index].Cells["amount"].Value = dr["amount"];                    
                    this.dataGridView1.Rows[index].Cells["bill_time"].Value = dr["bill_time"];                    
                    this.dataGridView1.Rows[index].Cells["bill_type"].Value = new PayReceipt().get_bill_type_name((int)dr["bill_type"]);                    
                    this.dataGridView1.Rows[index].Cells["custom"].Value = CustomerDao.getInstance().FindByID(Convert.ToInt32(dr["customer_id"])).Name;                    
                }
            }
            catch(Exception ex) {
                MessageBox.Show("查询错误, 请输入正确的条件.", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// event
        /// </summary>

        public override void refresh()
        {
            this.initList();
        }

        
        // TODO, add by xdz
        // TODO, add by xdz
        // TODO, add by xdz
        // TODO, add by xdz
        // TODO, add by xdz
        // TODO, add by xdz
        // 不能删除操作，以为每条单据都对应着客户的欠款情况，删除就乱了

        //del
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            List<int> list = this.dataGridView1.getSelectIDs("ID", "check");
            if (list == null || list.Count <= 0)
            {
                MessageBox.Show("请选择单据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            StringBuilder ids = new StringBuilder();
            for (int ii = 0; ii < list.Count; ii++)
            {
                ids.Append(list[ii]);
                ids.Append(" ");
            }
            if (MessageBox.Show(string.Format("是否删除流水号为{0}的单据?", ids.ToString()), "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    //modified by stone: 这里应该是PayReceiptDao，不是ProductCirculationDao
                    if (ProductStainlessCirculationDao.getInstance().FindByID(list[i]).Status > 1)
                    {
                        MessageBox.Show(string.Format("ID为{0}的单据已经审核, 无法删除!", list[i]), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        initList();
                        return;
                    }
                    ProductStainlessCirculationDao.getInstance().DeleteByID(list[i]);
                }
                initList();
                MessageBox.Show("删除单据成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //edit
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            List<int> list = this.dataGridView1.getSelectIDs("ID", "check");
            if (list == null || list.Count <= 0)
            {
                MessageBox.Show("请选择单据!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            mainForm.setForm(DataUtility.PURCHASE, 1, list[0]);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
        /*
            if (this.dataGridView1.SelectedRows == null || this.dataGridView1.SelectedRows.Count <= 0)
            {
                MessageBox.Show("请选择任务!");
                return;
            }
            int typeValue = (int)this.dataGridView1.SelectedRows[0].Cells["typeValue"].Value;
            string formString = "";
            switch (typeValue) { 
                case (int)ProductCirculation.CirculationType.purchase:
                    formString = DataUtility.PURCHASE;
                    break;
                case (int)ProductCirculation.CirculationType.purchaseBack:
                    formString = DataUtility.PURCHASE_BACK;
                    break;
                case (int)ProductCirculation.CirculationType.sell:
                    formString = DataUtility.SELL;
                    break;
                case (int)ProductCirculation.CirculationType.sellBack:
                    formString = DataUtility.SELL_BACK;
                    break;
                case (int)ProductCirculation.CirculationType.libOverflow:
                    formString = DataUtility.LIB_OVERFLOW;
                    break;
                case (int)ProductCirculation.CirculationType.libLoss:e
                    formString = DataUtility.LIB_LOSS;
                    break;
                default:
                    break;

            }
            mainForm.setForm(formString, 1, (int)this.dataGridView1.SelectedRows[0].Cells["ID"].Value);
            */
        }

        private void button1_Click(object sender, EventArgs e)
        {
            initList();
        }

        private void toolStripButton_selectAll_Click(object sender, EventArgs e)
        {
            selectAll(true);
        }

        private void toolStripButton_cancelAll_Click(object sender, EventArgs e)
        {
            selectAll(false);
        }

        private void selectAll(bool value) {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                DataGridViewCheckBoxCell cell = (row.Cells["check"] as DataGridViewCheckBoxCell);
                if (cell.IsInEditMode == true)
                    cell.EditingCellFormattedValue = value;
                cell.Value = value;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            refresh();
        }
    }
}