namespace Xamarin.Forms.Platform.UWP
{
	public partial class WindowsPage : WindowsBasePage
	{
		protected override Platform CreatePlatform()
		{
			return new WindowsPlatform(this);
		}
	}
}