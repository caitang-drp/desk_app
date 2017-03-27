using System;
using System.Collections.Generic;
using System.Text;
using LocalERP.WinForm;

namespace LocalERP.DataAccess.Data
{
    // �����Ӧ���ݿ���� PayReceipt
    public class PayReceipt
    {
        // Ӧ��Ӧ����(�ɹ���� �ɹ��˵㣬 �����տ �����˵�)
        //��������
        public enum BillType { BuyPay=1, BuyRefund=2, SellReceipt=3, SellRefund=4, OtherPay = 5, OtherReceipt=6};

        // ���ݱ��Ψһid
        public int id;
        // ���ݱ��
        public string serial;
        // ���������
        public DateTime bill_time;
        // ��ע
        public string comment;
        // �û�id
        public int customer_id;
        public string customerName;

        public int cashDirection;
        public int arrearDirection;

        //added by stone,����Ƿ��
        public double previousArrears;
        // ���
        public double amount;
        public double thisPayed;
        // ������
        public string handle_people;
        // ������
        public BillType bill_type;

        public int status;

        public static string[] statusContext = new string[] { "δ���", "undefined", "undefined", "�����" };

        //���ڿ���PayReceipt���ڵľ�������
        public static PayReceiptTypeConf PayReceiptTypeConf_BuyPay = new PayReceiptTypeConf(PayReceipt.BillType.BuyPay, UpdateType.PayReceiptUpdate, UpdateType.PayReceiptFinishUpdate, 1, -1, "�ɹ�����", "CGFK", "�� Ӧ ��:", "����");
        public static PayReceiptTypeConf PayReceiptTypeConf_BuyRefund = new PayReceiptTypeConf(PayReceipt.BillType.BuyRefund, UpdateType.PayReceiptUpdate, UpdateType.PayReceiptFinishUpdate, 1, 1, "�ɹ��˵�", "CGTD", "�� Ӧ ��:", "�˵�");
        public static PayReceiptTypeConf PayReceiptTypeConf_SellReceipt = new PayReceiptTypeConf(PayReceipt.BillType.SellReceipt, UpdateType.PayReceiptUpdate, UpdateType.PayReceiptFinishUpdate, -1, 1, "�����տ�", "XSSK", "��    ��:", "�տ�");
        public static PayReceiptTypeConf PayReceiptTypeConf_SellRefund = new PayReceiptTypeConf(PayReceipt.BillType.SellRefund, UpdateType.PayReceiptUpdate, UpdateType.PayReceiptFinishUpdate, -1, -1, "�����˵�", "XSTD", "��    ��:", "�˵�");
        public static PayReceiptTypeConf PayReceiptTypeConf_OtherPay = new PayReceiptTypeConf(PayReceipt.BillType.OtherPay, UpdateType.PayReceiptUpdate, UpdateType.PayReceiptFinishUpdate, 1, -1,"��������", "QTFK", "������λ", "����");
        public static PayReceiptTypeConf PayReceiptTypeConf_OtherReceipt = new PayReceiptTypeConf(PayReceipt.BillType.OtherReceipt, UpdateType.PayReceiptUpdate, UpdateType.PayReceiptFinishUpdate,-1, 1, "�����տ�", "QTSK", "������λ", "�տ�");
        
        //����Ҫ����typeֵ��˳��
        public static PayReceiptTypeConf[] PayReceiptTypeConfs = new PayReceiptTypeConf[] { PayReceiptTypeConf_BuyPay, PayReceiptTypeConf_BuyRefund, PayReceiptTypeConf_SellReceipt, PayReceiptTypeConf_SellRefund, PayReceiptTypeConf_OtherPay, PayReceiptTypeConf_OtherReceipt};

    }

    public class PayReceiptTypeConf
    {
        public PayReceipt.BillType type;
        public UpdateType notifyType;
        public UpdateType finishNotifyType;
        //��α�ʾծ��
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
