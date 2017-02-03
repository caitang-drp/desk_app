using System;
using System.Collections.Generic;
using System.Text;

namespace LocalERP.DataAccess.Data
{
    public class ElementFlow
    {
        private int elementID;

        public int ElementID
        {
            get { return elementID; }
            set { elementID = value; }
        }

        private string elementName;

        public string ElementName
        {
            get { return elementName; }
            set { elementName = value; }
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

        public ElementFlow(int elementID, string elementName, int flowType, int num, int accumulativeNum, string comment, DateTime flowTime, int type) {
            this.elementID = elementID;
            this.elementName = elementName;
            this.flowType = flowType;
            this.num = num;
            this.accumulativeNum = accumulativeNum;
            this.comment = comment;
            this.flowTime = flowTime;
            this.type = type;
        }
    }
}
