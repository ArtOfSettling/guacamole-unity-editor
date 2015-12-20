using UnityEditor;
using WellFired.Guacamole;

namespace WellFired.Guacamole.Unity.Editor
{
	public class Application : WellFired.Guacamole.IApplication
	{
		public IWindow MainWindow 
		{
			get;
			set;
		}

		public void Launch()
		{
			MainWindow = EditorWindow.GetWindow<GuacamoleWindow>();
		}
	}
}