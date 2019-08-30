using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: Dependency(typeof(WindowsSerializer))]

// Views

[assembly: Xamarin.Forms.Platform.UWP.ExportRendererAttribute(typeof(Layout), typeof(LayoutRenderer))]
[assembly: Xamarin.Forms.Platform.UWP.ExportRendererAttribute(typeof(BoxView), typeof(BoxViewRenderer))]
[assembly: Xamarin.Forms.Platform.UWP.ExportRendererAttribute(typeof(Image), typeof(ImageRenderer))]
[assembly: Xamarin.Forms.Platform.UWP.ExportRendererAttribute(typeof(ImageButton), typeof(ImageButtonRenderer))]
[assembly: Xamarin.Forms.Platform.UWP.ExportRendererAttribute(typeof(Label), typeof(LabelRenderer))]
[assembly: Xamarin.Forms.Platform.UWP.ExportRendererAttribute(typeof(Button), typeof(ButtonRenderer))]
[assembly: Xamarin.Forms.Platform.UWP.ExportRendererAttribute(typeof(ListView), typeof(ListViewRenderer))]
[assembly: Xamarin.Forms.Platform.UWP.ExportRendererAttribute(typeof(CollectionView), typeof(CollectionViewRenderer))]
[assembly: Xamarin.Forms.Platform.UWP.ExportRendererAttribute(typeof(ScrollView), typeof(ScrollViewRenderer))]
[assembly: Xamarin.Forms.Platform.UWP.ExportRendererAttribute(typeof(ProgressBar), typeof(ProgressBarRenderer))]
[assembly: Xamarin.Forms.Platform.UWP.ExportRendererAttribute(typeof(Slider), typeof(SliderRenderer))]
[assembly: Xamarin.Forms.Platform.UWP.ExportRendererAttribute(typeof(Switch), typeof(SwitchRenderer))]
[assembly: Xamarin.Forms.Platform.UWP.ExportRendererAttribute(typeof(WebView), typeof(WebViewRenderer))]
[assembly: Xamarin.Forms.Platform.UWP.ExportRendererAttribute(typeof(Frame), typeof(FrameRenderer))]
[assembly: Xamarin.Forms.Platform.UWP.ExportRendererAttribute(typeof(ActivityIndicator), typeof(ActivityIndicatorRenderer))]
[assembly: Xamarin.Forms.Platform.UWP.ExportRendererAttribute(typeof(Editor), typeof(EditorRenderer))]
[assembly: Xamarin.Forms.Platform.UWP.ExportRendererAttribute(typeof(Picker), typeof(PickerRenderer))]
[assembly: Xamarin.Forms.Platform.UWP.ExportRendererAttribute(typeof(TimePicker), typeof(TimePickerRenderer))]
[assembly: Xamarin.Forms.Platform.UWP.ExportRendererAttribute(typeof(DatePicker), typeof(DatePickerRenderer))]
[assembly: Xamarin.Forms.Platform.UWP.ExportRendererAttribute(typeof(Stepper), typeof(StepperRenderer))]
[assembly: Xamarin.Forms.Platform.UWP.ExportRendererAttribute(typeof(Entry), typeof(EntryRenderer))]
[assembly: Xamarin.Forms.Platform.UWP.ExportRendererAttribute(typeof(CheckBox), typeof(CheckBoxRenderer))]
[assembly: Xamarin.Forms.Platform.UWP.ExportRendererAttribute(typeof(TableView), typeof(TableViewRenderer))]
[assembly: Xamarin.Forms.Platform.UWP.ExportRendererAttribute(typeof(NativeViewWrapper), typeof(NativeViewWrapperRenderer))]

//ImageSources

[assembly: Xamarin.Forms.Platform.UWP.ExportImageSourceHandler(typeof(FileImageSource), typeof(FileImageSourceHandler))]
[assembly: Xamarin.Forms.Platform.UWP.ExportImageSourceHandler(typeof(StreamImageSource), typeof(StreamImageSourceHandler))]
[assembly: Xamarin.Forms.Platform.UWP.ExportImageSourceHandler(typeof(UriImageSource), typeof(UriImageSourceHandler))]
[assembly: Xamarin.Forms.Platform.UWP.ExportImageSourceHandler(typeof(FontImageSource), typeof(FontImageSourceHandler))]

// Pages

[assembly: Xamarin.Forms.Platform.UWP.ExportRendererAttribute(typeof(Page), typeof(PageRenderer))]
[assembly: Xamarin.Forms.Platform.UWP.ExportRendererAttribute(typeof(NavigationPage), typeof(NavigationPageRenderer))]
[assembly: Xamarin.Forms.Platform.UWP.ExportRendererAttribute(typeof(MasterDetailPage), typeof(MasterDetailPageRenderer))]
[assembly: Xamarin.Forms.Platform.UWP.ExportRendererAttribute(typeof(CarouselPage), typeof(CarouselPageRenderer))]

// Cells

[assembly: Xamarin.Forms.Platform.UWP.ExportCell(typeof(Cell), typeof(TextCellRenderer))]
[assembly: Xamarin.Forms.Platform.UWP.ExportCell(typeof(ImageCell), typeof(ImageCellRenderer))]
[assembly: Xamarin.Forms.Platform.UWP.ExportCell(typeof(EntryCell), typeof(EntryCellRenderer))]
[assembly: Xamarin.Forms.Platform.UWP.ExportCell(typeof(SwitchCell), typeof(SwitchCellRenderer))]
[assembly: Xamarin.Forms.Platform.UWP.ExportCell(typeof(ViewCell), typeof(ViewCellRenderer))]
[assembly: Dependency(typeof(WindowsResourcesProvider))]
[assembly: Xamarin.Forms.Platform.UWP.ExportRendererAttribute(typeof(SearchBar), typeof(SearchBarRenderer))]
[assembly: Xamarin.Forms.Platform.UWP.ExportRendererAttribute(typeof(TabbedPage), typeof(TabbedPageRenderer))]
