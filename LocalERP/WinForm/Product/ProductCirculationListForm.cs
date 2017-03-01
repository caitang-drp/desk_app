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
    public partial class ProductCirculationListForm : MyDockContent
    {
        private MainForm mainForm;
        private int circulationType;

        private ProductCirculationDao cirDao;

        public ProductCirculationListForm(MainForm mf, int type, string title, ProductCirculationDao cirDao)
        {
            InitializeComponent();

            this.mainForm = mf;
            this.circulationType = type;
            this.Text = title;

            DateTime dateTime = DateTime.Now;
            this.dateTimePicker3.Value = dateTime.AddMonths(-1);

            this.cirDao = cirDao;
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
            this.dataGridView1.Columns["realTotal"].Visible = false;
            this.textBox_customer.Visible = false;
        }

        private void initList()
        {
            try
            {
                DataTable dataTable = cirDao.FindList(circulationType, this.dateTimePicker3.Value, this.dateTimePicker4.Value.AddDays(1), (int)(comboBox1.SelectedValue), textBox_customer.Text.Trim());
                this.dataGridView1.Rows.Clear();
                foreach (DataRow dr in dataTable.Rows)
                {
                    int index = this.dataGridView1.Rows.Add();
                    this.dataGridView1.Rows[index].Cells["ID"].Value = dr["ID"];
                    this.dataGridView1.Rows[index].Cells["name"].Value = dr["code"];
                    int type = (int)(dr["type"]);
                    this.dataGridView1.Rows[index].Cells["type"].Value = ProductCirculation.CirculationTypeConfs[type - 1].name;
                    this.dataGridView1.Rows[index].Cells["typeValue"].Value = type;

                    this.dataGridView1.Rows[index].Cells["realTotal"].Value = double.Parse(dr["realTotal"].ToString()).ToString("0.00");

                    int status = (int)(dr["status"]);
                    this.dataGridView1.Rows[index].Cells["status"].Value = ProductCirculation.circulationStatusContext[status - 1];
                    if (status == 1)
                    {
                        this.dataGridView1.Rows[index].Cells["status"].Style.ForeColor = Color.Red;
                        this.dataGridView1.Rows[index].Cells["status"].Style.SelectionForeColor = Color.Red;
                    }
                    else
                    {
                        this.dataGridView1.Rows[index].Cells["status"].Style.ForeColor = Color.Black;
                        this.dataGridView1.Rows[index].Cells["status"].Style.SelectionForeColor = Color.Black;
                    }

                    if (circulationType < 3)
                        this.dataGridView1.Rows[index].Cells["customer"].Value = dr["name"];


                    this.dataGridView1.Rows[index].Cells["sellTime"].Value = dr["circulationTime"];                    
                }
            }
            catch(Exception ex) {
                //MessageBox.Show("查询错误, 请输入正确的条件.", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    if (cirDao.FindByID(list[i]).Status > 1) {
                        MessageBox.Show(string.Format("ID为{0}的单据已经审核, 无法删除!", list[i]), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        initList();
                        return;
                    }
                    cirDao.DeleteByID(list[i]);
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

        //
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
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
                case (int)ProductCirculation.CirculationType.easy:
                    formString = DataUtility.EASY;
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
                case (int)ProductCirculation.CirculationType.libLoss:
                    formString = DataUtility.LIB_LOSS;
                    break;
                default:
                    break;

            }
            mainForm.setForm(formString, 1, (int)this.dataGridView1.SelectedRows[0].Cells["ID"].Value);
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
    }
}