using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Xamarin.Forms.Internals;

#if __IOS__ || __ANDROID__
using NativeImage = Windows.UI.Xaml.Controls.Border;
#else
using NativeImage = Windows.UI.Xaml.Controls.Image;
#endif

namespace Xamarin.Forms.Platform.UWP
{
	public partial class ImageRenderer : ViewRenderer<Image, NativeImage>, IImageVisualElementRenderer
	{
		bool _measured;
		bool _disposed;

		public ImageRenderer() : base()
		{
			ImageElementManager.Init(this);
		}

		bool IImageVisualElementRenderer.IsDisposed => _disposed;

		public override SizeRequest GetDesiredSize(double widthConstraint, double heightConstraint)
		{
			if (ImageControl.Source == null)
				return new SizeRequest();

			_measured = true;

            var size = ImageControl.Source.GetImageSourceSize();
            var result = new Size { Width = size.Width, Height = size.Height };

            return new SizeRequest(result);
		}

		private Windows.UI.Xaml.Controls.Image ImageControl =>
#if __IOS__ || __ANDROID__
			Control?.Child as Windows.UI.Xaml.Controls.Image; 
#else
			Control;
#endif

		protected override void Dispose(bool disposing)
		{
			if (_disposed)
			{
				return;
			}

			_disposed = true;

			if (disposing)
			{
				ImageElementManager.Dispose(this);
				if (Control != null)
				{
					ImageControl.ImageOpened -= OnImageOpened;
					ImageControl.ImageFailed -= OnImageFailed;
				}
			}

			base.Dispose(disposing);
		}

		protected override async void OnElementChanged(ElementChangedEventArgs<Image> e)
		{
			base.OnElementChanged(e);

			if (e.NewElement != null)
			{
				if (Control == null)
				{

					var image = new Windows.UI.Xaml.Controls.Image();
					image.ImageOpened += OnImageOpened;
					image.ImageFailed += OnImageFailed;
#if __IOS__ || __ANDROID__
					SetNativeControl(new Windows.UI.Xaml.Controls.Border { Child = image });
#else
					SetNativeControl(image);
#endif

				}

				await TryUpdateSource().ConfigureAwait(false);
			}
		}

		protected override async void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (e.PropertyName == Image.SourceProperty.PropertyName)
				await TryUpdateSource().ConfigureAwait(false);
		}


#if HAS_UNO
		void OnImageOpened(object sender, EventArgs routedEventArgs)
#else
		void OnImageOpened(object sender, RoutedEventArgs routedEventArgs)
#endif
		{
			if (_measured)
			{
				ImageElementManager.RefreshImage(Element);
			}

			Element?.SetIsLoading(false);
		}

#if HAS_UNO
		protected virtual void OnImageFailed(object sender, EventArgs exceptionRoutedEventArgs)
		{
			//Log.Warning("Image Loading", $"Image failed to load: {exceptionRoutedEventArgs.ErrorMessage}" );
			//Element?.SetIsLoading(false);
		}
#else
		protected virtual void OnImageFailed(object sender, ExceptionRoutedEventArgs exceptionRoutedEventArgs)
		{
			Log.Warning("Image Loading", $"Image failed to load: {exceptionRoutedEventArgs.ErrorMessage}");
			Element?.SetIsLoading(false);
		}
#endif


		protected virtual async Task TryUpdateSource()
		{
			// By default we'll just catch and log any exceptions thrown by UpdateSource so we don't bring down
			// the application; a custom renderer can override this method and handle exceptions from
			// UpdateSource differently if it wants to

			try
			{
				await UpdateSource().ConfigureAwait(false);
			}
			catch (Exception ex)
			{
				Log.Warning(nameof(ImageRenderer), "Error loading image: {0}", ex);
			}
			finally
			{
				((IImageController)Element)?.SetIsLoading(false);
			}
		}

		protected async Task UpdateSource()
		{
			await ImageElementManager.UpdateSource(this).ConfigureAwait(false);
		}

		void IImageVisualElementRenderer.SetImage(Windows.UI.Xaml.Media.ImageSource image)
		{
			ImageControl.Source = image;
		}

		Windows.UI.Xaml.Controls.Image IImageVisualElementRenderer.GetImage() => ImageControl;
	}
}
