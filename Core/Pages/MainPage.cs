using Core.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Core.ViewModels;
using Core.Services;
using Core.Helpers.Controls;

namespace Core.Pages
{
	public class MainPage : ContentPage
	{
		MainPageViewModel _viewModel;

		public MainPage ()
		{
			_viewModel = new MainPageViewModel (new VolleyballLocationService ());
			BindingContext = _viewModel;

			BackgroundColor = Color.White;

			//Coordinates for the starting point of the map
			const double latitude = 41.951692;
			const double longitude = -87.993720;

			var location = new Position (latitude, longitude);

			var map = new CustomMap (MapSpan.FromCenterAndRadius (location, Distance.FromMiles (40))){ IsShowingUser = true };

			map.BindingContext = _viewModel;
			map.SetBinding<MainPageViewModel> (CustomMap.CustomPinsProperty, x => x.VolleyballLocations);
		
			Content = map.GetMapGrid ();
		}
	}
}