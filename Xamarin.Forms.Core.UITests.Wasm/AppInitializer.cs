using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Uno.UITest;

namespace Uno.UITests.Helpers
{
	/// <summary>
	/// A Uno.UITest initializer
	/// </summary>
	public class AppInitializer
	{
		/// <summary>
		/// Name of the environment variable containing the running UI Test platform.
		/// </summary>
		public const string UNO_UITEST_PLATFORM = "UNO_UITEST_PLATFORM";

		/// <summary>
		/// Name of the environment variable containing the iOS App Bundle path
		/// </summary>
		public const string UITEST_IOSBUNDLE_PATH = "UNO_UITEST_IOSBUNDLE_PATH";

		/// <summary>
		/// Name of the environment variable containing the iOS Device ID or name to use to run the UI Tests.
		/// </summary>
		public const string UITEST_IOSDEVICE_ID = "UITEST_IOSDEVICE_ID";

		/// <summary>
		/// Name of the environment variable containing the path of the APK to use when running android tests.
		/// </summary>
		public const string UITEST_ANDROIDAPK_PATH = "UNO_UITEST_ANDROIDAPK_PATH";

		/// <summary>
		/// Name of the environment variable containing the path to use when creating screenshots.
		/// </summary>
		public const string UITEST_SCREENSHOT_PATH = "UNO_UITEST_SCREENSHOT_PATH";

		private static IApp _currentApp;

		public static string WebAssemblyDefaultUri { get; set; }
		public static string ChromeDriverPath { get; set; }
		public static bool WebAssemblyHeadless { get; set; }

		/// <summary>
		/// Cold starts the registered app.
		/// </summary>
		/// <remarks>This method is generally called from the type constructor of a test assembly.</remarks>
		/// <returns>An <see cref="IApp"/> instance representing the running application.</returns>
		public static IApp ColdStartApp()
		{
			var app = StartApp(alreadyRunningApp: false);

			return app;
		}

		/// <summary>
		/// Attach to an already running application.
		/// </summary>
		/// <returns>An <see cref="IApp"/> instance representing the running application.</returns>
		public static IApp AttachToApp()
		{
			var app = StartApp(alreadyRunningApp: TestContext.CurrentContext.CurrentRepeatCount == 0);

			return app;
		}

		private static IApp StartApp(bool alreadyRunningApp)
		{
			Console.WriteLine($"Starting app ({alreadyRunningApp})");

			if (alreadyRunningApp)
			{
				return CreateBrowserApp(alreadyRunningApp);
			}
			else
			{
				// Skip cold app start, there's no notion of reuse active browser yet.
				return null;
			}
		}

		private static IApp CreateBrowserApp(bool alreadyRunningApp)
		{
			if (_currentApp != null)
			{
				if (!alreadyRunningApp)
				{
					_currentApp.Dispose();
				}
				else
				{
					return _currentApp;
				}
			}

			try
			{
				var configurator = Uno.UITest.Selenium.ConfigureApp
					.WebAssembly
					.Uri(new Uri(WebAssemblyDefaultUri));

				if (!string.IsNullOrEmpty(ChromeDriverPath))
				{
					configurator = configurator.ChromeDriverLocation(
						Path.Combine(TestContext.CurrentContext.TestDirectory,
						ChromeDriverPath.Replace('\\', Path.DirectorySeparatorChar)));
				}

				if (!WebAssemblyHeadless)
				{
					configurator = configurator
						.Headless(false)
						.SeleniumArgument("--remote-debugging-port=9222");
				}
				else
				{
					configurator = configurator
						.Headless(true)
						// Workaround for :
						//   ERROR:browser_process_sub_thread.cc(210)] Waited 5 ms for network service
						//
						.SeleniumArgument("--disable-features=NetworkService");
				}

				_currentApp = configurator.ScreenShotsPath(TestContext.CurrentContext.TestDirectory)
					.StartApp();

				return _currentApp;
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine(ex.Message);
				throw;
			}
		}
	}
}