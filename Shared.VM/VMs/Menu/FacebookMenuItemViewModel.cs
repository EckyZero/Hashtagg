using System;
using Shared.Common;
using Microsoft.Practices.Unity;
using System.Threading.Tasks;

namespace Shared.VM
{
	public class FacebookMenuItemViewModel : BaseMenuItemViewModel
	{
		#region Variables

		private IFacebookHelper _facebookHelper;
		private string _title = ApplicationResources.Facebook;
		private string _subtitle = String.Empty;

		#endregion

		#region Properties

		public override string Title {
			get { return _title;}
			set { Set (() => Title, ref _title, value); }
		}

		public override string Subtitle { 
			get { return _subtitle; }
			set { Set (() => Subtitle, ref _subtitle, value); }
		}

		#endregion

		#region Methods

		public FacebookMenuItemViewModel (Action<IListItem> selectedCallback) : base (selectedCallback)
		{
			_facebookHelper = IocContainer.GetContainer ().Resolve<IFacebookHelper> ();
		}

		public override async Task DidLoad ()
		{
			await base.DidLoad ();

			var account = _facebookHelper.GetAccount ();

			MenuItemType = (account == null) ? MenuItemType.Add : MenuItemType.Added;
			Subtitle = (account == null) ? String.Empty : account.Username;

			UpdateImageName ();
		}

		public override void Selected ()
		{
			var account = _facebookHelper.GetAccount ();

			if(account != null) {
				_facebookHelper.DeleteAccount ();
				base.OnSelected (this);
			} 
			else {
				_facebookHelper.Authenticate (() => base.OnSelected (this));
			}
		}

		#endregion
	}
}

