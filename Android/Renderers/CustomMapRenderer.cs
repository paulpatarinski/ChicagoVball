using System;
using Xamarin.Forms.Maps.Android;
using Android.Gms.Maps;
using Xamarin.Forms;
using Android.Gms.Maps.Model;
using Xamarin.Forms.Maps;
using Core;
using Core.Helpers.Controls;
using Android.Content;
using ChiVball.Android;

[assembly: ExportRenderer (typeof(Core.Helpers.Controls.CustomMap), typeof(Android.CustomMapRenderer))]
namespace Android
{
	public class CustomMapRenderer : MapRenderer
	{
		bool _isDrawnDone;

		CustomMap _customMap { get { return this.Element as CustomMap; } }

		Grid _customMapGrid { get { return _customMap.Parent as Grid; } }

		CustomMapContentView _customMapContentView { get { return _customMap.Parent.Parent as CustomMapContentView; } }


		protected override void OnElementPropertyChanged (object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged (sender, e);

			var androidMapView = (MapView)Control;

			if (e.PropertyName.Equals ("VisibleRegion") && !_isDrawnDone) {
				androidMapView.Map.Clear ();

				androidMapView.Map.MarkerClick += HandleMarkerClick;
				androidMapView.Map.MapClick += HandleMapClick;
				androidMapView.Map.MyLocationEnabled = _customMap.IsShowingUser;

				//The footer overlays the zoom controls
				androidMapView.Map.UiSettings.ZoomControlsEnabled = false;

				var formsPins = _customMap.CustomPins;

				foreach (var formsPin in formsPins) {
					var markerWithIcon = new MarkerOptions ();

					markerWithIcon.SetPosition (new LatLng (formsPin.Position.Latitude, formsPin.Position.Longitude));
					markerWithIcon.SetTitle (formsPin.Label);
					markerWithIcon.SetSnippet (formsPin.Address);

					if (!string.IsNullOrEmpty (formsPin.PinIcon))
						markerWithIcon.InvokeIcon (BitmapDescriptorFactory.FromAsset (String.Format ("{0}.png", formsPin.PinIcon)));
					else
						markerWithIcon.InvokeIcon (BitmapDescriptorFactory.DefaultMarker ());
						
					androidMapView.Map.AddMarker (markerWithIcon);
				}
			
				_isDrawnDone = true;
			}
		}

		Marker _previouslySelectedMarker {
			get;
			set;
		}

		CustomPin _previouslySelectedPin {
			get;
			set;
		}

		void HandleMapClick (object sender, GoogleMap.MapClickEventArgs e)
		{
			_customMapContentView.ShowFooter = false;

			ResetPrevioslySelectedMarker ();
		}

		void ResetPrevioslySelectedMarker ()
		{
			//todo : This should reset to the default icon for the pin (right now the icon is hard coded)
			if (_previouslySelectedMarker != null) {
				_previouslySelectedMarker.SetIcon (BitmapDescriptorFactory.FromAsset (String.Format ("{0}.png", _previouslySelectedPin.PinIcon))); 
				_previouslySelectedMarker = null;
			}
		}

		void HandleMarkerClick (object sender, GoogleMap.MarkerClickEventArgs e)
		{
			ResetPrevioslySelectedMarker ();

			var currentMarker = e.Marker;

			currentMarker.SetIcon (BitmapDescriptorFactory.DefaultMarker ());

			_customMap.SelectedPinAddress = currentMarker.Snippet;
			_customMapContentView.ShowFooter = true;

			_previouslySelectedPin = _customMap.SelectedPin;
			_previouslySelectedMarker = currentMarker;
		}
	}
}