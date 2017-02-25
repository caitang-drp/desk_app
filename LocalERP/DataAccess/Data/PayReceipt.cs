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
        public enum BillType { BuyPay=1, BuyRefund=2, SellReceipt=3, SellRefund=4};
        public string get_bill_type_name(int y)
        {
            string ans = "δ֪";
            BillType x = (BillType)y;

            switch (x)
            {
                case BillType.SellReceipt:
                    ans = "�����տ�";
                    break;
                case BillType.SellRefund:
                    ans = "�����˵�";
                    break;
                case BillType.BuyPay:
                    ans = "�ɹ�����";
                    break;
                case BillType.BuyRefund:
                    ans = "�ɹ��˵�";
                    break;
            }

            return ans;
        }

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
        //added by stone,����Ƿ��
        public double previousArrears;
        // ���
        public double amount;
        // ������
        public string handle_people;
        // ������
        public BillType bill_type;

        public int status;

        //���ڿ���PayReceipt���ڵľ�������
        public static PayReceiptTypeConf PayReceiptTypeConf_BuyPay = new PayReceiptTypeConf(PayReceipt.BillType.BuyPay, UpdateType.PayReceiptUpdate, UpdateType.PayReceiptFinishUpdate, 1, "�ɹ�����", "CGYF", "�� Ӧ ��:", "����");
        public static PayReceiptTypeConf PayReceiptTypeConf_BuyRefund = new PayReceiptTypeConf(PayReceipt.BillType.BuyRefund, UpdateType.PayReceiptUpdate, UpdateType.PayReceiptFinishUpdate, -1, "�ɹ��˵�", "CGTD", "�� Ӧ ��:", "�˵�");
        public static PayReceiptTypeConf PayReceiptTypeConf_SellReceipt = new PayReceiptTypeConf(PayReceipt.BillType.SellReceipt, UpdateType.PayReceiptUpdate, UpdateType.PayReceiptFinishUpdate, -1, "�����տ�", "XSYS", "��    ��:", "�տ�");
        public static PayReceiptTypeConf PayReceiptTypeConf_SellRefund = new PayReceiptTypeConf(PayReceipt.BillType.SellRefund, UpdateType.PayReceiptUpdate, UpdateType.PayReceiptFinishUpdate, 1, "�����˵�", "XSTD", "��    ��:", "�˵�");
        
        //����Ҫ����typeֵ��˳��
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
