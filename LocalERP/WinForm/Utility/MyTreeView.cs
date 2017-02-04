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
        /// ָʾ�Ƿ�ȡ���༭��TRUEȡ��
        /// </summary>
        bool IsCancelEdit = true;
        protected override void OnBeforeLabelEdit(System.Windows.Forms.NodeLabelEditEventArgs e)
        {
            //�༭֮ǰĬ�����ն���ȡ���༭��
            IsCancelEdit = true;
            base.OnBeforeLabelEdit(e);
        }

        protected override void OnAfterLabelEdit(System.Windows.Forms.NodeLabelEditEventArgs e)
        {
            //���»س���ʱ��ָʾ��ȡ���༭������ȡ����
            if (IsCancelEdit)
            {
                e.CancelEdit = true;
            }
            base.OnAfterLabelEdit(e);
        }
        protected override void OnPreviewKeyDown(System.Windows.Forms.PreviewKeyDownEventArgs e)
        {
            //���»س�����ָʾ��ȡ���༭
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                IsCancelEdit = false;
            }
            base.OnPreviewKeyDown(e);
        }
    }
}


