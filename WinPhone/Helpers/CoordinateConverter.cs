using System;
using System.Device.Location;
using Windows.Devices.Geolocation;

namespace WinPhone.Helpers
{
  public static class CoordinateConverter
  {
    public static GeoCoordinate ConvertGeocoordinate(Geocoordinate geocoordinate)
    {
      return new GeoCoordinate
          (
          geocoordinate.Latitude,
          geocoordinate.Longitude,
          geocoordinate.Altitude ?? Double.NaN,
          geocoordinate.Accuracy,
          geocoordinate.AltitudeAccuracy ?? Double.NaN,
          geocoordinate.Speed ?? Double.NaN,
          geocoordinate.Heading ?? Double.NaN
          );
    }
  }
}
