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

            check.HeaderText = "ѡ��";
            check.Name = "check";
            check.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            check.Width = 46;

            product.HeaderText = "��Ʒ";
            product.LookupForm = null;
            product.Name = "product";
            product.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            product.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            product.Width = 140;

            quantityPerPiece.HeaderText = "ÿ������";
            quantityPerPiece.Name = "quantityPerPiece";
            quantityPerPiece.Width = 90;

            pieces.HeaderText = "����";
            pieces.Name = "pieces";
            pieces.Width = 60;

            num.HeaderText = "����";
            num.Name = "num";
            num.Width = 100;

            unit.HeaderText = "��λ";
            unit.Name = "unit";
            unit.Width = 60;

            price.HeaderText = this.Text.Substring(0, 2) + "����/Ԫ";
            price.Name = "price";
            price.Width = 140;

            totalPrice.HeaderText = "�ܼ�/Ԫ";
            totalPrice.Name = "totalPrice";

            dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { ID, check, product, quantityPerPiece, pieces, num, unit, price, totalPrice });

            //�ر�ע�⣺�������ط���ProductCIForm�����½����õ�����ǰ�Ĵ���
            //�ǾͿ����ж��CirculationForm��DataGridViewLookupColumnָ��ͬһ��ProductCIForm
            //��ô����ǰ��CirculationFormû�����ٵ�����£�ProductCIForm�ͻᴥ����ǰ��valueChanged�¼����Ӷ������쳣
            (dgv.Columns["product"] as DataGridViewLookupColumn).LookupForm = FormSingletonFactory.getInstance().getProductCIForm_select();
        }

    }
}