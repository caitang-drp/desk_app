using LocalERP.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace LocalERP.DataAccess.DataDAO
{
    class ProductFlowDao
    {
        public static ProductFlowDao dao;
        public static ProductFlowDao getInstance()
        {
            if (dao == null)
                dao = new ProductFlowDao();
            return dao;
        }


        public int Insert(ProductFlow info)
        {
            try
            {
                //string commandText = @"insert into Users(userName,userPassword,userLevel,userPhone,userAddress) values (
					                                     //?userName,?userPassword,?userLevel,?userPhone,?userAddress)";

                string commandText = string.Format("insert into ProductFlow(productID, productName, flowType, num, accumulativeNum, comment, flowTime, type) values('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}')",
                    info.ProductID, info.ProductName, info.FlowType, info.Num, info.AccumulativeNum, info.Comment, info.FlowTime, info.Type);

                return DbHelperAccess.executeNonQuery(commandText);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable FindList(int productID, DateTime startTime, DateTime endTime, int flowType)
        {
            StringBuilder commandText = new StringBuilder(string.Format("SELECT * from ProductFlow where flowTime between #{0}# and #{1}#", startTime.ToString("yyyy-MM-dd"), endTime.ToString("yyyy-MM-dd")));
            if (productID > 0)
                commandText.Append(string.Format(" and productID={0}", productID));

            if (flowType == 1 || flowType == -1)
                commandText.Append(string.Format(" and flowType={0}", flowType));

            commandText.Append(" order by ID desc");
            return DbHelperAccess.executeQuery(commandText.ToString());
        }
    }
}
