using LocalERP.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace LocalERP.DataAccess.DataDAO
{
    class ElementDao
    {
        //singleton
        public static ElementDao dao;
        public static ElementDao getInstance(){
            if (dao == null)
                dao = new ElementDao();
            return dao;
        }

        public int Insert(Element info)
        {
            try
            {
                string commandText = string.Format("insert into Element(name, price, comment) values('{0}', {1}, '{2}')", info.Name, info.Price, info.Note);
                return DbHelperAccess.executeNonQuery(commandText);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Delete(int id)
        {
            string commandText = string.Format("delete from Element where ID={0}", id);
            return DbHelperAccess.executeNonQuery(commandText);
        }

        public DataTable FindList()
        {
            string commandText = "select * from Element";
            
            return DbHelperAccess.executeQuery(commandText);
        }

        public List<Element> FindListByProductID(int productID)
        {
            string commandText = "select * from Element, ProductElement where Element.ID = ProductElement.elementID and ProductElement.productID = " + productID;

            DataTable dt = DbHelperAccess.executeQuery(commandText);
            List<Element> elements = new List<Element>();
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Element element = new Element();
                    element.ID = (int)dr["ID"];
                    element.Name = dr["name"] as string;
                    element.Note = dr["comment"] as string;
                    element.Num = (int)dr["num"];
                    elements.Add(element);
                }
            }

            return elements;
        }

        public int Update(Element info)
        {
            string commandText = string.Format("update Element set name='{0}', comment='{1}', price={2} where ID={3}",
                info.Name, info.Note, info.Price, info.ID);

            return DbHelperAccess.executeNonQuery(commandText);
        }

        public int UpdateNum(int id, int num)
        {
            string commandText = string.Format("update Element set num={0} where ID={1}",
                num, id);

            return DbHelperAccess.executeNonQuery(commandText);
        }

        public Element FindByID(int ID)
        {
            string commandText = string.Format("select * from Element where ID={0}", ID);
            DataRow dr = DbHelperAccess.executeQueryGetOneRow(commandText);
            Element element = new Element();
            if (dr != null)
            {
                element.ID = (int)dr["ID"];
                element.Name = dr["name"] as string;
                element.Note = dr["comment"] as string;
                element.Num = (int)dr["num"];
                string priceStr = dr["price"].ToString();
                double price;
                double.TryParse(priceStr, out price);
                element.Price = price;
                return element;
            }
            return null;
        }

        public double FindPriceByID(int ID)
        {
            string commandText = string.Format("select price from Element where ID={0}", ID);
            DataRow dr = DbHelperAccess.executeQueryGetOneRow(commandText);
            double result = 0;
            if (dr != null)
            {
                string str = dr[0].ToString();
                double.TryParse(str, out result);
            }
            return result;
        }

    }
}
