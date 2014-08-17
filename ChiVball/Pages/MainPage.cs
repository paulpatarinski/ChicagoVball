using Xamarin.Forms;
using Core;
using Xamarin.Forms.Maps;

namespace ChiVball
{
	public class MainPage : ContentPage
	{
		public MainPage ()
		{
			BackgroundColor = Color.White;

			var mainGrid = new Grid {
				RowDefinitions = new RowDefinitionCollection {
					new RowDefinition {
						Height = new GridLength (0.88, GridUnitType.Star)

					},
					new RowDefinition {
						Height = new GridLength (0.12, GridUnitType.Star)
					},
				},
				ColumnDefinitions = new ColumnDefinitionCollection {
					new ColumnDefinition {
						Width = new GridLength (1, GridUnitType.Star)
					}
				}
			};

			var map = CreateMap ();
			mainGrid.Children.Add (map, 0, 0);

			Grid.SetRowSpan (map, 2);
			mainGrid.Children.Add (CreateFooter (), 0, 1);

			Content = mainGrid;
		}

		CustomMap _map {
			get;
			set;
		}

		View CreateMap ()
		{
			var latitude = 43.0714;
			var longitude = -89.3932;

			var location = new Position (latitude, longitude);

			_map = new CustomMap (MapSpan.FromCenterAndRadius (location, Distance.FromMiles (10))){ };

			for (int i = 1; i <= 10; i++) {

				latitude += 0.001;

				_map.CustomPins.Add (new CustomPin {
					Label = "Pin " + i, 
					Address = "Address " + i,
					Position = new Position (latitude, longitude),
					PinIcon = "CarWashMapIcon"
				});
			}


			return new ContentView (){ Content = _map };
		}

		View CreateFooter ()
		{
			var placeNameLabel = new Label {
				VerticalOptions = LayoutOptions.Center,
				HorizontalOptions = LayoutOptions.Center,
				Text = "Pin Label Shows Here" 
			};

			placeNameLabel.BindingContext = _map;
			placeNameLabel.SetBinding<CustomMap> (Label.TextProperty, vm => vm.SelectedPin.Label);


			var detailsLabel = new Label {
				VerticalOptions = LayoutOptions.Center,
				HorizontalOptions = LayoutOptions.Center,
				Text = "Address Shows Here"
			};

			detailsLabel.BindingContext = _map;
			detailsLabel.SetBinding<CustomMap> (Label.TextProperty, vm => vm.SelectedPin.Address);


			var footerStackLayout = new StackLayout { BackgroundColor = Colors.TransparentWhite
			};

			footerStackLayout.Children.Add (placeNameLabel);
			footerStackLayout.Children.Add (detailsLabel);


			return new ContentView{ Content = footerStackLayout };
		}
	}
}