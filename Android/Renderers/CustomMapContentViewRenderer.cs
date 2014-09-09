using System;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using ChiVball.Android;
using Core.Helpers.Controls;

[assembly: ExportRenderer (typeof(Core.Helpers.Controls.CustomMapContentView), typeof(CustomMapContentViewRenderer))]
namespace ChiVball.Android
{
	public class CustomMapContentViewRenderer : ViewRenderer
	{
		CustomMapContentView _mapContentView { get { return this.Element as CustomMapContentView; } }
		//		Grid _customMapGrid { get { return _customMap.Parent as Grid; } }

		protected override void OnElementPropertyChanged (object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged (sender, e);

			if (e.PropertyName == "Height") {
				var test = _mapContentView;

//				var mapRow = _customMapGrid.RowDefinitions [0];
//				var footerRow = _customMapGrid.RowDefinitions [1];
//
//				mapRow.Height = new GridLength (_customMapContentView.MapHeight, GridUnitType.Star);
//				footerRow.Height = new GridLength (_customMapContentView.FooterHeight, GridUnitType.Star);
//
			}
		}
	}
}

