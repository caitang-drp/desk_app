//create by stone 2017-02-07：用于表示不锈钢货品
using System;
using System.Collections.Generic;
using System.Text;
using LocalERP.DataAccess.DataDAO;

namespace LocalERP.DataAccess.Data
{
    public class ProductStainless : Product
    {
        private double quantityPerPiece;

        public double QuantityPerPiece
        {
            get { return quantityPerPiece; }
            set { quantityPerPiece = value; }
        }

        public ProductStainless() { }

        public ProductStainless(string serial, string name, int categoryID, double pricePurchase, double priceSell, string unit, double quantityPerPiece, string comment, bool disable)
            : base(serial, name, categoryID, pricePurchase, priceSell, unit, comment, disable)
        {
            this.quantityPerPiece = quantityPerPiece;
        }
    }
}
