using WellFired.Guacamole;

namespace WellFired.Guacamole.Unity.Editor
{
	public static class UnityColorExtensions 
	{
		public static UIColor ToUISize(this UnityEngine.Color source)
		{
			return UIColor.FromRGB ((int)(source.r * 255.0f), (int)(source.g * 255.0f), (int)(source.b * 255.0f));
		}
	}
}