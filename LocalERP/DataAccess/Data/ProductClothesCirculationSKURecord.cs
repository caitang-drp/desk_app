using System;
using System.Collections.Generic;
using System.Text;

namespace LocalERP.DataAccess.Data
{
    public class ProductClothesCirculationSKURecord
    {
        private int id;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }


        private int productSKUID;

        public int ProductSKUID
        {
            get { return productSKUID; }
            set { productSKUID = value; }
        }

        private ProductSKU productSKU;

        public ProductSKU ProductSKU
        {
            get { return productSKU; }
            set { productSKU = value; }
        }

        private int num;

        public int Num
        {
            get { return num; }
            set { num = value; }
        }

        private int recordID;

        public int RecordID
        {
            get { return recordID; }
            set { recordID = value; }
        }

        private double price;

        public double Price
        {
            get { return price; }
            set { price = value; }
        }
    }
}
