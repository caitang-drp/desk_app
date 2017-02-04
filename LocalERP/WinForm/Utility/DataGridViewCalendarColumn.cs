using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace LocalERP.WinForm.Utility
{
    /// <summary>
    /// ����ѡ��DataGridViewColumn
    /// </summary>
    public class DataGridViewCalendarColumn : DataGridViewColumn
    {
        /// <summary>
        /// ��ʼ��
        /// </summary>
        public DataGridViewCalendarColumn()
            : base(new DataGridViewCalendarCell())
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
                // Ensure that the cell used for the template is a CalendarCell.
                if (value != null &&
                    !value.GetType().IsAssignableFrom(typeof(DataGridViewCalendarCell)))
                {
                    throw new InvalidCastException("Must be a CalendarCell");
                }
                base.CellTemplate = value;
            }
        }
    }
    /// <summary>
    /// ����ѡ��DataGridViewTextBoxCell
    /// </summary>
    public class DataGridViewCalendarCell : DataGridViewTextBoxCell
    {
        /// <summary>
        /// ��ʼ��
        /// </summary>
        public DataGridViewCalendarCell()
            : base()
        {
            // Use the short date format.
            this.Style.Format = "d";
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
            DataGridViewCalendarEditingControl ctl =
                DataGridView.EditingControl as DataGridViewCalendarEditingControl;
            //ctl.Value = (DateTime)this.Value;
        }

        /// <summary>
        /// �༭ʱ������
        /// </summary>
        public override Type EditType
        {
            get
            {
                // Return the type of the editing contol that CalendarCell uses.
                return typeof(DataGridViewCalendarEditingControl);
            }
        }

        /// <summary>
        /// ֵ����
        /// </summary>
        public override Type ValueType
        {
            get
            {
                // Return the type of the value that CalendarCell contains.
                return typeof(DateTime);
            }
        }

        /// <summary>
        /// Ĭ��ֵ
        /// </summary>
        public override object DefaultNewRowValue
        {
            get
            {
                // Use the current date and time as the default value.
                return DateTime.Now;
            }
        }
    }

    /// <summary>
    /// DateTimePicker��Ԫ��ؼ�
    /// </summary>
    public class DataGridViewCalendarEditingControl : DateTimePicker, IDataGridViewEditingControl
    {
        DataGridView dataGridView;
        private bool valueChanged = false;
        int rowIndex;

        /// <summary>
        /// ��ʼ��
        /// </summary>
        public DataGridViewCalendarEditingControl()
        {
            this.Format = DateTimePickerFormat.Short;
        }


        /// <summary>
        /// ��ȡ�����ñ༭�������޸ĵĵ�Ԫ��ĸ�ʽ��ֵ��
        /// </summary>
        public object EditingControlFormattedValue
        {
            get
            {
                return this.Value.ToShortDateString();
            }
            set
            {
                if (value is String)
                {
                    this.Value = DateTime.Parse((String)value);
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
            return EditingControlFormattedValue;
        }

        /// <summary>
        /// ���Ŀؼ����û����� (UI)��ʹ֮��ָ����Ԫ����ʽһ�¡�
        /// </summary>
        /// <param name="dataGridViewCellStyle"></param>
        public void ApplyCellStyleToEditingControl(
            DataGridViewCellStyle dataGridViewCellStyle)
        {
            this.Font = dataGridViewCellStyle.Font;
            this.CalendarForeColor = dataGridViewCellStyle.ForeColor;
            this.CalendarMonthBackground = dataGridViewCellStyle.BackColor;
        }

        /// <summary>
        /// ��ȡ�����øó��ص�Ԫ��ĸ��е�������
        /// </summary>
        public int EditingControlRowIndex
        {
            get
            {
                return rowIndex;
            }
            set
            {
                rowIndex = value;
            }
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
                return dataGridView;
            }
            set
            {
                dataGridView = value;
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

        /// <summary>
        /// ��д���ݱ仯����
        /// </summary>
        /// <param name="eventargs"></param>
        protected override void OnValueChanged(EventArgs eventargs)
        {
            // Notify the DataGridView that the contents of the cell
            // have changed.
            valueChanged = true;
            this.EditingControlDataGridView.NotifyCurrentCellDirty(true);
            base.OnValueChanged(eventargs);
        }
    }
}
