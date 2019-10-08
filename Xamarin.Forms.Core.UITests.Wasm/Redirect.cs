using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.DeviceInfo { }

namespace Xamarin.UITest
{
	public interface IApp : Uno.UITest.IApp { }

	namespace Queries	
	{
		public interface AppRect : Uno.UITest.IAppRect { }
		public interface AppQuery : Uno.UITest.IAppQuery { }
		public interface AppResult : Uno.UITest.IAppResult { }
	}

	namespace iOS
	{
		internal class Stub { }
	}

	namespace Android
	{
		internal class Stub { }
	}

	namespace Helpers
	{
		internal class Stub { }
	}
}
