using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Shared.VM;
using GalaSoft.MvvmLight.Helpers;
using Shared.Common;

namespace Droid.Phone.Activities
{
    public abstract class GenericLookupBaseActivity<T,R> : BaseActivity where T : IIdentifiable
    {

        private bool _processScroll = false;

        private readonly object _lock = new object();

        protected abstract GenericLookupViewModelBase<T,R> ViewModel { get; }

        protected abstract string ParameterQueryString { get; }

        protected abstract string QueryHint { get; }

        private ListView _listView;

        private ProgressBar _lookupProgressBar;

        private SearchView _lookupSearchView;

        private TextView _lookupLookupErrorTextView;

        private TextView _lookupLookupErrorDetailsTextView;

        private TextView _lookupLookupErrorTryTextView;

        private TextView _lookupLookupErrorNoResultsTextView;

        private RelativeLayout _lookupLookupErrorRelativeLayout;

        private Button _lookupLookupCancelButton;

        private View _footerView;

        private bool _noRemainingResults;

        private int _totalPreviousResults = 0;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.BaseLookup);

            //Get Params
            GetAndStoreParameters();

            //Bind Controls and ViewModel to Class
            InitBindings();

            //Configure and Bind Search
            PrepareSearch();

            //Configure and Bind ListView and Adaptor
            PrepareListView();

            //Configure and Bind Screen Buttons
            PrepareButtons();

        }

        private void PrepareSearch()
        {
            _lookupSearchView.SetQueryHint(QueryHint);
            _lookupSearchView.RequestFocus();
            _lookupSearchView.QueryTextChange += TextChanged;
        }
 
        private void TextChanged(object sender, SearchView.QueryTextChangeEventArgs args)
        {
            ViewModel.SearchCommand.Execute(_lookupSearchView.Query);
            _totalPreviousResults = 0;
            AnimateOnSearch();
        }

        private void PrepareListView()
        {
            ObservableAdapter<T> adapter = new ObservableAdapter<T>()
            {
                DataSource = ViewModel.Results,
                GetTemplateDelegate = GetProviderTemplate
            };

            _listView = FindViewById<ListView>(Resource.Id.BaseLookupListView);
            _listView.ItemClick += ItemClicked;

            _footerView = LayoutInflater.Inflate(Resource.Layout.GetConnectedLookupProgressCell, null);

            _listView.AddFooterView(_footerView);

            _listView.Adapter = adapter;

            _listView.RemoveFooterView(_footerView);

            _listView.Scroll += ListView_Scroll;

            if (!string.IsNullOrEmpty(ParameterQueryString))
            {
                _lookupSearchView.SetQuery(ParameterQueryString, true);
            }
        }
 
        private void ItemClicked(object s, AdapterView.ItemClickEventArgs e)
        {
            ViewModel.SelectedResult = ViewModel.Results[e.Position];

            ViewModel.SelectionCommand.Execute(null);
        }

        private void PrepareButtons()
        {
            _lookupLookupCancelButton.SetCommand("Click", ViewModel.CancelCommand);
        }

        private async void ListView_Scroll(object sender, AbsListView.ScrollEventArgs e)
        {
            // BE CAREFUL EDITING THIS CODE

            //Scrolling is Async and Calling this method multiple times at once
            //We lock around our proceed check to prevent more than one instance 
            //from being able to proceed through to the actual search
            //NOTE: _processScroll is cleared in OnSearchFinished callback from ViewModel

            bool proceed = false;

            lock (_lock)
            {
                if (!_processScroll && !_noRemainingResults && e.TotalItemCount >= 10 &&
                    e.FirstVisibleItem + e.VisibleItemCount > (e.TotalItemCount - 1))
                {
                    _processScroll = true;
                    proceed = true;
                }
            }

            if (proceed)
            {
                ViewModel.ShowMoreCommand.Execute(_lookupSearchView.Query);
                if (_listView.FooterViewsCount == 0)
                {
                    _listView.AddFooterView(_footerView);
                }
            }
        }

        protected virtual void InitAndStoreViewModel()
        {
            
        }

        private void InitBindings()
        {
            InitAndStoreViewModel();

            ViewModel.SearchFinished += OnSearchFinished;

            _lookupSearchView = FindViewById<SearchView>(Resource.Id.BaseLookupSearch);
            _lookupProgressBar = FindViewById<ProgressBar>(Resource.Id.BaseLookupProgressBar);

            _lookupLookupErrorDetailsTextView = FindViewById<TextView>(Resource.Id.BaseLookupErrorDetails);
            _lookupLookupErrorTextView = FindViewById<TextView>(Resource.Id.BaseLookupError);
            _lookupLookupErrorNoResultsTextView = FindViewById<TextView>(Resource.Id.BaseLookupErrorNoResults);
            _lookupLookupErrorTryTextView = FindViewById<TextView>(Resource.Id.BaseLookupErrorTry);
            _lookupLookupErrorRelativeLayout = FindViewById<RelativeLayout>(Resource.Id.BaseLookupErrorLayout);

            _lookupLookupCancelButton = FindViewById<Button>(Resource.Id.BaseLookupCancel);
        }

        private void AnimateOnSearch()
        {
            HideLookupError();
            _lookupProgressBar.Visibility = ViewStates.Visible;
        }

        private void StopAnimating()
        {
            if (_listView.FooterViewsCount > 0)
            {
                _listView.RemoveFooterView(_footerView);
            }
            else
            {
                _lookupProgressBar.Visibility = ViewStates.Gone;
            }
        }

        private void HideLookupError()
        {
            _lookupLookupErrorRelativeLayout.Visibility = ViewStates.Gone;

        }

        private void ShowLookupError()
        {
            _lookupLookupErrorRelativeLayout.Visibility = ViewStates.Visible;

        }

        private void OnSearchFinished(object sender, SearchEventArgs args)
        {
            Application.SynchronizationContext.Post(state =>
            {
                _processScroll = false;
                if (ViewModel.Results.Count > 0 || args.SearchCancelled)
                {
                    StopAnimating();

                    _noRemainingResults = ViewModel.Results.Count - _totalPreviousResults < 10;
                    _totalPreviousResults = ViewModel.Results.Count;
                }
                else
                {
                    _lookupLookupErrorNoResultsTextView.Text = String.Format("{0} {1}", ViewModel.ErrorMessage, _lookupSearchView.Query);
                    _lookupLookupErrorDetailsTextView.Text = ViewModel.ErrorDetailMessage;
                    _totalPreviousResults = 0;
                    //_noRemainingResults = false;
                    StopAnimating();
                    ShowLookupError();
                }
            }
            , Application.Context);
        }

        protected abstract View GetProviderTemplate(int position, T provider, View convertView);

        protected abstract void GetAndStoreParameters();

    }
}