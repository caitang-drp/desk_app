using System;
using System.Collections.Generic;
using System.Text;

namespace LocalERP.DataAccess.Data
{
    // �����Ӧ���ݿ���� PayReceipt
    public class PayReceipt
    {
        // Ӧ��Ӧ����(�����տ �����˵㣬 �ɹ���� �ɹ��˵�)
        public enum BillType { SellReceipt = 1, SellRefund, BuyPay, BuyRefund};
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
        // ���
        public double amount;
        // ������
        public string handle_people;
        // ������
        public BillType bill_type;
    }
}
