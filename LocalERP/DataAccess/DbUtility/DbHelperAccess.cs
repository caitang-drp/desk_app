using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;

namespace LocalERP.DataAccess
{
    class DbHelperAccess
    {
        //public static readonly string CONN_STRING = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source="+Application.StartupPath+"\\ERP.mdb;Persist Security Info=True";   //注意将数据库与release或debug中的你的程序名.exe放在一个目录下，这样方便移植
        public static readonly string CONN_STRING = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\ERP.mdb;Persist Security Info=True";   //注意将数据库与release或debug中的你的程序名.exe放在一个目录下，这样方便移植


        /// <summary>
        /// 根据传来的请求sql执行添加、修改和删除操作
        /// </summary>
        /// <param name="sql">传来的请求sql</param>
        /// <returns></returns>
        public static int executeNonQuery(string sql)
        {
            int iResult = 0;  //影响的行数
            OleDbConnection conn = new OleDbConnection(CONN_STRING);
            try
            {
                conn.Open();
                //string sql = string.Format("delete from userinfo where id={0}",id);
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                
                iResult = cmd.ExecuteNonQuery();   //执行更新
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return iResult;
        }

        public static int executeLastID(string ID, string table) {
            int IDvalue = 0;
            OleDbConnection conn = new OleDbConnection(CONN_STRING);
            conn.Open();
            OleDbCommand cmdGetId = new OleDbCommand(string.Format("SELECT max({0}) from {1}", ID, table), conn);
            IDvalue = (int)cmdGetId.ExecuteScalar();
            conn.Close();
            return IDvalue;

        }

        public static int executeQueryNum(string sql)
        {
            int IDvalue = 0;
            OleDbConnection conn = new OleDbConnection(CONN_STRING);
            conn.Open();
            OleDbCommand cmdGetId = new OleDbCommand(sql, conn);
            IDvalue = (int)cmdGetId.ExecuteScalar();
            conn.Close();

            return IDvalue;
        }

        public static DataTable executeQuery(string sql)
        {
            OleDbConnection conn = new OleDbConnection(CONN_STRING);
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter();
            dataAdapter.SelectCommand = new OleDbCommand(sql, conn);
            OleDbCommandBuilder custCB = new OleDbCommandBuilder(dataAdapter);

            conn.Open();

            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            //dataAdapter.Update(dataTable);

            conn.Close();

            return dataTable;
        }

        public static DataRow executeQueryGetOneRow(string sql)
        {
            OleDbConnection conn = new OleDbConnection(CONN_STRING);
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter();
            dataAdapter.SelectCommand = new OleDbCommand(sql, conn);
            OleDbCommandBuilder custCB = new OleDbCommandBuilder(dataAdapter);

            conn.Open();

            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            //dataAdapter.Update(dataTable);

            conn.Close();

            if (dataTable != null && dataTable.Rows.Count >= 1)
                return dataTable.Rows[0];
            return null;
        } 

    }
}
