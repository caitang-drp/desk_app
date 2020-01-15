using System;
using System.Collections.Generic;
using System.Text;
using LocalERP.WinForm;
using LocalERP.DataAccess.Utility;

namespace LocalERP.DataAccess.Data
{
    public class Card
    {
        private int id;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        private string code;

        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        private DateTime cardTime;

        public DateTime CardTime
        {
            get { return cardTime; }
            set { cardTime = value; }
        }

        private string comment;

        public string Comment
        {
            get { return comment; }
            set { comment = value; }
        }
        
        private int status;
        public int Status
        {
            get { return status; }
            set { status = value; }
        }

        private int customerID;

        public int CustomerID
        {
            get { return customerID; }
            set { customerID = value; }
        }

        private string customerName;

        public string CustomerName
        {
            get { return customerName; }
            set { customerName = value; }
        }

        private double total;

        public double Total
        {
            get { return total; }
            set { total = value; }
        }

        private string oper;

        public string Oper
        {
            get { return oper; }
            set { oper = value; }
        }

        private int type;

        public int Type
        {
            get { return type; }
            set { type = value; }
        }

        public Card(int id, string name, DateTime cardTime, string comment, int customerID) {
            this.id = id;
            this.code = name;
            //this.circulationTime = sellTime;
            this.comment = comment;
            this.customerID = customerID;
        }

        public Card() { }

        private List<ProductCirculationRecord> records;

        public List<ProductCirculationRecord> Records
        {
            get { return records; }
            set { records = value; }
        }

        public string getFixedString(string str, int length){
            Encoding coding = Encoding.GetEncoding("gb2312");
            int dcount = 0;
            foreach (char ch in str.ToCharArray())
            {
                if (coding.GetByteCount(ch.ToString()) == 2)
                    dcount++;
            }
            string w = str.PadRight(length - dcount, ' ');
            return w;
        }

        public string getRecordsTxt()
        {
            StringBuilder sb = new StringBuilder();
            int i = 0;
            foreach (ProductCirculationRecord record in Records)
            {
                string name = this.getFixedString(record.ProductName, 18);
                string line = string.Format("{0}/{1,-8}/{2,-8}", name, record.Price, record.TotalNum);
                if (i < Records.Count - 1)
                    sb.AppendLine(line);
                else
                    sb.Append(line);
                i++;
            }
            return sb.ToString();
        }

        public static string[] circulationStatusContext = new string[] {"Î´ÉóºË","undefined","undefined", "ÒÑÉóºË" };

        //±ØÐë¸úÊÕ¸¶Ò»ÖÂ
        public enum CirculationType
        {
            purchase = 1, purchaseBack = 2, sell = 3, sellBack = 4, libOverflow = 5, libLoss = 6, manuCost=7, manuIn = 8
        }

        public int get_circulation_type_value(CirculationType x)
        {
            return Convert.ToInt32(x);
        }
    }
}
