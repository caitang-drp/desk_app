using System;
using System.Collections.Generic;
using System.Text;

namespace LocalERP.WinForm
{

    public class LookupAndNotifyDockContent : MyDockContent
    {
        public delegate void LookupCallback(LookupArg arg);
        public event LookupCallback lookupCallback;
        public virtual void showDialog(object value){}
        public void invokeLookupCallback(LookupArg arg) {
            if(lookupCallback!=null)
                this.lookupCallback.Invoke(arg);
        }
    }
}