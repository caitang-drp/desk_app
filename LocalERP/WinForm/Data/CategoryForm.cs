using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LocalERP.DataAccess.DataDAO;
using LocalERP.DataAccess.Data;

namespace LocalERP.WinForm
{
    public partial class CategoryForm : Form
    {
        //openMode 0:add   1:edit
        private int openMode = 0;
        private int categoryID = 0;
        private Category parent = null;

        private string categoryTableName;

        public delegate void ModifiedComplete();
        public event ModifiedComplete modifiedComplete;

        public CategoryForm(string categoryTableName, int mode, int id, Category parent)
        {
            openMode = mode;
            categoryID = id;
            this.parent = parent;

            this.categoryTableName = categoryTableName;

            InitializeComponent();
        }

        private void ElementForm_Load(object sender, EventArgs e)
        {
            if (openMode == 1)
            {
                this.label4.Text = "编辑 ID:" + categoryID;

                Category category = CategoryDao.getInstance().FindById(this.categoryTableName, categoryID);
                this.textBox_name.Text = category.Name;
                
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

        

        private bool getCategory(out Category category) {

            string name;
            bool isNameCorrect = this.getName(out name);
           
            if ( isNameCorrect)
            {
                category = new Category();
                category.Name = name;
                category.Id = categoryID;
                return true;
            }
            else
            {
                category = null;
                return false;
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
            Category category = null;
            if (this.getCategory(out category) == false)
                return;

            if (openMode == 0) {
                CategoryDao.getInstance().Insert(this.categoryTableName, parent, null, category);
                if (modifiedComplete != null)
                    modifiedComplete.Invoke();
                MessageBox.Show("保存配件成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else if (openMode == 1) {
                /*ElementDao.getInstance().Update(element);
                if (modifiedComplete != null)
                    modifiedComplete.Invoke();
                MessageBox.Show("修改配件成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();*/
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}