using System;
using System.Collections.Generic;
using System.Text;

namespace LocalERP.DataAccess.Data
{
    public class ProductJobRecord
    {
        private int id;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }


        private int productID;

        public int ProductID
        {
            get { return productID; }
            set { productID = value; }
        }

        private int number;

        public int Number
        {
            get { return number; }
            set { number = value; }
        }

        private int jobID;

        public int JobID
        {
            get { return jobID; }
            set { jobID = value; }
        }

        private int arrivalNum;

        public int ArrivalNum
        {
            get { return arrivalNum; }
            set { arrivalNum = value; }
        }
    }
}
