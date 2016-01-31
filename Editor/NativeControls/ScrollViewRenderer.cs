using UnityEditor;
using WellFired.Guacamole;

[assembly : CustomRenderer(typeof(ScrollView), typeof(WellFired.Guacamole.Unity.Editor.ScrollViewRenderer))]
namespace WellFired.Guacamole.Unity.Editor
{
	public class ScrollViewRenderer : BaseRenderer
	{
		public override void Render(UIRect renderRect)
		{
			UnityEditor.EditorGUI.DrawRect(renderRect.ToUnityRect(), Control.BackgroundColor.ToUnityColor());
		}
	}
}