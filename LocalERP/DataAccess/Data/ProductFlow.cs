using System;
using System.Collections.Generic;
using System.Text;

namespace LocalERP.DataAccess.Data
{
    public class ProductFlow
    {
        private int productID;

        public int ProductID
        {
            get { return productID; }
            set { productID = value; }
        }

        private string productName;

        public string ProductName
        {
            get { return productName; }
            set { productName = value; }
        }

        private int flowType;

        public int FlowType
        {
            get { return flowType; }
            set { flowType = value; }
        }

        private int num;

        public int Num
        {
            get { return num; }
            set { num = value; }
        }

        private int accumulativeNum;

        public int AccumulativeNum
        {
            get { return accumulativeNum; }
            set { accumulativeNum = value; }
        }

        private string comment;

        public string Comment
        {
            get { return comment; }
            set { comment = value; }
        }

        private DateTime flowTime;

        public DateTime FlowTime
        {
            get { return flowTime; }
            set { flowTime = value; }
        }

        private int type;

        public int Type
        {
            get { return type; }
            set { type = value; }
        }

        public ProductFlow(int productID, string productName, int flowType, int num, int accumulativeNum, string comment, DateTime time, int type) {
            this.productID = productID;
            this.productName = productName;
            this.flowType = flowType;
            this.num = num;
            this.accumulativeNum = accumulativeNum;
            this.comment = comment;
            this.flowTime = time;
            this.type = type;
        }
    }
}
