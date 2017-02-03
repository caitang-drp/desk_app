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

namespace LocalERP.WinForm
{
    public partial class CharactorForm : MyDockContent
    {
        private int charactorId = 1;

        public int CharactorId
        {
            get { return charactorId; }
            set { charactorId = value; }
        }

        private bool modify = false;

        public bool Modify
        {
            get { return modify; }
            set { modify = value; }
        }

        private bool needSave = false;

        public CharactorForm(int charactorId)
        {
            InitializeComponent();
            this.charactorId = charactorId;
            if (charactorId == 2)
                this.label3.Text = "尺码信息列表:";
            this.button_save.Enabled = false;
        }

        private void initList() {
            List <CharactorValue> list = CharactorValueDao.getInstance().FindList(charactorId);
            foreach (CharactorValue value in list) {
                int index = this.dataGridView1.Rows.Add(null, value.Id, value.Name);
            }
        }

        private void CharactorForm_Load(object sender, EventArgs e)
        {
            initList();
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            this.needSave = true;
            this.button_save.Enabled = true;

            int index = this.dataGridView1.Rows.Add();
            DataGridViewTextBoxCell cell = (this.dataGridView1.Rows[index].Cells["name"] as DataGridViewTextBoxCell);
            cell.ReadOnly = false;
            this.dataGridView1.CurrentCell = cell;
            this.dataGridView1.BeginEdit(true);
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            if (this.needSave == true)
            {
                //List<CharactorValue> values = new List<CharactorValue>();
                foreach (DataGridViewRow row in this.dataGridView1.Rows)
                {
                    CharactorValue value = new CharactorValue();
                    string name = null;
                    if(ValidateUtility.getString(row.Cells["name"], true, out name) == false)
                        return;
                    value.Name = name;
                    value.CharactorId = charactorId;

                    object id = row.Cells["ID"].Value;
                    if (id == null || id.ToString() == "")
                    {
                        value.Id = -1;
                        int idReturn = CharactorValueDao.getInstance().Insert(value);
                        row.Cells["ID"].Value = idReturn;
                    }
                    else
                    {
                        value.Id = int.Parse(id.ToString());
                        CharactorValueDao.getInstance().Update(value);
                    }

                    //values.Add(value);
                }
                /*
                foreach (CharactorValue value in values) {
                    if (value.Id <= 0)
                        CharactorValueDao.getInstance().Insert(value);
                    else
                        CharactorValueDao.getInstance().Update(value);
                }*/

                this.modify = true;
                
                this.needSave = false;
                this.button_save.Enabled = false;

                MessageBox.Show("保存成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
        }

        private void button_del_Click(object sender, EventArgs e)
        {
            List<DataGridViewRow> rows = this.dataGridView1.getCheckRows("check");
            if (rows.Count <= 0)
            {
                MessageBox.Show("请选择项!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("是否删除选中的项目?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                this.modify = true;

                foreach(DataGridViewRow row in rows)
                {
                    object id = row.Cells["ID"].Value;
                    if (id != null && id.ToString() != "")
                    {
                        if (CharactorValueDao.getInstance().Delete(int.Parse(id.ToString())) == false)
                        {
                            MessageBox.Show(string.Format("删除ID为{0}的项目失败,该项目可能已被引用!", id), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    this.dataGridView1.Rows.Remove(row);

                }
                MessageBox.Show("删除成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void CharactorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (needSave && MessageBox.Show("信息尚未保存，是否放弃保存？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
                e.Cancel = true;
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control.GetType().Equals(typeof(DataGridViewTextBoxEditingControl)))
            {
                DataGridViewTextBoxEditingControl editingControl = e.Control as DataGridViewTextBoxEditingControl;
                string columnName = this.dataGridView1.CurrentCell.OwningColumn.Name;
                if (columnName == "name")
                {
                    editingControl.TextChanged -= new EventHandler(editingControl_TextChanged);
                    editingControl.TextChanged += new EventHandler(editingControl_TextChanged);
                }
            }
        }

        void editingControl_TextChanged(object sender, EventArgs e)
        {
            DataGridViewTextBoxEditingControl temp = (sender as DataGridViewTextBoxEditingControl);
            //if change current cell, the editing control's value is changed, but needSave should not be changed
            //use Focused to identify
            bool test = temp.Focused;
            if(test == true)
            {
                this.needSave = true;
                this.button_save.Enabled = true;
            }
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}