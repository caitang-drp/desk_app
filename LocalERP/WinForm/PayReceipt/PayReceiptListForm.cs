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
using LocalERP.WinForm.Utility;

namespace LocalERP.WinForm
{
    public partial class PayReceiptListForm : MyDockContent
    {
        private MainForm mainForm;

        public PayReceiptListForm(MainForm mf)
        {
            InitializeComponent();

            this.mainForm = mf;

            DateTime dateTime = DateTime.Now;
            this.dateTimePicker3.Value = dateTime.AddMonths(-1);
        }

        private void PayReceiptListForm_Load(object sender, EventArgs e)
        {
            initCustomerCombox();
            initList();
            this.dataGridView1.Columns["check"].ReadOnly = false;
        }

        private void initCustomerCombox() {

            ArrayList arrayList = new ArrayList();

            arrayList.Add(new DictionaryEntry(0, "-全部-"));
            arrayList.Add(new DictionaryEntry(1, PayReceipt.statusContext[0]));
            arrayList.Add(new DictionaryEntry(4, PayReceipt.statusContext[3]));

            this.comboBox1.DataSource = arrayList;
            this.comboBox1.ValueMember = "Key";
            this.comboBox1.DisplayMember = "Value";
        }

        private void initList()
        {
            try
            {
                DataTable dataTable = PayReceiptDao.getInstance().FindList(this.dateTimePicker3.Value, this.dateTimePicker4.Value.AddDays(1), (int)(comboBox1.SelectedValue), textBox_customer.Text.Trim());
                this.dataGridView1.Rows.Clear();
                foreach (DataRow dr in dataTable.Rows)
                {
                    int index = this.dataGridView1.Rows.Add();
                    this.dataGridView1.Rows[index].Cells["ID"].Value = dr["ID"];
                    this.dataGridView1.Rows[index].Cells["code"].Value = dr["serial"];
                    this.dataGridView1.Rows[index].Cells["customer"].Value = dr["name"];
                    int type = (int)(dr["bill_type"]);
                    this.dataGridView1.Rows[index].Cells["type"].Value = PayReceipt.PayReceiptTypeConfs[type - 1].name;
                    this.dataGridView1.Rows[index].Cells["typeValue"].Value = type;

                    double sum = 0;
                    ValidateUtility.getDouble(dr, "amount", out sum);
                    if (type == 1 || type == 4 || type == 5)
                        ControlUtility.setCellWithColor(dataGridView1.Rows[index].Cells["sum"], Color.Green, string.Format("-{0:0.00}", sum));
                    else if (type == 2 || type == 3 || type==6)
                        ControlUtility.setCellWithColor(dataGridView1.Rows[index].Cells["sum"], Color.Red, string.Format("+{0:0.00}", sum));

                    int status = (int)(dr["status"]);
                    if (status == 1)
                        ControlUtility.setCellWithColor(this.dataGridView1.Rows[index].Cells["status"], Color.Red, PayReceipt.statusContext[status - 1]);
                    else
                        ControlUtility.setCellWithColor(this.dataGridView1.Rows[index].Cells["status"], Color.Black, PayReceipt.statusContext[status - 1]);

                    this.dataGridView1.Rows[index].Cells["time"].Value = dr["bill_time"];

                }
            }
            catch (Exception ex)
            {
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

        //del
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            List<int> list = this.dataGridView1.getSelectIDs("ID", "check");
            if (list == null || list.Count <= 0)
            {
                MessageBox.Show("请选择款单", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            StringBuilder ids = new StringBuilder();
            for (int ii = 0; ii < list.Count; ii++)
            {
                ids.Append(list[ii]);
                ids.Append(" ");
            }
            if (MessageBox.Show(string.Format("是否删除流水号为{0}的款单?", ids.ToString()), "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (PayReceiptDao.getInstance().FindByID(list[i]).status > 1) {
                        MessageBox.Show(string.Format("ID为{0}的款单已经审核, 无法删除!", list[i]), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        initList();
                        return;
                    }
                    PayReceiptDao.getInstance().Delete(list[i]);
                }
                initList();
                MessageBox.Show("删除款单成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //edit
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            List<DataGridViewRow> rows = this.dataGridView1.getCheckRows("check");
            if (rows == null || rows.Count <= 0)
            {
                MessageBox.Show("请选择款单!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            editPayReceipt(rows[0]);   
        }

        //
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGridView1.SelectedRows == null || this.dataGridView1.SelectedRows.Count <= 0)
            {
                MessageBox.Show("请选择款单!");
                return;
            }
            editPayReceipt(this.dataGridView1.SelectedRows[0]);
        }

        private void editPayReceipt(DataGridViewRow row) {
            int typeValue = (int)row.Cells["typeValue"].Value;
            int id = (int)row.Cells["ID"].Value;
            string formString = "";
            switch (typeValue)
            {
                case (int)PayReceipt.BillType.BuyPay:
                    formString = DataUtility.CASH_PAY;
                    break;
                case (int)PayReceipt.BillType.BuyRefund:
                    formString = DataUtility.CASH_PAY_REFUND;
                    break;
                case (int)PayReceipt.BillType.SellReceipt:
                    formString = DataUtility.CASH_RECEIPT;
                    break;
                case (int)PayReceipt.BillType.SellRefund:
                    formString = DataUtility.CASH_RECEIPT_REFUND;
                    break;
                case (int)PayReceipt.BillType.OtherPay:
                    formString = DataUtility.CASH_OTHER_PAY;
                    break;
                case (int)PayReceipt.BillType.OtherReceipt:
                    formString = DataUtility.CASH_OTHER_RECEIPT;
                    break;
                default:
                    break;

            }
            mainForm.setForm(formString, 1, id);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //initList();
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
    }
}