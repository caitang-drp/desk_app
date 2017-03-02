using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;
using LocalERP.UiDataProxy;

namespace LocalERP.WinForm
{
	//public delegate void NodeSelectEventHandler();
	/// <summary>
	/// ComboBoxTree control is a treeview that drops down much like a combobox
	/// </summary>
    public class LookupArg
    {
        private string argName;

        public string ArgName
        {
            get { return argName; }
            set { argName = value; }
        }

        private object value;

        public object Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        private string text;

        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        public LookupArg(object v, string t)
        {
            this.value = v;
            this.text = t;
        }

        public override string ToString()
        {
            return text;
        }
    }/// 

	public class LookupText : UserControl
	{
		#region Private Fields

        private LookupArg lookupArg;

        public LookupArg LookupArg
        {
            get { return lookupArg; }
            set {
                try
                {
                    if (lookupArg != value && valueSetted != null)
                        this.valueSetted.Invoke(this, value);
                }
                catch (Exception e) {
                    e.ToString();
                }
                lookupArg = value;
            }
        }

        protected String value_Lookup;
		private Panel topPanel;
		protected TextBox valueTextbox;
        private PictureBox picButton;
        private Image selectButtonBackGround;

        //commented by stone: send notify
        public delegate void ValueSetted(object sender, LookupArg arg);
        public event ValueSetted valueSetted;

        private string lookupFormType;

        public string LookupFormType
        {
            get { return lookupFormType; }
            set { lookupFormType = value; }
        }

        private LookupAndNotifyDockContent lookupForm;
        public LookupAndNotifyDockContent LookupForm
        {
            get { return lookupForm; }
            set { 
                lookupForm = value;
                if (lookupForm != null)
                {
                    //commented by stone: receive notify
                    lookupForm.lookupCallback -= new LookupAndNotifyDockContent.LookupCallback(lookupForm_lookupCallback);
                    lookupForm.lookupCallback += new LookupAndNotifyDockContent.LookupCallback(lookupForm_lookupCallback);
                }
            }
        }

        protected virtual void lookupForm_lookupCallback(LookupArg arg)
        {
            //File.AppendAllText("e:\\debug.txt", string.Format("lookupText deliver invoke, thread:{0}\r\n", System.Threading.Thread.CurrentThread.ManagedThreadId));

            this.LookupArg = arg;
            this.Text_Lookup = arg.Text;
            
            if (valueSetted != null)
                this.valueSetted.Invoke(this, arg);
        }

		#endregion

		#region Public Properties
        
        [Browsable(true), Description("The text in the control"), Category("Appearance")]
        public string Text_Lookup
        {
            get { return this.valueTextbox.Text; }
            set {
                if (!string.IsNullOrEmpty(value))
                {
                    this.valueTextbox.Text = value;
                }
                else
                    this.valueTextbox.Text = "µ¥»÷Ñ¡Ôñ...";
            }
        }

        [Browsable(true), Description("The value in the ComboBoxTree control"), Category("Appearance")]
        public virtual String Value_Lookup
        {
            get { return value_Lookup; }
            set { this.value_Lookup = value; }
        }



        [Description("Ñ¡Ôñ°´Å¥±³¾°Í¼"), Category("Appearance")]
        public Image SelectButtonBackGround
        {
            get { return selectButtonBackGround; }
            set { selectButtonBackGround = value;
            this.picButton.BackgroundImage = selectButtonBackGround;
            }
        }
		#endregion

        public LookupText() 
		{
			this.InitializeComponent();

            this.BackColor = Color.Transparent;

			//top
			this.topPanel = new Panel();
			this.topPanel.BorderStyle = BorderStyle.Fixed3D;
            this.topPanel.BackColor = Color.White;
			//this.topPanel.AutoScroll = false;
			
			this.valueTextbox = new TextBox();
            this.valueTextbox.ReadOnly = true;
            //this.valueTextbox.ForeColor = Color.Gray;
            this.valueTextbox.BorderStyle = BorderStyle.None;
            this.valueTextbox.BackColor = Color.White;
            this.valueTextbox.Cursor = Cursors.Hand;
            this.valueTextbox.Multiline = true;
            
            this.valueTextbox.Click += new EventHandler(picButton_Click);
            
            this.lookupArg = new LookupArg(this.value_Lookup, this.Text_Lookup);

            this.picButton = new PictureBox();
            this.picButton.Size = new Size(18, 18);
            this.picButton.BackgroundImageLayout = ImageLayout.Center;
            this.picButton.Cursor = Cursors.Hand;
            this.picButton.Dock = DockStyle.Right;
            this.picButton.MouseHover += new EventHandler(picButton_MouseHover);
            this.picButton.MouseLeave += new EventHandler(picButton_MouseLeave);
            this.picButton.Click += new EventHandler(picButton_Click);

			SetStyle(ControlStyles.DoubleBuffer,true);
			SetStyle(ControlStyles.ResizeRedraw,true);

            this.topPanel.Controls.AddRange(new Control[]{picButton, valueTextbox});
			this.Controls.Add(this.topPanel);
        }

        void picButton_MouseLeave(object sender, EventArgs e)
        {
            this.picButton.BackColor = Color.White;
        }

        void picButton_MouseHover(object sender, EventArgs e)
        {
            this.picButton.BackColor = Color.LightBlue;
        }

        void picButton_Click(object sender, EventArgs e)
        {
            object value = null;
            if (this.lookupArg != null)
                value = lookupArg.Value;
            this.lookupForm.showDialog(value);
        }
 

		#region Events

		private void InitializeComponent()
		{
            this.SuspendLayout();
            // 
            // ComboBoxTree
            // 
            this.Name = "ComboBoxTree";
            this.Layout += new System.Windows.Forms.LayoutEventHandler(this.ComboBoxTree_Layout);
            this.ResumeLayout(false);

		}

		private void ComboBoxTree_Layout(object sender, System.Windows.Forms.LayoutEventArgs e)
		{
            this.topPanel.Width = this.Width;
            this.topPanel.Height = this.Height-2;

            this.valueTextbox.Width = this.topPanel.Width - this.picButton.Width;
            this.valueTextbox.Height = this.topPanel.Height;
		}

		#endregion
	}
}