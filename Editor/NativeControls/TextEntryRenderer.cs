using System.ComponentModel;
using UnityEngine;
using WellFired.Guacamole;

[assembly : CustomRenderer(typeof(TextEntry), typeof(WellFired.Guacamole.Unity.Editor.TextEntryRenderer))]
namespace WellFired.Guacamole.Unity.Editor
{
	public class TextEntryRenderer : BaseRenderer
	{
		private GUIStyle TextStyle { get; set; }

		public override void Render(UIRect renderRect)
		{
			base.Render(renderRect);

			if (TextStyle == null)
				TextStyle = new GUIStyle();

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

			var offset = (float)entry.CornerRadius;
			var smallest = (int)(Mathf.Min(offset, Mathf.Min(renderRect.Width * 0.5f, renderRect.Height * 0.5f)) + 0.5f);
			smallest = Mathf.Max(smallest, 2);
			TextStyle.border = new RectOffset(smallest, smallest, smallest, smallest);
			TextStyle.padding = new RectOffset(smallest, smallest, 0, 0);

			entry.Text = UnityEditor.EditorGUI.TextField(renderRect.ToUnityRect(), entry.Text, TextStyle);
		}
	}
}