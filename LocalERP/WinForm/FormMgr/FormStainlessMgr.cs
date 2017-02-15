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
                productPurchaseBackForm = new ProductCirculationForm(ProductCirculation.CirculationTypeConf_PurchaseBack, ProductStainlessCirculationDao.getInstance());
                appendEvent(productPurchaseBackForm);
            }
            return productPurchaseBackForm;
        }

        /******************** data setting********************************/
        //product category item form
        //private CategoryItemForm productCIForm = null;
        public override CategoryItemForm getProductCIForm()
        {
            if (productCIForm == null || productCIForm.IsDisposed)
            {
                productCIForm = new CategoryItemForm(1, new ProductStainlessCategoryItemProxy(), DataUtility.DATA_PRODUCT, this.mainForm);
                productCIForm.initVersions(FormMgr.getInstance().getVersions(),
                    UpdateType.ProductUpdate, UpdateType.ProductCategoryUpdate, UpdateType.CustomerUpdate, UpdateType.CustomerCategoryUpdate);
                appendEvent(productCIForm);
            }
            return productCIForm;
        }

        public override CategoryItemForm getProductCIForm_select()
        {
            CategoryItemForm productCIForm_select = new CategoryItemForm(0, new ProductStainlessCategoryItemProxy(), DataUtility.DATA_PRODUCT, this.mainForm);
            productCIForm_select.initVersions(FormMgr.getInstance().getVersions(),
                UpdateType.ProductUpdate, UpdateType.ProductCategoryUpdate, UpdateType.CustomerUpdate, UpdateType.CustomerCategoryUpdate);
                    
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
