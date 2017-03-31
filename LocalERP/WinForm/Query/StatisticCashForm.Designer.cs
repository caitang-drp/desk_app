namespace LocalERP.WinForm
{
    partial class StatisticCashForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StatisticCashForm));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.label_notice = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label_lib = new System.Windows.Forms.Label();
            this.label_needReceipt = new System.Windows.Forms.Label();
            this.label_sumCash1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label_assets = new System.Windows.Forms.Label();
            this.label_needPay = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label_debt = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label_purchaseBack = new System.Windows.Forms.Label();
            this.label_sellBack = new System.Windows.Forms.Label();
            this.label_sumCash = new System.Windows.Forms.Label();
            this.label_otherPay = new System.Windows.Forms.Label();
            this.label_paySum = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label_pay = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label_sumReceipt = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label_otherReceipt = new System.Windows.Forms.Label();
            this.label_receipt = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.label20 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "folder-open.16px.png");
            this.imageList1.Images.SetKeyName(1, "folder-close.16px.1.png");
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 609);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip1.Size = new System.Drawing.Size(759, 22);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // label_notice
            // 
            this.label_notice.AutoSize = true;
            this.label_notice.ForeColor = System.Drawing.Color.Red;
            this.label_notice.Location = new System.Drawing.Point(33, 15);
            this.label_notice.Name = "label_notice";
            this.label_notice.Size = new System.Drawing.Size(168, 14);
            this.label_notice.TabIndex = 55;
            this.label_notice.Text = "数据有更新, 请重新统计!";
            this.label_notice.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(43, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 14);
            this.label1.TabIndex = 56;
            this.label1.Text = "资产";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::LocalERP.Properties.Resources.selectall;
            this.pictureBox1.Location = new System.Drawing.Point(21, 34);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(20, 21);
            this.pictureBox1.TabIndex = 57;
            this.pictureBox1.TabStop = false;
            // 
            // label_lib
            // 
            this.label_lib.AutoSize = true;
            this.label_lib.Location = new System.Drawing.Point(18, 69);
            this.label_lib.Name = "label_lib";
            this.label_lib.Size = new System.Drawing.Size(70, 14);
            this.label_lib.TabIndex = 58;
            this.label_lib.Text = "库存成本:";
            // 
            // label_needReceipt
            // 
            this.label_needReceipt.AutoSize = true;
            this.label_needReceipt.Location = new System.Drawing.Point(18, 96);
            this.label_needReceipt.Name = "label_needReceipt";
            this.label_needReceipt.Size = new System.Drawing.Size(70, 14);
            this.label_needReceipt.TabIndex = 59;
            this.label_needReceipt.Text = "应收货款:";
            // 
            // label_sumCash1
            // 
            this.label_sumCash1.AutoSize = true;
            this.label_sumCash1.ForeColor = System.Drawing.Color.Red;
            this.label_sumCash1.Location = new System.Drawing.Point(18, 124);
            this.label_sumCash1.Name = "label_sumCash1";
            this.label_sumCash1.Size = new System.Drawing.Size(70, 14);
            this.label_sumCash1.TabIndex = 60;
            this.label_sumCash1.Text = "结存金额:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 141);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(238, 14);
            this.label5.TabIndex = 61;
            this.label5.Text = "_________________________________";
            // 
            // label_assets
            // 
            this.label_assets.AutoSize = true;
            this.label_assets.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label_assets.Location = new System.Drawing.Point(43, 167);
            this.label_assets.Name = "label_assets";
            this.label_assets.Size = new System.Drawing.Size(45, 14);
            this.label_assets.TabIndex = 62;
            this.label_assets.Text = "合计:";
            // 
            // label_needPay
            // 
            this.label_needPay.AutoSize = true;
            this.label_needPay.Location = new System.Drawing.Point(285, 69);
            this.label_needPay.Name = "label_needPay";
            this.label_needPay.Size = new System.Drawing.Size(70, 14);
            this.label_needPay.TabIndex = 65;
            this.label_needPay.Text = "应付货款:";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::LocalERP.Properties.Resources.selectall;
            this.pictureBox2.Location = new System.Drawing.Point(288, 34);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(20, 21);
            this.pictureBox2.TabIndex = 64;
            this.pictureBox2.TabStop = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label10.Location = new System.Drawing.Point(310, 35);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(37, 14);
            this.label10.TabIndex = 63;
            this.label10.Text = "负债";
            // 
            // label_debt
            // 
            this.label_debt.AutoSize = true;
            this.label_debt.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label_debt.Location = new System.Drawing.Point(310, 167);
            this.label_debt.Name = "label_debt";
            this.label_debt.Size = new System.Drawing.Size(45, 14);
            this.label_debt.TabIndex = 69;
            this.label_debt.Text = "合计:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(283, 141);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(238, 14);
            this.label12.TabIndex = 68;
            this.label12.Text = "_________________________________";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label_debt);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label_needPay);
            this.groupBox1.Controls.Add(this.pictureBox2);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label_assets);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label_sumCash1);
            this.groupBox1.Controls.Add(this.label_needReceipt);
            this.groupBox1.Controls.Add(this.label_lib);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(50, 349);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(545, 216);
            this.groupBox1.TabIndex = 70;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "资产负债表";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label_purchaseBack);
            this.groupBox2.Controls.Add(this.label_sellBack);
            this.groupBox2.Controls.Add(this.label_sumCash);
            this.groupBox2.Controls.Add(this.label_otherPay);
            this.groupBox2.Controls.Add(this.label_paySum);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label_pay);
            this.groupBox2.Controls.Add(this.pictureBox3);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.label_sumReceipt);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.label_otherReceipt);
            this.groupBox2.Controls.Add(this.label_receipt);
            this.groupBox2.Controls.Add(this.pictureBox4);
            this.groupBox2.Controls.Add(this.label20);
            this.groupBox2.Location = new System.Drawing.Point(50, 56);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(545, 259);
            this.groupBox2.TabIndex = 71;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "收支统计表";
            // 
            // label_purchaseBack
            // 
            this.label_purchaseBack.AutoSize = true;
            this.label_purchaseBack.Location = new System.Drawing.Point(18, 97);
            this.label_purchaseBack.Name = "label_purchaseBack";
            this.label_purchaseBack.Size = new System.Drawing.Size(98, 14);
            this.label_purchaseBack.TabIndex = 74;
            this.label_purchaseBack.Text = "采购退货收入:";
            // 
            // label_sellBack
            // 
            this.label_sellBack.AutoSize = true;
            this.label_sellBack.Location = new System.Drawing.Point(285, 97);
            this.label_sellBack.Name = "label_sellBack";
            this.label_sellBack.Size = new System.Drawing.Size(98, 14);
            this.label_sellBack.TabIndex = 73;
            this.label_sellBack.Text = "销售退货支出:";
            // 
            // label_sumCash
            // 
            this.label_sumCash.AutoSize = true;
            this.label_sumCash.ForeColor = System.Drawing.Color.Red;
            this.label_sumCash.Location = new System.Drawing.Point(18, 216);
            this.label_sumCash.Name = "label_sumCash";
            this.label_sumCash.Size = new System.Drawing.Size(70, 14);
            this.label_sumCash.TabIndex = 71;
            this.label_sumCash.Text = "结存金额:";
            // 
            // label_otherPay
            // 
            this.label_otherPay.AutoSize = true;
            this.label_otherPay.Location = new System.Drawing.Point(286, 125);
            this.label_otherPay.Name = "label_otherPay";
            this.label_otherPay.Size = new System.Drawing.Size(70, 14);
            this.label_otherPay.TabIndex = 70;
            this.label_otherPay.Text = "其他支出:";
            // 
            // label_paySum
            // 
            this.label_paySum.AutoSize = true;
            this.label_paySum.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label_paySum.Location = new System.Drawing.Point(310, 181);
            this.label_paySum.Name = "label_paySum";
            this.label_paySum.Size = new System.Drawing.Size(45, 14);
            this.label_paySum.TabIndex = 69;
            this.label_paySum.Text = "合计:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(283, 156);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(238, 14);
            this.label8.TabIndex = 68;
            this.label8.Text = "_________________________________";
            // 
            // label_pay
            // 
            this.label_pay.AutoSize = true;
            this.label_pay.Location = new System.Drawing.Point(285, 69);
            this.label_pay.Name = "label_pay";
            this.label_pay.Size = new System.Drawing.Size(70, 14);
            this.label_pay.TabIndex = 65;
            this.label_pay.Text = "采购支出:";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::LocalERP.Properties.Resources.selectall;
            this.pictureBox3.Location = new System.Drawing.Point(288, 34);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(20, 21);
            this.pictureBox3.TabIndex = 64;
            this.pictureBox3.TabStop = false;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label14.Location = new System.Drawing.Point(310, 35);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(37, 14);
            this.label14.TabIndex = 63;
            this.label14.Text = "支出";
            // 
            // label_sumReceipt
            // 
            this.label_sumReceipt.AutoSize = true;
            this.label_sumReceipt.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label_sumReceipt.Location = new System.Drawing.Point(43, 181);
            this.label_sumReceipt.Name = "label_sumReceipt";
            this.label_sumReceipt.Size = new System.Drawing.Size(45, 14);
            this.label_sumReceipt.TabIndex = 62;
            this.label_sumReceipt.Text = "合计:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(16, 156);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(238, 14);
            this.label16.TabIndex = 61;
            this.label16.Text = "_________________________________";
            // 
            // label_otherReceipt
            // 
            this.label_otherReceipt.AutoSize = true;
            this.label_otherReceipt.Location = new System.Drawing.Point(18, 125);
            this.label_otherReceipt.Name = "label_otherReceipt";
            this.label_otherReceipt.Size = new System.Drawing.Size(70, 14);
            this.label_otherReceipt.TabIndex = 59;
            this.label_otherReceipt.Text = "其他收入:";
            // 
            // label_receipt
            // 
            this.label_receipt.AutoSize = true;
            this.label_receipt.Location = new System.Drawing.Point(18, 69);
            this.label_receipt.Name = "label_receipt";
            this.label_receipt.Size = new System.Drawing.Size(70, 14);
            this.label_receipt.TabIndex = 58;
            this.label_receipt.Text = "销售收入:";
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::LocalERP.Properties.Resources.selectall;
            this.pictureBox4.Location = new System.Drawing.Point(21, 34);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(20, 21);
            this.pictureBox4.TabIndex = 57;
            this.pictureBox4.TabStop = false;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label20.Location = new System.Drawing.Point(43, 35);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(37, 14);
            this.label20.TabIndex = 56;
            this.label20.Text = "收入";
            // 
            // button1
            // 
            this.button1.Image = global::LocalERP.Properties.Resources.search_16px;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(479, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(95, 28);
            this.button1.TabIndex = 72;
            this.button1.Text = "  开始统计";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button_add_Click);
            // 
            // StatisticCashForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(759, 631);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label_notice);
            this.Controls.Add(this.statusStrip1);
            this.Font = new System.Drawing.Font("宋体", 10F);
            this.HideOnClose = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "StatisticCashForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "财务统计";
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
/*        private System.Windows.Forms.DataGridViewCheckBoxColumn check;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn tel;
        private System.Windows.Forms.DataGridViewTextBoxColumn phone;
        private System.Windows.Forms.DataGridViewTextBoxColumn address;
        private System.Windows.Forms.DataGridViewTextBoxColumn comment;*/
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Label label_notice;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label_lib;
        private System.Windows.Forms.Label label_needReceipt;
        private System.Windows.Forms.Label label_sumCash1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label_assets;
        private System.Windows.Forms.Label label_needPay;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label_debt;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label_paySum;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label_pay;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label_sumReceipt;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label_otherReceipt;
        private System.Windows.Forms.Label label_receipt;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label_otherPay;
        private System.Windows.Forms.Label label_sumCash;
        private System.Windows.Forms.Label label_sellBack;
        private System.Windows.Forms.Label label_purchaseBack;
    }
}