using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Device.Location;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Windows.Devices.Geolocation;
using Core.Helpers.Controls;
using Microsoft.Phone.Maps.Controls;
using Microsoft.Phone.Maps.Toolkit;
using WinPhone.Helpers;
using WinPhone.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Platform.WinPhone;
using Map = Microsoft.Phone.Maps.Controls.Map;
using Point = System.Windows.Point;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace WinPhone.Renderers
{
  public class CustomMapRenderer : ViewRenderer<CustomMap, Map>
  {
    private Map _nativeMap;
    private CustomMap _formsMap;
    private CustomPin _selectedPin;

    protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      base.OnElementPropertyChanged(sender, e);

      if (_nativeMap == null)
      {
        _formsMap = (CustomMap) sender;
        _formsMap.NavigationButton.Clicked += NavigationButtonOnClicked;
        //Center position Chicago
        const double latitude = 41.951692;
        const double longitude = -87.993720;

        _nativeMap = new Map {ZoomLevel = 9, Center = new GeoCoordinate(latitude, longitude)};
        //_nativeMap.Tap += NativeMapOnTap;
        
        this.SetNativeControl(_nativeMap);

        AddCurrentLocationToMap();

        foreach (var formsPin in _formsMap.CustomPins)
        {
          var nativePin = new Pushpin();
          nativePin.Tap += NativePinOnTap;
          
          var geoCoordinate = new GeoCoordinate(formsPin.Position.Latitude, formsPin.Position.Longitude);
          nativePin.GeoCoordinate = geoCoordinate;
          nativePin.Content = formsPin.Label + "\n" + formsPin.Address;
         
          var nativePinMapOverlay = new MapOverlay();
          nativePinMapOverlay.Content = nativePin;
          nativePinMapOverlay.GeoCoordinate = geoCoordinate;
          
          var mapLayer = new MapLayer();
          mapLayer.Add(nativePinMapOverlay);

          _nativeMap.Layers.Add(mapLayer);
        }
      }
    }

    private async void NavigationButtonOnClicked(object sender, EventArgs eventArgs)
    {
      var uri = new Uri(string.Format("ms-drive-to:?destination.latitude={0}&destination.longitude={1}&destination.address={2}&destination.name={3}", _selectedPin.Position.Latitude,
         _selectedPin.Position.Longitude,_selectedPin.Address, _selectedPin.Label));

      await Windows.System.Launcher.LaunchUriAsync(uri);
    }

    private void NativePinOnTap(object sender, GestureEventArgs gestureEventArgs)
    {
      var nativePin = (Pushpin)sender;

      var contentArray = nativePin.Content.ToString().Split('\n');

      var label = contentArray[0];
      var description = contentArray[1];

      _selectedPin = new CustomPin
      {
        Label = label,
        Address = description,
        Position = new Position(nativePin.GeoCoordinate.Latitude, nativePin.GeoCoordinate.Longitude)
      };

      _formsMap.SelectedPin = _selectedPin;

      _formsMap.ShowFooter = true;
    }
   
    private async Task AddCurrentLocationToMap()
    {
      var myGeolocator = new Geolocator();
      var myGeoposition = await myGeolocator.GetGeopositionAsync();
      var myGeocoordinate = myGeoposition.Coordinate;
      var myGeoCoordinate =
          CoordinateConverter.ConvertGeocoordinate(myGeocoordinate);
      var myCircle = new Ellipse();
      myCircle.Fill = new SolidColorBrush(Colors.Blue);
      myCircle.Height = 20;
      myCircle.Width = 20;
      myCircle.Opacity = 50;

      var myLocationOverlay = new MapOverlay();
      myLocationOverlay.Content = myCircle;
      myLocationOverlay.PositionOrigin = new Point(0.5, 0.5);
      myLocationOverlay.GeoCoordinate = myGeoCoordinate;

      // Create a MapLayer to contain the MapOverlay.
      var myLocationLayer = new MapLayer();
      myLocationLayer.Add(myLocationOverlay);

      _nativeMap.Layers.Add(myLocationLayer);
    }
  }
}