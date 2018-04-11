using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace LocalERP.WinForm.Utility
{
    public class DataGridViewComboEditBoxColumn : DataGridViewComboBoxColumn
    {
        public DataGridViewComboEditBoxColumn(){
            DataGridViewComboEditBoxCell obj = new DataGridViewComboEditBoxCell();
            this.CellTemplate = obj;
        }
    }

    public class DataGridViewComboEditBoxCell : DataGridViewComboBoxCell
    {
        public override void InitializeEditingControl(int rowIndex, object initialFormattedValue,
            DataGridViewCellStyle dataGridViewCellStyle){
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);

            ComboBox comboBox = (ComboBox)base.DataGridView.EditingControl;
            if (comboBox != null){
                comboBox.DropDownStyle = ComboBoxStyle.Simple;
                comboBox.AutoCompleteMode = AutoCompleteMode.Suggest;
                
                //comboBox.Validating += new CancelEventHandler(comboBox_Validating);
            }
        }
        
        protected override object GetFormattedValue(object value, int rowIndex,
            ref DataGridViewCellStyle cellStyle, TypeConverter valueTypeConverter,
            TypeConverter formattedValueTypeConverter, DataGridViewDataErrorContexts context) {
            /*if (value != null && value.ToString().Trim() != string.Empty)
            {
                if (Items.IndexOf(value) == -1)// 如果下拉框中不存在填入的值，则添加到下拉框中
                {
                    Items.Add(value);
                    // 添加到该列所有单元所绑定的下拉列表中
                    DataGridViewComboBoxColumn col = (DataGridViewComboBoxColumn)OwningColumn;
                    col.Items.Add(value);
                }
            }*/

            return base.GetFormattedValue(value, rowIndex, ref cellStyle, valueTypeConverter, formattedValueTypeConverter, context);
        }
        private void comboBox_Validating(object sender, CancelEventArgs e)
        {
            DataGridViewComboBoxEditingControl cbo = (DataGridViewComboBoxEditingControl)sender;
            if (cbo.Text.Trim() == string.Empty)
                return;

            DataGridView grid = cbo.EditingControlDataGridView;
            object value = cbo.Text;

            if (cbo.Items.IndexOf(value) == -1)
            {
                DataGridViewComboBoxColumn cboCol = (DataGridViewComboBoxColumn)grid.Columns[grid.CurrentCell.ColumnIndex];
                // 添加到当前下拉框中以及模版中，避免出现重复项
                cbo.Items.Add(value);
                //cboCol.Items.Add(value);
                grid.CurrentCell.Value = value;
            }
        }
    }
}
