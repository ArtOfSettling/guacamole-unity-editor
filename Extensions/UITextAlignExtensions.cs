﻿using System;
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
						case UITextAlign.Middle: return TextAnchor.MiddleLeft;
						case UITextAlign.End: return TextAnchor.LowerLeft;
						default:
							throw new ArgumentOutOfRangeException("verticalAlign", verticalAlign, null);
					}
				case UITextAlign.Middle:
					switch (verticalAlign)
					{
						case UITextAlign.Start: return TextAnchor.UpperCenter;
						case UITextAlign.Middle: return TextAnchor.MiddleCenter;
						case UITextAlign.End: return TextAnchor.LowerCenter;
						default:
							throw new ArgumentOutOfRangeException("verticalAlign", verticalAlign, null);
					}
				case UITextAlign.End:
					switch (verticalAlign)
					{
						case UITextAlign.Start: return TextAnchor.UpperRight;
						case UITextAlign.Middle: return TextAnchor.MiddleRight;
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