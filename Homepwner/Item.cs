using System;
using Foundation;

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

		public string ItemKey
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
			ItemKey = Guid.NewGuid().ToString();
		}

		static string[] Adjectives = new string[] { "Fluffy", "Rusty", "Shiny" };
		static string[] Nouns = new string[] { "Bear", "Spork", "Mac" };
		static Random randomGenerator = new Random();

		public Item()
		{

		}

		public Item(bool random)
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
				ItemKey = Guid.NewGuid().ToString();
			}
		}

		//public void EncodeTo(NSCoder encoder)
		//{
		//	encoder.Encode(new NSString(Name), nameof(Name));
		//	encoder.Encode(DateCreated.Ticks, nameof(DateCreated));
		//	encoder.Encode(new NSString(ItemKey), nameof(ItemKey));
		//	encoder.Encode(new NSString(SerialNumber), nameof(SerialNumber));

		//	encoder.Encode(ValueInDollars, nameof(ValueInDollars));
		//}

		//public Item (NSCoder encoder) 
		//{
		//	Name = (encoder.DecodeObject(nameof(Name)) as NSString).ToString();
		//	var dateCreatedTicks = encoder.DecodeLong(nameof(DateCreated));
		//	DateCreated = new DateTime(dateCreatedTicks);
		//	ItemKey = (encoder.DecodeObject(nameof(ItemKey)) as NSString).ToString();
		//	SerialNumber = (encoder.DecodeObject(nameof(SerialNumber)) as NSString).ToString();

		//	ValueInDollars = encoder.DecodeInt(nameof(ValueInDollars));
		//}
	}
}
