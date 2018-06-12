using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Core;
using Windows.ApplicationModel.LockScreen;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage;
using Windows.Storage.Search;
using Windows.Storage.Streams;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Xamarin.Forms.Internals;

namespace Xamarin.Forms.Platform.UWP
{
	internal abstract class WindowsBasePlatformServices : IPlatformServices
	{
		readonly CoreDispatcher _dispatcher;

		protected WindowsBasePlatformServices(CoreDispatcher dispatcher)
		{
			if (dispatcher == null)
				throw new ArgumentNullException(nameof(dispatcher));

			_dispatcher = dispatcher;
		}

		public void BeginInvokeOnMainThread(Action action)
		{
			_dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => action()).WatchForError();
		}

		public Ticker CreateTicker()
		{
			return new WindowsTicker();
		}

		public virtual Assembly[] GetAssemblies()
		{
#if !HAS_UNO
			var options = new QueryOptions { FileTypeFilter = { ".exe", ".dll" } };

			StorageFileQueryResult query = Package.Current.InstalledLocation.CreateFileQueryWithOptions(options);
			IReadOnlyList<StorageFile> files = query.GetFilesAsync().AsTask().Result;

			var assemblies = new List<Assembly>(files.Count);
			foreach (StorageFile file in files)
			{
				try
				{
					Assembly assembly = Assembly.Load(new AssemblyName { Name = Path.GetFileNameWithoutExtension(file.Name) });

					assemblies.Add(assembly);
				}
				catch (IOException)
				{
				}
				catch (BadImageFormatException)
				{
				}
			}

			Assembly thisAssembly = GetType().GetTypeInfo().Assembly;
			// this happens with .NET Native
			if (!assemblies.Contains(thisAssembly))
				assemblies.Add(thisAssembly);

			Assembly xamlAssembly = typeof(Xamarin.Forms.Xaml.IMarkupExtension).GetTypeInfo().Assembly;
			if (!assemblies.Contains(xamlAssembly))
				assemblies.Add(xamlAssembly);

			return assemblies.ToArray();

#else
			return AppDomain.CurrentDomain.GetAssemblies();
#endif
		}

		public string GetMD5Hash(string input)
		{
#if HAS_UNO
			// MSDN - Documentation -https://msdn.microsoft.com/en-us/library/system.security.cryptography.md5(v=vs.110).aspx
			using (System.Security.Cryptography.MD5 md5Hash = System.Security.Cryptography.MD5.Create())
			{
				// Convert the input string to a byte array and compute the hash.
				byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

				// Create a new Stringbuilder to collect the bytes
				// and create a string.
				StringBuilder sBuilder = new StringBuilder();

				// Loop through each byte of the hashed data 
				// and format each one as a hexadecimal string.
				for (int i = 0; i < data.Length; i++)
				{
					sBuilder.Append(data[i].ToString("x2"));
				}

				// Return the hexadecimal string.
				return sBuilder.ToString();
			}

#else
			HashAlgorithmProvider algorithm = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Md5);
			IBuffer buffer = algorithm.HashData(Encoding.Unicode.GetBytes(input).AsBuffer());
			return CryptographicBuffer.EncodeToHexString(buffer);
#endif
		}

		public double GetNamedSize(NamedSize size, Type targetElementType, bool useOldSizes)
		{
			return size.GetFontSize();
		}

		public async Task<Stream> GetStreamAsync(Uri uri, CancellationToken cancellationToken)
		{
			using (var client = new HttpClient())
			{
				HttpResponseMessage streamResponse = await client.GetAsync(uri.AbsoluteUri).ConfigureAwait(false);

				if (!streamResponse.IsSuccessStatusCode)
				{
					Log.Warning("HTTP Request", $"Could not retrieve {uri}, status code {streamResponse.StatusCode}");
					return null;
				}

				return await streamResponse.Content.ReadAsStreamAsync().ConfigureAwait(false);
			}
		}

		public IIsolatedStorageFile GetUserStoreForApplication()
		{
			return new WindowsIsolatedStorage(ApplicationData.Current.LocalFolder);
		}

		public bool IsInvokeRequired => !_dispatcher.HasThreadAccess;

		public string RuntimePlatform => Device.UWP;

		public void OpenUriAction(Uri uri)
		{
			Launcher.LaunchUriAsync(uri).WatchForError();
		}

		public void StartTimer(TimeSpan interval, Func<bool> callback)
		{
			var timer = new DispatcherTimer { Interval = interval };
			timer.Start();
			timer.Tick += (sender, args) =>
			{
				bool result = callback();
				if (!result)
					timer.Stop();
			};
		}

		public void QuitApplication()
		{
			Log.Warning(nameof(WindowsBasePlatformServices), "Platform doesn't implement QuitApp");
		}
	}
}