using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using WeifenLuo.WinFormsUI.Docking;
using LocalERP.DataAccess.Data;
using LocalERP.UiDataProxy;
using LocalERP.DataAccess.DataDAO;
using LocalERP.WinForm.Utility;
using System.Collections;

namespace LocalERP.WinForm
{
    public partial class StatisticProductStainlessForm : StatisticProductForm
    {
        protected override void statisticProduct()
        {
            itemsTable = ProductStainlessDao.getInstance().FindList(null, null);
            itemsTable.Columns.Add("staticNum");
            itemsTable.Columns.Add("sum");

            foreach (DataRow dr in itemsTable.Rows)
            {
                int totalNum = 0;
                double sum = 0;
                int productID = (int)dr["ProductStainless.ID"];

                this.getSum(this.dateTimePicker3.Value, this.dateTimePicker4.Value, (int)this.comboBox2.SelectedValue, productID, 0, out totalNum, out sum);

                dr["staticNum"] = totalNum;
                dr["sum"] = sum;
            }
        }

        protected override void initProductItems()
        {
            this.dataGridView1.Rows.Clear();
            foreach (DataRow dr in itemsTable.Rows)
            {
                this.dataGridView1.Rows.Add(dr["serial"], dr["ProductStainless.name"], dr["staticNum"], dr["sum"]);
            }
        }

        protected override void getSum(DateTime start, DateTime end, int type, int productID, int customerID, out int totalNum, out double sum) {
            DataTable dt = ProductStainlessCirculationRecordDao.getInstance().FindList(start, end.AddDays(1).AddSeconds(-1), type, null, productID, null, customerID);
            totalNum = 0;
            sum = 0; 
            foreach (DataRow row in dt.Rows)
            {
                int tempInt = (int)row["totalNum"];
                double tempDouble = 0;
                double.TryParse(row["price"].ToString(), out tempDouble);
                int flowType = (int)row["flowType"];

                totalNum += tempInt*flowType;
                sum += tempDouble * tempInt*flowType;
                
            }

            sum = Math.Abs(sum);
            totalNum = Math.Abs(totalNum);
        }
    }
}