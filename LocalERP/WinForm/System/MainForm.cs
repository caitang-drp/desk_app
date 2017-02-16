using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using LocalERP.DataAccess.Utility;
using System.IO;
using LocalERP.DataAccess.Data;
using LocalERP.UiDataProxy;
using System.Reflection;

namespace LocalERP.WinForm
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            loadingForm = new LoadingForm();

            NavigationUC navigation = new NavigationUC(this);
            navigation.Dock = DockStyle.Fill;
            this.panel1.Controls.Add(navigation);

            getWelcomeForm().Show(this.dockPanel1);
            this.toolStripStatusLabel1.Text = "��ǰ����: " + DateTime.Now.ToShortDateString();

            FormSingletonFactory.getInstance().MainForm = this;

            Control.CheckForIllegalCrossThreadCalls = false;
        }

        //������ʾ�ȴ�����
        private LoadingForm loadingForm;

        private WelcomeForm welcomeForm = null;
        private WelcomeForm getWelcomeForm() { if (welcomeForm == null || welcomeForm.IsDisposed) welcomeForm = new WelcomeForm(this); return welcomeForm; }

        public void setForm(String buttonText) {
            this.setForm(buttonText, 0, 0);
        }

        public void setForm(String buttonText, int openMode, int ID)
        {
            switch (buttonText) {
                case DataUtility.PURCHASE_LIST:
                    FormSingletonFactory.getInstance().getProductPurchaseListForm().Show(this.dockPanel1);
                    break;
                case DataUtility.PURCHASE:
                    FormSingletonFactory.getInstance().getProductPurchaseForm().Show(this.dockPanel1);
                    FormSingletonFactory.getInstance().getProductPurchaseForm().reload(openMode, ID);
                    break;
                case DataUtility.PURCHASE_BACK:
                    FormSingletonFactory.getInstance().getProductPurchaseBackForm().Show(this.dockPanel1);
                    FormSingletonFactory.getInstance().getProductPurchaseBackForm().reload(openMode, ID);
                    break;
                case DataUtility.SELL_LIST:
                    FormSingletonFactory.getInstance().getProductSellListForm().Show(this.dockPanel1);
                    break;
                case DataUtility.SELL:
                    FormSingletonFactory.getInstance().getProductSellForm().Show(this.dockPanel1);
                    FormSingletonFactory.getInstance().getProductSellForm().reload(openMode, ID);
                    break;
                case DataUtility.SELL_BACK:
                    FormSingletonFactory.getInstance().getProductSellBackForm().Show(this.dockPanel1);
                    FormSingletonFactory.getInstance().getProductSellBackForm().reload(openMode, ID);
                    break;
                case DataUtility.LIB_LIST:
                    FormSingletonFactory.getInstance().getProductLibListForm().Show(this.dockPanel1);
                    break;
                case DataUtility.LIB_OVERFLOW:
                    FormSingletonFactory.getInstance().getProductLibOverflowForm().Show(this.dockPanel1);
                    FormSingletonFactory.getInstance().getProductLibOverflowForm().reload(openMode, ID);
                    break;
                case DataUtility.LIB_LOSS:
                    FormSingletonFactory.getInstance().getProductLibLossForm().Show(this.dockPanel1);
                    FormSingletonFactory.getInstance().getProductLibLossForm().reload(openMode, ID);
                    break;
                case DataUtility.QUERY_LIB:
                    FormSingletonFactory.getInstance().getQueryLibForm().Show(this.dockPanel1);
                    break;
                case DataUtility.QUERY_DETAIL:
                    FormSingletonFactory.getInstance().getQueryDetailForm().Show(this.dockPanel1);
                    break;
                case DataUtility.STATISTIC:
                    FormSingletonFactory.getInstance().getProductStatisticForm().Show(this.dockPanel1);
                    break;
                case DataUtility.DATA_CUSTOMER:
                    FormSingletonFactory.getInstance().getCustomerCIForm().Show(this.dockPanel1);
                    break;
                case DataUtility.DATA_PRODUCT:
                    FormSingletonFactory.getInstance().getProductCIForm().Show(this.dockPanel1);
                    break;
                case DataUtility.CASH_LIST:
                    FormSingletonFactory.getInstance().getPayReceiptListForm().Show(this.dockPanel1);
                    break;
                case DataUtility.CASH_PAY:
                    FormSingletonFactory.getInstance().getbuyPayBillForm().Show(this.dockPanel1);
                    break;
                case DataUtility.CASH_RECEIPT:
                    FormSingletonFactory.getInstance().getSellReceiptBillForm().Show(this.dockPanel1);
                    //FormMgr.getInstance().getSellReceiptBillForm().reload(openMode, ID);
                    break;
                default:
                    break;
            }
        }

        public void showLoading() {
            try
            {
                this.loadingForm.ShowDialog();
            }
            catch (TargetInvocationException ex) {
                ex.Message.ToString();
            }
            //this.Enabled = false;
        }

        public void hideLoading() {
            this.loadingForm.Close();
            //this.Enabled = true;
        }

        private void ���õ�½����ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PasswordForm form = new PasswordForm();
            form.ShowDialog();
        }

        private void �������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm form = new AboutForm();
            form.ShowDialog();
        }

        private void ���������ļ�ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Access�ļ�(*.mdb)|*.mdb";
            saveFile.FilterIndex = 1;
            saveFile.FileName = "ERP_back_" + DateTime.Now.ToString("yyyyMMdd");
            if (saveFile.ShowDialog() == System.Windows.Forms.DialogResult.OK && saveFile.FileName.Length > 0){
                try{
                    File.Copy(Application.StartupPath+"\\ERP.mdb", saveFile.FileName.ToString(), true);
                    MessageBox.Show("����ɹ�!", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch(Exception ex){
                    MessageBox.Show(ex.Message, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ���������ļ�ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Access�ļ�(*.mdb)|*.mdb";
            openFile.FilterIndex = 1;
            if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK && openFile.FileName.Length > 0)
            {
                try
                {
                    if (MessageBox.Show("�������ݺ�,�������ݽ�ȫ��������,�Ƿ����?", "��ʾ", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK)
                        return;

                    File.Copy(openFile.FileName.ToString(), Application.StartupPath + "\\ERP.mdb", true);
                    MessageBox.Show("�������ݳɹ�,���������!", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ��ʾ������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.mySplitter1.SplitterHide = false;
        }

        private void ��ʾ��ӭҳToolStripMenuItem_Click(object sender, EventArgs e)
        {
            getWelcomeForm().Show(this.dockPanel1);
        }

        private void �˳�ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton_purchase_Click(object sender, EventArgs e)
        {
            this.setForm(DataUtility.PURCHASE);
        }

        private void toolStripButton_sell_Click(object sender, EventArgs e)
        {
            this.setForm(DataUtility.SELL);
        }

        private void toolStripButton_queryLib_Click(object sender, EventArgs e)
        {
            this.setForm(DataUtility.QUERY_LIB);
        }

        private void toolStripButton_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton_statistic_Click(object sender, EventArgs e)
        {
            this.setForm(DataUtility.STATISTIC);
        }
    }
}