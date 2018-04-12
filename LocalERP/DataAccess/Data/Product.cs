using System;
using System.Collections.Generic;
using System.Text;

namespace LocalERP.DataAccess.Data
{
    public class Product
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

        private string serial;

        public string Serial
        {
            get { return serial; }
            set { serial = value; }
        }

        private int categoryID;

        public int CategoryID
        {
            get { return categoryID; }
            set { categoryID = value; }
        }

        private string comment;

        public string Comment
        {
            get { return comment; }
            set { comment = value; }
        }

        private int num;

        public int Num
        {
            get { return num; }
            set { num = value; }
        }

        private double pricePurchase;

        public double PricePurchase
        {
            get { return pricePurchase; }
            set { pricePurchase = value; }
        }

        private double priceCost;

        public double PriceCost
        {
            get
            {
                if (priceCost <= 0)
                    return PricePurchase;
                else
                    return priceCost;
            }
            set { priceCost = value; }
        }

        private double priceSell;

        public double PriceSell
        {
            get { return priceSell; }
            set { priceSell = value; }
        }

        private string unit;

        public string Unit
        {
            get { return unit; }
            set { unit = value; }
        }

        private bool disable;

        public bool Disable
        {
            get { return disable; }
            set { disable = value; }
        }

        public Product(string name, int categoryID, double price, string comment) {
            this.name = name;
            this.categoryID = categoryID;
            this.pricePurchase = price;
            this.comment = comment;
        }

        public Product(string serial, string name, int categoryID, double pricePurchase, double priceSell, string unit, string comment, bool disable)
        {
            this.serial = serial;
            this.name = name;
            this.categoryID = categoryID;
            this.pricePurchase = pricePurchase;
            this.priceSell = priceSell;
            this.priceCost = pricePurchase;
            this.unit = unit;
            this.comment = comment;
            this.disable = disable;
        }

        public Product() { }

    }
}
