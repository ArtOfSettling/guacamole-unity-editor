﻿using WellFired.Guacamole;

namespace WellFired.Guacamole.Unity.Editor
{
	public static class UIExtensions 
	{
		public static UnityEngine.Rect ToUnityRect(this UIRect source)
		{
			return new UnityEngine.Rect (source.X, source.Y, source.Width, source.Height);
		}
	}
}