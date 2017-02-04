using System;
using System.Collections.Generic;
using System.Text;

namespace LocalERP.DataAccess.Data
{
    public class ProductSKU
    {
        private int id;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        private Product product;

        public Product Product
        {
            get { return product; }
            set { product = value; }
        }

        private int num;

        public int Num
        {
            get { return num; }
            set { num = value; }
        }

        private double price;

        public double Price
        {
            get { return price; }
            set { price = value; }
        }

        private CharactorValue color;

        public CharactorValue Color
        {
            get { return color; }
            set { color = value; }
        }

        private CharactorValue size;

        public CharactorValue Size
        {
            get { return size; }
            set { size = value; }
        }

        public string getName() {
            return string.Format("{0}/{1}", color.Name, size.Name);
        }

        public ProductSKU(string name, double price, string comment)
        {
            this.price = price;
        }
        public ProductSKU() { }
    }
}
