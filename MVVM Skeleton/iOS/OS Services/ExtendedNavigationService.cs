using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GalaSoft.MvvmLight.Views;
using Shared.Common;
using UIKit;

namespace iOS
{
	public class ExtendedNavigationService : NavigationService, IExtendedNavigationService
	{
		private UINavigationController _navigation;
		private readonly Dictionary<string, TypeOrAction> _pagesByKey = new Dictionary<string, TypeOrAction>();

		public ExtendedNavigationService () : base()
		{
		}

		public string CurrentPageKey
		{
			get;
			protected set;
		}


		//Method hides parent method
		public void Initialize(UINavigationController navigation)
		{
			_navigation = navigation;
			base.Initialize (navigation);
		}

		//Method hides parent method
		public void Configure(string key, Func<object, UIViewController> createAction)
		{
			lock (_pagesByKey)
			{
				var item = new TypeOrAction
				{
					CreateControllerAction = createAction
				};

				if (_pagesByKey.ContainsKey(key))
				{
					_pagesByKey[key] = item;
				}
				else
				{
					_pagesByKey.Add(key, item);
				}
			}

			base.Configure (key, createAction);
		}

		//Method hides parent method
		public void Configure(string key, Type controllerType)
		{
			lock (_pagesByKey)
			{
				var item = new TypeOrAction
				{
					ControllerType = controllerType
				};

				if (_pagesByKey.ContainsKey(key))
				{
					_pagesByKey[key] = item;
				}
				else
				{
					_pagesByKey.Add(key, item);
				}
			}
		}


		private struct TypeOrAction
		{
			public Type ControllerType;
			public Func<object, UIViewController> CreateControllerAction;
		}

		public void Present(string pageKey, object parameter = null)
		{
			lock (_pagesByKey)
			{
				if (_pagesByKey.ContainsKey(pageKey))
				{
					var item = _pagesByKey[pageKey];
					UIViewController controller = null;

					if (item.ControllerType != null)
					{
						controller =MakeController(item.ControllerType, parameter);

						if (controller == null)
						{
							throw new InvalidOperationException(
								"No suitable constructor found for page " + pageKey);
						}
					}

					if (item.CreateControllerAction != null)
					{
						controller = item.CreateControllerAction(parameter);
					}
						
					CurrentNavigationController().PresentViewControllerAsync (controller, true);
					CurrentPageKey = pageKey;
				}
				else
				{
					throw new ArgumentException(
						string.Format(
							"No such page: {0}. Did you forget to call NavigationService.Configure?",
							pageKey),
						"pageKey");
				}
			}
		}

		public void GoBack()
		{
			CurrentNavigationController ().PopViewController (true);
		}

		public void NavigateTo(string pageKey)
		{
			NavigateTo (pageKey, null);
		}

		public void NavigateTo(string pageKey, object parameter)
		{
			lock (_pagesByKey)
			{
				if (_pagesByKey.ContainsKey(pageKey))
				{
					var item = _pagesByKey[pageKey];
					UIViewController controller = null;

					if (item.ControllerType != null)
					{
						controller = MakeController(item.ControllerType, parameter);

						if (controller == null)
						{
							throw new InvalidOperationException(
								"No suitable constructor found for page " + pageKey);
						}
					}

					if (item.CreateControllerAction != null)
					{
						controller = item.CreateControllerAction(parameter);
					}
						
					CurrentNavigationController().PushViewController(controller, true);
					CurrentPageKey = pageKey;
				}
				else
				{
					throw new ArgumentException(
						string.Format(
							"No such page: {0}. Did you forget to call NavigationService.Configure?",
							pageKey),
						"pageKey");
				}
			}
		}


		private UIViewController MakeController(Type controllerType, object parameter)
		{
			ConstructorInfo constructor;
			object[] parameters;

			if (parameter == null)
			{
				constructor = controllerType.GetTypeInfo()
					.DeclaredConstructors
					.FirstOrDefault(c => !c.GetParameters().Any());

				parameters = new object[]
				{
				};
			}
			else
			{
				constructor = controllerType.GetTypeInfo()
					.DeclaredConstructors
					.FirstOrDefault(
						c =>
						{
							var p = c.GetParameters();
							return p.Count() == 1
								&& p[0].ParameterType == parameter.GetType();
						});

				parameters = new[]
				{
					parameter
				};
			}

			if (constructor == null)
			{
				return null;
			}

			var controller = constructor.Invoke(parameters) as UIViewController;
			return controller;
		}


		public void DismissPage()
		{
			_navigation.DismissViewController (true, null);
		}

		private UINavigationController CurrentNavigationController ()
		{
			var controller = UIApplication.SharedApplication.KeyWindow.RootViewController;
			var destController = controller.FindViewControllerClass(typeof(UINavigationController)) as UINavigationController;

			return destController;
		}
	}
}

