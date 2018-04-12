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
using LocalERP.WinForm.Utility;
using LocalERP.DataAccess.Utility;

namespace LocalERP.WinForm
{
    public partial class ProductStainlessForm : MyDockContent
    {
        //openMode 0:add   1:edit
        private int openMode = 0;
        private int productID = 0;

        public ProductStainlessForm()
        {
            InitializeComponent();

            openMode = 0;
            productID = 0;

            ProductStainlessCategoryItemProxy categoryItemProxy = new ProductStainlessCategoryItemProxy();
            categoryItemProxy.initTree(this.comboBoxTree_category.tvTreeView);
        }

        public void reload(int openMode, int ID) {
            this.openMode = openMode;
            this.productID = ID;

            if (openMode == 0)
            {
                clearProduct();
                return;
            }

            initProduct();
        }

        private void initProduct()
        {
            this.label4.Text = "编辑货品, ID:" + productID;

            ProductStainless product = ProductStainlessDao.getInstance().FindByID(productID);
            this.textBox_name.Text = product.Name;
            this.textBox_serial.Text = product.Serial;

            this.comboBoxTree_category.setSelectNode(product.CategoryID.ToString());

            this.textBox_purchasePrice.Text = product.PricePurchase.ToString();
            this.textBox_sellPrice.Text = product.PriceSell.ToString();
            this.textBox_cost.Text = product.PriceCost.ToString();

            this.textBox_quantityPerPiece.Text = product.QuantityPerPiece.ToString();
            this.comboBox_unit.Text = product.Unit;

            this.textBox_comment.Text = product.Comment;
            this.radioButton2.Checked = product.Disable;
        }

        private void clearProduct()
        {
            this.label4.Text = "新增";
            this.textBox_name.Text = null;
            this.textBox_serial.Text = null;

            this.comboBoxTree_category.setSelectNode(null);

            this.textBox_purchasePrice.Text = null;
            this.textBox_sellPrice.Text = null;
            this.textBox_cost.Text = null;

            this.textBox_quantityPerPiece.Text = null;
            this.comboBox_unit.Text = null;
            
            this.textBox_comment.Text = "";
            this.radioButton2.Checked = false;
        }

        public override void refresh()
        {
            ProxyMgr.getInstance().getProductCIProxy().initTree(this.comboBoxTree_category.tvTreeView);
        }

        /// <summary>
        /// get value from control with valiadating
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private bool getCategoryID(out int categoryID) { 

            if(string.IsNullOrEmpty(this.comboBoxTree_category.Text) || this.comboBoxTree_category.SelectedNode == null)
            {
                this.errorProvider1.SetError(this.comboBoxTree_category, "请选择类别!");
                categoryID = 1;
                return false;
            }
            else
            {
                this.errorProvider1.SetError(this.comboBoxTree_category, string.Empty);
                categoryID = int.Parse(this.comboBoxTree_category.SelectedNode.Name);
                return true;
            }
        }

        private bool getProduct(out ProductStainless product) {
            double price_purchase, price_sell;
            string name;
            int quantityPerPiece, categoryID;

            if (ValidateUtility.getName(this.textBox_name, this.errorProvider1, out name) && 
                ValidateUtility.getDouble(this.textBox_purchasePrice, this.errorProvider1, true, true, out price_purchase) && 
                ValidateUtility.getDouble(this.textBox_sellPrice, this.errorProvider1, true,true,  out price_sell) && 
                ValidateUtility.getInt(this.textBox_quantityPerPiece, this.errorProvider1, false, true, out quantityPerPiece) &&
                this.getCategoryID(out categoryID))
            {
                product = new ProductStainless(this.textBox_serial.Text, name, categoryID, price_purchase, price_sell, this.comboBox_unit.Text, quantityPerPiece, this.textBox_comment.Text, this.radioButton2.Checked);
                product.ID = productID;
                return true;
            }
            else
            {
                product = null;
                return false;
            }
        }

        /// <summary>
        /// event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton_save_Click(object sender, EventArgs e)
        {
            ProductStainless product = null;
            if (this.getProduct(out product) == false)
                return;

            if (openMode == 0) {
                ProductStainlessDao.getInstance().Insert(product);
                MessageBox.Show("保存货品成功,在相应的类别目录下可以找到该货品!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (openMode == 1) {
                product.ID = productID;
                bool basicResult = ProductStainlessDao.getInstance().Update(product);
                if(basicResult)
                    MessageBox.Show("修改货品成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            this.invokeUpdateNotify(UpdateType.ProductUpdate);
            this.Close();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}