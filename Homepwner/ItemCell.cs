using System;
using Foundation;
using UIKit;

namespace Homepwner
{
	[Register("ItemCell")]
	public partial class ItemCell : UITableViewCell
	{
		public ItemCell(IntPtr handle) : base(handle)
		{
		}

		public override void AwakeFromNib()
		{
			base.AwakeFromNib();
			NameLabel.AdjustsFontForContentSizeCategory = true;
			ValueLabel.AdjustsFontForContentSizeCategory = true;
			SerialNumberLabel.AdjustsFontForContentSizeCategory = true;
		}
	}
}
