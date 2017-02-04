namespace LocalERP.WinForm.Utility
{
    partial class PickValue
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.button_left = new System.Windows.Forms.Button();
            this.button_right = new System.Windows.Forms.Button();
            this.listBox_right = new System.Windows.Forms.ListBox();
            this.listBox_left = new System.Windows.Forms.ListBox();
            this.button_all_left = new System.Windows.Forms.Button();
            this.button_all_right = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button_left
            // 
            this.button_left.Location = new System.Drawing.Point(172, 143);
            this.button_left.Name = "button_left";
            this.button_left.Size = new System.Drawing.Size(40, 23);
            this.button_left.TabIndex = 10;
            this.button_left.Text = "<";
            this.button_left.UseVisualStyleBackColor = true;
            this.button_left.Click += new System.EventHandler(this.button_select_Click);
            // 
            // button_right
            // 
            this.button_right.Location = new System.Drawing.Point(172, 74);
            this.button_right.Name = "button_right";
            this.button_right.Size = new System.Drawing.Size(40, 23);
            this.button_right.TabIndex = 9;
            this.button_right.Text = ">";
            this.button_right.UseVisualStyleBackColor = true;
            this.button_right.Click += new System.EventHandler(this.button_select_Click);
            // 
            // listBox_right
            // 
            this.listBox_right.Font = new System.Drawing.Font("宋体", 10F);
            this.listBox_right.FormattingEnabled = true;
            this.listBox_right.Location = new System.Drawing.Point(224, 28);
            this.listBox_right.Margin = new System.Windows.Forms.Padding(0);
            this.listBox_right.Name = "listBox_right";
            this.listBox_right.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBox_right.Size = new System.Drawing.Size(160, 212);
            this.listBox_right.TabIndex = 7;
            this.listBox_right.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBox_right_MouseDoubleClick);
            // 
            // listBox_left
            // 
            this.listBox_left.Font = new System.Drawing.Font("宋体", 10F);
            this.listBox_left.FormattingEnabled = true;
            this.listBox_left.Location = new System.Drawing.Point(0, 28);
            this.listBox_left.Margin = new System.Windows.Forms.Padding(0);
            this.listBox_left.Name = "listBox_left";
            this.listBox_left.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBox_left.Size = new System.Drawing.Size(160, 212);
            this.listBox_left.TabIndex = 6;
            this.listBox_left.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBox_left_MouseDoubleClick);
            // 
            // button_all_left
            // 
            this.button_all_left.Location = new System.Drawing.Point(172, 172);
            this.button_all_left.Name = "button_all_left";
            this.button_all_left.Size = new System.Drawing.Size(40, 23);
            this.button_all_left.TabIndex = 11;
            this.button_all_left.Text = "<<";
            this.button_all_left.UseVisualStyleBackColor = true;
            this.button_all_left.Click += new System.EventHandler(this.button_all_Click);
            // 
            // button_all_right
            // 
            this.button_all_right.Location = new System.Drawing.Point(172, 45);
            this.button_all_right.Name = "button_all_right";
            this.button_all_right.Size = new System.Drawing.Size(40, 23);
            this.button_all_right.TabIndex = 8;
            this.button_all_right.Text = ">>";
            this.button_all_right.UseVisualStyleBackColor = true;
            this.button_all_right.Click += new System.EventHandler(this.button_all_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10F);
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 14);
            this.label1.TabIndex = 12;
            this.label1.Text = "可选项目:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10F);
            this.label2.Location = new System.Drawing.Point(226, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 14);
            this.label2.TabIndex = 13;
            this.label2.Text = "已选项目:";
            // 
            // PickValue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_all_left);
            this.Controls.Add(this.button_left);
            this.Controls.Add(this.button_right);
            this.Controls.Add(this.button_all_right);
            this.Controls.Add(this.listBox_right);
            this.Controls.Add(this.listBox_left);
            this.Name = "PickValue";
            this.Size = new System.Drawing.Size(388, 250);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_left;
        private System.Windows.Forms.Button button_right;
        private System.Windows.Forms.ListBox listBox_right;
        private System.Windows.Forms.ListBox listBox_left;
        private System.Windows.Forms.Button button_all_left;
        private System.Windows.Forms.Button button_all_right;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}
