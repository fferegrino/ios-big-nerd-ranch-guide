using System;
using System.Linq;
using System.Threading.Tasks;
using FlickrNet;
using Foundation;
using UIKit;

namespace Photorama
{
    [Register(nameof(PhotosViewController))]
    public partial class PhotosViewController : UIViewController
    {

        [Outlet]
        UIKit.UIImageView ImageView { get; set; }

        public PhotoStore Store { get; set; }

        public PhotosViewController(IntPtr handle) : base(handle)
        {
        }

        public override async void ViewDidLoad()
        {
            base.ViewDidLoad();
            await Store.FetchInterestingPhotos(Completion);
        }

        private async void Completion(PhotoCollection photoCollection)
        {
            var image = await FromUrl(photoCollection.First().ThumbnailUrl);
            InvokeOnMainThread(() =>
            {
                ImageView.Image = image;
            });


        }

        static async Task<UIImage> FromUrl(string uri)
        {
            UIImage img = null;
            await Task.Run(() =>
            {
                using (var url = new NSUrl(uri))
                using (var data = NSData.FromUrl(url))
                    img = UIImage.LoadFromData(data);
            });

            return img;
        }
    }
}