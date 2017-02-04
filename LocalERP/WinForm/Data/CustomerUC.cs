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
    public partial class CustomerUC : UserControl
    {
        public delegate void CustomerChanged();
        public event CustomerChanged customerChanged;

        public CustomerUC()
        {
            InitializeComponent();
        }

        private void CustomerUC_Load(object sender, EventArgs e)
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
            DataTable dataTable = CustomerDao.getInstance().FindList(null, null);
            foreach (DataRow dr in dataTable.Rows)
            {
                int index = this.dataGridView1.Rows.Add();
                this.dataGridView1.Rows[index].Cells["ID"].Value = dr["ID"];
                this.dataGridView1.Rows[index].Cells["name"].Value = dr["name"];
                this.dataGridView1.Rows[index].Cells["tel"].Value = dr["tel"];
                this.dataGridView1.Rows[index].Cells["phone"].Value = dr["phone"];
                this.dataGridView1.Rows[index].Cells["address"].Value = dr["address"];
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
            CustomerForm customerForm = new CustomerForm();
            customerForm.ShowDialog();
        }

        void refresh()
        {
            initList();
            if (customerChanged != null)
                customerChanged.Invoke();
        }

        //modify
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            List<int> list = getSelectIDs();
            if (list == null || list.Count <= 0)
            {
                MessageBox.Show("请选择客户!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            CustomerForm form = new CustomerForm();
            form.ShowDialog();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGridView1.SelectedRows == null || this.dataGridView1.SelectedRows.Count <= 0)
            {
                MessageBox.Show("请选择客户!");
                return;
            }
            CustomerForm form = new CustomerForm();
            form.ShowDialog();
        }

        //del
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            List<int> list = getSelectIDs();
            if (list == null || list.Count <= 0)
            {
                MessageBox.Show("请选择客户", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            StringBuilder ids = new StringBuilder();
            for (int ii = 0; ii < list.Count; ii++)
            {
                ids.Append(list[ii]);
                ids.Append(" ");
            }
            if (MessageBox.Show(string.Format("是否删除ID为{0}的客户?", ids.ToString()), "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                CustomerDao dao = CustomerDao.getInstance();
                try
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        dao.Delete(list[i]);
                    }

                    MessageBox.Show("删除客户成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                }
                catch {
                    MessageBox.Show("删除客户失败，可能是其他数据引用到该客户!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
