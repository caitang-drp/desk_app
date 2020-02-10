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
                string commandText = string.Format("insert into Consume(code, consumeTime, cardID, status, type, num, operator, comment, leftNum) values('{0}', '{1}', {2}, {3}, {4}, {5}, '{6}', '{7}', {8})",
                    info.Code, info.ConsumeTime, info.CardID, info.Status, info.Type, info.Number, info.Oper, info.Comment, info.LeftNumber);
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
            string commandText = string.Format("delete from Consume where ID={0}", id);
            return DbHelperAccess.executeNonQuery(commandText);
        }

        public int Update(Consume info)
        {
            string commandText = string.Format("update Consume set code='{0}', consumeTime='{1}',cardID={2}, status={3}, type={4}, num={5}, operator='{6}', comment='{7}', leftNum={8} where ID={9}",
                info.Code, info.ConsumeTime, info.CardID, info.Status, info.Type, info.Number, info.Oper, info.Comment, info.LeftNumber, info.ID);

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

                consume.Card = CardDao.getInstance().FindByID(consume.CardID);

                consume.Number = (int)dr["Consume.num"];
                consume.LeftNumber = (int)dr["Consume.leftNum"];
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

        public List<Consume> FindList(int cardId, int status)
        {
            String commandText = string.Format("select * from Card, Customer, Consume where Consume.cardID = Card.ID and Card.customerID = Customer.ID");

            if (cardId > 0)
                commandText += string.Format(" and Consume.cardID={0}", cardId);

            if(status > 0)
                commandText += string.Format(" and Consume.status={0}", status);

            commandText += " order by Consume.ID desc";
            
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
