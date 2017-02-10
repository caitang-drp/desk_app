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
    public class ProductStainlessCirculationForm : ProductCirculationForm
    {
        public ProductStainlessCirculationForm(CirculationTypeConf conf):base(conf)
        {
        }
        
        public override void initDatagridview(DataGridView dgv)
        {
            DataGridViewTextBoxColumn ID = new DataGridViewTextBoxColumn();
            DataGridViewCheckBoxColumn check = new DataGridViewCheckBoxColumn();
            DataGridViewLookupColumn product = new DataGridViewLookupColumn();

            DataGridViewTextBoxColumn quantityPerPiece = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn pieces = new DataGridViewTextBoxColumn();
            
            DataGridViewTextBoxColumn num = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn unit = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn price = new DataGridViewTextBoxColumn();

            DataGridViewTextBoxColumn totalPrice = new DataGridViewTextBoxColumn();

            ID.HeaderText = "ID";
            ID.Name = "ID";
            ID.Visible = false;
            ID.Width = 60;

            check.HeaderText = "选择";
            check.Name = "check";
            check.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            check.Width = 46;

            product.HeaderText = "产品";
            product.LookupForm = null;
            product.Name = "product";
            product.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            product.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            product.Width = 140;

            quantityPerPiece.HeaderText = "每件数量";
            quantityPerPiece.Name = "quantityPerPiece";
            quantityPerPiece.Width = 90;

            pieces.HeaderText = "件数";
            pieces.Name = "pieces";
            pieces.Width = 60;

            num.HeaderText = "数量";
            num.Name = "num";
            num.Width = 100;

            unit.HeaderText = "单位";
            unit.Name = "unit";
            unit.Width = 60;

            price.HeaderText = this.Text.Substring(0, 2) + "单价/元";
            price.Name = "price";
            price.Width = 140;

            totalPrice.HeaderText = "总价/元";
            totalPrice.Name = "totalPrice";

            dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { ID, check, product, quantityPerPiece, pieces, num, unit, price, totalPrice });

            //特别注意：如果这个地方的ProductCIForm不是新建，用的是以前的窗口
            //那就可能有多个CirculationForm的DataGridViewLookupColumn指向同一个ProductCIForm
            //那么当以前的CirculationForm没有销毁的情况下，ProductCIForm就会触发以前的valueChanged事件，从而出现异常
            (dgv.Columns["product"] as DataGridViewLookupColumn).LookupForm = FormSingletonFactory.getInstance().getProductCIForm_select();
        }

    }
}