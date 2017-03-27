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
        public enum BillType { BuyPay=1, BuyRefund=2, SellReceipt=3, SellRefund=4, OtherPay = 5, OtherReceipt=6};

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
        public string customerName;

        public int cashDirection;
        public int arrearDirection;

        //added by stone,以上欠款
        public double previousArrears;
        // 金额
        public double amount;
        public double thisPayed;
        // 经手人
        public string handle_people;
        // 表单类型
        public BillType bill_type;

        public int status;

        public static string[] statusContext = new string[] { "未审核", "undefined", "undefined", "已审核" };

        //用于控制PayReceipt窗口的具体类型
        public static PayReceiptTypeConf PayReceiptTypeConf_BuyPay = new PayReceiptTypeConf(PayReceipt.BillType.BuyPay, UpdateType.PayReceiptUpdate, UpdateType.PayReceiptFinishUpdate, 1, -1, "采购付款", "CGFK", "供 应 商:", "付款");
        public static PayReceiptTypeConf PayReceiptTypeConf_BuyRefund = new PayReceiptTypeConf(PayReceipt.BillType.BuyRefund, UpdateType.PayReceiptUpdate, UpdateType.PayReceiptFinishUpdate, 1, 1, "采购退点", "CGTD", "供 应 商:", "退点");
        public static PayReceiptTypeConf PayReceiptTypeConf_SellReceipt = new PayReceiptTypeConf(PayReceipt.BillType.SellReceipt, UpdateType.PayReceiptUpdate, UpdateType.PayReceiptFinishUpdate, -1, 1, "销售收款", "XSSK", "客    户:", "收款");
        public static PayReceiptTypeConf PayReceiptTypeConf_SellRefund = new PayReceiptTypeConf(PayReceipt.BillType.SellRefund, UpdateType.PayReceiptUpdate, UpdateType.PayReceiptFinishUpdate, -1, -1, "销售退点", "XSTD", "客    户:", "退点");
        public static PayReceiptTypeConf PayReceiptTypeConf_OtherPay = new PayReceiptTypeConf(PayReceipt.BillType.OtherPay, UpdateType.PayReceiptUpdate, UpdateType.PayReceiptFinishUpdate, 1, -1,"其他付款", "QTFK", "往来单位", "付款");
        public static PayReceiptTypeConf PayReceiptTypeConf_OtherReceipt = new PayReceiptTypeConf(PayReceipt.BillType.OtherReceipt, UpdateType.PayReceiptUpdate, UpdateType.PayReceiptFinishUpdate,-1, 1, "其他收款", "QTSK", "往来单位", "收款");
        
        //这里要按照type值的顺序
        public static PayReceiptTypeConf[] PayReceiptTypeConfs = new PayReceiptTypeConf[] { PayReceiptTypeConf_BuyPay, PayReceiptTypeConf_BuyRefund, PayReceiptTypeConf_SellReceipt, PayReceiptTypeConf_SellRefund, PayReceiptTypeConf_OtherPay, PayReceiptTypeConf_OtherReceipt};

    }

    public class PayReceiptTypeConf
    {
        public PayReceipt.BillType type;
        public UpdateType notifyType;
        public UpdateType finishNotifyType;
        //如何表示债务
        public int arrearDirection;
        public int cashDirection;

        public string name;
        public string serial;
        public string customer;
        public string business;

        public PayReceiptTypeConf(PayReceipt.BillType type, UpdateType notifyType, UpdateType finishNotifyType, int arrearDirection, int cashDirection, string name, string code, string customer, string business)
        {
            this.type = type;
            this.notifyType = notifyType;
            this.finishNotifyType = finishNotifyType;

            this.arrearDirection = arrearDirection;
            this.cashDirection = cashDirection;

            this.name = name;
            this.serial = code;

            this.customer = customer;
            this.business = business;
        }
    }
}
