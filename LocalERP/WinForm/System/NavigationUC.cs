using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using LocalERP.DataAccess.Utility;

namespace LocalERP.WinForm
{
    public partial class NavigationUC : UserControl
    {
        private MainForm mainForm;
        public NavigationUC(MainForm mf)
        {
            InitializeComponent();
            this.mainForm = mf;
            this.initNavigation();
        }

        //navigation
        private void initNavigation()
        {
            TreeView treeView_purchase = this.getTreeView(
                new string[] { DataUtility.PURCHASE_LIST, DataUtility.PURCHASE, DataUtility.PURCHASE_BACK });
            this.xPanderPanel_pur.Controls.Add(treeView_purchase);

            TreeView treeView_manufacture = this.getTreeView(
                new string[] { DataUtility.MANUFACTURE_LIST, DataUtility.EASY });
            this.xPanderPanel_manu.Controls.Add(treeView_manufacture);

            TreeView treeView_sell = this.getTreeView(
                new string[] { DataUtility.SELL_LIST, DataUtility.SELL, DataUtility.SELL_BACK });
            this.xPanderPanel_sell.Controls.Add(treeView_sell);

            TreeView treeView_lib = this.getTreeView(
                new string[] {DataUtility.LIB_LIST, DataUtility.LIB_OVERFLOW, DataUtility.LIB_LOSS });
            this.xPanderPanel_lib.Controls.Add(treeView_lib);

            // should pay, should receipt
            TreeView treeView_should_pay_receipt = this.getTreeView(
                new string[] { DataUtility.CASH_LIST, DataUtility.CASH_PAY, DataUtility.CASH_PAY_REFUND, DataUtility.CASH_RECEIPT, DataUtility.CASH_RECEIPT_REFUND, DataUtility.CASH_OTHER_PAY, DataUtility.CASH_OTHER_RECEIPT });
            this.xPanderPanel_should_pay_receipt.Controls.Add(treeView_should_pay_receipt);

            TreeView treeView_queryStatistic = this.getTreeView(
                new string[] { /*DataUtility.QUERY_LIB,*/ DataUtility.QUERY_PRODUCT_DETAIL, DataUtility.STATISTIC_PRODUCT, DataUtility.QUERY_CASH_DETAIL, /*DataUtility.STATISTIC_DEBT,*/ DataUtility.STATISTIC_PROFIT});
            this.xPanderPanel_query.Controls.Add(treeView_queryStatistic);

            //tree4
            TreeView treeView_data = this.getTreeView(
                new string[] { DataUtility.DATA_PRODUCT, DataUtility.DATA_CUSTOMER, DataUtility.DATA_COMPANY/*, DataUtility.ACCOUNT_INPUT, DataUtility.ACCOUNT_OPEN*/ });
            this.xPanderPanel_data.Controls.Add(treeView_data);

        }

        void treeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            this.mainForm.setForm(e.Node.Text);
        }

        private TreeView getTreeView(String [] nodes)
        {
            TreeView treeView = new TreeView();
            treeView.ShowRootLines = false;
            treeView.ImageList = this.imageList1;
            treeView.BorderStyle = BorderStyle.None;
            treeView.Dock = DockStyle.Fill;
            treeView.HotTracking = true;
            treeView.HideSelection = false;
            treeView.ItemHeight = 26;

            foreach (String node in nodes) {
                treeView.Nodes.Add(new TreeNode(node, 0, 1));
            }

            treeView.NodeMouseClick += new TreeNodeMouseClickEventHandler(treeView_NodeMouseClick);

            return treeView;
        }

    }
}
