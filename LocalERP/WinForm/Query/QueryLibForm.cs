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
    public partial class QueryLibForm : CategoryItemForm
    {
        private string searchName = null;

        private CategoryItemTypeConf categoryItemProxy;

        public CategoryItemTypeConf CategoryItemProxy
        {
            get { return categoryItemProxy; }
            set { categoryItemProxy = value; }
        }

        private DataTable recordsDataTable;

        public QueryLibForm(CategoryItemTypeConf conf, Form parentForm, string title)
            :base(0, conf, title, parentForm)
        {
            this.Owner = parentForm;
            InitializeComponent();
            this.Text = title;
            this.categoryItemProxy = conf;

            this.backgroundWorker.DoWork += new DoWorkEventHandler(backgroundWorker_DoWork);
            this.backgroundWorker.RunWorkerCompleted +=new RunWorkerCompletedEventHandler(backgroundWorker_RunWorkerCompleted);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            initColumns();
            initTree();
        }

        protected override void initColumns()
        {
            DataGridViewCheckBoxColumn check = new DataGridViewCheckBoxColumn();
            DataGridViewTextBoxColumn ID = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn name = new DataGridViewTextBoxColumn();
            DataGridViewResizeTextBoxColumn num = new DataGridViewResizeTextBoxColumn();
            DataGridViewTextBoxColumn totalNum = new DataGridViewTextBoxColumn();

            check.HeaderText = "选择";
            check.Name = "check";
            check.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            check.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            check.Width = 60;
            check.Visible = false;
            // 
            // ID
            // 
            ID.HeaderText = "货品ID";
            ID.Name = "ID";
            ID.ReadOnly = true;
            ID.Width = 60;
            // 
            // name
            // 
            name.HeaderText = "货品名称";
            name.Name = "name";
            name.ReadOnly = true;
            name.Width = 140;
            // 
            // comment
            // 
            num.HeaderText = "库存数量";
            num.Name = "num";
            num.ReadOnly = true;
            num.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                check,
                ID,
                name,
                num});
        }

        protected override DataTable getRecordsTable(int parentId, string name)
        {
            Category parent = CategoryDao.getInstance().FindById(conf.CategoryTableName, parentId);
            DataTable dataTable = ProductClothesDao.getInstance().FindList(parent, name);
            dataTable.Columns.Add("numText");
            foreach (DataRow dr in dataTable.Rows)
                dr["numText"] = ProductClothesDao.getInstance().getNumString((int)(dr["Product.ID"]));
            return dataTable;
        }

        protected override void initRecords(DataTable dataTable)
        {
            dataGridView1.Rows.Clear();
            foreach (DataRow dr in dataTable.Rows)
            {
                int index = dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells["ID"].Value = dr["Product.ID"];
                dataGridView1.Rows[index].Cells["name"].Value = dr["Product.name"];
                DataGridViewResizeTextBoxCell numCell = dataGridView1.Rows[index].Cells["num"] as DataGridViewResizeTextBoxCell;
                numCell.Value = dr["numText"].ToString();
                numCell.resetSize(numCell.Value.ToString());
            }
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

        public override void refresh()
        {
            //categoryItemProxy.initTree(this.treeView1);
        }

        public override void showDialog(object value)
        {
            this.ShowDialog();
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            //recordsDataTable = categoryItemProxy.getRecordsTable((int)e.Argument,searchName);
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //categoryItemProxy.initRecords(this.dataGridView1, recordsDataTable);
            this.invokeEndLoadNotify();
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            searchName = this.textBox_search.Text;
            this.backgroundWorker.RunWorkerAsync(-1);
            this.invokeBeginLoadNotify();
        }

        protected override MyDockContent getItemForm(Form owner, int openMode, int ID)
        {
            return null;
        }

        protected override void delItem(int id)
        {
        }
    }
}