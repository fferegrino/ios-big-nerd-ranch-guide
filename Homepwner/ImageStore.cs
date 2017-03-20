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
		}

		public UIImage Image(string key)
		{
			return Cache.ObjectForKey(new NSString(key)) as UIImage;
		}

		public void DeleteImage(string key)
		{
			Cache.RemoveObjectForKey(new NSString(key));
		}
	}
}

