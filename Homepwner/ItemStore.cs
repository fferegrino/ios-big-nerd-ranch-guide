using System;
using System.Collections.Generic;

namespace Homepwner
{
	public class ItemStore
	{
		public ItemStore()
		{
			AllItems = new List<Item>();
		}

		public List<Item> AllItems { get; set; }

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
	}
}
