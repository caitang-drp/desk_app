using LocalERP.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace LocalERP.DataAccess.DataDAO
{
    class ElementOrderRecordDao
    {
        public static ElementOrderRecordDao dao;
        public static ElementOrderRecordDao getInstance()
        {
            if (dao == null)
                dao = new ElementOrderRecordDao();
            return dao;
        }

        public int Insert(ElementOrderRecord info)
        {
            try
            {
                //string commandText = @"insert into Users(userName,userPassword,userLevel,userPhone,userAddress) values (
					                                     //?userName,?userPassword,?userLevel,?userPhone,?userAddress)";
                string commandText = string.Format("insert into ElementOrderRecord(elementID, num, price, orderID) values({0}, {1}, {2}, {3})",
                    info.ElementID, info.Number, info.Price, info.OrderID);

                return DbHelperAccess.executeNonQuery(commandText);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable FindList(int orderID)
        {
            string commandText = string.Format("select * from ElementOrderRecord where orderID = {0}", orderID);
            return DbHelperAccess.executeQuery(commandText);
        }

        public int Update(ElementOrderRecord info)
        {
            string commandText = string.Format("update ElementOrderRecord set elementID={0}, num={1}, price={2}, arrivalNum={3}  where ID={4}", 
                info.ElementID, info.Number, info.Price, info.ArrivalNum, info.ID);

            return DbHelperAccess.executeNonQuery(commandText);
        }

        public int UpdateArrivalNum(int ID, int arrivalNum)
        {
            string commandText = string.Format("update ElementOrderRecord set arrivalNum={0}  where ID={1}",
                arrivalNum, ID);

            return DbHelperAccess.executeNonQuery(commandText);
        }

        public int DeleteByOrderID(int orderID) {
            string commandText = string.Format("delete from ElementOrderRecord where orderID={0}", orderID);

            return DbHelperAccess.executeNonQuery(commandText);
        }


//        #region ===É¾³ý¼ÇÂ¼===

//        public int Delete(int id)
//        {
//            string commandText = "delete from Users where userID=?userID";

//            MySqlParameter[] commandParameters = new MySqlParameter[1];
//            commandParameters[0] = new MySqlParameter("?userID", MySqlDbType.Int32);
//            commandParameters[0].Value = id;

//            try
//            {
//                return DbHelperMySQL.ExecuteSql(commandText, commandParameters);
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        #endregion

//        public UserBE GetUser(string userName, string userPassword)
//        {
//            string commandText = "select * from Users where userName=?userName and userPassword=?userPassword";
//            MySqlParameter[] commandParameters = new MySqlParameter[2];
//            commandParameters[0] = new MySqlParameter("?userName", MySqlDbType.VarChar);
//            commandParameters[0].Value = userName;
//            commandParameters[1] = new MySqlParameter("?userPassword", MySqlDbType.VarChar);
//            commandParameters[1].Value = userPassword;
                
//            MySqlConnection outConnection = null;
//            MySqlDataReader dr = null;      
//            UserBE userBE = null;

//            try
//            {
//                dr = DbHelperMySQL.ExecuteReader(commandText, out outConnection, commandParameters);
//                while (dr.Read())
//                {
//                    userBE = new UserBE();
//                    userBE.userID = Int32.Parse(dr["userID"].ToString());
//                    userBE.userName = dr["userName"].ToString();
//                    userBE.userPassword = dr["userPassword"].ToString();
//                    userBE.userLevel = dr["userLevel"].ToString();
//                    userBE.userPhone = dr["userPhone"].ToString();
//                    userBE.userAddress = dr["userAddress"].ToString();

//                    string scopeString = dr["userScope"].ToString().Trim();

//                    userBE.minLat = int.Parse(scopeString.Substring(0, 2));
//                    userBE.minLon = int.Parse(scopeString.Substring(3, 3));
//                    userBE.maxLat = int.Parse(scopeString.Substring(7, 2));
//                    userBE.maxLon = int.Parse(scopeString.Substring(10, 3));

//                    userBE.minLat = userBE.minLat / 5 * 5;
//                    userBE.minLon = userBE.minLon / 5 * 5;
//                    userBE.maxLat = userBE.maxLat / 5 * 5;
//                    userBE.maxLon = userBE.maxLon / 5 * 5;
//                }

//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//            finally
//            {
//                if (dr != null)
//                    dr.Close();
//                if (dr.IsClosed)
//                    System.Threading.Thread.Sleep(0);
//                if (outConnection != null)
//                    outConnection.Close();
//            }
//            return userBE;
//        }

    }
}
