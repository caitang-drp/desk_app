using LocalERP.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace LocalERP.DataAccess.DataDAO
{
    //1-9企业信息,10-11软件信息, 12打印封面日期格式, 13备份路径
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

        public void UpdateCompanyInfo(string company, string address, string contract, string phone, string mobile, string bank, string other, string pic) {
            string commandText = string.Format("update Conf set conf='{0}' where ID=3", company);
            DbHelperAccess.executeNonQuery(commandText);

            commandText = string.Format("update Conf set conf='{0}' where ID=4", address);
            DbHelperAccess.executeNonQuery(commandText);

            commandText = string.Format("update Conf set conf='{0}' where ID=5", contract);
            DbHelperAccess.executeNonQuery(commandText);

            commandText = string.Format("update Conf set conf='{0}' where ID=6", phone);
            DbHelperAccess.executeNonQuery(commandText);

            commandText = string.Format("update Conf set conf='{0}' where ID=7", mobile);
            DbHelperAccess.executeNonQuery(commandText);

            commandText = string.Format("update Conf set conf='{0}' where ID=8", bank);
            DbHelperAccess.executeNonQuery(commandText);

            commandText = string.Format("update Conf set conf='{0}' where ID=9", other);
            DbHelperAccess.executeNonQuery(commandText);

            commandText = string.Format("update Conf set conf='{0}' where ID=14", pic);
            DbHelperAccess.executeNonQuery(commandText);
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

        public DataTable GetAll() {
            string commandText = string.Format("select * from Conf order by ID");
            return DbHelperAccess.executeQuery(commandText);
        }
    }
}
