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
        public static string debugPath = "";
        public static string debugPathReport = "";

        //public static string debugPath = "\\..\\..";
        //public static string debugPathReport = debugPath + "\\grid++";

        //mdb���Զ��Ӹ�Ŀ¼������release�£���rpt���ᣬ�ǲ������õ�����
        public static readonly string CONN_STRING = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + debugPath + "\\ERP.mdb;Persist Security Info=True";   //��ʽ����ʱ������·��������binĿ¼��
        public static string cir_report_path = Application.StartupPath + debugPathReport + "\\circulation_report.grf";
        public static string letter_report_path = Application.StartupPath + debugPathReport + "\\letter_report.grf";
        public static string check_report_path = Application.StartupPath + debugPathReport + "\\check_report.grf";

        private const String SOFT_NAME = "������ҵ�������";
        private const String PRODUCT_NAME = "������ҵ�������";

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

        public static string getContract() {
            string phone = ConfDao.getInstance().Get(6).ToString();
            string tel = ConfDao.getInstance().Get(7).ToString();

            string temp = "";
            if (string.IsNullOrEmpty(phone))
                temp = tel;
            else if (string.IsNullOrEmpty(tel))
                temp = phone;
            else
                temp = string.Format("{0} / {1}", tel, phone);
            return temp;
        }

        public static string GetBackupPath()
        {
            string path = ConfDao.getInstance().Get(13);
            if (!string.IsNullOrEmpty(path) && !path.Equals("0") && !path.Equals("1"))
            {
                return path;
            }
            return "D:\\�����������";
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


        public static string GetLastPayReceiptOpen()
        {
            string serialType = ConfDao.getInstance().Get(16);
            if (!string.IsNullOrEmpty(serialType) && serialType.Equals("lastPayReceiptOpen"))
            {
                return "lastPayReceiptOpen";
            }
            return "lastPayReceiptClose";
        }
    }
}
