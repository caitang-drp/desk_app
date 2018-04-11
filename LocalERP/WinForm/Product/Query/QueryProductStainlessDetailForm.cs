using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using LocalERP.DataAccess.DataDAO;
using LocalERP.DataAccess.Data;
using System.Collections;
using LocalERP.DataAccess.Utility;
using LocalERP.WinForm.Utility;

namespace LocalERP.WinForm
{
    public partial class QueryProductStainlessDetailForm : QueryProductDetailForm
    {
        /*public QueryProductStainlessDetailForm():base()
        {
        }*/

        protected override void initDataGridView()
        {
            
            DataGridViewTextBoxColumn serial = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn name = new DataGridViewTextBoxColumn();

            DataGridViewTextBoxColumn type = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn time = new DataGridViewTextBoxColumn();

            DataGridViewTextBoxColumn num = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn unit = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn price = new DataGridViewTextBoxColumn();

            DataGridViewTextBoxColumn customer = new DataGridViewTextBoxColumn();

            serial.HeaderText = "货号";
            serial.Name = "serial";
            serial.Width = 70;

            name.HeaderText = "货品名称";
            name.Name = "name";
            name.Width = 100;

            type.HeaderText = "类型";
            type.Name = "type";
            type.Width = 80;

            time.HeaderText = "时间";
            time.Name = "time";
            time.Width = 160;

            num.HeaderText = "数量";
            num.Name = "num";
            num.Width = 100;

            unit.HeaderText = "单位";
            unit.Name = "unit";
            unit.Width = 70;

            customer.HeaderText = "往来单位";
            customer.Name = "customer";
            customer.Width = 100;

            price.HeaderText = "单价/元";
            price.Name = "price";
            price.Width = 140;

            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] 
                { serial, name, type, time, num, unit, customer });
        }

        protected override void initList()
        {   
            DataTable dataTable = ProductStainlessCirculationRecordDao.getInstance().FindList(
                            this.dateTimePicker3.Value, this.dateTimePicker4.Value.AddDays(1), 0,
                            this.textBox_product.Text, -1, this.textBox_customer.Text, -1);
            this.dataGridView1.Rows.Clear();
            foreach (DataRow dr in dataTable.Rows)
            {
                int index = this.dataGridView1.Rows.Add();

                this.dataGridView1.Rows[index].Cells["serial"].Value = dr["serial"];
                this.dataGridView1.Rows[index].Cells["name"].Value = dr["ProductStainless.name"];

                int type = (int)(dr["type"]);
                this.dataGridView1.Rows[index].Cells["type"].Value = ProductCirculation.CirculationTypeConfs[type - 1].name;

                this.dataGridView1.Rows[index].Cells["time"].Value = dr["circulationTime"];

                int num = (int)dr["totalNum"];// *(int)dr["flowType"];
                if (type == 1 || type == 4 || type == 5 || type == 8)
                    ControlUtility.setCellWithColor(dataGridView1.Rows[index].Cells["num"], Color.Red, string.Format("+{0}", num));
                else if (type == 2 || type == 3 || type == 6 || type ==7)
                    ControlUtility.setCellWithColor(dataGridView1.Rows[index].Cells["num"], Color.Green, string.Format("-{0}", num));

                this.dataGridView1.Rows[index].Cells["unit"].Value = dr["unit"];

                this.dataGridView1.Rows[index].Cells["customer"].Value = dr["circulation.name"];  
            }
        }
    }
}