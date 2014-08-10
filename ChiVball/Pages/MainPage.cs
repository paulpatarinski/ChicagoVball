using System;
using Xamarin.Forms;

namespace ChiVball
{
	public class MainPage : ContentPage
	{
		public MainPage ()
		{
			Content = new Label {
				Text = "Hello, Forms!",
				VerticalOptions = LayoutOptions.CenterAndExpand,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
			};
		}
	}
}