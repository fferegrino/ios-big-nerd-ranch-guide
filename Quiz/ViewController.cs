using System;

using UIKit;

namespace Quiz
{
	public partial class ViewController : UIViewController
	{
		protected ViewController(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		readonly string[] questions = new string[]
		{
			"What is 7+7?",
			"What is the capital of Vermont?",
			"What is cognac made from?"
		};

		readonly string[] answers = new string[]
		{
			"14",
			"Montpelier",
			"Grapes"
		};

		int currentQuestionIndex = 0;

		partial void ShowNextQuestion(Foundation.NSObject sender)
		{
			currentQuestionIndex += 1;
			if (currentQuestionIndex == questions.Length)
			{
				currentQuestionIndex = 0;
			}
			var question = questions[currentQuestionIndex];
			NextQuestionLabel.Text = question;
			AnswerLabel.Text = "???";

			AnimateLabelTransitions();
		}

		partial void ShowAnswer(Foundation.NSObject sender)
		{
			var answer = answers[currentQuestionIndex];
			AnswerLabel.Text = answer;
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			CurrentQuestionLabel.Text = questions[currentQuestionIndex];

			UpdateOffScreenLabel();
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			NextQuestionLabel.Alpha = 0;
		}

		void AnimateLabelTransitions()
		{
			View.LayoutIfNeeded();

			var screenWidth = View.Frame.Width;
			NextQuestionLabelCenterXConstraint.Constant = 0;
			CurrentQuestionLabelCenterXConstraint.Constant += screenWidth;
			
			UIView.Animate(duration: 0.5, 
			               delay: 0, 
			               options: UIViewAnimationOptions.CurveLinear,
			               animation: () =>
							{
								this.CurrentQuestionLabel.Alpha = 0;
								this.NextQuestionLabel.Alpha = 1;

								View.LayoutIfNeeded();

							}, 
			               completion: () =>
							{
								var oldLabelReference = NextQuestionLabel;
								NextQuestionLabel = CurrentQuestionLabel;
								CurrentQuestionLabel = oldLabelReference;

								var oldLabelConstraintReference = NextQuestionLabelCenterXConstraint;
								NextQuestionLabelCenterXConstraint = CurrentQuestionLabelCenterXConstraint;
								CurrentQuestionLabelCenterXConstraint = oldLabelConstraintReference;

								UpdateOffScreenLabel();
							});
		}

		void UpdateOffScreenLabel()
		{
			var screenWidth = View.Frame.Width;
			NextQuestionLabelCenterXConstraint.Constant = -screenWidth;
		}
	}
}
