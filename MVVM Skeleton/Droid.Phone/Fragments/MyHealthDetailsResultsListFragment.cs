using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Shared.Common;
using Shared.VM;
using Shared.VM.Interfaces;
using GalaSoft.MvvmLight.Helpers;
using Microsoft.Practices.Unity;
using Android.Graphics;
using AutoMapper;
using Droid.Phone.Activities;
using Droid.Phone.UIHelpers;
using Shared.BL;
using Fragment = Android.Support.V4.App.Fragment;
using Droid.Phone.UIHelpers.Incentives;

namespace Droid.Phone.Fragments
{
    public class MyHealthDetailsResultsListFragment : MyHealthDetailsBaseListFragment
    {
        private HealthDetailsResultListViewModel _viewModel;

        private LayoutInflater _inflater;

        private ObservableCollection<BaseListItemViewModel> _viewModelDataSource;

        private ObservableCollection<ExtendedBaseListItemViewModel> _listDataSource;


		public MyHealthDetailsResultsListFragment(HealthDetailsResultListViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            _inflater = inflater;

			var view = base.OnCreateView (inflater, container, savedInstanceState);

            return view;
        }

        //Here we decide to go to the lookup page and assign what we want to do on select
        protected override void ViewModel_RequestAddPage(object sender, EventArgs e)
        {
			
        }
        protected override void Models_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
        }

        protected override void RetrieveData()
        {

			foreach(var item in ListDataSource){
				var vm = (BiometricResultCellViewModel) item;
				vm.MoveToDetailsPageHandler -= NavigateToDetailsPage;
				vm.MoveToDetailsPageHandler -= NavigateToMyIncentivesPage;
			}

			ListDataSource.Clear();

            _viewModelDataSource = _viewModel.Models;

            foreach (var data in _viewModelDataSource)
            {
				var item = (BiometricResultCellViewModel) data;
				item.MoveToDetailsPageHandler += NavigateToDetailsPage;
                ListDataSource.Add(item);
            }

			var viewMyIncentivesVm = new BiometricResultCellViewModel (ApplicationResources.ViewMyIncentives);
			viewMyIncentivesVm.MoveToDetailsPageHandler += NavigateToMyIncentivesPage;
			ListDataSource.Add (viewMyIncentivesVm);

            base.RetrieveData();
        }

		private void NavigateToDetailsPage(object sender, BiometricResult biometricResult)
		{
			var detailsViewModel = new BiometricResultDetailViewModel ();
			detailsViewModel.OrganizeHistoricalData(_viewModel.GetBiometricHistoryFor(biometricResult));
			NavigationService.NavigateTo(ViewModelLocator.BIOMETRIC_RESULT_DETAIL_VIEW_KEY, detailsViewModel);
		}

		private void NavigateToMyIncentivesPage(object sender, BiometricResult biometricResult)
		{
			NavigationService.NavigateTo(ViewModelLocator.INCENTIVE_SUMMARY_KEY, 
				IocContainer.GetContainer().Resolve<IMenuItemsBL>().HasIncentives
				? MenuActionType.Incentives
				: MenuActionType.Recommendations);
		}


        protected override View SetupCell(int position, ExtendedBaseListItemViewModel cellViewModel, View convertView)
        {
			var vm = cellViewModel as BiometricResultCellViewModel;
			return vm.ToView (_inflater);
        }
			
        protected override ObservableCollection<ExtendedBaseListItemViewModel> ListDataSource
        {
            get { return _listDataSource = _listDataSource ?? new ObservableCollection<ExtendedBaseListItemViewModel>(); }
            set { _listDataSource = value; }
        }

        protected override int MainImage
        {
			get { return Resource.Drawable.procedures; }
        }

        protected override BaseListViewModel ViewModel
        {
            get
            {
                return _viewModel;
            }
        }

        protected override void ViewModel_RequestPreviousPage(object sender, EventArgs e)
        {
            FragmentManager.PopBackStack(MyHealthDetailsResultsFragment.RESULTS_PROMPT_BACK_KEY, (int)PopBackStackFlags.Inclusive);
        }
    }
}

