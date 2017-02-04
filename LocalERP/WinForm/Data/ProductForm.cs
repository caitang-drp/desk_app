using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LocalERP.DataAccess.DataDAO;
using LocalERP.DataAccess.Data;

namespace LocalERP.WinForm
{
    public partial class ProductForm : Form
    {
        //openMode 0:add   1:edit
        private int openMode = 0;
        private int productID = 0;

        private DataTable elementTable = null;

        public delegate void ModifiedComplete();
        public event ModifiedComplete modifiedComplete;

        public ProductForm(int mode, int id)
        {
            openMode = mode;
            productID = id;
            InitializeComponent();
        }

        private void ElementForm_Load(object sender, EventArgs e)
        {
            DataGridViewComboBoxColumn elementColumn = this.dataGridView1.Columns["element"] as DataGridViewComboBoxColumn;
            elementTable = ElementDao.getInstance().FindList();
            elementColumn.DataSource = elementTable;
            elementColumn.ValueMember = "ID";
            elementColumn.DisplayMember = "name";

            initProduct();
        }

        private void initProduct() {
            if (openMode == 1)
            {
                this.label4.Text = "编辑 ID:" + productID;

                Product product = ProductDao.getInstance().FindByID(productID);
                this.textBox_name.Text = product.Name;
                this.textBox_price.Text = product.Price.ToString();
                this.textBox_comment.Text = product.Comment;

                this.dataGridView1.Rows.Clear();

                List<Element> list = ElementDao.getInstance().FindListByProductID(productID);
                foreach (Element element in list)
                {
                    int index = this.dataGridView1.Rows.Add();
                    this.dataGridView1.Rows[index].Cells["element"].Value = element.ID;
                }
            }
        }

        /// <summary>
        /// get value from control with valiadating
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private bool getName(out string name) {
            if (string.IsNullOrEmpty(this.textBox_name.Text))
            {
                this.errorProvider1.SetError(this.textBox_name, "输入不能为空!");
                name = "";
                return false;
            }
            else
            {
                this.errorProvider1.SetError(this.textBox_name, string.Empty);
                name = this.textBox_name.Text;
                return true;
            }
        }

        private bool getPrice(out double price) {
            double tempPrice = 0;
            if (string.IsNullOrEmpty(this.textBox_price.Text) || double.TryParse(this.textBox_price.Text, out tempPrice))
            {
                this.errorProvider1.SetError(this.textBox_price, string.Empty);
                price = tempPrice;
                return true;
            }
            else {
                this.errorProvider1.SetError(this.textBox_price, "请输入数字!");
                price = tempPrice;
                return false;
            }
        }

        private bool getProduct(out Product product) {
            double price;
            string name;
            bool isNameCorrect = this.getName(out name);
            bool isPriceCorrect = this.getPrice(out price);
            if ( isNameCorrect && isPriceCorrect)
            {
                product = new Product(name, 0, price, this.textBox_comment.Text);
                product.ID = productID;
                return true;
            }
            else
            {
                product = null;
                return false;
            }
        }

        private List<int> getElementIDs()
        {
            this.textBox_comment.Focus();

            List<int> elementIDs = new List<int>();
            int number = this.dataGridView1.RowCount;
            foreach(DataGridViewRow row in this.dataGridView1.Rows)
            {
                DataGridViewComboBoxCell cell = row.Cells["element"] as DataGridViewComboBoxCell;
                //stone: this should be modified, bug if only one element is add
                int elementID = Convert.ToInt32(cell.Value);
                
                if (elementID == 0)
                    continue;

                elementIDs.Add(elementID);
            }

            return elementIDs;
        }

        private List<int> getSelectRows()
        {
            List<int> list = new List<int>();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                DataGridViewCheckBoxCell cell = row.Cells["check"] as DataGridViewCheckBoxCell;
                //commented by stone: this is not very reasonable
                if ((bool)cell.EditedFormattedValue == true)
                    list.Add(row.Index);
            }
            return list;
        }

        /// <summary>
        /// event for validating
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_name_Validating(object sender, CancelEventArgs e)
        {
            string name;
            getName(out name);
        }

        private void textBox_price_Validating(object sender, CancelEventArgs e)
        {
            double temp;
            this.getPrice(out temp);
        }

        /// <summary>
        /// event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Product product = null;
            if (this.getProduct(out product) == false)
                return;

            if (openMode == 0) {
                ProductDao.getInstance().Insert(product, this.getElementIDs());
                if (modifiedComplete != null)
                    modifiedComplete.Invoke();
                MessageBox.Show("保存成品成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else if (openMode == 1) {
                ProductDao.getInstance().Update(product, this.getElementIDs());
                if (modifiedComplete != null)
                    modifiedComplete.Invoke();
                MessageBox.Show("修改成品成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton_add_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Rows.Add();
            if (elementTable != null && elementTable.Rows.Count > 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1];
                int elementID = (int)(elementTable.Rows[0]["ID"]);
                (row.Cells["element"] as DataGridViewComboBoxCell).Value = elementID;
            }
        }

        private void toolStripButton_del_Click(object sender, EventArgs e)
        {
            List<int> rowsIndex = this.getSelectRows();

            if (rowsIndex.Count <= 0)
            {
                MessageBox.Show("请选择项", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            for (int i=rowsIndex.Count - 1;i>=0;i--) {
                this.dataGridView1.Rows.RemoveAt(rowsIndex[i]);
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