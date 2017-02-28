using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.IO;

namespace LocalERP.WinForm.Utility
{
    /// <summary>
    /// ����ѡ��DataGridViewColumn
    /// </summary>
    public class DataGridViewLookupColumn : DataGridViewColumn
    {
        /// <summary>
        /// ��ʼ��
        /// </summary>
        public DataGridViewLookupColumn()
            : base(new DataGridViewLookupCell())
        {
            
        }

        /// <summary>
        /// ��ȡ���������ڴ����µ�Ԫ���ģ�塣
        /// </summary>
        public override DataGridViewCell CellTemplate
        {
            get
            {
                return base.CellTemplate;
            }
            set
            {
                // Ensure that the cell used for the template is a LookupCell.
                if (value != null &&
                    !value.GetType().IsAssignableFrom(typeof(DataGridViewLookupCell)))
                {
                    throw new InvalidCastException("Must be a LookupCell");
                }
                base.CellTemplate = value;
            }
        }
    }
    /// <summary>
    /// ����ѡ��DataGridViewTextBoxCell
    /// </summary>
    public class DataGridViewLookupCell : DataGridViewResizeTextBoxCell
    {
        /// <summary>
        /// ��ʼ��
        /// </summary>
        public DataGridViewLookupCell()
            : base()
        {
            //commented by stone: assigned value first, or calling method GetFormattedValue will except as value is null
            this.Value = this.DefaultNewRowValue;
        }

        /// <summary>
        /// ���Ӳ���ʼ�����޵ı༭�ؼ���
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="initialFormattedValue"></param>
        /// <param name="dataGridViewCellStyle"></param>
        public override void InitializeEditingControl(int rowIndex, object
            initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            // Set the value of the editing control to the current cell value.
            base.InitializeEditingControl(rowIndex, initialFormattedValue,
                dataGridViewCellStyle);
            DataGridViewLookupEditingControl ctl =
                DataGridView.EditingControl as DataGridViewLookupEditingControl;
            
            //added by stone
            //ctl.LookupForm = lookupForm;//(this.DataGridView.Columns[this.ColumnIndex] as DataGridViewLookupColumn).LookupForm;
            ctl.Text_Lookup = (this.Value as LookupArg).Text;

            //File.AppendAllText("e:\\debug.txt", string.Format("InitializeEditingControl: ctl-{0}; form-{1}\r\n", ctl.GetHashCode(), cif.GetHashCode()));
        }

        /// <summary>
        /// �༭ʱ������
        /// </summary>
        public override Type EditType
        {
            get
            {
                return typeof(DataGridViewLookupEditingControl);
            }
        }

        /// <summary>
        /// ֵ����
        /// </summary>
        public override Type ValueType
        {
            get
            {
                return typeof(LookupArg);
            }
        }

        public override Type FormattedValueType
        {
            get
            {
                return typeof(LookupArg);
            }
        }

        //added by stone: get the value no matter is in editing mode
        public object EditedValue {
            get {
                if (this.IsInEditMode)
                {
                    DataGridViewLookupEditingControl ctl = DataGridView.EditingControl as DataGridViewLookupEditingControl;
                    return ctl.EditingControlFormattedValue;
                }
                else
                    return Value;
            }
        }

        /// <summary>
        /// Ĭ��ֵ
        /// </summary>
        public override object DefaultNewRowValue{
            get{
                return new LookupArg("", "");
            }
        }

        
        protected override object GetFormattedValue(object value,
                                                    int rowIndex,
                                                    ref DataGridViewCellStyle cellStyle,
                                                    TypeConverter valueTypeConverter,
                                                    TypeConverter formattedValueTypeConverter,
                                                    DataGridViewDataErrorContexts context)
        {
            object formattedValue = base.GetFormattedValue(value, rowIndex, ref cellStyle, valueTypeConverter, formattedValueTypeConverter, context);
            
            //File.AppendAllText("e:\\debug.txt", string.Format("Cell.GetFormattedValue: {0}, row: {1}\r\n", formattedValue is ValueText_Lookup?(formattedValue as ValueText_Lookup).Text: "", rowIndex));
            string result = (formattedValue as LookupArg).Text;
            return result;
        }
    }

    /// <summary>
    /// DateTimePicker��Ԫ��ؼ�
    /// </summary>
    public class DataGridViewLookupEditingControl : LookupText, IDataGridViewEditingControl
    {
        DataGridView dataGridView;
        private bool valueChanged = false;
        int rowIndex;

        //added by stone
        int columnIndex;

        /// <summary>
        /// ��ʼ��
        /// </summary>
        public DataGridViewLookupEditingControl()
        {
            //this.Format = DateTimePickerFormat.Short;
            this.SelectButtonBackGround = global::LocalERP.Properties.Resources.folder_open_16px;

            //modified by stone:ԭ��lookupForm�Ƿ���column�����ͬ��column��ͬ��controlEdit�п��ܲ�һ����
            //���Բ�ͬ��controlEdit���Ӧ��һ��form����������form��valueChanged�ᴫ�ݵ�����Ҫ��controlEdit��
            //������ʱ����control�������������͵�ѡ�񴰿ڣ�������չ
            this.LookupForm = FormSingletonFactory.getInstance().getProductCIForm_select();
        }

        
        protected override void lookupForm_lookupCallback(LookupArg arg)
        {
            base.lookupForm_lookupCallback(arg);

            if (EditingControlDataGridView.CurrentCell != null)
                (EditingControlDataGridView.CurrentCell as DataGridViewLookupCell).resetSize(this.Text_Lookup);

            valueChanged = true;
            this.EditingControlDataGridView.NotifyCurrentCellDirty(true);
             
        }

        private int getMaxLength(string text) {
            string[] strs = text.Split('\n');
            int num=1;
            foreach (string str in strs)
                if (str.Length > num)
                    num = str.Length;
            return num;
        }

        /// <summary>
        /// ��ȡ�����ñ༭�������޸ĵĵ�Ԫ��ĸ�ʽ��ֵ��
        /// </summary>
        public object EditingControlFormattedValue
        {
            get
            {
                return this.LookupArg;
            }
            set
            {
                if (value is LookupArg)
                {
                    this.LookupArg = (LookupArg)value;
                }
            }
        }

        /// <summary>
        /// ������Ԫ��ĸ�ʽ��ֵ��
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public object GetEditingControlFormattedValue(
            DataGridViewDataErrorContexts context)
        {
            //File.AppendAllText("e:\\debug.txt", string.Format("EditingControl.GetEditingControlFormattedValue: {0}\r\n", this.EditingControlFormattedValue));
            return this.EditingControlFormattedValue;
        }

        /// <summary>
        /// ���Ŀؼ����û����� (UI)��ʹ֮��ָ����Ԫ����ʽһ�¡�
        /// </summary>
        /// <param name="dataGridViewCellStyle"></param>
        public void ApplyCellStyleToEditingControl(
            DataGridViewCellStyle dataGridViewCellStyle)
        {
            this.Font = dataGridViewCellStyle.Font;
            //this.CalendarForeColor = dataGridViewCellStyle.ForeColor;
            //this.CalendarMonthBackground = dataGridViewCellStyle.BackColor;
        }

        /// <summary>
        /// ��ȡ�����øó��ص�Ԫ��ĸ��е�������
        /// </summary>
        public int EditingControlRowIndex
        {
            get{return rowIndex;}
            set{rowIndex = value;}
        }
        //added by stone
        public int ColumnIndex
        {
            get { return columnIndex; }
            set { columnIndex = value; }
        }

        /// <summary>
        /// ȷ��ָ���ļ���Ӧ�ɱ༭�ؼ�����ĳ��������������Ӧ�� System.Windows.Forms.DataGridView ������������
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataGridViewWantsInputKey"></param>
        /// <returns></returns>
        public bool EditingControlWantsInputKey(
            Keys key, bool dataGridViewWantsInputKey)
        {
            // Let the DateTimePicker handle the keys listed.
            switch (key & Keys.KeyCode)
            {
                case Keys.Left:
                case Keys.Up:
                case Keys.Down:
                case Keys.Right:
                case Keys.Home:
                case Keys.End:
                case Keys.PageDown:
                case Keys.PageUp:
                    return true;
                default:
                    return !dataGridViewWantsInputKey;
            }
        }

        /// <summary>
        /// ׼����ǰѡ�еĵ�Ԫ���Խ��б༭��
        /// </summary>
        /// <param name="selectAll"></param>
        public void PrepareEditingControlForEdit(bool selectAll)
        {
            // No preparation needs to be done.
            //this.columnIndex = this.dataGridView.CurrentCell.ColumnIndex;
            //File.AppendAllText("e:\\debug.txt", string.Format("prepare editing control for edit. dataGridView.Rows.Count={0}\r\n", dataGridView.Rows.Count));
        }

        /// <summary>
        /// ��ȡ������һ��ֵ����ֵָʾÿ��ֵ����ʱ���Ƿ���Ҫ���¶�λ��Ԫ������ݡ�
        /// </summary>
        public bool RepositionEditingControlOnValueChange
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// ��ȡ�����ð�����Ԫ��� System.Windows.Forms.DataGridView��
        /// </summary>
        public DataGridView EditingControlDataGridView
        {
            get
            {
                //File.AppendAllText("e:\\debug.txt", string.Format("get dataGridView. dataGridView.Rows.Count={0}\r\n", dataGridView.Rows.Count));
                //File.AppendAllText("e:\\debug.txt", string.Format("get dataGridView. dataGridView hash code={0}, dataGridView name={1}\r\n", dataGridView.GetHashCode(), dataGridView.Name));
                return dataGridView;
            }
            set
            {
                dataGridView = value;
                //File.AppendAllText("e:\\debug.txt", string.Format("set dataGridView. dataGridView.Rows.Count={0}\r\n", dataGridView.Rows.Count));
                //File.AppendAllText("e:\\debug.txt", string.Format("set dataGridView. dataGridView hash code={0}, dataGridView name={1}\r\n", dataGridView.GetHashCode(), dataGridView.Name));
            }
        }

        /// <summary>
        /// ��ȡ������һ��ֵ����ֵָʾ�༭�ؼ���ֵ�Ƿ�����ص�Ԫ���ֵ��ͬ��
        /// </summary>
        public bool EditingControlValueChanged
        {
            get
            {
                return valueChanged;
            }
            set
            {
                valueChanged = value;
            }
        }

        /// <summary>
        /// ��ȡ�����ָ��λ�� System.Windows.Forms.DataGridView.EditingPanel �Ϸ�����λ�ڱ༭�ؼ��Ϸ�ʱ��ʹ�õĹ�ꡣ
        /// </summary>
        public Cursor EditingPanelCursor
        {
            get
            {
                return base.Cursor;
            }
        }
    }
}
