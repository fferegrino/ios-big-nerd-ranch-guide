// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using UIKit;

namespace WorldTrotter
{
	public partial class ConversionViewController : UIViewController, IUITextFieldDelegate
	{
		public ConversionViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			System.Diagnostics.Debug.WriteLine("ConversionViewController loaded its view");

			UpdateCelsiusLabel();
		}

		double? _farenheitValue;
		double? FarenheitValue
		{
			get { return _farenheitValue; }
			set { _farenheitValue = value; UpdateCelsiusLabel(); }
		}

		double? CelsiusValue
		{
			get
			{
				if (FarenheitValue != null)
				{
					var value = FarenheitValue.Value;
					return (value - 32) * 5 / 9;
				}
				return null;
			}
		}

		partial void FarenheitFieldEditingChanged(UITextField textField)
		{
			var text = textField.Text;
			double farenheit;
			if (!String.IsNullOrEmpty(text) && Double.TryParse(text, out farenheit))
			{
				FarenheitValue = farenheit;
			}
			else
			{
				FarenheitValue = null;
			}
		}

		partial void DismissKeyboard(UIGestureRecognizer sender)
		{
			TextField.ResignFirstResponder();
		}

		void UpdateCelsiusLabel()
		{
			if (CelsiusValue != null)
			{
				CelsiusLabel.Text = $"{CelsiusValue.Value:0.0}";
			}
			else
			{
				CelsiusLabel.Text = "???";
			}
		}

		[Export("textField:shouldChangeCharactersInRange:replacementString:")]
		public bool ShouldChangeCharacters(UITextField textField, NSRange range, string replacementString)
		{
			var currentLocale = NSLocale.CurrentLocale;
			var decimalSeparator = currentLocale.DecimalSeparator ?? ".";

			if (replacementString.Length == 0)
				return true;

			NSCharacterSet validCharSet = NSCharacterSet.FromString("0123456789" + decimalSeparator);
			foreach(char c in replacementString)
			{
				if (!validCharSet.Contains(c))
					return false;
			}


			var existingDecimalSeparatorIndex = textField.Text.IndexOf(decimalSeparator, StringComparison.CurrentCulture);
			var replacementDecimalSeparatorIndex = replacementString.IndexOf(decimalSeparator, StringComparison.CurrentCulture);

			if (existingDecimalSeparatorIndex >= 0 &&
			   replacementDecimalSeparatorIndex >= 0)
			{
				return false;
			}
			else 
			{
				return true;
			}
		}
	}
}
