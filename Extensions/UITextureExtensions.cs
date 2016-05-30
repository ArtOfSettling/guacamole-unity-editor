using System.IO;
using UnityEngine;
using WellFired.Guacamole.Drawing;
using Rect = WellFired.Guacamole.Drawing.Rect;

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

		public static Texture2D CreateRoundedTexture(int width, int height, UIColor backgroundColor, UIColor outlineColor, double radius)
		{
			var result = new Texture2D(width, height)
			{
				wrapMode = TextureWrapMode.Clamp
			};

			var path = new GraphicsPath();

			var diameter = radius * 2;

			var upperLeft = new Rect(0, 0, diameter, diameter);
			path.AddArc(upperLeft, 180, 90);

			var upperRight = new Rect(width - diameter, 0, diameter, diameter);
			path.AddArc(upperRight, 90, 90);
			
			var lowerRight = new Rect(width - diameter, height - diameter, diameter, diameter);
			path.AddArc(lowerRight, 0, 90);

			var lowerLeft = new Rect(0, height - diameter, diameter, diameter);
			path.AddArc(lowerLeft, 270, 90);

			path.Close();

			var pixelData = path.Draw(width, height, backgroundColor);

			var unityPixelData = new Color[pixelData.Length];
			for (var index = 0; index < unityPixelData.Length; index++)
				unityPixelData[index] = pixelData[index].ToUnityColor();

			result.SetPixels(unityPixelData);
			result.Apply();
			result.hideFlags = HideFlags.HideAndDontSave;

			var bytes = result.EncodeToPNG();
			File.WriteAllBytes(Application.dataPath + "/../SavedScreen.png", bytes);

			return result;
		}
	}
}