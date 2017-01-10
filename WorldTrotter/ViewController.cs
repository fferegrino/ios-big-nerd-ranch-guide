using System;
using CoreGraphics;
using UIKit;

namespace WorldTrotter
{
	public partial class ViewController : UIViewController
	{
		protected ViewController(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			var firstFrame = new CGRect(160, 240, 100, 150);
			var firstView = new UIView(firstFrame);
			firstView.BackgroundColor = UIColor.Blue;
			View.AddSubview(firstView);

			var secondFrame = new CGRect(10, 30, 50, 50);
			var secondView = new UIView(secondFrame);
			secondView.BackgroundColor = UIColor.Green;
			firstView.AddSubview(secondView);
		}
	}
}
