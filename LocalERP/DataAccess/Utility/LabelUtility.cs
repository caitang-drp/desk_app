/*created by stone: 用于定义各个功能的名称*/

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using LocalERP.DataAccess.DataDAO;

namespace LocalERP.DataAccess.Utility
{
    public static class LabelUtility
    {
        public const String PURCHASE_MANAGE = "采购管理";
        public const String PURCHASE_LIST = "采购列表";
        public const String PURCHASE = "采购入库";
        public const String PURCHASE_BACK = "采购退货";

        //added by stone 2017-02-04
        public const String MANU_MANAGE = "生产管理";
        public const String MANUFACTURE_LIST = "生产单据列表";
        public const string MANU_COST = "生产消耗";
        public const string MANU_IN = "生产入库";

        public const String SELL_MANAGE = "销售管理";
        public const String SELL_LIST = "销售单据列表";
        public const String SELL = "销售出库";
        public const String SELL_BACK = "销售退货";

        public const String CASH_MANAGE = "收款付款";
        public const String CASH_LIST = "收付单据列表";
        public const String CASH_PAY = "采购付款";
        public const String CASH_PAY_REFUND = "采购退点";
        public const String CASH_RECEIPT = "销售收款";
        public const String CASH_RECEIPT_REFUND = "销售退点";
        public const String CASH_OTHER_PAY = "其他付款";
        public const String CASH_OTHER_RECEIPT = "其他收款";

        public const String LIB_MANAGE = "库存盘点";
        public const String LIB_LIST = "盘点单据列表";
        public const String LIB_OVERFLOW = "盘点报溢";
        public const String LIB_LOSS = "盘点报损";

        public const String QUERY_MANAGE = "查询统计";
        public const String QUERY_LIB = "库存查询";
        public const String QUERY_PRODUCT_DETAIL = "进销存明细";
        public const String QUERY_CASH_DETAIL = "对账明细";
        public const String STATISTIC_PRODUCT = "进销统计";
        public const String STATISTIC_CASH = "财务统计";
        public const String STATISTIC_DEBT = "应收应付";
        public const String STATISTIC_PROFIT = "利润计算";

        public const String DATA_SETTING = "数据设置";
        public const String DATA_CUSTOMER = "往来单位";
        public const String DATA_PRODUCT = "货品信息";
        public const String DATA_COMPANY = "公司信息";
        public const String ACCOUNT_INPUT = "期初库存录入";
        public const String ACCOUNT_OPEN = "期初开账";

        public const string THIS_PAY = "本单已付:";
        public const string THIS_RECEIPT = "本单已收:";

        public const string LAST_PAY = "上次已付：";
        public const string LAST_RECEIPT = "上次来款：";

        //消费管理
        public const String CONSUME_MANAGE = "消费管理";
        public const String CONSUME_LIST = "消费列表";
        public const String CONSUME_ADD = "新增消费";

        public const String CARD_MANAGE = "卡片管理";
        public const String CARD_LIST = "卡片列表";
        public const String CARD_ADD = "新增卡片";

    }
}
