using UnityEditor;

namespace WellFired.Guacamole.Unity.Editor
{
	public class Application : UnityEditor.EditorWindow, IApplication
	{
		public ViewBase MainContent
		{
			get;
			set;
		}

		public UIRect Rect 
		{
			get 
			{
				return this.position.ToUIRect ();
			}
			set 
			{
				this.position = value.ToUnityRect ();
			}
		}

		public UISize MinSize 
		{
			get 
			{
				return this.minSize.ToUISize ();
			}
			set 
			{
				this.minSize = value.ToUnityVector2 ();
			}
		}

		public UISize MaxSize 
		{
			get 
			{
				return this.maxSize.ToUISize ();
			}
			set 
			{
				this.maxSize = value.ToUnityVector2 ();
			}
		}

		public static IApplication Launch()
		{
			var application = EditorWindow.GetWindow<Application>();
			application.Show();
			return application;
		}

		public void OnGUI()
		{

		}
	}
}