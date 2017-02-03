using System;
using System.Collections.Generic;
using System.Text;

namespace LocalERP.DataAccess.Data
{
    public class ProductSellRecord
    {
        private int id;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }


        private int productID;

        public int ProductID
        {
            get { return productID; }
            set { productID = value; }
        }

        //stone: this is for reports, not reasonable
        private string productName;

        public string ProductName
        {
            get { return productName; }
            set { productName = value; }
        }

        private int num;

        public int Num
        {
            get { return num; }
            set { num = value; }
        }

        private int sellID;

        public int SellID
        {
            get { return sellID; }
            set { sellID = value; }
        }

        private double price;

        public double Price
        {
            get { return price; }
            set { price = value; }
        }

        private double totalPrice;

        public double TotalPrice
        {
            get { return totalPrice; }
            set { totalPrice = value; }
        }
    }
}
