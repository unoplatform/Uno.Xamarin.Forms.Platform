using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
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
	public partial class ImageRenderer : ViewRenderer<Image, NativeImage>
	{
		bool _measured;
		bool _disposed;

		public override SizeRequest GetDesiredSize(double widthConstraint, double heightConstraint)
		{
			if (ImageControl.Source == null)
				return new SizeRequest();

			_measured = true;

			var result = new Size { Width = ((BitmapSource)ImageControl.Source).PixelWidth, Height = ((BitmapSource)ImageControl.Source).PixelHeight };

			return new SizeRequest(result);
		}

		private Windows.UI.Xaml.Controls.Image ImageControl =>
#if __IOS__ || __ANDROID__
			(Windows.UI.Xaml.Controls.Image)Control.Child; 
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

				await TryUpdateSource();
				UpdateAspect();
			}
		}

		protected override async void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (e.PropertyName == Image.SourceProperty.PropertyName)
				await TryUpdateSource();
			else if (e.PropertyName == Image.AspectProperty.PropertyName)
				UpdateAspect();
		}

		static Stretch GetStretch(Aspect aspect)
		{
			switch (aspect)
			{
				case Aspect.Fill:
					return Stretch.Fill;
				case Aspect.AspectFill:
					return Stretch.UniformToFill;
				default:
				case Aspect.AspectFit:
					return Stretch.Uniform;
			}
		}

#if HAS_UNO
		void OnImageOpened(object sender, EventArgs routedEventArgs)
#else
		void OnImageOpened(object sender, RoutedEventArgs routedEventArgs)
#endif
		{
			if (_measured)
			{
				RefreshImage();
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
			Log.Warning("Image Loading", $"Image failed to load: {exceptionRoutedEventArgs.ErrorMessage}" );
			Element?.SetIsLoading(false);
		}
#endif

		void RefreshImage()
		{
			((IVisualElementController)Element)?.InvalidateMeasure(InvalidationTrigger.RendererReady);
		}

		void UpdateAspect()
		{
			if (_disposed || Element == null || Control == null)
			{
				return;
			}

			ImageControl.Stretch = GetStretch(Element.Aspect);
			if (Element.Aspect == Aspect.AspectFill || Element.Aspect == Aspect.AspectFit) // Then Center Crop
			{
				ImageControl.HorizontalAlignment = HorizontalAlignment.Center;
				ImageControl.VerticalAlignment = VerticalAlignment.Center;
			}
			else // Default
			{
				ImageControl.HorizontalAlignment = HorizontalAlignment.Left;
				ImageControl.VerticalAlignment = VerticalAlignment.Top;
			}
		}

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
			if (_disposed || Element == null || Control == null)
			{
				return;
			}

			Element.SetIsLoading(true);

			ImageSource source = Element.Source;
			IImageSourceHandler handler;
			if (source != null && (handler = Internals.Registrar.Registered.GetHandlerForObject<IImageSourceHandler>(source)) != null)
			{
				Windows.UI.Xaml.Media.ImageSource imagesource;

				try
				{
					imagesource = await handler.LoadImageAsync(source);
				}
				catch (OperationCanceledException)
				{
					imagesource = null;
				}

				// In the time it takes to await the imagesource, some zippy little app
				// might have disposed of this Image already.
				if (Control != null)
				{
					ImageControl.Source = imagesource;
				}

				RefreshImage();
			}
			else
			{
				ImageControl.Source = null;
				Element.SetIsLoading(false);
			}
		}
	}
}
