using System;
using System.Collections.Generic;
using System.Text;
using LocalERP.DataAccess.Utility;

namespace LocalERP.WinForm
{
    public class FormSingletonFactory
    {
        //singleton
        private static FormMgr formMgr;
        public static FormMgr getInstance()
        {
            if (formMgr == null)
            {
                if (ConfUtility.softType == 1)
                    formMgr = new FormCardMgr();
                else if (ConfUtility.softType == 0)
                    formMgr = new FormStainlessMgr();
            }
            return formMgr;
        }
    }
}
