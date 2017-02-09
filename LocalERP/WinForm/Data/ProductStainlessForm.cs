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
            this.label4.Text = "�༭��Ʒ, ID:" + productID;

            ProductStainless product = ProductStainlessDao.getInstance().FindByID(productID);
            this.textBox_name.Text = product.Name;
            this.textBox_purchasePrice.Text = product.PricePurchase.ToString();
            this.textBox_comment.Text = product.Comment;

            this.comboBoxTree_category.setSelectNode(product.CategoryID.ToString());
        }

        private void clearProduct()
        {/*
            this.label4.Text = "����";
            this.textBox_name.Text = "";
            this.textBox_price.Text = "";
            this.textBox_comment.Text = "";
            this.comboBoxTree1.setSelectNode(null);
            this.pickValue_color.allToLeft();
            this.pickValue_size.allToLeft();

            this.attributeChanged = false;

            this.setPickValue(true);*/
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

        private bool getName(out string name) {
            if (string.IsNullOrEmpty(this.textBox_name.Text))
            {
                this.errorProvider1.SetError(this.textBox_name, "���벻��Ϊ��!");
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
            if (string.IsNullOrEmpty(this.textBox_purchasePrice.Text) || double.TryParse(this.textBox_purchasePrice.Text, out tempPrice))
            {
                this.errorProvider1.SetError(this.textBox_purchasePrice, string.Empty);
                price = tempPrice;
                return true;
            }
            else {
                this.errorProvider1.SetError(this.textBox_purchasePrice, "����������!");
                price = tempPrice;
                return false;
            }
        }

        private bool getCategoryID(out int categoryID) { 
            if(this.comboBoxTree_category.SelectedNode == null)
            {
                this.errorProvider1.SetError(this.comboBoxTree_category, "��ѡ�����!");
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
            double price;
            string name;
            int categoryID;

            bool isNameCorrect = this.getName(out name);
            bool isPriceCorrect = this.getPrice(out price);
            bool isCategoryCorrect = this.getCategoryID(out categoryID);
            if ( isNameCorrect && isPriceCorrect && isCategoryCorrect)
            {
                product = new ProductStainless("serial", name, categoryID, price, price, "��", 10, this.textBox_comment.Text);
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
                MessageBox.Show("�����Ʒ�ɹ�,����Ӧ�����Ŀ¼�¿����ҵ�����Ʒ!", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (openMode == 1) {
                /*product.ID = productID;
                bool basicResult = ProductStainlessDao.getInstance().UpdateBasicInfo(product);
                bool basicAttr = true;
                if (attributeChanged)
                    basicAttr = ProductClothesDao.getInstance().UpdateAttributes(productID, product, this.pickValue_color.getListRight(), this.pickValue_size.getListRight());
                if(basicResult && basicAttr)
                    MessageBox.Show("�޸���Ʒ�ɹ�!", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("����Ʒ�ѱ��������ã��޷��޸���ɫ�ͳ��������!", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);*/
            }
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
        {/*
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
            }*/
        }
    }
}