using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using LocalERP.DataAccess.Utility;
using BSE.Windows.Forms;

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

            this.addPanderPanel(true, LabelUtility.PURCHASE_MANAGE, "purchase").Controls.Add(this.getTreeView(new string[] { LabelUtility.PURCHASE_LIST, LabelUtility.PURCHASE, LabelUtility.PURCHASE_BACK }));
            
            this.addPanderPanel(false, LabelUtility.MANU_MANAGE, "manu").Controls.Add(this.getTreeView( new string[] { LabelUtility.MANUFACTURE_LIST, LabelUtility.MANU_COST, LabelUtility.MANU_IN }));

            this.addPanderPanel(false, LabelUtility.SELL_MANAGE, "sell").Controls.Add(this.getTreeView(new string[] { LabelUtility.SELL_LIST, LabelUtility.SELL, LabelUtility.SELL_BACK }));

            this.addPanderPanel(false, LabelUtility.LIB_MANAGE, "lib").Controls.Add(this.getTreeView(new string[] { LabelUtility.LIB_LIST, LabelUtility.LIB_OVERFLOW, LabelUtility.LIB_LOSS }));

            this.addPanderPanel(false, LabelUtility.CASH_MANAGE, "cash").Controls.Add(this.getTreeView(new string[] { LabelUtility.CASH_LIST, LabelUtility.CASH_PAY, LabelUtility.CASH_PAY_REFUND, LabelUtility.CASH_RECEIPT, LabelUtility.CASH_RECEIPT_REFUND, LabelUtility.CASH_OTHER_PAY, LabelUtility.CASH_OTHER_RECEIPT }));

            this.addPanderPanel(false, LabelUtility.QUERY_MANAGE, "query").Controls.Add(this.getTreeView(new string[] { LabelUtility.QUERY_PRODUCT_DETAIL, LabelUtility.STATISTIC_PRODUCT, LabelUtility.QUERY_CASH_DETAIL, LabelUtility.STATISTIC_CASH}));

            this.addPanderPanel(false, LabelUtility.DATA_SETTING, "data").Controls.Add(this.getTreeView(new string[] { LabelUtility.DATA_PRODUCT, LabelUtility.DATA_CUSTOMER, LabelUtility.DATA_COMPANY }));
            
            /*
            this.addPanderPanel(true, LabelUtility.CONSUME_MANAGE, "consume").Controls.Add(this.getTreeView(new string[] {LabelUtility.CONSUME_LIST, LabelUtility.CONSUME_ADD }));
            this.addPanderPanel(false, LabelUtility.CARD_MANAGE, "card").Controls.Add(this.getTreeView(new string[] { LabelUtility.CARD_LIST, LabelUtility.CARD_ADD }));
            */
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

        //stone: 2019-10-29
        private XPanderPanel addPanderPanel(bool expand, string text, string name)
        {
            XPanderPanel xPanderPanel = new BSE.Windows.Forms.XPanderPanel();

            xPanderPanel.BackColor = System.Drawing.SystemColors.Window;
            xPanderPanel.CaptionFont = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            xPanderPanel.CaptionForeColor = System.Drawing.SystemColors.ControlText;
            xPanderPanel.ColorCaptionGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(232)))), ((int)(((byte)(252)))));
            xPanderPanel.ColorCaptionGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(177)))), ((int)(((byte)(230)))));
            xPanderPanel.ColorCaptionGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(210)))), ((int)(((byte)(243)))));
            xPanderPanel.ColorScheme = BSE.Windows.Forms.ColorScheme.Custom;
            xPanderPanel.Expand = expand;
            xPanderPanel.ForeColor = System.Drawing.SystemColors.ControlText;
            xPanderPanel.Image = global::LocalERP.Properties.Resources.apply16;
            xPanderPanel.Name = "xPanderPanel_"+name;
            xPanderPanel.Padding = new System.Windows.Forms.Padding(12, 33, 12, 11);
            xPanderPanel.TabIndex = 5;
            xPanderPanel.Text = text;

            this.xPanderPanelList1.Controls.Add(xPanderPanel);

            return xPanderPanel;
        }

    }
}
