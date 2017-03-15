using System;
using System.Collections.Generic;

namespace Homepwner
{
	public class ItemStore
	{
		public ItemStore()
		{
			AllItems = new List<Item>();
			for (int i = 0; i < 5; i++)
			{
				CreateItem();
			}
		}

		public List<Item> AllItems { get; set; }

		public Item CreateItem()
		{
			var newItem = new Item(true);
			AllItems.Add(newItem);
			return newItem;
		}
	}
}
