using Xamarin.Forms;

namespace Core.Helpers.Controls
{
	/// <summary>
	/// Creates a button with text and an image.
	/// The image can be on the left, above, on the right or below the text.
	/// </summary>
	public class ImageButton : Button
	{
		/// <summary>
		/// The name of the image without path or file type information.
		/// Android: There should be a drable resource with the same name
		/// iOS: There should be an image in the Resources folder with a build action of BundleResource.
		/// Windows Phone: There should be an image in the Images folder with a type of .png and build action set to resource.
		/// </summary>
		public static readonly BindableProperty ImageProperty =
			BindableProperty.Create<ImageButton, string> (
				p => p.Image, default(string));

		public string Image {
			get { return (string)GetValue (ImageProperty); }
			set { SetValue (ImageProperty, value); }
		}

		/// <summary>
		/// The orientation of the image relative to the text.
		/// </summary>
		public static readonly BindableProperty OrientationProperty =
			BindableProperty.Create<ImageButton, ImageOrientation> (
				p => p.Orientation, ImageOrientation.ImageToLeft);

		public ImageOrientation Orientation {
			get { return (ImageOrientation)GetValue (OrientationProperty); }
			set { SetValue (OrientationProperty, value); }
		}

		/// <summary>
		/// The requested height of the image.  If less than or equal to zero than a 
		/// height of 50 will be used.
		/// </summary>
		public static readonly BindableProperty ImageHeightRequestProperty =
			BindableProperty.Create<ImageButton, int> (
				p => p.ImageHeightRequest, default(int));

		public int ImageHeightRequest {
			get { return (int)GetValue (ImageHeightRequestProperty); }
			set { SetValue (ImageHeightRequestProperty, value); }
		}

		/// <summary>
		/// The requested width of the image.  If less than or equal to zero than a 
		/// width of 50 will be used.
		/// </summary>
		public static readonly BindableProperty ImageWidthRequestProperty =
			BindableProperty.Create<ImageButton, int> (
				p => p.ImageWidthRequest, default(int));

		public int ImageWidthRequest {
			get { return (int)GetValue (ImageWidthRequestProperty); }
			set { SetValue (ImageWidthRequestProperty, value); }
		}
	}
}