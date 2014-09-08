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
				if (FooterHeight == COLLAPSED_FOOTER_HEIGHT) {
					ExpandFooter ();
				} else {
					ExpandMap ();
				}
			}));
		}

		static double COLLAPSED_FOOTER_HEIGHT = 0.13;
		static double COLLAPSED_MAP_HEIGHT = 0.3;
		static double EXPANDED_MAP_HEIGHT = 0.87;
		static double EXPANDED_FOOTER_HEIGHT = 0.7;

		public Button NavigationButton	{ get; set; }

		Grid _footerMasterGrid;

		Grid _mapGrid;

		public static readonly BindableProperty SelectedPinProperty = BindableProperty.Create<CustomMap, CustomPin> (x => x.SelectedPin, new CustomPin{ Label = "test123" });

		public CustomPin SelectedPin {
			get{ return (CustomPin)base.GetValue (SelectedPinProperty); }
			set{ base.SetValue (SelectedPinProperty, value); }
		}

		public static readonly BindableProperty ShowFooterProperty = BindableProperty.Create<CustomMap, bool> (x => x.ShowFooter, false);

		public bool ShowFooter {
			get{ return (bool)base.GetValue (ShowFooterProperty); }
			set {
				base.SetValue (ShowFooterProperty, value);

				if (value == false) {
					ExpandMap ();
				}
			}
		}

		public static readonly BindableProperty ShowAdditionalInfoProperty = BindableProperty.Create<CustomMap, bool> (x => x.ShowAdditionalInfo, false);

		public bool ShowAdditionalInfo {
			get{ return (bool)base.GetValue (ShowAdditionalInfoProperty); }
			set {
				base.SetValue (ShowAdditionalInfoProperty, value);

				if (value == false) {
					_footerMasterGrid.Children.RemoveAt (1);
					_footerMasterGrid.RowDefinitions.RemoveAt (1);
					_footerMasterGrid.RowDefinitions [0].Height = new GridLength (1, GridUnitType.Star);
				} else {

					_footerMasterGrid.RowDefinitions.Add (
						new RowDefinition {
							Height = new GridLength (0.8, GridUnitType.Star)
						}
					);
					_footerMasterGrid.RowDefinitions [0].Height = new GridLength (0.2, GridUnitType.Star);

					_footerMasterGrid.Children.Add (GetExtraInfoView (), 0, 1);
				}

			}
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

		void ExpandFooter ()
		{
			Grid.SetRowSpan (this, 1);
			MapHeight = COLLAPSED_MAP_HEIGHT;
			FooterHeight = EXPANDED_FOOTER_HEIGHT;
			ShowAdditionalInfo = true;
		}

		void ExpandMap ()
		{
			Grid.SetRowSpan (this, 2);
			MapHeight = EXPANDED_MAP_HEIGHT;
			FooterHeight = COLLAPSED_FOOTER_HEIGHT;
			ShowAdditionalInfo = false;
		}

		View CreateFooter ()
		{
			_footerMasterGrid = new Grid {
				RowDefinitions = new RowDefinitionCollection {
					new RowDefinition {
						Height = new GridLength (1, GridUnitType.Star)
					},
				},
				ColumnDefinitions = new ColumnDefinitionCollection {
					new ColumnDefinition {
						Width = new GridLength (1, GridUnitType.Star)
					},
				}
			};

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

			var addressLabel = new Label {
				Text = "Address Shows Here",
				TextColor = Color.Gray,
			};

			Device.OnPlatform (iOS: () => addressLabel.Font = Font.SystemFontOfSize (14),
				Android: () => addressLabel.Font = Font.SystemFontOfSize (14),
				WinPhone: () => addressLabel.Font = Font.SystemFontOfSize (18));

			addressLabel.BindingContext = this;
			addressLabel.SetBinding<CustomMap> (Label.TextProperty, vm => vm.SelectedPin.Address);

	
			var pinInfoStackLayout = new StackLayout { Padding = new Thickness (22, 8, 0, 0)	};

			pinInfoStackLayout.Children.Add (placeNameLabel);
			pinInfoStackLayout.Children.Add (addressLabel);
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

			_footerMasterGrid.Children.Add (footerGrid, 0, 0);

			return new ContentView{ Content = _footerMasterGrid, BackgroundColor = Colors.TransparentWhite };
		}

		ContentView GetExtraInfoView ()
		{
			var phoneLabel = new Label {
				Text = "773 733 2333",
				TextColor = Color.Gray,
			};

			Device.OnPlatform (iOS: () => phoneLabel.Font = Font.SystemFontOfSize (14),
				Android: () => phoneLabel.Font = Font.SystemFontOfSize (14),
				WinPhone: () => phoneLabel.Font = Font.SystemFontOfSize (18));

			phoneLabel.BindingContext = this;
//			phoneLabel.SetBinding<CustomMap> (Label.TextProperty, vm => vm.SelectedPin.PhoneNumber);

			return new ContentView{ Content = phoneLabel };
		}
	}
}

