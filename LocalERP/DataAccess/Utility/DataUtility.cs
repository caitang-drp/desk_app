/*created by stone: 用于定义各个功能的名称*/

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace LocalERP.DataAccess.Utility
{
    public static class DataUtility
    {
        public const String PURCHASE_LIST = "采购单据列表";
        public const String PURCHASE = "采购入库";
        public const String PURCHASE_BACK = "采购退货";

        //added by stone 2017-02-04
        public const String MANUFACTURE_LIST = "生产单据列表";
        public const string EASY = "易耗品领用";

        public const String SELL_LIST = "销售单据列表";
        public const String SELL = "销售出库";
        public const String SELL_BACK = "销售退货";

        public const String CASH_LIST = "收付单据列表";
        public const String CASH_PAY = "采购付款";
        public const String CASH_PAY_REFUND = "采购退点";
        public const String CASH_RECEIPT = "销售收款";
        public const String CASH_RECEIPT_REFUND = "销售退点";
        public const String CASH_OTHER_PAY = "其他付款";
        public const String CASH_OTHER_RECEIPT = "其他收款";
        
        public const String LIB_LIST = "盘点单据列表";
        public const String LIB_OVERFLOW = "盘点报溢";
        public const String LIB_LOSS = "盘点报损";

        public const String QUERY_LIB = "库存查询";
        public const String QUERY_PRODUCT_DETAIL = "进销存明细";
        public const String QUERY_CASH_DETAIL = "收付明细";
        public const String STATISTIC_PRODUCT = "进销统计";
        public const String STATISTIC_CASH = "财务统计";
        public const String STATISTIC_DEBT = "应收应付";
        public const String STATISTIC_PROFIT = "销售利润";
        //经营统计

        public const String DATA_SETTING = "数据设置";
        public const String DATA_CUSTOMER = "往来单位";
        public const String DATA_PRODUCT = "商品信息";
        public const String DATA_COMPANY = "公司信息";
        public const String ACCOUNT_INPUT = "期初库存录入";
        public const String ACCOUNT_OPEN = "期初开账";


    }
}
