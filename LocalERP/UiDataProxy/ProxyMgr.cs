using LocalERP.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace LocalERP.UiDataProxy
{
    class ProxyMgr
    {
        //singleton
        public static ProxyMgr mgr;
        public static ProxyMgr getInstance()
        {
            if (mgr == null)
                mgr = new ProxyMgr();
            return mgr;
        }

        private ProductCategoryItemProxy productCIProxy;
        public ProductCategoryItemProxy getProductCIProxy() {
            if (productCIProxy == null)
                productCIProxy = new ProductCategoryItemProxy();
            return productCIProxy;
        }

        private CustomerCategoryItemProxy customerCIProxy;
        public CustomerCategoryItemProxy getCustomerCIProxy()
        {
            if (customerCIProxy == null)
                customerCIProxy = new CustomerCategoryItemProxy();
            return customerCIProxy;
        }

        private QueryLibProxy productLibQueryProxy = null;
        public QueryLibProxy getProductLibQueryProxy()
        {
            if (productLibQueryProxy == null)
                productLibQueryProxy = new QueryLibProxy();
            return productLibQueryProxy; 
        }
        
    }
}
