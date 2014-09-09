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

		}

		public static readonly BindableProperty SelectedPinProperty = BindableProperty.Create<CustomMap, CustomPin> (x => x.SelectedPin, new CustomPin{ Label = "test123" });

		public CustomPin SelectedPin {
			get{ return (CustomPin)base.GetValue (SelectedPinProperty); }
			set{ base.SetValue (SelectedPinProperty, value); }
		}

		public static readonly BindableProperty CustomPinsProperty = BindableProperty.Create<CustomMap, ObservableCollection<CustomPin>> (x => x.CustomPins, new ObservableCollection<CustomPin> (){ new CustomPin (){ Label = "test123" } });

		public ObservableCollection<CustomPin> CustomPins {
			get{ return (ObservableCollection<CustomPin>)base.GetValue (CustomPinsProperty); }
			set{ base.SetValue (CustomPinsProperty, value); }
		}
	}
}