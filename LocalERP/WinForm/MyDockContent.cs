//created by stone: DockContent为《可停靠窗口框架WinFormsUI》的类，它继承了Form，所有要实现可停靠的窗口都要继承它
//定义MyDockContent用于存放一些共有的方法和属性

using System;
using System.Collections.Generic;
using System.Text;
using WeifenLuo.WinFormsUI.Docking;

namespace LocalERP.WinForm
{
    public class MyDockContent : DockContent
    {
        //commented by stone: 后台执行类，实现长时间的操作时，防止UI阻塞
        protected System.ComponentModel.BackgroundWorker backgroundWorker;

        public MyDockContent() {
            backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.Activated += new EventHandler(MyDockContent_Activated);
        }

        void MyDockContent_Activated(object sender, EventArgs e)
        {
            bool refresh = false;
            //reference to FormMgr is high coupling
            Dictionary<UpdateType, int> dic = FormSingletonFactory.getInstance().getVersions();
            List<UpdateType> needUpdateVersions = new List<UpdateType>();
            foreach (UpdateType key in versionDic.Keys)
            {

                if (versionDic[key] < dic[key])
                {
                    needUpdateVersions.Add(key);
                    refresh = true;
                }
            }

            foreach (UpdateType version in needUpdateVersions)
                versionDic[version] = dic[version];

            if (refresh == true)
                this.refresh();
        }

        //参考FormMgr.updateVersion的注释
        protected Dictionary<UpdateType, int> versionDic = new Dictionary<UpdateType, int>();
        public void initVersions(Dictionary<UpdateType, int> dicCVs, params UpdateType[] cvs)
        {
            versionDic.Clear();
            foreach (UpdateType cv in cvs)
            { 
                versionDic.Add(cv, dicCVs[cv]);
            }
        }

        protected void refreshVersion(UpdateType type) {
            //reference to FormMgr is high coupling
            if (this.versionDic.ContainsKey(type))
                this.versionDic[type] = FormSingletonFactory.getInstance().getVersions()[type];
        }

        public delegate void UpdateNotify(UpdateType notifyType);
        public event UpdateNotify updateNotify;

        public void invokeUpdateNotify(UpdateType type) {
            if (updateNotify != null)
                updateNotify.Invoke(type);
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

    public enum UpdateType { 
        PurchaseUpdate,
        PurchaseFinishUpdate,

        SellUpdate,
        SellFinishUpdate,
        
        LibUpdate,
        LibFinishUpdate,
        
        ProductUpdate,
        ProductCategoryUpdate,
        
        CustomerUpdate,
        CustomerCategoryUpdate
    }
}
