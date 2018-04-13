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
    public abstract class FormMgr
    {
        //updateVersion��Ÿ���ҵ�����ݵİ汾����ֵԽ�ߣ�����Խ��
        //���Form�����ݰ汾����updateVersion������ʾ����ʱ��Ҫˢ��
        //�������ര������ͬ�����⣬����д������ݸ����ˣ�������ش��ڿ���ͬ������
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

        /*************  purchase  *****************/
        //product purchase list form
        protected ProductCirculationListForm productPurchaseListForm = null;
        public abstract ProductCirculationListForm getProductPurchaseListForm();

        //product purchase detail form
        protected ProductCirculationForm productPurchaseForm = null;
        public abstract ProductCirculationForm getProductPurchaseForm();

        //product purchase back detail form
        protected ProductCirculationForm productPurchaseBackForm = null;
        public abstract ProductCirculationForm getProductPurchaseBackForm();

        /*************** manufacture *************************/
        protected ProductCirculationListForm manufactureListForm = null;
        public abstract ProductCirculationListForm getManufactureListForm();

        protected ProductCirculationForm manuCostForm = null;
        public abstract ProductCirculationForm getManuCostForm();

        protected ProductCirculationForm manuInForm = null;
        public abstract ProductCirculationForm getManuInForm();

        /***************  sell   ************************/
        //product sell list form
        protected ProductCirculationListForm productSellListForm = null;
        public abstract ProductCirculationListForm getProductSellListForm();

        //product sell detail form
        protected ProductCirculationForm productSellForm = null;
        public abstract ProductCirculationForm getProductSellForm();

        //product sell back detail form
        protected ProductCirculationForm productSellBackForm = null;
        public abstract ProductCirculationForm getProductSellBackForm();

        /***************  lib  ************************/
        //product lib list form
        protected ProductCirculationListForm productLibListForm = null;
        public abstract ProductCirculationListForm getProductLibListForm();

        //product lib overflow detail form
        protected ProductCirculationForm productLibOverflowForm = null;
        public abstract ProductCirculationForm getProductLibOverflowForm();

        //product Lib loss detail form
        protected ProductCirculationForm productLibLossForm = null;
        public abstract ProductCirculationForm getProductLibLossForm();

        /****************cash circulation************************/
        // Ӧ��Ӧ���������б�
        private PayReceiptListForm payReceiptListForm = null;
        public PayReceiptListForm getPayReceiptListForm()
        {
            if (payReceiptListForm == null || payReceiptListForm.IsDisposed)
            {
                payReceiptListForm = new PayReceiptListForm(mainForm);
                payReceiptListForm.initVersions(getVersions(),
                    UpdateType.PayReceiptUpdate, UpdateType.PayReceiptFinishUpdate, UpdateType.CustomerUpdate);
                appendEvent(payReceiptListForm);
            }
            return payReceiptListForm;
        }

        // �ɹ����
        private PayReceiptForm buyPayBillForm = null;
        public PayReceiptForm getBuyPayBillForm()
        {
            if (buyPayBillForm == null || buyPayBillForm.IsDisposed)
            {
                //�Ƚ�һ�����죬��ȫ��stainless��clothesDao�ĵ��ö�����һ���ط�
                buyPayBillForm = new PayReceiptForm(PayReceipt.PayReceiptTypeConf_BuyPay, ProductStainlessCirculationDao.getInstance());
                appendEvent(buyPayBillForm);
            }
            return buyPayBillForm;
        }

        //�ɹ��˵㵥
        private PayReceiptForm buyRefundBillForm = null;
        public PayReceiptForm getBuyRefundBillForm()
        {
            if (buyRefundBillForm == null || buyRefundBillForm.IsDisposed)
            {
                buyRefundBillForm = new PayReceiptForm(PayReceipt.PayReceiptTypeConf_BuyRefund, ProductStainlessCirculationDao.getInstance());
                appendEvent(buyRefundBillForm);
            }
            return buyRefundBillForm;
        }

        // �����տ
        private PayReceiptForm sellReceiptBillForm = null;
        public PayReceiptForm getSellReceiptBillForm()
        {
            if (sellReceiptBillForm == null || sellReceiptBillForm.IsDisposed)
            {
                sellReceiptBillForm = new PayReceiptForm(PayReceipt.PayReceiptTypeConf_SellReceipt, ProductStainlessCirculationDao.getInstance());
                appendEvent(sellReceiptBillForm);
            }
            return sellReceiptBillForm;
        }

        // �����˵㵥
        private PayReceiptForm sellRefundBillForm = null;
        public PayReceiptForm getSellRefundBillForm()
        {
            if (sellRefundBillForm == null || sellRefundBillForm.IsDisposed)
            {
                sellRefundBillForm = new PayReceiptForm(PayReceipt.PayReceiptTypeConf_SellRefund, ProductStainlessCirculationDao.getInstance());
                appendEvent(sellRefundBillForm);
            }
            return sellRefundBillForm;
        }

        // �������
        private PayReceiptForm otherPayBillForm = null;
        public PayReceiptForm getOtherPayBillForm()
        {
            if (otherPayBillForm == null || otherPayBillForm.IsDisposed)
            {
                otherPayBillForm = new PayReceiptOtherForm(PayReceipt.PayReceiptTypeConf_OtherPay);
                appendEvent(otherPayBillForm);
            }
            return otherPayBillForm;
        }

        // �����տ
        private PayReceiptForm otherReceiptBillForm = null;
        public PayReceiptForm getOtherReceiptBillForm()
        {
            if (otherReceiptBillForm == null || otherReceiptBillForm.IsDisposed)
            {
                otherReceiptBillForm = new PayReceiptOtherForm(PayReceipt.PayReceiptTypeConf_OtherReceipt);
                appendEvent(otherReceiptBillForm);
            }
            return otherReceiptBillForm;
        }

        /**************** query statistic ***************************/
        //product lib query form
        protected QueryLibForm queryLibForm = null;
        public abstract QueryLibForm getQueryLibForm();

        //product detail query form
        protected QueryProductDetailForm queryProductDetailForm = null;
        public abstract QueryProductDetailForm getQueryProductDetailForm();

        // ���������ѯ��
        protected QuerySellProfitForm querySellProfitForm = null;
        public abstract QuerySellProfitForm getQuerySellProfitForm();

        //cash statistic form
        protected StatisticCashForm statisticCashForm = null;
        public abstract StatisticCashForm getStatisticCashForm();

        //product statistic form
        protected StatisticProductForm productStatisticForm = null;
        public abstract StatisticProductForm getProductStatisticForm();

        //customer detail form
        private QueryCashDetailForm queryCashDetailForm = null;
        public virtual QueryCashDetailForm getQueryCashDetailForm()
        {
            if (queryCashDetailForm == null || queryCashDetailForm.IsDisposed)
            {
                queryCashDetailForm = new QueryCashDetailForm(this.mainForm, LabelUtility.QUERY_CASH_DETAIL);
                queryCashDetailForm.initVersions(getVersions(), UpdateType.CustomerCategoryUpdate, 
                    UpdateType.CustomerUpdate, UpdateType.PayReceiptFinishUpdate, UpdateType.PurchaseFinishUpdate, UpdateType.SellFinishUpdate);
                appendEvent(queryCashDetailForm);
            }
            return queryCashDetailForm;
        }

        /******************** data setting********************************/
        //product category item form
        protected CategoryItemForm productCIForm = null;
        public abstract CategoryItemForm getProductCIForm();

        public abstract CategoryItemForm getProductCIForm_select();

        public abstract MyDockContent getProductForm();

        //customer category item form
        private CategoryItemForm customerCIForm = null;
        public virtual CategoryItemForm getCustomerCIForm()
        {
            if (customerCIForm == null || customerCIForm.IsDisposed)
            {
                customerCIForm = new CustomerCategoryItemForm(1, CategoryItemTypeConfs.CategoryItemType_Customer, LabelUtility.DATA_CUSTOMER, this.mainForm);
                customerCIForm.initVersions(getVersions(), UpdateType.CustomerCategoryUpdate, UpdateType.CustomerUpdate);
                appendEvent(customerCIForm);
            }
            return customerCIForm;
        }

        private CategoryItemForm customerCIForm_select = null;
        public virtual CategoryItemForm getCustomerCIForm_Select()
        {
            customerCIForm_select = new CustomerCategoryItemForm(0, CategoryItemTypeConfs.CategoryItemType_Customer, LabelUtility.DATA_CUSTOMER, this.mainForm);
            customerCIForm_select.initVersions(getVersions(), UpdateType.CustomerCategoryUpdate, UpdateType.CustomerUpdate);
            appendEvent(customerCIForm_select);
            
            return customerCIForm_select;
        }

        public virtual CustomerForm getCustomerForm()
        {
            CustomerForm customerForm = new CustomerForm();
            appendEvent(customerForm);
            return customerForm;
        }

        public virtual CategoryForm getCategoryForm(string categoryTableName, int mode, int id)
        {
            CategoryForm categoryForm = new CategoryForm(CategoryItemTypeConfs.CategoryItemType_ProductStainless, mode, id);
            appendEvent(categoryForm);
            return categoryForm;
        }

        /*************** general method *********************/
        protected void appendEvent(MyDockContent form)
        {
            //�������£�����form����ĺ������ã�ÿ��form����������¼�������
            form.updateNotify += new MyDockContent.UpdateNotify(updateNotify);

            //����ȴ�
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
