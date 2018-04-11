using System;
using System.Collections.Generic;
using System.Text;

namespace LocalERP.DataAccess.Data
{
    public class ProductStainlessCirculationRecord : ProductCirculationRecord
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

        private bool quantityNull = false;

        public bool QuantityNull
        {
            get { return quantityNull; }
            set { quantityNull = value; }
        }
    }
}
