using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using LocalERP.DataAccess.Data;
using LocalERP.DataAccess.DataDAO;

namespace LocalERP.WinForm
{
    public partial class ProductListUC : UserControl
    {
        public delegate void ProductChanged();
        public event ProductChanged productChanged;

        public ProductListUC()
        {
            InitializeComponent();
        }

        private void ProductListUC_Load(object sender, EventArgs e)
        {
            initList();
        }


        /// <summary>
        /// set control
        /// </summary>
        public void initList()
        {
            this.dataGridView1.Rows.Clear();
            DataTable dataTable = ProductDao.getInstance().FindList(null);
            foreach (DataRow dr in dataTable.Rows)
            {
                int index = this.dataGridView1.Rows.Add();
                this.dataGridView1.Rows[index].Cells["ID"].Value = dr["ID"];
                this.dataGridView1.Rows[index].Cells["name"].Value = dr["name"];
                this.dataGridView1.Rows[index].Cells["price"].Value = dr["price"];
                this.dataGridView1.Rows[index].Cells["comment"].Value = dr["comment"];
            }
        }

        //get value
        private List<int> getSelectIDs()
        {
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
        /// event
        /// </summary>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ProductForm form = new ProductForm(0, 0);
            form.modifiedComplete += new ProductForm.ModifiedComplete(refresh);
            form.ShowDialog();
        }

        void refresh()
        {
            initList();
            if (productChanged != null)
                productChanged.Invoke();
        }

        //edit
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGridView1.SelectedRows == null || this.dataGridView1.SelectedRows.Count <= 0)
            {
                MessageBox.Show("请选择成品!");
                return;
            }
            ProductForm form = new ProductForm(1, (int)this.dataGridView1.SelectedRows[0].Cells["ID"].Value);
            form.modifiedComplete += new ProductForm.ModifiedComplete(refresh);
            form.ShowDialog();
        }

        private void toolStripButton_edit_Click(object sender, EventArgs e)
        {
            List<int> list = getSelectIDs();
            if (list == null || list.Count <= 0)
            {
                MessageBox.Show("请选择成品!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            ProductForm form = new ProductForm(1, list[0]);
            form.modifiedComplete += new ProductForm.ModifiedComplete(refresh);
            form.ShowDialog();
        }

        private void toolStripButton_del_Click(object sender, EventArgs e)
        {
            List<int> list = getSelectIDs();
            if (list == null || list.Count <= 0)
            {
                MessageBox.Show("请选择成品", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            StringBuilder ids = new StringBuilder();
            for (int ii = 0; ii < list.Count; ii++)
            {
                ids.Append(list[ii]);
                ids.Append(" ");
            }
            if (MessageBox.Show(string.Format("是否删除ID为{0}的成品?", ids.ToString()), "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                ProductDao dao = ProductDao.getInstance();
                try
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        dao.Delete(list[i]);
                    }
                    MessageBox.Show("删除成品成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch
                {
                    MessageBox.Show("删除成品失败，可能是其他数据引用到该成品!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                refresh();
            }
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            e.Handled = true;
            e.PaintBackground(e.CellBounds, true);
            e.PaintContent(e.CellBounds);
        }
    }
}
