using UnityEditor;
using WellFired.Guacamole;

[assembly : CustomRenderer(typeof(Button), typeof(WellFired.Guacamole.Unity.Editor.ButtonRenderer))]
namespace WellFired.Guacamole.Unity.Editor
{
	public class ButtonRenderer : BaseRenderer
	{
		public override void Render(UIRect renderRect)
		{
			var button = Control as Button;
			if(UnityEngine.GUI.Button(renderRect.ToUnityRect(), button.Text)) 
			{
				EditorUtility.DisplayDialog("a", "a", "a");
			}
		}
	}
}