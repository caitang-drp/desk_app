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

            ControlUtility.initTree(this.comboBoxTree1.tvTreeView, CategoryTableName.CustomerCategory);
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
            this.textBox_arrear.Text = customer.arrear.ToString();

            this.comboBoxTree1.setSelectNode(customer.Parent.ToString());
        }

        private void clearCustomer() {
            this.label4.Text = "新增";

            this.textBox_name.Text = "";
            this.textBox_comment.Text = "";
            this.textBox_tel.Text = "";
            this.textBox_phone.Text = "";
            this.textBox_address.Text = "";
            this.textBox_arrear.Text = "";

            this.comboBoxTree1.setSelectNode(null);
        }

        public override void refresh()
        {
            ControlUtility.initTree(this.comboBoxTree1.tvTreeView, CategoryTableName.CustomerCategory);
        }

        /// <summary>
        /// get value from control with valiadating
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /*
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
        }*/

        private bool getCategoryID(out int categoryID)
        {
            if (string.IsNullOrEmpty(this.comboBoxTree1.Text) || this.comboBoxTree1.SelectedNode == null)
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
            double arrear;

            bool isNameCorrect = ValidateUtility.getName(this.textBox_name, this.errorProvider1, out name);
            bool isCategoryCorrect = this.getCategoryID(out categoryID);
            bool isArrearCorrent = ValidateUtility.getDouble(this.textBox_arrear, this.errorProvider1, false, false, out arrear);
            
            if ( isNameCorrect && isCategoryCorrect && isArrearCorrent)
            {
                customer = new Customer(name, this.textBox_comment.Text, this.textBox_tel.Text, 
                    this.textBox_phone.Text, this.textBox_address.Text, categoryID, arrear);
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

            //double arrearDif = 0;

            if (openMode == 0) {
                int customerId;
                bool insertResult = CustomerDao.getInstance().Insert(customer, out customerId);
                if (customer.arrear != 0 && insertResult == true)
                    generatePayReceipt(customer.arrear, 0, customerId);
                MessageBox.Show("保存来往单位成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);    

            }
            else if (openMode == 1) {
                double previousArrear = CustomerDao.getInstance().FindByID(this.customerID).arrear;
                CustomerDao.getInstance().Update(customer);
                if (customer.arrear != previousArrear)
                    generatePayReceipt(customer.arrear - previousArrear, previousArrear, this.customerID);

                MessageBox.Show("修改来往单位成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            this.invokeUpdateNotify(UpdateType.CustomerUpdate);
            this.Close();
        }

        private void generatePayReceipt(double arrearDif, double previousArrear, int customerID) {
            PayReceipt payReceipt = new PayReceipt();
            payReceipt.bill_type = PayReceipt.BillType.ChangeArrear;
            payReceipt.bill_time = DateTime.Now;
            payReceipt.cashDirection = arrearDif > 0 ? -1 : 1;
            payReceipt.arrearDirection = previousArrear + arrearDif > 0 ? 1 : -1;

            int max = PayReceiptDao.getInstance().getMaxCode("QKTZ");
            payReceipt.serial = string.Format("{0}-{1}-{2:0000}", "QKTZ", DateTime.Now.ToString("yyyyMMdd"), max + 1);

            payReceipt.customer_id = customerID;

            payReceipt.amount = Math.Abs(arrearDif);
            //previousArrear用的是正数表示
            payReceipt.previousArrears = Math.Round(previousArrear * payReceipt.arrearDirection, 2);

            payReceipt.status = 4;
            payReceipt.comment = "修改欠款数据时自动生成";
            PayReceiptDao.getInstance().Insert(payReceipt, out payReceipt.id);
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}