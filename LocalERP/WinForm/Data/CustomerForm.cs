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

namespace LocalERP.WinForm
{
    public partial class CustomerForm : MyDockContent
    {
        //openMode 0:add   1:edit
        private int openMode = 0;
        private int customerID = 0;

        public CustomerForm()
        {
            openMode = 0;
            customerID = 0;

            InitializeComponent();

            ProxyMgr.getInstance().getCustomerCIProxy().initTree(this.comboBoxTree1.tvTreeView);
        }

        public void reload(int mode, int id) {
            this.openMode = mode;
            this.customerID = id;

            if (mode == 0)
                clearCustomer();
            else if (mode == 1)
                initCustomer();
        }

        private void initCustomer() {
            this.label4.Text = "编辑 ID:" + customerID;

            Customer customer = CustomerDao.getInstance().FindByID(customerID);

            this.textBox_name.Text = customer.Name;
            this.textBox_comment.Text = customer.Comment;
            this.textBox_tel.Text = customer.Tel;
            this.textBox_phone.Text = customer.Phone;
            this.textBox_address.Text = customer.Address;

            this.comboBoxTree1.setSelectNode(customer.Parent.ToString());
        }

        private void clearCustomer() {
            this.label4.Text = "新增";

            this.textBox_name.Text = "";
            this.textBox_comment.Text = "";
            this.textBox_tel.Text = "";
            this.textBox_phone.Text = "";
            this.textBox_address.Text = "";

            this.comboBoxTree1.setSelectNode(null);
        }

        public override void refresh()
        {
            ProxyMgr.getInstance().getCustomerCIProxy().initTree(this.comboBoxTree1.tvTreeView);
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

        private bool getCategoryID(out int categoryID)
        {
            if (this.comboBoxTree1.SelectedNode == null)
            {
                this.errorProvider1.SetError(this.comboBoxTree1, "请选择类型!");
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

        private bool getCustomer(out Customer customer) {
            string name;
            int categoryID;
            bool isNameCorrect = this.getName(out name);
            bool isCategoryCorrect = this.getCategoryID(out categoryID);

            if ( isNameCorrect && isCategoryCorrect)
            {
                customer = new Customer(name, this.textBox_comment.Text, this.textBox_tel.Text, 
                    this.textBox_phone.Text, this.textBox_address.Text, categoryID);
                customer.ID = customerID;
                return true;
            }
            else
            {
                customer = null;
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
            Customer customer = null;
            if (this.getCustomer(out customer) == false)
                return;

            if (openMode == 0) {
                CustomerDao.getInstance().Insert(customer);
                MessageBox.Show("保存来往单位成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);    
            }
            else if (openMode == 1) {
                CustomerDao.getInstance().Update(customer);
                MessageBox.Show("修改来往单位成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            this.invokeChangeNotify(NotifyType.CustomerUpdate);
            this.Close();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}