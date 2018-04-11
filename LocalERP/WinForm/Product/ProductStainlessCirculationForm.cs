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
            this.label1_tip.Text = "����ÿ���������͡���������Ϊ�գ�";

            DataGridViewTextBoxColumn ID = new DataGridViewTextBoxColumn();
            DataGridViewCheckBoxColumn check = new DataGridViewCheckBoxColumn();
            DataGridViewLookupColumn product = new DataGridViewLookupColumn();

            DataGridViewTextBoxColumn serial = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn quantityPerPiece = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn pieces = new DataGridViewTextBoxColumn();

            DataGridViewTextBoxColumn num = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn unit = new DataGridViewTextBoxColumn();
            DataGridViewComboBoxEditColumn price = new DataGridViewComboBoxEditColumn();

            DataGridViewTextBoxColumn totalPrice = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn comment = new DataGridViewTextBoxColumn();

            ID.HeaderText = "ID";
            ID.Name = "ID";
            ID.Visible = false;
            ID.Width = 60;

            check.HeaderText = "ѡ��";
            check.Name = "check";
            check.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            check.Width = 46;

            product.HeaderText = "��Ʒ";
            //product.LookupForm = null;
            product.Name = "product";
            product.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            product.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            product.Width = 140;

            serial.HeaderText = "����";
            serial.Name = "serial";
            serial.Width = 80;

            quantityPerPiece.HeaderText = "ÿ������";
            quantityPerPiece.Name = "quantityPerPiece";
            quantityPerPiece.Width = 90;

            pieces.HeaderText = "����";
            pieces.Name = "pieces";
            pieces.Width = 60;

            num.HeaderText = "����";
            num.Name = "num";
            num.Width = 100;

            unit.HeaderText = "��λ";
            unit.Name = "unit";
            unit.Width = 100;

            price.HeaderText = this.conf.business + "����/Ԫ";
            price.Name = "price";
            price.Width = 140;

            totalPrice.HeaderText = "С��/Ԫ";
            totalPrice.Name = "totalPrice";

            comment.HeaderText = "��ע";
            comment.Name = "comment";
            comment.Width = 100;

            dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { ID, check, product, serial, quantityPerPiece, pieces, num, unit, price, totalPrice, comment });

            //2017-02-28:����ط��ٴγ���bug���Լ�д�Ŀؼ����ǵ���

            //�ر�ע�⣺�������ط���ProductCIForm�����½����õ�����ǰ�Ĵ���
            //�ǾͿ����ж��CirculationForm��DataGridViewLookupColumnָ��ͬһ��ProductCIForm
            //��ô����ǰ��CirculationFormû�����ٵ�����£�ProductCIForm�ͻᴥ����ǰ��valueChanged�¼����Ӷ������쳣
            //(dgv.Columns["product"] as DataGridViewLookupColumn).LookupForm = FormSingletonFactory.getInstance().getProductCIForm_select();
        }

        protected override void initDatagridviewEnable(bool elementReadonly)
        {
            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                setCellEnable(row.Cells["product"], !elementReadonly);
                setCellEnable(row.Cells["quantityPerPiece"], !elementReadonly);
                setCellEnable(row.Cells["pieces"], !elementReadonly);
                setCellEnable(row.Cells["num"], !elementReadonly);
                setCellEnable(row.Cells["unit"], !elementReadonly);
                setCellEnable(row.Cells["price"], !elementReadonly);
                setCellEnable(row.Cells["comment"], !elementReadonly);
                
                setCellEnable(row.Cells["totalPrice"], false);
                setCellEnable(row.Cells["serial"], false);
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
            row.Cells["serial"].Value = record.Serial;
            row.Cells["quantityPerPiece"].Value = record.QuantityNull?null:record.QuantityPerPiece.ToString();
            row.Cells["pieces"].Value = record.PiecesNull?null:record.Pieces.ToString();
            row.Cells["num"].Value = record.TotalNum;
            row.Cells["unit"].Value = record.Unit;
            row.Cells["price"].Value = record.Price.ToString();
            row.Cells["totalPrice"].Value = record.TotalPrice;
            row.Cells["comment"].Value = record.Comment;
        }

        //for event: caculate total price
        protected override void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control.GetType().Equals(typeof(DataGridViewTextBoxEditingControl)))//cellΪ��TextBoxʱ
            {
                e.CellStyle.BackColor = Color.FromName("window");
                DataGridViewTextBoxEditingControl editingControl = e.Control as DataGridViewTextBoxEditingControl;

                editingControl.TextChanged -= new EventHandler(editingControl_TextChanged);
                editingControl.TextChanged += new EventHandler(editingControl_TextChanged);
                
            }
            else if (e.Control.GetType().Equals(typeof(DataGridViewComboBoxEditEditingControl)))//cellΪpriceʱ
            {
                e.CellStyle.BackColor = Color.FromName("window");

                DataGridViewComboBoxEditEditingControl editingControl = e.Control as DataGridViewComboBoxEditEditingControl;
                editingControl.Items.Clear();
                int productID = 0, customerID = 0;

                DataGridViewLookupCell cell = editingControl.EditingControlDataGridView.Rows[editingControl.EditingControlRowIndex].Cells["product"] as DataGridViewLookupCell;
                if (cell.Value != null && !string.IsNullOrEmpty((cell.Value as LookupArg).ArgName))
                    productID = (int)(cell.Value as LookupArg).Value;
                if (this.lookupText1.LookupArg != null)
                    int.TryParse(this.lookupText1.LookupArg.Value.ToString(), out customerID);

                if (productID != 0 && customerID != 0)
                {
                    List<string> prices = ProductStainlessCirculationRecordDao.getInstance().FindPriceList((int)conf.type, productID, customerID);
                    //items�Ƿ���column��
                    editingControl.Items.AddRange(prices.ToArray());
                }

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
            //File.AppendAllText("e:\\debug.txt", string.Format("value changed, control hash code={0}, control name={1}\r\n", control.GetHashCode(), control.Name));
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
                        //stone ��ʱ�ر�
                        control.EditingControlDataGridView.Rows[control.EditingControlRowIndex].Cells["serial"].Value = product.Serial;
                        control.EditingControlDataGridView.Rows[control.EditingControlRowIndex].Cells["quantityPerPiece"].Value = product.QuantityPerPiece;
                        control.EditingControlDataGridView.Rows[control.EditingControlRowIndex].Cells["unit"].Value = product.Unit;
                        if (conf.type == ProductCirculation.CirculationType.sell || conf.type == ProductCirculation.CirculationType.sellBack)
                            //����Ҫ�Ӹ�.ToString()�������Զ���Cell��value���Ͳ���һ��
                            control.EditingControlDataGridView.Rows[control.EditingControlRowIndex].Cells["price"].Value = product.PriceSell.ToString();
                        else
                            control.EditingControlDataGridView.Rows[control.EditingControlRowIndex].Cells["price"].Value = product.PricePurchase.ToString();
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
            //���senderΪprice�ģ�controlΪ�գ�û��Ӱ��
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
            string unit, comment;
            bool isInputCorrect = true;

            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                object productID = null;
                int ID = 0;
                if (ValidateUtility.getLookupValue(row.Cells["product"], out productID) == false 
                    || ValidateUtility.getInt(row.Cells["ID"], false, true, out ID) == false
                    || ValidateUtility.getInt(row.Cells["quantityPerPiece"], false, true, out quantityPerPiece, out isQuantityNull) == false
                    || ValidateUtility.getInt(row.Cells["pieces"], false, true, out pieces, out isPiecesNull) == false
                    || ValidateUtility.getInt(row.Cells["num"], true, true, out num) == false
                    || ValidateUtility.getString(row.Cells["unit"], false, out unit) == false
                    || ValidateUtility.getDouble(row.Cells["price"], out price) == false
                    || ValidateUtility.getDouble(row.Cells["totalPrice"], out totalPrice) == false
                    || ValidateUtility.getString(row.Cells["comment"], false, out comment) == false)
                    return false;
                ProductStainlessCirculationRecord record = new ProductStainlessCirculationRecord();

                LookupArg arg = ((row.Cells["product"] as DataGridViewLookupCell).EditedValue as LookupArg);
                record.ProductID = (int)arg.Value;
                record.ProductName = arg.Text;

                record.ID = ID;

                string serial;
                ValidateUtility.getString(row.Cells["serial"], false, out serial);
                record.Serial = serial;
                
                record.QuantityPerPiece = quantityPerPiece;
                record.QuantityNull = isQuantityNull;

                record.Pieces = pieces;
                record.PiecesNull = isPiecesNull;

                record.TotalNum = num;
                record.Unit = unit;
                record.Price = price;
                record.TotalPrice = totalPrice;
                record.Comment = comment;

                records.Add(record);
            }

            return isInputCorrect;
        }

        protected override void updateCostAndProfit(ProductCirculation cir, ProductCirculationRecord record)
        {
            /*********���������ͳɱ��ܼ�**********/
            ProductStainlessDao stainlessDao = cirDao.getProductDao() as ProductStainlessDao;
            ProductStainless stainless = stainlessDao.FindByID(record.ProductID);

            int leftNum = stainless.Num + conf.productDirection * record.TotalNum;

            //ֻ�вɹ��������Ҫ���³ɱ��ۣ�ͨ���ɱ������¼������ֳ��ո��Ĳ��
            //���ۡ������˻����ɹ��˻�ͨ������ļ����������ո��Ĳ��
            //�ɹ��˻�֮���Բ����¼���ɱ��ۣ�����Ϊ����˻��۸�ܸߵĻ������ܵ��³ɱ���Ϊ����
            if (conf.type == ProductCirculation.CirculationType.purchase){
                double totalCost = stainless.PriceCost * stainless.Num + conf.productDirection * record.Price * record.TotalNum;
                if (leftNum != 0)
                {
                    double cost = totalCost / leftNum;
                    stainless.PriceCost = cost;
                }
            }

            stainless.Num = stainless.Num + conf.productDirection * record.TotalNum;
            stainlessDao.Update(stainless);


            /*************����������¼**********/
            if (conf.type == ProductCirculation.CirculationType.sell || conf.type == ProductCirculation.CirculationType.sellBack || conf.type == ProductCirculation.CirculationType.purchaseBack)
            {
                SellProfit profit = new SellProfit(cir, record, stainless.PriceCost);
                SellProfitDao.getInstance().Insert(profit);
            }
        }

        //����
        protected override void cancelUpdateCostAndProfit(ProductCirculation cir, ProductCirculationRecord record)
        {
            /*********��������**********/
            ProductStainlessDao stainlessDao = cirDao.getProductDao() as ProductStainlessDao;
            ProductStainless stainless = stainlessDao.FindByID(record.ProductID);

            stainless.Num = stainless.Num - conf.productDirection * record.TotalNum;
            stainlessDao.Update(stainless);
        }

    }
}