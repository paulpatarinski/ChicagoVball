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
			mainGrid.Children [1].SetBinding<CustomMap> (VisualElement.IsVisibleProperty, x => x.ShowFooter);

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
				Text = "Address Shows Here"
			};

			detailsLabel.BindingContext = _map;
			detailsLabel.SetBinding<CustomMap> (Label.TextProperty, vm => vm.SelectedPin.Address);


			var pinInfoStackLayout = new StackLayout { Padding = new Thickness (25, 8, 0, 0)	};

			pinInfoStackLayout.Children.Add (placeNameLabel);
			pinInfoStackLayout.Children.Add (detailsLabel);
			pinInfoStackLayout.Spacing = 0;

			var navigationIconGrid = new Grid {
				RowDefinitions = new RowDefinitionCollection {
					new RowDefinition {
						Height = new GridLength (0.72, GridUnitType.Star)
					},
					new RowDefinition {
						Height = new GridLength (0.28, GridUnitType.Star)
					},
				},
				ColumnDefinitions = new ColumnDefinitionCollection {
					new ColumnDefinition {
						Width = new GridLength (1, GridUnitType.Star)
					}
				},
				VerticalOptions = LayoutOptions.Start,
			};

			var navigationIconImage = new Image { Source = "NavigateIcon", HeightRequest = 50 };

			var navigationTimeLabel = new Label {
				Text = "10 min", TextColor = Color.FromHex ("FF3A84DF"),
				Font = Font.SystemFontOfSize (13),
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Start
			};

			navigationIconGrid.Children.Add (navigationIconImage, 0, 0);
			navigationIconGrid.Children.Add (navigationTimeLabel, 0, 1);
			navigationIconGrid.RowSpacing = 1;
			Grid.SetRowSpan (navigationIconImage, 2);

			footerGrid.Children.Add (pinInfoStackLayout, 0, 0);
			footerGrid.Children.Add (navigationIconGrid, 1, 0);

			return new ContentView{ Content = footerGrid, BackgroundColor = Colors.TransparentWhite };
		}
	}
}