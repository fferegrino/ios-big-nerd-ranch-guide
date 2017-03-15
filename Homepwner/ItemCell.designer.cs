// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Homepwner
{
	partial class ItemCell
	{
		[Outlet]
		public UIKit.UILabel NameLabel { get; set; }

		[Outlet]
		public UIKit.UILabel SerialNumberLabel { get; set; }

		[Outlet]
		public UIKit.UILabel ValueLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (NameLabel != null) {
				NameLabel.Dispose ();
				NameLabel = null;
			}

			if (SerialNumberLabel != null) {
				SerialNumberLabel.Dispose ();
				SerialNumberLabel = null;
			}

			if (ValueLabel != null) {
				ValueLabel.Dispose ();
				ValueLabel = null;
			}
		}
	}
}
