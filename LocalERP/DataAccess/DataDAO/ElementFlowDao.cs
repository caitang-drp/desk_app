using LocalERP.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace LocalERP.DataAccess.DataDAO
{
    class ElementFlowDao
    {
        public static ElementFlowDao dao;
        public static ElementFlowDao getInstance()
        {
            if (dao == null)
                dao = new ElementFlowDao();
            return dao;
        }


        public int Insert(ElementFlow info)
        {
            try
            {
                string commandText = string.Format("insert into ElementFlow(elementID, elementName, flowType, num, accumulativeNum, comment, flowTime,type) values('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}')",
                    info.ElementID, info.ElementName, info.FlowType, info.Num, info.AccumulativeNum, info.Comment, info.FlowTime, info.Type);

                return DbHelperAccess.executeNonQuery(commandText);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable FindList(int elementID, DateTime startTime, DateTime endTime, int flowType)
        {
            StringBuilder commandText = new StringBuilder(string.Format("SELECT * from ElementFlow where flowTime between #{0}# and #{1}#", startTime.ToString("yyyy-MM-dd"), endTime.ToString("yyyy-MM-dd")));
            if (elementID > 0) 
                commandText.Append(string.Format(" and elementID={0}", elementID));

            if (flowType == 1 || flowType == -1)
                commandText.Append(string.Format(" and flowType={0}", flowType));

            commandText.Append(" order by ID desc");
            return DbHelperAccess.executeQuery(commandText.ToString());
        }

    }
}
