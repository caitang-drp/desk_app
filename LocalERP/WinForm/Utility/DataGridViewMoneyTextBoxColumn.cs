using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace LocalERP.WinForm.Utility
{
    public class DataGridViewMoneyTextBoxColumn : DataGridViewTextBoxColumn
    {
        public DataGridViewMoneyTextBoxColumn()
        {
            this.CellTemplate = new DataGridViewMoneyCell();
        }
        public override DataGridViewCell CellTemplate
        {
            get
            {
                return base.CellTemplate;
            }
            set
            {
                DataGridViewMoneyCell cell = value as DataGridViewMoneyCell;
                if (value != null && cell == null)
                {
                    throw new InvalidCastException("Value provided for CellTemplate must be of type TEditNumDataGridViewCell or derive from it.");
                }
                base.CellTemplate = value;
            }
        }
    }

    public class DataGridViewMoneyCell : DataGridViewTextBoxCell
    {
        public DataGridViewMoneyCell()
        {

        }
        int decimalLength;

        private static Type defaultEditType = typeof(MoneyTextBoxDataGridViewEditingControl);
        private static Type defaultValueType = typeof(System.Decimal);

        public int DecimalLength
        {
            get { return decimalLength; }
            set { decimalLength = value; }
        }

        public override Type EditType
        {
            get { return defaultEditType; }
        }

        public override Type ValueType
        {
            get
            {
                Type valueType = base.ValueType;
                if (valueType != null)
                {
                    return valueType;
                }
                return defaultValueType;
            }
        }
    }
    public class MoneyTextBoxDataGridViewEditingControl : TextBox, IDataGridViewEditingControl
    {
        private DataGridView dataGridView;  // grid owning this editing control
        private bool valueChanged;  // editing control's value has changed or not
        private int rowIndex;  //  row index in which the editing control resides

        #region IDataGridViewEditingControl ≥…‘±

        public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
        {
            this.Font = dataGridViewCellStyle.Font;
            if (dataGridViewCellStyle.BackColor.A < 255)
            {
            }
            else
            {
                this.BackColor = dataGridViewCellStyle.BackColor;
            }
            this.ForeColor = dataGridViewCellStyle.ForeColor;
        }

        public DataGridView EditingControlDataGridView
        {
            get
            {
                return this.dataGridView;
            }
            set
            {
                this.dataGridView = value;
            }
        }

        public object EditingControlFormattedValue
        {
            get
            {
                return GetEditingControlFormattedValue(DataGridViewDataErrorContexts.Formatting);
            }
            set
            {
                this.Text = (string)value;
            }
        }

        public int EditingControlRowIndex
        {
            get
            {
                return this.rowIndex;
            }
            set
            {
                this.rowIndex = value;
            }
        }

        public bool EditingControlValueChanged
        {
            get { return this.valueChanged; }
            set { this.valueChanged = value; }
        }

        public bool EditingControlWantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
        {
            switch (keyData & Keys.KeyCode)
            {
                case Keys.Right:
                case Keys.Left:
                case Keys.Down:
                case Keys.Up:
                case Keys.Home:
                case Keys.End:
                case Keys.Delete:
                    return true;
            }
            return !dataGridViewWantsInputKey;
        }

        public Cursor EditingPanelCursor
        {
            get { return Cursors.Default; }
        }

        public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
        {
            return this.Text;
        }

        public void PrepareEditingControlForEdit(bool selectAll)
        {
            if (selectAll)
            {
                this.SelectAll();
            }
            else
            {
                this.SelectionStart = this.Text.Length;
            }
        }

        public bool RepositionEditingControlOnValueChange
        {
            get { return false; }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            this.EditingControlDataGridView.NotifyCurrentCellDirty(true);
            base.OnTextChanged(e);
        }
        #endregion
    }
}
