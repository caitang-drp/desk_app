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

namespace LocalERP.WinForm
{
    public partial class ElementOrderForm : DockContent
    {
        public delegate void ElementChanged();
        public event ElementChanged elementChanged;

        public ElementOrderForm()
        {
            InitializeComponent();
        }

        private void ElementOrderForm_Load(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            this.dateTimePicker1.Value = dateTime.AddMonths(-1);
           
            ArrayList arrayList = new ArrayList();

            arrayList.Add(new DictionaryEntry(0, "全部"));
            for(int i = 1;i<=4;i++)
                arrayList.Add(new DictionaryEntry(i, ElementOrder.statusEnum[i-1]));
  

            this.comboBox_status.DataSource = arrayList;
            this.comboBox_status.ValueMember = "Key";
            this.comboBox_status.DisplayMember = "Value";
            
            initList();
            this.dataGridView1.Columns["check"].ReadOnly = false;
        }


        public void initList()
        {
            this.dataGridView1.Rows.Clear();
            DataTable dataTable = ElementOrderDao.getInstance().FindList(this.dateTimePicker1.Value, this.dateTimePicker2.Value.AddDays(1), (int)(comboBox_status.SelectedValue));
            foreach (DataRow dr in dataTable.Rows)
            {
                int index = this.dataGridView1.Rows.Add();
                this.dataGridView1.Rows[index].Cells["ID"].Value = dr["ID"];
                this.dataGridView1.Rows[index].Cells["serial"].Value = dr["serial"];
                this.dataGridView1.Rows[index].Cells["orderTime"].Value = dr["orderTime"];
                this.dataGridView1.Rows[index].Cells["status"].Value = ElementOrder.statusEnum[(int)(dr["status"])-1];
            }
        }


        /// <summary>
        /// get value from datagridview
        /// </summary>
        /// <returns></returns>
        private List<int> getSelectIDs()
        {
            //this.dataGridView1.CurrentCell.Value = this.dataGridView1.CurrentCell.EditedFormattedValue;
            List<int> listID = new List<int>();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                DataGridViewCheckBoxCell cell = row.Cells["check"] as DataGridViewCheckBoxCell;
                //commented by stone: this is not very reasonable
                if ((bool)cell.EditedFormattedValue == true)
                //if (cell.Value != null && Convert.ToInt32(cell.Value) == 1)
                    listID.Add((int)row.Cells["ID"].Value);
            }
            return listID;
        }

        /// <summary>
        /// for event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        //add
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ElementOrderDetailForm form = new ElementOrderDetailForm(0, 0);
            form.modifiedComplete += new ElementOrderDetailForm.ModifiedComplete(refresh);
            form.ShowDialog();
        }

        void refresh()
        {
            this.initList();

            //deliver the ElementOrderDetailForm's modifiedComplete event to mainForm
            if (elementChanged != null)
                elementChanged.Invoke();
        }

        //edit
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            List<int> list = getSelectIDs();
            if (list == null || list.Count <= 0)
            {
                MessageBox.Show("请选择订单!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            ElementOrderDetailForm form = new ElementOrderDetailForm(1,list[0]);
            form.modifiedComplete += new ElementOrderDetailForm.ModifiedComplete(refresh);
            form.ShowDialog();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGridView1.SelectedRows == null || this.dataGridView1.SelectedRows.Count <= 0)
            {
                MessageBox.Show("请选择订单!");
                return;
            }
            ElementOrderDetailForm form = new ElementOrderDetailForm(1, (int)this.dataGridView1.SelectedRows[0].Cells["ID"].Value);
            form.modifiedComplete += new ElementOrderDetailForm.ModifiedComplete(refresh);
            form.ShowDialog();
        }

        //del
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            List<int> list = getSelectIDs();
            if (list == null || list.Count <= 0)
            {
                MessageBox.Show("请选择订单", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            StringBuilder ids = new StringBuilder();
            for (int ii = 0; ii < list.Count; ii++)
            {
                ids.Append(list[ii]);
                ids.Append(" ");
            }
            if (MessageBox.Show(string.Format("是否删除流水号为{0}的订单?", ids.ToString()), "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                ElementOrderDao dao = ElementOrderDao.getInstance();
                for (int i = 0; i < list.Count; i++)
                {
                    dao.Delete(list[i]);
                }
                initList();
                MessageBox.Show("删除订单成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //query
        private void button1_Click(object sender, EventArgs e)
        {
            initList();
        }

        //commented by stone: del dotted line
        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            e.Handled = true;
            e.PaintBackground(e.CellBounds, true);
            e.PaintContent(e.CellBounds);
        }
    }
}