using System;
using System.IO;
using Uno.UITest;
using Xamarin.UITest;

namespace Xamarin.Forms.Controls
{
	internal class AppWrapper : UITest.IApp
	{
		private Uno.UITest.IApp app;

		public AppWrapper(Uno.UITest.IApp app) => this.app = app;

		public IDevice Device => app.Device;

		public void Back() => app.Back();
		public void ClearText() => app.ClearText();
		public void ClearText(string marked) => app.ClearText(marked);
		public void ClearText(Func<IAppQuery, IAppQuery> query) => app.ClearText(query);
		public void ClearText(Func<IAppQuery, IAppWebQuery> query) => app.ClearText(query);
		public void DismissKeyboard() => app.DismissKeyboard();
		public void Dispose() => app.Dispose();
		public void DoubleTap(Func<IAppQuery, IAppQuery> query) => app.DoubleTap(query);
		public void DoubleTap(string marked) => app.DoubleTap(marked);
		public void DoubleTapCoordinates(float x, float y) => app.DoubleTapCoordinates(x,y);
		public void DragAndDrop(Func<IAppQuery, IAppQuery> from, Func<IAppQuery, IAppQuery> to) => app.DragAndDrop(from, to);
		public void DragAndDrop(string from, string to) => app.DragAndDrop(from, to);
		public void DragCoordinates(float fromX, float fromY, float toX, float toY) => app.DragCoordinates(fromX, fromY, toX, toY);
		public void EnterText(string marked, string text) => app.EnterText(marked, text);
		public void EnterText(string text) => app.EnterText(text);
		public void EnterText(Func<IAppQuery, IAppWebQuery> query, string text) => app.EnterText(query, text);
		public void EnterText(Func<IAppQuery, IAppQuery> query, string text) => app.EnterText(query, text);
		public IAppResult[] Flash(string marked) => app.Flash(marked);
		public IAppResult[] Flash(Func<IAppQuery, IAppQuery> query = null) => app.Flash(query);
		public object Invoke(string methodName, object[] arguments) => app.Invoke(methodName, arguments);
		public object Invoke(string methodName, object argument = null) => app.Invoke(methodName, argument);
		public void PinchToZoomIn(string marked, TimeSpan? duration = null) => app.PinchToZoomIn(marked, duration);
		public void PinchToZoomIn(Func<IAppQuery, IAppQuery> query, TimeSpan? duration = null) => app.PinchToZoomIn(query, duration);
		public void PinchToZoomInCoordinates(float x, float y, TimeSpan? duration) => app.PinchToZoomInCoordinates(x, y, duration);
		public void PinchToZoomOut(string marked, TimeSpan? duration = null) => app.PinchToZoomOut(marked, duration);
		public void PinchToZoomOut(Func<IAppQuery, IAppQuery> query, TimeSpan? duration = null) => app.PinchToZoomOut(query, duration);
		public void PinchToZoomOutCoordinates(float x, float y, TimeSpan? duration) => app.PinchToZoomOutCoordinates(x, y, duration);
		public void PressEnter() => app.PressEnter();
		public void PressVolumeDown() => app.PressVolumeDown();
		public void PressVolumeUp() => app.PressVolumeUp();
		public IAppResult[] Query(Func<IAppQuery, IAppQuery> query = null) => app.Query(query);
		public string[] Query(Func<IAppQuery, IInvokeJSAppQuery> query) => app.Query(query);
		public IAppResult[] Query(string marked) => app.Query(marked);
		public IAppWebResult[] Query(Func<IAppQuery, IAppWebQuery> query) => app.Query(query);
		public T[] Query<T>(Func<IAppQuery, IAppTypedSelector<T>> query) => app.Query<T>(query);
		public void Repl() => app.Repl();
		public FileInfo Screenshot(string title) => app.Screenshot(title);
		public void ScrollDown(string withinMarked, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true)
			=> app.ScrollDown(withinMarked, strategy, swipeSpeed, swipeSpeed, withInertia);
		public void ScrollDown(Func<IAppQuery, IAppQuery> withinQuery = null, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true)
			=> app.ScrollDown(withinQuery, strategy, swipeSpeed, swipeSpeed, withInertia);
		public void ScrollDownTo(Func<IAppQuery, IAppWebQuery> toQuery, Func<IAppQuery, IAppQuery> withinQuery = null, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true, TimeSpan? timeout = null) 
			=> app.ScrollDownTo(toQuery, withinQuery, strategy, swipePercentage, swipeSpeed, withInertia, timeout);
		public void ScrollDownTo(Func<IAppQuery, IAppQuery> toQuery, Func<IAppQuery, IAppQuery> withinQuery = null, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true, TimeSpan? timeout = null)
			=> app.ScrollDownTo(toQuery, withinQuery, strategy, swipePercentage, swipeSpeed, withInertia, timeout);
		public void ScrollDownTo(Func<IAppQuery, IAppWebQuery> toQuery, string withinMarked, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true, TimeSpan? timeout = null) 
			=> app.ScrollDownTo(toQuery, withinMarked, strategy, swipePercentage, swipeSpeed, withInertia, timeout);
		public void ScrollDownTo(string toMarked, string withinMarked = null, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true, TimeSpan? timeout = null) 
			=> app.ScrollDownTo(toMarked, withinMarked, strategy, swipePercentage, swipeSpeed, withInertia, timeout);
		public void ScrollTo(string toMarked, string withinMarked = null, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true, TimeSpan? timeout = null) 
			=> app.ScrollDownTo(toMarked, withinMarked, strategy, swipePercentage, swipeSpeed, withInertia, timeout);
		public void ScrollUp(Func<IAppQuery, IAppQuery> query = null, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true) 
			=> app.ScrollUp(query, strategy, swipePercentage, swipeSpeed, withInertia);
		public void ScrollUp(string withinMarked, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true) 
			=> app.ScrollUp(withinMarked, strategy, swipePercentage, swipeSpeed, withInertia);
		public void ScrollUpTo(Func<IAppQuery, IAppQuery> toQuery, Func<IAppQuery, IAppQuery> withinQuery = null, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true, TimeSpan? timeout = null) 
			=> app.ScrollUpTo(toQuery, withinQuery, strategy, swipePercentage, swipeSpeed, withInertia, timeout);
		public void ScrollUpTo(Func<IAppQuery, IAppWebQuery> toQuery, Func<IAppQuery, IAppQuery> withinQuery = null, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true, TimeSpan? timeout = null)
			=> app.ScrollUpTo(toQuery, withinQuery, strategy, swipePercentage, swipeSpeed, withInertia, timeout);
		public void ScrollUpTo(Func<IAppQuery, IAppWebQuery> toQuery, string withinMarked, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true, TimeSpan? timeout = null)
			=> app.ScrollUpTo(toQuery, withinMarked, strategy, swipePercentage, swipeSpeed, withInertia, timeout);
		public void ScrollUpTo(string toMarked, string withinMarked = null, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true, TimeSpan? timeout = null) 
			=> app.ScrollUpTo(toMarked, withinMarked, strategy, swipePercentage, swipeSpeed, withInertia, timeout);
		public void SetOrientationLandscape() => app.SetOrientationLandscape();
		public void SetOrientationPortrait() => app.SetOrientationPortrait();
		public void SetSliderValue(string marked, double value) => app.SetSliderValue(marked, value);
		public void SetSliderValue(Func<IAppQuery, IAppQuery> query, double value) 
			=> app.SetSliderValue(query, value);
		public void SwipeLeftToRight(Func<IAppQuery, IAppWebQuery> query, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true)
			=> app.SwipeLeftToRight(query, swipePercentage, swipeSpeed, withInertia);
		public void SwipeLeftToRight(string marked, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true)
			=> app.SwipeLeftToRight(marked, swipePercentage, swipeSpeed, withInertia);
		public void SwipeLeftToRight(double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true) 
			=> app.SwipeLeftToRight(swipePercentage, swipeSpeed, withInertia);
		public void SwipeLeftToRight(Func<IAppQuery, IAppQuery> query, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true) 
			=> app.SwipeLeftToRight(query, swipePercentage, swipeSpeed, withInertia);
		public void SwipeRightToLeft(double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true)
			=> app.SwipeRightToLeft(swipePercentage, swipeSpeed, withInertia);
		public void SwipeRightToLeft(Func<IAppQuery, IAppWebQuery> query, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true)
			=> app.SwipeRightToLeft(query, swipePercentage, swipeSpeed, withInertia);
		public void SwipeRightToLeft(Func<IAppQuery, IAppQuery> query, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true) 
			=> app.SwipeRightToLeft(query, swipePercentage, swipeSpeed, withInertia);
		public void SwipeRightToLeft(string marked, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true) => app.ClearText(marked);
		public void Tap(string marked) => app.Tap(marked);
		public void Tap(Func<IAppQuery, IAppWebQuery> query) => app.Tap(query);
		public void Tap(Func<IAppQuery, IAppQuery> query) => app.Tap(query);
		public void TapCoordinates(float x, float y) => app.TapCoordinates(x, y);
		public void TouchAndHold(Func<IAppQuery, IAppQuery> query) => app.TouchAndHold(query);
		public void TouchAndHold(string marked) => app.TouchAndHold(marked);
		public void TouchAndHoldCoordinates(float x, float y) => app.TouchAndHoldCoordinates(x, y);
		public void WaitFor(Func<bool> predicate, string timeoutMessage = "Timed out waiting...", TimeSpan? timeout = null, TimeSpan? retryFrequency = null, TimeSpan? postTimeout = null) 
			=> app.WaitFor(predicate, timeoutMessage, timeout, retryFrequency, postTimeout);
		public IAppWebResult[] WaitForElement(Func<IAppQuery, IAppWebQuery> query, string timeoutMessage = "Timed out waiting for element...", TimeSpan? timeout = null, TimeSpan? retryFrequency = null, TimeSpan? postTimeout = null)
			=> app.WaitForElement(query, timeoutMessage, timeout, retryFrequency, postTimeout);
		public IAppResult[] WaitForElement(string marked, string timeoutMessage = "Timed out waiting for element...", TimeSpan? timeout = null, TimeSpan? retryFrequency = null, TimeSpan? postTimeout = null) => app.WaitForElement(marked);
		public IAppResult[] WaitForElement(Func<IAppQuery, IAppQuery> query, string timeoutMessage = "Timed out waiting for element...", TimeSpan? timeout = null, TimeSpan? retryFrequency = null, TimeSpan? postTimeout = null) 
			=> app.WaitForElement(query, timeoutMessage, timeout, retryFrequency, postTimeout);
		public void WaitForNoElement(Func<IAppQuery, IAppQuery> query, string timeoutMessage = "Timed out waiting for no element...", TimeSpan? timeout = null, TimeSpan? retryFrequency = null, TimeSpan? postTimeout = null) 
			=> app.WaitForNoElement(query, timeoutMessage, timeout, retryFrequency, postTimeout);
		public void WaitForNoElement(string marked, string timeoutMessage = "Timed out waiting for no element...", TimeSpan? timeout = null, TimeSpan? retryFrequency = null, TimeSpan? postTimeout = null)
			=> app.WaitForNoElement(marked, timeoutMessage, timeout, retryFrequency, postTimeout);
		public void WaitForNoElement(Func<IAppQuery, IAppWebQuery> query, string timeoutMessage = "Timed out waiting for no element...", TimeSpan? timeout = null, TimeSpan? retryFrequency = null, TimeSpan? postTimeout = null) 
			=> app.WaitForNoElement(query, timeoutMessage, timeout, retryFrequency, postTimeout);
	}
}