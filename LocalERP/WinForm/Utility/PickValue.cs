using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using LocalERP.DataAccess.Data;

namespace LocalERP.WinForm.Utility
{
    public partial class PickValue : UserControl
    {
        private int charactorType = 1;

        private List<CharactorValue> items = null;
        private Dictionary<int, CharactorValue> dics = new Dictionary<int, CharactorValue>();
        public delegate void SelectValueChanged();
        public event SelectValueChanged selectValueChanged;

        public PickValue()
        {
            InitializeComponent();
        }
        
        public void initValue(List<CharactorValue> listLeft, string value, string key)
        {
            items = listLeft;

            this.listBox_left.ValueMember = key;
            this.listBox_left.DisplayMember = value;

            this.listBox_right.ValueMember = key;
            this.listBox_right.DisplayMember = value;

            dics.Clear();

            foreach (CharactorValue cv in listLeft)
                dics.Add(cv.Id, cv);
            
            allToLeft();
        }

        public void allToLeft() {
            this.listBox_left.Items.Clear();
            this.listBox_right.Items.Clear();

            if (items != null && items.Count > 0)
                foreach (CharactorValue c in items)
                    this.listBox_left.Items.Add(c);
            
        }

        private void button_select_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            ListBox listFrom = listBox_right, listTo = listBox_left;
            if (button.Name.Equals("button_right")) {
                listFrom = listBox_left;
                listTo = listBox_right;
            }

            for (int i = 0; i < listFrom.SelectedItems.Count; i++)
            {
                CharactorValue menu = (CharactorValue)listFrom.SelectedItems[i];
                listTo.Items.Add(menu);
            }
            for (int i = listFrom.SelectedItems.Count - 1; i >= 0; i--)
            {
                CharactorValue menu = (CharactorValue)listFrom.SelectedItems[i];
                listFrom.Items.Remove(menu);
            }

            if (selectValueChanged != null)
                selectValueChanged.Invoke();
        }

        private void listBox_left_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.listBox_right.Items.Add(listBox_left.SelectedItem);
            this.listBox_left.Items.Remove(listBox_left.SelectedItem);
            if (selectValueChanged != null)
                selectValueChanged.Invoke();
        }

        private void listBox_right_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.listBox_left.Items.Add(listBox_right.SelectedItem);
            this.listBox_right.Items.Remove(listBox_right.SelectedItem);
            if (selectValueChanged != null)
                selectValueChanged.Invoke();
        }

        private void button_all_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            ListBox listFrom = listBox_right, listTo = listBox_left;
            if (button.Name.Equals("button_all_right"))
            {
                listFrom = listBox_left;
                listTo = listBox_right;
            }

            for (int i = 0; i < listFrom.Items.Count; i++)
            {
                CharactorValue menu = (CharactorValue)listFrom.Items[i];
                listTo.Items.Add(menu);
            }
            for (int i = listFrom.Items.Count - 1; i >= 0; i--)
            {
                CharactorValue menu = (CharactorValue)listFrom.Items[i];
                listFrom.Items.Remove(menu);
            }
            if (selectValueChanged != null)
                selectValueChanged.Invoke();
        }

        public List<CharactorValue> getListRight() {
            List<CharactorValue> results = new List<CharactorValue>();
            foreach (CharactorValue c in listBox_right.Items)
                results.Add(c);
            return results;
        }

        public void setSelectItems(List<ProductAttribute> attrs) {
            
            foreach (ProductAttribute id in attrs) { 
                this.listBox_left.Items.Remove(dics[id.CharactorValueId]);
                this.listBox_right.Items.Add(dics[id.CharactorValueId]);
            }
        }

        public void setSelectItems(List<CharactorValue> cvs)
        {
            foreach (CharactorValue cv in cvs)
            {
                CharactorValue value = getValue(cv.Id);
                if (value != null)
                {
                    this.listBox_left.Items.Remove(value);
                    this.listBox_right.Items.Add(value);
                }
            }
        }

        private CharactorValue getValue(int id) {
            if (dics.ContainsKey(id))
                return dics[id];
            else
                return null;
        }
    }
}
