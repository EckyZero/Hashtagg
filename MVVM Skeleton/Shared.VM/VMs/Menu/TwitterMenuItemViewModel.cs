using System;
using Shared.Common;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace Shared.VM
{
	public class TwitterMenuItemViewModel : BaseMenuItemViewModel
	{
		#region Variables

		private ITwitterHelper _twitterHelper;
		private string _subtitle = String.Empty;

		#endregion

		#region Properties

		public override string Title {
			get { return ApplicationResources.Twitter; }
		}

		public override string Subtitle { 
			get { return _subtitle; }
		}
		// TODO: Return the imageName of the current state
		public override string ImageName { get { return ""; } }

		#endregion

		#region Methods

		public TwitterMenuItemViewModel () : base ()
		{
			_twitterHelper = IocContainer.GetContainer ().Resolve<ITwitterHelper> ();
		}

		public override async Task DidLoad ()
		{
			await base.DidLoad ();

			var account = await _twitterHelper.GetAccount ();

			_subtitle = account.Properties ["email"];
		}

		public override async void Selected ()
		{
			var exists = await _twitterHelper.AccountExists();

			if(exists) {
				_twitterHelper.DeleteAccount ();
				RequestLoggedOutFormat (this);
			} 
			else {
				_twitterHelper.Authenticate (() => RequestLoggedInFormatExecute());
			}
		}

		#endregion
	}
}

