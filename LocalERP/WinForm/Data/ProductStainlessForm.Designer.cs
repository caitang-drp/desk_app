namespace LocalERP.WinForm
{
    partial class ProductStainlessForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductStainlessForm));
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_name = new System.Windows.Forms.TextBox();
            this.textBox_purchasePrice = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_comment = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton_save = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_cancel = new System.Windows.Forms.ToolStripButton();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.comboBoxTree_category = new Verlinea.ComboBoxTree.ComboBoxTree();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.textBox_quantityPerPiece = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.textBox_sellPrice = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.textBox_serial = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.comboBox_unit = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 128);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "货品名称:";
            // 
            // textBox_name
            // 
            this.textBox_name.Location = new System.Drawing.Point(87, 123);
            this.textBox_name.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBox_name.Name = "textBox_name";
            this.textBox_name.Size = new System.Drawing.Size(373, 23);
            this.textBox_name.TabIndex = 1;
            // 
            // textBox_purchasePrice
            // 
            this.errorProvider1.SetIconPadding(this.textBox_purchasePrice, 20);
            this.textBox_purchasePrice.Location = new System.Drawing.Point(87, 159);
            this.textBox_purchasePrice.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBox_purchasePrice.Name = "textBox_purchasePrice";
            this.textBox_purchasePrice.Size = new System.Drawing.Size(114, 23);
            this.textBox_purchasePrice.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 164);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "进货价:";
            // 
            // textBox_comment
            // 
            this.textBox_comment.Location = new System.Drawing.Point(311, 159);
            this.textBox_comment.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBox_comment.Name = "textBox_comment";
            this.textBox_comment.Size = new System.Drawing.Size(148, 23);
            this.textBox_comment.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(261, 162);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 14);
            this.label6.TabIndex = 10;
            this.label6.Text = "备注:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 58);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 14);
            this.label3.TabIndex = 30;
            this.label3.Text = "状态:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(88, 58);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 14);
            this.label4.TabIndex = 31;
            this.label4.Text = "新增";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(8, 128);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(14, 14);
            this.label5.TabIndex = 32;
            this.label5.Text = "*";
            // 
            // toolStrip2
            // 
            this.toolStrip2.BackgroundImage = global::LocalERP.Properties.Resources.toolBack;
            this.toolStrip2.Font = new System.Drawing.Font("宋体", 10F);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton_save,
            this.toolStripButton_cancel});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(490, 37);
            this.toolStrip2.TabIndex = 29;
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
            // toolStripButton_cancel
            // 
            this.toolStripButton_cancel.Image = global::LocalERP.Properties.Resources.cancel16;
            this.toolStripButton_cancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_cancel.Name = "toolStripButton_cancel";
            this.toolStripButton_cancel.Size = new System.Drawing.Size(39, 34);
            this.toolStripButton_cancel.Text = "取消";
            this.toolStripButton_cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton_cancel.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(263, 92);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(42, 14);
            this.label7.TabIndex = 34;
            this.label7.Text = "类别:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Enabled = false;
            this.label8.Location = new System.Drawing.Point(262, 200);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(42, 14);
            this.label8.TabIndex = 36;
            this.label8.Text = "停用:";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Enabled = false;
            this.radioButton1.Location = new System.Drawing.Point(357, 198);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(39, 18);
            this.radioButton1.TabIndex = 38;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "否";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Enabled = false;
            this.radioButton2.Location = new System.Drawing.Point(312, 198);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(39, 18);
            this.radioButton2.TabIndex = 39;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "是";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // comboBox2
            // 
            this.comboBox2.Enabled = false;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(311, 267);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(148, 21);
            this.comboBox2.TabIndex = 42;
            this.comboBox2.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Enabled = false;
            this.label9.Location = new System.Drawing.Point(262, 270);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(42, 14);
            this.label9.TabIndex = 41;
            this.label9.Text = "品牌:";
            this.label9.Visible = false;
            // 
            // comboBoxTree_category
            // 
            this.comboBoxTree_category.AbsoluteChildrenSelectableOnly = false;
            this.comboBoxTree_category.BranchSeparator = null;
            this.comboBoxTree_category.Imagelist = null;
            this.comboBoxTree_category.Location = new System.Drawing.Point(312, 89);
            this.comboBoxTree_category.Name = "comboBoxTree_category";
            this.comboBoxTree_category.SelectedNode = null;
            this.comboBoxTree_category.Size = new System.Drawing.Size(149, 24);
            this.comboBoxTree_category.TabIndex = 43;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(250, 91);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(14, 14);
            this.label10.TabIndex = 44;
            this.label10.Text = "*";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(20, 270);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(42, 14);
            this.label11.TabIndex = 47;
            this.label11.Text = "单位:";
            // 
            // textBox_quantityPerPiece
            // 
            this.textBox_quantityPerPiece.Location = new System.Drawing.Point(87, 231);
            this.textBox_quantityPerPiece.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBox_quantityPerPiece.Name = "textBox_quantityPerPiece";
            this.textBox_quantityPerPiece.Size = new System.Drawing.Size(114, 23);
            this.textBox_quantityPerPiece.TabIndex = 46;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(20, 236);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(70, 14);
            this.label12.TabIndex = 45;
            this.label12.Text = "每件数量:";
            // 
            // textBox_sellPrice
            // 
            this.errorProvider1.SetIconPadding(this.textBox_sellPrice, 20);
            this.textBox_sellPrice.Location = new System.Drawing.Point(87, 193);
            this.textBox_sellPrice.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBox_sellPrice.Name = "textBox_sellPrice";
            this.textBox_sellPrice.Size = new System.Drawing.Size(114, 23);
            this.textBox_sellPrice.TabIndex = 50;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(20, 198);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(56, 14);
            this.label13.TabIndex = 49;
            this.label13.Text = "销售价:";
            // 
            // textBox_serial
            // 
            this.textBox_serial.Location = new System.Drawing.Point(88, 89);
            this.textBox_serial.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBox_serial.Name = "textBox_serial";
            this.textBox_serial.Size = new System.Drawing.Size(137, 23);
            this.textBox_serial.TabIndex = 52;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(20, 92);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(42, 14);
            this.label14.TabIndex = 51;
            this.label14.Text = "货号:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(204, 164);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(21, 14);
            this.label15.TabIndex = 53;
            this.label15.Text = "元";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(204, 198);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(21, 14);
            this.label16.TabIndex = 54;
            this.label16.Text = "元";
            // 
            // comboBox_unit
            // 
            this.comboBox_unit.FormattingEnabled = true;
            this.comboBox_unit.Items.AddRange(new object[] {
            "个",
            "盒"});
            this.comboBox_unit.Location = new System.Drawing.Point(87, 267);
            this.comboBox_unit.Name = "comboBox_unit";
            this.comboBox_unit.Size = new System.Drawing.Size(114, 21);
            this.comboBox_unit.TabIndex = 56;
            // 
            // ProductStainlessForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(490, 314);
            this.Controls.Add(this.comboBox_unit);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.textBox_serial);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.textBox_sellPrice);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.textBox_quantityPerPiece);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.comboBoxTree_category);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.textBox_comment);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBox_purchasePrice);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_name);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("宋体", 10F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "ProductStainlessForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "货品详细信息";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_name;
        private System.Windows.Forms.TextBox textBox_purchasePrice;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_comment;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton toolStripButton_save;
        private System.Windows.Forms.ToolStripButton toolStripButton_cancel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label9;
        private Verlinea.ComboBoxTree.ComboBoxTree comboBoxTree_category;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBox_quantityPerPiece;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBox_sellPrice;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBox_serial;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox comboBox_unit;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
    }
}