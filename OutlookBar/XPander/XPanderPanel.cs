using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Windows.Forms;
using System.ComponentModel.Design;

namespace BSE.Windows.Forms
{
    #region Partial class XPanderPanel
	/// <summary>
	/// Used to group collections of controls. 
	/// </summary>
	/// <remarks>
	/// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
	/// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
	/// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR
	/// PURPOSE. IT CAN BE DISTRIBUTED FREE OF CHARGE AS LONG AS THIS HEADER 
	/// REMAINS UNCHANGED.
	/// </remarks>
	/// <copyright>Copyright © 2006-2007 Uwe Eichkorn</copyright>
    [Designer(typeof(XPanderPanelDesigner))]
    [DesignTimeVisible(false)]
	public partial class XPanderPanel : BasePanel
	{
		#region EventsPublic
		/// <summary>
		/// Occurs when the caption of the xpanderpanel is clicked. 
		/// </summary>
        [Description("Occurs when the caption of the xpanderpanel is clicked.")]
        public event EventHandler<XPanderPanelClickEventArgs> CaptionClick;
		#endregion
		
		#region Constants
		#endregion

		#region FieldsPrivate
		
		private BSE.Windows.Forms.PanelStyle m_ePanelStyle;
		private System.Drawing.Image m_imageChevron;
        private System.Drawing.Image m_imageChevronUp;
        private System.Drawing.Image m_imageChevronDown;
        private Color m_colorChevron;
        private bool m_bExpand;

		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the caption style in this xpanderpanel.
		/// </summary>
		[DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden)]
		public override BSE.Windows.Forms.PanelStyle PanelStyle
		{
			get {return this.m_ePanelStyle;}
			set
			{
                if (value != this.m_ePanelStyle)
                {
                    this.m_ePanelStyle = value;
                    this.Invalidate();
                }
			}
		}
        /// <summary>
        /// The foreground color of this component, which is used to display the caption text.
        /// </summary>
        [Description("The foreground color of this component, which is used to display the caption text.")]
        [DefaultValue(typeof(SystemColors), "System.Drawing.SystemColors.ActiveCaptionText")]
        [Category("Appearance")]
        public override Color CaptionForeColor
        {
            get { return base.CaptionForeColor; }
            set
            {
                if (value != base.CaptionForeColor)
                {
                    base.CaptionForeColor = value;
                    this.m_colorChevron = Color.FromArgb(255,
                        Math.Min(255, (int)((float)(base.CaptionForeColor.R) / (float)(1.01))),
                        Math.Min(255, (int)((float)(base.CaptionForeColor.G) / (float)(1.01))),
                        Math.Min(255, (int)((float)(base.CaptionForeColor.B) / (float)(1.01))));
                    this.Invalidate(this.CaptionRectangle);
                }
            }
        }
		/// <summary>
		/// Expands this xpanderpanel in it's xpanderpanellist
		/// </summary>
		[Description("Expand this XpanderPanel")]
		[DefaultValue(false)]
		[Category("Appearance")]
		public bool Expand
		{
			get {return this.m_bExpand;}
			set
			{
                if (value != this.m_bExpand)
                {
                    this.m_bExpand = value;
                    if (this.DesignMode == true)
                    {
                        if (this.m_bExpand == true)
                        {
                            if (this.CaptionClick != null)
                            {
                                this.CaptionClick(this, new XPanderPanelClickEventArgs(this.m_bExpand));
                            }
                        }
                    }
                    this.Invalidate();
                }
			}
		}
        internal Color ChevronColor
        {
            get { return this.m_colorChevron; }
        }
		#endregion

		#region MethodsPublic
		/// <summary>
		/// Initializes a new instance of the XPanderPanel class.
		/// </summary>
		public XPanderPanel()
		{
			InitializeComponent();

			this.CaptionForeColor = SystemColors.ControlText;
            this.BackColor = SystemColors.Window;
            this.ForeColor = SystemColors.ControlText;
            this.DockPadding.Top = this.CaptionHeight;
			this.Height = this.CaptionHeight;
			this.ShowBorder = true;

            this.m_imageChevronUp = global::BSE.Windows.Forms.Properties.Resources.chevron_up;
            this.m_imageChevronDown = global::BSE.Windows.Forms.Properties.Resources.chevron_down;
		}

		#endregion

		#region MethodsProtected
		/// <summary>
		/// Paints the background of the control.
		/// </summary>
		/// <param name="pevent">A PaintEventArgs that contains information about the control to paint.</param>
		protected override void OnPaintBackground(PaintEventArgs pevent)
		{
			base.OnPaintBackground(pevent);
		}
		/// <summary>
		/// Raises the Paint event.
		/// </summary>
		/// <param name="e">A PaintEventArgs that contains the event data.</param>
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
            switch (this.m_ePanelStyle)
            {
                case PanelStyle.None:
                case PanelStyle.Default:
					DrawStyleDefault(e);
                    CalculatePanelHeights();
					DrawBorder(e);
                    break;
                case PanelStyle.Aqua:
					DrawStyleAqua(e);
                    CalculatePanelHeights();
                    break;
            }
		}
		/// <summary>
		/// Raises the OnMouseMove event.
		/// </summary>
		/// <param name="e">A MouseEventArgs that contains the event data.</param>
		protected override void OnMouseMove(System.Windows.Forms.MouseEventArgs e)
		{
            base.OnMouseMove(e);
            //Change cursor to hand when mouse moves over caption area.
			if (e.Y <= this.CaptionHeight)
			{
				Cursor.Current = Cursors.Hand;
			}
			else
			{
				Cursor.Current = Cursors.Default;
			}
		}
		/// <summary>
		/// Raises the MouseDown event.
		/// </summary>
		/// <param name="e">A MouseEventArgs that contains data about the OnMouseDown event.</param>
		protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
		{
            base.OnMouseDown(e);
            //Don't do anything if did not click on caption.
			if (e.Y > this.CaptionHeight)
			{
				return;
			}

			if (this.m_bExpand == false)
			{
				this.m_bExpand = true;
                if (this.CaptionClick != null)
                {
                    this.CaptionClick(this, new BSE.Windows.Forms.XPanderPanelClickEventArgs(this.m_bExpand));
                }
			}
		}
		/// <summary>
		/// Raises the VisibleChanged event.
		/// </summary>
		/// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            if (this.DesignMode == true)
            {
                return;
            }
            if (this.Visible == false)
            {
                if (this.Expand == true)
                {
                    this.Expand = false;
                    foreach (Control control in this.Parent.Controls)
                    {
                        BSE.Windows.Forms.XPanderPanel xpanderPanel =
                            control as BSE.Windows.Forms.XPanderPanel;

                        if (xpanderPanel != null)
                        {
                            if (xpanderPanel.Visible == true)
                            {
                                xpanderPanel.Expand = true;
                                return;
                            }
                        }
                    }
                }
            }
#if DEBUG
            //System.Diagnostics.Trace.WriteLine("Visibility: " + this.Name + this.Visible);
#endif
            CalculatePanelHeights();
        }

		#endregion

		#region MethodsPrivate

		private void DrawStyleDefault(PaintEventArgs e)
		{
            Rectangle rectangle = this.CaptionRectangle;
            Color colorGradientBegin = ProfessionalColors.ToolStripGradientBegin;
            Color colorGradientMiddle = ProfessionalColors.ToolStripGradientMiddle;
            Color colorGradientEnd = ProfessionalColors.ToolStripGradientEnd;

            if (this.ColorScheme == ColorScheme.Custom)
            {
                colorGradientBegin = this.ColorCaptionGradientBegin;
                colorGradientMiddle = this.ColorCaptionGradientMiddle;
                colorGradientEnd = this.ColorCaptionGradientEnd;
            }

            if (this.m_bExpand == true)
            {
                if (this.ColorScheme == ColorScheme.Professional)
                {
                    colorGradientBegin = ProfessionalColors.ButtonCheckedGradientBegin;
                    colorGradientMiddle = ProfessionalColors.ButtonCheckedGradientMiddle;
                    colorGradientEnd = ProfessionalColors.ButtonCheckedGradientEnd;
                }
                this.m_imageChevron = this.m_imageChevronUp;
            }
            else
            {
                this.m_imageChevron = this.m_imageChevronDown;
            }
            
			RenderDoubleBackgroundGradient(
				e.Graphics,
				rectangle,
				colorGradientBegin,
				colorGradientMiddle,
				colorGradientEnd,
				LinearGradientMode.Vertical,
				false);

			int iTextPositionX = CaptionSpacing;
			if (this.Image != null)
			{
				Rectangle imageRectangle = this.ImageRectangle;
				if (this.RightToLeft == RightToLeft.No)
				{
					DrawImage(e.Graphics, this.Image, imageRectangle);
					iTextPositionX += this.ImageSize.Width + CaptionSpacing;
				}
				else
				{
					imageRectangle.X = rectangle.Right - RightImagePositionX;
					DrawImage(e.Graphics, this.Image, imageRectangle);
				}
			}
			Rectangle textRectangle = rectangle;
			textRectangle.X = iTextPositionX;
			textRectangle.Width -= iTextPositionX + CaptionSpacing;
			if (this.RightToLeft == RightToLeft.Yes)
			{
				if (this.Image != null)
				{
					textRectangle.Width -= RightImagePositionX;
				}
			}
			DrawString(e.Graphics, textRectangle, this.CaptionFont, this.CaptionForeColor, this.Text, this.RightToLeft);

            if (this.ShowBorder == true)
            {
                if (this.Expand == true)
                {
                    using (SolidBrush borderBrush = new SolidBrush(System.Windows.Forms.ProfessionalColors.ToolStripBorder))
                    {
                        using (Pen borderPen = new Pen(borderBrush, 1))
                        {
                            e.Graphics.DrawLine(
                                borderPen,
                                this.ClientRectangle.Left,
                                this.CaptionHeight,
                                this.ClientRectangle.Width,
                                this.CaptionHeight);
                        }
                    }
                }
            }
		}

        private void DrawStyleAqua(PaintEventArgs e)
        {
            Color colorGradientBegin = ProfessionalColors.ToolStripGradientBegin;
            Color colorGradientEnd = ProfessionalColors.ToolStripGradientEnd;

            if (this.ColorScheme == ColorScheme.Custom)
            {
                colorGradientBegin = this.ColorCaptionGradientBegin;
                colorGradientEnd = this.ColorCaptionGradientEnd;
            }
            // defines the chevron direction
            if (this.m_bExpand == true)
            {
                this.m_imageChevron = this.m_imageChevronUp;
            }
            else
            {
                this.m_imageChevron = this.m_imageChevronDown;
            }
            Rectangle outerRectangle = this.CaptionRectangle;

            using (GraphicsPath outerRectangleGraphicsPath = GetBackgroundPath(outerRectangle, 20))
            {
                using (LinearGradientBrush outerLinearGradientBrush = new LinearGradientBrush(
                    outerRectangle,
                    colorGradientBegin,
                    colorGradientEnd,
                    LinearGradientMode.Vertical))
                {
                    e.Graphics.FillPath(outerLinearGradientBrush, outerRectangleGraphicsPath); //draw top bubble
                }
            }
            //
            // Create top water color to give "aqua" effect
            // 
            Rectangle innerRectangle = outerRectangle;
            innerRectangle.Height = 14;

			using (GraphicsPath innerRectangleGraphicsPath = GetPath(innerRectangle, 20))
			{
                using (LinearGradientBrush innerRectangleBrush = new LinearGradientBrush(
                    innerRectangle,
                    Color.FromArgb(255, Color.White),
                    Color.FromArgb(32, Color.White),
                    LinearGradientMode.Vertical))
				{
					//
					// draw shapes
					//
					e.Graphics.FillPath(innerRectangleBrush, innerRectangleGraphicsPath); //draw top bubble
				}
			}
			//DrawImage
            int iTextPositionX1 = CaptionSpacing;
			Rectangle imageRectangleLeft = this.ImageRectangle;
            if (RightToLeft == RightToLeft.No)
            {
                if (this.Image != null)
                {
                    DrawImage(e.Graphics, this.Image, imageRectangleLeft);
                    iTextPositionX1 += this.ImageSize.Width + CaptionSpacing;
                }
            }
            else
            {
                DrawChevron(e.Graphics, this.m_imageChevron,imageRectangleLeft, this.ChevronColor);
				iTextPositionX1 += this.ImageSize.Width + CaptionSpacing;
            }
            //
            // Draw Caption text
            //
            Rectangle textRectangle = outerRectangle;
			textRectangle.X = iTextPositionX1;
			textRectangle.Width = outerRectangle.Width - textRectangle.X - RightImagePositionX - CaptionSpacing;
			int iTextPositionX2 = outerRectangle.Right - RightImagePositionX;
            Rectangle imageRectangleRight = this.ImageRectangle;
			imageRectangleRight.X = iTextPositionX2;
			if (RightToLeft == RightToLeft.No)
            {
                DrawChevron(e.Graphics, this.m_imageChevron, imageRectangleRight, this.ChevronColor);
            }
            else
            {
				if (this.Image != null)
				{
					DrawImage(e.Graphics, this.Image, imageRectangleRight);
				}
				else
				{
					textRectangle.Width += CaptionSpacing + this.ImageSize.Width;
				}
            }
			DrawString(e.Graphics, textRectangle, this.CaptionFont, this.CaptionForeColor, this.Text, this.RightToLeft);
        }

		private void DrawBorder(PaintEventArgs e)
		{
			if (this.ShowBorder == true)
			{
                Rectangle borderRectangle = this.ClientRectangle;
                borderRectangle.Inflate(0, 1);
                borderRectangle.Offset(0, -1);

                ControlPaint.DrawBorder(
                    e.Graphics,
                    borderRectangle,
                    System.Windows.Forms.ProfessionalColors.ToolStripBorder,
                     ButtonBorderStyle.Solid);
			}
		}

		private void CalculatePanelHeights()
		{
			if (this.Parent == null)
			{
				return;
			}

            int iPanelHeight = this.Parent.Padding.Top;

            foreach (Control control in this.Parent.Controls)
            {
				BSE.Windows.Forms.XPanderPanel xpanderPanel =
					control as BSE.Windows.Forms.XPanderPanel;

				if (xpanderPanel != null)
				{
					if (xpanderPanel.Visible == true)
					{
						iPanelHeight += this.CaptionHeight;
					}
				}
            }

			iPanelHeight += this.Parent.Padding.Bottom;

            foreach (Control control in this.Parent.Controls)
			{
				BSE.Windows.Forms.XPanderPanel xpanderPanel =
					control as BSE.Windows.Forms.XPanderPanel;

                if (xpanderPanel != null)
                {
                    if (xpanderPanel.Expand == true)
                    {
                        xpanderPanel.Height = this.Parent.Height
                            + this.CaptionHeight
                            - iPanelHeight;
                    }
                    else
                    {
                        xpanderPanel.Height = this.CaptionHeight;
                    }
                }
			}

			int iTop = this.Parent.Padding.Top;
			foreach (Control control in this.Parent.Controls)
			{
				BSE.Windows.Forms.XPanderPanel xpanderPanel =
					control as BSE.Windows.Forms.XPanderPanel;

				if (xpanderPanel != null)
			    {
					if (xpanderPanel.Visible == true)
					{
						xpanderPanel.Top = iTop;
						iTop += xpanderPanel.Height;
					}
			    }
			}
		}

		#endregion
    }

    #endregion

    #region Class XPanderPanelDesigner

	internal class XPanderPanelDesigner : System.Windows.Forms.Design.ScrollableControlDesigner
	{
		#region FieldsPrivate

		private Pen m_BorderPen = new Pen(Color.FromKnownColor(KnownColor.ControlDarkDark));

		#endregion

		#region MethodsPublic
		/// <summary>
		/// 
		/// </summary>
		public XPanderPanelDesigner()
		{
			this.m_BorderPen.DashStyle = DashStyle.Dash;
		}
		/// <summary>
		/// Initializes the designer with the specified component.
		/// </summary>
		/// <param name="component">The IComponent to associate with the designer.</param>
		public override void Initialize(IComponent component)
		{
			base.Initialize(component);
		}
		/// <summary>
		/// Gets the selection rules that indicate the movement capabilities of a component.
		/// </summary>
		public override System.Windows.Forms.Design.SelectionRules SelectionRules
		{
			get
			{
				System.Windows.Forms.Design.SelectionRules selectionRules
					= System.Windows.Forms.Design.SelectionRules.None;

				return selectionRules;
			}
		}

		#endregion

		#region MethodsProtected
		/// <summary>
		/// Receives a call when the control that the designer is managing has painted its surface so the designer can
		/// paint any additional adornments on top of the xpanderpanel.
		/// </summary>
		/// <param name="e">A PaintEventArgs the designer can use to draw on the xpanderpanel.</param>
		protected override void OnPaintAdornments(PaintEventArgs e)
		{
			base.OnPaintAdornments(e);
			e.Graphics.DrawRectangle(
				this.m_BorderPen,
				0,
				0,
				this.Control.Width - 2,
				this.Control.Height - 2);
		}
		/// <summary>
		/// Allows a designer to change or remove items from the set of properties that it exposes through a <see cref="TypeDescriptor">TypeDescriptor</see>. 
		/// </summary>
		/// <param name="properties">The properties for the class of the component.</param>
		protected override void PostFilterProperties(IDictionary properties)
		{
			base.PostFilterProperties(properties);
			properties.Remove("AccessibilityObject");
			properties.Remove("AccessibleDefaultActionDescription");
			properties.Remove("AccessibleDescription");
			properties.Remove("AccessibleName");
			properties.Remove("AccessibleRole");
			properties.Remove("AllowDrop");
            properties.Remove("Anchor");
            properties.Remove("AntiAliasing");
			properties.Remove("AutoScroll");
			properties.Remove("AutoScrollMargin");
			properties.Remove("AutoScrollMinSize");
			properties.Remove("BackgroundImage");
			properties.Remove("BackgroundImageLayout");
			properties.Remove("CausesValidation");
			properties.Remove("ContextMenuStrip");
			properties.Remove("Dock");
			properties.Remove("GenerateMember");
			properties.Remove("ImeMode");
            properties.Remove("Location");
			properties.Remove("MaximumSize");
			properties.Remove("MinimumSize");
            properties.Remove("Size");
		}

		#endregion
	}

    #endregion

}
