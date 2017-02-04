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
    public partial class ProductFlowForm : DockContent
    {
        public delegate void ProductChanged();
        public event ProductChanged productChanged;

        public ProductFlowForm()
        {
            InitializeComponent();
        }

        private void ProductFlowForm_Load(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            this.dateTimePicker1.Value = dateTime.AddMonths(-1);

            initProductCombox();
            initList();
        }

        private void initProductCombox()
        {
            DataTable productDT = ProductDao.getInstance().FindList(null);
            DataRow topRow = productDT.NewRow();
            topRow["name"] = "-全部-";
            topRow["ID"] = "0";
            productDT.Rows.InsertAt(topRow, 0);
            this.comboBox_product.DataSource = productDT;
            this.comboBox_product.DisplayMember = "name";
            this.comboBox_product.ValueMember = "ID";
        }

        private void initList()
        {
            this.dataGridView1.Rows.Clear();
            DataTable dataTable = ProductFlowDao.getInstance().FindList((int)comboBox_product.SelectedValue, this.dateTimePicker1.Value, this.dateTimePicker2.Value.AddDays(1), 0);
            foreach (DataRow dr in dataTable.Rows)
            {
                int index = this.dataGridView1.Rows.Add();
                this.dataGridView1.Rows[index].Cells["ID"].Value = dr["ID"];
                this.dataGridView1.Rows[index].Cells["name"].Value = dr["productName"];
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
            this.initProductCombox();
            this.initList();
        }

        /// <summary>
        /// for event
        /// </summary>
        private bool checkProductNum()
        {
            int num = ProductDao.getInstance().FindList(null).Rows.Count;
            if (num <= 0)
            {
                MessageBox.Show("目前暂无成品,请先增加成品.", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        //flow in
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (checkProductNum() == false)
                return;

            ProductFlowDetailForm form = new ProductFlowDetailForm(0);
            form.modifiedComplete += new ProductFlowDetailForm.ModifiedComplete(refresh);
            form.ShowDialog();
        }

        void refresh()
        {
            initList();
            if (productChanged != null)
                productChanged.Invoke();
        }


        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (checkProductNum() == false)
                return;

            ProductFlowDetailForm form = new ProductFlowDetailForm(1);
            form.modifiedComplete += new ProductFlowDetailForm.ModifiedComplete(refresh);
            form.ShowDialog();
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