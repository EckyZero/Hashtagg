using System;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using System.Collections.Generic;

namespace Shared.Common
{
	
    // You should be able to change the IoC container here if you want. You should not need to.

    
    // From http://www.codeproject.com/Tips/655515/Integrating-Unity-Container-with-MVVM-Light-Framew
    public class UnityServiceLocator : IServiceLocator
	{
		private readonly IUnityContainer _unityContainer; 

		public UnityServiceLocator(IUnityContainer unityContainer)
		{
			_unityContainer = unityContainer;
		} 
		public object GetInstance(Type serviceType)
		{
			return _unityContainer.Resolve(serviceType);
		} 
		public object GetInstance(Type serviceType, string key)
		{
			return _unityContainer.Resolve(serviceType, key);
		} 
		public IEnumerable<object> GetAllInstances(Type serviceType)
		{
			return _unityContainer.ResolveAll(serviceType);
		} 
		public TService GetInstance<TService>()
		{
			return _unityContainer.Resolve<TService>();
		} 
		public TService GetInstance<TService>(string key)
		{
			return _unityContainer.Resolve<TService>(key);
		} 
		public IEnumerable<TService> GetAllInstances<TService>()
		{
			return _unityContainer.ResolveAll<TService>();
		}
		public object GetService (Type serviceType)
		{
			return _unityContainer.Resolve(serviceType);
		}
	}
}

