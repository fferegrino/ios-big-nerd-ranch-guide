// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace WorldTrotter
{
	[Register ("ConversionViewController")]
	partial class ConversionViewController
	{
		[Outlet]
		UIKit.UILabel CelsiusLabel { get; set; }

		[Outlet]
		UIKit.UITextField TextField { get; set; }

		[Action ("DismissKeyboard:")]
		partial void DismissKeyboard (UIKit.UIGestureRecognizer sender);

		[Action ("FarenheitFieldEditingChanged:")]
		partial void FarenheitFieldEditingChanged (UIKit.UITextField sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (CelsiusLabel != null) {
				CelsiusLabel.Dispose ();
				CelsiusLabel = null;
			}

			if (TextField != null) {
				TextField.Dispose ();
				TextField = null;
			}
		}
	}
}
