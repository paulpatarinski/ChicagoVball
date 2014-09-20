using System;
using Xamarin.Forms;
using System.Windows.Input;

namespace Core
{
	public class HorizontalCell : ViewCell
	{
		public HorizontalCell ()
		{
			Grid grid = new Grid {
				Padding = new Thickness (5, 0, 5, 0),
				ColumnDefinitions = {
					new ColumnDefinition { Width = new GridLength (0.5, GridUnitType.Star) },
					new ColumnDefinition { Width = new GridLength (0.5, GridUnitType.Star) },
				},
				RowDefinitions = {
					new RowDefinition { Height = new GridLength (1, GridUnitType.Star) }
				}
			};

			var leftLabel = new Label {
				YAlign = TextAlignment.Center,
				XAlign = TextAlignment.Start,
			};

			var rightLabel = new Label {
				YAlign = TextAlignment.Center,
				XAlign = TextAlignment.End,
			};

			leftLabel.SetBinding<HorizontalCell> (Label.TextProperty, vm => vm.Text);
			leftLabel.SetBinding<HorizontalCell> (Label.TextColorProperty, vm => vm.TextColor);

			rightLabel.SetBinding<HorizontalCell> (Label.TextProperty, vm => vm.Detail);
			rightLabel.SetBinding<HorizontalCell> (Label.TextColorProperty, vm => vm.DetailColor);

			grid.Children.Add (leftLabel, 0, 0);   
			grid.Children.Add (rightLabel, 1, 0);

			grid.BindingContext = this;
			View = grid;
		}

		public static readonly BindableProperty DetailProperty =
			BindableProperty.Create<HorizontalCell, string> (
				p => p.Detail, string.Empty);

		public static readonly BindableProperty DetailColorProperty =
			BindableProperty.Create<HorizontalCell, Color> (
				p => p.DetailColor, Color.Default);

		public static readonly BindableProperty TextProperty =
			BindableProperty.Create<HorizontalCell, string> (
				p => p.Text, string.Empty);

		public static readonly BindableProperty TextColorProperty =
			BindableProperty.Create<HorizontalCell, Color> (
				p => p.TextColor, Color.Default);

		public string Detail {
			get {
				return (string)base.GetValue (DetailProperty);
			}
			set {
				base.SetValue (DetailProperty, value);
			}
		}

		public Color DetailColor {
			get {
				return (Color)base.GetValue (DetailColorProperty);
			}
			set {
				base.SetValue (DetailColorProperty, value);
			}
		}

		public string Text {
			get {
				return (string)base.GetValue (TextProperty);
			}
			set {
				base.SetValue (TextProperty, value);
			}
		}

		public Color TextColor {
			get {
				return (Color)base.GetValue (TextColorProperty);
			}
			set {
				base.SetValue (TextColorProperty, value);
			}
		}
	}
}