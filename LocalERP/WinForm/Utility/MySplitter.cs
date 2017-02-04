using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.IO;

namespace LocalERP.WinForm.Utility
{
    public partial class MySplitter : Splitter
    {
        public MySplitter()
        {
            InitializeComponent();
        }

        public MySplitter(IContainer container)
        {
            noticeSF = new StringFormat();
            noticeSF.Alignment = StringAlignment.Center;
            noticeSF.LineAlignment = StringAlignment.Center;
            noticeString = "<";

            container.Add(this);
            InitializeComponent();
            this.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(MySplitter_SplitterMoved);
            this.Resize += new EventHandler(MySplitter_Resize);
        }

        private Rectangle splitterButtonRectangle = new Rectangle();

        private StringFormat noticeSF;
        private String noticeString;
        private Brush splitterButtonBrush = new SolidBrush(Color.Gray);
        private Brush splitterButtonBrushOn = new SolidBrush(Color.LightBlue); //new LinearGradientBrush(new Rectangle(0, 0, 100, 100), Color.Gray, Color.LightGray, LinearGradientMode.ForwardDiagonal);
        private Brush splitterNoticeBrush = new SolidBrush(Color.Black);

        private int splitPositionMemory = 0;

        private bool splitterHide = false;
        [Description("获取或设置边栏是否收起"), Category("扩展")]
        public bool SplitterHide
        {
            get{return splitterHide;}
            set
            {
                if (this.splitterHide == value)
                    return;
                splitterHide = value;
                if (value)
                {
                    //this.splitPositionMemory = this.SplitPosition;
                    this.SplitPosition = 0;
                    //File.AppendAllText("e:\\debug.txt","hide. mem=" + this.splitPositionMemory.ToString() + "\r\n");
                }
                else
                {
                    this.SplitPosition = 142;
                    //File.AppendAllText("e:\\debug.txt", "expand. mem=" + this.splitPositionMemory.ToString() + "\r\n");
                    //this.IsSplitterFixed = false;
                }
                this.setNoticeString();
                this.Invalidate(this.splitterButtonRectangle);
            }
        }


        void MySplitter_Resize(object sender, EventArgs e)
        {
            this.SetSplitterButtonRectangle();
            this.Invalidate(this.ClientRectangle);
        }

        void MySplitter_SplitterMoved(object sender, System.Windows.Forms.SplitterEventArgs e)
        {
            //this.SetSplitterButtonRectangle();
            //this.Invalidate(this.splitterButtonRectangle);
            //File.AppendAllText("e:\\debug.txt", "move. split_position=" + this.SplitPosition.ToString() + "\r\n");
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (this.splitterButtonMouseHover)
                this.SplitterHide = !this.splitterHide;
        }

        private void SetSplitterButtonRectangle()
        {
            this.splitterButtonRectangle.X = 0;//.ClientRectangle.X;
            this.splitterButtonRectangle.Y = this.Height / 2 - 50;// this.ClientRectangle.Height / 2 - 50;
            this.splitterButtonRectangle.Width = this.Width;// this.ClientRectangle.Width;
            this.splitterButtonRectangle.Height = 100;

            //File.AppendAllText("e:\\debug.txt", this.splitterButtonRectangle.ToString() + "\r\n");
        }

        private bool splitterButtonMouseHover = false;
        private bool SplitterButtonMouseHover
        {
            get { return splitterButtonMouseHover; }
            set
            {
                if (value == splitterButtonMouseHover)
                    return;
                if (value)
                    this.Cursor = Cursors.Hand;
                else
                    this.Cursor = Cursors.VSplit;
                
                splitterButtonMouseHover = value;
                this.Invalidate(this.splitterButtonRectangle);
            }
        }

        //重写鼠标移动
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.X >= this.splitterButtonRectangle.X && 
                e.X <= this.splitterButtonRectangle.Width + this.splitterButtonRectangle.X &&
                e.Y >= this.splitterButtonRectangle.Y && 
                e.Y <= this.splitterButtonRectangle.Y + this.splitterButtonRectangle.Height)
                this.SplitterButtonMouseHover = true;
            else
                this.SplitterButtonMouseHover = false;
            base.OnMouseMove(e);
        }
        //重写
        protected override void OnMouseLeave(EventArgs e)
        {
            this.SplitterButtonMouseHover = false;
            base.OnMouseLeave(e);
        }

        private void setNoticeString()
        {
            if (this.SplitterHide)
                noticeString = ">";
            else
                noticeString = "<";
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if(!SplitterButtonMouseHover)
                e.Graphics.FillRectangle(this.splitterButtonBrush, this.splitterButtonRectangle);
            else
                e.Graphics.FillRectangle(this.splitterButtonBrushOn, this.splitterButtonRectangle);
            
            //using(noticeString){
                e.Graphics.DrawString(noticeString, this.Font, splitterNoticeBrush, this.splitterButtonRectangle, this.noticeSF);
            //}
            base.OnPaint(e);
        }
    }
}
