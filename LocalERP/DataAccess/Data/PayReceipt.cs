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
