using System;
using CoreGraphics;

namespace TouchTracker
{
	public struct Line
	{
		public Line(CGPoint begin, CGPoint end)
		{
			Begin = begin;
			End = end;
		}
		public static Line Empty = new Line(CGPoint.Empty, CGPoint.Empty);

		public CGPoint Begin { get; set; }
		public CGPoint End { get; set; }
	}
}
