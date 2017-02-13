using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using LocalERP.WinForm;
using LocalERP.WinForm.Utility;

namespace LocalERP.DataAccess.Utility
{
    public static class ValidateUtility
    {
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
            if (required == false && positive ==false) 
            {
                if (temp == null || temp == "" || int.TryParse(temp, out result))
                {
                    cell.ErrorText = string.Empty;
                    return true;
                }
                else
                {
                    cell.ErrorText = "请输入整数!";
                    return false;
                }
            }
            else if (required == false && positive == true) {
                uint resultTemp=0;
                if (temp == null || temp == "" || uint.TryParse(temp, out resultTemp))
                {
                    result = (int)resultTemp;
                    cell.ErrorText = string.Empty;
                    return true;
                }
                else
                {
                    cell.ErrorText = "请输入正整数!";
                    return false;
                }
            }
            return false;
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
    }
}
