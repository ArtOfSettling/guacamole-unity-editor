using UnityEditor;
using WellFired.Guacamole;

[assembly : CustomRenderer(typeof(NumberEntry), typeof(WellFired.Guacamole.Unity.Editor.NumberEntryRenderer))]
namespace WellFired.Guacamole.Unity.Editor
{
	public class NumberEntryRenderer : BaseRenderer
	{
		public override void Render(UIRect renderRect)
		{
			var entry = Control as NumberEntry;
			entry.Number = UnityEditor.EditorGUI.FloatField(renderRect.ToUnityRect(), entry.Label, entry.Number);
		}
	}
}