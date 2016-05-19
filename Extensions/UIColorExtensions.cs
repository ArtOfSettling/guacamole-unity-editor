namespace WellFired.Guacamole.Unity.Editor
{
	public static class UIColorExtensions 
	{
		public static UnityEngine.Color ToUnityColor(this UIColor source)
		{
			return new UnityEngine.Color (source.R, source.G, source.B);
		}
	}
}