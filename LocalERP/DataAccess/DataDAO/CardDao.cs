using LocalERP.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using LocalERP.DataAccess.Utility;

namespace LocalERP.DataAccess.DataDAO
{
    class CardDao
    {
        //singleton
        public static CardDao dao;
        public static CardDao getInstance()
        {
            if (dao == null)
                dao = new CardDao();
            return dao;
        }

        public bool Insert(Card info, out int id)
        {
            try
            {
                string commandText = string.Format("insert into Card(code, cardTime, status, customerID, type, total, num, operator, comment) values('{0}', '{1}', {2}, {3}, {4}, {5}, {6}, '{7}', '{8}')",
                    info.Code, info.CardTime, info.Status, info.CustomerID, info.Type, info.Total, info.Number, info.Oper, info.Comment);
                DbHelperAccess.executeNonQuery(commandText);
                id = DbHelperAccess.executeMax("ID", "Card");
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Delete(int id)
        {
            string commandText = string.Format("delete from Customer where ID={0}", id);
            return DbHelperAccess.executeNonQuery(commandText);
        }

        public DataTable FindList(DateTime startTime, DateTime endTime, int status, string customerName)
        {
            StringBuilder commandText = null;
            commandText = new StringBuilder(string.Format("select Card.*, Customer.name from Card, Customer where Customer.ID = Card.customerID and cardTime between #{0}# and #{1}# ", startTime.ToString("yyyy-MM-dd"), endTime.ToString("yyyy-MM-dd")));
            
            if (!string.IsNullOrEmpty(customerName))
                commandText.Append(string.Format(" and Customer.name like '%{0}%'", customerName));

            commandText.Append(" order by Card.cardTime desc");
            return DbHelperAccess.executeQuery(commandText.ToString());
        }

        public DataTable FindListForStatistic(Category parent)
        {
            string commandText = "select ID, name from Customer";
            if (parent != null)
                commandText = string.Format("select Customer.ID, Customer.name from Customer, CustomerCategory where Customer.parent=CustomerCategory.ID and CustomerCategory.lft>={0} and CustomerCategory.rgt<={1}", parent.Left, parent.Right);
            return DbHelperAccess.executeQuery(commandText);
        }

        public int Update(Customer info)
        {
            string commandText = string.Format("update Customer set name='{0}', comment='{1}', tel='{2}', phone='{3}', address='{4}', arrear={5}, receipt={6} where ID={7}",
                info.Name, info.Comment, info.Tel, info.Phone, info.Address, info.arrear, info.receipt, info.ID);

            return DbHelperAccess.executeNonQuery(commandText);
        }

        // 更新用户的收款，也就是我们欠供应商的钱
        public void update_receipt(int customer_id, double now)
        {
        }


        public Card getBeanFromDataRow(DataRow dr) {
            Card card = new Card();
            if (dr != null)
            {
                card.ID = (int)dr["Card.ID"];
                card.Code = dr["code"] as string;
                card.CardTime = (DateTime)dr["cardTime"];
                card.CustomerID = (int)dr["customerID"];
                double total = 0;
                ValidateUtility.getDouble(dr, "total", out total);
                card.Total = total;
                card.Number = (int)dr["num"];
                card.Comment = dr["Card.comment"] as string;
                card.Oper = dr["operator"] as string;
                card.Status = (int)dr["status"];
                card.Type = (int)dr["type"];
                card.CustomerName = dr["name"] as string;

                return card;
            }
            return null;
        }

        public Card FindByID(int ID)
        {
            string commandText = string.Format("select * from Card, Customer where Card.ID={0} and Card.customerID = Customer.ID", ID);
            DataRow dr = DbHelperAccess.executeQueryGetOneRow(commandText);
            return getBeanFromDataRow(dr);
        }
        /*
        public List<Customer> FindByParentId(int parentId)
        {
            String commandText = string.Format("select * from Customer where parent = {0} order by ID", parentId);
            DataTable dt = DbHelperAccess.executeQuery(commandText);

            List<Customer> categorys = new List<Customer>();

            foreach (DataRow dr in dt.Rows)
            {
                categorys.Add(this.getBeanFromDataRow(dr));
            }

            return categorys;
        }*/

        public void ClearAllArrear() {
            string commandText = string.Format("update Customer set arrear=0");
            DbHelperAccess.executeNonQuery(commandText);
        }

    }
}
