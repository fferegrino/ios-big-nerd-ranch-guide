// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Quiz
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		UIKit.UILabel AnswerLabel { get; set; }

		[Outlet]
		UIKit.UILabel CurrentQuestionLabel { get; set; }

		[Outlet]
		UIKit.UILabel NextQuestionLabel { get; set; }

		[Action ("ShowAnswer:")]
		partial void ShowAnswer (Foundation.NSObject sender);

		[Action ("ShowNextQuestion:")]
		partial void ShowNextQuestion (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (AnswerLabel != null) {
				AnswerLabel.Dispose ();
				AnswerLabel = null;
			}

			if (NextQuestionLabel != null) {
				NextQuestionLabel.Dispose ();
				NextQuestionLabel = null;
			}

			if (CurrentQuestionLabel != null) {
				CurrentQuestionLabel.Dispose ();
				CurrentQuestionLabel = null;
			}
		}
	}
}
