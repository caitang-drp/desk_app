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
    class FormClothesMgr: FormMgr
    {
        /******************** data setting********************************/
        //product category item form
        //private CategoryItemForm productCIForm = null;
        public override CategoryItemForm getProductCIForm()
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

        public override CategoryItemForm getProductCIForm_select()
        {
            CategoryItemForm productCIForm_select = new CategoryItemForm(0, new ProductCategoryItemProxy(), DataUtility.DATA_PRODUCT, this.mainForm);
            productCIForm_select.initVersions(FormMgr.getInstance().getVersions(),
                UpdateType.ProductUpdate, UpdateType.ProductCategoryUpdate, UpdateType.CustomerUpdate, UpdateType.CustomerCategoryUpdate);

            appendEvent(productCIForm_select);
            return productCIForm_select;
        }

        public override MyDockContent getProductForm()
        {
            ProductClothesForm productForm = new ProductClothesForm();
            appendEvent(productForm);
            return productForm;
        }
    }
}
