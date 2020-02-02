using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using LocalERP.DataAccess.DataDAO;
using LocalERP.DataAccess.Data;
using LocalERP.DataAccess.Utility;

namespace LocalERP.WinForm
{
    public partial class WelcomeForm : DockContent
    {
        private MainForm mainForm;
        public WelcomeForm(MainForm form)
        {
            this.mainForm = form;
            InitializeComponent();

            Label label = addRow(LabelUtility.CONSUME_MANAGE, new string[] {LabelUtility.CONSUME_LIST, LabelUtility.CONSUME_ADD }, 55);
            label = addRow(LabelUtility.CARD_MANAGE, new string[] { LabelUtility.CARD_LIST, LabelUtility.CARD_ADD }, label.Location.Y + 50);
            label = addRow(LabelUtility.DATA_SETTING, new string[] { LabelUtility.DATA_CUSTOMER, LabelUtility.DATA_COMPANY }, label.Location.Y + 50);
            /*Label label = addRow(LabelUtility.PURCHASE_MANAGE, new string[] { LabelUtility.PURCHASE_LIST, LabelUtility.PURCHASE, LabelUtility.PURCHASE_BACK }, 55);
            label = addRow(LabelUtility.MANU_MANAGE, new string[] { LabelUtility.MANUFACTURE_LIST, LabelUtility.MANU_IN, LabelUtility.MANU_COST }, label.Location.Y + 50);
            label = addRow(LabelUtility.SELL_MANAGE, new string[] { LabelUtility.SELL_LIST, LabelUtility.SELL, LabelUtility.SELL_BACK }, label.Location.Y + 50);
            label = addRow(LabelUtility.CASH_MANAGE, new string[] { LabelUtility.CASH_PAY, LabelUtility.CASH_PAY_REFUND, LabelUtility.CASH_RECEIPT, LabelUtility.CASH_RECEIPT_REFUND, LabelUtility.CASH_OTHER_PAY, LabelUtility.CASH_OTHER_RECEIPT }, label.Location.Y + 50);
            label = addRow(LabelUtility.LIB_MANAGE, new string[] { LabelUtility.LIB_LIST, LabelUtility.LIB_OVERFLOW, LabelUtility.LIB_LOSS }, label.Location.Y + 50);
            label = addRow(LabelUtility.QUERY_MANAGE, new string[] { LabelUtility.QUERY_PRODUCT_DETAIL, LabelUtility.STATISTIC_PRODUCT, LabelUtility.QUERY_CASH_DETAIL, LabelUtility.STATISTIC_CASH }, label.Location.Y + 50);
            label = addRow(LabelUtility.DATA_SETTING, new string[] { LabelUtility.DATA_PRODUCT, LabelUtility.DATA_CUSTOMER, LabelUtility.DATA_COMPANY}, label.Location.Y + 50);*/
        }

        private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel linkLabel = sender as LinkLabel;
            mainForm.setForm(linkLabel.Text);
        }

        private Label addRow(String type, String [] navs, int localY) {
            Label label = new System.Windows.Forms.Label();
            
            label.Font = new System.Drawing.Font("ËÎÌו", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            label.Image = global::LocalERP.Properties.Resources.Yes;
            label.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            label.Location = new System.Drawing.Point(74, localY);
            label.Name = "label1";
            label.Size = new System.Drawing.Size(100, 18);
            label.TabIndex = 58;
            label.Text = type;
            label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            this.Controls.Add(label);

            for (int i = 0; i<navs.Length; i++) {
                LinkLabel linkLabel = new System.Windows.Forms.LinkLabel();

                linkLabel.AutoSize = true;
                linkLabel.Font = new System.Drawing.Font("ËÎÌו", 10F, System.Drawing.FontStyle.Bold);
                linkLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
                linkLabel.LinkColor = System.Drawing.Color.Green;
                linkLabel.Location = new System.Drawing.Point(207 + i* 110, label.Location.Y + 2);
                linkLabel.Name = "linkLabel"+ type +i;
                linkLabel.Size = new System.Drawing.Size(67, 14);
                linkLabel.TabIndex = 17;
                linkLabel.TabStop = true;
                linkLabel.Text = navs[i];
                linkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_LinkClicked);

                this.Controls.Add(linkLabel);
            }

            return label;
        }
    }
}