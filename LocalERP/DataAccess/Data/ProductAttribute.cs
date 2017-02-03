using System;
using System.Collections.Generic;
using System.Text;

namespace LocalERP.DataAccess.Data
{
    public class ProductAttribute
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

        private int charactorId;

        public int CharactorId
        {
            get { return charactorId; }
            set { charactorId = value; }
        }
        private int charactorValueId;

        public int CharactorValueId
        {
            get { return charactorValueId; }
            set { charactorValueId = value; }
        }

        private CharactorValue charactorValue;

        public CharactorValue CharactorValue
        {
            get { return charactorValue; }
            set { charactorValue = value; }
        }

        public ProductAttribute()
        {}
    }
}
