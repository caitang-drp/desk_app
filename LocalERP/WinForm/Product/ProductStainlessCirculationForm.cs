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
    public class ProductStainlessCirculationForm : ProductCirculationForm
    {
        public ProductStainlessCirculationForm(CirculationTypeConf conf)
            : base(conf)
        {
        }

        public override void initDatagridview(DataGridView dgv)
        {
            DataGridViewTextBoxColumn ID = new DataGridViewTextBoxColumn();
            DataGridViewCheckBoxColumn check = new DataGridViewCheckBoxColumn();
            DataGridViewLookupColumn product = new DataGridViewLookupColumn();

            DataGridViewTextBoxColumn quantityPerPiece = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn pieces = new DataGridViewTextBoxColumn();

            DataGridViewTextBoxColumn num = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn unit = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn price = new DataGridViewTextBoxColumn();

            DataGridViewTextBoxColumn totalPrice = new DataGridViewTextBoxColumn();

            ID.HeaderText = "ID";
            ID.Name = "ID";
            ID.Visible = false;
            ID.Width = 60;

            check.HeaderText = "选择";
            check.Name = "check";
            check.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            check.Width = 46;

            product.HeaderText = "产品";
            product.LookupForm = null;
            product.Name = "product";
            product.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            product.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            product.Width = 140;

            quantityPerPiece.HeaderText = "每件数量";
            quantityPerPiece.Name = "quantityPerPiece";
            quantityPerPiece.Width = 90;

            pieces.HeaderText = "件数";
            pieces.Name = "pieces";
            pieces.Width = 60;

            num.HeaderText = "数量";
            num.Name = "num";
            num.Width = 100;

            unit.HeaderText = "单位";
            unit.Name = "unit";
            unit.Width = 60;

            price.HeaderText = this.Text.Substring(0, 2) + "单价/元";
            price.Name = "price";
            price.Width = 140;

            totalPrice.HeaderText = "总价/元";
            totalPrice.Name = "totalPrice";

            dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { ID, check, product, quantityPerPiece, pieces, num, unit, price, totalPrice });

            //特别注意：如果这个地方的ProductCIForm不是新建，用的是以前的窗口
            //那就可能有多个CirculationForm的DataGridViewLookupColumn指向同一个ProductCIForm
            //那么当以前的CirculationForm没有销毁的情况下，ProductCIForm就会触发以前的valueChanged事件，从而出现异常
            (dgv.Columns["product"] as DataGridViewLookupColumn).LookupForm = FormSingletonFactory.getInstance().getProductCIForm_select();
        }

        //for event: caculate total price
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
                if (!string.IsNullOrEmpty(arg.ArgName) && arg.ArgName == "ProductStainless")
                {
                    int productID = (int)(arg.Value);
                    int oldID = -1;
                    int.TryParse((control.EditingControlDataGridView.Rows[control.EditingControlRowIndex].Cells["product"].Value).ToString(), out oldID);
                    if (productID != oldID)
                    {
                        ProductStainless product = ProductStainlessDao.getInstance().FindByID(productID);
                        control.EditingControlDataGridView.Rows[control.EditingControlRowIndex].Cells["quantityPerPiece"].Value = product.QuantityPerPiece;
                        control.EditingControlDataGridView.Rows[control.EditingControlRowIndex].Cells["unit"].Value = product.Unit;
                        control.EditingControlDataGridView.Rows[control.EditingControlRowIndex].Cells["price"].Value = product.PricePurchase;
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

        protected override void editingControl_TextChanged(object sender, EventArgs e)
        {
            DataGridViewTextBoxEditingControl control = (sender as DataGridViewTextBoxEditingControl);
            DataGridView dgv = this.dataGridView1;
            DataGridViewCell cell = dgv.CurrentCell;

            String columnName = cell.OwningColumn.Name;
            float quantity, pieces, num;
            if (columnName == "quantityPerPiece" || columnName == "pieces")
            {
                if (float.TryParse(dgv.Rows[control.EditingControlRowIndex].Cells["pieces"].EditedFormattedValue.ToString(), out pieces)
                    && float.TryParse(dgv.Rows[control.EditingControlRowIndex].Cells["quantityPerPiece"].EditedFormattedValue.ToString(), out quantity))
                    dgv.Rows[control.EditingControlRowIndex].Cells["num"].Value = quantity * pieces;
            }
            else if (columnName == "num") {
                if (float.TryParse(dgv.Rows[control.EditingControlRowIndex].Cells["num"].EditedFormattedValue.ToString(), out num)
                    &&float.TryParse(dgv.Rows[control.EditingControlRowIndex].Cells["quantityPerPiece"].EditedFormattedValue.ToString(), out quantity) && quantity != 0)
                    dgv.Rows[control.EditingControlRowIndex].Cells["pieces"].Value = num / quantity;
            }

            if (columnName != "unit")
            {
                setSubTotalPrice(cell.RowIndex);
                setTotalPrice();
            }
            

            this.resetNeedSave(true);
            this.recordChanged = true;
        }

        protected bool getRecords(out List<ProductStainlessCirculationRecord> records)
        {
            records = new List<ProductStainlessCirculationRecord>();

            int number = this.dataGridView1.RowCount;

            double quantityPerPiece, pieces, price;
            int num;
            string unit;
            bool isInputCorrect = true;

            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                object productID = null;

                if (ValidateUtility.getLookupValue(row.Cells["product"], out productID) == false 
                    || ValidateUtility.getDouble(row.Cells["quantityPerPiece"], out quantityPerPiece) == false
                    || ValidateUtility.getDouble(row.Cells["pieces"], out pieces) == false
                    || ValidateUtility.getInt(row.Cells["num"], true, true, out num) == false
                    || ValidateUtility.getString(row.Cells["unit"], false, out unit)
                    || ValidateUtility.getDouble(row.Cells["price"], out price) == false)
                    return false;
                ProductStainlessCirculationRecord record = new ProductStainlessCirculationRecord();

                record.ProductName = ((row.Cells["product"] as DataGridViewLookupCell).EditedValue as LookupArg).Text;
                record.QuantityPerPiece = quantityPerPiece;
                record.Pieces = pieces;
                record.TotalNum = num;
                record.Unit = unit;
                record.Price = price;
                
                records.Add(record);
            }

            return isInputCorrect;
        }

        protected override void toolStripButton_save_Click(object sender, EventArgs e)
        {
            //for datagridview validate
            if (dataGridView1.Rows.Count > 0 && dataGridView1.Columns["totalPrice"].Visible == true)
                dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells["totalPrice"];

            List<ProductStainlessCirculationRecord> records;
            bool isRecordsCorrect = getRecords(out records);

            ProductCirculation circulation;
            bool isSellCorrect = getCirculation(out circulation);
            if (isRecordsCorrect == false || isSellCorrect == false)
                return;

            for (int i = 0; i < records.Count; i++)
            {
                ProductStainlessCirculationRecord record = records[i];
                for (int j = 0; j < records.Count; j++)
                {
                    ProductStainlessCirculationRecord compare = records[j];
                    if (compare.ProductID == record.ProductID && i != j)
                    {
                        MessageBox.Show(string.Format("商品{0}有多条记录,请在同一记录里输入该商品的所有数量!", compare.ProductName), "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }

            try
            {
                if (openMode == 0)
                {
                    circulation.Status = 1;
                    ProductStainlessCirculationDao.getInstance().Insert(circulation, records, out circulationID);
                    MessageBox.Show(string.Format("增加{0}成功!", this.Text), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (openMode == 1)
                {/*
                    ProductStainlessCirculationDao.getInstance().UpdateBaiscInfo(circulation);
                    if (recordChanged)
                        ProductStainlessCirculationDao.getInstance().updateRecords(circulation.ID, records);
                    MessageBox.Show(string.Format("保存{0}成功!", this.Text), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);*/
                }

                openMode = 1;
                //this.initCirculation();


            }
            catch (Exception ex)
            {
                if (openMode == 0)
                    ProductStainlessCirculationDao.getInstance().DeleteByID(circulationID);
                MessageBox.Show("保存有误,可能是往来单位或商品属性被修改过,请重新编辑!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            //so important: if edit ,it should be refresh also, because edit will del exist item and add new item

            this.invokeUpdateNotify(notifyType);
        }
    }
}