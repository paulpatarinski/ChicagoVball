using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Core.Pages;
using System.Linq;

namespace Core.Helpers.Controls
{
	public class CustomMap : Map
	{
		public CustomMap (MapSpan mapSpan) : base (mapSpan)
		{

		}

		public static readonly BindableProperty SelectedPinAddressProperty = BindableProperty.Create<CustomMap, string> (x => x.SelectedPinAddress, string.Empty);

		public string SelectedPinAddress {
			get{ return (string)base.GetValue (SelectedPinAddressProperty); }
			set {
				base.SetValue (SelectedPinProperty, CustomPins.FirstOrDefault (x => x.Address.Equals (value)) as CustomPin); 		
				base.SetValue (SelectedPinAddressProperty, value);
			}
		}

		public static readonly BindableProperty SelectedPinProperty = BindableProperty.Create<CustomMap, CustomPin> (x => x.SelectedPin, new CustomPin ());

		public CustomPin SelectedPin {
			get { return (CustomPin)base.GetValue (SelectedPinProperty); }
			set{ base.SetValue (SelectedPinAddressProperty, value); }
		}

		public static readonly BindableProperty CustomPinsProperty = BindableProperty.Create<CustomMap, ObservableCollection<CustomPin>> (x => x.CustomPins, new ObservableCollection<CustomPin> (){ new CustomPin (){ Label = "test123" } });

		public ObservableCollection<CustomPin> CustomPins {
			get{ return (ObservableCollection<CustomPin>)base.GetValue (CustomPinsProperty); }
			set{ base.SetValue (CustomPinsProperty, value); }
		}
	}
}