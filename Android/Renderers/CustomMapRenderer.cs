using System;
using Xamarin.Forms.Maps.Android;
using Android.Gms.Maps;
using Xamarin.Forms;
using Android.Gms.Maps.Model;
using Core.Helpers.Controls;

[assembly: ExportRenderer (typeof(CustomMap), typeof(Android.CustomMapRenderer))]

namespace Android
{
  public class CustomMapRenderer : MapRenderer
  {
    private bool _isDrawnDone;
    private Marker _previouslySelectedMarker { get; set; }
    private CustomPin _previouslySelectedPin { get; set; }
    
    private CustomMap _customMap
    {
      get { return Element as CustomMap; }
    }

    private CustomMapContentView _customMapContentView
    {
      get { return _customMap.Parent.Parent as CustomMapContentView; }
    }

    protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
      //base.OnElementPropertyChanged (sender, e);

      var androidMapView = (MapView) Control;

      if (e.PropertyName.Equals("CenterOnPosition"))
      {
        CenterOnLocation(new LatLng(_customMap.CenterOnPosition.Latitude, _customMap.CenterOnPosition.Longitude),
          _customMap.CameraFocusYOffset);
      }

      if (e.PropertyName.Equals("VisibleRegion") && !_isDrawnDone)
      {
        androidMapView.Map.Clear();

        androidMapView.Map.MarkerClick += HandleMarkerClick;
        androidMapView.Map.MapClick += HandleMapClick;
        androidMapView.Map.MyLocationEnabled = _customMap.IsShowingUser;

        //The footer overlays the zoom controls
        androidMapView.Map.UiSettings.ZoomControlsEnabled = false;

        var formsPins = _customMap.CustomPins;

        foreach (var formsPin in formsPins)
        {
          var markerWithIcon = new MarkerOptions();


          markerWithIcon.SetPosition(new LatLng(formsPin.Position.Latitude, formsPin.Position.Longitude));
          markerWithIcon.SetTitle(formsPin.Label);
          markerWithIcon.SetSnippet(formsPin.Address);

          if (!string.IsNullOrEmpty(formsPin.PinIcon))
            markerWithIcon.InvokeIcon(BitmapDescriptorFactory.FromAsset(String.Format("{0}.png", formsPin.PinIcon)));
          else
            markerWithIcon.InvokeIcon(BitmapDescriptorFactory.DefaultMarker());

          androidMapView.Map.AddMarker(markerWithIcon);
        }

        _isDrawnDone = true;
      }
    }

    private void HandleMapClick(object sender, GoogleMap.MapClickEventArgs e)
    {
      _customMapContentView.FooterMode = FooterMode.Hidden;

      ResetPrevioslySelectedMarker();
    }

    private void ResetPrevioslySelectedMarker()
    {
      //todo : This should reset to the default icon for the pin (right now the icon is hard coded)
      if (_previouslySelectedMarker != null)
      {
        _previouslySelectedMarker.SetIcon(
          BitmapDescriptorFactory.FromAsset(String.Format("{0}.png", _previouslySelectedPin.PinIcon)));
        _previouslySelectedMarker = null;
      }
    }

    private void HandleMarkerClick(object sender, GoogleMap.MarkerClickEventArgs e)
    {
      ResetPrevioslySelectedMarker();

      var currentMarker = e.Marker;

      CenterOnLocation(currentMarker.Position);

      currentMarker.SetIcon(BitmapDescriptorFactory.DefaultMarker());

      _customMap.SelectedPinAddress = currentMarker.Snippet;

      if (_customMapContentView.FooterMode == FooterMode.Hidden)
      {
        _customMapContentView.FooterMode = FooterMode.Minimized;
      }

      _previouslySelectedPin = _customMap.SelectedPin;
      _previouslySelectedMarker = currentMarker;
    }

    private void CenterOnLocation(LatLng location, int yOffset = 100)
    {
      var mapView = (MapView) Control;

      var projection = mapView.Map.Projection;

      var screenLocation = projection.ToScreenLocation(location);
      screenLocation.Y += yOffset;

      var offsetTarget = projection.FromScreenLocation(screenLocation);

      // Animate to the calculated lat/lng
      mapView.Map.AnimateCamera(CameraUpdateFactory.NewLatLng(offsetTarget));
    }
  }
}