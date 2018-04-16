namespace LocalERP.WinForm
{
    partial class LetterSettingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LetterSettingForm));
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.textBox_timeFormat = new System.Windows.Forms.TextBox();
            this.checkBox_name = new System.Windows.Forms.CheckBox();
            this.checkBox_address = new System.Windows.Forms.CheckBox();
            this.checkBox_phone = new System.Windows.Forms.CheckBox();
            this.checkBox_comment = new System.Windows.Forms.CheckBox();
            this.checkBox_pieces = new System.Windows.Forms.CheckBox();
            this.checkBox_time = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox_name = new System.Windows.Forms.TextBox();
            this.textBox_address = new System.Windows.Forms.TextBox();
            this.textBox_tel = new System.Windows.Forms.TextBox();
            this.textBox_pieces = new System.Windows.Forms.TextBox();
            this.textBox_comment = new System.Windows.Forms.TextBox();
            this.textBox_time = new System.Windows.Forms.TextBox();
            this.textBox_contractor = new System.Windows.Forms.TextBox();
            this.checkBox_contractor = new System.Windows.Forms.CheckBox();
            this.checkBox_contractorPhone = new System.Windows.Forms.CheckBox();
            this.textBox_contractorPhone = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // textBox_timeFormat
            // 
            this.textBox_timeFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_timeFormat.Location = new System.Drawing.Point(14, 301);
            this.textBox_timeFormat.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBox_timeFormat.Name = "textBox_timeFormat";
            this.textBox_timeFormat.Size = new System.Drawing.Size(111, 23);
            this.textBox_timeFormat.TabIndex = 38;
            // 
            // checkBox_name
            // 
            this.checkBox_name.AutoSize = true;
            this.checkBox_name.Checked = true;
            this.checkBox_name.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_name.Enabled = false;
            this.checkBox_name.Location = new System.Drawing.Point(14, 31);
            this.checkBox_name.Name = "checkBox_name";
            this.checkBox_name.Size = new System.Drawing.Size(110, 18);
            this.checkBox_name.TabIndex = 46;
            this.checkBox_name.Text = "往来单位名称";
            this.checkBox_name.UseVisualStyleBackColor = true;
            // 
            // checkBox_address
            // 
            this.checkBox_address.AutoSize = true;
            this.checkBox_address.Checked = true;
            this.checkBox_address.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_address.Enabled = false;
            this.checkBox_address.Location = new System.Drawing.Point(14, 64);
            this.checkBox_address.Name = "checkBox_address";
            this.checkBox_address.Size = new System.Drawing.Size(110, 18);
            this.checkBox_address.TabIndex = 47;
            this.checkBox_address.Text = "往来单位地址";
            this.checkBox_address.UseVisualStyleBackColor = true;
            // 
            // checkBox_phone
            // 
            this.checkBox_phone.AutoSize = true;
            this.checkBox_phone.Checked = true;
            this.checkBox_phone.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_phone.Enabled = false;
            this.checkBox_phone.Location = new System.Drawing.Point(14, 97);
            this.checkBox_phone.Name = "checkBox_phone";
            this.checkBox_phone.Size = new System.Drawing.Size(110, 18);
            this.checkBox_phone.TabIndex = 48;
            this.checkBox_phone.Text = "往来单位电话";
            this.checkBox_phone.UseVisualStyleBackColor = true;
            // 
            // checkBox_comment
            // 
            this.checkBox_comment.AutoSize = true;
            this.checkBox_comment.Checked = true;
            this.checkBox_comment.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_comment.Enabled = false;
            this.checkBox_comment.Location = new System.Drawing.Point(14, 163);
            this.checkBox_comment.Name = "checkBox_comment";
            this.checkBox_comment.Size = new System.Drawing.Size(82, 18);
            this.checkBox_comment.TabIndex = 49;
            this.checkBox_comment.Text = "单据备注";
            this.checkBox_comment.UseVisualStyleBackColor = true;
            // 
            // checkBox_pieces
            // 
            this.checkBox_pieces.AutoSize = true;
            this.checkBox_pieces.Checked = true;
            this.checkBox_pieces.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_pieces.Enabled = false;
            this.checkBox_pieces.Location = new System.Drawing.Point(14, 130);
            this.checkBox_pieces.Name = "checkBox_pieces";
            this.checkBox_pieces.Size = new System.Drawing.Size(82, 18);
            this.checkBox_pieces.TabIndex = 49;
            this.checkBox_pieces.Text = "单据件数";
            this.checkBox_pieces.UseVisualStyleBackColor = true;
            // 
            // checkBox_time
            // 
            this.checkBox_time.AutoSize = true;
            this.checkBox_time.Checked = true;
            this.checkBox_time.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_time.Enabled = false;
            this.checkBox_time.Location = new System.Drawing.Point(14, 260);
            this.checkBox_time.Name = "checkBox_time";
            this.checkBox_time.Size = new System.Drawing.Size(82, 18);
            this.checkBox_time.TabIndex = 49;
            this.checkBox_time.Text = "单据时间";
            this.checkBox_time.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 281);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 14);
            this.label1.TabIndex = 50;
            this.label1.Text = "单据时间格式:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 364);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(117, 23);
            this.button1.TabIndex = 51;
            this.button1.Text = "调整字段位置";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(13, 334);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(117, 23);
            this.button2.TabIndex = 52;
            this.button2.Text = "保存字段设置";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(260, 348);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(119, 23);
            this.button3.TabIndex = 53;
            this.button3.Text = "打印表格";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox_contractorPhone);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.checkBox_contractor);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.checkBox_pieces);
            this.groupBox1.Controls.Add(this.checkBox_time);
            this.groupBox1.Controls.Add(this.checkBox_comment);
            this.groupBox1.Controls.Add(this.checkBox_phone);
            this.groupBox1.Controls.Add(this.checkBox_address);
            this.groupBox1.Controls.Add(this.checkBox_name);
            this.groupBox1.Controls.Add(this.textBox_timeFormat);
            this.groupBox1.Location = new System.Drawing.Point(11, 14);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(147, 411);
            this.groupBox1.TabIndex = 54;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "字段设置";
            // 
            // textBox_name
            // 
            this.textBox_name.Location = new System.Drawing.Point(179, 44);
            this.textBox_name.Name = "textBox_name";
            this.textBox_name.Size = new System.Drawing.Size(268, 23);
            this.textBox_name.TabIndex = 55;
            // 
            // textBox_address
            // 
            this.textBox_address.Location = new System.Drawing.Point(179, 77);
            this.textBox_address.Name = "textBox_address";
            this.textBox_address.Size = new System.Drawing.Size(268, 23);
            this.textBox_address.TabIndex = 56;
            // 
            // textBox_tel
            // 
            this.textBox_tel.Location = new System.Drawing.Point(179, 110);
            this.textBox_tel.Name = "textBox_tel";
            this.textBox_tel.Size = new System.Drawing.Size(268, 23);
            this.textBox_tel.TabIndex = 57;
            // 
            // textBox_pieces
            // 
            this.textBox_pieces.Location = new System.Drawing.Point(179, 143);
            this.textBox_pieces.Name = "textBox_pieces";
            this.textBox_pieces.Size = new System.Drawing.Size(268, 23);
            this.textBox_pieces.TabIndex = 58;
            // 
            // textBox_comment
            // 
            this.textBox_comment.Location = new System.Drawing.Point(179, 176);
            this.textBox_comment.Name = "textBox_comment";
            this.textBox_comment.Size = new System.Drawing.Size(268, 23);
            this.textBox_comment.TabIndex = 59;
            // 
            // textBox_time
            // 
            this.textBox_time.Location = new System.Drawing.Point(179, 274);
            this.textBox_time.Name = "textBox_time";
            this.textBox_time.Size = new System.Drawing.Size(268, 23);
            this.textBox_time.TabIndex = 60;
            // 
            // textBox_contractor
            // 
            this.textBox_contractor.Location = new System.Drawing.Point(179, 208);
            this.textBox_contractor.Name = "textBox_contractor";
            this.textBox_contractor.Size = new System.Drawing.Size(268, 23);
            this.textBox_contractor.TabIndex = 61;
            // 
            // checkBox_contractor
            // 
            this.checkBox_contractor.AutoSize = true;
            this.checkBox_contractor.Checked = true;
            this.checkBox_contractor.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_contractor.Enabled = false;
            this.checkBox_contractor.Location = new System.Drawing.Point(14, 195);
            this.checkBox_contractor.Name = "checkBox_contractor";
            this.checkBox_contractor.Size = new System.Drawing.Size(96, 18);
            this.checkBox_contractor.TabIndex = 60;
            this.checkBox_contractor.Text = "发货人名称";
            this.checkBox_contractor.UseVisualStyleBackColor = true;
            // 
            // checkBox_contractorPhone
            // 
            this.checkBox_contractorPhone.AutoSize = true;
            this.checkBox_contractorPhone.Checked = true;
            this.checkBox_contractorPhone.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_contractorPhone.Enabled = false;
            this.checkBox_contractorPhone.Location = new System.Drawing.Point(14, 227);
            this.checkBox_contractorPhone.Name = "checkBox_contractorPhone";
            this.checkBox_contractorPhone.Size = new System.Drawing.Size(96, 18);
            this.checkBox_contractorPhone.TabIndex = 61;
            this.checkBox_contractorPhone.Text = "发货人电话";
            this.checkBox_contractorPhone.UseVisualStyleBackColor = true;
            // 
            // textBox_contractorPhone
            // 
            this.textBox_contractorPhone.Location = new System.Drawing.Point(179, 240);
            this.textBox_contractorPhone.Name = "textBox_contractorPhone";
            this.textBox_contractorPhone.Size = new System.Drawing.Size(268, 23);
            this.textBox_contractorPhone.TabIndex = 62;
            // 
            // LetterSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(459, 437);
            this.Controls.Add(this.textBox_contractorPhone);
            this.Controls.Add(this.textBox_contractor);
            this.Controls.Add(this.textBox_time);
            this.Controls.Add(this.textBox_comment);
            this.Controls.Add(this.textBox_pieces);
            this.Controls.Add(this.textBox_tel);
            this.Controls.Add(this.textBox_address);
            this.Controls.Add(this.textBox_name);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button3);
            this.Font = new System.Drawing.Font("宋体", 10F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "LetterSettingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "封面打印设置";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.TextBox textBox_timeFormat;
        private System.Windows.Forms.CheckBox checkBox_phone;
        private System.Windows.Forms.CheckBox checkBox_address;
        private System.Windows.Forms.CheckBox checkBox_name;
        private System.Windows.Forms.CheckBox checkBox_comment;
        private System.Windows.Forms.CheckBox checkBox_pieces;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox_time;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textBox_time;
        private System.Windows.Forms.TextBox textBox_comment;
        private System.Windows.Forms.TextBox textBox_pieces;
        private System.Windows.Forms.TextBox textBox_tel;
        private System.Windows.Forms.TextBox textBox_address;
        private System.Windows.Forms.TextBox textBox_name;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox_contractorPhone;
        private System.Windows.Forms.TextBox textBox_contractor;
        private System.Windows.Forms.CheckBox checkBox_contractorPhone;
        private System.Windows.Forms.CheckBox checkBox_contractor;
    }
}