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

			var mainGrid = new Grid {
				RowDefinitions = new RowDefinitionCollection {
					new RowDefinition {
						Height = new GridLength (0.87, GridUnitType.Star)
					},
					new RowDefinition {
						Height = new GridLength (0.13, GridUnitType.Star)
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
			mainGrid.RowSpacing = 0;

			mainGrid.Children.Add (CreateFooter (), 0, 1);

			//Bind the footer to the ShowFooter property
			mainGrid.BindingContext = _map;
			mainGrid.Children [1].SetBinding<CustomMap> (IsVisibleProperty, x => x.ShowFooter);

			Content = mainGrid;
		}

		CustomMap _map {
			get;
			set;
		}

		View CreateMap ()
		{
			//Coordinates for the starting point of the map
			const double latitude = 41.951692;
			const double longitude = -87.993720;

			var location = new Position (latitude, longitude);

			_map = new CustomMap (MapSpan.FromCenterAndRadius (location, Distance.FromMiles (40))){ IsShowingUser = true };

			_map.BindingContext = _viewModel;
			_map.SetBinding<MainPageViewModel> (CustomMap.CustomPinsProperty, x => x.VolleyballLocations);

			return new ContentView (){ Content = _map };
		}

		View CreateFooter ()
		{
			var footerGrid = new Grid {
				RowDefinitions = new RowDefinitionCollection {
					new RowDefinition {
						Height = new GridLength (1, GridUnitType.Star)
					}
				},
				ColumnDefinitions = new ColumnDefinitionCollection {
					new ColumnDefinition {
						Width = new GridLength (0.75, GridUnitType.Star)
					},
					new ColumnDefinition {
						Width = new GridLength (0.25, GridUnitType.Star)
					},
				}
			};

			var placeNameLabel = new Label {
				Text = "Pin Label Shows Here",
				TextColor = Color.Black,
				Font = Font.SystemFontOfSize (22)

			};

			placeNameLabel.BindingContext = _map;
			placeNameLabel.SetBinding<CustomMap> (Label.TextProperty, vm => vm.SelectedPin.Label);


			var detailsLabel = new Label {
				Text = "Address Shows Here",
				TextColor = Color.Gray,
				Font = Font.SystemFontOfSize (14)
			};

			detailsLabel.BindingContext = _map;
			detailsLabel.SetBinding<CustomMap> (Label.TextProperty, vm => vm.SelectedPin.Address);

			var pinInfoStackLayout = new StackLayout { Padding = new Thickness (25, 8, 0, 0)	};

			pinInfoStackLayout.Children.Add (placeNameLabel);
			pinInfoStackLayout.Children.Add (detailsLabel);
			pinInfoStackLayout.Spacing = 0;

			//todo : replace with ImageButton when Labs is fixed
			var navButton = new ImageButton () {
				Image = "navigate_icon",
				ImageHeightRequest = 140,
				ImageWidthRequest = 140,
				BackgroundColor = Colors.TransparentWhite,
				VerticalOptions = LayoutOptions.Center,
				HorizontalOptions = LayoutOptions.Center
			};

			_map.NavigationButton = navButton;


			footerGrid.Children.Add (pinInfoStackLayout, 0, 0);
			footerGrid.Children.Add (new ContentView (){ Content = navButton }, 1, 0);

			return new ContentView{ Content = footerGrid, BackgroundColor = Colors.TransparentWhite };
		}
	}
}