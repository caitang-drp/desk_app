using System;
using System.Collections.Generic;
using System.Text;

namespace LocalERP.DataAccess.Data
{
    public class ProductJob
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

        private DateTime jobTime;

        public DateTime JobTime
        {
            get { return jobTime; }
            set { jobTime = value; }
        }

        private string comment;

        public string Comment
        {
            get { return comment; }
            set { comment = value; }
        }

        private int status;
        public int Status
        {
            get { return status; }
            set { status = value; }
        }

        public ProductJob(int id, string name, DateTime jobTime, string comment) {
            this.id = id;
            this.name = name;
            this.jobTime = jobTime;
            this.comment = comment;
        }

        public ProductJob() { }

        public static string[] statusEnum = new string[] { "未领配件", "已领配件", "部分完成", "已结束" }; 
    }
}
