using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using LocalERP.WinForm;
using LocalERP.WinForm.Utility;
using System.Data;

namespace LocalERP.DataAccess.Utility
{
    public static class ValidateUtility
    {
        /************** textbox ******************/
        public static bool getDouble(TextBox textBox, ErrorProvider errorProvider, bool required, out double result)
        {
            result = 0;
            string temp = textBox.Text.ToString();
            if (required == true && (string.IsNullOrEmpty(temp) || !double.TryParse(temp, out result))
                || required == false && !string.IsNullOrEmpty(temp) && !double.TryParse(temp, out result))
            {
                errorProvider.SetError(textBox, "请输入正确的数字!");
                return false;
            }
            else
            {
                errorProvider.SetError(textBox, string.Empty);
                return true;
            }
            
        }


        public static bool getName(TextBox textBox, ErrorProvider errorProvider, out string name)
        {
            if (string.IsNullOrEmpty(textBox.Text))
            {
                errorProvider.SetError(textBox, "输入不能为空!");
                name = "";
                return false;
            }
            else
            {
                errorProvider.SetError(textBox, string.Empty);
                name = textBox.Text;
                return true;
            }
        }

        public static bool getLookupValueID(LookupText lookupText, ErrorProvider errorProvider, out int id)
        {
            if (lookupText.LookupArg == null || lookupText.LookupArg.Value == null)
            {
                errorProvider.SetError(lookupText, "不能为空,请选择项目!");
                id = 0;
                return false;
            }
            else
            {
                errorProvider.SetError(lookupText, string.Empty);
                id = int.Parse(lookupText.LookupArg.Value.ToString());
                return true;
            }
        }

        /************** cell *****************/
        public static bool getDouble(DataGridViewCell cell, out double result)
        {
            result = 0;
            string temp = cell.EditedFormattedValue.ToString();
            if (temp == null || temp == "" || double.TryParse(temp, out result))
            {
                cell.ErrorText = string.Empty;
                return true;
            }
            else
            {
                cell.ErrorText = "请输入数字!";
                return false;
            }
        }

        //commented by stone:还需增加判断是否超过int的最大值
        public static bool getInt(DataGridViewCell cell, bool required, bool positive, out int result)
        {
            result = 0;
            string temp = cell.EditedFormattedValue.ToString();

            if (int.TryParse(temp, out result))
            {
                if (positive == true && result < 0)
                {
                    cell.ErrorText = "请输入正整数!";
                    return false;
                }
                else
                {
                    cell.ErrorText = string.Empty;
                    return true;
                }
            }
            else {
                if (required == false && string.IsNullOrEmpty(temp))
                {
                    cell.ErrorText = string.Empty;
                    return true;
                }
                else {
                    cell.ErrorText = "请输入整数!";
                    return false;
                }
            }
        }

        public static bool getInt(DataGridViewCell cell, bool required, bool positive, out int result, out bool isNull) { 
            string temp = cell.EditedFormattedValue.ToString();
            isNull = string.IsNullOrEmpty(temp) ? true : false;

            return getInt(cell, required, positive, out result);
        }

        public static bool getString(DataGridViewCell cell, bool required, out string result)
        {
            result = cell.EditedFormattedValue.ToString();
            if (required && string.IsNullOrEmpty(result))
            {
                cell.ErrorText = "输入不能为空!";
                return false;
            }
            else
            {
                cell.ErrorText = string.Empty;
                return true;
            }
        }

        public static bool getLookupValue(DataGridViewCell cell, out object result)
        {
            result = null;
            LookupArg temp = (cell as DataGridViewLookupCell).EditedValue as LookupArg;
            if (temp != null && !string.IsNullOrEmpty(temp.Text))
            {
                result = temp.Value;
                cell.ErrorText = string.Empty;
                return true;
            }
            else
            {
                cell.ErrorText = "此项为必填!";
                return false;
            }
        }

        /******************** DataRow **************/
        public static bool getInt(DataRow dr, string name, out int result, out bool isNull) {
            object drValue = dr[name];
            if (drValue != null && !string.IsNullOrEmpty(drValue.ToString()))
            {
                isNull = false;
                return int.TryParse(drValue.ToString(), out result);
            }
            else
            {
                result = 0;
                isNull = true;
                return true;
            }
        }

        public static bool getDouble(DataRow dr, string name, out double result, out bool isNull)
        {
            object drValue = dr[name];
            if (drValue != null && !string.IsNullOrEmpty(drValue.ToString()))
            {
                isNull = false;
                return double.TryParse(drValue.ToString(), out result);
            }
            else
            {
                result = 0;
                isNull = true;
                return true;
            }
        }
    }
}
