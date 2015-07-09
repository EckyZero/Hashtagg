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
		private string _title = ApplicationResources.Twitter;
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

		public TwitterMenuItemViewModel (Action<IListItem> selectedCallback) : base (selectedCallback)
		{
			_twitterHelper = IocContainer.GetContainer ().Resolve<ITwitterHelper> ();
		}

		public override async Task DidLoad ()
		{
			await base.DidLoad ();

			var account = _twitterHelper.GetAccount ();

			MenuItemType = (account == null) ? MenuItemType.Add : MenuItemType.Added;
			Subtitle = (account == null) ? String.Empty : String.Format("@{0}", account.Properties ["screen_name"]);

			UpdateImageName ();
		}

		public override void Selected ()
		{
			var account = _twitterHelper.GetAccount ();

			if(account != null) {
				_twitterHelper.DeleteAccount ();
				base.OnSelected (this);
			} 
			else {
				_twitterHelper.Authenticate (() => {
					base.OnSelected(this);	
				});
			}
		}

		#endregion
	}
}

