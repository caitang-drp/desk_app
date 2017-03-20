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
    class FormStainlessMgr: FormMgr
    {
        /************* purchase ******************/
        public override ProductCirculationListForm getProductPurchaseListForm()
        {
            if (productPurchaseListForm == null || productPurchaseListForm.IsDisposed)
            {
                productPurchaseListForm = new ProductCirculationListForm(mainForm, 1, "�ɹ������б�", ProductStainlessCirculationDao.getInstance());
                productPurchaseListForm.initVersions(getVersions(), 
                    //��ʾ���������ҵ���и��£�����ҲҪˢ��
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

        /***************** manufacture ************************/
        public override ProductCirculationListForm getManufactureListForm()
        {
            if (manufactureListForm == null || manufactureListForm.IsDisposed)
            {
                manufactureListForm = new ProductCirculationListForm(mainForm, 4, "���������б�", ProductStainlessCirculationDao.getInstance());
                manufactureListForm.initVersions(getVersions(),
                    UpdateType.ManuUpdate, UpdateType.ManuFinishUpdate);

                manufactureListForm.hideControls();
                appendEvent(manufactureListForm);
            }
            return manufactureListForm;
        }

        public override ProductCirculationForm  getEasyForm()
        {
            if (easyForm == null || easyForm.IsDisposed)
            {
                easyForm = new ProductStainlessCirculationForm(ProductCirculation.CirculationTypeConf_Easy, ProductStainlessCirculationDao.getInstance());
                appendEvent(easyForm);
                easyForm.hideSomeControls();
            }
            return easyForm;
        }

        /****************** sell ****************/
        public override ProductCirculationListForm getProductSellListForm()
        {
            if (productSellListForm == null || productSellListForm.IsDisposed)
            {
                productSellListForm = new ProductCirculationListForm(mainForm, 2, "���۵����б�", ProductStainlessCirculationDao.getInstance());
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
                productLibListForm = new ProductCirculationListForm(mainForm, 3, "�̵㵥���б�", ProductStainlessCirculationDao.getInstance());
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
                productLibOverflowForm = new ProductStainlessCirculationForm(ProductCirculation.CirculationTypeConf_LibOverflow, ProductStainlessCirculationDao.getInstance());
                productLibOverflowForm.hideSomeControls();
                appendEvent(productLibOverflowForm);
            }
            return productLibOverflowForm;
        }

        public override ProductCirculationForm getProductLibLossForm()
        {
            if (productLibLossForm == null || productLibLossForm.IsDisposed)
            {
                productLibLossForm = new ProductStainlessCirculationForm(ProductCirculation.CirculationTypeConf_LibLoss, ProductStainlessCirculationDao.getInstance());
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
                queryLibForm = new QueryLibForm(ProxyMgr.getInstance().getProductLibQueryProxy(), null, "����ѯ");
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
                queryProductDetailForm = new QueryProductStainlessDetailForm();
                queryProductDetailForm.initVersions(getVersions(),
                    UpdateType.PurchaseFinishUpdate, UpdateType.SellFinishUpdate, UpdateType.LibFinishUpdate, UpdateType.ProductUpdate, UpdateType.CustomerUpdate);

                appendEvent(queryProductDetailForm);
            }
            return queryProductDetailForm;
        }

        public override QuerySellProfitForm getQuerySellProfitForm()
        {
            if (querySellProfitForm == null || querySellProfitForm.IsDisposed)
            {
                querySellProfitForm = new QuerySellProfitForm();
                querySellProfitForm.initVersions(getVersions(), UpdateType.SellFinishUpdate, UpdateType.ProductUpdate, UpdateType.CustomerUpdate);

                appendEvent(querySellProfitForm);
            }
            return querySellProfitForm;
        }

        public override StatisticProductForm getProductStatisticForm()
        {
            if (productStatisticForm == null || productStatisticForm.IsDisposed)
            {
                productStatisticForm = new StatisticProductStainlessForm();
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
                productCIForm = new CategoryItemForm(1, new ProductStainlessCategoryItemProxy(), DataUtility.DATA_PRODUCT, this.mainForm);
                productCIForm.initVersions(this.getVersions(),
                    UpdateType.ProductUpdate, UpdateType.ProductCategoryUpdate, UpdateType.PurchaseFinishUpdate, UpdateType.SellFinishUpdate, UpdateType.LibFinishUpdate);
                appendEvent(productCIForm);
            }
            return productCIForm;
        }

        public override CategoryItemForm getProductCIForm_select()
        {
            CategoryItemForm productCIForm_select = new CategoryItemForm(0, new ProductStainlessCategoryItemProxy(), DataUtility.DATA_PRODUCT, this.mainForm);
            productCIForm_select.initVersions(getVersions(),
                UpdateType.ProductUpdate, UpdateType.ProductCategoryUpdate, UpdateType.PurchaseFinishUpdate, UpdateType.SellFinishUpdate, UpdateType.LibFinishUpdate);
                    
            appendEvent(productCIForm_select);
            return productCIForm_select;
        }
        
        public override MyDockContent getProductForm() {
            ProductStainlessForm productForm = new ProductStainlessForm();
            appendEvent(productForm);
            return productForm;
        }
    }
}
