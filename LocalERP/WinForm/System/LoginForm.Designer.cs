namespace LocalERP.WinForm
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_ps = new System.Windows.Forms.TextBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label_name = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(86, 139);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "密码:";
            // 
            // textBox_ps
            // 
            this.textBox_ps.Location = new System.Drawing.Point(130, 136);
            this.textBox_ps.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBox_ps.Name = "textBox_ps";
            this.textBox_ps.PasswordChar = '*';
            this.textBox_ps.Size = new System.Drawing.Size(157, 23);
            this.textBox_ps.TabIndex = 1;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(122, 173);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(60, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "登陆";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 218);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(378, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BackColor = System.Drawing.Color.Transparent;
            this.toolStripStatusLabel1.ImageTransparentColor = System.Drawing.Color.White;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(269, 17);
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(130, 102);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(157, 23);
            this.textBox1.TabIndex = 6;
            this.textBox1.Text = "管理员";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(85, 105);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 14);
            this.label2.TabIndex = 5;
            this.label2.Text = "账户:";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(198, 173);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(60, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "关闭";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Wheat;
            this.panel1.Controls.Add(this.label_name);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(378, 84);
            this.panel1.TabIndex = 8;
            // 
            // label_name
            // 
            this.label_name.AutoSize = true;
            this.label_name.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_name.Location = new System.Drawing.Point(6, 29);
            this.label_name.Name = "label_name";
            this.label_name.Size = new System.Drawing.Size(23, 24);
            this.label_name.TabIndex = 0;
            this.label_name.Text = ".";
            // 
            // LoginForm
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(378, 240);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox_ps);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("宋体", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "系统登陆";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_ps;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label_name;
    }
}