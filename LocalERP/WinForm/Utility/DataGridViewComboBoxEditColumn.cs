using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace LocalERP.WinForm.Utility
{
    /// <summary>
    /// ����ѡ��DataGridViewColumn
    /// </summary>
    public class DataGridViewComboBoxEditColumn : DataGridViewColumn
    {
        /// <summary>
        /// ��ʼ��
        /// </summary>
        public DataGridViewComboBoxEditColumn()
            : base(new DataGridViewComboBoxEditCell())
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
                    !value.GetType().IsAssignableFrom(typeof(DataGridViewComboBoxEditCell)))
                {
                    throw new InvalidCastException("Must be a DataGridViewComboBoxEditCell");
                }
                base.CellTemplate = value;
            }
        }
    }
    /// <summary>
    /// ����ѡ��DataGridViewTextBoxCell
    /// </summary>
    public class DataGridViewComboBoxEditCell : DataGridViewTextBoxCell
    {
        /// <summary>
        /// ��ʼ��
        /// </summary>
        public DataGridViewComboBoxEditCell()
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
            DataGridViewComboBoxEditEditingControl ctl =
                DataGridView.EditingControl as DataGridViewComboBoxEditEditingControl;
            //DataGridViewTextBoxCellû��Text������ValueΪobject���͵ģ�����������һ��ValueType��Ҫ������ʲô���͵�(String)
            ctl.EditingControlFormattedValue = this.Value;
        }

        /// <summary>
        /// �༭ʱ������
        /// </summary>
        public override Type EditType
        {
            get
            {
                // Return the type of the editing contol that CalendarCell uses.
                return typeof(DataGridViewComboBoxEditEditingControl);
            }
        }

        /// <summary>
        /// ֵ����
        /// </summary>
        public override Type ValueType
        {
            get
            {
                return typeof(object);
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
                return "";
            }
        }
    }

    /// <summary>
    /// DateTimePicker��Ԫ��ؼ�
    /// </summary>
    public class DataGridViewComboBoxEditEditingControl : ComboBox, IDataGridViewEditingControl
    {
        DataGridView dataGridView;
        private bool valueChanged = false;
        int rowIndex;

        /// <summary>
        /// ��ʼ��
        /// </summary>
        public DataGridViewComboBoxEditEditingControl()
        {
            //stone
            //this.Format = DateTimePickerFormat.Short;
        }


        /// <summary>
        /// ��ȡ�����ñ༭�������޸ĵĵ�Ԫ��ĸ�ʽ��ֵ��
        /// </summary>
        public object EditingControlFormattedValue
        {
            get
            {
                return this.Text;//.ToShortDateString();
            }
            set
            {
                //if (value is String)
                //{
                //comboBoxֻ��Text��û��Value
                this.Text = (string)value;
                //}
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
            this.ForeColor = dataGridViewCellStyle.ForeColor;
            this.BackColor = dataGridViewCellStyle.BackColor;
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

        //���һ��Ҫ��Ҫ��ȻeditingControl��ֵ�޷�����cell
        //�����ֹ�����
        protected override void OnTextUpdate(EventArgs e)
        {
            valueChanged = true;
            this.EditingControlDataGridView.NotifyCurrentCellDirty(true);
            base.OnTextUpdate(e);
        }
        //����ѡ��
        protected override void OnSelectedValueChanged(EventArgs e)
        {
            valueChanged = true;
            this.EditingControlDataGridView.NotifyCurrentCellDirty(true);
            base.OnSelectedValueChanged(e);
        }
    }
}
