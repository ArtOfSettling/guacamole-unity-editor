using WellFired.Guacamole;

namespace WellFired.Guacamole.Unity.Editor
{
	public static class UnityVector2Extensions 
	{
		public static UISize ToUISize(this UnityEngine.Vector2 source)
		{
			if(source == null)
				return null;
			
			return new UISize ((int)source.x, (int)source.y);
		}
	}
}