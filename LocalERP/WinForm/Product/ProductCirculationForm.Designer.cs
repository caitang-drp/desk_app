using LocalERP.UiDataProxy;
using LocalERP.DataAccess.Utility;
namespace LocalERP.WinForm
{
    partial class ProductCirculationForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductCirculationForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTime_sellTime = new System.Windows.Forms.DateTimePicker();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridView1 = new LocalERP.WinForm.MyDataGridView();
            this.textBox_comment = new System.Windows.Forms.TextBox();
            this.textBox_serial = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label_status = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.textBox_cutoff = new System.Windows.Forms.TextBox();
            this.textBox_backFreightPerPiece = new System.Windows.Forms.TextBox();
            this.textBox_thisPayed = new System.Windows.Forms.TextBox();
            this.textBox_realTotal = new System.Windows.Forms.TextBox();
            this.textBox_previousArrears = new System.Windows.Forms.TextBox();
            this.textBox_freight = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label_customer = new System.Windows.Forms.Label();
            this.sellDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label_operator = new System.Windows.Forms.Label();
            this.label_title = new System.Windows.Forms.Label();
            this.textBox_operator = new System.Windows.Forms.TextBox();
            this.panel_pay = new System.Windows.Forms.Panel();
            this.panel_lastPayReceipt = new System.Windows.Forms.Panel();
            this.label22 = new System.Windows.Forms.Label();
            this.label_lastPayReceipt = new System.Windows.Forms.Label();
            this.panel_payBackFreight = new System.Windows.Forms.Panel();
            this.label_totalBackFreight = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label_totalPieces = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label_1 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.panel_payBasic = new System.Windows.Forms.Panel();
            this.label_accCap = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.textBox_accumulative = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label_thisPayed = new System.Windows.Forms.Label();
            this.label1_accumulative = new System.Windows.Forms.Label();
            this.label_sum = new System.Windows.Forms.Label();
            this.label_arrears = new System.Windows.Forms.Label();
            this.label1_tip = new System.Windows.Forms.Label();
            this.panel_basic = new System.Windows.Forms.Panel();
            this.lookupText1 = new LocalERP.WinForm.LookupText();
            this.button_del = new System.Windows.Forms.Button();
            this.button_add = new System.Windows.Forms.Button();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton_save = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_approval = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_finish = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_finishCancel = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_print = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_printLetter = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sellDataSetBindingSource)).BeginInit();
            this.panel_pay.SuspendLayout();
            this.panel_lastPayReceipt.SuspendLayout();
            this.panel_payBackFreight.SuspendLayout();
            this.panel_payBasic.SuspendLayout();
            this.panel_basic.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "备    注:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(272, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "日    期:";
            // 
            // dateTime_sellTime
            // 
            this.dateTime_sellTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dateTime_sellTime.Enabled = false;
            this.dateTime_sellTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTime_sellTime.Location = new System.Drawing.Point(345, 38);
            this.dateTime_sellTime.Margin = new System.Windows.Forms.Padding(2);
            this.dateTime_sellTime.Name = "dateTime_sellTime";
            this.dateTime_sellTime.Size = new System.Drawing.Size(173, 23);
            this.dateTime_sellTime.TabIndex = 23;
            this.dateTime_sellTime.ValueChanged += new System.EventHandler(this.Controls_TextChanged);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.AllowUserToResizeRows = false;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.PaleGreen;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridView2.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView2.BackgroundColor = System.Drawing.Color.Yellow;
            this.dataGridView2.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.ColumnHeadersVisible = false;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.dataGridView2.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridView2.Enabled = false;
            this.dataGridView2.EnableHeadersVisualStyles = false;
            this.dataGridView2.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dataGridView2.Location = new System.Drawing.Point(18, 388);
            this.dataGridView2.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.RowHeadersVisible = false;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.Yellow;
            this.dataGridView2.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridView2.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.dataGridView2.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.White;
            this.dataGridView2.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridView2.RowTemplate.Height = 23;
            this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView2.Size = new System.Drawing.Size(839, 22);
            this.dataGridView2.TabIndex = 7;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Column2";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.PaleGreen;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridView1.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dataGridView1.IsLastRowSort = false;
            this.dataGridView1.Location = new System.Drawing.Point(18, 203);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridView1.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.White;
            this.dataGridView1.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView1.Size = new System.Drawing.Size(839, 186);
            this.dataGridView1.TabIndex = 33;
            this.dataGridView1.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView1_EditingControlShowing);
            // 
            // textBox_comment
            // 
            this.textBox_comment.Location = new System.Drawing.Point(78, 39);
            this.textBox_comment.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_comment.Name = "textBox_comment";
            this.textBox_comment.Size = new System.Drawing.Size(154, 23);
            this.textBox_comment.TabIndex = 22;
            this.textBox_comment.TextChanged += new System.EventHandler(this.Controls_TextChanged);
            // 
            // textBox_serial
            // 
            this.textBox_serial.Location = new System.Drawing.Point(345, 6);
            this.textBox_serial.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_serial.Name = "textBox_serial";
            this.textBox_serial.Size = new System.Drawing.Size(173, 23);
            this.textBox_serial.TabIndex = 20;
            this.textBox_serial.TextChanged += new System.EventHandler(this.Controls_TextChanged);
            this.textBox_serial.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_serial_Validating);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(272, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 14);
            this.label3.TabIndex = 19;
            this.label3.Text = "单据编号:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 14);
            this.label4.TabIndex = 21;
            this.label4.Text = "单据状态:";
            // 
            // label_status
            // 
            this.label_status.AutoSize = true;
            this.label_status.Font = new System.Drawing.Font("宋体", 10F);
            this.label_status.ForeColor = System.Drawing.Color.Red;
            this.label_status.Location = new System.Drawing.Point(75, 11);
            this.label_status.Name = "label_status";
            this.label_status.Size = new System.Drawing.Size(35, 14);
            this.label_status.TabIndex = 22;
            this.label_status.Text = "新增";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // textBox_cutoff
            // 
            this.textBox_cutoff.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.errorProvider1.SetIconPadding(this.textBox_cutoff, 20);
            this.textBox_cutoff.Location = new System.Drawing.Point(77, 9);
            this.textBox_cutoff.Name = "textBox_cutoff";
            this.textBox_cutoff.Size = new System.Drawing.Size(97, 23);
            this.textBox_cutoff.TabIndex = 40;
            this.textBox_cutoff.Text = "100";
            this.textBox_cutoff.TextChanged += new System.EventHandler(this.Controls_TextChanged);
            // 
            // textBox_backFreightPerPiece
            // 
            this.textBox_backFreightPerPiece.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.errorProvider1.SetIconPadding(this.textBox_backFreightPerPiece, 20);
            this.textBox_backFreightPerPiece.Location = new System.Drawing.Point(90, 3);
            this.textBox_backFreightPerPiece.Name = "textBox_backFreightPerPiece";
            this.textBox_backFreightPerPiece.Size = new System.Drawing.Size(97, 23);
            this.textBox_backFreightPerPiece.TabIndex = 77;
            this.textBox_backFreightPerPiece.TextChanged += new System.EventHandler(this.Controls_TextChanged);
            // 
            // textBox_thisPayed
            // 
            this.textBox_thisPayed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.errorProvider1.SetIconPadding(this.textBox_thisPayed, 20);
            this.textBox_thisPayed.Location = new System.Drawing.Point(77, 40);
            this.textBox_thisPayed.Name = "textBox_thisPayed";
            this.textBox_thisPayed.Size = new System.Drawing.Size(97, 23);
            this.textBox_thisPayed.TabIndex = 42;
            this.textBox_thisPayed.TextChanged += new System.EventHandler(this.Controls_TextChanged);
            // 
            // textBox_realTotal
            // 
            this.textBox_realTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.errorProvider1.SetIconPadding(this.textBox_realTotal, 20);
            this.textBox_realTotal.Location = new System.Drawing.Point(330, 9);
            this.textBox_realTotal.Name = "textBox_realTotal";
            this.textBox_realTotal.Size = new System.Drawing.Size(97, 23);
            this.textBox_realTotal.TabIndex = 41;
            this.textBox_realTotal.TextChanged += new System.EventHandler(this.Controls_TextChanged);
            // 
            // textBox_previousArrears
            // 
            this.textBox_previousArrears.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox_previousArrears.Enabled = false;
            this.errorProvider1.SetIconPadding(this.textBox_previousArrears, 20);
            this.textBox_previousArrears.Location = new System.Drawing.Point(330, 40);
            this.textBox_previousArrears.Name = "textBox_previousArrears";
            this.textBox_previousArrears.Size = new System.Drawing.Size(98, 23);
            this.textBox_previousArrears.TabIndex = 43;
            // 
            // textBox_freight
            // 
            this.textBox_freight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.errorProvider1.SetIconPadding(this.textBox_freight, 20);
            this.textBox_freight.Location = new System.Drawing.Point(583, 9);
            this.textBox_freight.Name = "textBox_freight";
            this.textBox_freight.Size = new System.Drawing.Size(97, 23);
            this.textBox_freight.TabIndex = 68;
            this.textBox_freight.TextChanged += new System.EventHandler(this.Controls_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(259, 11);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(14, 14);
            this.label5.TabIndex = 34;
            this.label5.Text = "*";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(16, 187);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 14);
            this.label8.TabIndex = 38;
            this.label8.Text = "单据明细:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(576, 11);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(14, 14);
            this.label9.TabIndex = 43;
            this.label9.Text = "*";
            // 
            // label_customer
            // 
            this.label_customer.AutoSize = true;
            this.label_customer.Location = new System.Drawing.Point(589, 11);
            this.label_customer.Name = "label_customer";
            this.label_customer.Size = new System.Drawing.Size(70, 14);
            this.label_customer.TabIndex = 41;
            this.label_customer.Text = "客    户:";
            // 
            // label_operator
            // 
            this.label_operator.AutoSize = true;
            this.label_operator.Location = new System.Drawing.Point(589, 42);
            this.label_operator.Name = "label_operator";
            this.label_operator.Size = new System.Drawing.Size(70, 14);
            this.label_operator.TabIndex = 45;
            this.label_operator.Text = "经 手 人:";
            // 
            // label_title
            // 
            this.label_title.AutoSize = true;
            this.label_title.Font = new System.Drawing.Font("宋体", 16F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.label_title.Location = new System.Drawing.Point(367, 52);
            this.label_title.Name = "label_title";
            this.label_title.Size = new System.Drawing.Size(125, 22);
            this.label_title.TabIndex = 51;
            this.label_title.Text = "产品流转单";
            // 
            // textBox_operator
            // 
            this.textBox_operator.Location = new System.Drawing.Point(662, 38);
            this.textBox_operator.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_operator.Name = "textBox_operator";
            this.textBox_operator.Size = new System.Drawing.Size(153, 23);
            this.textBox_operator.TabIndex = 24;
            this.textBox_operator.TextChanged += new System.EventHandler(this.Controls_TextChanged);
            // 
            // panel_pay
            // 
            this.panel_pay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_pay.Controls.Add(this.panel_lastPayReceipt);
            this.panel_pay.Controls.Add(this.panel_payBackFreight);
            this.panel_pay.Controls.Add(this.panel_payBasic);
            this.panel_pay.Location = new System.Drawing.Point(9, 415);
            this.panel_pay.Name = "panel_pay";
            this.panel_pay.Size = new System.Drawing.Size(870, 101);
            this.panel_pay.TabIndex = 58;
            // 
            // panel_lastPayReceipt
            // 
            this.panel_lastPayReceipt.Controls.Add(this.label22);
            this.panel_lastPayReceipt.Controls.Add(this.label_lastPayReceipt);
            this.panel_lastPayReceipt.Location = new System.Drawing.Point(466, 6);
            this.panel_lastPayReceipt.Name = "panel_lastPayReceipt";
            this.panel_lastPayReceipt.Size = new System.Drawing.Size(392, 24);
            this.panel_lastPayReceipt.TabIndex = 89;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(3, 4);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(112, 14);
            this.label22.TabIndex = 88;
            this.label22.Text = "上次收付款信息:";
            // 
            // label_lastPayReceipt
            // 
            this.label_lastPayReceipt.AutoSize = true;
            this.label_lastPayReceipt.Location = new System.Drawing.Point(121, 4);
            this.label_lastPayReceipt.Name = "label_lastPayReceipt";
            this.label_lastPayReceipt.Size = new System.Drawing.Size(0, 14);
            this.label_lastPayReceipt.TabIndex = 87;
            // 
            // panel_payBackFreight
            // 
            this.panel_payBackFreight.Controls.Add(this.label_totalBackFreight);
            this.panel_payBackFreight.Controls.Add(this.textBox_backFreightPerPiece);
            this.panel_payBackFreight.Controls.Add(this.label21);
            this.panel_payBackFreight.Controls.Add(this.label_totalPieces);
            this.panel_payBackFreight.Controls.Add(this.label20);
            this.panel_payBackFreight.Controls.Add(this.label_1);
            this.panel_payBackFreight.Controls.Add(this.label14);
            this.panel_payBackFreight.Controls.Add(this.label19);
            this.panel_payBackFreight.Location = new System.Drawing.Point(4, 4);
            this.panel_payBackFreight.Name = "panel_payBackFreight";
            this.panel_payBackFreight.Size = new System.Drawing.Size(456, 27);
            this.panel_payBackFreight.TabIndex = 86;
            // 
            // label_totalBackFreight
            // 
            this.label_totalBackFreight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_totalBackFreight.AutoSize = true;
            this.label_totalBackFreight.Location = new System.Drawing.Point(376, 7);
            this.label_totalBackFreight.Name = "label_totalBackFreight";
            this.label_totalBackFreight.Size = new System.Drawing.Size(0, 14);
            this.label_totalBackFreight.TabIndex = 84;
            // 
            // label21
            // 
            this.label21.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(412, 7);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(28, 14);
            this.label21.TabIndex = 83;
            this.label21.Text = "元)";
            // 
            // label_totalPieces
            // 
            this.label_totalPieces.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_totalPieces.AutoSize = true;
            this.label_totalPieces.Location = new System.Drawing.Point(279, 7);
            this.label_totalPieces.Name = "label_totalPieces";
            this.label_totalPieces.Size = new System.Drawing.Size(0, 14);
            this.label_totalPieces.TabIndex = 82;
            // 
            // label20
            // 
            this.label20.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(316, 7);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(56, 14);
            this.label20.TabIndex = 81;
            this.label20.Text = "总退费:";
            // 
            // label_1
            // 
            this.label_1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_1.AutoSize = true;
            this.label_1.Location = new System.Drawing.Point(210, 7);
            this.label_1.Name = "label_1";
            this.label_1.Size = new System.Drawing.Size(63, 14);
            this.label_1.TabIndex = 80;
            this.label_1.Text = "(总件数:";
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(189, 7);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(21, 14);
            this.label14.TabIndex = 79;
            this.label14.Text = "元";
            // 
            // label19
            // 
            this.label19.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label19.AutoSize = true;
            this.label19.ForeColor = System.Drawing.Color.Black;
            this.label19.Location = new System.Drawing.Point(5, 7);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(84, 14);
            this.label19.TabIndex = 78;
            this.label19.Text = "每件退运费:";
            // 
            // panel_payBasic
            // 
            this.panel_payBasic.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_payBasic.Controls.Add(this.label_accCap);
            this.panel_payBasic.Controls.Add(this.label12);
            this.panel_payBasic.Controls.Add(this.textBox_freight);
            this.panel_payBasic.Controls.Add(this.label15);
            this.panel_payBasic.Controls.Add(this.label18);
            this.panel_payBasic.Controls.Add(this.label16);
            this.panel_payBasic.Controls.Add(this.textBox_previousArrears);
            this.panel_payBasic.Controls.Add(this.label17);
            this.panel_payBasic.Controls.Add(this.label10);
            this.panel_payBasic.Controls.Add(this.textBox_cutoff);
            this.panel_payBasic.Controls.Add(this.label11);
            this.panel_payBasic.Controls.Add(this.textBox_accumulative);
            this.panel_payBasic.Controls.Add(this.label13);
            this.panel_payBasic.Controls.Add(this.label7);
            this.panel_payBasic.Controls.Add(this.label6);
            this.panel_payBasic.Controls.Add(this.textBox_realTotal);
            this.panel_payBasic.Controls.Add(this.textBox_thisPayed);
            this.panel_payBasic.Controls.Add(this.label_thisPayed);
            this.panel_payBasic.Controls.Add(this.label1_accumulative);
            this.panel_payBasic.Controls.Add(this.label_sum);
            this.panel_payBasic.Controls.Add(this.label_arrears);
            this.panel_payBasic.Location = new System.Drawing.Point(17, 33);
            this.panel_payBasic.Name = "panel_payBasic";
            this.panel_payBasic.Size = new System.Drawing.Size(835, 66);
            this.panel_payBasic.TabIndex = 85;
            // 
            // label_accCap
            // 
            this.label_accCap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_accCap.AutoSize = true;
            this.label_accCap.Location = new System.Drawing.Point(709, 44);
            this.label_accCap.Name = "label_accCap";
            this.label_accCap.Size = new System.Drawing.Size(0, 14);
            this.label_accCap.TabIndex = 76;
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(683, 13);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(21, 14);
            this.label12.TabIndex = 70;
            this.label12.Text = "元";
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label15.AutoSize = true;
            this.label15.ForeColor = System.Drawing.Color.Black;
            this.label15.Location = new System.Drawing.Point(510, 13);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(70, 14);
            this.label15.TabIndex = 69;
            this.label15.Text = "运费支出:";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.ForeColor = System.Drawing.Color.Red;
            this.label18.Location = new System.Drawing.Point(216, 13);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(14, 14);
            this.label18.TabIndex = 67;
            this.label18.Text = "*";
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(430, 44);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(21, 14);
            this.label16.TabIndex = 66;
            this.label16.Text = "元";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("宋体", 10F);
            this.label17.ForeColor = System.Drawing.Color.Red;
            this.label17.Location = new System.Drawing.Point(699, 13);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(105, 14);
            this.label17.TabIndex = 71;
            this.label17.Text = "(计入其他支出)";
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(430, 13);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(21, 14);
            this.label10.TabIndex = 63;
            this.label10.Text = "元";
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(177, 13);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(14, 14);
            this.label11.TabIndex = 62;
            this.label11.Text = "%";
            // 
            // textBox_accumulative
            // 
            this.textBox_accumulative.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox_accumulative.Enabled = false;
            this.textBox_accumulative.Location = new System.Drawing.Point(584, 40);
            this.textBox_accumulative.Name = "textBox_accumulative";
            this.textBox_accumulative.Size = new System.Drawing.Size(97, 23);
            this.textBox_accumulative.TabIndex = 44;
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(5, 13);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(70, 14);
            this.label13.TabIndex = 59;
            this.label13.Text = "整单折扣:";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(683, 44);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(21, 14);
            this.label7.TabIndex = 56;
            this.label7.Text = "元";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(177, 44);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(21, 14);
            this.label6.TabIndex = 55;
            this.label6.Text = "元";
            // 
            // label_thisPayed
            // 
            this.label_thisPayed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_thisPayed.AutoSize = true;
            this.label_thisPayed.ForeColor = System.Drawing.Color.Red;
            this.label_thisPayed.Location = new System.Drawing.Point(5, 44);
            this.label_thisPayed.Name = "label_thisPayed";
            this.label_thisPayed.Size = new System.Drawing.Size(70, 14);
            this.label_thisPayed.TabIndex = 49;
            this.label_thisPayed.Text = "本单已付:";
            // 
            // label1_accumulative
            // 
            this.label1_accumulative.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1_accumulative.AutoSize = true;
            this.label1_accumulative.ForeColor = System.Drawing.Color.Black;
            this.label1_accumulative.Location = new System.Drawing.Point(468, 44);
            this.label1_accumulative.Name = "label1_accumulative";
            this.label1_accumulative.Size = new System.Drawing.Size(112, 14);
            this.label1_accumulative.TabIndex = 46;
            this.label1_accumulative.Text = "累计欠款(应付):";
            // 
            // label_sum
            // 
            this.label_sum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_sum.AutoSize = true;
            this.label_sum.ForeColor = System.Drawing.Color.Black;
            this.label_sum.Location = new System.Drawing.Point(229, 13);
            this.label_sum.Name = "label_sum";
            this.label_sum.Size = new System.Drawing.Size(98, 14);
            this.label_sum.TabIndex = 58;
            this.label_sum.Text = "本单实计应付:";
            // 
            // label_arrears
            // 
            this.label_arrears.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_arrears.AutoSize = true;
            this.label_arrears.Location = new System.Drawing.Point(216, 44);
            this.label_arrears.Name = "label_arrears";
            this.label_arrears.Size = new System.Drawing.Size(112, 14);
            this.label_arrears.TabIndex = 64;
            this.label_arrears.Text = "以上欠款(应付):";
            // 
            // label1_tip
            // 
            this.label1_tip.AutoSize = true;
            this.label1_tip.ForeColor = System.Drawing.Color.Red;
            this.label1_tip.Location = new System.Drawing.Point(213, 185);
            this.label1_tip.Name = "label1_tip";
            this.label1_tip.Size = new System.Drawing.Size(0, 14);
            this.label1_tip.TabIndex = 59;
            // 
            // panel_basic
            // 
            this.panel_basic.Controls.Add(this.textBox_operator);
            this.panel_basic.Controls.Add(this.lookupText1);
            this.panel_basic.Controls.Add(this.label_operator);
            this.panel_basic.Controls.Add(this.label9);
            this.panel_basic.Controls.Add(this.label_customer);
            this.panel_basic.Controls.Add(this.label5);
            this.panel_basic.Controls.Add(this.label_status);
            this.panel_basic.Controls.Add(this.label4);
            this.panel_basic.Controls.Add(this.textBox_serial);
            this.panel_basic.Controls.Add(this.label3);
            this.panel_basic.Controls.Add(this.textBox_comment);
            this.panel_basic.Controls.Add(this.dateTime_sellTime);
            this.panel_basic.Controls.Add(this.label2);
            this.panel_basic.Controls.Add(this.label1);
            this.panel_basic.Location = new System.Drawing.Point(10, 96);
            this.panel_basic.Name = "panel_basic";
            this.panel_basic.Size = new System.Drawing.Size(847, 83);
            this.panel_basic.TabIndex = 60;
            // 
            // lookupText1
            // 
            this.lookupText1.BackColor = System.Drawing.Color.Transparent;
            this.lookupText1.Location = new System.Drawing.Point(662, 7);
            this.lookupText1.LookupForm = null;
            this.lookupText1.LookupFormType = null;
            this.lookupText1.Name = "lookupText1";
            this.lookupText1.SelectButtonBackGround = global::LocalERP.Properties.Resources.user16;
            this.lookupText1.Size = new System.Drawing.Size(153, 24);
            this.lookupText1.TabIndex = 21;
            this.lookupText1.Text_Lookup = "单击选择...";
            this.lookupText1.Value_Lookup = null;
            this.lookupText1.valueSetted += new LocalERP.WinForm.LookupText.ValueSetted(this.lookupText1_valueSetted);
            // 
            // button_del
            // 
            this.button_del.Image = global::LocalERP.Properties.Resources.del16;
            this.button_del.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_del.Location = new System.Drawing.Point(147, 179);
            this.button_del.Name = "button_del";
            this.button_del.Size = new System.Drawing.Size(60, 24);
            this.button_del.TabIndex = 32;
            this.button_del.Text = "删除";
            this.button_del.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_del.UseVisualStyleBackColor = true;
            this.button_del.Click += new System.EventHandler(this.button_del_Click);
            // 
            // button_add
            // 
            this.button_add.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button_add.Image = global::LocalERP.Properties.Resources.add16;
            this.button_add.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_add.Location = new System.Drawing.Point(88, 179);
            this.button_add.Name = "button_add";
            this.button_add.Size = new System.Drawing.Size(60, 24);
            this.button_add.TabIndex = 31;
            this.button_add.Text = "添加";
            this.button_add.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_add.UseVisualStyleBackColor = true;
            this.button_add.Click += new System.EventHandler(this.button_add_Click);
            // 
            // toolStrip2
            // 
            this.toolStrip2.BackgroundImage = global::LocalERP.Properties.Resources.toolBack;
            this.toolStrip2.Font = new System.Drawing.Font("宋体", 10F);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton_save,
            this.toolStripButton_approval,
            this.toolStripButton_finish,
            this.toolStripButton_finishCancel,
            this.toolStripButton_print,
            this.toolStripButton_printLetter,
            this.toolStripButton1});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(879, 37);
            this.toolStrip2.TabIndex = 10;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripButton_save
            // 
            this.toolStripButton_save.Image = global::LocalERP.Properties.Resources.save16;
            this.toolStripButton_save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_save.Name = "toolStripButton_save";
            this.toolStripButton_save.Size = new System.Drawing.Size(39, 34);
            this.toolStripButton_save.Text = "保存";
            this.toolStripButton_save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton_save.Click += new System.EventHandler(this.toolStripButton_save_Click);
            // 
            // toolStripButton_approval
            // 
            this.toolStripButton_approval.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_approval.Image")));
            this.toolStripButton_approval.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_approval.Name = "toolStripButton_approval";
            this.toolStripButton_approval.Size = new System.Drawing.Size(53, 34);
            this.toolStripButton_approval.Text = "已下单";
            this.toolStripButton_approval.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolStripButton_approval.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton_approval.Visible = false;
            // 
            // toolStripButton_finish
            // 
            this.toolStripButton_finish.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_finish.Image")));
            this.toolStripButton_finish.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_finish.Name = "toolStripButton_finish";
            this.toolStripButton_finish.Size = new System.Drawing.Size(39, 34);
            this.toolStripButton_finish.Text = "审核";
            this.toolStripButton_finish.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton_finish.Click += new System.EventHandler(this.toolStripButton_finish_Click);
            // 
            // toolStripButton_finishCancel
            // 
            this.toolStripButton_finishCancel.Image = global::LocalERP.Properties.Resources.del161;
            this.toolStripButton_finishCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_finishCancel.Name = "toolStripButton_finishCancel";
            this.toolStripButton_finishCancel.Size = new System.Drawing.Size(39, 34);
            this.toolStripButton_finishCancel.Text = "弃核";
            this.toolStripButton_finishCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton_finishCancel.Click += new System.EventHandler(this.toolStripButton_finishCancel_Click);
            // 
            // toolStripButton_print
            // 
            this.toolStripButton_print.Image = global::LocalERP.Properties.Resources.print16;
            this.toolStripButton_print.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_print.Name = "toolStripButton_print";
            this.toolStripButton_print.Size = new System.Drawing.Size(39, 34);
            this.toolStripButton_print.Text = "打印";
            this.toolStripButton_print.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton_print.Click += new System.EventHandler(this.toolStripButton_print_Click);
            // 
            // toolStripButton_printLetter
            // 
            this.toolStripButton_printLetter.Image = global::LocalERP.Properties.Resources.print16;
            this.toolStripButton_printLetter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_printLetter.Name = "toolStripButton_printLetter";
            this.toolStripButton_printLetter.Size = new System.Drawing.Size(67, 34);
            this.toolStripButton_printLetter.Text = "打印封面";
            this.toolStripButton_printLetter.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton_printLetter.Click += new System.EventHandler(this.toolStripButton_printLetter_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = global::LocalERP.Properties.Resources.cancel16;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(39, 34);
            this.toolStripButton1.Text = "取消";
            this.toolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton_cancel_Click);
            // 
            // ProductCirculationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(879, 522);
            this.Controls.Add(this.panel_basic);
            this.Controls.Add(this.label1_tip);
            this.Controls.Add(this.panel_pay);
            this.Controls.Add(this.label_title);
            this.Controls.Add(this.button_del);
            this.Controls.Add(this.button_add);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.toolStrip2);
            this.Font = new System.Drawing.Font("宋体", 10F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ProductCirculationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "产品流转单";
            this.Load += new System.EventHandler(this.ProductCirculationForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sellDataSetBindingSource)).EndInit();
            this.panel_pay.ResumeLayout(false);
            this.panel_lastPayReceipt.ResumeLayout(false);
            this.panel_lastPayReceipt.PerformLayout();
            this.panel_payBackFreight.ResumeLayout(false);
            this.panel_payBackFreight.PerformLayout();
            this.panel_payBasic.ResumeLayout(false);
            this.panel_payBasic.PerformLayout();
            this.panel_basic.ResumeLayout(false);
            this.panel_basic.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTime_sellTime;
        protected MyDataGridView dataGridView1;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton toolStripButton_save;
        private System.Windows.Forms.ToolStripButton toolStripButton_finish;
        private System.Windows.Forms.ToolStripButton toolStripButton_print;
        private System.Windows.Forms.TextBox textBox_comment;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.TextBox textBox_serial;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label_status;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolStripButton toolStripButton_approval;
        private System.Windows.Forms.Button button_del;
        private System.Windows.Forms.Button button_add;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label_customer;
        private System.Windows.Forms.BindingSource sellDataSetBindingSource;
        private System.Windows.Forms.Label label_operator;
        protected LookupText lookupText1;
        private System.Windows.Forms.Label label_title;
        private System.Windows.Forms.TextBox textBox_operator;
        private System.Windows.Forms.Panel panel_pay;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.TextBox textBox_cutoff;
        protected System.Windows.Forms.Label label1_tip;
        private System.Windows.Forms.Panel panel_basic;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label_1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox textBox_backFreightPerPiece;
        private System.Windows.Forms.Label label_totalPieces;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label_totalBackFreight;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ToolStripButton toolStripButton_finishCancel;
        private System.Windows.Forms.ToolStripButton toolStripButton_printLetter;
        private System.Windows.Forms.Panel panel_payBasic;
        private System.Windows.Forms.Label label_accCap;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBox_freight;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox textBox_previousArrears;
        private System.Windows.Forms.Label label_arrears;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBox_accumulative;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label_sum;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_realTotal;
        private System.Windows.Forms.TextBox textBox_thisPayed;
        private System.Windows.Forms.Label label_thisPayed;
        private System.Windows.Forms.Label label1_accumulative;
        private System.Windows.Forms.Panel panel_payBackFreight;
        private System.Windows.Forms.Label label_lastPayReceipt;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Panel panel_lastPayReceipt;
        
       
    }
}