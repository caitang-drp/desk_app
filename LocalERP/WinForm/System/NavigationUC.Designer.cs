namespace LocalERP.WinForm
{
    partial class NavigationUC
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NavigationUC));
            this.xPanderPanelList1 = new BSE.Windows.Forms.XPanderPanelList();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // xPanderPanelList1
            // 
            this.xPanderPanelList1.ColorScheme = BSE.Windows.Forms.ColorScheme.Custom;
            this.xPanderPanelList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xPanderPanelList1.Font = new System.Drawing.Font("宋体", 10F);
            this.xPanderPanelList1.GradientBackground = System.Drawing.Color.Empty;
            this.xPanderPanelList1.Location = new System.Drawing.Point(0, 0);
            this.xPanderPanelList1.Name = "xPanderPanelList1";
            this.xPanderPanelList1.Padding = new System.Windows.Forms.Padding(3);
            this.xPanderPanelList1.PanelStyle = BSE.Windows.Forms.PanelStyle.Aqua;
            this.xPanderPanelList1.ShowGradientBackground = true;
            this.xPanderPanelList1.Size = new System.Drawing.Size(178, 602);
            this.xPanderPanelList1.TabIndex = 1;
            this.xPanderPanelList1.Text = "xPanderPanelList1";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "arrow1.png");
            this.imageList1.Images.SetKeyName(1, "arrow1-1.png");
            // 
            // NavigationUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.xPanderPanelList1);
            this.Name = "NavigationUC";
            this.Size = new System.Drawing.Size(178, 602);
            this.ResumeLayout(false);

        }

        #endregion

        protected BSE.Windows.Forms.XPanderPanelList xPanderPanelList1;
        private System.Windows.Forms.ImageList imageList1;
    }
}
