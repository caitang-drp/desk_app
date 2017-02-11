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
    class FormStainlessMgr: FormMgr
    {
        public override ProductCirculationForm getProductPurchaseForm()
        {
            if (productPurchaseForm == null || productPurchaseForm.IsDisposed)
            {
                productPurchaseForm = new ProductStainlessCirculationForm(ProductCirculation.CirculationTypeConf_Purchase);
                appendEvent(productPurchaseForm);
            }
            return productPurchaseForm;
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
