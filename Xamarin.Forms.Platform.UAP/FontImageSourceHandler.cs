﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

#if !HAS_UNO
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Text;
using Microsoft.Graphics.Canvas.UI.Xaml;
using WFontIconSource = Microsoft.UI.Xaml.Controls.FontIconSource;
#endif

namespace Xamarin.Forms.Platform.UWP
{
	public sealed class FontImageSourceHandler : IImageSourceHandler, IIconElementHandler
	{
		public Task<Windows.UI.Xaml.Media.ImageSource> LoadImageAsync(ImageSource imagesource,
			CancellationToken cancelationToken = default(CancellationToken))
		{
			if (!(imagesource is FontImageSource fontsource))
				return null;

#if !HAS_UNO
            var device = CanvasDevice.GetSharedDevice();
			var dpi = Math.Max(_minimumDpi, Windows.Graphics.Display.DisplayInformation.GetForCurrentView().LogicalDpi);

			var textFormat = new CanvasTextFormat
			{
				FontFamily = fontsource.FontFamily.ToFontFamily().Source,
				FontSize = (float)fontsource.Size,
				HorizontalAlignment = CanvasHorizontalAlignment.Center,
				VerticalAlignment = CanvasVerticalAlignment.Center,
				Options = CanvasDrawTextOptions.Default
			};

			using (var layout = new CanvasTextLayout(device, fontsource.Glyph, textFormat, (float)fontsource.Size, (float)fontsource.Size))
			{
				var canvasWidth = (float)layout.LayoutBounds.Width + 2;
				var canvasHeight = (float)layout.LayoutBounds.Height + 2;

				var imageSource = new CanvasImageSource(device, canvasWidth, canvasHeight, dpi);
				using (var ds = imageSource.CreateDrawingSession(Windows.UI.Colors.Transparent))
				{
					var iconcolor = (fontsource.Color != Color.Default ? fontsource.Color : Color.White).ToWindowsColor();

					// offset by 1 as we added a 1 inset
					var x = (float)layout.DrawBounds.X * -1;

					ds.DrawTextLayout(layout, x, 1f, iconcolor);
				}

				return Task.FromResult((Windows.UI.Xaml.Media.ImageSource)imageSource);
			}
#else
            return null;
#endif		
		}

#if !HAS_UNO
		public Task<Microsoft.UI.Xaml.Controls.IconSource> LoadIconSourceAsync(ImageSource imagesource, CancellationToken cancellationToken = default(CancellationToken))
		{
			Microsoft.UI.Xaml.Controls.IconSource image = null;

			if (imagesource is FontImageSource fontImageSource)
			{
				image = new WFontIconSource
				{
					Glyph = fontImageSource.Glyph,
					FontSize = fontImageSource.Size,
					Foreground = fontImageSource.Color.ToBrush()
				};

				var uwpFontFamily = fontImageSource.FontFamily.ToFontFamily().Source;

				if (!string.IsNullOrEmpty(uwpFontFamily))
					((WFontIconSource)image).FontFamily = new FontFamily(uwpFontFamily);
			}

			return Task.FromResult(image);
		}
#endif

		public Task<IconElement> LoadIconElementAsync(ImageSource imagesource, CancellationToken cancellationToken = default(CancellationToken))
		{
			IconElement image = null;

			if (imagesource is FontImageSource fontImageSource)
			{
				image = new FontIcon
				{
					Glyph = fontImageSource.Glyph,
					FontSize = fontImageSource.Size,
					Foreground = fontImageSource.Color.ToBrush()
				};

				var uwpFontFamily = fontImageSource.FontFamily.ToFontFamily().Source;

				if (!string.IsNullOrEmpty(uwpFontFamily))
					((FontIcon)image).FontFamily = new FontFamily(uwpFontFamily);
			}

			return Task.FromResult(image);
		}
	}
}
