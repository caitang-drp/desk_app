using System;
using System.Collections.Generic;
using System.Text;
using WeifenLuo.WinFormsUI.Docking;
using System.Windows.Forms;

namespace LocalERP.WinForm
{
    public class MyDataGridView : DataGridView
    {
        public MyDataGridView() : base() {
            this.CellPainting += new DataGridViewCellPaintingEventHandler(MyDataGridView_CellPainting);
            this.CellFormatting += new DataGridViewCellFormattingEventHandler(MyDataGridView_CellFormatting);
        }

        void MyDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            e.CellStyle.SelectionForeColor = e.CellStyle.ForeColor;
        }

        void MyDataGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            e.Handled = true;
            e.PaintBackground(e.CellBounds, true);
            e.PaintContent(e.CellBounds);
        }

        public List<int> getSelectIDs(string idColumnName, string checkColumnName)
        {
            List<int> listID = new List<int>();
            foreach (DataGridViewRow row in this.Rows)
            {
                DataGridViewCheckBoxCell checkCell = row.Cells[checkColumnName] as DataGridViewCheckBoxCell;
                //commented by stone: this is not very reasonable
                //commented by stone: 2018-3-13 add 'cell.Value!=null'
                if ((bool)checkCell.EditedFormattedValue == true && row.Cells[idColumnName].Value != null)
                    //if (cell.Value != null && Convert.ToInt32(cell.Value) == 1)
                    listID.Add((int)row.Cells[idColumnName].Value);
            }
            return listID;
        }

        public List<DataGridViewRow> getCheckRows(string checkColumnName)
        {
            List<DataGridViewRow> rows = new List<DataGridViewRow>();
            foreach (DataGridViewRow row in this.Rows)
            {
                DataGridViewCheckBoxCell cell = row.Cells[checkColumnName] as DataGridViewCheckBoxCell;
                //commented by stone: this is not very reasonable
                if ((bool)cell.EditedFormattedValue == true)
                    //if (cell.Value != null && Convert.ToInt32(cell.Value) == 1)
                    rows.Add(row);
            }
            return rows;
        }
    }
}
