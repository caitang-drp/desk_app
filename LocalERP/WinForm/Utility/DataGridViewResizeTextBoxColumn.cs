using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.IO;

namespace LocalERP.WinForm.Utility
{
    
    public class DataGridViewResizeTextBoxColumn : DataGridViewColumn
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public DataGridViewResizeTextBoxColumn()
            : base(new DataGridViewResizeTextBoxCell())
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
                    !value.GetType().IsAssignableFrom(typeof(DataGridViewResizeTextBoxCell)))
                {
                    throw new InvalidCastException("Must be a ResizeTextBoxCell");
                }
                base.CellTemplate = value;
            }
        }
    }
    /// <summary>
    /// 日期选择DataGridViewTextBoxCell
    /// </summary>
    public class DataGridViewResizeTextBoxCell : DataGridViewTextBoxCell
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public DataGridViewResizeTextBoxCell()
            : base()
        {
        }

        public void resetSize(string text) {
            Label panel = new Label();
            panel.AutoSize = true;
            panel.Text = text;
            panel.Hide();

            this.DataGridView.Controls.Add(panel);
            int widthNew = panel.Width + 20;
            int heightNew = panel.Height + 8;

            if (widthNew > this.OwningColumn.Width)
                this.OwningColumn.Width = widthNew;

            if (heightNew > this.OwningRow.Height)
                this.OwningRow.Height = heightNew;

            this.DataGridView.Controls.Remove(panel);
        }
    }
}
