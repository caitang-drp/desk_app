using System;
using System.Collections.Generic;
using System.Text;
using LocalERP.WinForm;
using LocalERP.DataAccess.Utility;

namespace LocalERP.DataAccess.Data
{
    public class Consume
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

        private DateTime consumeTime;

        public DateTime ConsumeTime
        {
            get { return consumeTime; }
            set { consumeTime = value; }
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

        private int cardID;

        public int CardID
        {
            get { return cardID; }
            set { cardID = value; }
        }

        private int number;

        public int Number
        {
            get { return number; }
            set { number = value; }
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

        public Consume(int id, string name, DateTime cardTime, string comment, int customerID) {
            this.id = id;
            this.code = name;
            this.comment = comment;
            this.customerID = customerID;
        }

        public Consume() { }

        public static string[] consumeStatusContext = new string[] {"Œ¥…Û∫À","undefined","undefined", "“—…Û∫À" };

        public enum ConsumeType
        {
            card = 1, cash = 2
        }
    }
}
