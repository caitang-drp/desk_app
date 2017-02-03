using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using WeifenLuo.WinFormsUI.Docking;

namespace LocalERP.WinForm
{
    public partial class DataSettingForm : DockContent
    {
        public delegate void DataSettingChanged();
        public event DataSettingChanged dataSettingChanged;

        public DataSettingForm()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.treeView1.ExpandAll();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.splitContainer1.Panel2.Controls.Clear();

            int index = this.treeView1.SelectedNode.Index;
            switch (index) { 
                case 0:
                    ElementUC elementUC = new ElementUC();
                    elementUC.elementChanged += new ElementUC.ElementChanged(dataSetting_Changed);
                    elementUC.Dock = System.Windows.Forms.DockStyle.Fill;
                    this.splitContainer1.Panel2.Controls.Add(elementUC);
                    break;
                case 1:
                    ProductListUC productUC = new ProductListUC();
                    productUC.productChanged += new ProductListUC.ProductChanged(dataSetting_Changed);
                    productUC.Dock = System.Windows.Forms.DockStyle.Fill;
                    this.splitContainer1.Panel2.Controls.Add(productUC);
                    break;
                case 2:
                    CustomerUC customerUC = new CustomerUC();
                    customerUC.customerChanged+=new CustomerUC.CustomerChanged(dataSettingChanged);
                    customerUC.Dock = System.Windows.Forms.DockStyle.Fill;
                    this.splitContainer1.Panel2.Controls.Add(customerUC);
                    break;
                default:
                    break;
            }
        }

        void dataSetting_Changed()
        {
            if (dataSettingChanged != null)
                dataSettingChanged.Invoke();
        }
    }
}