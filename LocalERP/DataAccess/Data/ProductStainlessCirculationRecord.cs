using System;
using System.Collections.Generic;
using System.Text;

namespace LocalERP.DataAccess.Data
{
    public class ProductStainlessCirculationRecord : ProductCirculationRecord
    {
        private double quantityPerPiece;

        public double QuantityPerPiece
        {
            get { return quantityPerPiece; }
            set { quantityPerPiece = value; }
        }

        private double pieces;

        public double Pieces
        {
            get { return pieces; }
            set { pieces = value; }
        }

    }
}
