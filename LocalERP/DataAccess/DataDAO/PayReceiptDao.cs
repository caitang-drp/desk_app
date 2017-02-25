using LocalERP.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace LocalERP.DataAccess.DataDAO
{
    class PayReceiptDao
    {
        //singleton
        public static PayReceiptDao dao;
        public static PayReceiptDao getInstance(){
            if (dao == null)
                dao = new PayReceiptDao();
            return dao;
        }

        public bool Insert(PayReceipt info, out int id)
        {
            try
            {
                string commandText = string.Format("insert into PayReceipt(serial, bill_time, comment, customer_id, bill_type, handle_people, previousArrears, amount, status) values('{0}', '{1}', '{2}', {3}, {4}, '{5}', {6}, {7}, {8})", 
                    info.serial, info.bill_time, info.comment, (int)info.customer_id, (int)info.bill_type, info.handle_people, info.previousArrears, info.amount, info.status);
                DbHelperAccess.executeNonQuery(commandText);
                id = DbHelperAccess.executeLastID("ID", "PayReceipt");
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Delete(int id)
        {
            string commandText = string.Format("delete from PayReceipt where ID={0}", id);
            return DbHelperAccess.executeNonQuery(commandText);
        }

        public DataTable FindList()
        {
            StringBuilder commandText = new StringBuilder("select * from PayReceipt");
            return DbHelperAccess.executeQuery(commandText.ToString());
        }

        public int getMaxCode(string code)
        {
            string commandText = string.Format("select max(serial) from PayReceipt where serial like '{0}-{1}-%'", code, DateTime.Now.ToString("yyyyMMdd"));
            DataRow dr = DbHelperAccess.executeQueryGetOneRow(commandText);
            string result = dr[0] as string;
            if (string.IsNullOrEmpty(result))
                return 0;
            else
            {
                int max = 0;
                if (int.TryParse(result.Substring(result.LastIndexOf('-')), out max))
                    return Math.Abs(max);
                else
                    return 0;
            }
        }

    }
}
