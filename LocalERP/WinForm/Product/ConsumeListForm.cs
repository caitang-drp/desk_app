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
    public partial class ConsumeListForm : LookupAndNotifyDockContent
    {
        private MainForm mainForm;

        private int openMode = 1;

        public ConsumeListForm(MainForm mf, int openMode)
        {
            InitializeComponent();

            this.mainForm = mf;
            this.openMode = openMode;

            if (openMode == 0) {
                this.toolStrip1.Visible = false;
                this.label_tip.Text = "双击选中消费记录";
                this.dataGridView1.Columns["check"].Visible = false;
            }

            DateTime dateTime = DateTime.Now;
            this.dateTimePicker3.Value = dateTime.AddMonths(-1);
        }

        private void ConsumeListForm_Load(object sender, EventArgs e)
        {
            initCustomerCombox();
            initList();
            
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

        
        private void initList()
        {
            List<Consume> list = ConsumeDao.getInstance().FindList(0);
            this.dataGridView1.Rows.Clear();

            for(int i=0;i<list.Count;i++) {
                Consume consume = list[i];
                this.dataGridView1.Rows.Add();
                this.dataGridView1.Rows[i].Cells["ID"].Value = consume.ID;
                this.dataGridView1.Rows[i].Cells["name"].Value = consume.Code;
                this.dataGridView1.Rows[i].Cells["customer"].Value = consume.CardID;
                /*int type = (int)(dr["type"]);
                this.dataGridView1.Rows[index].Cells["type"].Value = ProductCirculation.CirculationTypeConfs[type - 1].name;
                this.dataGridView1.Rows[index].Cells["typeValue"].Value = type;*/

                this.dataGridView1.Rows[i].Cells["num"].Value = consume.Number;

                int leftNum = 0;

                int status = consume.Status;
                this.dataGridView1.Rows[i].Cells["status"].Value = Consume.consumeStatusContext[status - 1];
                if (status == 1)
                {
                    this.dataGridView1.Rows[i].Cells["status"].Style.ForeColor = Color.Red;
                    this.dataGridView1.Rows[i].Cells["status"].Style.SelectionForeColor = Color.Red;

                    leftNum = consume.Card.LeftNumber;
                }
                else
                {
                    this.dataGridView1.Rows[i].Cells["status"].Style.ForeColor = Color.Black;
                    this.dataGridView1.Rows[i].Cells["status"].Style.SelectionForeColor = Color.Black;

                    leftNum = consume.LeftNumber;
                }
                this.dataGridView1.Rows[i].Cells["cardInfo"].Value = consume.Card.getInfo(leftNum);

                this.dataGridView1.Rows[i].Cells["sellTime"].Value = consume.ConsumeTime.ToString("yyyy-MM-dd HH:mm:ss");
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
            if (MessageBox.Show(string.Format("是否删除ID号为{0}的消费?", ids.ToString()), "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (ConsumeDao.getInstance().FindByID(list[i]).Status > 1)
                    {
                        MessageBox.Show(string.Format("ID为{0}的消费已经审核, 无法删除!", list[i]), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        initList();
                        return;
                    }
                    ConsumeDao.getInstance().Delete(list[i]);
                }
                initList();
                MessageBox.Show("删除消费成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //edit
        private void toolStripButton3_Click(object sender, EventArgs e)
        {/*
            List<DataGridViewRow> rows = this.dataGridView1.getCheckRows("check");
            if (rows == null || rows.Count <= 0)
            {
                MessageBox.Show("请选择货单!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            editCirculation(rows[0]);     */       
        }

        //
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {


            if (e.RowIndex < 0)
                return;

            if (this.dataGridView1.Rows[e.RowIndex].Cells["ID"].Value == null)
                return;

            if (this.dataGridView1.SelectedRows == null || this.dataGridView1.SelectedRows.Count <= 0)
            {
                MessageBox.Show("请选择消费记录!");
                return;
            }
            editCard(this.dataGridView1.SelectedRows[0]);

        }

        private void editCard(DataGridViewRow row) {
            
            int id = (int)row.Cells["ID"].Value;

            string formString = "";
            
            mainForm.setForm(LabelUtility.CONSUME_ADD, 1, id);
        }

        private void button1_Click(object sender, EventArgs e)
        {
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

        public override void showDialog(object value)
        {
            this.ShowDialog();
        }
    }
}