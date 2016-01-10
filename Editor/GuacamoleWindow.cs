using System;
using UnityEditor;
using UnityEngine;

namespace WellFired.Guacamole.Unity.Editor
{
	public class GuacamoleWindow : EditorWindow, IWindow
	{
		private ApplicationInitializationContextScriptableObject ApplicationInitializationContextScriptableObject
		{ get; set; }

		[SerializeField]
		private WellFired.Guacamole.Window window;

		public string Title
		{
			get { return this.titleContent.text; }
			set { this.titleContent.text = value; }
		}

		public UIRect Rect 
		{
			get { return this.position.ToUIRect(); }
			set { this.position = value.ToUnityRect(); }
		}

		public UISize MinSize 
		{
			get { return this.minSize.ToUISize(); }
			set { this.minSize = value.ToUnityVector2(); }
		}

		public UISize MaxSize 
		{
			get { return this.maxSize.ToUISize(); }
			set { this.maxSize = value.ToUnityVector2(); }
		}

		public WellFired.Guacamole.Window MainContent 
		{ 
			get 
			{
				if(window == null) 
				{
					var contentType = ApplicationInitializationContextScriptableObject.MainContent;
					window = (Window)contentType.GetConstructor(new Type[] { }).Invoke(new object[] { });

					if (window == null)
						throw new GuacamoleWindowCantBeCreated();
				}

				return window;
			}
		}

		public void Launch(IInitializationContext initializationContext)
		{
			initializationContext.ValidateSetup();

			ApplicationInitializationContextScriptableObject = initializationContext as ApplicationInitializationContextScriptableObject;
			if(ApplicationInitializationContextScriptableObject == null)
				throw new InitializationContextNull();
			
			Title = ApplicationInitializationContextScriptableObject.Title;
			Rect = ApplicationInitializationContextScriptableObject.UIRect;
			MinSize = ApplicationInitializationContextScriptableObject.MinSize;
			MaxSize = ApplicationInitializationContextScriptableObject.MaxSize;

			if(MaxSize == UISize.Min)
				MaxSize = new UISize(100000, 100000);

			Debug.Log("Constructing : " + MainContent);
		}

		public void OnGUI()
		{
			if (Event.current.type == EventType.Layout) 
			{
				try
				{
					MainContent.Layout (Rect);
				}
				catch(Exception e) 
				{
					Debug.Log ("Exception was thrown whilst performing Layout : " + e);
				}
			}
			if (Event.current.type == EventType.Repaint) 
			{
				try
				{
					MainContent.Render ();
				}
				catch(Exception e) 
				{
					Debug.Log ("Exception was thrown whilst performing Repaint : " + e);
				}
			}
		}
	}
}