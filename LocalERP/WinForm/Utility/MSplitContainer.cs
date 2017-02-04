using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace WindowsControlLibrary1
{
    /// <summary>
    /// 确定splitter的一部分如何进行收起操作
    /// </summary>
    public enum splitterFangXiang
    {
        /// <summary>
        /// 无收起按钮
        /// </summary>
        None,
        /// <summary>
        /// 右边收起
        /// </summary>
        Right,
        /// <summary>
        /// 左边收起
        /// </summary>
        Left,
        /// <summary>
        /// 上边收起
        /// </summary>
        Up,
        /// <summary>
        /// 下边收起
        /// </summary>
        Bottom
    }


    public partial class MSplitContainer : SplitContainer
    {
        public MSplitContainer()
        {
            InitializeComponent();
            SetSplitterButtonRectangle();
            
            this.splitterButtonBrush = this.splitterButtonBrushOff;

            this.SetStyle(ControlStyles.Selectable, false);
        }
        /// <summary>
        /// Panel如何收起(方向)
        /// </summary>
        private splitterFangXiang splitterFX = splitterFangXiang.Right;
        /// <summary>
        /// 获取或设置 Panel如何收起(方向)
        /// </summary>
        [Description("获取或设置Panel收起方向"), Category("扩展")]
        public splitterFangXiang SplitterFX
        {
            get { return splitterFX; }
            set
            {
                switch (value)
                {
                    case splitterFangXiang.Right:
                        this.Orientation = Orientation.Vertical;
                        this.FixedPanel = FixedPanel.Panel2;
                        break;
                    case splitterFangXiang.Left:
                        this.Orientation = Orientation.Vertical;
                        this.FixedPanel = FixedPanel.Panel1;
                        break;
                    case splitterFangXiang.Up:
                        this.Orientation = Orientation.Horizontal;
                        this.FixedPanel = FixedPanel.Panel1;
                        break;
                    case splitterFangXiang.Bottom:
                        this.Orientation = Orientation.Horizontal;
                        this.FixedPanel = FixedPanel.Panel2;
                        break;
                }
                splitterFX = value;
                setFXStr();
            }
        }

        private int splitterMinvalue = 0;
        /// <summary>
        /// 获取或设置边侧栏展开后的最小宽度或高度
        /// </summary>
        [Description("获取或设置边侧栏展开后的最小宽度或高度"), Category("扩展")]
        public int SplitterMinvalue
        {
            get { return splitterMinvalue; }
            set { splitterMinvalue = value; }
        }

        private bool splitterShouQi = false;
        /// <summary>
        /// 获取获设置边侧栏是否已收起
        /// </summary>
        [Description("获取或设置边栏是否收起"), Category("扩展")]
        public bool SplitterShouQi
        {
            get
            {

                return splitterShouQi;
            }
            set
            {
                if (this.splitterShouQi == value)
                {
                    return;
                }
                splitterShouQi = value;
                if (value)
                {
                    this.splitterLine = this.SplitterDistance;
                    switch (this.splitterFX)
                    {
                        case splitterFangXiang.Right:
                            this.SplitterDistance = this.Width - this.SplitterWidth;
                            break;
                        case splitterFangXiang.Bottom:
                            this.SplitterDistance = this.Height - this.SplitterWidth;
                            break;
                        case splitterFangXiang.Left:
                        case splitterFangXiang.Up:
                            this.SplitterDistance = 0;
                            break;
                    }
                    this.IsSplitterFixed = true;
                }
                else
                {
                    this.SplitterDistance = this.splitterLine;
                    this.IsSplitterFixed = false;
                }
                setFXStr();

            }
        }

        private Image splitterButtonBackGround;
        /// <summary>
        /// 分割调整按钮背景图
        /// </summary>
        [Description("分割调整按钮背景图"), Category("扩展")]
        public Image SplitterButtonBackGround
        {
            get { return splitterButtonBackGround; }
            set { splitterButtonBackGround = value; }
        }
        private Image splitterButtonBackGroundHover;
        /// <summary>
        /// 分割调整按钮背景图-鼠标在上方时
        /// </summary>
        [Description("分割调整按钮背景图-鼠标在上方时"), Category("扩展")]
        public Image SplitterButtonBackGroundHover
        {
            get { return splitterButtonBackGroundHover; }
            set { splitterButtonBackGroundHover = value; }
        }


        /// <summary>
        /// 字段-按钮区域
        /// </summary>
        private Rectangle splitterButtonRectangle = new Rectangle(0, 0, 10, 10);
        /// <summary>
        /// 字段 -- 鼠标是否在按钮上
        /// </summary>
        private bool splitterButtonMouseHover = false;
        /// <summary>
        /// 私有属性 -- 鼠标是否位于分割按钮上
        /// </summary>
        private bool SplitterButtonMouseHover
        {
            get { return splitterButtonMouseHover; }
            set
            {
                if (value == splitterButtonMouseHover)
                {
                    return;
                }
                if (value)
                {
                    //this.splitterButtonBrush.Dispose();
                    //this.splitterButtonBrush = new LinearGradientBrush(this.splitterButtonRectangle, Color.DimGray, Color.Gray, LinearGradientMode.ForwardDiagonal);
                    this.splitterButtonBrush = this.splitterButtonBrushOn;
                    this.Cursor = Cursors.Hand;
                }
                else
                {
                    //this.splitterButtonBrush.Dispose();
                    //this.splitterButtonBrush = new LinearGradientBrush(this.splitterButtonRectangle, Color.LightGray, Color.Gray, LinearGradientMode.BackwardDiagonal);
                    this.splitterButtonBrush = this.splitterButtonBrushOff;
                    this.Cursor = Cursors.Default;
                }
                splitterButtonMouseHover = value;
                this.Invalidate(this.splitterButtonRectangle);
            }
        }
        /// <summary>
        /// 字段 -- 按扭笔刷
        /// </summary>
        //private Brush splitterButtonBrush = new LinearGradientBrush(new Rectangle(0, 0, 50, 50), Color.LightGray, Color.Gray, LinearGradientMode.ForwardDiagonal);
        private Brush splitterButtonBrushOff = new SolidBrush(Color.DimGray);
        private Brush splitterButtonBrushOn = new LinearGradientBrush(new Rectangle(0, 0, 50, 50), Color.Gray, Color.LightGray, LinearGradientMode.ForwardDiagonal);
        private Brush splitterNoticeBrush = new SolidBrush(Color.Black);
        private Brush splitterButtonBrush = null;
        /// <summary>
        /// 鼠标移动 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.X > this.splitterButtonRectangle.X && e.X < this.splitterButtonRectangle.Width + this.splitterButtonRectangle.X && e.Y > this.splitterButtonRectangle.Y && e.Y < this.splitterButtonRectangle.Y + this.splitterButtonRectangle.Height)
            {
                this.SplitterButtonMouseHover = true;
            }
            else
            {
                this.SplitterButtonMouseHover = false;
            }
            base.OnMouseMove(e);
        }
        /// <summary>
        /// 鼠标离开
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeave(EventArgs e)
        {
            this.SplitterButtonMouseHover = false;
            base.OnMouseLeave(e);
        }
        /// <summary>
        /// 重写--鼠标按下事件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            /**********如果鼠标不在按钮之上**********/
            if (this.splitterButtonMouseHover == false)
            {
                base.OnMouseDown(e);
            }

        }

        /// <summary>
        /// 重写 分割线移动后事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MSplitContainer_SplitterMoved(object sender, SplitterEventArgs e)
        {
            switch (this.splitterFX)
            {
                case splitterFangXiang.Right:
                    if (this.splitterShouQi == false && this.SplitterDistance > this.Width - this.splitterMinvalue)
                    {
                        this.SplitterDistance = this.Width - this.splitterMinvalue;
                    }
                    break;
                case splitterFangXiang.Left:
                    if (this.splitterShouQi == false && this.SplitterDistance < this.splitterMinvalue)
                    {
                        this.SplitterDistance = this.splitterMinvalue;
                    }
                    break;
                case splitterFangXiang.Bottom:
                    if (this.splitterShouQi == false && this.SplitterDistance > this.Height - this.splitterMinvalue)
                    {
                        this.SplitterDistance = this.Height - this.splitterMinvalue;
                    }
                    break;
                case splitterFangXiang.Up:
                    if (this.SplitterDistance > this.splitterMinvalue)
                    {
                        this.SplitterDistance = this.splitterMinvalue;
                    }
                    break;
            }
            SetSplitterButtonRectangle();
            this.Invalidate(this.SplitterRectangle);

        }
        /// <summary>
        /// 设置按扭区域的方法
        /// </summary>
        private void SetSplitterButtonRectangle()
        {
            if (this.Orientation == Orientation.Vertical)
            {
                this.splitterButtonRectangle = new Rectangle(SplitterRectangle.X, SplitterRectangle.Height / 2 - 30, this.SplitterRectangle.Width, 60);
            }
            else
            {
                this.splitterButtonRectangle = new Rectangle(SplitterRectangle.Width / 2 - 30, SplitterRectangle.Y, 60, this.SplitterWidth);
            }

        }
        string FXstr = "-";
        /// <summary>
        /// 重写-重绘事件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(this.splitterButtonBrush, this.splitterButtonRectangle);

            if (this.splitterButtonMouseHover && this.splitterButtonBackGroundHover != null)
            {
                e.Graphics.DrawImage(this.SplitterButtonBackGroundHover, this.splitterButtonRectangle);
            }
            else if (this.splitterButtonBackGround != null)
            {
                e.Graphics.DrawImage(this.SplitterButtonBackGround, this.splitterButtonRectangle);
            }

            //using (Brush br = new LinearGradientBrush(this.splitterButtonRectangle, Color.Purple, Color.Green, LinearGradientMode.BackwardDiagonal))
            //{
                using (StringFormat sf = new StringFormat())
                {
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;
                    e.Graphics.DrawString(FXstr, this.Font, splitterNoticeBrush, this.splitterButtonRectangle, sf);
                }

            //}

            base.OnPaint(e);
        }
        /// <summary>
        /// 设置方向文本
        /// </summary>
        private void setFXStr()
        {
            if ((this.splitterShouQi && this.splitterFX == splitterFangXiang.Right) || (this.splitterShouQi == false) && this.splitterFX == splitterFangXiang.Left)
            {
                FXstr = "<";
            }
            else if ((this.splitterShouQi == false && this.splitterFX == splitterFangXiang.Right) || (this.splitterShouQi && this.splitterFX == splitterFangXiang.Left))
            {
                FXstr = ">";
            }
            else if ((this.splitterShouQi && this.splitterFX == splitterFangXiang.Up) || (this.splitterShouQi == false && this.splitterFX == splitterFangXiang.Bottom))
            {
                FXstr = "∨";
            }
            else
            {
                FXstr = "∧";
            }
            //this.Invalidate();
        }

        /// <summary>
        /// 边框栏不展开时的 SptterDistance 值
        /// </summary>
        private int splitterLine = 0;

        /// <summary>
        /// 将边侧栏收起或展开
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (this.splitterButtonMouseHover)
            {
                this.SplitterShouQi = !this.SplitterShouQi;
            }

        }



    }


}
