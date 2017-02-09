using LocalERP.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using LocalERP.UiDataProxy;
using LocalERP.DataAccess.Utility;
using System.Windows.Forms;
using System.IO;

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
                productPurchaseListForm.initVersions(FormMgr.getInstance().getVersions(), 
                    UpdateType.PurchaseUpdate, UpdateType.PurchaseFinishUpdate, UpdateType.CustomerUpdate);
                
                appendEvent(productPurchaseListForm);
            }
            return productPurchaseListForm;
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
            {
                productSellListForm = new ProductCirculationListForm(mainForm, 2, "销售单据列表");
                productSellListForm.initVersions(FormMgr.getInstance().getVersions(),
                    UpdateType.SellUpdate, UpdateType.SellFinishUpdate, UpdateType.CustomerUpdate);

                appendEvent(productSellListForm);
            }
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
                productLibListForm.initVersions(FormMgr.getInstance().getVersions(),
                    UpdateType.LibUpdate, UpdateType.LibFinishUpdate);
                productLibListForm.hideControls();

                appendEvent(productLibListForm);
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
                queryLibForm.initVersions(FormMgr.getInstance().getVersions(),
                    UpdateType.PurchaseFinishUpdate, UpdateType.SellFinishUpdate, UpdateType.LibFinishUpdate, UpdateType.ProductUpdate, UpdateType.ProductCategoryUpdate);
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
                queryDetailForm.initVersions(FormMgr.getInstance().getVersions(),
                    UpdateType.PurchaseFinishUpdate, UpdateType.SellFinishUpdate, UpdateType.LibFinishUpdate, UpdateType.ProductUpdate, UpdateType.CustomerUpdate);

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
                productStatisticForm.initVersions(FormMgr.getInstance().getVersions(),
                    UpdateType.PurchaseFinishUpdate, UpdateType.SellFinishUpdate, UpdateType.LibFinishUpdate, UpdateType.ProductUpdate, UpdateType.CustomerUpdate);
                appendEvent(productStatisticForm);
            }
            return productStatisticForm;
        }

        /******************** data setting********************************/
        //product category item form
        private CategoryItemForm productCIForm = null;
        public CategoryItemForm getProductCIForm()
        {
            if (productCIForm == null || productCIForm.IsDisposed)
            {
                productCIForm = new CategoryItemForm(1, new ProductCategoryItemProxy(), DataUtility.DATA_PRODUCT, this.mainForm);
                productCIForm.initVersions(FormMgr.getInstance().getVersions(),
                    UpdateType.ProductUpdate, UpdateType.ProductCategoryUpdate, UpdateType.CustomerUpdate, UpdateType.CustomerCategoryUpdate);
                appendEvent(productCIForm);
            }
            return productCIForm;
        }

        //private CategoryItemForm productCIForm_select = null;
        public CategoryItemForm getProductCIForm_select()
        {
            //if (productCIForm_select == null || productCIForm_select.IsDisposed)
            //{
                //File.AppendAllText("e:\\debug.txt", "get product ci form, form = null\r\n");
            CategoryItemForm productCIForm_select = new CategoryItemForm(0, new ProductCategoryItemProxy(), DataUtility.DATA_PRODUCT, this.mainForm);
            productCIForm_select.initVersions(FormMgr.getInstance().getVersions(),
                UpdateType.ProductUpdate, UpdateType.ProductCategoryUpdate, UpdateType.CustomerUpdate, UpdateType.CustomerCategoryUpdate);
                    
            appendEvent(productCIForm_select);
            //}
            //File.AppendAllText("e:\\debug.txt", string.Format("get product ci form, form hash code={0}\r\n", productCIForm_select.GetHashCode()));
            return productCIForm_select;
        }
        

        //private ProductClothesForm productForm = null;
        public ProductClothesForm getProductForm() {
            //if (productForm == null || productForm.IsDisposed)
            //{
                ProductClothesForm productForm = new ProductClothesForm();
                appendEvent(productForm);
            //}
            return productForm;
        }

        //customer category item form
        //private CustomerForm customerForm = null;
        public CustomerForm getCustomerForm()
        {
            //if (customerForm == null || customerForm.IsDisposed)
            //{
                CustomerForm customerForm = new CustomerForm();
                appendEvent(customerForm);
            //}
            return customerForm;
        }

        private CategoryItemForm customerCIForm = null;
        public CategoryItemForm getCustomerCIForm()
        {
            if (customerCIForm == null || customerCIForm.IsDisposed)
            {
                customerCIForm = new CategoryItemForm(1, new CustomerCategoryItemProxy(), DataUtility.DATA_CUSTOMER, this.mainForm);
                customerCIForm.initVersions(FormMgr.getInstance().getVersions(), UpdateType.CustomerCategoryUpdate, UpdateType.CustomerUpdate);
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
                customerCIForm_select.initVersions(FormMgr.getInstance().getVersions(), UpdateType.CustomerCategoryUpdate, UpdateType.CustomerUpdate);
                appendEvent(customerCIForm_select);
            }
            return customerCIForm_select;
        }

        /*void updateNotify(UpdateNotifyType notifyType)
        {
            switch (notifyType)
            {
                case UpdateNotifyType.PurchaseUpdate:
                    refreshForm(this.productPurchaseListForm, this.queryLibForm, this.queryDetailForm);
                    break;
                case UpdateNotifyType.SellUpdate:
                    refreshForm(this.productSellListForm, this.queryLibForm, this.queryDetailForm);
                    break;
                case UpdateNotifyType.LibUpdate:
                    refreshForm(this.productLibListForm, this.queryLibForm, this.queryDetailForm);
                    break;
                case UpdateNotifyType.ProductUpdate:
                    refreshForm(this.productPurchaseForm, this.productPurchaseBackForm, 
                        this.productSellForm, this.productSellBackForm,
                        this.productLibOverflowForm, this.productLibLossForm,
                        this.queryLibForm, this.queryDetailForm, 
                        this.productCIForm);
                    break;
                case UpdateNotifyType.ProductCategoryUpdate:
                    refreshForm(this.productPurchaseForm, this.productPurchaseBackForm,
                        this.productSellForm, this.productSellBackForm,
                        this.productLibOverflowForm, this.productLibLossForm,
                        this.queryLibForm, this.queryDetailForm, 
                        this.productForm);
                    break;
                case UpdateNotifyType.CustomerUpdate:
                    refreshForm(
                        this.queryDetailForm,
                        this.customerCIForm, this.customerCIForm_select);
                    break;
                case UpdateNotifyType.CustomerCategoryUpdate:
                    refreshForm(
                        this.queryDetailForm,
                        this.customerForm, this.customerCIForm, this.customerCIForm_select);
                    break;
                default:
                    return;
            }
        }
         
        private void refreshForm(params MyDockContent [] forms) {
            foreach (MyDockContent form in forms) {
                if (form != null && !form.IsDisposed)
                    form.refresh();
            }
        }
        */

        private void appendEvent(MyDockContent form) {
            form.updateNotify += new MyDockContent.UpdateNotify(updateNotify);
            form.beginLoadNotify += new MyDockContent.BeginLoadNotify(beginLoadNotify);
            form.endLoadNotify += new MyDockContent.EndLoadNotify(endLoadNotify); 
        }

        void updateNotify(UpdateType notifyType)
        {
            FormMgr.getInstance().getVersions()[notifyType]++;
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
