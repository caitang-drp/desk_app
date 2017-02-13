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
                errorProvider.SetError(textBox, "��������ȷ������!");
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
                errorProvider.SetError(textBox, "���벻��Ϊ��!");
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
                errorProvider.SetError(lookupText, "����Ϊ��,��ѡ����Ŀ!");
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
                cell.ErrorText = "����������!";
                return false;
            }
        }

        //commented by stone:���������ж��Ƿ񳬹�int�����ֵ
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
                    cell.ErrorText = "����������!";
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
                    cell.ErrorText = "������������!";
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
                cell.ErrorText = "���벻��Ϊ��!";
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
                cell.ErrorText = "����Ϊ����!";
                return false;
            }
        }
    }
}
