using LocalERP.UiDataProxy;
using LocalERP.DataAccess.Utility;
namespace LocalERP.WinForm
{
    partial class SellReceiptBillForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SellReceiptBillForm));
            this.label_comment = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTime_pay_time = new System.Windows.Forms.DateTimePicker();
            this.textBox_comment = new System.Windows.Forms.TextBox();
            this.textBox_serial = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label_customer = new System.Windows.Forms.Label();
            this.sellDataSet = new LocalERP.CrystalReport.SellDataSet();
            this.sellDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label_operator = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label_title = new System.Windows.Forms.Label();
            this.textBox_operator = new System.Windows.Forms.TextBox();
            this.lookuptext_customer = new LocalERP.WinForm.LookupText();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton_save = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_approval = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_finish = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_print = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.textBox_now_arrear = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.panel_pay = new System.Windows.Forms.Panel();
            this.label16 = new System.Windows.Forms.Label();
            this.textBox_last_arrear = new System.Windows.Forms.TextBox();
            this.label_last_arrear = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox_receipt_amount = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sellDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sellDataSetBindingSource)).BeginInit();
            this.toolStrip2.SuspendLayout();
            this.panel_pay.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label_comment
            // 
            this.label_comment.AutoSize = true;
            this.label_comment.Location = new System.Drawing.Point(7, 201);
            this.label_comment.Name = "label_comment";
            this.label_comment.Size = new System.Drawing.Size(70, 14);
            this.label_comment.TabIndex = 0;
            this.label_comment.Text = "备    注:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(84, 137);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "还款日期:";
            // 
            // dateTime_pay_time
            // 
            this.dateTime_pay_time.Location = new System.Drawing.Point(159, 134);
            this.dateTime_pay_time.Margin = new System.Windows.Forms.Padding(2);
            this.dateTime_pay_time.Name = "dateTime_pay_time";
            this.dateTime_pay_time.Size = new System.Drawing.Size(150, 23);
            this.dateTime_pay_time.TabIndex = 15;
            // 
            // textBox_comment
            // 
            this.textBox_comment.Location = new System.Drawing.Point(110, 192);
            this.textBox_comment.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_comment.Multiline = true;
            this.textBox_comment.Name = "textBox_comment";
            this.textBox_comment.Size = new System.Drawing.Size(608, 126);
            this.textBox_comment.TabIndex = 18;
            // 
            // textBox_serial
            // 
            this.textBox_serial.Location = new System.Drawing.Point(159, 104);
            this.textBox_serial.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_serial.Name = "textBox_serial";
            this.textBox_serial.Size = new System.Drawing.Size(150, 23);
            this.textBox_serial.TabIndex = 20;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(84, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 14);
            this.label3.TabIndex = 19;
            this.label3.Text = "单据编号:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 14);
            this.label4.TabIndex = 21;
            this.label4.Text = "还款金额:";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(63, 105);
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
            this.label9.Location = new System.Drawing.Point(469, 103);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(14, 14);
            this.label9.TabIndex = 43;
            this.label9.Text = "*";
            // 
            // label_customer
            // 
            this.label_customer.AutoSize = true;
            this.label_customer.Location = new System.Drawing.Point(490, 104);
            this.label_customer.Name = "label_customer";
            this.label_customer.Size = new System.Drawing.Size(70, 14);
            this.label_customer.TabIndex = 41;
            this.label_customer.Text = "客    户:";
            // 
            // sellDataSet
            // 
            this.sellDataSet.DataSetName = "SellDataSet";
            this.sellDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // sellDataSetBindingSource
            // 
            this.sellDataSetBindingSource.DataSource = this.sellDataSet;
            this.sellDataSetBindingSource.Position = 0;
            // 
            // label_operator
            // 
            this.label_operator.AutoSize = true;
            this.label_operator.Location = new System.Drawing.Point(490, 134);
            this.label_operator.Name = "label_operator";
            this.label_operator.Size = new System.Drawing.Size(70, 14);
            this.label_operator.TabIndex = 45;
            this.label_operator.Text = "经 手 人:";
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(425, 31);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(70, 14);
            this.label12.TabIndex = 46;
            this.label12.Text = "累计欠款:";
            // 
            // label_title
            // 
            this.label_title.AutoSize = true;
            this.label_title.Font = new System.Drawing.Font("宋体", 16F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.label_title.Location = new System.Drawing.Point(367, 52);
            this.label_title.Name = "label_title";
            this.label_title.Size = new System.Drawing.Size(125, 22);
            this.label_title.TabIndex = 51;
            this.label_title.Text = "销售收款单";
            // 
            // textBox_operator
            // 
            this.textBox_operator.Location = new System.Drawing.Point(565, 131);
            this.textBox_operator.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_operator.Name = "textBox_operator";
            this.textBox_operator.Size = new System.Drawing.Size(153, 23);
            this.textBox_operator.TabIndex = 52;
            // 
            // lookuptext_customer
            // 
            this.lookuptext_customer.BackColor = System.Drawing.Color.Transparent;
            this.lookuptext_customer.Location = new System.Drawing.Point(566, 104);
            this.lookuptext_customer.LookupForm = null;
            this.lookuptext_customer.LookupFormType = null;
            this.lookuptext_customer.Name = "lookuptext_customer";
            this.lookuptext_customer.SelectButtonBackGround = global::LocalERP.Properties.Resources.user16;
            this.lookuptext_customer.Size = new System.Drawing.Size(153, 24);
            this.lookuptext_customer.TabIndex = 50;
            this.lookuptext_customer.Text_Lookup = "单击选择...";
            this.lookuptext_customer.Value_Lookup = null;
            // 
            // toolStrip2
            // 
            this.toolStrip2.BackgroundImage = global::LocalERP.Properties.Resources.toolBack;
            this.toolStrip2.Font = new System.Drawing.Font("宋体", 10F);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton_save,
            this.toolStripButton_approval,
            this.toolStripButton_finish,
            this.toolStripButton_print,
            this.toolStripButton1});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(832, 37);
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
            // textBox_now_arrear
            // 
            this.textBox_now_arrear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox_now_arrear.Location = new System.Drawing.Point(501, 27);
            this.textBox_now_arrear.Name = "textBox_now_arrear";
            this.textBox_now_arrear.Size = new System.Drawing.Size(97, 23);
            this.textBox_now_arrear.TabIndex = 53;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(604, 31);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(21, 14);
            this.label7.TabIndex = 56;
            this.label7.Text = "元";
            // 
            // panel_pay
            // 
            this.panel_pay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel_pay.Controls.Add(this.label16);
            this.panel_pay.Controls.Add(this.textBox_last_arrear);
            this.panel_pay.Controls.Add(this.label_last_arrear);
            this.panel_pay.Controls.Add(this.label7);
            this.panel_pay.Controls.Add(this.textBox_now_arrear);
            this.panel_pay.Controls.Add(this.label12);
            this.panel_pay.Location = new System.Drawing.Point(10, 445);
            this.panel_pay.Name = "panel_pay";
            this.panel_pay.Size = new System.Drawing.Size(800, 55);
            this.panel_pay.TabIndex = 58;
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(362, 36);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(21, 14);
            this.label16.TabIndex = 66;
            this.label16.Text = "元";
            // 
            // textBox_last_arrear
            // 
            this.textBox_last_arrear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox_last_arrear.Location = new System.Drawing.Point(262, 27);
            this.textBox_last_arrear.Name = "textBox_last_arrear";
            this.textBox_last_arrear.Size = new System.Drawing.Size(94, 23);
            this.textBox_last_arrear.TabIndex = 65;
            // 
            // label_last_arrear
            // 
            this.label_last_arrear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_last_arrear.AutoSize = true;
            this.label_last_arrear.Location = new System.Drawing.Point(186, 36);
            this.label_last_arrear.Name = "label_last_arrear";
            this.label_last_arrear.Size = new System.Drawing.Size(70, 14);
            this.label_last_arrear.TabIndex = 64;
            this.label_last_arrear.Text = "以上欠款:";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(201, 15);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(21, 14);
            this.label8.TabIndex = 67;
            this.label8.Text = "元";
            // 
            // textBox_receipt_amount
            // 
            this.textBox_receipt_amount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox_receipt_amount.Location = new System.Drawing.Point(98, 12);
            this.textBox_receipt_amount.Name = "textBox_receipt_amount";
            this.textBox_receipt_amount.Size = new System.Drawing.Size(97, 23);
            this.textBox_receipt_amount.TabIndex = 67;
            this.textBox_receipt_amount.TextChanged += new System.EventHandler(this.textBox_receipt_amount_textchanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.textBox_receipt_amount);
            this.panel1.Location = new System.Drawing.Point(12, 365);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(808, 57);
            this.panel1.TabIndex = 67;
            // 
            // SellReceiptBillForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(832, 522);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel_pay);
            this.Controls.Add(this.textBox_operator);
            this.Controls.Add(this.label_title);
            this.Controls.Add(this.lookuptext_customer);
            this.Controls.Add(this.label_operator);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label_customer);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox_serial);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_comment);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.dateTime_pay_time);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label_comment);
            this.Font = new System.Drawing.Font("宋体", 10F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SellReceiptBillForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "产品流转单";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sellDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sellDataSetBindingSource)).EndInit();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.panel_pay.ResumeLayout(false);
            this.panel_pay.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton toolStripButton_save;
        private System.Windows.Forms.ToolStripButton toolStripButton_finish;
        private System.Windows.Forms.ToolStripButton toolStripButton_print;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolStripButton toolStripButton_approval;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label_customer;
        private System.Windows.Forms.BindingSource sellDataSetBindingSource;
        private LocalERP.CrystalReport.SellDataSet sellDataSet;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label_title;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label8;
        // 单据编号
        private System.Windows.Forms.TextBox textBox_serial;
        // 还款日期
        private System.Windows.Forms.DateTimePicker dateTime_pay_time;
        // 客户
        private LookupText lookuptext_customer;
        // 经手人
        private System.Windows.Forms.Label label_operator;
        private System.Windows.Forms.TextBox textBox_operator;
        // 备注
        private System.Windows.Forms.Label label_comment;
        private System.Windows.Forms.TextBox textBox_comment;
        // 还款组合框
        private System.Windows.Forms.Panel panel_pay;
        // 还款金额
        public System.Windows.Forms.TextBox textBox_receipt_amount;
        // 以上欠款
        private System.Windows.Forms.TextBox textBox_last_arrear;
        private System.Windows.Forms.Label label_last_arrear;
        // 累计欠款(累计欠款=以上欠款-还款金额)
        private System.Windows.Forms.TextBox textBox_now_arrear;
        private System.Windows.Forms.Panel panel1;
    }
}