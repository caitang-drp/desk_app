namespace LocalERP.WinForm
{
    partial class CirSettingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CirSettingForm));
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.checkBox_backFreight = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBox_freight = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBox_printLetter = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.checkBox_lastPayReceipt = new System.Windows.Forms.CheckBox();
            this.toolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 63);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "单据编号格式:";
            // 
            // toolStrip2
            // 
            this.toolStrip2.BackgroundImage = global::LocalERP.Properties.Resources.toolBack;
            this.toolStrip2.Font = new System.Drawing.Font("宋体", 10F);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton3,
            this.toolStripButton4});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(469, 37);
            this.toolStrip2.TabIndex = 29;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Image = global::LocalERP.Properties.Resources.save16;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(39, 34);
            this.toolStripButton3.Text = "保存";
            this.toolStripButton3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.Image = global::LocalERP.Properties.Resources.cancel16;
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(39, 34);
            this.toolStripButton4.Text = "取消";
            this.toolStripButton4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton4.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(140, 62);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(137, 18);
            this.radioButton1.TabIndex = 44;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "类型-年月日-序号";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(283, 62);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(158, 18);
            this.radioButton2.TabIndex = 45;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "类型-客户-年月-序号";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // checkBox_backFreight
            // 
            this.checkBox_backFreight.AutoSize = true;
            this.checkBox_backFreight.Location = new System.Drawing.Point(140, 99);
            this.checkBox_backFreight.Name = "checkBox_backFreight";
            this.checkBox_backFreight.Size = new System.Drawing.Size(54, 18);
            this.checkBox_backFreight.TabIndex = 46;
            this.checkBox_backFreight.Text = "开启";
            this.checkBox_backFreight.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 99);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 14);
            this.label2.TabIndex = 47;
            this.label2.Text = "退运费:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(263, 139);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 14);
            this.label3.TabIndex = 49;
            this.label3.Text = "运费支出:";
            this.label3.Visible = false;
            // 
            // checkBox_freight
            // 
            this.checkBox_freight.AutoSize = true;
            this.checkBox_freight.Location = new System.Drawing.Point(387, 139);
            this.checkBox_freight.Name = "checkBox_freight";
            this.checkBox_freight.Size = new System.Drawing.Size(54, 18);
            this.checkBox_freight.TabIndex = 48;
            this.checkBox_freight.Text = "开启";
            this.checkBox_freight.UseVisualStyleBackColor = true;
            this.checkBox_freight.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 133);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 14);
            this.label4.TabIndex = 51;
            this.label4.Text = "打印封面:";
            // 
            // checkBox_printLetter
            // 
            this.checkBox_printLetter.AutoSize = true;
            this.checkBox_printLetter.Location = new System.Drawing.Point(140, 133);
            this.checkBox_printLetter.Name = "checkBox_printLetter";
            this.checkBox_printLetter.Size = new System.Drawing.Size(54, 18);
            this.checkBox_printLetter.TabIndex = 50;
            this.checkBox_printLetter.Text = "开启";
            this.checkBox_printLetter.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 164);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 14);
            this.label5.TabIndex = 53;
            this.label5.Text = "上次收付信息:";
            // 
            // checkBox_lastPayReceipt
            // 
            this.checkBox_lastPayReceipt.AutoSize = true;
            this.checkBox_lastPayReceipt.Location = new System.Drawing.Point(140, 164);
            this.checkBox_lastPayReceipt.Name = "checkBox_lastPayReceipt";
            this.checkBox_lastPayReceipt.Size = new System.Drawing.Size(54, 18);
            this.checkBox_lastPayReceipt.TabIndex = 52;
            this.checkBox_lastPayReceipt.Text = "开启";
            this.checkBox_lastPayReceipt.UseVisualStyleBackColor = true;
            // 
            // CirSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(469, 203);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.checkBox_lastPayReceipt);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.checkBox_printLetter);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.checkBox_freight);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.checkBox_backFreight);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("宋体", 10F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "CirSettingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "单据设置";
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBox_freight;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBox_backFreight;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBox_printLetter;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox checkBox_lastPayReceipt;
    }
}