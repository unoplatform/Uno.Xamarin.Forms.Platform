﻿using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.UAP;
using UwpScrollBarVisibility = Windows.UI.Xaml.Controls.ScrollBarVisibility;
using UWPApp = Windows.UI.Xaml.Application;
using UWPDataTemplate = Windows.UI.Xaml.DataTemplate;
using System.Collections.Specialized;

namespace Xamarin.Forms.Platform.UWP
{
	public abstract partial class ItemsViewRenderer : ViewRenderer<ItemsView, ListViewBase>
	{
		protected CollectionViewSource CollectionViewSource;

		protected ListViewBase ListViewBase { get; private set; }
		UwpScrollBarVisibility? _defaultHorizontalScrollVisibility;
		UwpScrollBarVisibility? _defaultVerticalScrollVisibility;

		protected UWPDataTemplate ViewTemplate => (UWPDataTemplate)UWPApp.Current.Resources["View"];
		protected UWPDataTemplate ItemsViewTemplate => (UWPDataTemplate)UWPApp.Current.Resources["ItemsViewDefaultTemplate"];

		FrameworkElement _emptyView;
		View _formsEmptyView;

		protected ItemsControl ItemsControl { get; private set; }

		protected override void OnElementChanged(ElementChangedEventArgs<ItemsView> args)
		{
			base.OnElementChanged(args);
			TearDownOldElement(args.OldElement);
			SetUpNewElement(args.NewElement);
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs changedProperty)
		{
			base.OnElementPropertyChanged(sender, changedProperty);

			if (changedProperty.Is(ItemsView.ItemsSourceProperty))
			{
				UpdateItemsSource();
			}
			else if (changedProperty.Is(ItemsView.ItemTemplateProperty))
			{
				UpdateItemTemplate();
			}
			else if (changedProperty.Is(ItemsView.HorizontalScrollBarVisibilityProperty))
			{
				UpdateHorizontalScrollBarVisibility();
			}
			else if (changedProperty.Is(ItemsView.VerticalScrollBarVisibilityProperty))
			{
				UpdateVerticalScrollBarVisibility();
			}
			else if (changedProperty.IsOneOf(ItemsView.EmptyViewProperty, ItemsView.EmptyViewTemplateProperty))
			{
				UpdateEmptyView();
			}
		}

		protected abstract ListViewBase SelectListViewBase();
		protected abstract void HandleLayoutPropertyChange(PropertyChangedEventArgs property);
		protected abstract IItemsLayout Layout { get; }

		protected virtual void UpdateItemsSource()
		{
			if (ListViewBase == null)
			{
				return;
			}

			// TODO hartez 2018-05-22 12:59 PM Handle grouping

			CleanUpCollectionViewSource();

			if (Element.ItemsSource == null)
			{
				return;
			}

			CollectionViewSource = CreateCollectionViewSource();

			if (CollectionViewSource?.Source is INotifyCollectionChanged incc)
			{
				incc.CollectionChanged += ItemsChanged;
			}

			ListViewBase.ItemsSource = CollectionViewSource.View;

			UpdateEmptyViewVisibility();
		}

		protected virtual void CleanUpCollectionViewSource()
		{
			if (CollectionViewSource != null)
			{
				if (CollectionViewSource.Source is ObservableItemTemplateCollection observableItemTemplateCollection)
				{
					observableItemTemplateCollection.CleanUp();
				}
			}

			if (Element.ItemsSource == null)
			{
				if (CollectionViewSource?.Source is INotifyCollectionChanged incc)
				{
					incc.CollectionChanged -= ItemsChanged;
				}

				if (CollectionViewSource != null)
				{
					CollectionViewSource.Source = null;
				}

				CollectionViewSource = null;
				ListViewBase.ItemsSource = null;
				return;
			}
		}

		protected virtual CollectionViewSource CreateCollectionViewSource()
		{
			var itemsSource = Element.ItemsSource;
			var itemTemplate = Element.ItemTemplate;

			if (itemTemplate != null)
			{
				return new CollectionViewSource
				{
					Source = TemplatedItemSourceFactory.Create(itemsSource, itemTemplate, Element),
					IsSourceGrouped = false
				};
			}
			
			return new CollectionViewSource
			{
				Source = itemsSource,
				IsSourceGrouped = false
			};
		}

		void ItemsChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			UpdateEmptyViewVisibility();
		}

		protected virtual void UpdateItemTemplate()
		{
			if (Element == null || ListViewBase == null)
			{
				return;
			}

			ListViewBase.ItemTemplate = Element.ItemTemplate == null ? null : ItemsViewTemplate;

			UpdateItemsSource();
		}

		void LayoutPropertyChanged(object sender, PropertyChangedEventArgs property)
		{
			HandleLayoutPropertyChange(property);
		}

		protected virtual void SetUpNewElement(ItemsView newElement)
		{
			if (newElement == null)
			{
				return;
			}

			if (ListViewBase == null)
			{
				ListViewBase = SelectListViewBase();
				ListViewBase.IsSynchronizedWithCurrentItem = false;

				Layout.PropertyChanged += LayoutPropertyChanged;

				SetNativeControl(ListViewBase);
			}

			UpdateItemTemplate();
			UpdateItemsSource();
			UpdateVerticalScrollBarVisibility();
			UpdateHorizontalScrollBarVisibility();
			UpdateEmptyView();

			// Listen for ScrollTo requests
			newElement.ScrollToRequested += ScrollToRequested;
		}

		protected virtual void TearDownOldElement(ItemsView oldElement)
		{
			if (oldElement == null)
			{
				return;
			}

			if (Layout != null)
			{
				// Stop tracking the old layout
				Layout.PropertyChanged -= LayoutPropertyChanged;
			}

			// Stop listening for ScrollTo requests
			oldElement.ScrollToRequested -= ScrollToRequested;

			if (ListViewBase != null)
			{
				ListViewBase.ItemsSource = null;
			}

			if (CollectionViewSource != null)
			{
				CollectionViewSource.Source = null;
			}

		}

		void UpdateVerticalScrollBarVisibility()
		{
			if (_defaultVerticalScrollVisibility == null)
				_defaultVerticalScrollVisibility = ScrollViewer.GetVerticalScrollBarVisibility(Control);

			switch (Element.VerticalScrollBarVisibility)
			{
				case (ScrollBarVisibility.Always):
					ScrollViewer.SetVerticalScrollBarVisibility(Control, UwpScrollBarVisibility.Visible);
					break;
				case (ScrollBarVisibility.Never):
					ScrollViewer.SetVerticalScrollBarVisibility(Control, UwpScrollBarVisibility.Hidden);
					break;
				case (ScrollBarVisibility.Default):
					ScrollViewer.SetVerticalScrollBarVisibility(Control, _defaultVerticalScrollVisibility.Value);
					break;
			}
		}

		void UpdateHorizontalScrollBarVisibility()
		{
			if (_defaultHorizontalScrollVisibility == null)
				_defaultHorizontalScrollVisibility = ScrollViewer.GetHorizontalScrollBarVisibility(Control);

			switch (Element.HorizontalScrollBarVisibility)
			{
				case (ScrollBarVisibility.Always):
					ScrollViewer.SetHorizontalScrollBarVisibility(Control, UwpScrollBarVisibility.Visible);
					break;
				case (ScrollBarVisibility.Never):
					ScrollViewer.SetHorizontalScrollBarVisibility(Control, UwpScrollBarVisibility.Hidden);
					break;
				case (ScrollBarVisibility.Default):
					ScrollViewer.SetHorizontalScrollBarVisibility(Control, _defaultHorizontalScrollVisibility.Value);
					break;
			}
		}

		protected virtual async Task ScrollTo(ScrollToRequestEventArgs args)
		{
			if (!(Control is ListViewBase list))
			{
				return;
			}

			var item = FindBoundItem(args);

			if (item == null)
			{
				// Item wasn't found in the list, so there's nothing to scroll to
				return;
			}

			if (args.IsAnimated)
			{
				await ScrollHelpers.AnimateToItemAsync(list, item, args.ScrollToPosition);
			}
			else
			{
				await ScrollHelpers.JumpToItemAsync(list, item, args.ScrollToPosition);
			}
		}

		async void ScrollToRequested(object sender, ScrollToRequestEventArgs args)
		{
			await ScrollTo(args);
		}

		object FindBoundItem(ScrollToRequestEventArgs args)
		{
			if (args.Mode == ScrollToMode.Position)
			{
				if (args.Index >= CollectionViewSource.View.Count)
				{
					return null;
				}

				return CollectionViewSource.View[args.Index];
			}

			if (Element.ItemTemplate == null)
			{
				return args.Item;
			}

			for (int n = 0; n < CollectionViewSource.View.Count; n++)
			{
				if (CollectionViewSource.View[n] is ItemTemplateContext pair)
				{
					if (pair.Item == args.Item)
					{
						return CollectionViewSource.View[n];
					}
				}
			}

			return null;
		}

		protected virtual void UpdateEmptyView()
		{
			if (Element == null || ListViewBase == null)
			{
				return;
			}

			var emptyView = Element.EmptyView;

			if (emptyView == null)
			{
				return;
			}

			switch (emptyView)
			{
				case string text:
					_emptyView = new TextBlock { Text = text };
					break;
				case View view:
					_emptyView = RealizeEmptyView(view);
					break;
				default:
					_emptyView = RealizeEmptyViewTemplate(emptyView, Element.EmptyViewTemplate);
					break;
			}

			(ListViewBase as IEmptyView)?.SetEmptyView(_emptyView);

			UpdateEmptyViewVisibility();
		}

		FrameworkElement RealizeEmptyViewTemplate(object bindingContext, DataTemplate emptyViewTemplate)
		{
			if (emptyViewTemplate == null)
			{
				return new TextBlock { Text = bindingContext.ToString() };
			}

			var template = emptyViewTemplate.SelectDataTemplate(bindingContext, null);
			var view = template.CreateContent() as View;

			view.BindingContext = bindingContext;
			return RealizeEmptyView(view);
		}

		FrameworkElement RealizeEmptyView(View view)
		{
			_formsEmptyView = view;
			return view.GetOrCreateRenderer().ContainerElement;
		}

		protected virtual void UpdateEmptyViewVisibility()
		{
			if (_emptyView != null && ListViewBase is IEmptyView emptyView)
			{
				emptyView.EmptyViewVisibility = (CollectionViewSource?.View?.Count ?? 0) == 0
					? Visibility.Visible
					: Visibility.Collapsed;

				if (emptyView.EmptyViewVisibility == Visibility.Visible)
				{
					if (ActualWidth >= 0 && ActualHeight >= 0)
					{
						_formsEmptyView?.Layout(new Rectangle(0, 0, ActualWidth, ActualHeight));
					}
				}
			}
		}
	}
}