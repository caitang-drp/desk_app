using LocalERP.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using LocalERP.UiDataProxy;
using LocalERP.DataAccess.Utility;
using System.Windows.Forms;

namespace LocalERP.WinForm
{
    class FormMgr
    {
        //singleton
        private static FormMgr formMgr;
        public static FormMgr getInstance()
        {
            if (formMgr == null)
                formMgr = new FormMgr();
            return formMgr;
        }
        private MainForm mainForm;

        public MainForm MainForm
        {
            get { return mainForm; }
            set { mainForm = value; }
        }

        /*************  purchase  *****************/
        //product purchase list form
        private ProductCirculationListForm productPurchaseListForm = null;
        public ProductCirculationListForm getProductPurchaseListForm()
        {
            if (productPurchaseListForm == null || productPurchaseListForm.IsDisposed)
            {
                productPurchaseListForm = new ProductCirculationListForm(mainForm, 1, "采购单据列表");
                appendEvent(productPurchaseListForm);
            }
            return productPurchaseListForm;
        }

        //product purchase detail form
        private ProductCirculationForm productPurchaseForm = null;
        public ProductCirculationForm getProductPurchaseForm()
        {
            if (productPurchaseForm == null || productPurchaseForm.IsDisposed)
            {
                productPurchaseForm = new ProductCirculationForm(ProductCirculation.CirculationTypeConf_Purchase);
                appendEvent(productPurchaseForm);
            }
            return productPurchaseForm;
        }

        //product purchase back detail form
        private ProductCirculationForm productPurchaseBackForm = null;
        public ProductCirculationForm getProductPurchaseBackForm()
        {
            if (productPurchaseBackForm == null || productPurchaseBackForm.IsDisposed)
            {
                productPurchaseBackForm = new ProductCirculationForm(ProductCirculation.CirculationTypeConf_PurchaseBack);
                appendEvent(productPurchaseBackForm);
            }
            return productPurchaseBackForm;
        }

        /***************  sell   ************************/
        //product sell list form
        private ProductCirculationListForm productSellListForm = null;
        public ProductCirculationListForm getProductSellListForm()
        {
            if (productSellListForm == null || productSellListForm.IsDisposed)
                productSellListForm = new ProductCirculationListForm(mainForm, 2, "销售单据列表");
            return productSellListForm;
        }

        //product sell detail form
        private ProductCirculationForm productSellForm = null;
        public ProductCirculationForm getProductSellForm()
        {
            if (productSellForm == null || productSellForm.IsDisposed)
            {
                productSellForm = new ProductCirculationForm(ProductCirculation.CirculationTypeConf_Sell);
                appendEvent(productSellForm);
            }
            return productSellForm;
        }

        //product sell back detail form
        private ProductCirculationForm productSellBackForm = null;
        public ProductCirculationForm getProductSellBackForm()
        {
            if (productSellBackForm == null || productSellBackForm.IsDisposed)
            {
                productSellBackForm = new ProductCirculationForm(ProductCirculation.CirculationTypeConf_SellBack);
                appendEvent(productSellBackForm);
            }
            return productSellBackForm;
        }

        /***************  lib  ************************/
        //product lib list form
        private ProductCirculationListForm productLibListForm = null;
        public ProductCirculationListForm getProductLibListForm()
        {
            if (productLibListForm == null || productLibListForm.IsDisposed)
            {
                productLibListForm = new ProductCirculationListForm(mainForm, 3, "盘点单据列表");
                productLibListForm.hideControls();
            }
            return productLibListForm;
        }

        //product lib overflow detail form
        private ProductCirculationForm productLibOverflowForm = null;
        public ProductCirculationForm getProductLibOverflowForm()
        {
            if (productLibOverflowForm == null || productLibOverflowForm.IsDisposed)
            {
                productLibOverflowForm = new ProductCirculationForm(ProductCirculation.CirculationTypeConf_LibOverflow);
                productLibOverflowForm.hideSomeControls();
                appendEvent(productLibOverflowForm);
            }
            return productLibOverflowForm;
        }

        //product Lib loss detail form
        private ProductCirculationForm productLibLossForm = null;
        public ProductCirculationForm getProductLibLossForm()
        {
            if (productLibLossForm == null || productLibLossForm.IsDisposed)
            {
                productLibLossForm = new ProductCirculationForm(ProductCirculation.CirculationTypeConf_LibLoss);
                productLibLossForm.hideSomeControls();
                appendEvent(productLibLossForm);
            }
            return productLibLossForm;
        }

        /**************** query statistic ***************************/
        //product lib query form
        private QueryLibForm queryLibForm = null;
        public QueryLibForm getQueryLibForm()
        {
            if (queryLibForm == null || queryLibForm.IsDisposed)
            {
                queryLibForm = new QueryLibForm(ProxyMgr.getInstance().getProductLibQueryProxy(), null, "库存查询");
                appendEvent(queryLibForm);
            }
            return queryLibForm; 
        }

        //product detail query form
        private QueryDetailForm queryDetailForm = null;
        public QueryDetailForm getQueryDetailForm()
        {
            if (queryDetailForm == null || queryDetailForm.IsDisposed)
            {
                queryDetailForm = new QueryDetailForm();
                appendEvent(queryDetailForm);
            }
            return queryDetailForm;
        }

        //product statistic form
        private ProductStatisticForm productStatisticForm = null;
        public ProductStatisticForm getProductStatisticForm()
        {
            if (productStatisticForm == null || productStatisticForm.IsDisposed)
            {
                productStatisticForm = new ProductStatisticForm();
                appendEvent(productStatisticForm);
            }
            return productStatisticForm;
        }

        /******************** data setting********************************/
        //customer category item form
        private CategoryItemForm customerCIForm = null;
        public CategoryItemForm getCustomerCIForm()
        {
            if (customerCIForm == null || customerCIForm.IsDisposed)
            {
                customerCIForm = new CategoryItemForm(1, new CustomerCategoryItemProxy(), DataUtility.DATA_CUSTOMER, this.mainForm);
                appendEvent(customerCIForm);
            }
            return customerCIForm;
        }

        private CategoryItemForm customerCIForm_select = null;
        public CategoryItemForm getCustomerCIForm_Select()
        {
            if (customerCIForm_select == null || customerCIForm_select.IsDisposed)
            {
                customerCIForm_select = new CategoryItemForm(0, new CustomerCategoryItemProxy(), DataUtility.DATA_CUSTOMER, this.mainForm);
                appendEvent(customerCIForm_select);
            }
            return customerCIForm_select;
        }

        //product category item form
        private CategoryItemForm productCIForm = null;
        public CategoryItemForm getProductCIForm()
        {
            if (productCIForm == null || productCIForm.IsDisposed)
            {
                productCIForm = new CategoryItemForm(1, new ProductCategoryItemProxy(), DataUtility.DATA_PRODUCT, this.mainForm);
                appendEvent(productCIForm);
            }
            return productCIForm;
        }

        private CategoryItemForm productCIForm_select = null;
        public CategoryItemForm getProductCIForm_select()
        {
            if (productCIForm_select == null || productCIForm_select.IsDisposed)
            {
                productCIForm_select = new CategoryItemForm(0, new ProductCategoryItemProxy(), DataUtility.DATA_PRODUCT, this.mainForm);
                appendEvent(productCIForm_select);
            }
            return productCIForm_select;
        }
        

        private ProductClothesForm productForm = null;
        public ProductClothesForm getProductForm() {
            if (productForm == null || productForm.IsDisposed)
            {
                productForm = new ProductClothesForm();
                appendEvent(productForm);
            }
            return productForm;
        }

        private CustomerForm customerForm = null;
        public CustomerForm getCustomerForm()
        {
            if (customerForm == null || customerForm.IsDisposed)
            {
                customerForm = new CustomerForm();
                appendEvent(customerForm);
            }
            return customerForm;
        }

        void formChangedNotify(NotifyType notifyType)
        {
            switch (notifyType)
            {
                case NotifyType.PurchaseUpdate:
                    refreshForm(this.productPurchaseListForm);
                    refreshForm(this.queryLibForm);
                    refreshForm(this.queryDetailForm);
                    break;
                case NotifyType.SellUpdate:
                    refreshForm(this.productSellListForm);
                    refreshForm(this.queryLibForm);
                    refreshForm(this.queryDetailForm);
                    break;
                case NotifyType.LibUpdate:
                    refreshForm(this.productLibListForm);
                    refreshForm(this.queryLibForm);
                    refreshForm(this.queryDetailForm);
                    break;
                case NotifyType.ProductUpdate:
                    refreshForm(this.productCIForm);
                    refreshForm(this.productCIForm_select);
                    refreshForm(this.queryLibForm);
                    refreshForm(this.queryDetailForm);
                    break;
                case NotifyType.ProductCategoryUpdate:
                    refreshForm(this.productForm);
                    refreshForm(this.productCIForm);
                    refreshForm(this.productCIForm_select);
                    refreshForm(this.queryLibForm);
                    refreshForm(this.queryDetailForm);
                    break;
                case NotifyType.CustomerUpdate:
                    refreshForm(this.customerCIForm);
                    refreshForm(this.customerCIForm_select);
                    refreshForm(this.productPurchaseListForm);
                    refreshForm(this.productSellListForm);
                    break;
                case NotifyType.CustomerCategoryUpdate:
                    refreshForm(this.customerForm);
                    refreshForm(this.customerCIForm);
                    refreshForm(this.customerCIForm_select);
                    break;
                default:
                    return;
            }
        }

        private void refreshForm(MyDockContent form) {
            if (form != null && !form.IsDisposed)
                form.refresh();
        }

        private void appendEvent(MyDockContent form) {
            form.changedNotify += new MyDockContent.ChangedNotify(formChangedNotify);
            form.beginLoadNotify += new MyDockContent.BeginLoadNotify(beginLoadNotify);
            form.endLoadNotify += new MyDockContent.EndLoadNotify(endLoadNotify); 
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
