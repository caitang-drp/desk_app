using System;
using System.Collections.Generic;
using System.Windows.Forms;
using LocalERP.WinForm;
using System.Text;
using System.IO;
using LocalERP.DataAccess.DataDAO;
using LocalERP.DataAccess.Utility;

namespace LocalERP
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            /*AuthUtility.checkSN()获取数据库sn号，然后进行验证*/
            if (ConfUtility.isTrial == false && AuthUtility.checkSN() == false) {
                SignForm signForm = new SignForm();
                signForm.ShowDialog();
                if (signForm.DialogResult != DialogResult.OK)
                    return;
            }

            /*AuthUtility.checkSN()获取数据库sn号，然后进行验证*/
            if (ConfUtility.isTrial == true)
            {
                if (DateTime.Compare(DateTime.Now, ConfUtility.dateLine) >= 0)
                {
                    MessageBox.Show("试用期已过!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            LoginForm loginForm = new LoginForm();
            loginForm.ShowDialog();
            if (loginForm.DialogResult == DialogResult.OK)
                Application.Run(new MainForm());
            
        }
    }
}