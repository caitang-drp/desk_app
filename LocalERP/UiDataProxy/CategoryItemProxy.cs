using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using LocalERP.DataAccess.DataDAO;
using LocalERP.DataAccess.Data;
using LocalERP.WinForm;
using System.IO;

namespace LocalERP.UiDataProxy
{
    public abstract class CategoryItemProxy
    {
        private NotifyType notifyType;

        public NotifyType NotifyType
        {
            get { return notifyType; }
            set { notifyType = value; }
        }
        private string categoryTableName;

        public string CategoryTableName
        {
            get { return categoryTableName; }
            set { categoryTableName = value; }
        }

        private string itemName;

        public string ItemName
        {
            get { return itemName; }
            set { itemName = value; }
        }

        public abstract void initColumns(DataGridView dgv);
        public abstract void initTree(TreeView tv);
        public abstract void initItems(DataGridView dgv, int parentId);
        public abstract DataTable getRecordsTable(int parentId, string name);
        public abstract void initRecords(DataGridView dgv, DataTable dt);

        public virtual void delItems(int id) { }

        public abstract Form getItemForm(int openMode, int ID);
    }
}
