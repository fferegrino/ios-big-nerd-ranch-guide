using System;
using System.Linq;
using System.Threading.Tasks;
using FlickrNet;
using Foundation;
using UIKit;

namespace Photorama
{
    public partial class PhotosViewController : UIViewController
    {

        [Outlet]
        UIKit.UICollectionView CollectionView { get; set; }

        public PhotoStore Store { get; set; }
        public PhotoDataSource PhotoDataSource { get; set; } = new PhotoDataSource();

        public PhotosViewController(IntPtr handle) : base(handle)
        {
        }

        public override async void ViewDidLoad()
        {
            base.ViewDidLoad();
            CollectionView.DataSource = PhotoDataSource;
            await Store.FetchInterestingPhotos(Completion);
        }

        private void Completion(PhotoCollection collection)
        {
            PhotoDataSource.Photos = collection.ToArray();
            CollectionView.ReloadSections(NSIndexSet.FromIndex(0));
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