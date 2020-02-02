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
    class FormCardMgr: FormStainlessMgr
    {
                          
        public override CategoryItemForm getCustomerCIForm()
        {
            if (customerCIForm == null || customerCIForm.IsDisposed)
            {
                customerCIForm = new CustomerCardCategoryItemForm(1, CategoryItemTypeConfs.CategoryItemType_Customer, LabelUtility.DATA_CUSTOMER, this.mainForm);
                customerCIForm.initVersions(getVersions(), UpdateType.CustomerCategoryUpdate, UpdateType.CustomerUpdate);
                appendEvent(customerCIForm);
            }
            return customerCIForm;
        }
        
        public override CategoryItemForm getCustomerCIForm_Select()
        {
            customerCIForm_select = new CustomerCardCategoryItemForm(0, CategoryItemTypeConfs.CategoryItemType_Customer, LabelUtility.DATA_CUSTOMER, this.mainForm);
            customerCIForm_select.initVersions(getVersions(), UpdateType.CustomerCategoryUpdate, UpdateType.CustomerUpdate);
            appendEvent(customerCIForm_select);

            return customerCIForm_select;
        }

        public override CustomerForm getCustomerForm()
        {
            CustomerCardForm customerForm = new CustomerCardForm();
            appendEvent(customerForm);
            return customerForm;
        }
    }
}
