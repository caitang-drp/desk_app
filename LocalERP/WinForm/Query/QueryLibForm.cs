using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using WeifenLuo.WinFormsUI.Docking;
using LocalERP.DataAccess.Data;
using LocalERP.UiDataProxy;
using LocalERP.DataAccess.DataDAO;
using LocalERP.WinForm.Utility;

namespace LocalERP.WinForm
{
    public partial class QueryLibForm : LookupAndNotifyDockContent
    {
        private string searchName = null;

        private CategoryItemProxy categoryItemProxy;

        public CategoryItemProxy CategoryItemProxy
        {
            get { return categoryItemProxy; }
            set { categoryItemProxy = value; }
        }

        private DataTable recordsDataTable;

        public QueryLibForm(CategoryItemProxy proxy, Form parentForm, string title):base()
        {
            this.Owner = parentForm;
            InitializeComponent();
            this.Text = title;
            this.categoryItemProxy = proxy;

            this.backgroundWorker.DoWork += new DoWorkEventHandler(backgroundWorker_DoWork);
            this.backgroundWorker.RunWorkerCompleted +=new RunWorkerCompletedEventHandler(backgroundWorker_RunWorkerCompleted);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            categoryItemProxy.initColumns(this.dataGridView1);
            categoryItemProxy.initTree(this.treeView1);
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            int parent = -1;
            if (treeView1.SelectedNode != null)
                parent = int.Parse(treeView1.SelectedNode.Name);

            searchName = null;
            this.backgroundWorker.RunWorkerAsync(parent);
            this.invokeBeginLoadNotify();
        }

        private void toolStripButton_addType_Click(object sender, EventArgs e)
        {
            Category parent = null;
            if(treeView1.SelectedNode != null)
                parent = CategoryDao.getInstance().FindById(CategoryItemProxy.CategoryTableName, int.Parse(treeView1.SelectedNode.Name));

            CategoryForm form = new CategoryForm(this.categoryItemProxy.CategoryTableName, 0, -1, parent);
            form.modifiedComplete += new CategoryForm.ModifiedComplete(refresh);
            form.ShowDialog(this);
        }

        private void toolStripButton_add_Click(object sender, EventArgs e)
        {
            int parent;
            if (treeView1.SelectedNode != null)
                parent = int.Parse(treeView1.SelectedNode.Name);

            else {
                MessageBox.Show("请选择类型，如无类型请先增加!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            categoryItemProxy.getItemForm(0,0).ShowDialog();
        }

        public override void refresh()
        {
            categoryItemProxy.initTree(this.treeView1);
        }

        public override void showDialog(object value)
        {
            this.ShowDialog();
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            recordsDataTable = categoryItemProxy.getRecordsTable((int)e.Argument,searchName);
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            categoryItemProxy.initRecords(this.dataGridView1, recordsDataTable);
            this.invokeEndLoadNotify();
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            searchName = this.textBox_search.Text;
            this.backgroundWorker.RunWorkerAsync(-1);
            this.invokeBeginLoadNotify();
        }
    }
}