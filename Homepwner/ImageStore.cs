using System.Linq;
using Foundation;
using UIKit;

namespace Homepwner
{
	public class ImageStore 
	{
		NSCache Cache;

		public ImageStore()
		{
			Cache = new NSCache();
		}

		public void SetImage(UIImage image, string key)
		{
			Cache.SetObjectforKey(image, new NSString(key));

			var url = ImageUrl(key);

			var data = image.AsJPEG((System.nfloat)0.5);
			try
			{
				data?.Save(url.Path, true);
			}
			catch
			{
			}
		}

		public UIImage Image(string key)
		{
			var existingImage =  Cache.ObjectForKey(new NSString(key)) as UIImage;
			if (existingImage != null)
				return existingImage;

			var url = ImageUrl(key);
			var imageFromDisk = UIImage.FromFile(url.Path);
			if (imageFromDisk == null)
				return null;

			Cache.SetObjectforKey(imageFromDisk, new NSString(key));
			return imageFromDisk;
		}

		public void DeleteImage(string key)
		{
			Cache.RemoveObjectForKey(new NSString(key));
			var url = ImageUrl(key);
			NSError deleteError;
			NSFileManager.DefaultManager.Remove(url.Path, out deleteError);

			if (deleteError != null)
			{
				System.Diagnostics.Debug.WriteLine($"Error removing the image from disk: {deleteError}");
			}
		}

		public NSUrl ImageUrl(string key)
		{
			var documentDirectories = NSFileManager.DefaultManager.GetUrls(NSSearchPathDirectory.DocumentDirectory,
															   NSSearchPathDomain.User);
			var documentDirectory = documentDirectories.First();
			return documentDirectory.AppendPathExtension(key);
		}
	}
}

