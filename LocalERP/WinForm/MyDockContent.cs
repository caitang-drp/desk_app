using System;
using System.Collections.Generic;
using System.Text;
using WeifenLuo.WinFormsUI.Docking;

namespace LocalERP.WinForm
{
    public class MyDockContent : DockContent
    {
        protected System.ComponentModel.BackgroundWorker backgroundWorker;

        public MyDockContent() {
            backgroundWorker = new System.ComponentModel.BackgroundWorker();
        }

        public delegate void ChangedNotify(NotifyType notifyType);
        public event ChangedNotify changedNotify;

        public void invokeChangeNotify(NotifyType type) {
            if (changedNotify != null)
                changedNotify.Invoke(type);
        }

        public delegate void BeginLoadNotify();
        public event BeginLoadNotify beginLoadNotify;

        public void invokeBeginLoadNotify()
        {
            if (beginLoadNotify != null)
                beginLoadNotify.Invoke();
        }

        public delegate void EndLoadNotify();
        public event EndLoadNotify endLoadNotify;

        public void invokeEndLoadNotify()
        {
            if (endLoadNotify != null)
                endLoadNotify.Invoke();
        }

        public virtual void refresh() { }
    }

    public enum NotifyType { 
        PurchaseUpdate,
        SellUpdate,
        LibUpdate,
        ProductUpdate,
        ProductCategoryUpdate,
        CustomerUpdate,
        CustomerCategoryUpdate
    }
}
