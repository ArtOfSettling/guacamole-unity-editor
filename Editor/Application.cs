using UnityEditor;
using WellFired.Guacamole;
using System.Reflection;

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
			NativeRendererHelper.LaunchedAssembly = Assembly.GetExecutingAssembly();
			MainWindow = EditorWindow.GetWindow<GuacamoleWindow>();
		}
	}
}