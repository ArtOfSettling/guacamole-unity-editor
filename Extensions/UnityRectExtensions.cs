using WellFired.Guacamole;

namespace WellFired.Guacamole.Unity.Editor
{
	public static class UnityRectExtensions 
	{
		public static UIRect ToUIRect(this UnityEngine.Rect source)
		{
			if(source == null)
				return null;
			
			return new UIRect((int)source.x, (int)source.y, (int)source.width, (int)source.height);
		}
	}
}