using System;
using System.Collections.Generic;
using System.Text;

namespace LocalERP.DataAccess.Data
{
    public class ElementOrder
    {
        private int id;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        private string serial;

        public string Serial
        {
            get { return serial; }
            set { serial = value; }
        }

        private DateTime orderTime;

        public DateTime OrderTime
        {
            get { return orderTime; }
            set { orderTime = value; }
        }

        private String comment;
        public String Comment {
            get { return comment; }
            set { comment = value; }
        }

        private int status;
        public int Status {
            get { return status; }
            set { status = value; }
        }

        public enum StatusEnum{
            apply=0, approval=1, partArrival=2, arrvival=3
        }

        public static string[] statusEnum = new string[] { "未下单", "已下单","部分到货","已结束" }; 
    }
}
