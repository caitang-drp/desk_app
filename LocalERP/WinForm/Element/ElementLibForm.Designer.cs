namespace LocalERP.WinForm
{
    partial class ElementLibForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ElementLibForm));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboBox_endMon = new System.Windows.Forms.ComboBox();
            this.comboBox_endYear = new System.Windows.Forms.ComboBox();
            this.comboBox_startMon = new System.Windows.Forms.ComboBox();
            this.comboBox_startYear = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.rowMergeView1 = new RowMergeView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rowMergeView1)).BeginInit();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "arrow1-1.png");
            this.imageList1.Images.SetKeyName(1, "arrow1.png");
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.comboBox_endMon);
            this.panel1.Controls.Add(this.comboBox_endYear);
            this.panel1.Controls.Add(this.comboBox_startMon);
            this.panel1.Controls.Add(this.comboBox_startYear);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(716, 50);
            this.panel1.TabIndex = 15;
            // 
            // comboBox_endMon
            // 
            this.comboBox_endMon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_endMon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_endMon.FormattingEnabled = true;
            this.comboBox_endMon.Location = new System.Drawing.Point(510, 12);
            this.comboBox_endMon.Name = "comboBox_endMon";
            this.comboBox_endMon.Size = new System.Drawing.Size(65, 21);
            this.comboBox_endMon.TabIndex = 26;
            // 
            // comboBox_endYear
            // 
            this.comboBox_endYear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_endYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_endYear.FormattingEnabled = true;
            this.comboBox_endYear.Location = new System.Drawing.Point(385, 12);
            this.comboBox_endYear.Name = "comboBox_endYear";
            this.comboBox_endYear.Size = new System.Drawing.Size(117, 21);
            this.comboBox_endYear.TabIndex = 25;
            // 
            // comboBox_startMon
            // 
            this.comboBox_startMon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_startMon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_startMon.FormattingEnabled = true;
            this.comboBox_startMon.Location = new System.Drawing.Point(234, 12);
            this.comboBox_startMon.Name = "comboBox_startMon";
            this.comboBox_startMon.Size = new System.Drawing.Size(65, 21);
            this.comboBox_startMon.TabIndex = 24;
            // 
            // comboBox_startYear
            // 
            this.comboBox_startYear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_startYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_startYear.FormattingEnabled = true;
            this.comboBox_startYear.Location = new System.Drawing.Point(109, 12);
            this.comboBox_startYear.Name = "comboBox_startYear";
            this.comboBox_startYear.Size = new System.Drawing.Size(117, 21);
            this.comboBox_startYear.TabIndex = 23;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(594, 10);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(108, 25);
            this.button1.TabIndex = 22;
            this.button1.Text = "生产月统计数";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.label2.Location = new System.Drawing.Point(313, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 14);
            this.label2.TabIndex = 21;
            this.label2.Text = "结束时间:";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.label1.Location = new System.Drawing.Point(37, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 14);
            this.label1.TabIndex = 19;
            this.label1.Text = "开始时间:";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(716, 50);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // rowMergeView1
            // 
            this.rowMergeView1.AllowUserToAddRows = false;
            this.rowMergeView1.AllowUserToResizeRows = false;
            this.rowMergeView1.BackgroundColor = System.Drawing.Color.White;
            this.rowMergeView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.rowMergeView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.name,
            this.num});
            this.rowMergeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rowMergeView1.Location = new System.Drawing.Point(0, 50);
            this.rowMergeView1.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.rowMergeView1.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("rowMergeView1.MergeColumnNames")));
            this.rowMergeView1.Name = "rowMergeView1";
            this.rowMergeView1.ReadOnly = true;
            this.rowMergeView1.RowHeadersVisible = false;
            this.rowMergeView1.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(200)))), ((int)(((byte)(79)))));
            this.rowMergeView1.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.rowMergeView1.RowTemplate.Height = 23;
            this.rowMergeView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.rowMergeView1.Size = new System.Drawing.Size(716, 449);
            this.rowMergeView1.TabIndex = 16;
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            // 
            // name
            // 
            this.name.HeaderText = "配件";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            // 
            // num
            // 
            this.num.HeaderText = "数量";
            this.num.Name = "num";
            this.num.ReadOnly = true;
            // 
            // ElementLibForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(716, 499);
            this.Controls.Add(this.rowMergeView1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 10F);
            this.HideOnClose = true;
            this.Name = "ElementLibForm";
            this.Text = "配件存量查询";
            this.Load += new System.EventHandler(this.ElementLibForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rowMergeView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox_endMon;
        private System.Windows.Forms.ComboBox comboBox_endYear;
        private System.Windows.Forms.ComboBox comboBox_startMon;
        private System.Windows.Forms.ComboBox comboBox_startYear;
        private RowMergeView rowMergeView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn num;
    }
}