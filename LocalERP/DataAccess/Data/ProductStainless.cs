//create by stone 2017-02-07：用于表示不锈钢货品
using System;
using System.Collections.Generic;
using System.Text;
using LocalERP.DataAccess.DataDAO;

namespace LocalERP.DataAccess.Data
{
    public class ProductStainless : Product
    {
        private int quantityPerPiece;

        public int QuantityPerPiece
        {
            get { return quantityPerPiece; }
            set { quantityPerPiece = value; }
        }

        public ProductStainless() { }

        public ProductStainless(string serial, string name, int categoryID, double pricePurchase, double priceSell, string unit, int quantityPerPiece, string comment, bool disable)
            : base(serial, name, categoryID, pricePurchase, priceSell, unit, comment, disable)
        {
            this.quantityPerPiece = quantityPerPiece;
        }
    }
}
