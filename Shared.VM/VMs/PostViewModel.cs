using System;
using GalaSoft.MvvmLight.Command;
using Shared.Common;
using Shared.Service;
using Microsoft.Practices.Unity;
using System.Text;
using System.Threading.Tasks;

namespace Shared.VM
{
	public class PostViewModel : SharedViewModelBase
	{
        #region Variables

        private string _message;
        private string _characterCount;
        private bool _isFacebookSelected;
        private bool _isTwitterSelected;

        private ITwitterService _twitterService;
        private IFacebookService _facebookService;

        private ITwitterHelper _twitterHelper;
        private IFacebookHelper _facebookHelper;

        #endregion

        #region Properties

        public Action<bool> CanExecute { get; set; }

        public string Placeholder
        {
            get { return ApplicationResources.WhatsHappening; }
        }

        public PSColor PlaceholderTextColor
        {
            get { return ThemeManager.Instance.CurrentTheme.Disabled; }
        }

        public PSColor TextColor
        {
            get { return ThemeManager.Instance.CurrentTheme.TextPrimaryColor; }
        }

        public string Message 
        { 
            get { return _message; } 
            set 
            { 
                Set(() => Message, ref _message, value); 
                CharacterCount = _message.Equals(Placeholder) ? "0" : _message.Length.ToString();
                RequestCanExecute();
            }
        }

        public bool IsFacebookSelected
        {
            get { return _isFacebookSelected; }
            set 
            { 
                Set(() => IsFacebookSelected, ref _isFacebookSelected, value); 
                RequestCanExecute();
            }
        }

        public bool IsTwitterSelected
        {
            get { return _isTwitterSelected; }
            set 
            { 
                Set(() => IsTwitterSelected, ref _isTwitterSelected, value); 
                RequestCanExecute();
            }
        }

        public string CharacterCount 
        { 
            get { return _characterCount; }
            set { Set(() => CharacterCount, ref _characterCount, value); }
        }

        public RelayCommand PostCommand { get; private set; }
        public RelayCommand FacebookCommand { get; private set; }
        public RelayCommand TwitterCommand { get; private set; }

        #endregion

        #region Methods

        public PostViewModel () : base ()
 		{
            _twitterHelper = IocContainer.GetContainer().Resolve<ITwitterHelper>();
            _facebookHelper = IocContainer.GetContainer().Resolve<IFacebookHelper>();

            _twitterService = IocContainer.GetContainer().Resolve<ITwitterService>();
            _facebookService = IocContainer.GetContainer().Resolve<IFacebookService>();
		}

        protected override void InitCommands()
        {
            PostCommand = new RelayCommand(PostCommandExecute);
            FacebookCommand = new RelayCommand(FacebookCommandExecute);
            TwitterCommand = new RelayCommand(TwitterCommandExecute);
        }

        private async void PostCommandExecute ()
        {
            _hudService.Show(ApplicationResources.SendingMessage);

            bool facebookError;
            bool twitterError;

            // Fire off all posts
            try
            {
                facebookError = await TryPostFacebook(Message);
                twitterError = await TryPostTwitter(Message);   
            }
            finally
            {
                _hudService.Dismiss();   
            }

            // Format error message as needed
            if(facebookError || twitterError)
            {
                var builder = new StringBuilder();

                builder.AppendFormat(", {0}", facebookError ? ApplicationResources.Facebook : string.Empty);
                builder.AppendFormat(", {0}", twitterError ? ApplicationResources.Twitter : string.Empty);

                var message = builder.ToString();

                message.Trim(new [] { ' ', ',' });
                message.Insert(0, string.Format("{0}\n\n", ApplicationResources.TheFollowingServicesFailed));

                await  _dialogService.ShowMessage(message, ApplicationResources.PleaseTryAgain);
            }
            else
            {
                await _dialogService.ShowMessage(string.Empty, ApplicationResources.Success);

                // Reset state
                Message = string.Empty;
                IsFacebookSelected = false;
                IsTwitterSelected = false;
            }
        }

        private void RequestCanExecute ()
        {
            var canExecute = !string.IsNullOrWhiteSpace(Message) && !Message.Equals(Placeholder);

            if(IsTwitterSelected && Message.Length > 140)
            {
                canExecute = false;
            }
            else if (!IsFacebookSelected && !IsTwitterSelected)
            {
                canExecute = false;
            }

            if(CanExecute != null)
            {
                CanExecute(canExecute);
            }
        }

        private void FacebookCommandExecute ()
        {
            IsFacebookSelected = !IsFacebookSelected;
        }

        private void TwitterCommandExecute ()
        {
            IsTwitterSelected = !IsTwitterSelected;
        }

        private async Task<bool> TryPostFacebook (string message)
        {
            SocialAccount account = _facebookHelper.GetAccount();
            bool error = false;

            // Only try if we have a valid account is enabled
            if(account != null && IsFacebookSelected)
            {
                var id = account.Properties["id"];
                var response = await _facebookService.Post(id, message);

                error = (response != ServiceResponseType.SUCCESS);
            }

            return error;
        }

        private async Task<bool> TryPostTwitter (string message)
        {
            SocialAccount account = _twitterHelper.GetAccount();
            bool error = false;

            // Only try if we have a valid account is enabled
            if(account != null && IsTwitterSelected)
            {
                var response = await _twitterService.Post(message);

                error = (response != ServiceResponseType.SUCCESS);
            }

            return error;
        }

        #endregion
	}
}

