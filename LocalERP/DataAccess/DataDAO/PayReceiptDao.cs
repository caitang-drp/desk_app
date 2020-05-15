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
                string commandText = string.Format("insert into PayReceipt(serial, bill_time, comment, customer_id, bill_type, handle_people, previousArrears, amount, status, cashDirection, arrearDirection, thisPayed, hide) values('{0}', '{1}', '{2}', {3}, {4}, '{5}', {6}, {7}, {8}, {9}, {10}, {11}, {12})", 
                    info.serial, info.bill_time, info.comment,  info.customer_id <= 0 ? "null" : info.customer_id.ToString(), (int)info.bill_type, info.handle_people, info.previousArrears, info.amount, info.status, info.cashDirection, info.arrearDirection, info.thisPayed, info.hide);
                DbHelperAccess.executeNonQuery(commandText);
                id = DbHelperAccess.executeMax("ID", "PayReceipt");
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int UpdateStatus(int ID, int status)
        {
            string commandText = string.Format("update PayReceipt set status = {0} where ID={1}",status, ID);

            return DbHelperAccess.executeNonQuery(commandText);
        }

        //没有加入cashDirection和arrearDirection, status, hide
        public void Update(PayReceipt info)
        {
            string commandText = string.Format("update PayReceipt set serial='{0}', bill_time='{1}', comment='{2}', customer_id={3}, bill_type={4}, handle_people='{5}', previousArrears={6}, amount={7}, thisPayed={8}, status={9} where ID={10}",
                info.serial, info.bill_time, info.comment,  info.customer_id <= 0 ? "null" : info.customer_id.ToString(), (int)info.bill_type, info.handle_people, info.previousArrears, info.amount, info.thisPayed, info.status, info.id);

            DbHelperAccess.executeNonQuery(commandText);
        }

        private PayReceipt formatPayReceipt(DataRow dr) {
            PayReceipt payReceipt = new PayReceipt();
            if (dr != null)
            {
                payReceipt.id = (int)dr["ID"];
                payReceipt.serial = dr["serial"] as string;
                payReceipt.bill_time = (DateTime)dr["bill_time"];
                payReceipt.comment = dr["comment"] as string;
                payReceipt.status = (int)dr["status"];
                payReceipt.bill_type = (PayReceipt.BillType)dr["bill_type"];

                int customerID = 0;
                if (int.TryParse(dr["customer_id"].ToString(), out customerID))
                    payReceipt.customer_id = customerID;

                payReceipt.handle_people = dr["handle_people"] as string;

                double previousArrears, amount, thisPayed;
                if (double.TryParse(dr["previousArrears"].ToString(), out previousArrears))
                    payReceipt.previousArrears = previousArrears;
                if (double.TryParse(dr["amount"].ToString(), out amount))
                    payReceipt.amount = amount;
                if (double.TryParse(dr["thisPayed"].ToString(), out thisPayed))
                    payReceipt.thisPayed = thisPayed;

                payReceipt.cashDirection = (int)dr["cashDirection"];
                payReceipt.arrearDirection = (int)dr["arrearDirection"];

                try
                {
                    payReceipt.customerName = dr["name"] as string;
                }
                catch { }
                payReceipt.hide = (int)dr["hide"];

                return payReceipt;
            }
            return null;
        }

        public PayReceipt FindByID(int ID)
        {
            string commandText = string.Format("select PayReceipt.*, Customer.name from PayReceipt left join Customer on PayReceipt.customer_id = Customer.ID where PayReceipt.ID={0}", ID);
            DataRow dr = DbHelperAccess.executeQueryGetOneRow(commandText);
            //要测试字段是否正确
            return formatPayReceipt(dr);
        }

        public int Delete(int id)
        {
            string commandText = string.Format("delete from PayReceipt where ID={0}", id);
            return DbHelperAccess.executeNonQuery(commandText);
        }

        public int DeleteAll()
        {
            string commandText = string.Format("delete from PayReceipt");
            return DbHelperAccess.executeNonQuery(commandText);
        }

        public PayReceipt FindLastestByCustomerID(int customerID, bool payNotNull)
        {
            ////模仿FindByID，所以left join customer，其实可以不要
            string commandText = string.Format("select * from PayReceipt where bill_time = (SELECT max(bill_time) from PayReceipt where customer_id={0} and status=4)", customerID);
            if(payNotNull)
                commandText = string.Format("select * from PayReceipt where bill_time = (SELECT max(bill_time) from PayReceipt where customer_id={0} and status=4 and thisPayed <>0)", customerID);
            DataRow dr = DbHelperAccess.executeQueryGetOneRow(commandText);
            return formatPayReceipt(dr);
        }

       
        public PayReceipt FindLastestAccReceiptZero(int customerID)
        {
            string commandText = string.Format("select top 1 PayReceipt.*, Customer.name from PayReceipt left join Customer on PayReceipt.customer_id = Customer.ID where Customer.ID={0} and  (arrearDirection * previousArrears - cashDirection * (amount - thisPayed)) < 0 and previousArrears >=0 order by PayReceipt.ID desc", customerID);
            DataRow dr = DbHelperAccess.executeQueryGetOneRow(commandText);
            //要测试字段是否正确
            return formatPayReceipt(dr);
        }

        //DateTime? 相当于Nullable<DateTime>，调用时必须增加.value
        public List<PayReceipt> FindPayReceiptList(DateTime? startTime, DateTime? endTime, int status, string name, int parent, int hide)
        {
            //要注意，这个语句会筛选掉没有Customer信息的
            StringBuilder commandText = new StringBuilder("select PayReceipt.*, Customer.name from PayReceipt left join Customer on PayReceipt.customer_id = Customer.ID where 1=1 ");
            
            if (startTime != null && endTime != null)
                commandText.Append(string.Format(" and PayReceipt.bill_time between #{0}# and #{1}# ", startTime.Value.ToString("yyyy-MM-dd"), endTime.Value.ToString("yyyy-MM-dd")));
            if (status > 0)
                commandText.Append(string.Format(" and status = {0}", status));

            if (parent > 0)
                commandText.Append(string.Format(" and Customer.ID={0}", parent));
            
            //hide = 0，只显示不隐藏的，hide=1，显示所有
            commandText.Append(string.Format(" and hide <= {0}", hide));
            if (!string.IsNullOrEmpty(name))
                commandText.AppendFormat(" and Customer.name like '%{0}%'", name);

            commandText.Append(" order by PayReceipt.bill_time desc");

            DataTable dt = DbHelperAccess.executeQuery(commandText.ToString());
            List<PayReceipt> list = new List<PayReceipt>();
            foreach (DataRow pr in dt.Rows) { 
                list.Add(this.formatPayReceipt(pr));
            }
            return list;
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
