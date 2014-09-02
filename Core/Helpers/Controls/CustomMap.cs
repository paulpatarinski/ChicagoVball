using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Core.Pages;

namespace Core.Helpers.Controls
{
	public class CustomMap : Map
	{
		public CustomMap (MapSpan mapSpan) : base (mapSpan)
		{
			_mapGrid = new Grid {
				RowDefinitions = new RowDefinitionCollection {
					new RowDefinition {
						Height = new GridLength (EXPANDED_MAP_HEIGHT, GridUnitType.Star)
					},
					new RowDefinition {
						Height = new GridLength (COLLAPSED_FOOTER_HEIGHT, GridUnitType.Star)
					},
				},
				ColumnDefinitions = new ColumnDefinitionCollection {
					new ColumnDefinition {
						Width = new GridLength (1, GridUnitType.Star)
					}
				}
			};

			_mapGrid.Children.Add (this, 0, 0);
			Grid.SetRowSpan (this, 2);
			_mapGrid.RowSpacing = 0;

			_mapGrid.Children.Add (CreateFooter (), 0, 1);

			//Bind the footer to the ShowFooter property
			_mapGrid.BindingContext = this;
			_mapGrid.RowDefinitions [1].SetBinding<CustomMap> (HeightProperty, x => x.FooterHeight);
			_mapGrid.Children [1].SetBinding<CustomMap> (IsVisibleProperty, x => x.ShowFooter);

			_mapGrid.Children [1].GestureRecognizers.Add (new TapGestureRecognizer ((view, obj) => {
				//If the footer is collapsed, expand the footer and collapse the map
				if (FooterHeight == COLLAPSED_FOOTER_HEIGHT) {
					Grid.SetRowSpan (this, 1);
					MapHeight = COLLAPSED_MAP_HEIGHT;
					FooterHeight = EXPANDED_FOOTER_HEIGHT;
				} else {
					Grid.SetRowSpan (this, 2);
					MapHeight = EXPANDED_MAP_HEIGHT;
					FooterHeight = COLLAPSED_FOOTER_HEIGHT;
				}
			}));
		}

		static double COLLAPSED_FOOTER_HEIGHT = 0.13;
		static double COLLAPSED_MAP_HEIGHT = 0.3;
		static double EXPANDED_MAP_HEIGHT = 0.87;
		static double EXPANDED_FOOTER_HEIGHT = 0.7;

		public Button NavigationButton	{ get; set; }

		Grid _mapGrid;

		public static readonly BindableProperty SelectedPinProperty = BindableProperty.Create<CustomMap, CustomPin> (x => x.SelectedPin, new CustomPin{ Label = "test123" });

		public CustomPin SelectedPin {
			get{ return (CustomPin)base.GetValue (SelectedPinProperty); }
			set{ base.SetValue (SelectedPinProperty, value); }
		}

		public static readonly BindableProperty ShowFooterProperty = BindableProperty.Create<CustomMap, bool> (x => x.ShowFooter, false);

		public bool ShowFooter {
			get{ return (bool)base.GetValue (ShowFooterProperty); }
			set{ base.SetValue (ShowFooterProperty, value); }
		}

		public static readonly BindableProperty FooterHeightProperty = BindableProperty.Create<CustomMap, double> (x => x.FooterHeight, COLLAPSED_FOOTER_HEIGHT);

		public double FooterHeight {
			get{ return (double)base.GetValue (FooterHeightProperty); }
			set{ base.SetValue (FooterHeightProperty, value); }
		}

		public static readonly BindableProperty MapHeightProperty = BindableProperty.Create<CustomMap, double> (x => x.MapHeight, EXPANDED_MAP_HEIGHT);

		public double MapHeight {
			get{ return (double)base.GetValue (MapHeightProperty); }
			set{ base.SetValue (MapHeightProperty, value); }
		}

		public static readonly BindableProperty CustomPinsProperty = BindableProperty.Create<CustomMap, ObservableCollection<CustomPin>> (x => x.CustomPins, new ObservableCollection<CustomPin> (){ new CustomPin (){ Label = "test123" } });

		public ObservableCollection<CustomPin> CustomPins {
			get{ return (ObservableCollection<CustomPin>)base.GetValue (CustomPinsProperty); }
			set{ base.SetValue (CustomPinsProperty, value); }
		}


		public Grid GetMapGrid ()
		{
			return _mapGrid;
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
			};

			Device.OnPlatform (iOS: () => placeNameLabel.Font = Font.SystemFontOfSize (20),
				Android: () => placeNameLabel.Font = Font.SystemFontOfSize (20),
				WinPhone: () => placeNameLabel.Font = Font.SystemFontOfSize (24));

			placeNameLabel.BindingContext = this;
			placeNameLabel.SetBinding<CustomMap> (Label.TextProperty, vm => vm.SelectedPin.Label);

			var detailsLabel = new Label {
				Text = "Address Shows Here",
				TextColor = Color.Gray,
			};

			Device.OnPlatform (iOS: () => detailsLabel.Font = Font.SystemFontOfSize (14),
				Android: () => detailsLabel.Font = Font.SystemFontOfSize (14),
				WinPhone: () => detailsLabel.Font = Font.SystemFontOfSize (18));

			detailsLabel.BindingContext = this;
			detailsLabel.SetBinding<CustomMap> (Label.TextProperty, vm => vm.SelectedPin.Address);

			var pinInfoStackLayout = new StackLayout { Padding = new Thickness (22, 8, 0, 0)	};

			pinInfoStackLayout.Children.Add (placeNameLabel);
			pinInfoStackLayout.Children.Add (detailsLabel);
			pinInfoStackLayout.Spacing = 0;

			//todo : replace with ImageButton when Labs is fixed
			var navButton = new ImageButton () {
				Image = "navigate_icon",
				Text = "Route",
				TextColor = Colors.DarkBlue,
				Font = Font.SystemFontOfSize (14),
				Orientation = ImageOrientation.ImageOnTop,
				ImageHeightRequest = 75,
				ImageWidthRequest = 75,
				BackgroundColor = Colors.TransparentWhite,
				VerticalOptions = LayoutOptions.Center,
				HorizontalOptions = LayoutOptions.Center
			};

			NavigationButton = navButton;

			footerGrid.Children.Add (pinInfoStackLayout, 0, 0);
			footerGrid.Children.Add (new ContentView (){ Content = navButton }, 1, 0);

			return new ContentView{ Content = footerGrid, BackgroundColor = Colors.TransparentWhite };
		}
	}
}

