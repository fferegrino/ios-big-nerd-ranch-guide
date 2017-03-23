using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Foundation;
using UIKit;

namespace TouchTracker
{

	[Register(nameof(DrawView)), DesignTimeVisible(true)]
	public class DrawView : UIView
	{
		public Dictionary<NSValue, Line> CurrentLine
		{
			get;
			set;
		} = new Dictionary<NSValue, Line>();

		public List<Line> FinishedLines
		{
			get;
			set;
		} = new List<Line>();

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

			if (CurrentLine.Count > 0)
			{
				CurrentLineColor.SetStroke() ;

				foreach (var line in CurrentLine)
				{
					Stroke(line.Value);
				}
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
				CurrentLine[key] = newLine;
			}
			SetNeedsDisplay();
		}

		public override void TouchesMoved(NSSet touches, UIEvent evt)
		{
			var ths = touches.ToArray<UITouch>();

			foreach (var touch in ths)
			{
				var location = touch.LocationInView(this);
				var newLine = new Line(location, location);

				NSValue key = NSValue.ValueFromNonretainedObject(touch);
				if (CurrentLine.ContainsKey(key))
				{
					CurrentLine[key] = new Line(CurrentLine[key].Begin, location);
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
				if (CurrentLine.ContainsKey(key))
				{
					CurrentLine[key] = new Line(CurrentLine[key].Begin, location);
				}
				FinishedLines.Add(CurrentLine[key]);
				CurrentLine.Remove(key);
			}


			SetNeedsDisplay();
		}
	}
}
