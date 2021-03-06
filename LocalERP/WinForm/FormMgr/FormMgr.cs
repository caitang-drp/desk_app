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
        //updateVersion存放各种业务数据的版本，数值越高，数据越新
        //如果Form的数据版本低于updateVersion，则显示窗口时需要刷新
        //这个解决多窗口数据同步问题，如果有窗口数据更新了，其他相关窗口可以同步更新
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

        // 卡片
        private CardForm cardForm = null;
        public CardForm getCardForm()
        {
            if (cardForm == null || cardForm.IsDisposed)
            {
                cardForm = new CardForm();
                appendEvent(cardForm);
            }
            return cardForm;
        }
        private CardListForm cardListForm = null;
        public CardListForm getCardListForm()
        {
            if (cardListForm == null || cardListForm.IsDisposed)
            {
                cardListForm = new CardListForm(mainForm, 1);
                cardListForm.initVersions(getVersions(),
                    UpdateType.CardUpdate, UpdateType.ConsumeUpdate, UpdateType.CustomerUpdate);
                appendEvent(cardListForm);
            }
            return cardListForm;
        }

        private CardListForm cardListForm_select = null;
        public CardListForm getCardListForm_select()
        {
            if (cardListForm_select == null || cardListForm_select.IsDisposed)
            {
                cardListForm_select = new CardListForm(mainForm, 0);
                cardListForm_select.initVersions(getVersions(),
                    UpdateType.CardUpdate);
                appendEvent(cardListForm_select);
            }
            return cardListForm_select;
        }

        //消费
        private ConsumeForm consumeForm = null;
        public ConsumeForm getConsumeForm()
        {
            if (consumeForm == null || consumeForm.IsDisposed)
            {
                consumeForm = new ConsumeForm();
                appendEvent(consumeForm);
            }
            return consumeForm;
        }

        private ConsumeListForm consumeListForm = null;
        public ConsumeListForm getConsumeListForm()
        {
            if (consumeListForm == null || consumeListForm.IsDisposed)
            {
                consumeListForm = new ConsumeListForm(mainForm, 1);
                consumeListForm.initVersions(getVersions(),
                    UpdateType.ConsumeUpdate, UpdateType.CustomerUpdate);
                appendEvent(consumeListForm);
            }
            return consumeListForm;
        }

        /****************cash circulation************************/
        // 应收应付，单据列表
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

        // 采购付款单
        private PayReceiptForm buyPayBillForm = null;
        public PayReceiptForm getBuyPayBillForm()
        {
            if (buyPayBillForm == null || buyPayBillForm.IsDisposed)
            {
                //等进一步改造，把全部stainless、clothesDao的调用都放在一个地方
                buyPayBillForm = new PayReceiptForm(PayReceipt.PayReceiptTypeConf_BuyPay, ProductStainlessCirculationDao.getInstance());
                appendEvent(buyPayBillForm);
            }
            return buyPayBillForm;
        }

        //采购退点单
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

        // 销售收款单
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

        // 销售退点单
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

        // 其他付款单
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

        // 其他收款单
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

        // 销售利润查询表
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
        protected CategoryItemForm customerCIForm = null;
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

        protected CategoryItemForm customerCIForm_select = null;
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



        //category
        public virtual CategoryForm getCategoryForm(string categoryTableName, int mode, int id)
        {
            CategoryForm categoryForm = new CategoryForm(CategoryItemTypeConfs.CategoryItemType_ProductStainless, mode, id);
            appendEvent(categoryForm);
            return categoryForm;
        }

        /*************** general method *********************/
        protected void appendEvent(MyDockContent form)
        {
            //发布更新，会在form里面的函数调用，每个form都新增这个事件处理函数
            //比如customerUpdate更新，会让customerUpdate的版本加一
            form.updateNotify += new MyDockContent.UpdateNotify(updateNotify);

            //界面等待
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
