﻿using System;
using Xamarin.Forms.Maps.Android;
using Android.Gms.Maps;
using Xamarin.Forms;
using Android.Gms.Maps.Model;
using Xamarin.Forms.Maps;
using Core;
using Core.Helpers.Controls;

[assembly: ExportRenderer (typeof(Core.Helpers.Controls.CustomMap), typeof(Android.MapViewRenderer))]
namespace Android
{
	public class MapViewRenderer : MapRenderer
	{
		bool _isDrawnDone;

		protected override void OnElementPropertyChanged (object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged (sender, e);

			var androidMapView = (MapView)Control;
			var formsMap = (CustomMap)sender;
			 
			if (e.PropertyName.Equals ("VisibleRegion") && !_isDrawnDone) {
				androidMapView.Map.Clear ();

				androidMapView.Map.MarkerClick += HandleMarkerClick;
				androidMapView.Map.MapClick += HandleMapClick;
				androidMapView.Map.MyLocationEnabled = formsMap.IsShowingUser;

				//The footer overlays the zoom controls
				androidMapView.Map.UiSettings.ZoomControlsEnabled = false;

				var formsPins = formsMap.CustomPins;

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

		void HandleMapClick (object sender, GoogleMap.MapClickEventArgs e)
		{
			var customMapControl = this.Element as CustomMap;

			customMapControl.ShowFooter = false;

			ResetPrevioslySelectedMarker ();
		}

		void ResetPrevioslySelectedMarker ()
		{
			//todo : This should reset to the default icon for the pin (right now the icon is hard coded)
			if (_previouslySelectedMarker != null) {
				_previouslySelectedMarker.SetIcon (BitmapDescriptorFactory.FromAsset (String.Format ("{0}.png", Icons.CrazyRobot))); 
				_previouslySelectedMarker = null;
			}
		}

		void HandleMarkerClick (object sender, GoogleMap.MarkerClickEventArgs e)
		{
			ResetPrevioslySelectedMarker ();

			var currentMarker = e.Marker;

			currentMarker.SetIcon (BitmapDescriptorFactory.DefaultMarker ());

			var customMapControl = this.Element as CustomMap;

			var formsPin = new CustomPin {
				Label = currentMarker.Title,
				Address = currentMarker.Snippet,
				Position = new Position (currentMarker.Position.Latitude, currentMarker.Position.Longitude)
			};

			customMapControl.SelectedPin = formsPin;
			customMapControl.ShowFooter = true;

			_previouslySelectedMarker = currentMarker;
		}
	}
}