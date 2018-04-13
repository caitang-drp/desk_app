using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using LocalERP.DataAccess.DataDAO;
using LocalERP.DataAccess.Data;
using System.Collections;
using LocalERP.DataAccess.Utility;

namespace LocalERP.WinForm
{
    public partial class QueryProductDetailForm : MyDockContent
    {
        public QueryProductDetailForm()
        {
            InitializeComponent();
            initDataGridView();
        }

        protected virtual void initDataGridView() { }

        protected void QueryProductDetailForm_Load(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            this.dateTimePicker3.Value = dateTime.AddMonths(-1);

            initList();
        }

        protected virtual void initList()
        {
        }

        /// <summary>
        /// event
        /// </summary>

        public override void refresh()
        {
            //this.initList();
            this.label_notice.Visible = true;
        }

        protected void button1_Click(object sender, EventArgs e)
        {
            initList();
            this.label_notice.Visible = false;
        }

        protected void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            e.Handled = true;
            e.PaintBackground(e.CellBounds, true);
            e.PaintContent(e.CellBounds);
        }
    }
}