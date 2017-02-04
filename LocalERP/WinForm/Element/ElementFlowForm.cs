using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using LocalERP.DataAccess.DataDAO;

namespace LocalERP.WinForm
{
    public partial class ElementFlowForm : DockContent
    {
        public delegate void ElementChanged();
        public event ElementChanged elementChanged;

        public ElementFlowForm()
        {
            InitializeComponent();
        }

        private void ElementFlowForm_Load(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            this.dateTimePicker1.Value = dateTime.AddMonths(-1);
            
            initElementCombox();
            initList();
        }

        private void initElementCombox()
        {
            DataTable elementDT = ElementDao.getInstance().FindList();
            DataRow topRow = elementDT.NewRow();
            topRow["name"] = "-全部-";
            topRow["ID"] = "0";
            elementDT.Rows.InsertAt(topRow, 0);
            this.comboBox_element.DataSource = elementDT;
            this.comboBox_element.DisplayMember = "name";
            this.comboBox_element.ValueMember = "ID";
        }

        private void initList()
        {
            this.dataGridView1.Rows.Clear();
            DataTable dataTable = ElementFlowDao.getInstance().FindList((int)comboBox_element.SelectedValue, this.dateTimePicker1.Value, this.dateTimePicker2.Value.AddDays(1), 0);
            foreach (DataRow dr in dataTable.Rows)
            {
                int index = this.dataGridView1.Rows.Add();
                this.dataGridView1.Rows[index].Cells["ID"].Value = dr["ID"];
                this.dataGridView1.Rows[index].Cells["name"].Value = dr["elementName"];
                int type = (int)dr["flowType"];
                String typeString = "入库";
                if (type == -1)
                    typeString = "出库";
                this.dataGridView1.Rows[index].Cells["flowType"].Value = typeString;
                this.dataGridView1.Rows[index].Cells["num"].Value = (int)dr["num"] * (int)dr["flowType"];
                this.dataGridView1.Rows[index].Cells["accumulativeNum"].Value = dr["accumulativeNum"];
                this.dataGridView1.Rows[index].Cells["comment"].Value = dr["comment"];
                this.dataGridView1.Rows[index].Cells["flowTime"].Value = dr["flowTime"];
            }
        }

        public void refreshForMainForm() {
            this.initElementCombox();
            this.initList();
        }

        /// <summary>
        /// for event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private bool checkElementNum() {
            int num = ElementDao.getInstance().FindList().Rows.Count;
            if (num <= 0) {
                MessageBox.Show("目前暂无配件,请先增加配件.", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (checkElementNum() == false)
                return;

            ElementFlowDetailForm form = new ElementFlowDetailForm(0);
            form.modifiedComplete += new ElementFlowDetailForm.ModifiedComplete(refresh);
            form.ShowDialog();
        }

        private void toolStripButton_out_Click(object sender, EventArgs e)
        {
            if (checkElementNum() == false)
                return;

            ElementFlowDetailForm form = new ElementFlowDetailForm(1);
            form.modifiedComplete += new ElementFlowDetailForm.ModifiedComplete(refresh);
            form.ShowDialog();
        }

        void refresh()
        {
            initList();
            if (elementChanged != null)
                elementChanged.Invoke();
        }

        //query
        private void button1_Click(object sender, EventArgs e)
        {
            initList();
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            e.Handled = true;
            e.PaintBackground(e.CellBounds, true);
            e.PaintContent(e.CellBounds);
        }
    }
}