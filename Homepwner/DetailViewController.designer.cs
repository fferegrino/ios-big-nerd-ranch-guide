// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
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
		UIKit.UIImageView ImageView { get; set; }

		[Outlet]
		UIKit.UITextField NameField { get; set; }

		[Outlet]
		UIKit.UITextField SerialNumberField { get; set; }

		[Outlet]
		UIKit.UITextField ValueField { get; set; }

		[Action ("BackgroundTapped:")]
		partial void BackgroundTapped (UIKit.UITapGestureRecognizer sender);

		[Action ("ClearPicture:")]
		partial void ClearPicture (UIKit.UIBarButtonItem sender);

		[Action ("TakePicture:")]
		partial void TakePicture (UIKit.UIBarButtonItem sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (DateLabel != null) {
				DateLabel.Dispose ();
				DateLabel = null;
			}

			if (ImageView != null) {
				ImageView.Dispose ();
				ImageView = null;
			}

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
		}
	}
}
