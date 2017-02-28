using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.IO;

namespace LocalERP.WinForm.Utility
{
    /// <summary>
    /// 日期选择DataGridViewColumn
    /// </summary>
    public class DataGridViewLookupColumn : DataGridViewColumn
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public DataGridViewLookupColumn()
            : base(new DataGridViewLookupCell())
        {
            
        }

        /// <summary>
        /// 获取或设置用于创建新单元格的模板。
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
    /// 日期选择DataGridViewTextBoxCell
    /// </summary>
    public class DataGridViewLookupCell : DataGridViewResizeTextBoxCell
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public DataGridViewLookupCell()
            : base()
        {
            //commented by stone: assigned value first, or calling method GetFormattedValue will except as value is null
            this.Value = this.DefaultNewRowValue;
        }

        /// <summary>
        /// 附加并初始化寄宿的编辑控件。
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
        /// 编辑时的类型
        /// </summary>
        public override Type EditType
        {
            get
            {
                return typeof(DataGridViewLookupEditingControl);
            }
        }

        /// <summary>
        /// 值类型
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
        /// 默认值
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
    /// DateTimePicker单元格控件
    /// </summary>
    public class DataGridViewLookupEditingControl : LookupText, IDataGridViewEditingControl
    {
        DataGridView dataGridView;
        private bool valueChanged = false;
        int rowIndex;

        //added by stone
        int columnIndex;

        /// <summary>
        /// 初始化
        /// </summary>
        public DataGridViewLookupEditingControl()
        {
            //this.Format = DateTimePickerFormat.Short;
            this.SelectButtonBackGround = global::LocalERP.Properties.Resources.folder_open_16px;

            //modified by stone:原来lookupForm是放在column里，但是同个column不同的controlEdit有可能不一样，
            //所以不同的controlEdit会对应到一个form，这样导致form的valueChanged会传递到不需要的controlEdit里
            //现在临时放在control里，如果有其他类型的选择窗口，再做扩展
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
        /// 获取或设置编辑器正在修改的单元格的格式化值。
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
        /// 检索单元格的格式化值。
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
        /// 更改控件的用户界面 (UI)，使之与指定单元格样式一致。
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
        /// 获取或设置该承载单元格的父行的索引。
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
        /// 确定指定的键是应由编辑控件处理的常规输入键，还是应由 System.Windows.Forms.DataGridView 处理的特殊键。
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
        /// 准备当前选中的单元格以进行编辑。
        /// </summary>
        /// <param name="selectAll"></param>
        public void PrepareEditingControlForEdit(bool selectAll)
        {
            // No preparation needs to be done.
            //this.columnIndex = this.dataGridView.CurrentCell.ColumnIndex;
            //File.AppendAllText("e:\\debug.txt", string.Format("prepare editing control for edit. dataGridView.Rows.Count={0}\r\n", dataGridView.Rows.Count));
        }

        /// <summary>
        /// 获取或设置一个值，该值指示每当值更改时，是否需要重新定位单元格的内容。
        /// </summary>
        public bool RepositionEditingControlOnValueChange
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// 获取或设置包含单元格的 System.Windows.Forms.DataGridView。
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
        /// 获取或设置一个值，该值指示编辑控件的值是否与承载单元格的值不同。
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
        /// 获取当鼠标指针位于 System.Windows.Forms.DataGridView.EditingPanel 上方但不位于编辑控件上方时所使用的光标。
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
