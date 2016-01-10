using System;
using UnityEngine;

namespace WellFired.Guacamole.Unity.Editor
{
	[Serializable]
	internal class ApplicationInitializationContextScriptableObject : ScriptableObject, IInitializationContext
	{
		[SerializeField]
		private string mainContentString;

		[SerializeField]
		private Type mainContent;
		public Type MainContent
		{
			get 
			{
				if (mainContent == null) 
				{
					mainContent = Type.GetType(mainContentString);
				}

				return mainContent; 
			}
			set 
			{ 
				mainContent = value; 
				mainContentString = mainContent.AssemblyQualifiedName;
			}
		}

		[SerializeField]
		private UIRect uirect;
		public UIRect UIRect
		{
			get { return uirect; }
			set { uirect = value; }
		}

		[SerializeField]
		private UISize minSize;
		public UISize MinSize
		{
			get { return minSize; }
			set { minSize = value; }
		}

		[SerializeField]
		private UISize maxSize;
		public UISize MaxSize
		{
			get { return maxSize; }
			set { maxSize = value; }
		}

		[SerializeField]
		private string title;
		public string Title
		{
			get { return title; }
			set { title = value; }
		}

		#region IInitializationContext implementation
		public void ValidateSetup()
		{
			if (MainContent == null)
				throw new InitializationContextInvalid();
		}
		#endregion
	}

	public class ApplicationInitializationContext
	{
		public Type MainContent
		{
			get { return ScriptableObject.MainContent; }
			set { ScriptableObject.MainContent = value; }
		}

		public UIRect UIRect
		{
			get { return ScriptableObject.UIRect; }
			set { ScriptableObject.UIRect = value; }
		}

		public UISize MinSize
		{
			get { return ScriptableObject.MinSize; }
			set { ScriptableObject.MinSize = value; }
		}

		public UISize MaxSize
		{
			get { return ScriptableObject.MaxSize; }
			set { ScriptableObject.MaxSize = value; }
		}
			
		public string Title
		{
			get { return ScriptableObject.Title; }
			set { ScriptableObject.Title = value; }
		}
		
		internal ApplicationInitializationContextScriptableObject ScriptableObject
		{
			get;
			set;
		}

		public ApplicationInitializationContext()
		{
			ScriptableObject = UnityEngine.ScriptableObject.CreateInstance<ApplicationInitializationContextScriptableObject>();
		}
	}
}