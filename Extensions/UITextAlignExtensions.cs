using System;
using UnityEngine;

namespace WellFired.Guacamole.Unity.Editor
{
	public static class UITextAlignExtensions
	{
		public static TextAnchor Combine(UITextAlign horizontalAlign, UITextAlign verticalAlign)
		{
			switch (horizontalAlign)
			{
				case UITextAlign.Start:
					switch(verticalAlign)
					{
						case UITextAlign.Start: return TextAnchor.UpperLeft;
						case UITextAlign.Midle: return TextAnchor.UpperCenter;
						case UITextAlign.End: return TextAnchor.UpperRight;
						default:
							throw new ArgumentOutOfRangeException("verticalAlign", verticalAlign, null);
					}
				case UITextAlign.Midle:
					switch (verticalAlign)
					{
						case UITextAlign.Start: return TextAnchor.MiddleLeft;
						case UITextAlign.Midle: return TextAnchor.MiddleCenter;
						case UITextAlign.End: return TextAnchor.MiddleRight;
						default:
							throw new ArgumentOutOfRangeException("verticalAlign", verticalAlign, null);
					}
				case UITextAlign.End:
					switch (verticalAlign)
					{
						case UITextAlign.Start: return TextAnchor.LowerLeft;
						case UITextAlign.Midle: return TextAnchor.LowerCenter;
						case UITextAlign.End: return TextAnchor.LowerRight;
						default:
							throw new ArgumentOutOfRangeException("verticalAlign", verticalAlign, null);
					}
				default:
					throw new ArgumentOutOfRangeException("horizontalAlign", horizontalAlign, null);
			}
		}
	}
}