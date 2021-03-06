// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using UIKit;

namespace Homepwner
{
	public partial class ItemsViewController : UITableViewController
	{
		public ItemStore ItemStore { get; set; }
		public ImageStore ImageStore { get; set; }

		public ItemsViewController (IntPtr handle) : base (handle)
		{
			NavigationItem.LeftBarButtonItem = EditButtonItem;
		}

		//public ItemsViewController()
		//{
		//}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			var statusBarHeight = UIApplication.SharedApplication.StatusBarFrame.Height;

			TableView.RowHeight = UITableView.AutomaticDimension;
			TableView.EstimatedRowHeight = 65;
		}

		public override nint RowsInSection(UITableView tableView, nint section)
		{
			return ItemStore.AllItems.Count +1;
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			if (indexPath.Row < ItemStore.AllItems.Count)
			{
				var cell = tableView.DequeueReusableCell("ItemCell", indexPath) as ItemCell;
				var item = ItemStore.AllItems[indexPath.Row];

				cell.NameLabel.Text = item.Name;
				cell.SerialNumberLabel.Text = item.SerialNumber;
				cell.ValueLabel.Text = item.ValueInDollars.ToString();
				cell.ValueLabel.TextColor = item.ValueInDollars < 50 ? UIColor.Green : UIColor.Red;

				return cell;
			}
			else
			{
				return tableView.DequeueReusableCell("NoMoreItemsCellView", indexPath);
			}

		}

		[Action("AddNewItem:")]
		void AddNewItem(Foundation.NSObject sender)
		{
			var newItem = ItemStore.CreateItem();
			var indexPath = NSIndexPath.FromItemSection(ItemStore.AllItems.Count -1, 0);
			TableView.InsertRows(new[] { indexPath }, UITableViewRowAnimation.Automatic);
		}

		public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
		{
			return indexPath.Row != ItemStore.AllItems.Count;
		}

		public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
		{
			if (editingStyle == UITableViewCellEditingStyle.Delete)
			{
				var item = ItemStore.AllItems[indexPath.Row];


				var title = $"Delete {item.Name}";
				var message = "Are you sure you want to delete this item?";

				var ac = UIAlertController.Create(title, message, UIAlertControllerStyle.ActionSheet);

				var cancelAction = UIAlertAction.Create("Cancel", UIAlertActionStyle.Cancel, null);
				var deleteAction = UIAlertAction.Create("Delete", UIAlertActionStyle.Destructive, (obj) => 
				{
					ItemStore.RemoveItem(item);
					ImageStore.DeleteImage(item.ItemKey);
					tableView.DeleteRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Automatic);
				});

				ac.AddAction(deleteAction);
				ac.AddAction(cancelAction);

				PresentViewController(ac, true, null);
			}
		}

		public override string TitleForDeleteConfirmation(UITableView tableView, NSIndexPath indexPath)
		{
			return "Remove";
		}

		public override bool CanMoveRow(UITableView tableView, NSIndexPath indexPath)
		{
			return indexPath.Row != ItemStore.AllItems.Count;
		}

		public override NSIndexPath CustomizeMoveTarget(UITableView tableView, NSIndexPath sourceIndexPath, NSIndexPath proposedIndexPath)
		{
			if (proposedIndexPath.Row >= ItemStore.AllItems.Count)
			{
				return NSIndexPath.FromRowSection(ItemStore.AllItems.Count - 1, 0);
			}
			return proposedIndexPath;
		}

		public override void MoveRow(UITableView tableView, NSIndexPath sourceIndexPath, NSIndexPath destinationIndexPath)
		{
			ItemStore.MoveItem(sourceIndexPath.Row, destinationIndexPath.Row);
		}

		public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
		{
			switch (segue.Identifier)
			{
				case "ShowItem":
					var row = TableView.IndexPathForSelectedRow?.Row;
					if (row.HasValue)
					{
						var item = ItemStore.AllItems[row.Value];
						var detailViewController = segue.DestinationViewController as DetailViewController;
						detailViewController.ImageStore = ImageStore;
						detailViewController.Item = item;
					}
					break;
					default:
						throw new InvalidOperationException("Unexpected segue identifier");
			}
			base.PrepareForSegue(segue, sender);
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			TableView.ReloadData();
		}
	}
}
