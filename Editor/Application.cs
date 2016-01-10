using System;
using System.Reflection;
using UnityEditor;
using WellFired.Guacamole;

namespace WellFired.Guacamole.Unity.Editor
{
	public class Application : WellFired.Guacamole.IApplication
	{
		public void Launch(ApplicationInitializationContext initializationContext)
		{
			if(initializationContext == null)
				throw new InitializationContextNull();

			NativeRendererHelper.LaunchedAssembly = Assembly.GetExecutingAssembly();
			var mainWindow = EditorWindow.GetWindow<GuacamoleWindow>();
			mainWindow.Launch(initializationContext.ScriptableObject);
		}
	}
}