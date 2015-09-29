
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Shared.VM;
using GalaSoft.MvvmLight.Helpers;

namespace Droid.Phone
{
    public class PostFragment : BaseFragment
    {
        private TextView _messageCharacterCount;

        private PostViewModel _viewModel;

        private EditText _postMessage;

        private ImageButton _twitterButton;

        private ImageButton _facebookButton;

        public PostFragment(PostViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.PropertyChanged += OnPropertyChnaged;
            RightActionBarButton = () => NavigationService.GoBack(Shared.Common.AnimationFlag.Down);
        }

        private void OnPropertyChnaged (object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

            switch (e.PropertyName)
            {

                case "CharacterCount":
                    _messageCharacterCount.Text = _viewModel.CharacterCount;
                    break;
                case "IsFacebookSelected":
                    if (_viewModel.IsFacebookSelected)
                        _facebookButton.SetImageResource(Resource.Drawable.socialFacebookSelected);
                    else
                        _facebookButton.SetImageResource(Resource.Drawable.socialFacebookUnselected);
                    break;
                case "IsTwitterSelected":
                    if (_viewModel.IsTwitterSelected)
                        _twitterButton.SetImageResource(Resource.Drawable.socialTwitterSelected);
                    else
                        _twitterButton.SetImageResource(Resource.Drawable.socialTwitterUnselected);
                    break;
                default:
                    break;
            }


        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var contentView = inflater.Inflate(Resource.Layout.Post, container, false);
            var postButton = contentView.FindViewById<TextView>(Resource.Id.PostPostButton);
            _twitterButton = contentView.FindViewById<ImageButton>(Resource.Id.PostTwitterButton);
            _facebookButton = contentView.FindViewById<ImageButton>(Resource.Id.PostFacebookButton);
            _postMessage = contentView.FindViewById<EditText>(Resource.Id.PostMessageText);
            _messageCharacterCount = contentView.FindViewById<TextView>(Resource.Id.PostTextCount);


            if (_viewModel.IsFacebookEnabled)
                _facebookButton.SetCommand("Click", _viewModel.FacebookCommand);
            else
                _facebookButton.Visibility = ViewStates.Gone;
            
            if (_viewModel.IsTwitterEnabled)
                _twitterButton.SetCommand("Click", _viewModel.TwitterCommand);
            else
                _twitterButton.Visibility = ViewStates.Gone;

            _postMessage.TextChanged += PostMessage_TextChanged;
            _messageCharacterCount.Text = _viewModel.CharacterCount;

            postButton.SetCommand("Click", _viewModel.PostCommand);

            postButton.Enabled = false;
            _viewModel.CanExecute = (bool canExecute) => 
            {
                postButton.Enabled = canExecute;
            };
            _viewModel.RequestDismissPage = NavigationService.GoBack;

            return contentView;
        }

        void PostMessage_TextChanged (object sender, Android.Text.TextChangedEventArgs e)
        {
            _viewModel.Message = _postMessage.Text;
        }
    }
}

