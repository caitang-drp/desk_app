using System;
using System.Collections.Generic;
using System.Text;

namespace LocalERP.WinForm
{
    public class FormSingletonFactory
    {
        //singleton
        private static FormMgr formMgr;
        public static FormMgr getInstance()
        {
            if (formMgr == null)
                formMgr = new FormStainlessMgr();
            return formMgr;
        }
    }
}
