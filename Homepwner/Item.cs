using System;
namespace Homepwner
{
	public class Item
	{
		public string Name
		{
			get;
			set;
		}

		public int ValueInDollars
		{
			get;
			set;
		}

		public string SerialNumber
		{
			get;
			set;
		}

		public DateTime DateCreated
		{
			get;
			set;
		}

		public Item (string name, int valueInDollars, string serialNumber = null)
		{
			Name = name;
			ValueInDollars = valueInDollars;
			SerialNumber = serialNumber;
			DateCreated = DateTime.UtcNow;
		}

		static string[] Adjectives = new string[] { "Fluffy", "Rusty", "Shiny" };
		static string[] Nouns = new string[] { "Bear", "Spork", "Mac" };
		static Random randomGenerator = new Random();

		public Item(bool random = false)
		{
			if (random)
			{
				var idx = randomGenerator.Next(0, Adjectives.Length);
				var randomAdjective = Adjectives[idx];

				idx = randomGenerator.Next(0, Nouns.Length);
				var randomNoun = Nouns[idx];

				Name = $"{randomAdjective} {randomNoun}";
				ValueInDollars = randomGenerator.Next(0, 100);
				SerialNumber = Guid.NewGuid().ToString();
				DateCreated = DateTime.UtcNow;
			}
		}
	}
}
