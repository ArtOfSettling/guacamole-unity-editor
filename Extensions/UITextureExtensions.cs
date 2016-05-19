using UnityEngine;

namespace WellFired.Guacamole.Unity.Editor
{
	public static class Texture2DExtensions
	{
		public static Texture2D CreateTexture(int width, int height, Color colour)
		{
			var pixelColors = new Color[width * height];
			for (var i = 0; i < pixelColors.Length; i++)
				pixelColors[i] = colour;

			var result = new Texture2D(width, height)
			{
				wrapMode = TextureWrapMode.Repeat
			};

			result.SetPixels(pixelColors);
			result.Apply();
			result.hideFlags = HideFlags.HideAndDontSave;

			return result;
		}
	}
}