using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;
using LocalERP.DataAccess.Utility;

namespace LocalERP.DataAccess
{
    class DbHelperAccess
    {
        /// <summary>
        /// ���ݴ���������sqlִ����ӡ��޸ĺ�ɾ������
        /// </summary>
        /// <param name="sql">����������sql</param>
        /// <returns></returns>
        public static int executeNonQuery(string sql)
        {
            int iResult = 0;  //Ӱ�������
            OleDbConnection conn = new OleDbConnection(ConfUtility.CONN_STRING);
            try
            {
                conn.Open();
                //string sql = string.Format("delete from userinfo where id={0}",id);
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                
                iResult = cmd.ExecuteNonQuery();   //ִ�и���
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

        public static int executeMax(string property, string table) {
            int IDvalue = 0;
            OleDbConnection conn = new OleDbConnection(ConfUtility.CONN_STRING);
            conn.Open();
            OleDbCommand cmdGetId = new OleDbCommand(string.Format("SELECT max({0}) from {1}", property, table), conn);
            IDvalue = (int)cmdGetId.ExecuteScalar();
            conn.Close();
            return IDvalue;

        }

        public static int executeQueryNum(string sql)
        {
            int IDvalue = 0;
            OleDbConnection conn = new OleDbConnection(ConfUtility.CONN_STRING);
            conn.Open();
            OleDbCommand cmdGetId = new OleDbCommand(sql, conn);
            IDvalue = (int)cmdGetId.ExecuteScalar();
            conn.Close();

            return IDvalue;
        }

        public static DataTable executeQuery(string sql)
        {
            OleDbConnection conn = new OleDbConnection(ConfUtility.CONN_STRING);
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
            OleDbConnection conn = new OleDbConnection(ConfUtility.CONN_STRING);
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
