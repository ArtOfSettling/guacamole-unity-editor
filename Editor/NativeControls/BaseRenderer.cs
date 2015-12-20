using UnityEditor;
using WellFired.Guacamole;

namespace WellFired.Guacamole.Unity.Editor
{
	public class BaseRenderer : INativeRenderer
	{
		#region INativeRenderer implementation
		public ViewBase Control 
		{
			get;
			set;
		}
		#endregion
	}
}