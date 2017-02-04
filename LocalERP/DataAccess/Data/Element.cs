using System;
using System.Collections.Generic;
using System.Text;

namespace LocalERP.DataAccess.Data
{
    public class Element
    {
        private int id;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string note;

        public string Note
        {
            get { return note; }
            set { note = value; }
        }

        private int num;

        public int Num
        {
            get { return num; }
            set { num = value; }
        }

        private double price;
        public double Price { get { return price; } set { price = value; } }

        public Element() { }

        public Element(string name, double price, string note) {
            this.name = name;
            this.price = price;
            this.note = note;
        }
    }
}
