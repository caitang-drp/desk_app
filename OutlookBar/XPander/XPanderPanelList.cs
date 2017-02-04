using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Data;
using System.Windows.Forms;

using BSE.Windows.Forms.Properties;

namespace BSE.Windows.Forms
{
    #region Partial Class XPanderPanelList
	/// <summary>
	/// Manages a related set of xpanderpanels.
	/// </summary>
	/// <remarks>
	/// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
	/// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
	/// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR
	/// PURPOSE. IT CAN BE DISTRIBUTED FREE OF CHARGE AS LONG AS THIS HEADER 
	/// REMAINS UNCHANGED.
	/// </remarks>
	/// <copyright>Copyright © 2006-2007 Uwe Eichkorn</copyright>
    [Designer(typeof(XPanderPanelListDesigner)),
	DesignTimeVisibleAttribute(true)]
	[ToolboxBitmap(typeof(System.Windows.Forms.Panel))]
	public partial class XPanderPanelList : ScrollableControl
	{

        #region FieldsPrivate

        private bool m_bShowBorder;
        private bool m_bShowGradientBackground;
        private LinearGradientMode m_linearGradientMode;
        private System.Drawing.Color m_colorGradientBackground;
		private BSE.Windows.Forms.PanelStyle m_ePanelStyle;
		private BSE.Windows.Forms.ColorScheme m_eColorScheme;
		private XPanderPanelCollection m_xpanderPanels;

		#endregion

		#region Properties
		/// <summary>
		/// Gets the collection of xpanderpanels in this xpanderpanellist.
		/// </summary>
		[RefreshProperties(RefreshProperties.Repaint),
		Category("Collections"),
		Browsable(true),
		Description("Collection containing all the XPanderPanels for the xpanderpanellist.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Editor(typeof(XPanderPanelCollectionEditor), typeof(UITypeEditor))]
		public XPanderPanelCollection XPanderPanels
		{
			get { return this.m_xpanderPanels; }
		}
		/// <summary>
		/// Specifies the style of the panels in this xpanderpanellist.
		/// </summary>
		[Description("Specifies the style of the xpanderpanels in this xpanderpanellist."),
		DefaultValue(BSE.Windows.Forms.PanelStyle.Default),
		Category("Appearance")]
		public BSE.Windows.Forms.PanelStyle PanelStyle
		{
			get { return this.m_ePanelStyle; }
			set
			{
                if (value != this.m_ePanelStyle)
                {
                    this.m_ePanelStyle = value;
                    if (this.m_ePanelStyle != PanelStyle.Aqua)
                    {
                        this.Padding = new System.Windows.Forms.Padding(0);
                    }
                    else
                    {
                        this.Padding = new System.Windows.Forms.Padding(3);
                    }

                    foreach (XPanderPanel xpanderPanel in this.XPanderPanels)
                    {
                        xpanderPanel.PanelStyle = this.m_ePanelStyle;
                        xpanderPanel.Left = this.Padding.Left;
                        xpanderPanel.Width = this.ClientRectangle.Width
                            - this.Padding.Left
                            - this.Padding.Right;
                    }
                }
			}
		}
		/// <summary>
		/// Specifies the colorscheme of the xpanderpanels in this xpanderpanellist
		/// </summary>
		[Description("Specifies the colorscheme of the xpanderpanels in this xpanderpanellist")]
        [DefaultValue(BSE.Windows.Forms.ColorScheme.Professional)]
        [Category("Appearance")]
		public ColorScheme ColorScheme
        {
            get { return this.m_eColorScheme; }
            set
            {
                if (value != this.m_eColorScheme)
                {
                    this.m_eColorScheme = value;
                    foreach (XPanderPanel xpanderPanel in this.XPanderPanels)
                    {
                        xpanderPanel.ColorScheme = this.m_eColorScheme;
                    }
                }
            }
        }
		/// <summary>
		/// LinearGradientMode of the background in this xpanderpanellist 
		/// </summary>
		[Description("LinearGradientMode of the background in this xpanderpanellist"),
        DefaultValue(LinearGradientMode.Vertical),
        Category("Appearance")]
        public LinearGradientMode LinearGradientMode
        {
            get { return this.m_linearGradientMode; }
            set
            {
                if (value != this.m_linearGradientMode)
                {
                    this.m_linearGradientMode = value;
                    this.Invalidate();
                }
            }
        }
		/// <summary>
		/// Gets or sets a value indicating whether a xpanderpanellist's gradient background is shown. 
		/// </summary>
		[Description("Gets or sets a value indicating whether a xpanderpanellist's gradient background is shown."),
        DefaultValue(false),
        Category("Appearance")]
        public bool ShowGradientBackground
        {
            get { return this.m_bShowGradientBackground; }
            set
            {
                if (value != this.m_bShowGradientBackground)
                {
                    this.m_bShowGradientBackground = value;
                    this.Invalidate();
                }
            }
        }
		/// <summary>
		/// Gets or sets a value indicating whether a xpanderpanellist's border is shown
		/// </summary>
		[Description("Gets or sets a value indicating whether a xpanderpanellist's border is shown"),
        DefaultValue(true),
        Category("Appearance")]
        public bool ShowBorder
        {
            get { return this.m_bShowBorder; }
            set
            {
                if (value != this.m_bShowBorder)
                {
                    this.m_bShowBorder = value;
                    foreach (XPanderPanel xpanderPanel in this.XPanderPanels)
                    {
                        xpanderPanel.ShowBorder = this.m_bShowBorder;
                    }
                    this.Invalidate();
                }
            }
        }
		/// <summary>
		/// Gradientcolor background in this xpanderpanellist
		/// </summary>
		[Description("Gradientcolor background in this xpanderpanellist"),
        DefaultValue(false),
        Category("Appearance")]
        public System.Drawing.Color GradientBackground
        {
            get { return this.m_colorGradientBackground; }
            set
            {
                if (value != this.m_colorGradientBackground)
                {
                    this.m_colorGradientBackground = value;
                    this.Invalidate();
                }
            }
        }
		#endregion

		#region MethodsPublic
		/// <summary>
		/// Initializes a new instance of the XPanderPanelList class.
		/// </summary>
		public XPanderPanelList()
		{
			// Dieser Aufruf ist für den Windows Form-Designer erforderlich.
			SetStyle(ControlStyles.DoubleBuffer, true);
			SetStyle(ControlStyles.ResizeRedraw, false);
			SetStyle(ControlStyles.UserPaint, true);
			SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			SetStyle(ControlStyles.SupportsTransparentBackColor, true);
			
			InitializeComponent();

            this.m_xpanderPanels = new XPanderPanelCollection(this);
            
            this.ShowBorder = true;
            this.PanelStyle = PanelStyle.Default;
            this.LinearGradientMode = LinearGradientMode.Vertical;
		}
		/// <summary>
		/// expands the specified xpanderpanel
		/// </summary>
		/// <param name="xpanderPanel">xpanderpanel for expand</param>
        public void Expand(XPanderPanel xpanderPanel)
        {
			if (xpanderPanel == null)
			{
				throw new ArgumentException(
					string.Format(System.Globalization.CultureInfo.CurrentUICulture,
					Resources.IDS_ArgumentException,
					typeof(XPanderPanel).Name));
			}
			else
			{
				foreach (XPanderPanel tmpXPanderPanel in this.m_xpanderPanels)
				{
					if (tmpXPanderPanel.Equals(xpanderPanel) == false)
					{
						tmpXPanderPanel.Expand = false;
					}
				}
				xpanderPanel.Expand = true;
			}
        }
		#endregion

		#region MethodsProtected
		/// <summary>
		/// Paints the background of the xpanderpanellist.
		/// </summary>
		/// <param name="pevent">A PaintEventArgs that contains information about the control to paint.</param>
		protected override void OnPaintBackground(PaintEventArgs pevent)
        {
			base.OnPaintBackground(pevent);
            if (this.m_bShowGradientBackground == true)
            {
                Rectangle rectangle = new Rectangle(0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height);
                using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(
                    rectangle,
                    this.BackColor,
                    this.GradientBackground,
                    this.LinearGradientMode))
                {
					pevent.Graphics.FillRectangle(linearGradientBrush, rectangle);
                }
            }
        }
		/// <summary>
		/// Raises the ControlAdded event.
		/// </summary>
		/// <param name="e">A ControlEventArgs that contains the event data.</param>
		protected override void OnControlAdded(System.Windows.Forms.ControlEventArgs e)
		{
			base.OnControlAdded(e);
			BSE.Windows.Forms.XPanderPanel xpanderPanel = e.Control as BSE.Windows.Forms.XPanderPanel;
			if (xpanderPanel != null)
			{
				if (xpanderPanel.Expand == true)
				{
					foreach (XPanderPanel tmpXPanderPanel in this.XPanderPanels)
					{
						if (tmpXPanderPanel != xpanderPanel)
						{
							tmpXPanderPanel.Expand = false;
							tmpXPanderPanel.Height = xpanderPanel.CaptionHeight;
						}
					}
				}
                xpanderPanel.Parent = this;
				xpanderPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
				xpanderPanel.Left = this.Padding.Left;
				xpanderPanel.Width = this.ClientRectangle.Width
					- this.Padding.Left
					- this.Padding.Right;
				xpanderPanel.PanelStyle = this.PanelStyle;
                xpanderPanel.ShowBorder = this.ShowBorder;
                xpanderPanel.ColorScheme = this.ColorScheme;
				xpanderPanel.Top = this.GetTopPosition();
                xpanderPanel.CaptionClick += new EventHandler<XPanderPanelClickEventArgs>(this.XPanderPanelCaptionClick);
			}
			else
			{
				throw new InvalidOperationException("Can only add BSE.Windows.Forms.XPanderPanel");
			}
		}
		/// <summary>
		/// Raises the ControlRemoved event.
		/// </summary>
		/// <param name="e">A ControlEventArgs that contains the event data.</param>
		protected override void OnControlRemoved(System.Windows.Forms.ControlEventArgs e)
		{
			base.OnControlRemoved(e);

			BSE.Windows.Forms.XPanderPanel xpanderPanel =
				e.Control as BSE.Windows.Forms.XPanderPanel;

			if (xpanderPanel != null)
			{
                xpanderPanel.CaptionClick -= new EventHandler<XPanderPanelClickEventArgs>(this.XPanderPanelCaptionClick);
			}
		}
		/// <summary>
		/// Raises the Resize event.
		/// </summary>
		/// <param name="e">An EventArgs that contains the event data.</param>
		protected override void OnResize(System.EventArgs e)
		{
			base.OnResize(e);
			int iXPanderPanelCaptionHeight = 0;
			
			if (this.m_xpanderPanels != null)
			{
				foreach (XPanderPanel xpanderPanel in this.m_xpanderPanels)
				{
					xpanderPanel.Width = this.ClientRectangle.Width
						- this.Padding.Left
						- this.Padding.Right;
					if (xpanderPanel.Visible == false)
					{
						iXPanderPanelCaptionHeight -= xpanderPanel.CaptionHeight;
					}
					iXPanderPanelCaptionHeight += xpanderPanel.CaptionHeight;
				}

				foreach (XPanderPanel xpanderPanel in this.m_xpanderPanels)
				{
					if (xpanderPanel.Expand == true)
					{
						xpanderPanel.Height =
							this.Height
							- iXPanderPanelCaptionHeight
							- this.Padding.Top
							- this.Padding.Bottom
							+ xpanderPanel.CaptionHeight;
						return;
					}
				}
			}
		}

		#endregion

		#region MethodsPrivate

        private void XPanderPanelCaptionClick(object sender, BSE.Windows.Forms.XPanderPanelClickEventArgs e)
		{
			BSE.Windows.Forms.XPanderPanel xpanderPanel = sender as BSE.Windows.Forms.XPanderPanel;
            if (xpanderPanel != null)
			{
                this.Expand(xpanderPanel);
			}
		}

        private int GetTopPosition()
        {
			int iTopPosition = this.Padding.Top;
			int iNextTopPosition = 0;

            //The next top position is the highest top value + that controls height, with a
            //little vertical spacing thrown in for good measure
            IEnumerator enumerator = this.XPanderPanels.GetEnumerator();
            while (enumerator.MoveNext())
            {
				XPanderPanel xpanderPanel = (XPanderPanel)enumerator.Current;

				if (xpanderPanel.Visible == true)
				{
					if (iNextTopPosition == this.Padding.Top)
					{
						iTopPosition = this.Padding.Top;
					}
					else
					{
						iTopPosition = iNextTopPosition;
					}
					iNextTopPosition = iTopPosition + xpanderPanel.Height;
				}
            }
			return iTopPosition;
        }
		
        #endregion

    }

    #endregion

    #region Class XPanderPanelListDesigner

    internal class XPanderPanelListDesigner : System.Windows.Forms.Design.ParentControlDesigner
    {
        #region FieldsPrivate

        private Pen m_borderPen = new Pen(Color.FromKnownColor(KnownColor.ControlDarkDark));
        private XPanderPanelList m_xpanderPanelList;

        #endregion

        #region MethodsPublic

        public XPanderPanelListDesigner()
        {
            this.m_borderPen.DashStyle = DashStyle.Dash;
        }
		/// <summary>
		/// Initializes the designer with the specified component.
		/// </summary>
		/// <param name="component">The IComponent to associate with the designer.</param>
        public override void Initialize(System.ComponentModel.IComponent component)
        {
            base.Initialize(component);
            this.m_xpanderPanelList = (XPanderPanelList)this.Control;
            //Disable the autoscroll feature for the control during design time.  The control
            //itself sets this property to true when it initializes at run time.  Trying to position
            //controls in this control with the autoscroll property set to True is problematic.
            this.m_xpanderPanelList.AutoScroll = false;
        }
		/// <summary>
		/// This member overrides ParentControlDesigner.ActionLists
		/// </summary>
        public override DesignerActionListCollection ActionLists
        {
            get
            {
                // Create action list collection
                DesignerActionListCollection actionLists = new DesignerActionListCollection();

                // Add custom action list
                actionLists.Add(new XPanderPanelListDesignerActionList(this.Component));

                // Return to the designer action service
                return actionLists;
            }
        }

        #endregion

        #region MethodsProtected

        protected override void OnPaintAdornments(PaintEventArgs e)
        {
            base.OnPaintAdornments(e);
            e.Graphics.DrawRectangle(this.m_borderPen, 0, 0, this.m_xpanderPanelList.Width - 2, this.m_xpanderPanelList.Height - 2);
        }

        #endregion
    }

    #endregion

    #region Class XPanderPanelListDesignerActionList

    public class XPanderPanelListDesignerActionList : DesignerActionList
	{
		#region Properties
		
		[Editor(typeof(XPanderPanelCollectionEditor), typeof(UITypeEditor))]
		public XPanderPanelCollection XPanderPanels
		{
			get { return this.XPanderPanelList.XPanderPanels; }
		}
		public PanelStyle PanelStyle
		{
			get { return this.XPanderPanelList.PanelStyle; }
			set { SetProperty("PanelStyle", value); }
		}
		public ColorScheme ColorScheme
        {
            get { return this.XPanderPanelList.ColorScheme; }
            set { SetProperty("ColorScheme", value); }
        }
        public bool ShowBorder
        {
            get { return this.XPanderPanelList.ShowBorder; }
            set { SetProperty("ShowBorder", value); }
        }
		#endregion

		#region MethodsPublic

		public XPanderPanelListDesignerActionList(System.ComponentModel.IComponent component)
            : base(component)
        {
            // Automatically display smart tag panel when
            // design-time component is dropped onto the
            // Windows Forms Designer
			base.AutoShow = true;
        }

        public override DesignerActionItemCollection GetSortedActionItems()
        {
            // Create list to store designer action items
            DesignerActionItemCollection actionItems = new DesignerActionItemCollection();

            actionItems.Add(
              new DesignerActionMethodItem(
                this,
                "ToggleDockStyle",
                GetDockStyleText(),
                "Design",
                "Dock or undock this control in it's parent container.",
                true));
            
            actionItems.Add(
                new DesignerActionPropertyItem(
                "ShowBorder",
                "Show Border",
                GetCategory(this.XPanderPanelList, "ShowBorder")));
			
            actionItems.Add(
				new DesignerActionPropertyItem(
				"PanelStyle",
				"Select PanelStyle",
				GetCategory(this.XPanderPanelList, "PanelStyle")));

            actionItems.Add(
                new DesignerActionPropertyItem(
                "ColorScheme",
                "Select ColorScheme",
                GetCategory(this.XPanderPanelList, "ColorScheme")));
            
            actionItems.Add(
			  new DesignerActionPropertyItem(
				"XPanderPanels",
				"Edit XPanderPanels",
				GetCategory(this.XPanderPanelList, "XPanderPanels")));
		
            return actionItems;
        }

        // Dock/Undock designer action method implementation
        //[CategoryAttribute("Design")]
        //[DescriptionAttribute("Dock/Undock in parent container.")]
        //[DisplayNameAttribute("Dock/Undock in parent container")]
        public void ToggleDockStyle()
        {
            // Toggle ClockControl's Dock property
            if (this.XPanderPanelList.Dock != DockStyle.Fill)
            {
                SetProperty("Dock", DockStyle.Fill);
            }
            else
            {
                SetProperty("Dock", DockStyle.None);
            }
        }

		#endregion

		#region MethodsPrivate
        /// <summary>
        /// Helper method that returns an appropriate display name for the Dock/Undock property,
		/// based on the ClockControl's current Dock property value
        /// </summary>
        /// <returns>the string to display</returns>
		private string GetDockStyleText()
        {
            if (this.XPanderPanelList.Dock == DockStyle.Fill)
            {
                return "Undock in parent container";
            }
            else
            {
                return "Dock in parent container";
            }
        }

        private XPanderPanelList XPanderPanelList
        {
            get { return (XPanderPanelList)this.Component; }
        }
		
        // Helper method to safely set a component’s property
        private void SetProperty(string propertyName, object value)
        {
            // Get property
            System.ComponentModel.PropertyDescriptor property
                = System.ComponentModel.TypeDescriptor.GetProperties(this.XPanderPanelList)[propertyName];
            // Set property value
            property.SetValue(this.XPanderPanelList, value);
        }
		// Helper method to return the Category string from a
		// CategoryAttribute assigned to a property exposed by 
		//the specified object
		private static string GetCategory(object source, string propertyName)
		{
			System.Reflection.PropertyInfo property = source.GetType().GetProperty(propertyName);
			CategoryAttribute attribute = (CategoryAttribute)property.GetCustomAttributes(typeof(CategoryAttribute), false)[0];
			if (attribute == null)
			{
				return null;
			}
			else
			{
				return attribute.Category;
			}
		}

		#endregion
	}

    #endregion

	#region Class XPanderPanelCollection

	[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1010:CollectionsShouldImplementGenericInterface")]
	public sealed class XPanderPanelCollection : IList,ICollection,IEnumerable
    {

        #region FieldsPrivate

        private XPanderPanelList m_xpanderPanelList;
		private Control.ControlCollection m_controlCollection;

        #endregion

        #region Constructor

		internal XPanderPanelCollection(XPanderPanelList xpanderPanelList)
        {
            this.m_xpanderPanelList = xpanderPanelList;
			this.m_controlCollection = this.m_xpanderPanelList.Controls;
        }

        #endregion

        #region Properties
		/// <summary>
		/// Gets or sets a XPanderPanel in the collection. 
		/// </summary>
		/// <param name="iIndex">The zero-based index of the XPanderPanel to get or set.</param>
		/// <returns>The xPanderPanel at the specified index.</returns>
        public XPanderPanel this[int index]
        {
            get { return (XPanderPanel)this.m_controlCollection[index] as XPanderPanel; }
        }

        #endregion

        #region MethodsPublic
        /// <summary>
        /// Determines whether the XPanderPanelCollection contains a specific XPanderPanel
        /// </summary>
        /// <param name="value">The XPanderPanel to locate in the XPanderPanelCollection</param>
        /// <returns>true if the XPanderPanelCollection contains the specified value; otherwise, false.</returns>
        public bool Contains(XPanderPanel xpanderPanel)
        {
			return this.m_controlCollection.Contains(xpanderPanel);
        }
		/// <summary>
		/// Adds a XPanderPanel to the collection.  
		/// </summary>
		/// <param name="xpanderPanel">The XPanderPanel to add.</param>
        public void Add(XPanderPanel xpanderPanel)
        {
			this.m_controlCollection.Add(xpanderPanel);
			this.m_xpanderPanelList.Invalidate();

        }
        /// <summary>
        /// Removes the first occurrence of a specific XPanderPanel from the XPanderPanelCollection
        /// </summary>
        /// <param name="value">The XPanderPanel to remove from the XPanderPanelCollection</param>
        public void Remove(XPanderPanel xpanderPanel)
        {
			this.m_controlCollection.Remove(xpanderPanel);
        }
		/// <summary>
		/// Removes all the XPanderPanels from the collection. 
		/// </summary>
		public void Clear()
		{
			this.m_controlCollection.Clear();
		}
		/// <summary>
		/// Gets the number of XPanderPanels in the collection. 
		/// </summary>
		public int Count
		{
			get { return this.m_controlCollection.Count; }
		}
		/// <summary>
		/// Gets a value indicating whether the collection is read-only. 
		/// </summary>
		public bool IsReadOnly
		{
			get { return this.m_controlCollection.IsReadOnly; }
		}
		/// <summary>
		/// Returns an enumeration of all the XPanderPanels in the collection.  
		/// </summary>
		/// <returns></returns>
		public IEnumerator GetEnumerator()
		{
			return this.m_controlCollection.GetEnumerator();
		}
		/// <summary>
		/// Returns the index of the specified XPanderPanel in the collection. 
		/// </summary>
		/// <param name="xpanderPanel">The xpanderPanel to find the index of.</param>
		/// <returns>The index of the xpanderPanel, or -1 if the xpanderPanel is not in the <see ref="ControlCollection">ControlCollection</see> instance.</returns>
		public int IndexOf(XPanderPanel xpanderPanel)
		{
			return this.m_controlCollection.IndexOf(xpanderPanel);
		}
		/// <summary>
		/// Removes the XPanderPanel at the specified index from the collection. 
		/// </summary>
		/// <param name="iIndex">The zero-based index of the xpanderPanel to remove from the ControlCollection instance.</param>
		public void RemoveAt(int index)
		{
			this.m_controlCollection.RemoveAt(index);
		}
		/// <summary>
		/// Inserts an XPanderPanel to the collection at the specified index. 
		/// </summary>
		/// <param name="index">The zero-based index at which value should be inserted. </param>
		/// <param name="xpanderPanel">The XPanderPanel to insert into the Collection.</param>
		public void Insert(int index, XPanderPanel xpanderPanel)
		{
			((IList)this).Insert(index, (object)xpanderPanel);
		}
		/// <summary>
		/// Copies the elements of the collection to an Array, starting at a particular Array index.
		/// </summary>
		/// <param name="xPanderPanels">The one-dimensional Array that is the destination of the elements copied from ICollection.
		/// The Array must have zero-based indexing.
		///</param>
		/// <param name="index">The zero-based index in array at which copying begins.</param>
		public void CopyTo(XPanderPanel[] xpanderPanels, int index)
		{
			this.m_controlCollection.CopyTo(xpanderPanels, index);
		}
        
		#endregion

		#region Interface ICollection

		int ICollection.Count
		{
			get { return this.Count; }
		}

		bool ICollection.IsSynchronized
		{
			get { return ((ICollection)this.m_controlCollection).IsSynchronized; }
		}

		object ICollection.SyncRoot
		{
			get { return ((ICollection)this.m_controlCollection).SyncRoot; }
		}

		void ICollection.CopyTo(Array array, int index)
		{
			((ICollection)this.m_controlCollection).CopyTo(array, index);
		}

		#endregion

		#region Interface IList

		object IList.this[int index]
		{
			get { return this.m_controlCollection[index]; }
            set {}
		}

		int IList.Add(object value)
		{
			XPanderPanel xpanderPanel = value as XPanderPanel;
			if (xpanderPanel == null)
			{
				throw new ArgumentException(string.Format(System.Globalization.CultureInfo.CurrentUICulture,
					Resources.IDS_ArgumentException,
					typeof(XPanderPanel).Name));
			}
			this.Add(xpanderPanel);
			return this.IndexOf(xpanderPanel);
		}

		bool IList.Contains(object value)
		{
			return this.Contains(value as XPanderPanel);
		}

		int IList.IndexOf(object value)
		{
			return this.IndexOf(value as XPanderPanel);
		}

		void IList.Insert(int index, object value)
		{
			if ((value is XPanderPanel) == false)
			{
				throw new ArgumentException(
					string.Format(System.Globalization.CultureInfo.CurrentUICulture,
					Resources.IDS_ArgumentException,
					typeof(XPanderPanel).Name));
			}
		}

		void IList.Remove(object value)
		{
			this.Remove(value as XPanderPanel);
		}

		void IList.RemoveAt(int index)
		{
			this.RemoveAt(index);
		}

		bool IList.IsReadOnly
		{
			get { return this.IsReadOnly; }
		}

		bool IList.IsFixedSize
		{
			get { return ((IList)this.m_controlCollection).IsFixedSize; }
		}

		#endregion
	}

	#endregion

	#region Class XPanderPanelCollectionEditor

	internal class XPanderPanelCollectionEditor : CollectionEditor
	{
		#region FieldsPrivate

		private CollectionForm m_collectionForm;

		#endregion

		#region MethodsPublic

        public XPanderPanelCollectionEditor(Type type)
            : base(type)
        {
        }

		//public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		//{
		//    if (this.m_collectionForm != null && this.m_collectionForm.Visible)
		//    {
		//        XPanderPanelCollectionEditor editor = new XPanderPanelCollectionEditor(this.m_collectionForm);
		//        return editor.EditValue(context, provider, value);
		//    }
		//    else
		//    {
		//        return base.EditValue(context, provider, value);
		//    }
		//} 

		#endregion

		#region MethodsProtected

		protected override CollectionForm CreateCollectionForm()
		{
			this.m_collectionForm = base.CreateCollectionForm();
			return this.m_collectionForm;
		}

        protected override Object CreateInstance(Type ItemType)
        {
            /* you can create the new instance yourself 
                 * ComplexItem ci=new ComplexItem(2,"ComplexItem",null);
                 * we know for sure that the itemType it will always be ComplexItem
                 *but this time let it to do the job... 
                 */

            BSE.Windows.Forms.XPanderPanel xpanderPanel =
                (BSE.Windows.Forms.XPanderPanel)base.CreateInstance(ItemType);

            if (this.Context.Instance != null)
            {
                xpanderPanel.Expand = true;
            }
            return xpanderPanel;
        }

		#endregion
	}

	#endregion

}
