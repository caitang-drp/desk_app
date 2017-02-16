using LocalERP.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using LocalERP.UiDataProxy;
using LocalERP.DataAccess.Utility;
using System.Windows.Forms;
using System.IO;
using LocalERP.DataAccess.DataDAO;

namespace LocalERP.WinForm
{
    public abstract class FormMgr
    {
        //singleton
        /*private static FormMgr formMgr;
        public static FormMgr getInstance()
        {
            if (formMgr == null)
                formMgr = new FormMgr();
            return formMgr;
        }*/

        //updateVersion存放各种业务数据的版本，数值越高，数据越新
        //如果Form的数据版本低于updateVersion，则显示窗口时需要刷新
        //这个解决多窗口数据同步问题，如果有窗口数据更新了，其他相关窗口可以同步更新
        private Dictionary<UpdateType, int> updateVersions = null;
        public Dictionary<UpdateType, int> getVersions()
        {
            if (updateVersions == null)
            {
                updateVersions = new Dictionary<UpdateType, int>();
                Type versionType = typeof(UpdateType);
                foreach (UpdateType version in Enum.GetValues(versionType))
                {
                    updateVersions.Add(version, 0);
                }
            }
            return updateVersions;
        }

        protected MainForm mainForm;

        public MainForm MainForm
        {
            get { return mainForm; }
            set { mainForm = value; }
        }

        /*************  purchase  *****************/
        //product purchase list form
        protected ProductCirculationListForm productPurchaseListForm = null;
        public abstract ProductCirculationListForm getProductPurchaseListForm();

        //product purchase detail form
        protected ProductCirculationForm productPurchaseForm = null;
        public abstract ProductCirculationForm getProductPurchaseForm();

        //product purchase back detail form
        protected ProductCirculationForm productPurchaseBackForm = null;
        public abstract ProductCirculationForm getProductPurchaseBackForm();

        /***************  sell   ************************/
        //product sell list form
        protected ProductCirculationListForm productSellListForm = null;
        public abstract ProductCirculationListForm getProductSellListForm();

        //product sell detail form
        protected ProductCirculationForm productSellForm = null;
        public abstract ProductCirculationForm getProductSellForm();

        //product sell back detail form
        protected ProductCirculationForm productSellBackForm = null;
        public abstract ProductCirculationForm getProductSellBackForm();

        /***************  lib  ************************/
        //product lib list form
        protected ProductCirculationListForm productLibListForm = null;
        public abstract ProductCirculationListForm getProductLibListForm();

        //product lib overflow detail form
        protected ProductCirculationForm productLibOverflowForm = null;
        public abstract ProductCirculationForm getProductLibOverflowForm();

        //product Lib loss detail form
        protected ProductCirculationForm productLibLossForm = null;
        public abstract ProductCirculationForm getProductLibLossForm();

        /****************cash circulation************************/
        // 销售收款单
        private SellReceiptBillForm sellReceiptBillForm = null;
        public SellReceiptBillForm getSellReceiptBillForm()
        {
            if (sellReceiptBillForm == null || sellReceiptBillForm.IsDisposed)
            {
                sellReceiptBillForm = new SellReceiptBillForm(ProductCirculation.CirculationTypeConf_Purchase);
                appendEvent(sellReceiptBillForm);
            }
            return sellReceiptBillForm;
        }

        // 采购付款单
        private BuyPayBillForm buyPayBillForm = null;
        public BuyPayBillForm getbuyPayBillForm()
        {
            if (buyPayBillForm == null || buyPayBillForm.IsDisposed)
            {
                buyPayBillForm = new BuyPayBillForm(ProductCirculation.CirculationTypeConf_Purchase);
                appendEvent(buyPayBillForm);
            }
            return buyPayBillForm;
        }

        // 应收应付，单据列表
        private PayReceiptListForm payReceiptListForm = null;
        public PayReceiptListForm getPayReceiptListForm()
        {
            if (payReceiptListForm == null || payReceiptListForm.IsDisposed)
            {
                payReceiptListForm = new PayReceiptListForm(mainForm, 2, "单据列表");
                appendEvent(payReceiptListForm);
            }
            return payReceiptListForm;
        }

        /**************** query statistic ***************************/
        //product lib query form
        protected QueryLibForm queryLibForm = null;
        public abstract QueryLibForm getQueryLibForm();

        //product detail query form
        protected QueryDetailForm queryDetailForm = null;
        public abstract QueryDetailForm getQueryDetailForm();

        //product statistic form
        protected ProductStatisticForm productStatisticForm = null;
        public abstract ProductStatisticForm getProductStatisticForm();

        /******************** data setting********************************/
        //product category item form
        protected CategoryItemForm productCIForm = null;
        public abstract CategoryItemForm getProductCIForm();

        public abstract CategoryItemForm getProductCIForm_select();

        public abstract MyDockContent getProductForm();

        //customer category item form
        private CategoryItemForm customerCIForm = null;
        public virtual CategoryItemForm getCustomerCIForm()
        {
            if (customerCIForm == null || customerCIForm.IsDisposed)
            {
                customerCIForm = new CategoryItemForm(1, new CustomerCategoryItemProxy(), DataUtility.DATA_CUSTOMER, this.mainForm);
                customerCIForm.initVersions(getVersions(), UpdateType.CustomerCategoryUpdate, UpdateType.CustomerUpdate);
                appendEvent(customerCIForm);
            }
            return customerCIForm;
        }

        private CategoryItemForm customerCIForm_select = null;
        public virtual CategoryItemForm getCustomerCIForm_Select()
        {
            if (customerCIForm_select == null || customerCIForm_select.IsDisposed)
            {
                customerCIForm_select = new CategoryItemForm(0, new CustomerCategoryItemProxy(), DataUtility.DATA_CUSTOMER, this.mainForm);
                customerCIForm_select.initVersions(getVersions(), UpdateType.CustomerCategoryUpdate, UpdateType.CustomerUpdate);
                appendEvent(customerCIForm_select);
            }
            return customerCIForm_select;
        }

        public virtual CustomerForm getCustomerForm()
        {
            CustomerForm customerForm = new CustomerForm();
            appendEvent(customerForm);
            return customerForm;
        }

        /*************** general method *********************/
        protected void appendEvent(MyDockContent form)
        {
            form.updateNotify += new MyDockContent.UpdateNotify(updateNotify);
            form.beginLoadNotify += new MyDockContent.BeginLoadNotify(beginLoadNotify);
            form.endLoadNotify += new MyDockContent.EndLoadNotify(endLoadNotify); 
        }

        void updateNotify(UpdateType notifyType)
        {
            getVersions()[notifyType]++;
        }

        void beginLoadNotify()
        {
            mainForm.showLoading();
        }

        void endLoadNotify()
        {
            mainForm.hideLoading();
        }
    }
}
