using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Management;
using System.IO;

public partial class AuthForm : Form {
    private static byte infoKey = 0;

    public static byte InfoKey {
        get {
            return infoKey;
        }
    }

    public AuthForm() {
        InitializeComponent();
    }


    private String[] getComputerInfo() {
        string[] str = new string[3];
        str[0] = "";
        str[1] = "";
        str[2] = "";

        //获取CPU序列号
        ManagementClass mcCpu = new ManagementClass("win32_Processor");
        ManagementObjectCollection mocCpu = mcCpu.GetInstances();
        foreach (ManagementObject m in mocCpu) {
            str[0] += m["ProcessorId"].ToString();
        }

        //获取硬盘序列号
        ManagementClass mcHD = new ManagementClass("win32_logicaldisk");
        ManagementObjectCollection mocHD = mcHD.GetInstances();
        foreach (ManagementObject m in mocHD) {
            if (m["DeviceID"].ToString() == "C:") {
                str[1] += m["VolumeSerialNumber"].ToString();
                break;
            }
        }

        //获取网卡MAC地址
        ManagementClass mcMAC = new ManagementClass("Win32_NetworkAdapterConfiguration");
        ManagementObjectCollection mocMAC = mcMAC.GetInstances();
        foreach (ManagementObject m in mocMAC) {
            if ((bool) m["IPEnabled"]) {
                str[2] += m["MacAddress"].ToString();
                break;
            }
        }
        return str;
    }

    private string getCPU() {
        string temp = "";

        //获取CPU序列号
        ManagementClass mcCpu = new ManagementClass("win32_Processor");
        ManagementObjectCollection mocCpu = mcCpu.GetInstances();
        foreach (ManagementObject m in mocCpu)
        {
            temp += m["ProcessorId"].ToString();
        }
        return temp;
    }

    private void button1_Click(object sender, EventArgs e) {
        /*
        */
        Encoding enc = Encoding.ASCII;
        byte[] data = null;
        data = enc.GetBytes(this.textBox_source.Text);
        char [] sn_chars = new char[6];

        sn_chars[0] = (((ushort)data[5] * 2 + (ushort)data[0] + (ushort)data[4] * 7) % 10).ToString().ToCharArray()[0];
        sn_chars[1] = (((ushort)data[5] * 9 + (ushort)data[1] + (ushort)data[3] * 3) % 10).ToString().ToCharArray()[0];
        sn_chars[2] = (((ushort)data[2] * 6 + (ushort)data[5] + (ushort)data[4] * 2) % 10).ToString().ToCharArray()[0];
        sn_chars[3] = (((ushort)data[4] * 3 + (ushort)data[1] + (ushort)data[5] * 8) % 10).ToString().ToCharArray()[0];
        sn_chars[4] = (((ushort)data[3] * 7 + (ushort)data[5] + (ushort)data[3] * 7) % 10).ToString().ToCharArray()[0];
        sn_chars[5] = (((ushort)data[2] * 5 + (ushort)data[4] + (ushort)data[5] * 12) % 10).ToString().ToCharArray()[0];
        
        this.textBox_serial.Text = new string(sn_chars);
    }

    private void button2_Click(object sender, EventArgs e)
    {
        this.Close();
    }
}

