using System;
using Demo.Shared.Exceptions;
using Demo.Shared.Helpers;
using Microsoft.Practices.Unity;
using Xamarin;

namespace Demo.Shared
{
    public static class SharedDal
    {
        public static ISecureDatabase GetDatabase()
        {
			return App.IocContainer.Resolve<ISecureDatabase>(); 
        }
    }
}