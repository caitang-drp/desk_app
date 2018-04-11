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
using System.IO;

namespace LocalERP.WinForm
{
    public partial class CategoryItemForm : LookupAndNotifyDockContent
    {
        //openMode: 0 select mode; 1 manage mode
        private int openMode = 0;
        //used as parameter delivered to multi thread
        private string searchName = "";

        private CategoryItemProxy categoryItemProxy;

        public CategoryItemProxy CategoryItemProxy
        {
            get { return categoryItemProxy; }
            set { categoryItemProxy = value; }
        }

        private DataTable recordsDataTable;

        public CategoryItemForm(int openMode, CategoryItemProxy proxy, string title, Form parentForm)
        {
            InitializeComponent();

            this.openMode = openMode;

            this.categoryItemProxy = proxy;
            this.Text = title;
            this.toolStripLabel_manage.Text = title + "����:";
            
            this.Owner = parentForm;

            categoryItemProxy.initColumns(this.dataGridView1);
            categoryItemProxy.initTree(this.treeView1);

            if (this.openMode == 0)
            {
                this.label_tip.Text = "˫������ѡ��" + title;
                categoryItemProxy.hideSomeColumns(this.dataGridView1);
            }
            else
                this.label_tip.Text = "˫�����ɱ༭" + title;

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            int parent = -1;
            if (treeView1.SelectedNode != null)
                parent = int.Parse(treeView1.SelectedNode.Name);
            searchName = null;

            selectCategory(parent);
        }

        private void selectCategory(int parent) {
            searchName = this.textBox_name.Text;
            recordsDataTable = categoryItemProxy.getRecordsTable(parent, this.searchName);
            categoryItemProxy.initRecords(this.dataGridView1, recordsDataTable);
        }

        public override void refresh()
        {
            categoryItemProxy.initTree(this.treeView1);
        }

        public void refreshList() {
            this.treeView1_AfterSelect(null, null);
        }

        public void initTree() {
            categoryItemProxy.initTree(this.treeView1);
        }

        private bool isAdd = false;

        //add child
        private void toolStripButton_addType_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null){
                TreeNode node = new TreeNode("�½����");
                treeView1.SelectedNode.Nodes.Add(node);
                treeView1.SelectedNode.Expand();
                isAdd = true;
                node.BeginEdit();
            }
            else if (treeView1.Nodes == null && treeView1.Nodes.Count == 0) {
                TreeNode node = new TreeNode("�½����");
                treeView1.Nodes.Add(node);
                isAdd = true;
                node.BeginEdit();
            }
        }


        private void toolStripButton_addType_brother_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null){
                TreeNode node = new TreeNode("�½����");
                TreeNode parent = treeView1.SelectedNode.Parent;
                if(parent!=null)
                    parent.Nodes.Insert(treeView1.SelectedNode.Index+1, node);
                else
                    treeView1.Nodes.Insert(treeView1.SelectedNode.Index+1, node);
                
                isAdd = true;
                node.BeginEdit();
            }
            else if (treeView1.Nodes == null && treeView1.Nodes.Count == 0)
            {
                TreeNode node = new TreeNode("�½����");
                treeView1.Nodes.Add(node);
                isAdd = true;
                node.BeginEdit();
            }
        }

        private void treeView1_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            //File.AppendAllText("e:\\debug.txt", string.Format("call after edit: {0},{1},{2}\r\n", e.Label, e.Node.Text, isAdd));
            //e.Label is the new text being edited. e.Node.Text is the original text
            //for a newly add node, its e.Node.Text is "�½����", and the e.Label is null unless is edited again

            string text = "";
            if (e.Label == null)
                if (isAdd)
                    text = e.Node.Text;
                else
                    return;
            
            if (e.Label=="") {

                if (isAdd)
                {
                    e.Node.Text = "";
                    e.Node.Remove();
                    MessageBox.Show("������Ʋ���Ϊ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    e.CancelEdit = true;
                    MessageBox.Show("������Ʋ���Ϊ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                return;
            }

            if (e.Label != null)
                text = e.Label;

            if (isAdd)
            {
                TreeNode node = e.Node;
                Category parent = null;
                Category right = null;
                if (node.Parent != null)
                    parent = CategoryDao.getInstance().FindById(CategoryItemProxy.CategoryTableName, int.Parse(node.Parent.Name));
                else if(node.PrevNode !=null)
                    right = CategoryDao.getInstance().FindById(CategoryItemProxy.CategoryTableName, int.Parse(node.PrevNode.Name));
                
                Category category = new Category();
                category.Name = text;
                int id = CategoryDao.getInstance().Insert(CategoryItemProxy.CategoryTableName, parent, right, category);
                node.Name = id.ToString();

                isAdd = false;
            }
            else
                CategoryDao.getInstance().UpdateName(CategoryItemProxy.CategoryTableName, int.Parse(e.Node.Name), text);

            this.invokeUpdateNotify(this.categoryItemProxy.UpdateType_Category);
            //ˢ�±���Ĵ��ڣ�
            this.refreshVersion(this.categoryItemProxy.UpdateType_Category);
        }


        private void toolStripButton_editType_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {

                int id = int.Parse(treeView1.SelectedNode.Name);
                CategoryForm form = FormSingletonFactory.getInstance().getCategoryForm(this.categoryItemProxy, 1, id);
                form.updateNotify -= new UpdateNotify(form_updateNotify_category);
                form.updateNotify += new UpdateNotify(form_updateNotify_category);
                form.ShowDialog();
            }
            else
                MessageBox.Show("��ѡ�����.", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void form_updateNotify_category(UpdateType notifyType)
        {
            this.refresh();
            this.refreshVersion(notifyType);
        }

        //����
        private void toolStripButton_upType_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                TreeNode preNode = treeView1.SelectedNode.PrevNode;

                if (preNode == null) {
                    MessageBox.Show("������Ѿ��ﵽ����������ˣ������޸�������������޸�.", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Category node = CategoryDao.getInstance().FindById(CategoryItemProxy.CategoryTableName, int.Parse(treeView1.SelectedNode.Name));
                Category pNode = CategoryDao.getInstance().FindById(CategoryItemProxy.CategoryTableName, int.Parse(preNode.Name));
                CategoryDao.getInstance().nodeUp(CategoryItemProxy.CategoryTableName, node, pNode);

                TreeNode parent = treeView1.SelectedNode.Parent;
                int index = treeView1.SelectedNode.Index;

                if (parent == null)
                {
                    treeView1.Nodes.Remove(preNode);
                    treeView1.Nodes.Insert(index, preNode);
                }
                else {
                    parent.Nodes.Remove(preNode);
                    parent.Nodes.Insert(index, preNode);
                }
            }
            else
                MessageBox.Show("��ѡ�����.", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void toolStripButton_delType_Click(object sender, EventArgs e)
        {
            
            if (treeView1.SelectedNode != null)
            {
                int id = int.Parse(treeView1.SelectedNode.Name);

                if (MessageBox.Show(string.Format("�Ƿ�ɾ�����:{0}?", treeView1.SelectedNode.Text), "��ʾ", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK)
                    return;

                if (treeView1.Nodes.Count == 1 && treeView1.Nodes[0].Nodes.Count == 0)
                {
                    MessageBox.Show("���һ������޷�ɾ��!", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!CategoryDao.getInstance().DeleteDeaf(categoryItemProxy.CategoryTableName, id))
                    MessageBox.Show("�޷�ɾ��,�������������Ӧ��.", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    treeView1.SelectedNode.Remove();
                    this.invokeUpdateNotify(this.categoryItemProxy.UpdateType_Category);
                    this.refreshVersion(this.categoryItemProxy.UpdateType_Category);
                }
            }else
                MessageBox.Show("��ѡ�����.", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /************  item  ***********************/
        private void toolStripButton_add_Click(object sender, EventArgs e)
        {
            int parent;
            if (treeView1.SelectedNode != null)
                parent = int.Parse(treeView1.SelectedNode.Name);

            else {
                MessageBox.Show("��ѡ��������������������!", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            MyDockContent form = categoryItemProxy.getItemForm(this, 0, 0);
            //����ط����ܻ����ԸĽ�
            form.updateNotify -= new UpdateNotify(form_updateNotify);
            form.updateNotify += new UpdateNotify(form_updateNotify);
            form.ShowDialog();
        }

        //���ֻ�Ǹ���categoryItemForm���Ӵ��ڻ������ⷢ���¼�
        void form_updateNotify(UpdateType notifyType)
        {
            this.refreshList();
            this.refreshVersion(notifyType);
        }

        public override void showDialog(object value)
        {
            this.ShowDialog();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (this.dataGridView1.Rows[e.RowIndex].Cells["ID"].Value == null)
                return;

            int id = (int)this.dataGridView1.Rows[e.RowIndex].Cells["ID"].Value;
            //openMode == 0����ѡ��
            if (openMode == 0)
            {
                string name = this.dataGridView1.Rows[e.RowIndex].Cells["name"].Value.ToString();
                LookupArg lookupArg = new LookupArg(id, name);
                lookupArg.ArgName = this.categoryItemProxy.CategoryName;

                //File.AppendAllText("e:\\debug.txt", string.Format("double click, thread:{0}\r\n", System.Threading.Thread.CurrentThread.ManagedThreadId));

                this.Close();
                this.invokeLookupCallback(lookupArg);
            }
            else if (openMode == 1) {
                MyDockContent form = categoryItemProxy.getItemForm(this, 1, id);
                form.updateNotify -= new UpdateNotify(form_updateNotify);
                form.updateNotify += new UpdateNotify(form_updateNotify);
                form.ShowDialog();
            }
        }

        private void toolStripButton_edit_Click(object sender, EventArgs e)
        {
            List<int> list = this.dataGridView1.getSelectIDs("ID", "check");
            if (list == null || list.Count <= 0)
            {
                MessageBox.Show("��ѡ��༭" + categoryItemProxy.ItemName, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            MyDockContent form = categoryItemProxy.getItemForm(this, 1, list[0]);
            form.updateNotify -= new UpdateNotify(form_updateNotify);
            form.updateNotify += new UpdateNotify(form_updateNotify);
            form.ShowDialog();
        }

        private void toolStripButton_delItem_Click(object sender, EventArgs e)
        {
            List<int> list = this.dataGridView1.getSelectIDs("ID", "check");
            if (list == null || list.Count <= 0)
            {
                MessageBox.Show("��ѡ��ɾ��"+categoryItemProxy.ItemName, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            StringBuilder ids = new StringBuilder();
            for (int ii = 0; ii < list.Count; ii++)
            {
                ids.Append(list[ii]);
                ids.Append(" ");
            }
            if (MessageBox.Show(string.Format("�Ƿ�ɾ��IDΪ{0}��{1}?", ids.ToString(),categoryItemProxy.ItemName), "��ʾ", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                try
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        categoryItemProxy.delItems(list[i]);
                    }
                    MessageBox.Show(string.Format("ɾ��{0}�ɹ�!", categoryItemProxy.ItemName), "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch
                {
                    MessageBox.Show(string.Format("ɾ��{0}ʧ�ܣ������������������õ���{0}!", categoryItemProxy.ItemName), "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                refreshList();
                this.invokeUpdateNotify(this.categoryItemProxy.UpdateType_Item);
                this.refreshVersion(this.categoryItemProxy.UpdateType_Item);
            }
        }
        
        private void CategoryItemForm_Shown(object sender, EventArgs e)
        {
            this.treeView1.ExpandAll();
        }

        private void button_search_Click(object sender, EventArgs e)
        {
            selectCategory(-1);
        }

        private void textBox_name_TextChanged(object sender, EventArgs e)
        {
            selectCategory(-1);
        }
    }
}