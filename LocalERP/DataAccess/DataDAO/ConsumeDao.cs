using LocalERP.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using LocalERP.DataAccess.Utility;

namespace LocalERP.DataAccess.DataDAO
{
    class ConsumeDao
    {
        //singleton
        public static ConsumeDao dao;
        public static ConsumeDao getInstance()
        {
            if (dao == null)
                dao = new ConsumeDao();
            return dao;
        }

        public bool Insert(Consume info, out int id)
        {
            try
            {
                string commandText = string.Format("insert into Consume(code, consumeTime, cardID, status, type, num, operator, comment) values('{0}', '{1}', {2}, {3}, {4}, {5}, '{6}', '{7}')",
                    info.Code, info.ConsumeTime, info.CardID, info.Status, info.Type, info.Number, info.Oper, info.Comment);
                DbHelperAccess.executeNonQuery(commandText);
                id = DbHelperAccess.executeMax("ID", "Consume");
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Delete(int id)
        {
            string commandText = string.Format("delete from Card where ID={0}", id);
            return DbHelperAccess.executeNonQuery(commandText);
        }

        /*
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
        }*/

        public int Update(Card info)
        {
            string commandText = string.Format("update Card set code='{0}', cardTime='{1}', status={2}, customerID={3}, type={4}, total={5}, num={6}, operator='{7}', comment='{8}' where ID={9}",
                info.Code, info.CardTime, info.Status, info.CustomerID, info.Type, info.Total, info.Number, info.Oper, info.Comment, info.ID);

            return DbHelperAccess.executeNonQuery(commandText);
        }


        public Consume getBeanFromDataRow(DataRow dr) {
            Consume consume = new Consume();
            if (dr != null)
            {
                consume.ID = (int)dr["Consume.ID"];
                consume.Code = dr["Consume.code"] as string;
                consume.ConsumeTime = (DateTime)dr["consumeTime"];
                consume.CardID = (int)dr["cardID"];

                consume.Number = (int)dr["Consume.num"];
                consume.Comment = dr["Consume.comment"] as string;
                consume.Oper = dr["Consume.operator"] as string;
                consume.Status = (int)dr["Consume.status"];
                consume.Type = (int)dr["Consume.type"];

                return consume;
            }
            return null;
        }

        public Consume FindByID(int ID)
        {
            string commandText = string.Format("select * from Card, Customer, Consume where Consume.ID={0} and Consume.cardID = Card.ID and Card.customerID = Customer.ID", ID);
            DataRow dr = DbHelperAccess.executeQueryGetOneRow(commandText);
            return getBeanFromDataRow(dr);
        }

        public List<Consume> FindList(int parentId)
        {
            String commandText = string.Format("select * from Card, Customer, Consume where Consume.cardID = Card.ID and Card.customerID = Customer.ID order by Consume.ID");
            DataTable dt = DbHelperAccess.executeQuery(commandText);

            List<Consume> consumes = new List<Consume>();

            foreach (DataRow dr in dt.Rows)
            {
                consumes.Add(this.getBeanFromDataRow(dr));
            }

            return consumes;
        }

    }
}
