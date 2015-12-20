using System;
using UnityEditor;
using UnityEngine;

namespace WellFired.Guacamole.Unity.Editor
{
	public class GuacamoleWindow : EditorWindow, IWindow
	{
		public Window MainContent
		{
			get;
			set;
		}

		public string Title
		{
			get 
			{
				return this.titleContent.text;
			}
			set 
			{
				this.titleContent.text = value;
			}
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

		public void OnGUI()
		{
			if(Event.current.type == EventType.Layout)
				MainContent.Layout ();
			if(Event.current.type == EventType.Repaint)
				MainContent.Render ();
		}
	}
}