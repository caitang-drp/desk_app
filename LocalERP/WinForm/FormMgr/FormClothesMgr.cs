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
using LocalERP.WinForm.Data;

namespace LocalERP.WinForm
{
    class FormClothesMgr: FormMgr
    {
        /************* purchase ******************/
        public override ProductCirculationListForm getProductPurchaseListForm()
        {
            if (productPurchaseListForm == null || productPurchaseListForm.IsDisposed)
            {
                productPurchaseListForm = new ProductCirculationListForm(mainForm, 1, "采购单据列表", ProductStainlessCirculationDao.getInstance());
                productPurchaseListForm.initVersions(getVersions(),
                    UpdateType.PurchaseUpdate, UpdateType.PurchaseFinishUpdate, UpdateType.CustomerUpdate);

                appendEvent(productPurchaseListForm);
            }
            return productPurchaseListForm;
        }

        public override ProductCirculationForm getProductPurchaseForm()
        {
            if (productPurchaseForm == null || productPurchaseForm.IsDisposed)
            {
                productPurchaseForm = new ProductStainlessCirculationForm(ProductCirculation.CirculationTypeConf_Purchase, ProductStainlessCirculationDao.getInstance());
                appendEvent(productPurchaseForm);
            }
            return productPurchaseForm;
        }

        public override ProductCirculationForm getProductPurchaseBackForm()
        {
            if (productPurchaseBackForm == null || productPurchaseBackForm.IsDisposed)
            {
                productPurchaseBackForm = new ProductStainlessCirculationForm(ProductCirculation.CirculationTypeConf_PurchaseBack, ProductStainlessCirculationDao.getInstance());
                appendEvent(productPurchaseBackForm);
            }
            return productPurchaseBackForm;
        }

        public override ProductCirculationListForm getManufactureListForm()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override ProductCirculationForm getManuCostForm()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override ProductCirculationForm getManuInForm()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        /****************** sell ****************/
        public override ProductCirculationListForm getProductSellListForm()
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

        public override ProductCirculationForm getProductSellForm()
        {
            if (productSellForm == null || productSellForm.IsDisposed)
            {
                productSellForm = new ProductStainlessCirculationForm(ProductCirculation.CirculationTypeConf_Sell, ProductStainlessCirculationDao.getInstance());
                appendEvent(productSellForm);
            }
            return productSellForm;
        }

        public override ProductCirculationForm getProductSellBackForm()
        {
            if (productSellBackForm == null || productSellBackForm.IsDisposed)
            {
                productSellBackForm = new ProductStainlessCirculationForm(ProductCirculation.CirculationTypeConf_SellBack, ProductStainlessCirculationDao.getInstance());
                appendEvent(productSellBackForm);
            }
            return productSellBackForm;
        }

        /************ lib *************/
        public override ProductCirculationListForm getProductLibListForm()
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

        public override ProductCirculationForm getProductLibOverflowForm()
        {
            if (productLibOverflowForm == null || productLibOverflowForm.IsDisposed)
            {
                productLibOverflowForm = new ProductCirculationForm(ProductCirculation.CirculationTypeConf_LibOverflow, ProductStainlessCirculationDao.getInstance());
                productLibOverflowForm.hideSomeControls();
                appendEvent(productLibOverflowForm);
            }
            return productLibOverflowForm;
        }

        public override ProductCirculationForm getProductLibLossForm()
        {
            if (productLibLossForm == null || productLibLossForm.IsDisposed)
            {
                productLibLossForm = new ProductCirculationForm(ProductCirculation.CirculationTypeConf_LibLoss, ProductStainlessCirculationDao.getInstance());
                productLibLossForm.hideSomeControls();
                appendEvent(productLibLossForm);
            }
            return productLibLossForm;
        }

        /*************** query statistic ****************/
        public override QueryLibForm getQueryLibForm()
        {
            if (queryLibForm == null || queryLibForm.IsDisposed)
            {
                queryLibForm = new QueryLibForm(CategoryItemTypeConfs.CategoryItemType_QueryLib, null, "库存查询");
                queryLibForm.initVersions(getVersions(),
                    UpdateType.PurchaseFinishUpdate, UpdateType.SellFinishUpdate, UpdateType.LibFinishUpdate, UpdateType.ProductUpdate, UpdateType.ProductCategoryUpdate);
                appendEvent(queryLibForm);
            }
            return queryLibForm;
        }

        public override QueryProductDetailForm getQueryProductDetailForm()
        {
            if (queryProductDetailForm == null || queryProductDetailForm.IsDisposed)
            {
                queryProductDetailForm = new QueryProductDetailForm();
                queryProductDetailForm.initVersions(getVersions(),
                    UpdateType.PurchaseFinishUpdate, UpdateType.SellFinishUpdate, UpdateType.LibFinishUpdate, UpdateType.ProductUpdate, UpdateType.CustomerUpdate);

                appendEvent(queryProductDetailForm);
            }
            return queryProductDetailForm;
        }

        public override QuerySellProfitForm getQuerySellProfitForm()
        {
            if (queryProductDetailForm == null || querySellProfitForm.IsDisposed)
            {
                querySellProfitForm = new QuerySellProfitForm();
                querySellProfitForm.initVersions(getVersions(),
                    UpdateType.PurchaseFinishUpdate, UpdateType.SellFinishUpdate, UpdateType.LibFinishUpdate, UpdateType.ProductUpdate, UpdateType.CustomerUpdate);

                appendEvent(querySellProfitForm);
            }
            return querySellProfitForm;
        }

        public override StatisticCashForm getStatisticCashForm()
        {
            return null;
        }

        public override StatisticProductForm getProductStatisticForm()
        {
            if (productStatisticForm == null || productStatisticForm.IsDisposed)
            {
                productStatisticForm = new StatisticProductForm();
                productStatisticForm.initVersions(getVersions(),
                    UpdateType.PurchaseFinishUpdate, UpdateType.SellFinishUpdate, UpdateType.LibFinishUpdate, UpdateType.ProductUpdate, UpdateType.CustomerUpdate);
                appendEvent(productStatisticForm);
            }
            return productStatisticForm;
        }

        /************** data setting ************************/
        public override CategoryItemForm getProductCIForm()
        {
            if (productCIForm == null || productCIForm.IsDisposed)
            {
                productCIForm = new ProductStainlessCategoryItemForm(1, CategoryItemTypeConfs.CategoryItemType_ProductStainless, LabelUtility.DATA_PRODUCT, this.mainForm);
                productCIForm.initVersions(getVersions(),
                    UpdateType.ProductUpdate, UpdateType.ProductCategoryUpdate, UpdateType.CustomerUpdate, UpdateType.CustomerCategoryUpdate);
                appendEvent(productCIForm);
            }
            return productCIForm;
        }

        public override CategoryItemForm getProductCIForm_select()
        {
            CategoryItemForm productCIForm_select = new ProductStainlessCategoryItemForm(0, CategoryItemTypeConfs.CategoryItemType_ProductStainless, LabelUtility.DATA_PRODUCT, this.mainForm);
            productCIForm_select.initVersions(getVersions(),
                UpdateType.ProductUpdate, UpdateType.ProductCategoryUpdate, UpdateType.CustomerUpdate, UpdateType.CustomerCategoryUpdate);

            appendEvent(productCIForm_select);
            return productCIForm_select;
        }

        public override MyDockContent getProductForm()
        {
            ProductStainlessForm productForm = new ProductStainlessForm();
            appendEvent(productForm);
            return productForm;
        }
    }
}
