using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using LocalERP.DataAccess.DataDAO;

namespace LocalERP.WinForm
{
    public partial class ElementUC : UserControl
    {
        public delegate void ElementChanged();
        public event ElementChanged elementChanged;

        public ElementUC()
        {
            InitializeComponent();
        }

        private void ElementUC_Load(object sender, EventArgs e)
        {
            initList();
            this.dataGridView1.Columns["check"].ReadOnly = false;
        }

        /// <summary>
        /// set control
        /// </summary>
        public void initList()
        {
            this.dataGridView1.Rows.Clear();
            DataTable dataTable = ElementDao.getInstance().FindList();
            foreach (DataRow dr in dataTable.Rows)
            {
                int index = this.dataGridView1.Rows.Add();
                this.dataGridView1.Rows[index].Cells["ID"].Value = dr["ID"];
                this.dataGridView1.Rows[index].Cells["name"].Value = dr["name"];
                this.dataGridView1.Rows[index].Cells["price"].Value = dr["price"];
                this.dataGridView1.Rows[index].Cells["comment"].Value = dr["comment"];
            }
        }

        /// <summary>
        /// get value from datagridview
        /// </summary>
        /// <returns></returns>
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ElementForm elementForm = new ElementForm(0, 0);
            elementForm.modifiedComplete += new ElementForm.ModifiedComplete(refresh);
            elementForm.ShowDialog();
        }

        void refresh()
        {
            initList();
            if (elementChanged != null)
                elementChanged.Invoke();
        }

        //modify
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            List<int> list = getSelectIDs();
            if (list == null || list.Count <= 0)
            {
                MessageBox.Show("请选择配件!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            ElementForm form = new ElementForm(1, list[0]);
            form.modifiedComplete += new ElementForm.ModifiedComplete(refresh);
            form.ShowDialog();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGridView1.SelectedRows == null || this.dataGridView1.SelectedRows.Count <= 0)
            {
                MessageBox.Show("请选择订单!");
                return;
            }
            ElementForm form = new ElementForm(1, (int)this.dataGridView1.SelectedRows[0].Cells["ID"].Value);
            form.modifiedComplete += new ElementForm.ModifiedComplete(refresh);
            form.ShowDialog();
        }

        //del
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            List<int> list = getSelectIDs();
            if (list == null || list.Count <= 0)
            {
                MessageBox.Show("请选择配件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            StringBuilder ids = new StringBuilder();
            for (int ii = 0; ii < list.Count; ii++)
            {
                ids.Append(list[ii]);
                ids.Append(" ");
            }
            if (MessageBox.Show(string.Format("是否删除ID为{0}的配件?", ids.ToString()), "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                ElementDao dao = ElementDao.getInstance();
                try
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        dao.Delete(list[i]);
                    }

                    MessageBox.Show("删除配件成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);                  
                }
                catch {
                    MessageBox.Show("删除配件失败，可能是其他数据引用到该配件!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
