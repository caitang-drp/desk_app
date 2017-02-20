using System;
using System.Collections.Generic;
using System.Text;

namespace LocalERP.DataAccess.Data
{
    public class ProductStainlessCirculationRecord : ProductCirculationRecord
    {
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
    }
}
