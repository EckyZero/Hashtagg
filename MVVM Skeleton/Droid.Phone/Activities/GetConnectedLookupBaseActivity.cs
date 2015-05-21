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
    //T= Provider
	public abstract class GetConnectedLookupBaseActivity<T,R> : BaseActivity where T : IIdentifiable
    {

        private bool _processScroll = false;

        private readonly object _lock = new object();

        protected abstract LookupViewModelBase<T,R> ViewModel { get; }

        protected abstract string ParameterQueryString { get; }

        protected abstract Action ParameterAction { get; }

        protected abstract Action<T> OnSelectAndReturn { get; }

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

            SetContentView(Resource.Layout.GetConnectedLookup);

            //Bind Controls and ViewModel to Class
            InitBindings();

            //Get Params
            GetAndStoreParameters();

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
            _lookupSearchView.QueryTextChange += LookupSearchViewTextChanged;
        }
 
        private void LookupSearchViewTextChanged(object sender, SearchView.QueryTextChangeEventArgs args)
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

            _listView = FindViewById<ListView>(Resource.Id.GetConnectedLookupListView);
            _listView.ItemClick += ListViewItemClicked;

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
 
        private void ListViewItemClicked(object s, AdapterView.ItemClickEventArgs e)
        {
            ViewModel.SelectedResult = ViewModel.Results[e.Position];
                
            //If we just want to get the data and not save, the parameters will contain to select and return
            if (OnSelectAndReturn != null)
            {
                OnSelectAndReturn(ViewModel.SelectedResult);    
            }
            //If We want to save but preform an action after we will have an action
            else if (ParameterAction != null)
            {
                ViewModel.SelectionCommand.Execute(ViewModel.Results[e.Position]);
                ParameterAction();
            }
            //Else we just save
            else
            {
                ViewModel.SelectionCommand.Execute(ViewModel.Results[e.Position]);
            }
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

        protected abstract void InitAndStoreViewModel();

        private void InitBindings()
        {
            InitAndStoreViewModel();

            ViewModel.SearchFinished += OnSearchFinished;

            _lookupSearchView = FindViewById<SearchView>(Resource.Id.GetConnectedLookupSearch);
            _lookupProgressBar = FindViewById<ProgressBar>(Resource.Id.GetConnectedLookupProgressBar);

            _lookupLookupErrorDetailsTextView = FindViewById<TextView>(Resource.Id.GetConnectedLookupErrorDetails);
            _lookupLookupErrorTextView = FindViewById<TextView>(Resource.Id.GetConnectedLookupError);
            _lookupLookupErrorNoResultsTextView = FindViewById<TextView>(Resource.Id.GetConnectedLookupErrorNoResults);
            _lookupLookupErrorTryTextView = FindViewById<TextView>(Resource.Id.GetConnectedLookupErrorTry);
            _lookupLookupErrorRelativeLayout = FindViewById<RelativeLayout>(Resource.Id.GetConnectedLookupErrorLayout);

            _lookupLookupCancelButton = FindViewById<Button>(Resource.Id.GetConnectedLookupCancel);
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