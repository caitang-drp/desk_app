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

namespace LocalERP.WinForm
{
    public partial class ProductClothesForm : MyDockContent
    {
        //openMode 0:add   1:edit
        private int openMode = 0;
        private int productID = 0;

        private bool attributeChanged = false;

        public ProductClothesForm()
        {
            InitializeComponent();

            openMode = 0;
            productID = 0;

            this.pickValue_color.initValue(CharactorValueDao.getInstance().FindList(1), "Name", "Id");
            this.pickValue_size.initValue(CharactorValueDao.getInstance().FindList(2), "Name", "Id");

            this.pickValue_color.selectValueChanged += new PickValue.SelectValueChanged(pickValue_color_selectValueChanged);
            this.pickValue_size.selectValueChanged += new PickValue.SelectValueChanged(pickValue_color_selectValueChanged);

            ControlUtility.initTree(this.comboBoxTree1.tvTreeView, CategoryTableName.ProductStainlessCategory);
        }

        void pickValue_color_selectValueChanged()
        {
            attributeChanged = true;
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

            Product product = ProductClothesDao.getInstance().FindByID(productID);
            this.textBox_name.Text = product.Name;
            this.textBox_price.Text = product.PricePurchase.ToString();
            this.textBox_comment.Text = product.Comment;

            this.comboBoxTree1.setSelectNode(product.CategoryID.ToString());

            this.pickValue_color.allToLeft();
            this.pickValue_color.setSelectItems(ProductClothesDao.getInstance().findAttributes(productID, 1));
            this.pickValue_size.allToLeft();
            this.pickValue_size.setSelectItems(ProductClothesDao.getInstance().findAttributes(productID, 2));

            if (ProductClothesCirculationRecordDao.getInstance().FindCount(productID) > 0)
                this.setPickValue(false);
            else
                this.setPickValue(true);

            attributeChanged = false;
        }

        private void setPickValue(bool value) {
            this.pickValue_color.Enabled = value;
            this.button_color.Enabled = value;
            this.pickValue_size.Enabled = value;
            this.button_size.Enabled = value;
        
        }

        private void clearProduct()
        {
            this.label4.Text = "新增";
            this.textBox_name.Text = "";
            this.textBox_price.Text = "";
            this.textBox_comment.Text = "";
            this.comboBoxTree1.setSelectNode(null);
            this.pickValue_color.allToLeft();
            this.pickValue_size.allToLeft();

            this.attributeChanged = false;

            this.setPickValue(true);
        }

        public override void refresh()
        {
            ControlUtility.initTree(this.comboBoxTree1.tvTreeView, CategoryTableName.ProductStainlessCategory);
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

        private bool getCategoryID(out int categoryID) { 
            if(this.comboBoxTree1.SelectedNode == null)
            {
                this.errorProvider1.SetError(this.comboBoxTree1, "请选择类别!");
                categoryID = 1;
                return false;
            }
            else
            {
                this.errorProvider1.SetError(this.comboBoxTree1, string.Empty);
                categoryID = int.Parse(this.comboBoxTree1.SelectedNode.Name);
                return true;
            }
        }

        private bool getProduct(out Product product) {
            double price;
            string name;
            int categoryID;

            bool isNameCorrect = this.getName(out name);
            bool isPriceCorrect = this.getPrice(out price);
            bool isCategoryCorrect = this.getCategoryID(out categoryID);
            if ( isNameCorrect && isPriceCorrect && isCategoryCorrect)
            {
                product = new Product(name, categoryID, price, this.textBox_comment.Text);
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
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Product product = null;
            if (this.getProduct(out product) == false)
                return;

            if (openMode == 0) {
                ProductClothesDao.getInstance().Insert(product, this.pickValue_color.getListRight(), this.pickValue_size.getListRight());
                MessageBox.Show("保存货品成功,在相应的类别目录下可以找到该货品!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (openMode == 1) {
                product.ID = productID;
                bool basicResult = ProductClothesDao.getInstance().UpdateBasicInfo(product);
                bool basicAttr = true;
                if (attributeChanged)
                    basicAttr = ProductClothesDao.getInstance().UpdateAttributes(productID, product, this.pickValue_color.getListRight(), this.pickValue_size.getListRight());
                if(basicResult && basicAttr)
                    MessageBox.Show("修改货品成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("该货品已被单据引用，无法修改颜色和尺码等属性!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.attributeChanged = false;
            this.invokeUpdateNotify(UpdateType.ProductUpdate);
            this.Close();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_charactor_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            int charactorId = 1;
            if(button.Name.Equals("button_size"))
                charactorId = 2;
            CharactorForm form = new CharactorForm(charactorId);
            form.FormClosed += new FormClosedEventHandler(form_FormClosed);
            form.ShowDialog();
        }

        void form_FormClosed(object sender, FormClosedEventArgs e)
        {
            CharactorForm form = sender as CharactorForm;
            if (form.Modify == true)
            {
                PickValue pickValue = null;
                if (form.CharactorId == 1)
                    pickValue = pickValue_color;
                else
                    pickValue = pickValue_size;

                List<CharactorValue> values = pickValue.getListRight();
                pickValue.initValue(CharactorValueDao.getInstance().FindList(form.CharactorId), "Name", "Id");

                pickValue.setSelectItems(values);
            }
        }
    }
}