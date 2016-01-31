using UnityEditor;
using WellFired.Guacamole;

[assembly : CustomRenderer(typeof(AdjacentLayout), typeof(WellFired.Guacamole.Unity.Editor.AdjacentLayoutRenderer))]
namespace WellFired.Guacamole.Unity.Editor
{
	public class AdjacentLayoutRenderer : BaseRenderer
	{
		public override void Render(UIRect renderRect)
		{
			UnityEditor.EditorGUI.DrawRect(renderRect.ToUnityRect(), Control.BackgroundColor.ToUnityColor());
		}
	}
}