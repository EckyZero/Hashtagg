﻿using System;
using GalaSoft.MvvmLight.Command;
using Shared.Common;
using Microsoft.Practices.Unity;

namespace Shared.VM
{
	public class MenuViewModel : SharedViewModelBase
	{
		#region Variables

		private ITwitterHelper _twitterHelper;
		private IFacebookHelper _facebookHelper;

		#endregion

		#region Properties

		public RelayCommand TwitterCommand { get; private set; }
		public RelayCommand FacebookCommand { get; private set; }

		#endregion

		#region Methods

		public MenuViewModel () : base ()
		{
			_twitterHelper = IocContainer.GetContainer ().Resolve<ITwitterHelper> ();
			_facebookHelper = IocContainer.GetContainer ().Resolve<IFacebookHelper> ();
		}

		protected override void InitCommands ()
		{
			TwitterCommand = new RelayCommand (TwitterCommandExecute);
			FacebookCommand = new RelayCommand (FacebookCommandExecute);
		}

		private void TwitterCommandExecute ()
		{
			_twitterHelper.Authenticate(null);
		}

		private void FacebookCommandExecute ()
		{
			_facebookHelper.Authenticate (null);
		}

		#endregion
	}
}

