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
            string cpuStr = "", hdStr="", macStr="";
            char[] cpu_chars, hd_chars, mac_chars;

            ManagementClass mcCpu = new ManagementClass("win32_Processor");
            ManagementObjectCollection mocCpu = mcCpu.GetInstances();
            foreach (ManagementObject m in mocCpu)
            {
                cpuStr += m["ProcessorId"].ToString();
            }
            cpu_chars = cpuStr.ToCharArray();

            ManagementClass mcHD = new ManagementClass("win32_logicaldisk");
            ManagementObjectCollection mocHD = mcHD.GetInstances();
            foreach (ManagementObject m in mocHD)
            {
                hdStr += m["VolumeSerialNumber"].ToString();
                break;
            }
            hd_chars = hdStr.ToCharArray();

            char[] result_chars = new char[6];
            result_chars[0] = cpu_chars[cpu_chars.Length - 1];
            result_chars[1] = cpu_chars[cpu_chars.Length - 2];
            result_chars[2] = cpu_chars[cpu_chars.Length - 3];
            result_chars[3] = hd_chars[hd_chars.Length - 1];
            result_chars[4] = hd_chars[hd_chars.Length - 2];
            result_chars[5] = hd_chars[hd_chars.Length - 3];
            return new string(result_chars).ToUpper();

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
