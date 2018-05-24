using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace Xamarin.Forms.Internals
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class EnumerableExtensions
	{

		public static IEnumerable<T> GetChildGesturesFor<T>(this IEnumerable<GestureElement> elements, Func<T, bool> predicate = null) where T : GestureRecognizer
		{
			if (elements == null)
				yield break;

			if (predicate == null)
				predicate = x => true;

			foreach (var element in elements)
				foreach (var item in element.GestureRecognizers)
				{
					var gesture = item as T;
					if (gesture != null && predicate(gesture))
						yield return gesture;
				}
		}

		private struct Unroll3Enumerable<T>
		{
			internal const int Capacity = 3;
			internal int Count; 
			internal T Item0;
			internal T Item1;
			internal T Item2;

			internal Unroll3Enumerable(IEnumerable<T> enumerable)
			{
				Count = 0;
				Item0 = Item1 = Item2 = default(T);

				var enumerator = enumerable.GetEnumerator();
				if (enumerator.MoveNext())
				{
					Count++;
					Item0 = enumerator.Current;
					if (enumerator.MoveNext())
					{
						Count++;
						Item1 = enumerator.Current;
						if (enumerator.MoveNext())
						{
							Count++;
							Item2 = enumerator.Current;

							if (enumerator.MoveNext())
								Count++;
						}
					}
				}
			}

			public bool Overflow => Count > Capacity;

			public T this[int index]
			{
				get
				{
					switch (index)
					{
						case 0: return Item0;
						case 1: return Item1;
						case 2: return Item2;
						default: throw new InvalidOperationException("Unroll overflow");
					}
				}
			}
		}

		public static IEnumerable<T> GetGesturesFor<T>(this IEnumerable<IGestureRecognizer> gestures, Func<T, bool> predicate = null) where T : GestureRecognizer
		{
			if (gestures == null)
				yield break;

			if (predicate == null)
				predicate = x => true;

			// optimization to avoid new List<> allocation
			var unroll = new Unroll3Enumerable<IGestureRecognizer>(gestures);
			if (!unroll.Overflow)
			{
				for (var i = 0; i < unroll.Count; i++)
				{
					IGestureRecognizer item = unroll[i];
					var gesture = item as T;
					if (gesture != null && predicate(gesture))
					{
						yield return gesture;
					}
				}
			}

			foreach (IGestureRecognizer item in new List<IGestureRecognizer>(gestures))
			{
				var gesture = item as T;
				if (gesture != null && predicate(gesture))
				{
					yield return gesture;
				}
			}
		}

		internal static IEnumerable<T> Append<T>(this IEnumerable<T> enumerable, T item)
		{
			foreach (T x in enumerable)
				yield return x;

			yield return item;
		}

		public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
		{
			foreach (T item in enumeration)
			{
				action(item);
			}
		}

		public static int IndexOf<T>(this IEnumerable<T> enumerable, T item)
		{
			if (enumerable == null)
				throw new ArgumentNullException("enumerable");

			var i = 0;
			foreach (T element in enumerable)
			{
				if (Equals(element, item))
					return i;

				i++;
			}

			return -1;
		}

		public static int IndexOf<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
		{
			var i = 0;
			foreach (T element in enumerable)
			{
				if (predicate(element))
					return i;

				i++;
			}

			return -1;
		}

		public static IEnumerable<T> Prepend<T>(this IEnumerable<T> enumerable, T item)
		{
			yield return item;

			foreach (T x in enumerable)
				yield return x;
		}
	}
}