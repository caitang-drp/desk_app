//created by stone: DockContent为《可停靠窗口框架WinFormsUI》的类，它继承了Form，所有要实现可停靠的窗口都要继承它
//定义MyDockContent用于存放一些共有的方法和属性

using System;
using System.Collections.Generic;
using System.Text;
using WeifenLuo.WinFormsUI.Docking;
using System.IO;

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

        //打开窗口时，检查是否需要更新
        void MyDockContent_Activated(object sender, EventArgs e)
        {
            //File.AppendAllText("e:\\debug.txt", string.Format("Form:{0} activted. time:{1}\r\n", this.Name, DateTime.Now.ToLongTimeString()));
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
        //跟本窗口相关的version
        protected Dictionary<UpdateType, int> versionDic = new Dictionary<UpdateType, int>();
        public void initVersions(Dictionary<UpdateType, int> dicCVs, params UpdateType[] cvs)
        {
            versionDic.Clear();
            foreach (UpdateType cv in cvs)
            { 
                versionDic.Add(cv, dicCVs[cv]);
            }
        }

        //更新form本身的version，主要用在categoryItemForm里
        protected void refreshVersion(UpdateType type) {
            //reference to FormMgr is high coupling
            if (this.versionDic.ContainsKey(type))
                this.versionDic[type] = FormSingletonFactory.getInstance().getVersions()[type];
        }

        //向外发布更新提示，外面的处理事件的方法就是更新版本
        public delegate void UpdateNotify(UpdateType notifyType);
        public event UpdateNotify updateNotify;

        public void invokeUpdateNotify(UpdateType type) {
            if (updateNotify != null)
                updateNotify.Invoke(type);
        }

        //用于界面等待
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
        //表示货单更新
        PurchaseUpdate,
        //表示或货单审核了，库存有改变
        PurchaseFinishUpdate,

        ManuUpdate,
        ManuFinishUpdate,

        SellUpdate,
        SellFinishUpdate,
        
        PayReceiptUpdate,
        PayReceiptFinishUpdate,

        LibUpdate,
        LibFinishUpdate,
        
        ProductUpdate,
        ProductCategoryUpdate,
        
        CustomerUpdate,
        CustomerCategoryUpdate,

        CardUpdate,
        //ConsumeUpdate是否有必要
        ConsumeUpdate
    }
}
