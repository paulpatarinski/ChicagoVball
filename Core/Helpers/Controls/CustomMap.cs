using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using System.Linq;
using System;

namespace Core.Helpers.Controls
{
	public class CustomMap : Map
	{
		public CustomMap (MapSpan mapSpan) : base (mapSpan)
		{

		}

		public Position CenterOnPosition {
			get { return _centerOnPosition; }
			set {
				_centerOnPosition = value;
				OnPropertyChanged ();
			}
		}

		string _selectedPinAddress;

		public string SelectedPinAddress {
			get{ return _selectedPinAddress; }
			set {
				_selectedPinAddress = value;

				var selectedPin = CustomPins.FirstOrDefault (x => x.Address.Equals (value)) as CustomPin;

				var parent = this.Parent.Parent as CustomMapContentView;

				if (parent == null)
					throw new Exception ("Not able to retrieve the parent of the CustomMap");

				//Set the Expanded to Expanded (otherwise for some reason the footer minimizes)
				if (parent.FooterMode == FooterMode.Expanded) {
					parent.FooterMode = FooterMode.Expanded;
				}

				CenterOnPosition = selectedPin.Position;

				base.SetValue (SelectedPinProperty, selectedPin);
			}
		}

		public static readonly BindableProperty SelectedPinProperty = BindableProperty.Create<CustomMap, CustomPin> (x => x.SelectedPin, new CustomPin ());

		public CustomPin SelectedPin {
			get { return (CustomPin)base.GetValue (SelectedPinProperty); }
		}

		public static readonly BindableProperty CustomPinsProperty = BindableProperty.Create<CustomMap, ObservableCollection<CustomPin>> (x => x.CustomPins, new ObservableCollection<CustomPin> (){ new CustomPin (){ Label = "test123" } });
		private Position _centerOnPosition;

		public ObservableCollection<CustomPin> CustomPins {
			get{ return (ObservableCollection<CustomPin>)base.GetValue (CustomPinsProperty); }
			set{ base.SetValue (CustomPinsProperty, value); }
		}

		public int CameraFocusYOffset { get; set; }
	}
}