using System;
using System.Linq;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

#if __WASM__
using AppResult = Uno.UITest.IAppResult;
#endif

namespace Xamarin.Forms.Core.UITests
{
	internal sealed class EventViewContainerRemote : BaseViewContainerRemote
	{
		public EventViewContainerRemote(IApp app, Enum formsType, string platformViewType)
			: base(app, formsType, platformViewType)
		{
		}

		public AppResult GetEventLabel()
		{
			App.WaitForElement(q => q.Raw(EventLabelQuery));
			return App.Query(q => q.Raw(EventLabelQuery)).First();
		}
	}
}