using System;
using Foundation;
using UIKit;

namespace Homepwner
{
	[Register("DetailViewController")]
	public partial class DetailViewController : UIViewController
	{
		public Item Item { get; set; }

		public DetailViewController(IntPtr handle) : base (handle)
		{
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			NameField.Text = Item.Name;
			SerialNumberField.Text = Item.SerialNumber;
			ValueField.Text = $"{Item.ValueInDollars:0.00}";
			DateLabel.Text = $"{Item.DateCreated:dd/mm/yyyy}";
		}
	}
}
