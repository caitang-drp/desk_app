//create by stone 2017-02-07：用于表示不锈钢货品
using System;
using System.Collections.Generic;
using System.Text;

namespace LocalERP.DataAccess.Data
{
    public class ProductStainless : Product
    {
        private string serial;

        public string Serial
        {
            get { return serial; }
            set { serial = value; }
        }

        private int quantityPerPiece;

        public int QuantityPerPiece
        {
            get { return quantityPerPiece; }
            set { quantityPerPiece = value; }
        }

        public ProductStainless(string serial, string name, int categoryID, double pricePurchase, double priceSell, string unit, int quantityPerPiece, string comment)
            :base(name, categoryID, pricePurchase, priceSell, unit, comment){

            this.serial = serial;
            this.quantityPerPiece = quantityPerPiece;
        }
    }
}
