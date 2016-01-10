using UnityEditor;
using WellFired.Guacamole;

namespace WellFired.Guacamole.Unity.Editor
{
	public abstract class BaseRenderer : INativeRenderer
	{
		#region INativeRenderer implementation
		public ViewBase Control 
		{
			get;
			set;
		}

		public abstract void Render(UIRect renderRect);
		#endregion
	}
}