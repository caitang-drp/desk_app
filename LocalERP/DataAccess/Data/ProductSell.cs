using System;
using System.Collections.Generic;
using System.Text;

namespace LocalERP.DataAccess.Data
{
    public class ProductSell
    {
        private int id;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private DateTime sellTime;

        public DateTime SellTime
        {
            get { return sellTime; }
            set { sellTime = value; }
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

        private double totalPrice;

        public double TotalPrice
        {
            get { return totalPrice; }
            set { totalPrice = value; }
        }

        public ProductSell(int id, string name, DateTime sellTime, string comment, int customerID) {
            this.id = id;
            this.name = name;
            this.sellTime = sellTime;
            this.comment = comment;
            this.customerID = customerID;
        }

        public ProductSell() { }

        public static string[] statusEnum = new string[] {"未审核","已下单", "","已审核" }; 
    }
}
