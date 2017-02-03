using LocalERP.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace LocalERP.DataAccess.DataDAO
{
    class ElementOrderDao
    {
        //singleton
        public static ElementOrderDao dao;
        public static ElementOrderDao getInstance()
        {
            if (dao == null)
                dao = new ElementOrderDao();
            return dao;
        }

        public int Insert(ElementOrder info, List<ElementOrderRecord> records)
        {
            try
            {
                //string commandText = @"insert into Users(userName,userPassword,userLevel,userPhone,userAddress) values (
					                                     //?userName,?userPassword,?userLevel,?userPhone,?userAddress)";

                string commandText = string.Format("insert into ElementOrder(serial, orderTime, comment, status) values('{0}', '{1}', '{2}', '{3}')", info.Serial, info.OrderTime, info.Comment, info.Status);

                DbHelperAccess.executeNonQuery(commandText);

                int elementOrderID = DbHelperAccess.executeLastID("ID", "ElementOrder");

                ElementOrderRecordDao dao = new ElementOrderRecordDao();
                foreach (ElementOrderRecord record in records){
                    record.OrderID = elementOrderID;
                    dao.Insert(record);
                }
                return elementOrderID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int UpdateStatus(int orderID, int status) {
            string commandText = string.Format("update ElementOrder set status = {0} where ID={1}",
                    status, orderID);

            return DbHelperAccess.executeNonQuery(commandText);
        }

        public void Update(ElementOrder info, List<ElementOrderRecord> records)
        {
            string commandText = string.Format("update ElementOrder set serial='{0}', orderTime='{1}', comment='{2}' where ID={3}",
                info.Serial, info.OrderTime, info.Comment, info.ID);

            DbHelperAccess.executeNonQuery(commandText);

            ElementOrderRecordDao.getInstance().DeleteByOrderID(info.ID);

            foreach (ElementOrderRecord record in records)
            {
                record.OrderID = info.ID;
                ElementOrderRecordDao.getInstance().Insert(record);
            }
        }

        public int Delete(int id)
        {
            string commandText = string.Format("delete from ElementOrder where ID={0}", id);
            return DbHelperAccess.executeNonQuery(commandText);
        }

        public DataTable FindList(DateTime startTime, DateTime endTime, int status)
        {
            StringBuilder commandText = new StringBuilder(string.Format("select * from ElementOrder where orderTime between #{0}# and #{1}# ", startTime.ToString("yyyy-MM-dd"), endTime.ToString("yyyy-MM-dd")));
            if (status > 0 && status <= 4)
                commandText.Append(string.Format(" and status = {0}", status));
            commandText.Append(" order by ID desc");
            return DbHelperAccess.executeQuery(commandText.ToString());
        }


        public ElementOrder FindByID(int ID)
        {
            string commandText = string.Format("select * from ElementOrder where ID={0}", ID);
            DataRow dr = DbHelperAccess.executeQueryGetOneRow(commandText);
            ElementOrder order = null;
            if (dr != null) {
                order = new ElementOrder();
                order.ID = (int)dr["ID"];
                order.Serial = dr["serial"] as string;
                order.OrderTime = (DateTime)dr["orderTime"];
                order.Comment = dr["comment"] as string;
                order.Status = (int)dr["status"];
                return order;
            }
            return null;
            
        }

    }
}
