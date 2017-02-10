using System;
using System.Collections.Generic;
using System.Text;

namespace LocalERP.DataAccess.Data
{
    // 该类对应数据库表单： PayReceipt
    public class PayReceipt
    {
        // 应收应付，(销售收款， 销售退点， 采购付款， 采购退点)
        public enum BillType { SellReceipt = 1, SellRefund, BuyPay, BuyRefund};

        // 数据表的唯一id
        public int id;
        // 单据编号
        public string serial;
        // 表单相关日期
        public DateTime bill_time;
        // 备注
        public string comment;
        // 用户id
        public int customer_id;
        // 金额
        public double amount;
        // 经手人
        public string handle_people;
        // 表单类型
        public BillType bill_type;
    }
}
