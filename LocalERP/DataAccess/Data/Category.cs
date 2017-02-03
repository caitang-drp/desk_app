using System;
using System.Collections.Generic;
using System.Text;

namespace LocalERP.DataAccess.Data
{
    public class Category
    {
        private int id;

        public int Id
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

        private int left;

        public int Left
        {
            get { return left; }
            set { left = value; }
        }

        private int right;

        public int Right
        {
            get { return right; }
            set { right = value; }
        }

        private int parent;

        public int Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        
        public string getTestName() {
            return string.Format("{0}[{1},{2}]", name, left, right);
        }

        /*
        public List<string> getList(int parentId){
            List<string> results = new List<string>();
            results.Add("test1");
            results.Add("test2");
            return results;
        }*/

        public string getRootName() {
            return "Àà±ð";
        }
    }

    public class CategoryCustomer : Category {
    }
}
