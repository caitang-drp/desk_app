using System;

namespace BSE.Windows.Forms
{
	/// <summary>
	/// Zusammenfassung für XPanderPanelClickEventArgs.
	/// </summary>
	public class XPanderPanelClickEventArgs : EventArgs
	{
		#region FieldsPrivate
		
		private bool m_bExpand;
		
		#endregion
		
		#region Properties
		
		public bool Expand
		{
			get {return m_bExpand;}
			set {m_bExpand = value;}
		}
		
		#endregion

		#region MethodsPublic

		public XPanderPanelClickEventArgs(bool bExpand)
		{
			this.m_bExpand = bExpand;
		}

		#endregion
	}
}
