using System;
using Shared.Common;
using System.Threading.Tasks;

namespace Shared.Service
{
	public class LifecyleService : ILifecycleService
	{
		#region Properties

		public event EventHandler ApplicationWillStart;
		public event EventHandler ApplicationWillPause;
		public event EventHandler ApplicationWillResume;
		public event EventHandler ApplicationWillTerminate;

		#endregion

		public LifecyleService () { }

		#region Methods

		public void OnStart ()
		{
			if(ApplicationWillStart != null)
			{
				ApplicationWillStart (this, new EventArgs ());
			}
		}

		public void OnResume ()
		{
			if(ApplicationWillResume != null)
			{
				ApplicationWillResume (this, new EventArgs ());
			}
		}

		public void OnPause ()
		{
			if(ApplicationWillPause != null)
			{
				ApplicationWillPause (this, new EventArgs ());
			}
		}

		public void OnTerminated ()
		{
			if(ApplicationWillTerminate != null)
			{
				ApplicationWillTerminate (this, new EventArgs ());
			}
		}

		#endregion
	}
}

