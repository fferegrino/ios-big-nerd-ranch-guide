using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;

namespace Homepwner
{
	public class ItemStore
	{
		public ItemStore()
		{
			//AllItems = new List<Item>();

			var documentDirectories = NSFileManager.DefaultManager.GetUrls(NSSearchPathDirectory.DocumentDirectory, 
			                                                               NSSearchPathDomain.User);
			var documentDirectory = documentDirectories.First();
			ItemArchiveUrl = documentDirectory.AppendPathExtension("items.archive");

			var allItemsSerialized = NSKeyedUnarchiver.UnarchiveFile(ItemArchiveUrl.Path) as NSString;
			if (allItemsSerialized != null)
			{
				var str = allItemsSerialized.ToString();
				AllItems =  Newtonsoft.Json.JsonConvert.DeserializeObject<List<Item>>(str);
			}
			else
			{
				AllItems = new List<Item>();
			}

		}

		public List<Item> AllItems { get; set; }
		NSUrl ItemArchiveUrl { get; set; }

		public Item CreateItem()
		{
			var newItem = new Item(true);
			AllItems.Add(newItem);
			return newItem;
		}

		public void RemoveItem(Item item)
		{
			if (AllItems.Contains(item))
			{
				AllItems.Remove(item);
			}
		}

		public void MoveItem(int from, int to)
		{
			if (from == to)
				return;
			var item = AllItems[from];
			AllItems.RemoveAt(from);
			AllItems.Insert(to, item);
		}

		public bool SaveChanges() 
		{ 
			System.Diagnostics.Debug.WriteLine($"Saving items to {ItemArchiveUrl.Path}");
			var allItems = Newtonsoft.Json.JsonConvert.SerializeObject(AllItems);
			return NSKeyedArchiver.ArchiveRootObjectToFile(new NSString(allItems), ItemArchiveUrl.Path);
		}
	}
}
