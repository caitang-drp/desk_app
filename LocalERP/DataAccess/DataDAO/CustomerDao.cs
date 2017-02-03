using LocalERP.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace LocalERP.DataAccess.DataDAO
{
    class CustomerDao
    {
        //singleton
        public static CustomerDao dao;
        public static CustomerDao getInstance(){
            if (dao == null)
                dao = new CustomerDao();
            return dao;
        }

        public int Insert(Customer info)
        {
            try
            {
                string commandText = string.Format("insert into Customer(name, comment, tel, phone, address, parent) values('{0}', '{1}', '{2}', '{3}', '{4}', {5})", 
                    info.Name,info.Comment, info.Tel, info.Phone, info.Address, info.Parent);
                return DbHelperAccess.executeNonQuery(commandText);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Delete(int id)
        {
            string commandText = string.Format("delete from Customer where ID={0}", id);
            return DbHelperAccess.executeNonQuery(commandText);
        }

        public DataTable FindList(Category parent, string name)
        {
            StringBuilder commandText = new StringBuilder("select * from Customer, CustomerCategory where Customer.parent=CustomerCategory.ID");
            if(parent != null)
                commandText.AppendFormat(" and CustomerCategory.lft>={0} and CustomerCategory.rgt<={1}", parent.Left, parent.Right);
            
            if (!string.IsNullOrEmpty(name))
                commandText.AppendFormat(" and Customer.name like '%{0}%'", name);
            return DbHelperAccess.executeQuery(commandText.ToString());
        }

        public DataTable FindListForStatistic(Category parent)
        {
            string commandText = "select ID, name from Customer";
            if (parent != null)
                commandText = string.Format("select Customer.ID, Customer.name from Customer, CustomerCategory where Customer.parent=CustomerCategory.ID and CustomerCategory.lft>={0} and CustomerCategory.rgt<={1}", parent.Left, parent.Right);
            return DbHelperAccess.executeQuery(commandText);
        }

        public int Update(Customer info)
        {
            string commandText = string.Format("update Customer set name='{0}', comment='{1}', tel='{2}', phone='{3}', address='{4}' where ID={5}",
                info.Name, info.Comment, info.Tel, info.Phone, info.Address, info.ID);

            return DbHelperAccess.executeNonQuery(commandText);
        }

        public Customer FindByID(int ID)
        {
            string commandText = string.Format("select * from Customer where ID={0}", ID);
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

    }
}
