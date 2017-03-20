using System;
using Foundation;
using UIKit;

namespace Homepwner
{
	[Register("DetailViewController")]
	public partial class DetailViewController : UIViewController, IUITextFieldDelegate, IUINavigationControllerDelegate, IUIImagePickerControllerDelegate
	{

		private Item _item;
		public Item Item { get { return _item; } set { _item = value; NavigationItem.Title = Item.Name; } }
		public ImageStore ImageStore { get; set; }

		public DetailViewController(IntPtr handle) : base (handle)
		{
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			NameField.Text = Item.Name;
			SerialNumberField.Text = Item.SerialNumber;
			ValueField.Text = $"{Item.ValueInDollars:0}";
			DateLabel.Text = $"{Item.DateCreated:dd/mm/yyyy}";

			var key = Item.ItemKey;

			var imageToDisplay = ImageStore.Image(key);
			ImageView.Image = imageToDisplay;

			NameField.Delegate = this;
			SerialNumberField.Delegate = this;
			ValueField.Delegate = this;
		}

		public override void ViewWillDisappear(bool animated)
		{
			base.ViewWillDisappear(animated);

			View.EndEditing(true);

			Item.Name = NameField.Text ?? "";
			Item.SerialNumber = SerialNumberField.Text;

			int valueInDollars;
			Item.ValueInDollars = int.TryParse(ValueField.Text, out valueInDollars) ?
				valueInDollars : 0;
		}

		[Export("textFieldShouldReturn:")]
		public bool ShouldReturn(UITextField textField)
		{
			textField.ResignFirstResponder();
			return true;
		}

		partial void BackgroundTapped(UITapGestureRecognizer sender)
		{
			View.EndEditing(true);
		}

		partial void TakePicture(UIBarButtonItem sender)
		{
			var imagePicker = new UIImagePickerController();
			if (UIImagePickerController.IsSourceTypeAvailable(UIImagePickerControllerSourceType.Camera))
			{
				imagePicker.SourceType = UIImagePickerControllerSourceType.Camera;
				imagePicker.CameraOverlayView = new UIButton(UIButtonType.DetailDisclosure);
			}
			else
			{
				imagePicker.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;
			}
			imagePicker.AllowsEditing = true;
			imagePicker.Delegate = this;

			// Place image picker on the screen
			PresentViewController(imagePicker, true, null);
		}

		partial void ClearPicture(UIBarButtonItem sender)
		{
			ImageStore.DeleteImage(Item.ItemKey);
			ImageView.Image = null;
		}

		[Export("imagePickerController:didFinishPickingMediaWithInfo:")]
		public void FinishedPickingMedia(UIImagePickerController picker, NSDictionary info)
		{
			var image = info[UIImagePickerController.EditedImage] as UIImage;

			ImageStore.SetImage(image, Item.ItemKey);

			ImageView.Image = image;

			DismissViewController(true, null);
		}
	}
}
