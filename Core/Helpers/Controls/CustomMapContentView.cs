using System;
using Xamarin.Forms;
using Core.Helpers.Controls;
using System.Collections.Generic;

namespace Core.Helpers.Controls
{
	public class CustomMapContentView : ContentView
	{
		public CustomMapContentView (CustomMap customMap)
		{
			_customMap = customMap;

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

			_mapGrid.Children.Add (_customMap, 0, 0);
			_mapGrid.Children.Add (CreateFooter (), 0, 1);
		
			Grid.SetRowSpan (_customMap, 2);
			_mapGrid.RowSpacing = 0;

			//Bind the footer to the ShowFooter property
			_mapGrid.BindingContext = this;
			_mapGrid.Children [1].SetBinding<CustomMapContentView> (IsVisibleProperty, x => x.ShowFooter);

			_mapGrid.Children [1].GestureRecognizers.Add (new TapGestureRecognizer ((view, obj) => {
				if (_footerHeight == COLLAPSED_FOOTER_HEIGHT) {
					ExpandFooter ();
				} else {
					ExpandMap ();
				}
			}));


			Content = _mapGrid;
		}

		static double COLLAPSED_FOOTER_HEIGHT = 0.16;
		static double COLLAPSED_MAP_HEIGHT = 0.37;
		static double EXPANDED_MAP_HEIGHT = 0.87;
		static double EXPANDED_FOOTER_HEIGHT = 0.63;

		double _footerHeight { get { return _mapGrid.RowDefinitions [1].Height.Value; } set { _mapGrid.RowDefinitions [1].Height = new GridLength (value, GridUnitType.Star); } }

		double _mapHeight { get { return _mapGrid.RowDefinitions [0].Height.Value; } set { _mapGrid.RowDefinitions [0].Height = new GridLength (value, GridUnitType.Star); } }

		Grid _footerMasterGrid;

		Grid _mapGrid;

		CustomMap _customMap;

		public Button NavigationButton	{ get; set; }

		public static readonly BindableProperty ShowFooterProperty = BindableProperty.Create<CustomMapContentView, bool> (x => x.ShowFooter, false);

		public bool ShowFooter {
			get{ return (bool)base.GetValue (ShowFooterProperty); }
			set {
				base.SetValue (ShowFooterProperty, value);

				if (value == false) {
					ExpandMap ();
				}
			}
		}

		void ExpandFooter ()
		{
			Grid.SetRowSpan (_customMap, 1);
			_mapHeight = COLLAPSED_MAP_HEIGHT;
			_footerHeight = EXPANDED_FOOTER_HEIGHT;
			ShowFooterDetails ();
		}

		void ExpandMap ()
		{
			Grid.SetRowSpan (_customMap, 2);
			_mapHeight = EXPANDED_MAP_HEIGHT;
			_footerHeight = COLLAPSED_FOOTER_HEIGHT;
			HideFooterDetails ();
		}

		ContentView CreateFooter ()
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
				}, RowSpacing = 10
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
				}, BackgroundColor = Color.White, Padding = new Thickness (18, 8, 0, 0)
			};


			var placeNameLabel = new Label {
				Text = "Pin Label Shows Here",
				TextColor = Color.Black,
			};

			Device.OnPlatform (iOS: () => placeNameLabel.Font = Font.SystemFontOfSize (20),
				Android: () => placeNameLabel.Font = Font.SystemFontOfSize (20),
				WinPhone: () => placeNameLabel.Font = Font.SystemFontOfSize (24));

			placeNameLabel.BindingContext = _customMap;
			placeNameLabel.SetBinding<CustomMap> (Label.TextProperty, vm => vm.SelectedPin.Label);

			var addressLabel = new Label {
				Text = "Address Shows Here",
				TextColor = Color.Gray,
			};

			Device.OnPlatform (iOS: () => addressLabel.Font = Font.SystemFontOfSize (14),
				Android: () => addressLabel.Font = Font.SystemFontOfSize (14),
				WinPhone: () => addressLabel.Font = Font.SystemFontOfSize (18));

			addressLabel.BindingContext = _customMap;
			addressLabel.SetBinding<CustomMap> (Label.TextProperty, vm => vm.SelectedPin.Address);


			var pinInfoStackLayout = new StackLayout { };

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
				ImageHeightRequest = 85,
				ImageWidthRequest = 85,
				WidthRequest = 85,
				HeightRequest = 85,
				BackgroundColor = Color.White,
				VerticalOptions = LayoutOptions.Start,
				HorizontalOptions = LayoutOptions.Center
			};

			NavigationButton = navButton;

			footerGrid.Children.Add (pinInfoStackLayout, 0, 0);
			footerGrid.Children.Add (new ContentView (){ Content = navButton }, 1, 0);

			_footerMasterGrid.Children.Add (footerGrid, 0, 0);

			return new ContentView{ Content = _footerMasterGrid, BackgroundColor = Color.White, Opacity = 0.9 };
		}

		ScrollView CreateFooterDetails ()
		{
			var footerDetailsGrid = new Grid {
				RowDefinitions = new RowDefinitionCollection {
					new RowDefinition {
						Height = new GridLength (0.3, GridUnitType.Star)
					},
					new RowDefinition {
						Height = new GridLength (0.5, GridUnitType.Star)
					},
					new RowDefinition {
						Height = new GridLength (0.5, GridUnitType.Star)
					},
					new RowDefinition {
						Height = new GridLength (0.5, GridUnitType.Star)
					}

				},
				ColumnDefinitions = new ColumnDefinitionCollection {
					new ColumnDefinition {
						Width = new GridLength (1, GridUnitType.Star)
					},
				}, RowSpacing = 10, Padding = new Thickness (0, 0, 0, 0)
			};

			footerDetailsGrid.Children.Add (CreateActionButtonsGrid (), 0, 0);
			footerDetailsGrid.Children.Add (CreateScheduleGrid (), 0, 1);
			footerDetailsGrid.Children.Add (CreateOtherView (), 0, 2);

			return new ScrollView{ Content = footerDetailsGrid };
		}

		Grid CreateActionButtonsGrid ()
		{
			var callButton = new ImageButton () {
				Image = "call_icon",
				Text = "Call",
				TextColor = Colors.DarkBlue,
				Font = Font.SystemFontOfSize (14),
				Orientation = ImageOrientation.ImageOnTop,
				ImageHeightRequest = 75,
				ImageWidthRequest = 75,
				HeightRequest = 80,
				WidthRequest = 100,
				BackgroundColor = Color.White,
				VerticalOptions = LayoutOptions.Center,
				HorizontalOptions = LayoutOptions.Center,
			};

			callButton.Clicked += (sender, e) => {
				var phoneNumber = _customMap.SelectedPin.PhoneNumber;
				DependencyService.Get<IPhoneService> ().DialNumber (phoneNumber);
			};

			var shareButton = new ImageButton () {
				Image = "share_icon",
				Text = "Share",
				TextColor = Colors.DarkBlue,
				Font = Font.SystemFontOfSize (14),
				Orientation = ImageOrientation.ImageOnTop,
				ImageHeightRequest = 75,
				ImageWidthRequest = 75,
				HeightRequest = 80,
				WidthRequest = 100,
				BackgroundColor = Color.White,
				VerticalOptions = LayoutOptions.Center,
				HorizontalOptions = LayoutOptions.Center
			};

			var actionButtonsGrid = new Grid {
				RowDefinitions = new RowDefinitionCollection {
					new RowDefinition {
						Height = new GridLength (1, GridUnitType.Star)
					}
				},
				ColumnDefinitions = new ColumnDefinitionCollection {
					new ColumnDefinition {
						Width = new GridLength (0.5, GridUnitType.Star)
					},
					new ColumnDefinition {
						Width = new GridLength (0.5, GridUnitType.Star)
					},
				}, BackgroundColor = Color.White
			};

			actionButtonsGrid.Children.Add (callButton, 0, 0);
			actionButtonsGrid.Children.Add (shareButton, 1, 0);

			return actionButtonsGrid;
		}

		Grid CreateScheduleGrid ()
		{
			var scheduleGrid = new Grid {
				RowDefinitions = new RowDefinitionCollection {
					new RowDefinition {
						Height = new GridLength (1, GridUnitType.Star)
					}
				},
				ColumnDefinitions = new ColumnDefinitionCollection {
					new ColumnDefinition {
						Width = new GridLength (1, GridUnitType.Star)
					},
						

				}, BackgroundColor = Color.White, Padding = new Thickness (18, 0, 18, 0)
			};

			var listview = new ListView { };
		
			//Don't allow selection
			listview.ItemSelected += (object sender, SelectedItemChangedEventArgs e) => {
				listview.SelectedItem = null;
			};

			var itemTemplate = new DataTemplate (typeof(HorizontalCell));

			itemTemplate.SetBinding (HorizontalCell.TextProperty, "Day");
			itemTemplate.SetValue (HorizontalCell.TextColorProperty, Color.Black);
			itemTemplate.SetBinding (HorizontalCell.DetailProperty, "HoursOfOperation");
			itemTemplate.SetValue (HorizontalCell.DetailColorProperty, Color.Gray);

			listview.ItemTemplate = itemTemplate;
			listview.BindingContext = _customMap;
			listview.SetBinding<CustomMap> (ListView.ItemsSourceProperty, vm => vm.SelectedPin.ScheduleEntries);


			scheduleGrid.Children.Add (listview, 0, 0);

			return scheduleGrid;
		}

		View CreateOtherView ()
		{
			var contentView = new ContentView { Padding = new Thickness (18, 0, 18, 0), BackgroundColor = Color.White };

		
			var listview = new ListView { };

			//Don't allow selection
			listview.ItemSelected += (object sender, SelectedItemChangedEventArgs e) => {
				var url = e.SelectedItem as Url;
				
				if (url != null) {
					DependencyService.Get<IPhoneService> ().OpenBrowser (url.Value);
				}

				listview.SelectedItem = null;
			};

			var itemTemplate = new DataTemplate (typeof(HorizontalCell));

			itemTemplate.SetBinding (HorizontalCell.TextProperty, "Key");
			itemTemplate.SetValue (HorizontalCell.TextColorProperty, Color.Black);
			itemTemplate.SetBinding (HorizontalCell.DetailProperty, "Value");
			itemTemplate.SetValue (HorizontalCell.DetailColorProperty, Color.Gray);

			listview.ItemTemplate = itemTemplate;
			listview.BindingContext = _customMap;
			listview.SetBinding<CustomMap> (ListView.ItemsSourceProperty, vm => vm.SelectedPin.Others);


			contentView.Content = listview;

			return contentView;
		}

		void ShowFooterDetails ()
		{
			_footerMasterGrid.RowDefinitions.Add (
				new RowDefinition {
					Height = new GridLength (0.8, GridUnitType.Star)
				}
			);
			_footerMasterGrid.RowDefinitions [0].Height = new GridLength (0.2, GridUnitType.Star);

			var footerDetails = CreateFooterDetails ();

			_footerMasterGrid.Children.Add (footerDetails, 0, 1);
		}

		void HideFooterDetails ()
		{
			if (_footerMasterGrid.RowDefinitions.Count == 2) {
				_footerMasterGrid.Children.RemoveAt (1);
				_footerMasterGrid.RowDefinitions.RemoveAt (1);
				_footerMasterGrid.RowDefinitions [0].Height = new GridLength (1, GridUnitType.Star);
			}
		}

	}
}