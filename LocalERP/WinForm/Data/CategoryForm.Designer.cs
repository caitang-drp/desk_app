namespace LocalERP.WinForm
{
    partial class CategoryForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CategoryForm));
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_name = new System.Windows.Forms.TextBox();
            this.textBox_price = new System.Windows.Forms.TextBox();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxTree1 = new Verlinea.ComboBoxTree.ComboBoxTree();
            this.label9 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.toolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 59);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "名称:";
            // 
            // textBox_name
            // 
            this.textBox_name.Location = new System.Drawing.Point(86, 55);
            this.textBox_name.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBox_name.Name = "textBox_name";
            this.textBox_name.Size = new System.Drawing.Size(233, 23);
            this.textBox_name.TabIndex = 1;
            this.textBox_name.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_name_Validating);
            // 
            // textBox_price
            // 
            this.textBox_price.Location = new System.Drawing.Point(0, 0);
            this.textBox_price.Name = "textBox_price";
            this.textBox_price.Size = new System.Drawing.Size(100, 23);
            this.textBox_price.TabIndex = 33;
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
            this.toolStrip2.Size = new System.Drawing.Size(346, 37);
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
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(23, 58);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(14, 14);
            this.label5.TabIndex = 32;
            this.label5.Text = "*";
            // 
            // comboBoxTree1
            // 
            this.comboBoxTree1.AbsoluteChildrenSelectableOnly = false;
            this.comboBoxTree1.BranchSeparator = null;
            this.comboBoxTree1.Imagelist = null;
            this.comboBoxTree1.Location = new System.Drawing.Point(86, 96);
            this.comboBoxTree1.Name = "comboBoxTree1";
            this.comboBoxTree1.SelectedNode = null;
            this.comboBoxTree1.Size = new System.Drawing.Size(169, 24);
            this.comboBoxTree1.TabIndex = 49;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 99);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(70, 14);
            this.label9.TabIndex = 48;
            this.label9.Text = "上级类别:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(261, 95);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(58, 23);
            this.button1.TabIndex = 50;
            this.button1.Text = "清空";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // CategoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(346, 151);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboBoxTree1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.textBox_price);
            this.Controls.Add(this.textBox_name);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("宋体", 10F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "CategoryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "类别详细信息";
            this.Load += new System.EventHandler(this.ElementForm_Load);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_name;
        private System.Windows.Forms.TextBox textBox_price;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label label5;
        private Verlinea.ComboBoxTree.ComboBoxTree comboBoxTree1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button1;
    }
}