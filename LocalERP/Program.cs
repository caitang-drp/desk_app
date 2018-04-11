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
            if (AuthUtility.checkSN() == false) {
                SignForm signForm = new SignForm();
                signForm.ShowDialog();
                if (signForm.DialogResult != DialogResult.OK)
                    return;
            }
                
            LoginForm loginForm = new LoginForm();
            loginForm.ShowDialog();
            if (loginForm.DialogResult == DialogResult.OK)
                Application.Run(new MainForm());
            
        }
    }
}