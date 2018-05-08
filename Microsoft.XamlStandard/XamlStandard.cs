using Xamarin.Forms;
using System;

namespace Microsoft.XamlStandard
{
	#region Manual Generation

	[ContentPropertyAttribute("Child")]
	public class Border : Frame
	{
		public static readonly BindableProperty BorderBrushProperty = BorderColorProperty;

		public Color BorderBrush
		{
			get { return BorderColor; }
			set { BorderColor = value; }
		}

		public View Child
		{
			get { return Content; }
			set { Content = value; }
		}
	}

	public class Button : Xamarin.Forms.Button
	{
		public static readonly BindableProperty BorderBrushProperty = BorderColorProperty;

		public Color BorderBrush
		{
			get { return BorderColor; }
			set { BorderColor = value; }
		}
	}

	public class ComboBox : Picker
	{
		public ComboBox()
		{
			SelectedIndexChanged += (sender, args) => SelectionChanged?.Invoke(this, EventArgs.Empty);
		}

		public static readonly BindableProperty ForegroundProperty = TextColorProperty;

		public Color Foreground
		{
			get { return TextColor; }
			set { TextColor = value; }
		}

		public static readonly BindableProperty HeaderProperty = TitleProperty;

		public string Header
		{
			get { return Title; }
			set { Title = value; }
		}

		public event EventHandler SelectionChanged;
	}

	public class ProgressBar : Xamarin.Forms.ProgressBar
	{
		public static readonly BindableProperty ValueProperty = ProgressProperty;

		public double Value
		{
			get { return Progress; }
			set { Progress = value; }
		}
	}

	public class ProgressRing : ActivityIndicator
	{
		public static readonly BindableProperty IsActiveProperty = IsRunningProperty;

		public bool IsActive
		{
			get { return IsRunning; }
			set { IsRunning = value; }
		}
	}

	public class StackPanel : StackLayout
	{

	}

	public class TextBlock : Label
	{
		public static readonly BindableProperty ForegroundProperty = TextColorProperty;

		public Color Foreground
		{
			get { return TextColor; }
			set { TextColor = value; }
		}

		public static readonly BindableProperty TextWrappingProperty = BindableProperty.Create(nameof(TextWrapping), typeof(TextWrapping), typeof(TextBlock), TextWrapping.NoWrap, propertyChanged: TextWrappingChanged);

		static void TextWrappingChanged(BindableObject bindable, object oldValue, object newValue)
		{
			((TextBlock)bindable).LineBreakMode = ((TextWrapping)newValue).ToLineBreakMode();
		}

		public TextWrapping TextWrapping
		{
			get { return (TextWrapping)GetValue(TextWrappingProperty); }
			set { SetValue(TextWrappingProperty, value); }
		}
	}

	public class TextBox : Entry
	{
		public static readonly BindableProperty ForegroundProperty = TextColorProperty;

		public Color Foreground
		{
			get { return TextColor; }
			set { TextColor = value; }
		}

		public static readonly BindableProperty PlaceholderForegroundProperty = PlaceholderColorProperty;

		public Color PlaceholderForeground
		{
			get { return PlaceholderColor; }
			set { PlaceholderColor = value; }
		}

		public static readonly BindableProperty PlaceholderTextProperty = PlaceholderProperty;

		public string PlaceholderText
		{
			get { return Placeholder; }
			set { Placeholder = value; }
		}
	}

	public enum TextWrapping
	{
		NoWrap,
		Wrap,
		WrapWholeWords
	}

	public static class TextWrappingExtensions
	{
		public static LineBreakMode ToLineBreakMode(this TextWrapping self)
		{
			switch (self)
			{
				case TextWrapping.NoWrap:
					return LineBreakMode.NoWrap;
				case TextWrapping.Wrap:
					return LineBreakMode.CharacterWrap;
				case TextWrapping.WrapWholeWords:
					return LineBreakMode.WordWrap;
				default:
					throw new ArgumentOutOfRangeException(nameof(self), self, null);
			}
		}

		public static TextWrapping ToTextWrapping(this LineBreakMode self)
		{
			switch (self)
			{
				case LineBreakMode.NoWrap:
					return TextWrapping.NoWrap;
				case LineBreakMode.WordWrap:
					return TextWrapping.WrapWholeWords;
				case LineBreakMode.CharacterWrap:
					return TextWrapping.Wrap;
				case LineBreakMode.HeadTruncation:
					return TextWrapping.NoWrap;
				case LineBreakMode.TailTruncation:
					return TextWrapping.NoWrap;
				case LineBreakMode.MiddleTruncation:
					return TextWrapping.NoWrap;
				default:
					throw new ArgumentOutOfRangeException(nameof(self), self, null);
			}
		}
	}

	public class ToggleSwitch : Switch
	{
		public static readonly BindableProperty IsOnProperty = IsToggledProperty;

		public bool IsOn
		{
			get { return IsToggled; }
			set { IsToggled = value; }
		}
	}

	public class UserControl : ContentView
	{

	}

	#endregion

	public class AbsoluteLayout : Xamarin.Forms.AbsoluteLayout { }
	public class ActivityIndicator : Xamarin.Forms.ActivityIndicator { }
	public class Animation : Xamarin.Forms.Animation { }
	public class Application : Xamarin.Forms.Application { }
	public class AppLinkEntry : Xamarin.Forms.AppLinkEntry { }
	public class AutomationProperties : Xamarin.Forms.AutomationProperties { }
	public class BackButtonPressedEventArgs : Xamarin.Forms.BackButtonPressedEventArgs { }
	public class BoxView : Xamarin.Forms.BoxView { }
	public class CarouselPage : Xamarin.Forms.CarouselPage { }
	public class EntryCell : Xamarin.Forms.EntryCell { }
	public class ImageCell : Xamarin.Forms.ImageCell { }
	public class SwitchCell : Xamarin.Forms.SwitchCell { }
	public class TextCell : Xamarin.Forms.TextCell { }
	public class ViewCell : Xamarin.Forms.ViewCell { }
	public class ConstraintExpression : Xamarin.Forms.ConstraintExpression { }
	public class ContentPage : Xamarin.Forms.ContentPage { }
	public class ContentPresenter : Xamarin.Forms.ContentPresenter { }
	public class ContentView : Xamarin.Forms.ContentView { }
	public class ControlTemplate : Xamarin.Forms.ControlTemplate { }
	public class DataTemplate : Xamarin.Forms.DataTemplate { }
	public class DatePicker : Xamarin.Forms.DatePicker { }
	public class Editor : Xamarin.Forms.Editor { }
	public class Entry : Xamarin.Forms.Entry { }
	public class FlexLayout : Xamarin.Forms.FlexLayout { }
	public class FormattedString : Xamarin.Forms.FormattedString { }
	public class Frame : Xamarin.Forms.Frame { }
	public class Grid : Xamarin.Forms.Grid { }
	public class HtmlWebViewSource : Xamarin.Forms.HtmlWebViewSource { }
	public class Image : Xamarin.Forms.Image { }
	public class Label : Xamarin.Forms.Label { }
	public class ListView : Xamarin.Forms.ListView { }
	public class MasterDetailPage : Xamarin.Forms.MasterDetailPage { }
	public class Menu : Xamarin.Forms.Menu { }
	public class MenuItem : Xamarin.Forms.MenuItem { }
	public class MessagingCenter : Xamarin.Forms.MessagingCenter { }
	public class NavigationPage : Xamarin.Forms.NavigationPage { }
	public class On : Xamarin.Forms.On { }
	public class Page : Xamarin.Forms.Page { }
	public class PanGestureRecognizer : Xamarin.Forms.PanGestureRecognizer { }
	public class Picker : Xamarin.Forms.Picker { }
	public class RelativeLayout : Xamarin.Forms.RelativeLayout { }
	public class ResourceDictionary : Xamarin.Forms.ResourceDictionary { }
	public class ScrollView : Xamarin.Forms.ScrollView { }
	public class SearchBar : Xamarin.Forms.SearchBar { }
	public class Slider : Xamarin.Forms.Slider { }
	public class StackLayout : Xamarin.Forms.StackLayout { }
	public class Stepper : Xamarin.Forms.Stepper { }
	public class StreamImageSource : Xamarin.Forms.StreamImageSource { }
	public class Switch : Xamarin.Forms.Switch { }
	public class TabbedPage : Xamarin.Forms.TabbedPage { }
	public class TableView : Xamarin.Forms.TableView { }
	public class TemplateBinding : Xamarin.Forms.TemplateBinding { }
	public class TemplatedPage : Xamarin.Forms.TemplatedPage { }
	public class TemplatedView : Xamarin.Forms.TemplatedView { }
	public class TimePicker : Xamarin.Forms.TimePicker { }
	public class ToolbarItem : Xamarin.Forms.ToolbarItem { }
	public class UrlWebViewSource : Xamarin.Forms.UrlWebViewSource { }
	public class VisualStateGroupList : Xamarin.Forms.VisualStateGroupList { }
	public class WebView : Xamarin.Forms.WebView { }
}
