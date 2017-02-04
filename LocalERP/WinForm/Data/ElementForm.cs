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
    public partial class ElementForm : Form
    {
        //openMode 0:add   1:edit
        private int openMode = 0;
        private int elementID = 0;

        public delegate void ModifiedComplete();
        public event ModifiedComplete modifiedComplete;

        public ElementForm(int mode, int id)
        {
            openMode = mode;
            elementID = id;
            InitializeComponent();
        }

        private void ElementForm_Load(object sender, EventArgs e)
        {
            if (openMode == 1)
            {
                this.label4.Text = "编辑 ID:" + elementID;

                Element element = ElementDao.getInstance().FindByID(elementID);
                this.textBox_name.Text = element.Name;
                this.textBox_price.Text = element.Price.ToString();
                this.textBox_comment.Text = element.Note;
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

        private bool getElement(out Element element) {
            double price;
            string name;
            bool isNameCorrect = this.getName(out name);
            bool isPriceCorrect = this.getPrice(out price);
            if ( isNameCorrect && isPriceCorrect)
            {
                element = new Element(name, price, this.textBox_comment.Text);
                element.ID = elementID;
                return true;
            }
            else
            {
                element = null;
                return false;
            }
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
            Element element = null;
            if (this.getElement(out element) == false)
                return;

            if (openMode == 0) {
                ElementDao.getInstance().Insert(element);
                if (modifiedComplete != null)
                    modifiedComplete.Invoke();
                MessageBox.Show("保存配件成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else if (openMode == 1) {
                ElementDao.getInstance().Update(element);
                if (modifiedComplete != null)
                    modifiedComplete.Invoke();
                MessageBox.Show("修改配件成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}