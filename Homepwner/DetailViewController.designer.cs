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
	partial class DetailViewController
	{
		[Outlet]
		UIKit.UILabel DateLabel { get; set; }

		[Outlet]
		UIKit.UITextField NameField { get; set; }

		[Outlet]
		UIKit.UITextField SerialNumberField { get; set; }

		[Outlet]
		UIKit.UITextField ValueField { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (NameField != null) {
				NameField.Dispose ();
				NameField = null;
			}

			if (SerialNumberField != null) {
				SerialNumberField.Dispose ();
				SerialNumberField = null;
			}

			if (ValueField != null) {
				ValueField.Dispose ();
				ValueField = null;
			}

			if (DateLabel != null) {
				DateLabel.Dispose ();
				DateLabel = null;
			}
		}
	}
}
