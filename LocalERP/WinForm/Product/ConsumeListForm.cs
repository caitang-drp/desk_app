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
                this.label_tip.Text = "˫��ѡ�����Ѽ�¼";
                this.dataGridView1.Columns["check"].Visible = false;
            }

            DateTime dateTime = DateTime.Now;
            this.dateTimePicker3.Value = dateTime.AddMonths(-1);

            //this.cirDao = cirDao;
        }

        private void ConsumeListForm_Load(object sender, EventArgs e)
        {
            initCustomerCombox();
            initList();
            
        }

        private void initCustomerCombox() {

            ArrayList arrayList = new ArrayList();

            arrayList.Add(new DictionaryEntry(0, "-ȫ��-"));
            arrayList.Add(new DictionaryEntry(1, ProductCirculation.circulationStatusContext[0]));
            arrayList.Add(new DictionaryEntry(4, ProductCirculation.circulationStatusContext[3]));

            this.comboBox1.DataSource = arrayList;
            this.comboBox1.ValueMember = "Key";
            this.comboBox1.DisplayMember = "Value";

            
        }

        
        private void initList()
        {
            List<Consume> list = ConsumeDao.getInstance().FindList(0);

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
                //this.dataGridView1.Rows[index].Cells["status"].Value = dr["status"];

                /*
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
                */

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
                MessageBox.Show("��ѡ�񵥾�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            StringBuilder ids = new StringBuilder();
            for (int ii = 0; ii < list.Count; ii++)
            {
                ids.Append(list[ii]);
                ids.Append(" ");
            }
            if (MessageBox.Show(string.Format("�Ƿ�ɾ����ˮ��Ϊ{0}�Ŀ�Ƭ?", ids.ToString()), "��ʾ", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (CardDao.getInstance().FindByID(list[i]).Status > 1)
                    {
                        MessageBox.Show(string.Format("IDΪ{0}�Ŀ�Ƭ�Ѿ����, �޷�ɾ��!", list[i]), "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        initList();
                        return;
                    }
                    CardDao.getInstance().Delete(list[i]);
                }
                initList();
                MessageBox.Show("ɾ�����ݳɹ�!", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //edit
        private void toolStripButton3_Click(object sender, EventArgs e)
        {/*
            List<DataGridViewRow> rows = this.dataGridView1.getCheckRows("check");
            if (rows == null || rows.Count <= 0)
            {
                MessageBox.Show("��ѡ�����!", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            int id = (int)this.dataGridView1.Rows[e.RowIndex].Cells["ID"].Value;
            //openMode == 0����ѡ��
            if (openMode == 0)
            {
                string name = this.dataGridView1.Rows[e.RowIndex].Cells["name"].Value.ToString();
                string num = this.dataGridView1.Rows[e.RowIndex].Cells["num"].Value.ToString();
                string customer = this.dataGridView1.Rows[e.RowIndex].Cells["customer"].Value.ToString();
                LookupArg lookupArg = new LookupArg(id, string.Format("���{0} | {1} | ��{2}�� ", name, customer, num));
                //lookupArg.ArgName = conf.CategoryName;

                //File.AppendAllText("e:\\debug.txt", string.Format("double click, thread:{0}\r\n", System.Threading.Thread.CurrentThread.ManagedThreadId));

                this.Close();
                this.invokeLookupCallback(lookupArg);
            }
            else if (openMode == 1)
            {
                /*if (this.dataGridView1.SelectedRows == null || this.dataGridView1.SelectedRows.Count <= 0)
                {
                    MessageBox.Show("��ѡ��Ƭ!");
                    return;
                }*/
                editCard(this.dataGridView1.SelectedRows[0]);
            }
        }

        private void editCard(DataGridViewRow row) {
            
            int id = (int)row.Cells["ID"].Value;

            string formString = "";
            
            mainForm.setForm(LabelUtility.CARD_ADD, 1, id);
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

        public override void showDialog(object value)
        {
            this.ShowDialog();
        }
    }
}