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
                new string[] { LabelUtility.PURCHASE_LIST, LabelUtility.PURCHASE, LabelUtility.PURCHASE_BACK });
            this.xPanderPanel_pur.Controls.Add(treeView_purchase);

            TreeView treeView_manufacture = this.getTreeView(
                new string[] { LabelUtility.MANUFACTURE_LIST, LabelUtility.MANU_COST, LabelUtility.MANU_IN });
            this.xPanderPanel_manu.Controls.Add(treeView_manufacture);

            TreeView treeView_sell = this.getTreeView(
                new string[] { LabelUtility.SELL_LIST, LabelUtility.SELL, LabelUtility.SELL_BACK });
            this.xPanderPanel_sell.Controls.Add(treeView_sell);

            TreeView treeView_lib = this.getTreeView(
                new string[] {LabelUtility.LIB_LIST, LabelUtility.LIB_OVERFLOW, LabelUtility.LIB_LOSS });
            this.xPanderPanel_lib.Controls.Add(treeView_lib);

            // should pay, should receipt
            TreeView treeView_should_pay_receipt = this.getTreeView(
                new string[] { LabelUtility.CASH_LIST, LabelUtility.CASH_PAY, LabelUtility.CASH_PAY_REFUND, LabelUtility.CASH_RECEIPT, LabelUtility.CASH_RECEIPT_REFUND, LabelUtility.CASH_OTHER_PAY, LabelUtility.CASH_OTHER_RECEIPT });
            this.xPanderPanel_should_pay_receipt.Controls.Add(treeView_should_pay_receipt);

            TreeView treeView_queryStatistic = this.getTreeView(
                new string[] { /*DataUtility.QUERY_LIB,*/ LabelUtility.QUERY_PRODUCT_DETAIL, LabelUtility.STATISTIC_PRODUCT, LabelUtility.QUERY_CASH_DETAIL, LabelUtility.STATISTIC_CASH/*, LabelUtility.STATISTIC_PROFIT*/});
            this.xPanderPanel_query.Controls.Add(treeView_queryStatistic);

            //tree4
            TreeView treeView_data = this.getTreeView(
                new string[] { LabelUtility.DATA_PRODUCT, LabelUtility.DATA_CUSTOMER, LabelUtility.DATA_COMPANY/*, DataUtility.ACCOUNT_INPUT, DataUtility.ACCOUNT_OPEN*/ });
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
