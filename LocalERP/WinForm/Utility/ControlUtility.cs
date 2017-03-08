using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using LocalERP.WinForm;
using LocalERP.WinForm.Utility;
using System.Data;
using System.Drawing;

namespace LocalERP.WinForm.Utility
{
    public static class ControlUtility
    {
        public static void initColumns(DataGridView dgv, string[] columnNames, string [] columnTexts, int [] columnLengths)
        {
            DataGridViewColumn[] columns = new DataGridViewColumn[columnNames.Length];
            for (int i = 0; i < columnNames.Length; i++)
            {
                columns[i] = new DataGridViewTextBoxColumn();
                columns[i].HeaderText = columnTexts[i];
                columns[i].Name = columnNames[i];
                columns[i].ReadOnly = true;
                columns[i].Width = columnLengths[i];
            }
            dgv.Columns.Clear();
            dgv.Columns.AddRange(columns);
        }

        public static void setCellWithColor(DataGridViewCell cell, Color color, string text) {
            cell.Style.ForeColor = color;
            cell.Style.SelectionForeColor = color;
            cell.Value = text;
        }
    }
}
