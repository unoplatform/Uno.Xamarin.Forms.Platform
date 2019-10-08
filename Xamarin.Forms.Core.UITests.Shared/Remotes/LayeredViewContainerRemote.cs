using System;
using System.Linq;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

#if __WASM__
using AppResult = Uno.UITest.IAppResult;
#endif

namespace Xamarin.Forms.Core.UITests
{
	internal sealed class LayeredViewContainerRemote : BaseViewContainerRemote
	{
		public LayeredViewContainerRemote(IApp app, Enum formsType, string platformViewType)
			: base(app, formsType, platformViewType)
		{
		}

		public AppResult GetLayeredLabel()
		{
			return App.Query(q => q.Raw(LayeredLabelQuery)).First();
		}

		public void TapHiddenButton()
		{
			App.Tap(q => q.Raw(LayeredHiddenButtonQuery));
		}
	}
}