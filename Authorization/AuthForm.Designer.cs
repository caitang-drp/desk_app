
partial class AuthForm {
    /// <summary>
    /// 必需的设计器变量。
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// 清理所有正在使用的资源。
    /// </summary>
    /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
    protected override void Dispose(bool disposing) {
        if (disposing && (components != null)) {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows 窗体设计器生成的代码

    /// <summary>
    /// 设计器支持所需的方法 - 不要
    /// 使用代码编辑器修改此方法的内容。
    /// </summary>
    private void InitializeComponent() {
        this.button2 = new System.Windows.Forms.Button();
        this.button1 = new System.Windows.Forms.Button();
        this.textBox_source = new System.Windows.Forms.TextBox();
        this.label2 = new System.Windows.Forms.Label();
        this.label3 = new System.Windows.Forms.Label();
        this.textBox_serial = new System.Windows.Forms.TextBox();
        this.SuspendLayout();
        // 
        // button2
        // 
        this.button2.Location = new System.Drawing.Point(127, 111);
        this.button2.Name = "button2";
        this.button2.Size = new System.Drawing.Size(75, 23);
        this.button2.TabIndex = 6;
        this.button2.Text = "关闭(&C)";
        this.button2.UseVisualStyleBackColor = true;
        this.button2.Click += new System.EventHandler(this.button2_Click);
        // 
        // button1
        // 
        this.button1.Location = new System.Drawing.Point(46, 111);
        this.button1.Name = "button1";
        this.button1.Size = new System.Drawing.Size(75, 23);
        this.button1.TabIndex = 4;
        this.button1.Text = "生成(&G)";
        this.button1.UseVisualStyleBackColor = true;
        this.button1.Click += new System.EventHandler(this.button1_Click);
        // 
        // textBox_source
        // 
        this.textBox_source.Location = new System.Drawing.Point(73, 28);
        this.textBox_source.Name = "textBox_source";
        this.textBox_source.Size = new System.Drawing.Size(155, 21);
        this.textBox_source.TabIndex = 7;
        // 
        // label2
        // 
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(26, 33);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(41, 12);
        this.label2.TabIndex = 8;
        this.label2.Text = "校验码";
        // 
        // label3
        // 
        this.label3.AutoSize = true;
        this.label3.Location = new System.Drawing.Point(26, 68);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(41, 12);
        this.label3.TabIndex = 9;
        this.label3.Text = "注册码";
        // 
        // textBox_serial
        // 
        this.textBox_serial.Location = new System.Drawing.Point(73, 63);
        this.textBox_serial.Name = "textBox_serial";
        this.textBox_serial.Size = new System.Drawing.Size(155, 21);
        this.textBox_serial.TabIndex = 10;
        // 
        // AuthForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(258, 159);
        this.Controls.Add(this.textBox_serial);
        this.Controls.Add(this.label3);
        this.Controls.Add(this.label2);
        this.Controls.Add(this.textBox_source);
        this.Controls.Add(this.button2);
        this.Controls.Add(this.button1);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        this.Name = "AuthForm";
        this.Text = "注册码生成器";
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.TextBox textBox_source;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox textBox_serial;
}

