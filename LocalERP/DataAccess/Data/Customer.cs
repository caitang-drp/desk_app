using System;
using System.Collections.Generic;
using System.Text;

namespace LocalERP.DataAccess.Data
{
    public class Customer
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

        private int parent;

        public int Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        private string comment;

        public string Comment
        {
            get { return comment; }
            set { comment = value; }
        }

        private string tel;

        public string Tel
        {
            get { return tel; }
            set { tel = value; }
        }
        private string phone;

        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }
        private string address;

        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        // Ç·¿î
        public double arrear;
        // ÊÕ¿î
        public double receipt;

        public Customer(string name, string comment, string tel, string phone, string address, int parent, double arrear)
        {
            this.name = name;
            this.comment = comment;
            this.tel = tel;
            this.phone = phone;
            this.address = address;
            this.parent = parent;
            this.arrear = arrear;
        }
        public Customer() { }
    }
}
