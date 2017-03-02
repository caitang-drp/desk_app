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
    public partial class QueryProductClothesDetailForm : MyDockContent
    {
        public QueryProductClothesDetailForm()
        {
            InitializeComponent();
        }

        private void QueryProductDetailForm_Load(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            this.dateTimePicker3.Value = dateTime.AddMonths(-1);

            initList();
        }

        private void initList()
        {
            DataTable dataTable = ProductClothesCirculationSKURecordDao.getInstance().FindList(
                this.dateTimePicker3.Value, this.dateTimePicker4.Value.AddDays(1), 0, 
                this.textBox_product.Text, -1, this.textBox_customer.Text, -1);
            this.dataGridView1.Rows.Clear();
            foreach (DataRow dr in dataTable.Rows)
            {
                int index = this.dataGridView1.Rows.Add();
                this.dataGridView1.Rows[index].Cells["ID"].Value = dr["ProductCirculationSKURecord.ID"];
                this.dataGridView1.Rows[index].Cells["product"].Value = dr["Product.Name"];
                this.dataGridView1.Rows[index].Cells["color"].Value = dr["colorName"];
                this.dataGridView1.Rows[index].Cells["size"].Value = dr["sizeName"];
                int num = (int)dr["num"] * (int)dr["flowType"];
                this.dataGridView1.Rows[index].Cells["num"].Value = num;
                this.dataGridView1.Rows[index].Cells["code"].Value = dr["code"];
                int type = (int)(dr["type"]);
                this.dataGridView1.Rows[index].Cells["type"].Value = ProductCirculation.CirculationTypeConfs[type - 1].name;
                this.dataGridView1.Rows[index].Cells["customer"].Value = dr["circulation.name"];
                this.dataGridView1.Rows[index].Cells["time"].Value = dr["circulationTime"];
                /*this.dataGridView1.Rows[index].Cells["status"].Value = ProductCirculation.circulationStatusContext[(int)(dr["status"]) - 1];*/
            }
        }

        /// <summary>
        /// event
        /// </summary>

        public override void refresh()
        {
            this.initList();
        }

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