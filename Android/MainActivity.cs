
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;

using Xamarin.Forms.Platform.Android;
using Core;
using Android.Net;
using Android.Views;


namespace ChiVball.Android
{
	[Activity (MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : AndroidActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			this.Window.AddFlags (WindowManagerFlags.Fullscreen); //to hide
			Xamarin.FormsMaps.Init (this, bundle);
			Xamarin.Forms.Forms.Init (this, bundle);

			SetPage (App.GetMainPage ());

		}

		public  void LaunchGoogleMaps (string address)
		{
			var geoUri = Uri.Parse (string.Format ("geo:0,0?q={0}", address));
			var mapIntent = new Intent (Intent.ActionView, geoUri);
			StartActivity (mapIntent);
		}
	}
}

