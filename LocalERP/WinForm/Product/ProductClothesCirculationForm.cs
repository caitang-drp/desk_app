using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LocalERP.DataAccess.DataDAO;
using LocalERP.DataAccess.Data;
using LocalERP.UiDataProxy;
using LocalERP.DataAccess.Utility;
using LocalERP.WinForm.Utility;
using System.IO;

namespace LocalERP.WinForm
{
    public class ProductClothesCirculationForm : ProductCirculationForm
    {
        public ProductClothesCirculationForm(CirculationTypeConf conf, ProductCirculationDao dao):base(conf, dao)
        {
        }
        
        public override void initDatagridview(DataGridView dgv)
        {
            base.initDatagridview(dgv);
            DataGridViewLookupColumn num = new DataGridViewLookupColumn();

            num.HeaderText = "数量/个";
            num.LookupForm = null;
            num.Name = "num";
            num.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            num.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            num.Width = 240;

            dgv.Columns.Insert(3, num);

            (dgv.Columns["num"] as DataGridViewLookupColumn).LookupForm = new ProductClothesInputNumForm(this);
        }

        protected override void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control.GetType().Equals(typeof(DataGridViewTextBoxEditingControl)))//cell为类TextBox时
            {
                e.CellStyle.BackColor = Color.FromName("window");
                DataGridViewTextBoxEditingControl editingControl = e.Control as DataGridViewTextBoxEditingControl;

                editingControl.TextChanged -= new EventHandler(editingControl_TextChanged);
                editingControl.TextChanged += new EventHandler(editingControl_TextChanged);
            }
            else if (e.Control.GetType().Equals(typeof(DataGridViewLookupEditingControl)))
            {
                DataGridViewLookupEditingControl editingControl = e.Control as DataGridViewLookupEditingControl;
                string columnName = this.dataGridView1.CurrentCell.OwningColumn.Name;
                editingControl.valueSetted -= new LookupText.ValueSetted(productEditingControl_valueSetted);
                editingControl.valueSetted += new LookupText.ValueSetted(productEditingControl_valueSetted);
                if (columnName == "num")
                {
                    ProductClothesInputNumForm form = editingControl.LookupForm as ProductClothesInputNumForm;
                    LookupArg arg = this.dataGridView1.CurrentCell.OwningRow.Cells["product"].Value as LookupArg;
                    form.setTitle(arg.Text);
                    form.ProductId = (int)(arg.Value);

                    editingControl.LookupArg = (this.dataGridView1.CurrentCell.OwningRow.Cells["num"] as DataGridViewLookupCell).Value as LookupArg;
                }
            }

        }

        protected override void productEditingControl_valueSetted(object sender, LookupArg arg)
        {
            //File.AppendAllText("e:\\debug.txt", string.Format("value changed, thread:{0}\r\n", System.Threading.Thread.CurrentThread.ManagedThreadId));

            DataGridViewLookupEditingControl control = (sender as DataGridViewLookupEditingControl);

            try
            {
                //File.AppendAllText("e:\\debug.txt", string.Format("value changed, dataGridView hash code={0}, dataGridView name={1}\r\n", control.EditingControlDataGridView.GetHashCode(), control.EditingControlDataGridView.Name));
                if (control.EditingControlDataGridView.Rows.Count == 0 || control.EditingControlDataGridView.CurrentCell == null)
                    throw new Exception();

                //if(control.EditingControlDataGridView.CurrentCell.OwningColumn.Name == "product")
                if (!string.IsNullOrEmpty(arg.ArgName) && arg.ArgName == "Product")
                {
                    int productID = (int)(arg.Value);
                    int oldID = -1;
                    int.TryParse((control.EditingControlDataGridView.Rows[control.EditingControlRowIndex].Cells["product"].Value).ToString(), out oldID);
                    if (productID != oldID)
                    {
                        control.EditingControlDataGridView.Rows[control.EditingControlRowIndex].Cells["price"].Value = ProductClothesDao.getInstance().FindPriceByID(productID);
                        DataGridViewLookupEditingControl lookup = sender as DataGridViewLookupEditingControl;
                        this.setCellEnable(control.EditingControlDataGridView.Rows[control.EditingControlRowIndex].Cells["num"], true);
                        control.EditingControlDataGridView.Rows[control.EditingControlRowIndex].Cells["num"].Value = new LookupArg("", "");
                    }
                }
                //not reasonal
                setSubTotalPrice(control.EditingControlRowIndex);
                setTotalPrice();
            }
            catch (Exception ex)
            {
                //File.AppendAllText("e:\\debug.txt",string.Format("exception, dataGridView.Rows.Count={0}\r\n", this.dataGridView1.Rows.Count));
                //File.AppendAllText("e:\\debug.txt", string.Format("exception, dataGridView hash code={0}, dataGridView name={1}\r\n", control.EditingControlDataGridView.GetHashCode(), control.EditingControlDataGridView.Name));
                System.Threading.Thread.Sleep(0);
            }

            this.resetNeedSave(true);
            this.recordChanged = true;
        }

        protected override void setSubTotalPrice(int rowIndex)
        {
            DataGridViewRow row = this.dataGridView1.Rows[rowIndex];
            double price;
            int num = 0;
            ValidateUtility.getDouble(row.Cells["price"], out price);
            try
            {
                object temp = (row.Cells["num"] as DataGridViewLookupCell).EditedValue;
                num = ((temp as LookupArg).Value as ProductCirculationRecord).TotalNum;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            row.Cells["totalPrice"].Value = num * price;
        }

        protected override bool getRecords(out List<ProductCirculationRecord> records)
        {
            records = null;
            return true;
            /*for (int i = 0; i < records.Count; i++)
            {
                ProductClothesCirculationRecord record = records[i];
                for (int j = 0; j < records.Count; j++)
                {
                    ProductClothesCirculationRecord compare = records[j];
                    if (compare.ProductID == record.ProductID && i != j)
                    {
                        MessageBox.Show(string.Format("商品{0}有多条记录,请在同一记录里输入该商品的所有数量!", compare.ProductName), "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }*/
        }

    }
}