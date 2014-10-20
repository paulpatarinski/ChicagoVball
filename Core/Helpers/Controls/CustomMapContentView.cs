using System;
using System.Threading.Tasks;
using Core.Models;
using Xamarin.Forms;

namespace Core.Helpers.Controls
{
  public class CustomMapContentView : ContentView
  {
    public CustomMapContentView(CustomMap customMap)
    {
      _customMap = customMap;

      _mapGrid = new Grid
      {
        RowDefinitions = new RowDefinitionCollection
        {
          new RowDefinition
          {
            Height = new GridLength(EXPANDED_MAP_HEIGHT, GridUnitType.Star)
          },
          new RowDefinition
          {
            Height = new GridLength(COLLAPSED_FOOTER_HEIGHT, GridUnitType.Star)
          },
        },
        ColumnDefinitions = new ColumnDefinitionCollection
        {
          new ColumnDefinition
          {
            Width = new GridLength(1, GridUnitType.Star)
          }
        }
      };

      _mapGrid.Children.Add(_customMap, 0, 0);
      _mapGrid.Children.Add(CreateFooter(), 0, 1);

      Grid.SetRowSpan(_customMap, 2);

      _mapGrid.RowSpacing = 0;

      //Bind the footer to the ShowFooter property
      _mapGrid.BindingContext = this;

      
      _mapGrid.Children[1].GestureRecognizers.Add(new TapGestureRecognizer((view, obj) => ToogleFooter()));

      Content = _mapGrid;
    }

    private const uint COLLAPSE_ANIMATION_SPEED = 400;
    private const uint EXPAND_ANIMATION_SPEED = 400;
    private static double COLLAPSED_FOOTER_HEIGHT = 0.14;
    private static double COLLAPSED_MAP_HEIGHT = 0.37;
    private static double EXPANDED_MAP_HEIGHT = 0.86;
    private static double EXPANDED_FOOTER_HEIGHT = 0.63;
    private double _footerCollapsedHeight;
    private double _footerCollapsedY;
    private double _footerExpandedHeight;
    private double _footerExpandedY;
    private double _pageHeight;
    private Grid _footerMasterGrid;
    private Grid _mapGrid;
    private CustomMap _customMap;
    private FooterMode _footerMode;

    public FooterMode FooterMode
    {
      get { return _footerMode; }
      set
      {
        _footerMode = value;

        switch (value)
        {
          case FooterMode.Expanded:
            ExpandFooter();
            break;
          case FooterMode.Minimized:
            MinimizeFooter();
            break;
          default:
            HideFooter();
            break;
        }
      }
    }

    protected override void OnSizeAllocated(double width, double height)
    {
      //If the pageSize values have not been set yet, set them
      if (Math.Abs(_pageHeight) < 0.001)
      {
        _pageHeight = Bounds.Height;

        _footerCollapsedHeight = _pageHeight * COLLAPSED_FOOTER_HEIGHT;
        _footerCollapsedY = _pageHeight * EXPANDED_MAP_HEIGHT;

        _footerExpandedHeight = _pageHeight * EXPANDED_FOOTER_HEIGHT;
        _footerExpandedY = _pageHeight - _footerExpandedHeight;

        FooterMode = FooterMode.Hidden;
      }

      base.OnSizeAllocated(width, height);
    }

    private void ToogleFooter()
    {
      FooterMode = FooterMode == FooterMode.Expanded ? FooterMode.Minimized : FooterMode.Expanded;
    }

    void HideFooter()
    {
      var footerOldBounds = _mapGrid.Children[1].Bounds;
      var footerNewBounds = new Rectangle(footerOldBounds.X, _pageHeight, footerOldBounds.Width, 0);

      _mapGrid.Children[1].LayoutTo(footerNewBounds, EXPAND_ANIMATION_SPEED, Easing.SinIn);

      Task.Delay(TimeSpan.FromMilliseconds(COLLAPSE_ANIMATION_SPEED)).ContinueWith((result) => Device.BeginInvokeOnMainThread(HideFooterDetails));
    }

		void ExpandFooter ()
		{
		  var footerOldBounds = _mapGrid.Children[1].Bounds;
      var footerNewBounds = new Rectangle(footerOldBounds.X, _footerExpandedY, footerOldBounds.Width, _footerExpandedHeight);

      Task.Delay(TimeSpan.FromMilliseconds(110)).ContinueWith((result) => Device.BeginInvokeOnMainThread(ShowFooterDetails));

      _mapGrid.Children[1].LayoutTo(footerNewBounds, EXPAND_ANIMATION_SPEED, Easing.SinIn);

		  _customMap.CameraFocusYOffset = 500;
		  _customMap.CenterOnPosition = _customMap.SelectedPin.Position;
		}

		void MinimizeFooter ()
		{
      var footerOldBounds = _mapGrid.Children[1].Bounds;
      var footerNewBounds = new Rectangle(footerOldBounds.X, _footerCollapsedY, footerOldBounds.Width, _footerCollapsedHeight);

      _mapGrid.Children[1].LayoutTo(footerNewBounds, COLLAPSE_ANIMATION_SPEED, Easing.SinIn);

      _customMap.CameraFocusYOffset = 100;
      _customMap.CenterOnPosition = _customMap.SelectedPin.Position;

      Task.Delay(TimeSpan.FromMilliseconds(COLLAPSE_ANIMATION_SPEED)).ContinueWith((result) => Device.BeginInvokeOnMainThread(HideFooterDetails));
		}

		void ShowFooterDetails ()
		{
			var headerRowHeight = 0.21;

			_footerMasterGrid.RowDefinitions.Add (
				new RowDefinition {
					Height = new GridLength (1 - headerRowHeight, GridUnitType.Star)
				}
			);
			_footerMasterGrid.RowDefinitions [0].Height = new GridLength (headerRowHeight, GridUnitType.Star);

			var footerDetails = CreateFooterDetails (this.Height);

			_footerMasterGrid.Children.Add (footerDetails, 1, 1);
		}

		void HideFooterDetails ()
		{
			if (_footerMasterGrid.RowDefinitions.Count == 2) {
        _footerMasterGrid.Children.RemoveAt(1);
				_footerMasterGrid.RowDefinitions.RemoveAt (1);
				_footerMasterGrid.RowDefinitions [0].Height = new GridLength (1, GridUnitType.Star);
			}
		}

    #region UI Creation

    ContentView CreateFooter()
    {
      _footerMasterGrid = new Grid
      {
        RowDefinitions = new RowDefinitionCollection {
					new RowDefinition {
						Height = new GridLength (1, GridUnitType.Star)
					},
				},
        ColumnDefinitions = new ColumnDefinitionCollection {
					new ColumnDefinition {
						Width = new GridLength (0.05, GridUnitType.Star)
					}, new ColumnDefinition {
						Width = new GridLength (0.95, GridUnitType.Star)
					},
				},
        RowSpacing = 10
      };

      var footerGrid = new Grid
      {
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
				},
        BackgroundColor = Color.White
      };


      var placeNameLabel = new Label
      {
        Text = "Pin Label Shows Here",
        TextColor = Color.Black,
      };

      Device.OnPlatform(iOS: () => placeNameLabel.Font = Font.SystemFontOfSize(20),
        Android: () => placeNameLabel.Font = Font.SystemFontOfSize(20),
        WinPhone: () => placeNameLabel.Font = Font.SystemFontOfSize(24));

      placeNameLabel.BindingContext = _customMap;
      placeNameLabel.SetBinding<CustomMap>(Label.TextProperty, vm => vm.SelectedPin.Label);

      var addressLabel = new Label
      {
        Text = "Address Shows Here",
        TextColor = Color.Gray,
      };

      Device.OnPlatform(iOS: () => addressLabel.Font = Font.SystemFontOfSize(14),
        Android: () => addressLabel.Font = Font.SystemFontOfSize(14),
        WinPhone: () => addressLabel.Font = Font.SystemFontOfSize(18));

      addressLabel.BindingContext = _customMap;
      addressLabel.SetBinding<CustomMap>(Label.TextProperty, vm => vm.SelectedPin.Address);


      var pinInfoStackLayout = new StackLayout { };

      pinInfoStackLayout.Children.Add(placeNameLabel);
      pinInfoStackLayout.Children.Add(addressLabel);
      pinInfoStackLayout.Spacing = 0;

      footerGrid.Children.Add(pinInfoStackLayout, 0, 0);
      footerGrid.Children.Add(CreateImageButton("navigate_icon.png", "Route", (view, o) =>
      {
        var selectedPin = _customMap.SelectedPin;
        DependencyService.Get<IPhoneService>().LaunchNavigationAsync(new NavigationModel
        {
          Latitude = selectedPin.Position.Latitude,
          Longitude = selectedPin.Position.Longitude,
          DestinationAddress = selectedPin.Address,
          DestinationName = selectedPin.Label
        });
      }), 1, 0);

      _footerMasterGrid.Children.Add(footerGrid, 1, 0);

      return new ContentView { Content = _footerMasterGrid, BackgroundColor = Color.White, Opacity = 0.9 };
    }

    ScrollView CreateFooterDetails(double footerDetailsHeight)
    {
      var footerDetailsGrid = new Grid
      {
        RowDefinitions = new RowDefinitionCollection {
					new RowDefinition {
						Height = new GridLength (COLLAPSED_FOOTER_HEIGHT, GridUnitType.Star)
					},
					new RowDefinition {
						Height = new GridLength (0.14, GridUnitType.Star)
					},
					new RowDefinition {
						Height = new GridLength (0.3, GridUnitType.Star)
					},
					new RowDefinition {
						Height = new GridLength (0.3, GridUnitType.Star)
					},
				},
        ColumnDefinitions = new ColumnDefinitionCollection {
					new ColumnDefinition {
						Width = new GridLength (0.95, GridUnitType.Star)
					},
					new ColumnDefinition {
						Width = new GridLength (0.05, GridUnitType.Star)
					},
				},
        RowSpacing = 0,
        Padding = new Thickness(0, 10, 0, 0)
      };

      footerDetailsGrid.Children.Add(CreateActionButtonsGrid(), 0, 0);
      footerDetailsGrid.Children.Add(CreateScheduleGrid(), 0, 1);
      footerDetailsGrid.Children.Add(CreateOtherView(), 0, 2);

      return new ScrollView
      {
        Content = new ContentView
        {
          Content = footerDetailsGrid,
          HeightRequest = footerDetailsHeight
        },
      };
    }

    Grid CreateActionButtonsGrid()
    {
      var actionButtonsGrid = new Grid
      {
        RowDefinitions = new RowDefinitionCollection {
					new RowDefinition {
						Height = new GridLength (1, GridUnitType.Star)
					}
				},
        ColumnDefinitions = new ColumnDefinitionCollection {
					new ColumnDefinition {
						Width = new GridLength (0.2, GridUnitType.Star)
					},
					new ColumnDefinition {
						Width = new GridLength (0.25, GridUnitType.Star)
					},
					new ColumnDefinition {
						Width = new GridLength (0.1, GridUnitType.Star)
					},
					new ColumnDefinition {
						Width = new GridLength (0.25, GridUnitType.Star)
					},
					new ColumnDefinition {
						Width = new GridLength (0.2, GridUnitType.Star)
					},
				},
        BackgroundColor = Color.White
      };

      var callImageButton = CreateImageButton("call_icon.png", "Call", (view, o) =>
      {
        var phoneNumber = _customMap.SelectedPin.PhoneNumber;
        DependencyService.Get<IPhoneService>().DialNumber(phoneNumber);
      });

      var shareImageButton = CreateImageButton("share_icon.png", "Share", (view, o) =>
      {
        var selectedPin = _customMap.SelectedPin;
        var text = string.Format("I am playing vball at {0}, {1}.", selectedPin.Label, selectedPin.Address);
        DependencyService.Get<IPhoneService>().ShareText(text);
      });

      actionButtonsGrid.Children.Add(callImageButton, 1, 0);
      actionButtonsGrid.Children.Add(shareImageButton, 3, 0);

      return actionButtonsGrid;
    }

    Grid CreateScheduleGrid()
    {
      var scheduleGrid = new Grid
      {
        RowDefinitions = new RowDefinitionCollection {
					new RowDefinition {
						Height = new GridLength (1, GridUnitType.Star)
					}
				},
        ColumnDefinitions = new ColumnDefinitionCollection {
					new ColumnDefinition {
						Width = new GridLength (1, GridUnitType.Star)
					},
						

				},
        BackgroundColor = Color.White
      };

      var listview = new ListView { };

      //Don't allow selection
      listview.ItemSelected += (object sender, SelectedItemChangedEventArgs e) =>
      {
        listview.SelectedItem = null;
      };

      var itemTemplate = new DataTemplate(typeof(HorizontalCell));

      itemTemplate.SetBinding(HorizontalCell.TextProperty, "Day");
      itemTemplate.SetValue(HorizontalCell.TextColorProperty, Color.Black);
      itemTemplate.SetBinding(HorizontalCell.DetailProperty, "HoursOfOperation");
      itemTemplate.SetValue(HorizontalCell.DetailColorProperty, Color.Gray);

      listview.ItemTemplate = itemTemplate;
      listview.BindingContext = _customMap;
      listview.SetBinding<CustomMap>(ListView.ItemsSourceProperty, vm => vm.SelectedPin.ScheduleEntries);


      scheduleGrid.Children.Add(listview, 0, 0);

      return scheduleGrid;
    }

    View CreateOtherView()
    {
      var contentView = new ContentView { BackgroundColor = Color.White };


      var listview = new ListView { };

      //Don't allow selection
      listview.ItemSelected += (object sender, SelectedItemChangedEventArgs e) =>
      {
        var url = e.SelectedItem as Url;

        if (url != null && url.Value.Contains("www"))
        {
          DependencyService.Get<IPhoneService>().OpenBrowser(url.Value);
        }

        listview.SelectedItem = null;
      };

      var itemTemplate = new DataTemplate(typeof(HorizontalCell));

      itemTemplate.SetBinding(HorizontalCell.TextProperty, "Key");
      itemTemplate.SetValue(HorizontalCell.TextColorProperty, Color.Black);
      itemTemplate.SetBinding(HorizontalCell.DetailProperty, "Value");
      itemTemplate.SetValue(HorizontalCell.DetailColorProperty, Color.Gray);

      listview.ItemTemplate = itemTemplate;
      listview.BindingContext = _customMap;
      listview.SetBinding<CustomMap>(ListView.ItemsSourceProperty, vm => vm.SelectedPin.Others);


      contentView.Content = listview;

      return contentView;
    }

    ContentView CreateImageButton(string buttonImage, string buttonText, Action<View, Object> tappedCallback)
    {
      var grid = new Grid
      {
        RowDefinitions = new RowDefinitionCollection {
					new RowDefinition {
						Height = new GridLength (0.12, GridUnitType.Star)
					},
					new RowDefinition {
						Height = new GridLength (0.38, GridUnitType.Star)
					},
					new RowDefinition {
						Height = new GridLength (0.4, GridUnitType.Star)
					},
					new RowDefinition {
						Height = new GridLength (0.1, GridUnitType.Star)
					},
				},
        ColumnDefinitions = new ColumnDefinitionCollection {
					new ColumnDefinition {
						Width = new GridLength (1, GridUnitType.Star)
					},

				},
        BackgroundColor = Color.White,
        HorizontalOptions = LayoutOptions.Center,
        RowSpacing = 0
      };

      var navImageGrid = new Grid
      {
        RowDefinitions = new RowDefinitionCollection {
					new RowDefinition {
						Height = new GridLength (1, GridUnitType.Star)
					}
				},
        ColumnDefinitions = new ColumnDefinitionCollection {
					new ColumnDefinition {
						Width = new GridLength (0.28, GridUnitType.Star)
					},
					new ColumnDefinition {
						Width = new GridLength (0.44, GridUnitType.Star)
					},
					new ColumnDefinition {
						Width = new GridLength (0.28, GridUnitType.Star)
					},

				}
      };

      var navImage = new Image()
      {
        Source = ImageSource.FromFile(buttonImage),
        Aspect = Aspect.Fill,
        HorizontalOptions = LayoutOptions.Center
      };

      grid.GestureRecognizers.Add(new TapGestureRecognizer(tappedCallback));

      navImageGrid.Children.Add(navImage, 1, 0);

      var label = new Label
      {
        Text = buttonText,
        Font = Font.SystemFontOfSize(16),
        TextColor = Colors.DarkBlue,
        HorizontalOptions = LayoutOptions.Center
      };

      grid.Children.Add(navImageGrid, 0, 1);
      grid.Children.Add(label, 0, 2);

      return new ContentView { Content = grid };
    }

    #endregion UI Creation

	}

  public enum FooterMode
  {
    Expanded,
    Minimized,
    Hidden
  }
}