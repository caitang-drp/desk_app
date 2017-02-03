using System;
using System.Collections.Generic;
using System.Text;

namespace LocalERP.DataAccess.Data
{
    public class ElementOrderRecord
    {
        private int id;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        private int elementID;

        public int ElementID
        {
            get { return elementID; }
            set { elementID = value; }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private int number;

        public int Number
        {
            get { return number; }
            set { number = value; }
        }

        private double price;

        public double Price
        {
            get { return price; }
            set { price = value; }
        }

        private int orderID;

        public int OrderID
        {
            get { return orderID; }
            set { orderID = value; }
        }

        private int arrivalNum;

        public int ArrivalNum
        {
            get { return arrivalNum; }
            set { arrivalNum = value; }
        }
    }
}
