/*created by stone: 用于定义各个功能的名称*/

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using LocalERP.DataAccess.DataDAO;

namespace LocalERP.DataAccess.Utility
{
    public static class ConfUtility
    {
        //mdb会自动从根目录拷贝到release下，而rpt不会，是不是设置的问题
        public static readonly string CONN_STRING = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source="+Application.StartupPath+"\\ERP.mdb;Persist Security Info=True";   //正式发布时数量库路径，放在bin目录下
        //public static readonly string CONN_STRING = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "..\\..\\..\\ERP.mdb;Persist Security Info=True";   //调试时数据库路径，防止放在bin目录下被清空

        public static string cir_report_path = Application.StartupPath + "\\circulation_report.grf";
        //public static string cir_report_path = Application.StartupPath + "..\\..\\..\\grid++\\circulation_report.grf";

        public static string letter_report_path = Application.StartupPath + "\\letter_report.grf";

        private const String SOFT_NAME = "飞翔企业管理软件";
        private const String PRODUCT_NAME = "飞翔进销存软件";

        //2018-3-22，如果数据库Conf表有设置软件的名称，则用数据库的，否则使用默认
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
