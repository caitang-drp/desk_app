using LocalERP.UiDataProxy;
using LocalERP.DataAccess.Utility;
namespace LocalERP.WinForm
{
    partial class PayReceiptForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PayReceiptForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label_date = new System.Windows.Forms.Label();
            this.dateTime_time = new System.Windows.Forms.DateTimePicker();
            this.textBox_comment = new System.Windows.Forms.TextBox();
            this.textBox_serial = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label_status = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.textBox_thisPayed = new System.Windows.Forms.TextBox();
            this.textBox_sum = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label_customer = new System.Windows.Forms.Label();
            this.sellDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label_operator = new System.Windows.Forms.Label();
            this.label_thisPayed = new System.Windows.Forms.Label();
            this.label_accumulative = new System.Windows.Forms.Label();
            this.label_title = new System.Windows.Forms.Label();
            this.textBox_operator = new System.Windows.Forms.TextBox();
            this.lookupText1 = new LocalERP.WinForm.LookupText();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton_save = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_approval = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_finish = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_finishCancel = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_print = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label_pay_need = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.textBox_previousArrears = new System.Windows.Forms.TextBox();
            this.label_arrears = new System.Windows.Forms.Label();
            this.textBox_accumulative = new System.Windows.Forms.TextBox();
            this.panel_history = new System.Windows.Forms.Panel();
            this.label_tip = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label_needPayed = new System.Windows.Forms.Label();
            this.panel_sum = new System.Windows.Forms.Panel();
            this.panel_customer = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sellDataSetBindingSource)).BeginInit();
            this.toolStrip2.SuspendLayout();
            this.panel_history.SuspendLayout();
            this.panel_sum.SuspendLayout();
            this.panel_customer.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 212);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "摘要信息:";
            // 
            // label_date
            // 
            this.label_date.AutoSize = true;
            this.label_date.Location = new System.Drawing.Point(348, 140);
            this.label_date.Name = "label_date";
            this.label_date.Size = new System.Drawing.Size(70, 14);
            this.label_date.TabIndex = 2;
            this.label_date.Text = "日    期:";
            // 
            // dateTime_time
            // 
            this.dateTime_time.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dateTime_time.Enabled = false;
            this.dateTime_time.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTime_time.Location = new System.Drawing.Point(420, 135);
            this.dateTime_time.Margin = new System.Windows.Forms.Padding(2);
            this.dateTime_time.Name = "dateTime_time";
            this.dateTime_time.Size = new System.Drawing.Size(178, 23);
            this.dateTime_time.TabIndex = 15;
            this.dateTime_time.ValueChanged += new System.EventHandler(this.Controls_TextChanged);
            // 
            // textBox_comment
            // 
            this.textBox_comment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_comment.Location = new System.Drawing.Point(18, 232);
            this.textBox_comment.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_comment.Multiline = true;
            this.textBox_comment.Name = "textBox_comment";
            this.textBox_comment.Size = new System.Drawing.Size(885, 218);
            this.textBox_comment.TabIndex = 18;
            this.textBox_comment.TextChanged += new System.EventHandler(this.Controls_TextChanged);
            // 
            // textBox_serial
            // 
            this.textBox_serial.Location = new System.Drawing.Point(420, 102);
            this.textBox_serial.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_serial.Name = "textBox_serial";
            this.textBox_serial.Size = new System.Drawing.Size(178, 23);
            this.textBox_serial.TabIndex = 20;
            this.textBox_serial.TextChanged += new System.EventHandler(this.Controls_TextChanged);
            this.textBox_serial.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_serial_Validating);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(348, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 14);
            this.label3.TabIndex = 19;
            this.label3.Text = "单据编号:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(34, 108);
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
            this.label_status.Location = new System.Drawing.Point(104, 108);
            this.label_status.Name = "label_status";
            this.label_status.Size = new System.Drawing.Size(35, 14);
            this.label_status.TabIndex = 22;
            this.label_status.Text = "新增";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // textBox_thisPayed
            // 
            this.errorProvider1.SetIconPadding(this.textBox_thisPayed, 20);
            this.textBox_thisPayed.Location = new System.Drawing.Point(93, 8);
            this.textBox_thisPayed.Name = "textBox_thisPayed";
            this.textBox_thisPayed.Size = new System.Drawing.Size(131, 23);
            this.textBox_thisPayed.TabIndex = 53;
            this.textBox_thisPayed.TextChanged += new System.EventHandler(this.Controls_TextChanged);
            // 
            // textBox_sum
            // 
            this.errorProvider1.SetIconPadding(this.textBox_sum, 20);
            this.textBox_sum.Location = new System.Drawing.Point(126, 9);
            this.textBox_sum.Name = "textBox_sum";
            this.textBox_sum.Size = new System.Drawing.Size(98, 23);
            this.textBox_sum.TabIndex = 71;
            this.textBox_sum.TextChanged += new System.EventHandler(this.Controls_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(336, 107);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(14, 14);
            this.label5.TabIndex = 34;
            this.label5.Text = "*";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(2, 10);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(14, 14);
            this.label9.TabIndex = 43;
            this.label9.Text = "*";
            // 
            // label_customer
            // 
            this.label_customer.AutoSize = true;
            this.label_customer.Location = new System.Drawing.Point(16, 10);
            this.label_customer.Name = "label_customer";
            this.label_customer.Size = new System.Drawing.Size(70, 14);
            this.label_customer.TabIndex = 41;
            this.label_customer.Text = "客    户:";
            // 
            // label_operator
            // 
            this.label_operator.AutoSize = true;
            this.label_operator.Location = new System.Drawing.Point(679, 140);
            this.label_operator.Name = "label_operator";
            this.label_operator.Size = new System.Drawing.Size(70, 14);
            this.label_operator.TabIndex = 45;
            this.label_operator.Text = "经 手 人:";
            // 
            // label_thisPayed
            // 
            this.label_thisPayed.AutoSize = true;
            this.label_thisPayed.Location = new System.Drawing.Point(17, 11);
            this.label_thisPayed.Name = "label_thisPayed";
            this.label_thisPayed.Size = new System.Drawing.Size(70, 14);
            this.label_thisPayed.TabIndex = 49;
            this.label_thisPayed.Text = "本单已收:";
            // 
            // label_accumulative
            // 
            this.label_accumulative.AutoSize = true;
            this.label_accumulative.ForeColor = System.Drawing.Color.Black;
            this.label_accumulative.Location = new System.Drawing.Point(354, 13);
            this.label_accumulative.Name = "label_accumulative";
            this.label_accumulative.Size = new System.Drawing.Size(112, 14);
            this.label_accumulative.TabIndex = 46;
            this.label_accumulative.Text = "累计欠款(应付):";
            // 
            // label_title
            // 
            this.label_title.AutoSize = true;
            this.label_title.Font = new System.Drawing.Font("宋体", 16F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.label_title.Location = new System.Drawing.Point(327, 52);
            this.label_title.Name = "label_title";
            this.label_title.Size = new System.Drawing.Size(125, 22);
            this.label_title.TabIndex = 51;
            this.label_title.Text = "货款流转单";
            // 
            // textBox_operator
            // 
            this.textBox_operator.Location = new System.Drawing.Point(750, 137);
            this.textBox_operator.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_operator.Name = "textBox_operator";
            this.textBox_operator.Size = new System.Drawing.Size(134, 23);
            this.textBox_operator.TabIndex = 52;
            this.textBox_operator.TextChanged += new System.EventHandler(this.Controls_TextChanged);
            // 
            // lookupText1
            // 
            this.lookupText1.BackColor = System.Drawing.Color.Transparent;
            this.lookupText1.Location = new System.Drawing.Point(87, 6);
            this.lookupText1.LookupForm = null;
            this.lookupText1.LookupFormType = null;
            this.lookupText1.Name = "lookupText1";
            this.lookupText1.SelectButtonBackGround = global::LocalERP.Properties.Resources.user16;
            this.lookupText1.Size = new System.Drawing.Size(134, 24);
            this.lookupText1.TabIndex = 50;
            this.lookupText1.Text_Lookup = "单击选择...";
            this.lookupText1.Value_Lookup = null;
            this.lookupText1.valueSetted += new LocalERP.WinForm.LookupText.ValueSetted(this.lookupText1_valueSetted);
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
            this.toolStripButton1});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(926, 37);
            this.toolStrip2.TabIndex = 17;
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
            this.toolStripButton_finishCancel.Image = global::LocalERP.Properties.Resources.del16;
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
            this.toolStripButton_print.Visible = false;
            this.toolStripButton_print.Click += new System.EventHandler(this.toolStripButton_print_Click);
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
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(225, 11);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(21, 14);
            this.label6.TabIndex = 55;
            this.label6.Text = "元";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(603, 13);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(21, 14);
            this.label7.TabIndex = 56;
            this.label7.Text = "元";
            // 
            // label_pay_need
            // 
            this.label_pay_need.AutoSize = true;
            this.label_pay_need.ForeColor = System.Drawing.Color.Red;
            this.label_pay_need.Location = new System.Drawing.Point(5, 11);
            this.label_pay_need.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_pay_need.Name = "label_pay_need";
            this.label_pay_need.Size = new System.Drawing.Size(14, 14);
            this.label_pay_need.TabIndex = 67;
            this.label_pay_need.Text = "*";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(320, 12);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(21, 14);
            this.label16.TabIndex = 66;
            this.label16.Text = "元";
            // 
            // textBox_previousArrears
            // 
            this.textBox_previousArrears.Enabled = false;
            this.textBox_previousArrears.Location = new System.Drawing.Point(137, 9);
            this.textBox_previousArrears.Name = "textBox_previousArrears";
            this.textBox_previousArrears.Size = new System.Drawing.Size(178, 23);
            this.textBox_previousArrears.TabIndex = 65;
            // 
            // label_arrears
            // 
            this.label_arrears.AutoSize = true;
            this.label_arrears.Location = new System.Drawing.Point(23, 11);
            this.label_arrears.Name = "label_arrears";
            this.label_arrears.Size = new System.Drawing.Size(112, 14);
            this.label_arrears.TabIndex = 64;
            this.label_arrears.Text = "以上欠款(应付):";
            // 
            // textBox_accumulative
            // 
            this.textBox_accumulative.Enabled = false;
            this.textBox_accumulative.Location = new System.Drawing.Point(467, 9);
            this.textBox_accumulative.Name = "textBox_accumulative";
            this.textBox_accumulative.Size = new System.Drawing.Size(134, 23);
            this.textBox_accumulative.TabIndex = 60;
            // 
            // panel_history
            // 
            this.panel_history.Controls.Add(this.label16);
            this.panel_history.Controls.Add(this.textBox_previousArrears);
            this.panel_history.Controls.Add(this.label_arrears);
            this.panel_history.Controls.Add(this.label_accumulative);
            this.panel_history.Controls.Add(this.textBox_accumulative);
            this.panel_history.Controls.Add(this.label7);
            this.panel_history.Location = new System.Drawing.Point(283, 165);
            this.panel_history.Name = "panel_history";
            this.panel_history.Size = new System.Drawing.Size(643, 42);
            this.panel_history.TabIndex = 68;
            // 
            // label_tip
            // 
            this.label_tip.AutoSize = true;
            this.label_tip.ForeColor = System.Drawing.Color.Red;
            this.label_tip.Location = new System.Drawing.Point(466, 60);
            this.label_tip.Name = "label_tip";
            this.label_tip.Size = new System.Drawing.Size(147, 14);
            this.label_tip.TabIndex = 69;
            this.label_tip.Text = "* 该单将自动核销债务";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(5, 13);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 14);
            this.label2.TabIndex = 73;
            this.label2.Text = "*";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(225, 12);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(21, 14);
            this.label8.TabIndex = 72;
            this.label8.Text = "元";
            // 
            // label_needPayed
            // 
            this.label_needPayed.AutoSize = true;
            this.label_needPayed.Location = new System.Drawing.Point(17, 13);
            this.label_needPayed.Name = "label_needPayed";
            this.label_needPayed.Size = new System.Drawing.Size(112, 14);
            this.label_needPayed.TabIndex = 70;
            this.label_needPayed.Text = "退点金额(应收):";
            // 
            // panel_sum
            // 
            this.panel_sum.Controls.Add(this.label2);
            this.panel_sum.Controls.Add(this.label8);
            this.panel_sum.Controls.Add(this.textBox_sum);
            this.panel_sum.Controls.Add(this.label_needPayed);
            this.panel_sum.Location = new System.Drawing.Point(15, 126);
            this.panel_sum.Name = "panel_sum";
            this.panel_sum.Size = new System.Drawing.Size(273, 37);
            this.panel_sum.TabIndex = 74;
            // 
            // panel_customer
            // 
            this.panel_customer.Controls.Add(this.lookupText1);
            this.panel_customer.Controls.Add(this.label9);
            this.panel_customer.Controls.Add(this.label_customer);
            this.panel_customer.Location = new System.Drawing.Point(663, 97);
            this.panel_customer.Name = "panel_customer";
            this.panel_customer.Size = new System.Drawing.Size(258, 37);
            this.panel_customer.TabIndex = 75;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label_pay_need);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.textBox_thisPayed);
            this.panel1.Controls.Add(this.label_thisPayed);
            this.panel1.Location = new System.Drawing.Point(15, 164);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(273, 37);
            this.panel1.TabIndex = 76;
            // 
            // PayReceiptForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(926, 523);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel_customer);
            this.Controls.Add(this.panel_sum);
            this.Controls.Add(this.label_tip);
            this.Controls.Add(this.textBox_operator);
            this.Controls.Add(this.label_title);
            this.Controls.Add(this.label_operator);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label_status);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox_serial);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_comment);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.dateTime_time);
            this.Controls.Add(this.label_date);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel_history);
            this.Font = new System.Drawing.Font("宋体", 10F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PayReceiptForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "货款流转单";
            this.Load += new System.EventHandler(this.PayReceiptForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sellDataSetBindingSource)).EndInit();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.panel_history.ResumeLayout(false);
            this.panel_history.PerformLayout();
            this.panel_sum.ResumeLayout(false);
            this.panel_sum.PerformLayout();
            this.panel_customer.ResumeLayout(false);
            this.panel_customer.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_date;
        private System.Windows.Forms.DateTimePicker dateTime_time;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton toolStripButton_save;
        private System.Windows.Forms.ToolStripButton toolStripButton_finish;
        private System.Windows.Forms.ToolStripButton toolStripButton_print;
        private System.Windows.Forms.TextBox textBox_comment;
        private System.Windows.Forms.TextBox textBox_serial;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label_status;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolStripButton toolStripButton_approval;
        protected System.Windows.Forms.Label label9;
        protected System.Windows.Forms.Label label_customer;
        private System.Windows.Forms.BindingSource sellDataSetBindingSource;
        private System.Windows.Forms.Label label_operator;
        private System.Windows.Forms.Label label_thisPayed;
        private System.Windows.Forms.Label label_accumulative;
        protected LookupText lookupText1;
        private System.Windows.Forms.Label label_title;
        private System.Windows.Forms.TextBox textBox_operator;
        private System.Windows.Forms.TextBox textBox_thisPayed;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.TextBox textBox_accumulative;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox textBox_previousArrears;
        private System.Windows.Forms.Label label_arrears;
        private System.Windows.Forms.Label label_pay_need;
        protected System.Windows.Forms.Panel panel_history;
        protected System.Windows.Forms.Label label_tip;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox_sum;
        private System.Windows.Forms.Label label_needPayed;
        protected System.Windows.Forms.Panel panel_sum;
        protected System.Windows.Forms.Panel panel_customer;
        protected System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripButton toolStripButton_finishCancel;
        
       
    }
}