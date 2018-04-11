using System;
using System.Collections.Generic;
using System.Text;

namespace LocalERP.DataAccess.Data
{
    public class ProductCirculationRecord
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

        private Product product;

        public Product Product
        {
            get { return product; }
            set { product = value; }
        }

        //stone: this is for reports, not reasonable
        private string productName;

        public string ProductName
        {
            get { return productName; }
            set { productName = value; }
        }

        private string unit;

        public string Unit
        {
            get { return unit; }
            set { unit = value; }
        }

        private int totalNum;

        public int TotalNum
        {
            get { return totalNum; }
            set { totalNum = value; }
        }

        //从Stainless那里搬过来
        private int pieces;

        public int Pieces
        {
            get { return pieces; }
            set { pieces = value; }
        }

        private bool piecesNull = false;

        public bool PiecesNull
        {
            get { return piecesNull; }
            set { piecesNull = value; }
        }

        private int circulationID;

        public int CirculationID
        {
            get { return circulationID; }
            set { circulationID = value; }
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

        private string comment;

        public string Comment
        {
            get { return comment; }
            set { comment = value; }
        }

    }
}
