using LocalERP.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace LocalERP.DataAccess.DataDAO
{
    class ConfDao
    {
        //singleton
        public static ConfDao dao;
        public static ConfDao getInstance(){
            if (dao == null)
                dao = new ConfDao();
            return dao;
        }

        public int Update(int id, string value)
        {
            string commandText = string.Format("update Conf set conf='{0}' where ID={1}", value, id);

            return DbHelperAccess.executeNonQuery(commandText);
        }

        public string Get(int id)
        {
            string commandText = string.Format("select * from Conf where ID={0}", id);
            DataRow dr = DbHelperAccess.executeQueryGetOneRow(commandText);
            //DbHelperAccess.executeQuery(commandText);
            if (dr != null)
                return dr["conf"] as string;
            return null;
        }

    }
}
