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
        //private List<ProductStainlessCirculationRecord> records;

        public ProductStainlessCirculationForm(CirculationTypeConf conf, ProductCirculationDao dao)
            : base(conf, dao)
        {
        }

        public override void initDatagridview(DataGridView dgv)
        {
            this.label1_tip.Text = "（“每件数量”和“件数”可为空）";

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
            unit.Width = 100;

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

        protected override void initDatagridviewEnable(bool elementReadonly)
        {
            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                if (elementReadonly == true)
                {
                    setCellEnable(row.Cells["product"], false);
                    setCellEnable(row.Cells["quantityPerPiece"], false);
                    setCellEnable(row.Cells["pieces"], false);
                    setCellEnable(row.Cells["num"], false);
                    setCellEnable(row.Cells["unit"], false);
                    setCellEnable(row.Cells["price"], false);
                }
                setCellEnable(row.Cells["totalPrice"], false);
            }

            this.dataGridView1.Columns["check"].Visible = !elementReadonly;
        }

        public override void hideSomeControls()
        {
            base.hideSomeControls();
            this.dataGridView1.Columns["price"].Visible = false;
            this.dataGridView1.Columns["totalPrice"].Visible = false;
        }

        //set
        protected override void setRecord(DataGridViewRow row, ProductCirculationRecord rec) {
            ProductStainlessCirculationRecord record = rec as ProductStainlessCirculationRecord;
            row.Cells["ID"].Value = record.ID;
            row.Cells["product"].Value = new LookupArg(record.ProductID, record.ProductName);
            row.Cells["quantityPerPiece"].Value = record.QuantityNull?null:record.QuantityPerPiece.ToString();
            row.Cells["pieces"].Value = record.PiecesNull?null:record.Pieces.ToString();
            row.Cells["num"].Value = record.TotalNum;
            row.Cells["unit"].Value = record.Unit;
            row.Cells["price"].Value = record.Price;
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

        protected override bool getRecords(out List<ProductCirculationRecord> records)
        {
            records = new List<ProductCirculationRecord>();

            int number = this.dataGridView1.RowCount;

            double price, totalPrice;
            int quantityPerPiece, pieces, num;
            bool isQuantityNull = false, isPiecesNull = false;
            string unit;
            bool isInputCorrect = true;

            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                object productID = null;

                if (ValidateUtility.getLookupValue(row.Cells["product"], out productID) == false 
                    || ValidateUtility.getInt(row.Cells["quantityPerPiece"], false, true, out quantityPerPiece, out isQuantityNull) == false
                    || ValidateUtility.getInt(row.Cells["pieces"], false, true, out pieces, out isPiecesNull) == false
                    || ValidateUtility.getInt(row.Cells["num"], true, true, out num) == false
                    || ValidateUtility.getString(row.Cells["unit"], false, out unit) == false
                    || ValidateUtility.getDouble(row.Cells["price"], out price) == false
                    || ValidateUtility.getDouble(row.Cells["totalPrice"], out totalPrice) == false)
                    return false;
                ProductStainlessCirculationRecord record = new ProductStainlessCirculationRecord();

                LookupArg arg = ((row.Cells["product"] as DataGridViewLookupCell).EditedValue as LookupArg);
                record.ProductID = (int)arg.Value;
                record.ProductName = arg.Text;
                
                record.QuantityPerPiece = quantityPerPiece;
                record.QuantityNull = isQuantityNull;

                record.Pieces = pieces;
                record.PiecesNull = isPiecesNull;

                record.TotalNum = num;
                record.Unit = unit;
                record.Price = price;
                record.TotalPrice = totalPrice;
                
                records.Add(record);
            }

            return isInputCorrect;
        }

    }
}