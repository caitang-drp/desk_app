using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using WeifenLuo.WinFormsUI.Docking;
using LocalERP.DataAccess.Data;
using LocalERP.UiDataProxy;
using LocalERP.DataAccess.DataDAO;
using LocalERP.WinForm.Utility;
using LocalERP.DataAccess.Utility;

namespace LocalERP.WinForm
{
    public partial class ProductClothesInputNumForm : LookupAndNotifyDockContent
    {
        private int productId;
        private List<ProductAttribute> colorAttrs;
        private List<ProductAttribute> sizeAttrs;

        public int ProductId
        {
            get { return productId; }
            set { productId = value; }
        }

        public ProductClothesInputNumForm(Form parentForm)
        {
            this.Owner = parentForm;
            InitializeComponent();
        }

        private void initDataGridView() {

            this.dataGridView1.Rows.Clear();
            for (int i = this.dataGridView1.Columns.Count - 1; i > 0; i--) {
                this.dataGridView1.Columns.RemoveAt(i);
            }
            
            sizeAttrs = ProductClothesDao.getInstance().findAttributes(productId, 2);
            foreach (ProductAttribute attr in sizeAttrs)
            {
                this.dataGridView1.Columns.Add(attr.ID.ToString(), attr.CharactorValue.Name);
                DataGridViewColumn column = this.dataGridView1.Columns[this.dataGridView1.Columns.Count - 1];
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.Width = 80;
            }
            colorAttrs = ProductClothesDao.getInstance().findAttributes(productId, 1);
            foreach (ProductAttribute attr in colorAttrs)
            {
                this.dataGridView1.Rows.Add(new object[]{attr.CharactorValue.Name});
            }

            if (sizeAttrs.Count <= 0 || colorAttrs.Count <= 0)
                this.label1.Visible = true;
            else
                this.label1.Visible = false;
        }

        public void setTitle(string name) {
            this.productNameLabel.Text = name;
        }

        public override void showDialog(object value)
        {
            this.initDataGridView();
            string type = value.GetType().Name;
            if (type.Equals("ProductCirculationRecord"))
            {
                try
                {
                    ProductCirculationRecord record = value as ProductCirculationRecord;
                    List<ProductCirculationSKURecord> records = record.SkuRecords;
                    foreach (ProductCirculationSKURecord skuRecord in records)
                    {
                        int i = 0, j = 0;
                        for (; i < colorAttrs.Count; i++)
                            if (colorAttrs[i].CharactorValueId == skuRecord.ProductSKU.Color.Id)
                                break;
                        for (; j < sizeAttrs.Count; j++)
                            if (sizeAttrs[j].CharactorValueId == skuRecord.ProductSKU.Size.Id)
                                break;
                        this.dataGridView1.Rows.Count.ToString();
                        this.dataGridView1.ColumnCount.ToString();
                        this.dataGridView1[j + 1, i].Value = skuRecord.Num;
                    }
                }
                catch { }
            }
            this.ShowDialog();

        }

        private bool getRecord(out ProductCirculationRecord record) {

            record = new ProductCirculationRecord();
            record.SkuRecords = new List<ProductCirculationSKURecord>();
            int totalNum = 0;

            for (int i=0;i<this.dataGridView1.Rows.Count;i++) {
                for (int j = 1; j < this.dataGridView1.Rows[i].Cells.Count; j++) {
                    int num = 0;
                    if (!ValidateUtility.getInt(this.dataGridView1[j, i], false, true, out num))
                        return false;
                    
                    if (num == 0)
                        continue;

                    ProductCirculationSKURecord skuRecord = new ProductCirculationSKURecord();
                    skuRecord.Num = num;
                    skuRecord.ProductSKU = ProductSKUDao.getInstance().FindByComposeID(productId, colorAttrs[i].CharactorValueId, sizeAttrs[j - 1].CharactorValueId);
                    skuRecord.ProductSKUID = skuRecord.ProductSKU.ID;
                    
                    record.SkuRecords.Add(skuRecord);

                    totalNum += num;
                }
            }

            record.ProductID = ProductId;
            record.TotalNum = totalNum;
            return true;
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            ProductCirculationRecord record = null;
            if (this.getRecord(out record))
            {
                LookupArg arg = new LookupArg(record, record.getTxt());
                this.invokeLookupCallback(arg);
                this.Close();
            }
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }


}