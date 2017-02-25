using System;
using System.Collections.Generic;
using System.Text;
using LocalERP.WinForm;

namespace LocalERP.DataAccess.Data
{
    // 该类对应数据库表单： PayReceipt
    public class PayReceipt
    {
        // 应收应付，(采购付款， 采购退点， 销售收款， 销售退点)
        //重新排序
        public enum BillType { BuyPay=1, BuyRefund=2, SellReceipt=3, SellRefund=4};
        public string get_bill_type_name(int y)
        {
            string ans = "未知";
            BillType x = (BillType)y;

            switch (x)
            {
                case BillType.SellReceipt:
                    ans = "销售收款";
                    break;
                case BillType.SellRefund:
                    ans = "销售退点";
                    break;
                case BillType.BuyPay:
                    ans = "采购付款";
                    break;
                case BillType.BuyRefund:
                    ans = "采购退点";
                    break;
            }

            return ans;
        }

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
        //added by stone,以上欠款
        public double previousArrears;
        // 金额
        public double amount;
        // 经手人
        public string handle_people;
        // 表单类型
        public BillType bill_type;

        public int status;

        //用于控制PayReceipt窗口的具体类型
        public static PayReceiptTypeConf PayReceiptTypeConf_BuyPay = new PayReceiptTypeConf(PayReceipt.BillType.BuyPay, UpdateType.PayReceiptUpdate, UpdateType.PayReceiptFinishUpdate, 1, "采购付款", "CGYF", "供 应 商:", "付款");
        public static PayReceiptTypeConf PayReceiptTypeConf_BuyRefund = new PayReceiptTypeConf(PayReceipt.BillType.BuyRefund, UpdateType.PayReceiptUpdate, UpdateType.PayReceiptFinishUpdate, -1, "采购退点", "CGTD", "供 应 商:", "退点");
        public static PayReceiptTypeConf PayReceiptTypeConf_SellReceipt = new PayReceiptTypeConf(PayReceipt.BillType.SellReceipt, UpdateType.PayReceiptUpdate, UpdateType.PayReceiptFinishUpdate, -1, "销售收款", "XSYS", "客    户:", "收款");
        public static PayReceiptTypeConf PayReceiptTypeConf_SellRefund = new PayReceiptTypeConf(PayReceipt.BillType.SellRefund, UpdateType.PayReceiptUpdate, UpdateType.PayReceiptFinishUpdate, 1, "销售退点", "XSTD", "客    户:", "退点");
        
        //这里要按照type值的顺序
        public static PayReceiptTypeConf[] PayReceiptTypeConfs = new PayReceiptTypeConf[] { PayReceiptTypeConf_BuyPay, PayReceiptTypeConf_BuyRefund, PayReceiptTypeConf_SellReceipt, PayReceiptTypeConf_SellRefund };

    }

    public class PayReceiptTypeConf
    {
        public PayReceipt.BillType type;
        public UpdateType notifyType;
        public UpdateType finishNotifyType;
        public int flowType;

        public string name;
        public string code;
        public string customer;
        public string business;

        public PayReceiptTypeConf(PayReceipt.BillType type, UpdateType notifyType, UpdateType finishNotifyType, int flowType, string name, string code, string customer, string business)
        {
            this.type = type;
            this.notifyType = notifyType;
            this.finishNotifyType = finishNotifyType;

            this.flowType = flowType;

            this.name = name;
            this.code = code;

            this.customer = customer;
            this.business = business;
        }
    }
}
