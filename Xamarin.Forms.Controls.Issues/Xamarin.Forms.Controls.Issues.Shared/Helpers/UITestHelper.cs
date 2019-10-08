#if UITEST
using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.UITest;
using NUnit.Framework;
using Xamarin.UITest.Queries;

#if __WASM__
using AppRect = Uno.UITest.IAppRect;
using AppQuery = Uno.UITest.IAppQuery;
using AppResult = Uno.UITest.IAppResult;
#endif

namespace Xamarin.Forms.Controls.Issues
{
	public static class UITestHelper
	{
		public static string ReadText(this AppResult result) =>
			result.Text ?? result.Description;
	}
}

#endif