using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using CoreGraphics;
using Foundation;
using UIKit;

namespace TouchTracker
{

	[Register(nameof(DrawView)), DesignTimeVisible(true)]
	public class DrawView : UIView, IUIGestureRecognizerDelegate
	{
		public Dictionary<NSValue, Line> CurrentLines
		{
			get;
			set;
		} = new Dictionary<NSValue, Line>();

		public List<Line> FinishedLines
		{
			get;
			set;
		} = new List<Line>();

		int? _selectedLineIndex;

		UIPanGestureRecognizer MoveRecognizer;

		int? SelectedLineIndex 
		{
			get { return _selectedLineIndex; }
			set
			{
				_selectedLineIndex = value; if (value == null) { UIMenuController.SharedMenuController.MenuVisible = false; } }
		}

		private UIColor _finishedLineColor = UIColor.Black;
		[Export(nameof(FinishedLineColor)), Browsable(true)]
		public UIColor FinishedLineColor
		{
			get
			{
				return _finishedLineColor;
			}
			set
			{
				_finishedLineColor = value;
				SetNeedsDisplay();
			}

		}

		private UIColor _currentLineColor = UIColor.Red;
		[Export(nameof(CurrentLineColor)), Browsable(true)]
		public UIColor CurrentLineColor
		{
			get
			{
				return _currentLineColor;
			}
			set
			{
				_currentLineColor = value;
				SetNeedsDisplay();
			}

		}

		private nfloat _lineThickness = 10;
		[Export(nameof(LineThickness)), Browsable(true)]
		public nfloat LineThickness
		{
			get
			{
				return _lineThickness;
			}
			set
			{
				_lineThickness = value;
				SetNeedsDisplay();
			}

		}

		public DrawView(IntPtr handler) : base(handler)
		{
			Init();
		}

		public DrawView(NSCoder coder) : base(coder)
		{
			Init();
		}

		void Init()
		{
			var doubleTapRecognizer = new UITapGestureRecognizer(DoubleTap);
			doubleTapRecognizer.DelaysTouchesBegan = true;
			doubleTapRecognizer.NumberOfTapsRequired = 2;
			AddGestureRecognizer(doubleTapRecognizer);

			var tapRecognizer = new UITapGestureRecognizer(Tap);
			tapRecognizer.DelaysTouchesBegan = true;
			tapRecognizer.RequireGestureRecognizerToFail(doubleTapRecognizer);
			AddGestureRecognizer(tapRecognizer);

			var longPressRecognizer = new UILongPressGestureRecognizer(LongPress);
			AddGestureRecognizer(longPressRecognizer);

			MoveRecognizer = new UIPanGestureRecognizer(MoveLine);
			MoveRecognizer.Delegate = this;
			MoveRecognizer.CancelsTouchesInView = false;
			AddGestureRecognizer(MoveRecognizer);
		}

		void DoubleTap(UIGestureRecognizer gestureRecognizer)
		{
			SelectedLineIndex = null;
			CurrentLines.Clear();
			FinishedLines.Clear();
			SetNeedsDisplay();
		}

		void LongPress(UIGestureRecognizer gestureRecognizer)
		{
			switch (gestureRecognizer.State)
			{
				case UIGestureRecognizerState.Began:
					var point = gestureRecognizer.LocationInView(this);
					SelectedLineIndex = IndexOfLine(point);
					if (SelectedLineIndex.HasValue)
					{
						CurrentLines.Clear();
					}
					break;
				case UIGestureRecognizerState.Ended:
					SelectedLineIndex = null;
					break;
			}
			SetNeedsDisplay();
		}

		[Export("gestureRecognizer:shouldRecognizeSimultaneouslyWithGestureRecognizer:")]
		public bool ShouldRecognizeSimultaneously(UIGestureRecognizer gestureRecognizer, UIGestureRecognizer otherGestureRecognizer)
		{
			return true;
		}

		void MoveLine(UIPanGestureRecognizer gestureRecognizer)
		{
			if (SelectedLineIndex.HasValue)
			{
				switch (gestureRecognizer.State)
				{
					case UIGestureRecognizerState.Changed:
						var translation = gestureRecognizer.TranslationInView(this);

						var line = FinishedLines[SelectedLineIndex.Value];
						var newLine = new Line(
							new CGPoint(
								line.Begin.X + translation.X,
								line.Begin.Y + translation.Y),
							new CGPoint(
								line.End.X + translation.X,
								line.End.Y + translation.Y));
						FinishedLines[SelectedLineIndex.Value] = newLine;
						gestureRecognizer.SetTranslation(CGPoint.Empty, this);
						SetNeedsDisplay();
						break;
				}
			}
			SetNeedsDisplay();
		}


		void Tap(UIGestureRecognizer gestureRecognizer)
		{
			var point = gestureRecognizer.LocationInView(this);
			SelectedLineIndex = IndexOfLine(point);

			var menu = UIMenuController.SharedMenuController;
			if (SelectedLineIndex.HasValue)
			{
				BecomeFirstResponder();

				var deleteItem = new UIMenuItem("Delete", new ObjCRuntime.Selector("DeleteLine:"));

				menu.MenuItems = new UIMenuItem[] { deleteItem };

				var targetRect = new CGRect(point.X, point.Y, width: 2, height: 2);
				menu.SetTargetRect(targetRect, this);
				menu.MenuVisible = true;
			}
			else
			{
				menu.MenuVisible = false;
			}

			SetNeedsDisplay();

		}

		public override bool CanBecomeFirstResponder
		{
			get
			{
				return true;
			}
		}

		[Export("DeleteLine:")]
		private void DeleteLine(UIMenuController controller)
		{
			if (SelectedLineIndex.HasValue)
			{
				FinishedLines.RemoveAt(SelectedLineIndex.Value);
				SelectedLineIndex = null;
			}
			SetNeedsDisplay();
		}

		void Stroke(Line line)
		{
			var path = new UIBezierPath();
			path.LineWidth = _lineThickness;
			path.LineCapStyle = CoreGraphics.CGLineCap.Round;

			path.MoveTo(line.Begin);
			path.AddLineTo(line.End);
			path.Stroke();
		}

		public override void Draw(CoreGraphics.CGRect rect)
		{
			FinishedLineColor.SetStroke();
			foreach (var line in FinishedLines)
			{
				Stroke(line);
			}

			if (CurrentLines.Count > 0)
			{
				CurrentLineColor.SetStroke();

				foreach (var line in CurrentLines)
				{
					Stroke(line.Value);
				}
			}

			if (SelectedLineIndex.HasValue)
			{
				UIColor.Green.SetStroke();
				var selectedLine = FinishedLines[SelectedLineIndex.Value];
				Stroke(selectedLine);
			}
		}

		public override void TouchesBegan(NSSet touches, UIEvent evt)
		{
			var ths = touches.ToArray<UITouch>();

			foreach (var touch in ths)
			{
				var location = touch.LocationInView(this);
				var newLine = new Line(location, location);

				NSValue key = NSValue.ValueFromNonretainedObject(touch);
				CurrentLines[key] = newLine;
			}
			SetNeedsDisplay();
		}

		int? IndexOfLine(CGPoint point)
		{
			// Find a line close to point
			for (int i = 0; i < FinishedLines.Count; i++)
			{
				var begin = FinishedLines[i].Begin;
				var end = FinishedLines[i].End;

				for (double t = 0; t < 1; t += 0.05)
				{
					var x = begin.X + ((end.X - begin.X) * t);
					var y = begin.Y + ((end.Y - begin.Y) * t);

					if (Hypotenuse(x - point.X, y - point.Y) < 20)
					{
						return i;
					}
				}
			}

			return null;
		}

		static double Hypotenuse(double side1, double side2)
		{
			return Math.Sqrt(side1 * side1 + side2 * side2);
		}

		public override void TouchesMoved(NSSet touches, UIEvent evt)
		{
			var ths = touches.ToArray<UITouch>();

			foreach (var touch in ths)
			{
				var location = touch.LocationInView(this);
				var newLine = new Line(location, location);

				NSValue key = NSValue.ValueFromNonretainedObject(touch);
				if (CurrentLines.ContainsKey(key))
				{
					CurrentLines[key] = new Line(CurrentLines[key].Begin, location);
				}
			}

			SetNeedsDisplay();
		}

		public override void TouchesEnded(NSSet touches, UIEvent evt)
		{

			var ths = touches.ToArray<UITouch>();

			foreach (var touch in ths)
			{
				var location = touch.LocationInView(this);
				var newLine = new Line(location, location);

				NSValue key = NSValue.ValueFromNonretainedObject(touch);
				if (CurrentLines.ContainsKey(key))
				{
					CurrentLines[key] = new Line(CurrentLines[key].Begin, location);
				}
				FinishedLines.Add(CurrentLines[key]);
				CurrentLines.Remove(key);
			}


			SetNeedsDisplay();
		}
	}
}
