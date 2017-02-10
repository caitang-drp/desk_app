using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LocalERP.DataAccess.DataDAO;
using LocalERP.DataAccess.Data;
using LocalERP.UiDataProxy;
using LocalERP.DataAccess.Utility;
using LocalERP.WinForm.Utility;
using System.IO;

namespace LocalERP.WinForm
{
    public class ProductClothesCirculationForm : ProductCirculationForm
    {
        public ProductClothesCirculationForm(CirculationTypeConf conf):base(conf)
        {
        }
        
        public override void initDatagridview(DataGridView dgv)
        {
            base.initDatagridview(dgv);
            DataGridViewLookupColumn num = new DataGridViewLookupColumn();

            num.HeaderText = "ÊýÁ¿/¸ö";
            num.LookupForm = null;
            num.Name = "num";
            num.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            num.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            num.Width = 240;

            dgv.Columns.Insert(3, num);

            (dgv.Columns["num"] as DataGridViewLookupColumn).LookupForm = new ProductClothesInputNumForm(this);
        }

    }
}