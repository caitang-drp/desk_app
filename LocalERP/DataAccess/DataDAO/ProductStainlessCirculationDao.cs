using LocalERP.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace LocalERP.DataAccess.DataDAO
{
    public class ProductStainlessCirculationDao : ProductCirculationDao
    {
        //singleton
        public static ProductStainlessCirculationDao dao;
        public static ProductStainlessCirculationDao getInstance()
        {
            if (dao == null)
            {
                dao = new ProductStainlessCirculationDao();
                dao.TableName = "ProductStainlessCirculation";
            }
            return dao;
        }

        public override ProductCirculationRecordDao getRecordDao()
        {
            return ProductStainlessCirculationRecordDao.getInstance();
        }

        public override ProductDao getProductDao()
        {
            return ProductStainlessDao.getInstance();
        }

        public override bool Insert(ProductCirculation info, List<ProductCirculationRecord> records, out int ProductCirculationID)
        {
            ProductCirculationID = 0;
            try
            {
                this.Insert(info, out ProductCirculationID);
                this.insertRecords(ProductCirculationID, records);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void insertRecords(int ID, List<ProductCirculationRecord> records)
        {
            try
            {
                foreach (ProductStainlessCirculationRecord record in records)
                {
                    record.CirculationID = ID;
                    ProductStainlessCirculationRecordDao.getInstance().Insert(record);
                }
            }
            catch (Exception ex){
                throw ex;
            }
        }

        public override bool updateRecords(int ID, List<ProductCirculationRecord> records) {
            try
            {
                ProductStainlessCirculationRecordDao.getInstance().DeleteByCirculationID(ID);
                this.insertRecords(ID, records);
                return true;
            }
            catch {
                return false;
            }
        }

        //stone����Ҫ�����᲻������

        // ֻ��Ҫһ���򵥵Ĳ�ѯ��䣬�������������٣��Լ����������
        public DataTable find_all_list()
        {
            return DbHelperAccess.executeQuery("select * from ProductStainlessCirculation");
        }

        // ��ȡ ��� ͨ�� �� ��
        public List<ProductCirculation> get_reviewed_bill()
        {
            DataTable dataTable = find_all_list();

            // 
            List<ProductCirculation> ls = new List<ProductCirculation>();
            foreach (DataRow dr in dataTable.Rows)
            {
                // ���۵�ת����û��ORM������
                ProductCirculation tmp = FindByID(Convert.ToInt32(dr["ID"]));
                if (tmp == null)
                {
                    continue;
                }

                // ���״̬
                if (tmp.Status != 4)
                {
                    continue;
                }

                ls.Add(tmp);
            }

            return ls;
        }

    }
}
