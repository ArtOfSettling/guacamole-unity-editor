using UnityEditor;
using WellFired.Guacamole;

[assembly: CustomRenderer (typeof(Window), typeof(WellFired.Guacamole.Unity.Editor.WindowRenderer))]
namespace WellFired.Guacamole.Unity.Editor
{
	public class WindowRenderer : BaseRenderer
	{
		
	}
}