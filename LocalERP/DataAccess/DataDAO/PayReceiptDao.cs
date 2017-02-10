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

        public int Insert(PayReceipt info)
        {
            try
            {
                string commandText = string.Format("insert into PayReceipt(serial, bill_time, comment, customer_id, bill_type, handle_people, amount) values('{0}', '{1}', '{2}', {3}, {4}, '{5}', {6})", 
                    info.serial, info.bill_time, info.comment, (int)info.customer_id, (int)info.bill_type, info.handle_people, info.amount);
                return DbHelperAccess.executeNonQuery(commandText);
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

        /*
        public DataTable FindListForStatistic(Category parent)
        {
            string commandText = "select ID, name from Customer";
            if (parent != null)
                commandText = string.Format("select Customer.ID, Customer.name from PayReceipt, CustomerCategory where Customer.parent=CustomerCategory.ID and CustomerCategory.lft>={0} and CustomerCategory.rgt<={1}", parent.Left, parent.Right);
            return DbHelperAccess.executeQuery(commandText);
        }

        public int Update(Customer info)
        {
            string commandText = string.Format("update PayReceipt set name='{0}', comment='{1}', tel='{2}', phone='{3}', address='{4}' where ID={5}",
                info.Name, info.Comment, info.Tel, info.Phone, info.Address, info.ID);

            return DbHelperAccess.executeNonQuery(commandText);
        }

        public Customer FindByID(int ID)
        {
            string commandText = string.Format("select * from PayReceipt where ID={0}", ID);
            DataRow dr = DbHelperAccess.executeQueryGetOneRow(commandText);
            Customer customer = new Customer();
            if (dr != null)
            {
                customer.ID = (int)dr["ID"];
                customer.Parent = (int)dr["parent"];

                customer.Name = dr["name"] as string;
                customer.Comment = dr["comment"] as string;
                customer.Tel = dr["tel"] as string;
                customer.Phone = dr["phone"] as string;
                customer.Address = dr["address"] as string;
                
                return customer;
            }
            return null;
        }
        */

    }
}
