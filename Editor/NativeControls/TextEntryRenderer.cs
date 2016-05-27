using UnityEngine;
using WellFired.Guacamole;

[assembly : CustomRenderer(typeof(TextEntry), typeof(WellFired.Guacamole.Unity.Editor.TextEntryRenderer))]
namespace WellFired.Guacamole.Unity.Editor
{
	public class TextEntryRenderer : BaseRenderer
	{
		private GUIStyle TextStyle { get; set; }
		private Texture2D BackgroundTexture { get; set; } 

		public override void Render(UIRect renderRect)
		{
			if (TextStyle == null)
				TextStyle = new GUIStyle();

			if (BackgroundTexture == null)
			{
				BackgroundTexture = Texture2DExtensions.CreateRoundedTexture(
					64,
					64,
					Control.BackgroundColor.ToUnityColor(),
					Control.BackgroundColor.ToUnityColor(),
					64.0f);
			}

			var entry = Control as TextEntry;

			TextStyle.focused.background = BackgroundTexture;
			TextStyle.active.background = BackgroundTexture;
			TextStyle.hover.background = BackgroundTexture;
			TextStyle.normal.background = BackgroundTexture;

			TextStyle.alignment = UITextAlignExtensions.Combine(entry.HorizontalTextAlign, entry.VerticalTextAlign);

			TextStyle.focused.textColor = entry.TextColor.ToUnityColor();
			TextStyle.active.textColor = entry.TextColor.ToUnityColor();
			TextStyle.hover.textColor = entry.TextColor.ToUnityColor();
			TextStyle.normal.textColor = entry.TextColor.ToUnityColor();

			const int offset = 16;
			TextStyle.border = new RectOffset(offset, offset, offset, offset);

			entry.Text = UnityEditor.EditorGUI.TextField(renderRect.ToUnityRect(), entry.Text, TextStyle);
		}
	}
}