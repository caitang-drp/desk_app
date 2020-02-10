using LocalERP.UiDataProxy;
using LocalERP.DataAccess.Utility;
namespace LocalERP.WinForm
{
    partial class CardForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CardForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTime_cardTime = new System.Windows.Forms.DateTimePicker();
            this.textBox_comment = new System.Windows.Forms.TextBox();
            this.textBox_serial = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label_status = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.textBox_realTotal = new System.Windows.Forms.TextBox();
            this.textBox_num = new System.Windows.Forms.TextBox();
            this.textBox_leftNum = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label_customer = new System.Windows.Forms.Label();
            this.sellDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label_operator = new System.Windows.Forms.Label();
            this.label_title = new System.Windows.Forms.Label();
            this.textBox_operator = new System.Windows.Forms.TextBox();
            this.label1_tip = new System.Windows.Forms.Label();
            this.panel_basic = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label_sum = new System.Windows.Forms.Label();
            this.lookupText1 = new LocalERP.WinForm.LookupText();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton_save = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_finish = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_finishCancel = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.dataGridView1 = new LocalERP.WinForm.MyDataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.customer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sellTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.comment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sellDataSetBindingSource)).BeginInit();
            this.panel_basic.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "备    注:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(583, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "开卡日期:";
            // 
            // dateTime_cardTime
            // 
            this.dateTime_cardTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dateTime_cardTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTime_cardTime.Location = new System.Drawing.Point(656, 6);
            this.dateTime_cardTime.Margin = new System.Windows.Forms.Padding(2);
            this.dateTime_cardTime.Name = "dateTime_cardTime";
            this.dateTime_cardTime.Size = new System.Drawing.Size(182, 23);
            this.dateTime_cardTime.TabIndex = 23;
            this.dateTime_cardTime.ValueChanged += new System.EventHandler(this.Controls_TextChanged);
            // 
            // textBox_comment
            // 
            this.textBox_comment.Location = new System.Drawing.Point(92, 82);
            this.textBox_comment.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_comment.Name = "textBox_comment";
            this.textBox_comment.Size = new System.Drawing.Size(151, 23);
            this.textBox_comment.TabIndex = 22;
            this.textBox_comment.TextChanged += new System.EventHandler(this.Controls_TextChanged);
            // 
            // textBox_serial
            // 
            this.textBox_serial.Enabled = false;
            this.textBox_serial.Location = new System.Drawing.Point(372, 6);
            this.textBox_serial.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_serial.Name = "textBox_serial";
            this.textBox_serial.Size = new System.Drawing.Size(173, 23);
            this.textBox_serial.TabIndex = 20;
            this.textBox_serial.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_serial_Validating);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(299, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 14);
            this.label3.TabIndex = 19;
            this.label3.Text = "卡片编号:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 14);
            this.label4.TabIndex = 21;
            this.label4.Text = "卡片状态:";
            // 
            // label_status
            // 
            this.label_status.AutoSize = true;
            this.label_status.Font = new System.Drawing.Font("宋体", 10F);
            this.label_status.ForeColor = System.Drawing.Color.Red;
            this.label_status.Location = new System.Drawing.Point(89, 11);
            this.label_status.Name = "label_status";
            this.label_status.Size = new System.Drawing.Size(35, 14);
            this.label_status.TabIndex = 22;
            this.label_status.Text = "新增";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // textBox_realTotal
            // 
            this.textBox_realTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.errorProvider1.SetIconPadding(this.textBox_realTotal, 20);
            this.textBox_realTotal.Location = new System.Drawing.Point(92, 47);
            this.textBox_realTotal.Name = "textBox_realTotal";
            this.textBox_realTotal.Size = new System.Drawing.Size(151, 23);
            this.textBox_realTotal.TabIndex = 68;
            this.textBox_realTotal.TextChanged += new System.EventHandler(this.Controls_TextChanged);
            // 
            // textBox_num
            // 
            this.textBox_num.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.errorProvider1.SetIconPadding(this.textBox_num, 20);
            this.textBox_num.Location = new System.Drawing.Point(372, 46);
            this.textBox_num.Name = "textBox_num";
            this.textBox_num.Size = new System.Drawing.Size(173, 23);
            this.textBox_num.TabIndex = 69;
            this.textBox_num.TextChanged += new System.EventHandler(this.Textbox_num_TextChanged);
            // 
            // textBox_leftNum
            // 
            this.textBox_leftNum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox_leftNum.Enabled = false;
            this.errorProvider1.SetIconPadding(this.textBox_leftNum, 20);
            this.textBox_leftNum.Location = new System.Drawing.Point(656, 47);
            this.textBox_leftNum.Name = "textBox_leftNum";
            this.textBox_leftNum.Size = new System.Drawing.Size(182, 23);
            this.textBox_leftNum.TabIndex = 76;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(286, 11);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(14, 14);
            this.label5.TabIndex = 34;
            this.label5.Text = "*";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(29, 237);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 14);
            this.label8.TabIndex = 38;
            this.label8.Text = "消费明细:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(570, 86);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(14, 14);
            this.label9.TabIndex = 43;
            this.label9.Text = "*";
            // 
            // label_customer
            // 
            this.label_customer.AutoSize = true;
            this.label_customer.Location = new System.Drawing.Point(583, 86);
            this.label_customer.Name = "label_customer";
            this.label_customer.Size = new System.Drawing.Size(70, 14);
            this.label_customer.TabIndex = 41;
            this.label_customer.Text = "客    户:";
            // 
            // label_operator
            // 
            this.label_operator.AutoSize = true;
            this.label_operator.Location = new System.Drawing.Point(299, 86);
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
            this.label_title.Text = "卡片信息单";
            // 
            // textBox_operator
            // 
            this.textBox_operator.Location = new System.Drawing.Point(372, 82);
            this.textBox_operator.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_operator.Name = "textBox_operator";
            this.textBox_operator.Size = new System.Drawing.Size(173, 23);
            this.textBox_operator.TabIndex = 24;
            this.textBox_operator.TextChanged += new System.EventHandler(this.Controls_TextChanged);
            // 
            // label1_tip
            // 
            this.label1_tip.AutoSize = true;
            this.label1_tip.ForeColor = System.Drawing.Color.Red;
            this.label1_tip.Location = new System.Drawing.Point(213, 282);
            this.label1_tip.Name = "label1_tip";
            this.label1_tip.Size = new System.Drawing.Size(0, 14);
            this.label1_tip.TabIndex = 59;
            // 
            // panel_basic
            // 
            this.panel_basic.Controls.Add(this.label6);
            this.panel_basic.Controls.Add(this.textBox_leftNum);
            this.panel_basic.Controls.Add(this.label7);
            this.panel_basic.Controls.Add(this.label18);
            this.panel_basic.Controls.Add(this.label10);
            this.panel_basic.Controls.Add(this.label_sum);
            this.panel_basic.Controls.Add(this.lookupText1);
            this.panel_basic.Controls.Add(this.label_customer);
            this.panel_basic.Controls.Add(this.textBox_realTotal);
            this.panel_basic.Controls.Add(this.label9);
            this.panel_basic.Controls.Add(this.textBox_num);
            this.panel_basic.Controls.Add(this.textBox_operator);
            this.panel_basic.Controls.Add(this.label_operator);
            this.panel_basic.Controls.Add(this.label5);
            this.panel_basic.Controls.Add(this.label_status);
            this.panel_basic.Controls.Add(this.label4);
            this.panel_basic.Controls.Add(this.textBox_comment);
            this.panel_basic.Controls.Add(this.label1);
            this.panel_basic.Controls.Add(this.textBox_serial);
            this.panel_basic.Controls.Add(this.label3);
            this.panel_basic.Controls.Add(this.dateTime_cardTime);
            this.panel_basic.Controls.Add(this.label2);
            this.panel_basic.Location = new System.Drawing.Point(10, 96);
            this.panel_basic.Name = "panel_basic";
            this.panel_basic.Size = new System.Drawing.Size(869, 122);
            this.panel_basic.TabIndex = 60;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(583, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 14);
            this.label6.TabIndex = 77;
            this.label6.Text = "剩余次数:";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(299, 49);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 14);
            this.label7.TabIndex = 75;
            this.label7.Text = "卡片次数:";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.ForeColor = System.Drawing.Color.Red;
            this.label18.Location = new System.Drawing.Point(2, 49);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(14, 14);
            this.label18.TabIndex = 74;
            this.label18.Text = "*";
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(247, 52);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(21, 14);
            this.label10.TabIndex = 73;
            this.label10.Text = "元";
            // 
            // label_sum
            // 
            this.label_sum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_sum.AutoSize = true;
            this.label_sum.ForeColor = System.Drawing.Color.Black;
            this.label_sum.Location = new System.Drawing.Point(19, 50);
            this.label_sum.Name = "label_sum";
            this.label_sum.Size = new System.Drawing.Size(70, 14);
            this.label_sum.TabIndex = 72;
            this.label_sum.Text = "卡片价格:";
            // 
            // lookupText1
            // 
            this.lookupText1.BackColor = System.Drawing.Color.Transparent;
            this.lookupText1.Location = new System.Drawing.Point(656, 82);
            this.lookupText1.LookupForm = null;
            this.lookupText1.LookupFormType = null;
            this.lookupText1.Name = "lookupText1";
            this.lookupText1.SelectButtonBackGround = global::LocalERP.Properties.Resources.user16;
            this.lookupText1.Size = new System.Drawing.Size(182, 24);
            this.lookupText1.TabIndex = 21;
            this.lookupText1.Text_Lookup = "单击选择...";
            this.lookupText1.Value_Lookup = null;
            // 
            // toolStrip2
            // 
            this.toolStrip2.BackgroundImage = global::LocalERP.Properties.Resources.toolBack;
            this.toolStrip2.Font = new System.Drawing.Font("宋体", 10F);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton_save,
            this.toolStripButton_finish,
            this.toolStripButton_finishCancel,
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
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.name,
            this.customer,
            this.sellTime,
            this.type,
            this.num,
            this.comment,
            this.status});
            this.dataGridView1.Location = new System.Drawing.Point(32, 255);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(200)))), ((int)(((byte)(79)))));
            this.dataGridView1.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(816, 232);
            this.dataGridView1.TabIndex = 61;
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Width = 40;
            // 
            // name
            // 
            this.name.HeaderText = "编号";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            this.name.Width = 140;
            // 
            // customer
            // 
            this.customer.HeaderText = "客户";
            this.customer.Name = "customer";
            this.customer.ReadOnly = true;
            this.customer.Visible = false;
            // 
            // sellTime
            // 
            this.sellTime.HeaderText = "时间";
            this.sellTime.Name = "sellTime";
            this.sellTime.ReadOnly = true;
            this.sellTime.Width = 200;
            // 
            // type
            // 
            this.type.HeaderText = "类型";
            this.type.Name = "type";
            this.type.ReadOnly = true;
            this.type.Visible = false;
            this.type.Width = 60;
            // 
            // num
            // 
            this.num.HeaderText = "消费次数";
            this.num.Name = "num";
            this.num.ReadOnly = true;
            this.num.Width = 90;
            // 
            // comment
            // 
            this.comment.HeaderText = "备注";
            this.comment.Name = "comment";
            this.comment.Width = 200;
            // 
            // status
            // 
            this.status.HeaderText = "状态";
            this.status.Name = "status";
            this.status.ReadOnly = true;
            this.status.Width = 80;
            // 
            // CardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(879, 522);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel_basic);
            this.Controls.Add(this.label1_tip);
            this.Controls.Add(this.label_title);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.toolStrip2);
            this.Font = new System.Drawing.Font("宋体", 10F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CardForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "卡片信息单";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sellDataSetBindingSource)).EndInit();
            this.panel_basic.ResumeLayout(false);
            this.panel_basic.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTime_cardTime;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton toolStripButton_save;
        private System.Windows.Forms.ToolStripButton toolStripButton_finish;
        private System.Windows.Forms.TextBox textBox_comment;
        private System.Windows.Forms.TextBox textBox_serial;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label_status;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label_customer;
        private System.Windows.Forms.BindingSource sellDataSetBindingSource;
        private System.Windows.Forms.Label label_operator;
        protected LookupText lookupText1;
        private System.Windows.Forms.Label label_title;
        private System.Windows.Forms.TextBox textBox_operator;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        protected System.Windows.Forms.Label label1_tip;
        private System.Windows.Forms.Panel panel_basic;
        private System.Windows.Forms.ToolStripButton toolStripButton_finishCancel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label_sum;
        private System.Windows.Forms.TextBox textBox_realTotal;
        private System.Windows.Forms.TextBox textBox_num;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_leftNum;
        private MyDataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn customer;
        private System.Windows.Forms.DataGridViewTextBoxColumn sellTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn type;
        private System.Windows.Forms.DataGridViewTextBoxColumn num;
        private System.Windows.Forms.DataGridViewTextBoxColumn comment;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
        
       
    }
}