using System.ComponentModel;

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

		public virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
		{

		}
		#endregion
	}
}