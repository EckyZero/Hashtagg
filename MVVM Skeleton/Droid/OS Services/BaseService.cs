using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.Practices.Unity;
using Shared.Common;

namespace Droid
{
    public abstract class BaseService
    {
        protected ILogger _logger;

        protected Activity _activity;

        protected IBaseActivity Activity
        {
            get { return _activity as IBaseActivity; }
        }

        internal void RegisterActivity(Activity activity)
        {
            _activity = activity;
        }

        public BaseService()
        {
            _logger = IocContainer.GetContainer().Resolve<ILogger>();
        }

		public Activity GetCurrentActivity()
		{
			return _activity;
		}
    }
}