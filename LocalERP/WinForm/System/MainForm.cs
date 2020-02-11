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
using LocalERP.DataAccess.DataDAO;

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

            this.Text = ConfDao.getInstance().Get(3).ToString();

            resetNegativeItem();

            NavigationUC navigation = new NavigationUC(this);
            navigation.Dock = DockStyle.Fill;
            this.panel1.Controls.Add(navigation);

            getWelcomeForm().Show(this.dockPanel1);
            this.toolStripStatusLabel1.Text = string.Format("{0}. 当前日期: {1}", ConfUtility.GetProductNameWithCopyRight(), DateTime.Now.ToShortDateString());

            FormSingletonFactory.getInstance().MainForm = this;

            Control.CheckForIllegalCrossThreadCalls = false;

            if (ConfUtility.softType == 0) {
                this.toolStripButton_card.Visible = false;
                this.toolStripButton_consume.Visible = false;
            }
            else if (ConfUtility.softType == 1) {
                this.toolStripButton_purchase.Visible = false;
                this.toolStripButton_sell.Visible = false;
                this.toolStripButton_queryLib.Visible = false;
                this.toolStripButton_statistic.Visible = false;

                this.允许ToolStripMenuItem.Visible = false;
                this.单据设置ToolStripMenuItem.Visible = false;
            }
        }

        //用于显示等待界面
        private LoadingForm loadingForm;

        private WelcomeForm welcomeForm = null;
        private WelcomeForm getWelcomeForm() { if (welcomeForm == null || welcomeForm.IsDisposed) welcomeForm = new WelcomeForm(this); return welcomeForm; }

        public void setForm(String buttonText) {
            this.setForm(buttonText, 0, 0);
        }

        public void setForm(String buttonText, int openMode, int ID)
        {
            switch (buttonText) {
                case LabelUtility.PURCHASE_LIST:
                    FormSingletonFactory.getInstance().getProductPurchaseListForm().Show(this.dockPanel1);
                    break;
                case LabelUtility.PURCHASE:
                    FormSingletonFactory.getInstance().getProductPurchaseForm().Show(this.dockPanel1);
                    FormSingletonFactory.getInstance().getProductPurchaseForm().reload(openMode, ID);
                    break;
                case LabelUtility.PURCHASE_BACK:
                    FormSingletonFactory.getInstance().getProductPurchaseBackForm().Show(this.dockPanel1);
                    FormSingletonFactory.getInstance().getProductPurchaseBackForm().reload(openMode, ID);
                    break;

                case LabelUtility.MANUFACTURE_LIST:
                    FormSingletonFactory.getInstance().getManufactureListForm().Show(this.dockPanel1);
                    break;
                case LabelUtility.MANU_COST:
                    FormSingletonFactory.getInstance().getManuCostForm().Show(this.dockPanel1);
                    FormSingletonFactory.getInstance().getManuCostForm().reload(openMode, ID);
                    break;
                case LabelUtility.MANU_IN:
                    FormSingletonFactory.getInstance().getManuInForm().Show(this.dockPanel1);
                    FormSingletonFactory.getInstance().getManuInForm().reload(openMode, ID);
                    break;

                case LabelUtility.SELL_LIST:
                    FormSingletonFactory.getInstance().getProductSellListForm().Show(this.dockPanel1);
                    break;
                case LabelUtility.SELL:
                    FormSingletonFactory.getInstance().getProductSellForm().Show(this.dockPanel1);
                    FormSingletonFactory.getInstance().getProductSellForm().reload(openMode, ID);
                    break;
                case LabelUtility.SELL_BACK:
                    FormSingletonFactory.getInstance().getProductSellBackForm().Show(this.dockPanel1);
                    FormSingletonFactory.getInstance().getProductSellBackForm().reload(openMode, ID);
                    break;

                case LabelUtility.CASH_LIST:
                    FormSingletonFactory.getInstance().getPayReceiptListForm().Show(this.dockPanel1);
                    break;
                case LabelUtility.CASH_PAY:
                    FormSingletonFactory.getInstance().getBuyPayBillForm().Show(this.dockPanel1);
                    FormSingletonFactory.getInstance().getBuyPayBillForm().reload(openMode, ID);
                    break;
                case LabelUtility.CASH_PAY_REFUND:
                    FormSingletonFactory.getInstance().getBuyRefundBillForm().Show(this.dockPanel1);
                    FormSingletonFactory.getInstance().getBuyRefundBillForm().reload(openMode, ID);
                    break;
                case LabelUtility.CASH_RECEIPT:
                    FormSingletonFactory.getInstance().getSellReceiptBillForm().Show(this.dockPanel1);
                    FormSingletonFactory.getInstance().getSellReceiptBillForm().reload(openMode, ID);
                    break;
                case LabelUtility.CASH_RECEIPT_REFUND:
                    FormSingletonFactory.getInstance().getSellRefundBillForm().Show(this.dockPanel1);
                    FormSingletonFactory.getInstance().getSellRefundBillForm().reload(openMode, ID);
                    break;
                case LabelUtility.CASH_OTHER_PAY:
                    FormSingletonFactory.getInstance().getOtherPayBillForm().Show(this.dockPanel1);
                    FormSingletonFactory.getInstance().getOtherPayBillForm().reload(openMode, ID);
                    break;
                case LabelUtility.CASH_OTHER_RECEIPT:
                    FormSingletonFactory.getInstance().getOtherReceiptBillForm().Show(this.dockPanel1);
                    FormSingletonFactory.getInstance().getOtherReceiptBillForm().reload(openMode, ID);
                    break;
                case LabelUtility.STATISTIC_PROFIT:
                    FormSingletonFactory.getInstance().getQuerySellProfitForm().Show(this.dockPanel1);
                    break;
                case LabelUtility.LIB_LIST:
                    FormSingletonFactory.getInstance().getProductLibListForm().Show(this.dockPanel1);
                    break;
                case LabelUtility.LIB_OVERFLOW:
                    FormSingletonFactory.getInstance().getProductLibOverflowForm().Show(this.dockPanel1);
                    FormSingletonFactory.getInstance().getProductLibOverflowForm().reload(openMode, ID);
                    break;
                case LabelUtility.LIB_LOSS:
                    FormSingletonFactory.getInstance().getProductLibLossForm().Show(this.dockPanel1);
                    FormSingletonFactory.getInstance().getProductLibLossForm().reload(openMode, ID);
                    break;

                /*case LabelUtility.QUERY_LIB:
                    FormSingletonFactory.getInstance().getQueryLibForm().Show(this.dockPanel1);
                    break;*/
                case LabelUtility.QUERY_PRODUCT_DETAIL:
                    FormSingletonFactory.getInstance().getQueryProductDetailForm().Show(this.dockPanel1);
                    break;
                case LabelUtility.QUERY_CASH_DETAIL:
                    FormSingletonFactory.getInstance().getQueryCashDetailForm().Show(this.dockPanel1);
                    break;
                case LabelUtility.STATISTIC_PRODUCT:
                    FormSingletonFactory.getInstance().getProductStatisticForm().Show(this.dockPanel1);
                    break;
                case LabelUtility.STATISTIC_CASH:
                    FormSingletonFactory.getInstance().getStatisticCashForm().Show(this.dockPanel1);
                    break;
                case LabelUtility.DATA_CUSTOMER:
                    FormSingletonFactory.getInstance().getCustomerCIForm().Show(this.dockPanel1);
                    break;
                case LabelUtility.DATA_PRODUCT:
                    FormSingletonFactory.getInstance().getProductCIForm().Show(this.dockPanel1);
                    break;
                case LabelUtility.DATA_COMPANY:
                    ReportInfoForm form = new ReportInfoForm();
                    form.ShowDialog();
                    break;


                case LabelUtility.CARD_LIST:
                    FormSingletonFactory.getInstance().getCardListForm().Show(this.dockPanel1);
                    //FormSingletonFactory.getInstance().getCardListForm().reload(openMode, ID);
                    break;
                case LabelUtility.CARD_ADD:
                    FormSingletonFactory.getInstance().getCardForm().Show(this.dockPanel1);
                    FormSingletonFactory.getInstance().getCardForm().reload(openMode, ID);
                    break;

                case LabelUtility.CONSUME_ADD:
                    FormSingletonFactory.getInstance().getConsumeForm().Show(this.dockPanel1);
                    FormSingletonFactory.getInstance().getConsumeForm().reload(openMode, ID);
                    break;

                case LabelUtility.CONSUME_LIST:
                    FormSingletonFactory.getInstance().getConsumeListForm().Show(this.dockPanel1);
                    //FormSingletonFactory.getInstance().getConsumeListForm().reload(openMode, ID);
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

        private void 设置登陆密码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PasswordForm form = new PasswordForm();
            form.ShowDialog();
        }


        private void 单据设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CirSettingForm form = new CirSettingForm();
            form.ShowDialog();
        }


        private void 备份设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BackupForm form = new BackupForm();
            form.ShowDialog();
        }

        private void 关于软件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm form = new AboutForm();
            form.ShowDialog();
        }

        private void 导出数据文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Access文件(*.mdb)|*.mdb";
            saveFile.FilterIndex = 1;
            saveFile.FileName = "ERP_back_" + DateTime.Now.ToString("yyyyMMdd");
            if (saveFile.ShowDialog() == System.Windows.Forms.DialogResult.OK && saveFile.FileName.Length > 0){
                try{
                    File.Copy(Application.StartupPath+"\\ERP.mdb", saveFile.FileName.ToString(), true);
                    MessageBox.Show("保存成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch(Exception ex){
                    MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void 导入数据文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Access文件(*.mdb)|*.mdb";
            openFile.FilterIndex = 1;
            if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK && openFile.FileName.Length > 0)
            {
                try
                {
                    if (MessageBox.Show("导入数据后,现有数据将全部被覆盖,是否继续?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK)
                        return;

                    File.Copy(openFile.FileName.ToString(), Application.StartupPath + "\\ERP.mdb", true);
                    MessageBox.Show("导入数据成功,请重启软件!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void 显示导航栏ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.mySplitter1.SplitterHide = false;
        }

        private void 显示欢迎页ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            getWelcomeForm().Show(this.dockPanel1);
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void toolStripButton_Click(object sender, EventArgs e)
        {
            ToolStripButton button = sender as ToolStripButton;
            this.setForm(button.Text);
        }

        private void toolStripButton_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 全部数据清空ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("数据将全部被清除,建议先备份数据文件后再清除,是否备份?", "提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                导出数据文件ToolStripMenuItem_Click(null, null);
            }
            else if(result == DialogResult.No){
                if (MessageBox.Show("现有数据将全部被清除,是否继续?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK)
                    return;
                else {
                    CustomerDao.getInstance().ClearAllArrear();
                    PayReceiptDao.getInstance().DeleteAll();
                    ProductStainlessCirculationDao.getInstance().DeleteAll();
                    SellProfitDao.getInstance().DeleteAll();
                    ProductStainlessDao.getInstance().ClearAllNumAndCost();
                    MessageBox.Show("删除清空数据成功，系统将自动关闭，请重新启动软件!", "提示", MessageBoxButtons.OK);
                    this.Close();
                }
            }
        }

        private void resetNegativeItem() {
            string negative = ConfDao.getInstance().Get(20);
            if (!string.IsNullOrEmpty(negative) && negative == "1")
            {
                this.permitNegativeItem.Checked = true;
                this.notPermitNegativeItem.Checked = false;
            }
            else
            {
                this.permitNegativeItem.Checked = false;
                this.notPermitNegativeItem.Checked = true;
            }
        }

        private void permitNegativeItem_Click(object sender, EventArgs e)
        {
            ConfDao.getInstance().Update(20, "1");
            resetNegativeItem();
        }

        private void notPermitNegativeItem_Click(object sender, EventArgs e)
        {
            ConfDao.getInstance().Update(20, "0");
            resetNegativeItem();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            string path = ConfUtility.GetBackupPath();
            if (!Directory.Exists(path))//判断是否存在
            {
                Directory.CreateDirectory(path);//创建新路径
            }
            
            path += "\\ERP_back_" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + ".mdb";
            try
            {
                File.Copy(Application.StartupPath + "\\ERP.mdb", path, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "备份错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}