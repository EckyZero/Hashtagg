
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
using Shared.VM;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Helpers;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Util;
using Android.Views.InputMethods;


namespace Droid.Phone
{
    [Activity(Label = "CommentActivity")]			
    public class CommentActivity : ActionBarBaseActivity
    {
        private CommentViewModel _viewModel;

        private ListView _commentsList;

        private EditText _comment;

        private TextView _replyButton;


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            _viewModel = _navigationService.GetAndRemoveParameter(Intent) as CommentViewModel;
            SetContentView(Resource.Layout.Comments);

            SetSupportActionBar(FindViewById<Toolbar>(Resource.Id.CommentsToolbar));
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            Title = _viewModel.Title;

            _commentsList = FindViewById<ListView>(Resource.Id.CommentsListView);

            _replyButton = FindViewById<TextView>(Resource.Id.CommentReplyButton);
            _replyButton.SetCommand("Click", _viewModel.ReplyCommand);
            _replyButton.Enabled = false;

            _comment = FindViewById<EditText>(Resource.Id.CommentReplyText);
            _comment.Hint = _viewModel.CommentPlaceholder;

            _commentsList.Adapter = new ObservableAdapter<IListItem>()
            {
                    DataSource = _viewModel.CardViewModels,
                    GetTemplateDelegate = GetCell,
            };
            var footer = new View(this);
            footer.LayoutParameters = new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, (int)Math.Ceiling(TypedValue.ApplyDimension(ComplexUnitType.Dip, 50f, Application.Context.Resources.DisplayMetrics)));

            _commentsList.AddFooterView(footer);

            _comment.TextChanged += (sender, e) => _viewModel.Comments = _comment.Text;

            _viewModel.RequestDismissKeyboard = () =>
            {
                    InputMethodManager imm = (InputMethodManager)GetSystemService(Context.InputMethodService);
                    imm.HideSoftInputFromWindow(_comment.WindowToken, 0);

            };
            
            _viewModel.RequestCanExecute = (enabled) => _replyButton.Enabled = enabled;
            _viewModel.PropertyChanged += (sender, e) => 
                {
                    switch(e.PropertyName)
                    {
                        case "Comments":
                            if(!_comment.Text.Equals(_viewModel.Comments))
                                _comment.Text = _viewModel.Comments;
                            break;
                    }
                };
        }

        protected override void OnResume()
        {
            _viewModel.DidAppear();
            base.OnResume();
        }

        public override void GoBack()
        {
            base.GoBack();
        }

        private void CleanupCard(View card)
        {
            if(card != null && card.Tag != null)
            {
                if(card.Tag.GetType() == typeof(Droid.Phone.AdapterHelpers.BaseCardViewHolder))
                {
                    var vH = (card.Tag as Droid.Phone.AdapterHelpers.BaseCardViewHolder);
                    if(vH != null && vH.CleanUpCell != null )
                        vH.CleanUpCell();
                }
                //    case typeof(Droid.Phone.AdapterHelpers.HeaderCardViewHolder).ToString():

            }
        }

        public override void OnBackPressed()
        {
            _viewModel.WillDisappear();

            foreach (View card in allViews)
            {
                CleanupCard(card);
            }

            base.OnBackPressed();
        }
        private List<View> allViews = new List<View>();
        private View GetCell(int position, IListItem cVm, View convertView)
        {
            View cardCell = null;

            allViews.Remove(convertView);

            switch (cVm.ListItemType)
            {
                case ListItemType.Default:
                    cardCell = AdapterHelpers.ProcessSocialCard(position, cVm as BaseContentCardViewModel, convertView, LayoutInflater);
                    break;
                case ListItemType.Header:
                    CleanupCard(convertView);
                    cardCell = AdapterHelpers.ProcessHeaderCard(position, cVm, convertView);
                    break;
                case ListItemType.MenuItem:
                default:
                    CleanupCard(convertView);
                    break;
            }
            if(cardCell == null)
                cardCell = LayoutInflater.Inflate(Resource.Layout.DefaultCell, null, false);

            allViews.Add(cardCell);
            return cardCell;
        }
    }
}

