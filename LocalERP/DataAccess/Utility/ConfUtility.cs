/*created by stone: ���ڶ���������ܵ�����*/

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using LocalERP.DataAccess.DataDAO;

namespace LocalERP.DataAccess.Utility
{
    public static class ConfUtility
    {
        //mdb���Զ��Ӹ�Ŀ¼������release�£���rpt���ᣬ�ǲ������õ�����
        public static readonly string CONN_STRING = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source="+Application.StartupPath+"\\ERP.mdb;Persist Security Info=True";   //��ʽ����ʱ������·��������binĿ¼��
        //public static readonly string CONN_STRING = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "..\\..\\..\\ERP.mdb;Persist Security Info=True";   //����ʱ���ݿ�·������ֹ����binĿ¼�±����

        public static string cir_report_path = Application.StartupPath + "\\circulation_report.grf";
        //public static string cir_report_path = Application.StartupPath + "..\\..\\..\\grid++\\circulation_report.grf";

        public static string letter_report_path = Application.StartupPath + "\\letter_report.grf";

        private const String SOFT_NAME = "������ҵ�������";
        private const String PRODUCT_NAME = "������������";

        //2018-3-22��������ݿ�Conf����������������ƣ��������ݿ�ģ�����ʹ��Ĭ��
        public static String GetSoftName()
        {
            string soft = ConfDao.getInstance().Get(10).ToString();
            if (string.IsNullOrEmpty(soft) || soft == "0")
                soft = ConfUtility.SOFT_NAME;
            return soft;
        }

        public static String GetProductName()
        {
            string product = ConfDao.getInstance().Get(11).ToString();
            if (string.IsNullOrEmpty(product) || product == "0")
                product = ConfUtility.PRODUCT_NAME;
            return product;
        }

        public static String GetProductNameWithCopyRight()
        {
            string product = ConfDao.getInstance().Get(11).ToString();
            if (string.IsNullOrEmpty(product) || product == "0")
                product = ConfUtility.PRODUCT_NAME;
            return product + " copyright (c) 2018";
        }

        public static string GetSerialType() {
            string serialType = ConfDao.getInstance().Get(19);
            if (!string.IsNullOrEmpty(serialType) && serialType.Equals("serialType2"))
            {
                return "serialType2";
            }
            return "serialType1";
        }

        public static string GetBackFreightOpen()
        {
            string serialType = ConfDao.getInstance().Get(18);
            if (!string.IsNullOrEmpty(serialType) && serialType.Equals("backFreightOpen"))
            {
                return "backFreightOpen";
            }
            return "backFreightClose";
        }

        public static string GetPrintLetterOpen()
        {
            string serialType = ConfDao.getInstance().Get(17);
            if (!string.IsNullOrEmpty(serialType) && serialType.Equals("printLetterOpen"))
            {
                return "printLetterOpen";
            }
            return "printLetterClose";
        }
    }
}
