using System;
using System.Collections.Generic;
using System.Text;
using System.Management;
using LocalERP.DataAccess.DataDAO;

namespace LocalERP.DataAccess.Utility
{
    public static class AuthUtility
    {
        public static bool checkSN()
        {
            string sn = ConfDao.getInstance().Get(2);
            return checkSN(sn);
        }

        public static bool checkSN(string sn)
        {
            if (string.IsNullOrEmpty(sn))
                return false;

            if (sn.Equals(getSN()))
                return true;
            return false;
        }

        public static string getCPU()
        {
            string temp = "";

            //ªÒ»°CPU–Ú¡–∫≈
            ManagementClass mcCpu = new ManagementClass("win32_Processor");
            ManagementObjectCollection mocCpu = mcCpu.GetInstances();
            foreach (ManagementObject m in mocCpu)
            {
                temp += m["ProcessorId"].ToString();
            }

            char[] source_chars = temp.ToCharArray();

            char[] source_chars_new = new char[6];
            for (int i = 0; i < 3; i++)
            {
                source_chars_new[i] = source_chars[i];
                source_chars_new[5 - i] = source_chars[source_chars.Length - 1 - i];
            }
            return new string(source_chars_new);
        }

        private static string getSN()
        {
            Encoding enc = Encoding.ASCII;
            byte[] data = null;
            data = enc.GetBytes(getCPU());
            char[] sn_chars = new char[6];

            sn_chars[0] = (((ushort)data[5] * 2 + (ushort)data[0] + (ushort)data[4] * 7) % 10).ToString().ToCharArray()[0];
            sn_chars[1] = (((ushort)data[5] * 9 + (ushort)data[1] + (ushort)data[3] * 3) % 10).ToString().ToCharArray()[0];
            sn_chars[2] = (((ushort)data[2] * 6 + (ushort)data[5] + (ushort)data[4] * 2) % 10).ToString().ToCharArray()[0];
            sn_chars[3] = (((ushort)data[4] * 3 + (ushort)data[1] + (ushort)data[5] * 8) % 10).ToString().ToCharArray()[0];
            sn_chars[4] = (((ushort)data[3] * 7 + (ushort)data[5] + (ushort)data[3] * 7) % 10).ToString().ToCharArray()[0];
            sn_chars[5] = (((ushort)data[2] * 5 + (ushort)data[4] + (ushort)data[5] * 12) % 10).ToString().ToCharArray()[0];

            return new string(sn_chars);
        }
    }
}
