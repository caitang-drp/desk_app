using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LocalERP.DataAccess.DataDAO;
using LocalERP.DataAccess.Data;
using LocalERP.UiDataProxy;
using LocalERP.DataAccess.Utility;
using LocalERP.WinForm.Utility;

namespace LocalERP.WinForm
{
    public partial class CategoryForm : MyDockContent
    {
        //openMode 0:add   1:edit
        private int openMode = 0;
        private int categoryID = 0;
        private Category category;

        private CategoryItemTypeConf conf;

        public CategoryForm(CategoryItemTypeConf conf, int mode, int id)
        {
            openMode = mode;
            categoryID = id;

            this.conf = conf;

            InitializeComponent();

            ControlUtility.initTree(this.comboBoxTree1.tvTreeView, conf.CategoryTableName);
        }

        private void ElementForm_Load(object sender, EventArgs e)
        {
            if (openMode == 1)
            {
                category = CategoryDao.getInstance().FindById(conf.CategoryTableName, categoryID);
                this.textBox_name.Text = category.Name;
                this.comboBoxTree1.setSelectNode(category.Parent.ToString());
            }
        }

        /// <summary>
        /// get value from control with valiadating
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private bool getName(out string name) {
            if (string.IsNullOrEmpty(this.textBox_name.Text))
            {
                this.errorProvider1.SetError(this.textBox_name, "输入不能为空!");
                name = "";
                return false;
            }
            else
            {
                this.errorProvider1.SetError(this.textBox_name, string.Empty);
                name = this.textBox_name.Text;
                return true;
            }
        }

        /// <summary>
        /// event for validating
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_name_Validating(object sender, CancelEventArgs e)
        {
            string name;
            getName(out name);
        }

       

        /// <summary>
        /// event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (openMode == 0) {
            }
            else if (openMode == 1) {
                Category preParent = CategoryDao.getInstance().FindById(conf.CategoryTableName, category.Parent);

                Category newParent = null;
                if(this.comboBoxTree1.SelectedNode != null)
                    newParent = CategoryDao.getInstance().FindById(conf.CategoryTableName, int.Parse(this.comboBoxTree1.SelectedNode.Name));
                
                String name = "";
                if(this.getName(out name) == false)
                    return;

                if (newParent!=null && newParent.Left >= category.Left && newParent.Left <= category.Right)
                {
                    MessageBox.Show("不能选择子类别作为上级类别。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                CategoryDao.getInstance().UpdateName(conf.CategoryTableName, category.Id, name);
                CategoryDao.getInstance().UpdateParent(conf.CategoryTableName, category, preParent, newParent);

                this.invokeUpdateNotify(conf.UpdateType_Category);

                MessageBox.Show("修改类别成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.comboBoxTree1.setSelectNode("");
        }
    }
}