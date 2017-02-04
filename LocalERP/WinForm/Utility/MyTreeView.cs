using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace LocalERP.WinForm.Utility
{
    public partial class MyTreeView : TreeView
    {
        public MyTreeView()
        {
            InitializeComponent();
        }

        public MyTreeView(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        /// <summary>
        /// 指示是否取消编辑，TRUE取消
        /// </summary>
        bool IsCancelEdit = true;
        protected override void OnBeforeLabelEdit(System.Windows.Forms.NodeLabelEditEventArgs e)
        {
            //编辑之前默认最终都是取消编辑的
            IsCancelEdit = true;
            base.OnBeforeLabelEdit(e);
        }

        protected override void OnAfterLabelEdit(System.Windows.Forms.NodeLabelEditEventArgs e)
        {
            //按下回车键时，指示不取消编辑，否则被取消。
            if (IsCancelEdit)
            {
                e.CancelEdit = true;
            }
            base.OnAfterLabelEdit(e);
        }
        protected override void OnPreviewKeyDown(System.Windows.Forms.PreviewKeyDownEventArgs e)
        {
            //按下回车键，指示不取消编辑
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                IsCancelEdit = false;
            }
            base.OnPreviewKeyDown(e);
        }
    }
}


