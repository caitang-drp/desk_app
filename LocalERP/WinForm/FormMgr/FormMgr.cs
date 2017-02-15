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
    public class FormMgr
    {
        //singleton
        private static FormMgr formMgr;
        public static FormMgr getInstance()
        {
            if (formMgr == null)
                formMgr = new FormMgr();
            return formMgr;
        }

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

        /*************  purchase  *****************/
        //product purchase list form
        protected ProductCirculationListForm productPurchaseListForm = null;
        public virtual ProductCirculationListForm getProductPurchaseListForm()
        {
            return null;
        }

        //product purchase detail form
        protected ProductCirculationForm productPurchaseForm = null;
        public virtual ProductCirculationForm getProductPurchaseForm()
        {
            return null;
        }

        //product purchase back detail form
        protected ProductCirculationForm productPurchaseBackForm = null;
        public virtual ProductCirculationForm getProductPurchaseBackForm()
        {
            return null;
        }

        /***************  sell   ************************/
        //product sell list form
        private ProductCirculationListForm productSellListForm = null;
        public virtual ProductCirculationListForm getProductSellListForm()
        {
            if (productSellListForm == null || productSellListForm.IsDisposed)
            {
                productSellListForm = new ProductCirculationListForm(mainForm, 2, "销售单据列表", ProductStainlessCirculationDao.getInstance());
                productSellListForm.initVersions(getVersions(),
                    UpdateType.SellUpdate, UpdateType.SellFinishUpdate, UpdateType.CustomerUpdate);

                appendEvent(productSellListForm);
            }
            return productSellListForm;
        }

        //product sell detail form
        private ProductCirculationForm productSellForm = null;
        public virtual ProductCirculationForm getProductSellForm()
        {
            if (productSellForm == null || productSellForm.IsDisposed)
            {
                productSellForm = new ProductCirculationForm(ProductCirculation.CirculationTypeConf_Sell, ProductStainlessCirculationDao.getInstance());
                appendEvent(productSellForm);
            }
            return productSellForm;
        }

        //product sell back detail form
        private ProductCirculationForm productSellBackForm = null;
        public virtual ProductCirculationForm getProductSellBackForm()
        {
            if (productSellBackForm == null || productSellBackForm.IsDisposed)
            {
                productSellBackForm = new ProductCirculationForm(ProductCirculation.CirculationTypeConf_SellBack, ProductStainlessCirculationDao.getInstance());
                appendEvent(productSellBackForm);
            }
            return productSellBackForm;
        }

        /***************  lib  ************************/
        //product lib list form
        private ProductCirculationListForm productLibListForm = null;
        public virtual ProductCirculationListForm getProductLibListForm()
        {
            if (productLibListForm == null || productLibListForm.IsDisposed)
            {
                productLibListForm = new ProductCirculationListForm(mainForm, 3, "盘点单据列表", ProductStainlessCirculationDao.getInstance());
                productLibListForm.initVersions(getVersions(),
                    UpdateType.LibUpdate, UpdateType.LibFinishUpdate);
                productLibListForm.hideControls();

                appendEvent(productLibListForm);
            }
            return productLibListForm;
        }

        //product lib overflow detail form
        private ProductCirculationForm productLibOverflowForm = null;
        public virtual ProductCirculationForm getProductLibOverflowForm()
        {
            if (productLibOverflowForm == null || productLibOverflowForm.IsDisposed)
            {
                productLibOverflowForm = new ProductCirculationForm(ProductCirculation.CirculationTypeConf_LibOverflow, ProductStainlessCirculationDao.getInstance());
                productLibOverflowForm.hideSomeControls();
                appendEvent(productLibOverflowForm);
            }
            return productLibOverflowForm;
        }

        //product Lib loss detail form
        private ProductCirculationForm productLibLossForm = null;
        public virtual ProductCirculationForm getProductLibLossForm()
        {
            if (productLibLossForm == null || productLibLossForm.IsDisposed)
            {
                productLibLossForm = new ProductCirculationForm(ProductCirculation.CirculationTypeConf_LibLoss, ProductStainlessCirculationDao.getInstance());
                productLibLossForm.hideSomeControls();
                appendEvent(productLibLossForm);
            }
            return productLibLossForm;
        }

        /**************** query statistic ***************************/
        //product lib query form
        private QueryLibForm queryLibForm = null;
        public virtual QueryLibForm getQueryLibForm()
        {
            if (queryLibForm == null || queryLibForm.IsDisposed)
            {
                queryLibForm = new QueryLibForm(ProxyMgr.getInstance().getProductLibQueryProxy(), null, "库存查询");
                queryLibForm.initVersions(getVersions(),
                    UpdateType.PurchaseFinishUpdate, UpdateType.SellFinishUpdate, UpdateType.LibFinishUpdate, UpdateType.ProductUpdate, UpdateType.ProductCategoryUpdate);
                appendEvent(queryLibForm);
            }
            return queryLibForm; 
        }

        //product detail query form
        private QueryDetailForm queryDetailForm = null;
        public virtual QueryDetailForm getQueryDetailForm()
        {
            if (queryDetailForm == null || queryDetailForm.IsDisposed)
            {
                queryDetailForm = new QueryDetailForm();
                queryDetailForm.initVersions(getVersions(),
                    UpdateType.PurchaseFinishUpdate, UpdateType.SellFinishUpdate, UpdateType.LibFinishUpdate, UpdateType.ProductUpdate, UpdateType.CustomerUpdate);

                appendEvent(queryDetailForm);
            }
            return queryDetailForm;
        }

        //product statistic form
        private ProductStatisticForm productStatisticForm = null;
        public virtual ProductStatisticForm getProductStatisticForm()
        {
            if (productStatisticForm == null || productStatisticForm.IsDisposed)
            {
                productStatisticForm = new ProductStatisticForm();
                productStatisticForm.initVersions(getVersions(),
                    UpdateType.PurchaseFinishUpdate, UpdateType.SellFinishUpdate, UpdateType.LibFinishUpdate, UpdateType.ProductUpdate, UpdateType.CustomerUpdate);
                appendEvent(productStatisticForm);
            }
            return productStatisticForm;
        }

        /******************** data setting********************************/
        //product category item form
        protected CategoryItemForm productCIForm = null;
        public virtual CategoryItemForm getProductCIForm()
        {
            if (productCIForm == null || productCIForm.IsDisposed)
            {
                productCIForm = new CategoryItemForm(1, new ProductCategoryItemProxy(), DataUtility.DATA_PRODUCT, this.mainForm);
                productCIForm.initVersions(getVersions(),
                    UpdateType.ProductUpdate, UpdateType.ProductCategoryUpdate, UpdateType.CustomerUpdate, UpdateType.CustomerCategoryUpdate);
                appendEvent(productCIForm);
            }
            return productCIForm;
        }

        public virtual CategoryItemForm getProductCIForm_select()
        {
            //此处必须新建，详见ProductCirculationForm中引用到此方法时的注释
            CategoryItemForm productCIForm_select = new CategoryItemForm(0, new ProductCategoryItemProxy(), DataUtility.DATA_PRODUCT, this.mainForm);
            productCIForm_select.initVersions(getVersions(),
                UpdateType.ProductUpdate, UpdateType.ProductCategoryUpdate, UpdateType.CustomerUpdate, UpdateType.CustomerCategoryUpdate);
                    
            appendEvent(productCIForm_select);

            return productCIForm_select;
        }

        public virtual MyDockContent getProductForm()
        {
            ProductClothesForm productForm = new ProductClothesForm();
            appendEvent(productForm);
            return productForm;
        }

        public virtual CustomerForm getCustomerForm()
        {
            CustomerForm customerForm = new CustomerForm();
            appendEvent(customerForm);
            return customerForm;
        }

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
