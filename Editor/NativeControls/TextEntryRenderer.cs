using UnityEditor;
using WellFired.Guacamole;

[assembly : CustomRenderer(typeof(TextEntry), typeof(WellFired.Guacamole.Unity.Editor.TextEntryRenderer))]
namespace WellFired.Guacamole.Unity.Editor
{
	public class TextEntryRenderer : BaseRenderer
	{
		public override void Render(UIRect renderRect)
		{
			var entry = Control as TextEntry;
			entry.Text = UnityEditor.EditorGUI.TextField(renderRect.ToUnityRect(), entry.Label, entry.Text);
		}
	}
}