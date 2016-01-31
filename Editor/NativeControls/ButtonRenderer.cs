using UnityEditor;
using WellFired.Guacamole;

[assembly : CustomRenderer(typeof(Button), typeof(WellFired.Guacamole.Unity.Editor.ButtonRenderer))]
namespace WellFired.Guacamole.Unity.Editor
{
	public class ButtonRenderer : BaseRenderer
	{
		public override void Render(UIRect renderRect)
		{
			UnityEditor.EditorGUI.DrawRect(renderRect.ToUnityRect(), Control.BackgroundColor.ToUnityColor());
		}
	}
}