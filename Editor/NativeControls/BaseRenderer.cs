using System.ComponentModel;
using UnityEngine;

namespace WellFired.Guacamole.Unity.Editor
{
	public abstract class BaseRenderer : INativeRenderer
	{
		public ViewBase Control 
		{
			get;
			set;
		}

		protected Texture2D BackgroundTexture { get; set; }

		public virtual void Render(UIRect renderRect)
		{
			if (BackgroundTexture == null)
				CreateBackgroundTexture();
		}

		public virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == ViewBase.CornerRadiusProperty.PropertyName ||
			    e.PropertyName == ViewBase.BackgroundColorProperty.PropertyName ||
			    e.PropertyName == ViewBase.OutlineColorProperty.PropertyName)
			{
				CreateBackgroundTexture();
			}
		}

		private void CreateBackgroundTexture()
		{
			BackgroundTexture = Texture2DExtensions.CreateRoundedTexture(
				64,
				64,
				Control.BackgroundColor,
				Control.OutlineColor,
				Control.CornerRadius,
				Control.CornerMask);
		}
	}
}