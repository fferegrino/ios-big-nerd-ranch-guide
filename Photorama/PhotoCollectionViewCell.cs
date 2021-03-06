using System;
using System.Collections.Generic;
using System.Text;
using Foundation;
using UIKit;

namespace Photorama
{
    [Register(nameof(PhotoCollectionViewCell))]
    public partial class PhotoCollectionViewCell : UICollectionViewCell
    {
		public PhotoCollectionViewCell(IntPtr handle) : base(handle)
		{

		}
		public override void AwakeFromNib()
        {
            base.AwakeFromNib();
            Update(null);
        }

        public override void PrepareForReuse()
        {
            base.PrepareForReuse();
            Update(null);
        }

        [Outlet]
        private UIImageView ImageView { get; set; }
        [Outlet]
        private UIActivityIndicatorView Spinner { get; set; }

        public void Update(UIImage image)
        {
            if (image != null)
            {
                Spinner.StopAnimating();
                ImageView.Image = image;
            }
            else
            {
                Spinner.StartAnimating();
                ImageView.Image = null;
            }
        }
    }
}
