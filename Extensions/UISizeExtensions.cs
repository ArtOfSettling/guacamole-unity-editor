using WellFired.Guacamole;

public static class UISizeExtensions 
{
	public static UnityEngine.Vector2 ToUnityVector2(this UISize source)
	{
		return new UnityEngine.Vector2 (source.Width, source.Height);
	}
}