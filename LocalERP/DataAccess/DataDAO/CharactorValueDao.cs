using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using LocalERP.DataAccess.Data;

namespace LocalERP.DataAccess.DataDAO
{
    public class CharactorValueDao
    {
        public static CharactorValueDao dao;
        public static CharactorValueDao getInstance()
        {
            if (dao == null)
                dao = new CharactorValueDao();
            return dao;
        }

        public int Insert(CharactorValue info)
        {
            try
            {
                string commandText = string.Format("insert into CharactorValue(name, charactorID) values('{0}', {1})", info.Name, info.CharactorId);
                DbHelperAccess.executeNonQuery(commandText);
                int id = DbHelperAccess.executeLastID("ID", "CharactorValue");
                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public bool Delete(int id)
        {
            try
            {
                string commandText = string.Format("delete from CharactorValue where ID={0}", id);
                DbHelperAccess.executeNonQuery(commandText);
                return true;
            }
            catch {
                return false;
            }
        }

        public int Update(CharactorValue info)
        {
            string commandText = string.Format("update CharactorValue set name='{0}' where ID={1}",
                info.Name, info.Id);

            return DbHelperAccess.executeNonQuery(commandText);
        }

        public CharactorValue findById(int charactorValueID) {
            string commandText = "select * from CharactorValue where ID = " + charactorValueID.ToString();

            DataRow dr = DbHelperAccess.executeQueryGetOneRow(commandText);
            CharactorValue cv = new CharactorValue();
            if (dr != null)
            {
                cv.Id = (int)dr["ID"];
                cv.CharactorId = (int)dr["charactorID"];
                cv.Name = dr["name"] as string;
            }

            return cv;
        }

        public List<CharactorValue> FindList(int charactorID)
        {
            string commandText = "select * from CharactorValue where charactorID = "+charactorID.ToString();

            DataTable dt = DbHelperAccess.executeQuery(commandText);
            List<CharactorValue> elements = new List<CharactorValue>();
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    CharactorValue element = new CharactorValue();
                    element.Id = (int)dr["ID"];
                    element.Name = dr["name"] as string;
                    element.CharactorId = (int)dr["charactorID"];
                    elements.Add(element);
                }
            }

            return elements;
        }
    }
}
