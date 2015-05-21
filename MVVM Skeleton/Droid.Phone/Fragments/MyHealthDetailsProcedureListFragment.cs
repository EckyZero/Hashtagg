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
using Droid.Phone.UIHelpers.ViewHolders;
using Droid.UIHelpers;
using Shared.BL;

namespace Droid.Phone.Fragments
{
    public class MyHealthDetailsProcedureListFragment : MyHealthDetailsBaseListFragment
    {
        private HealthDetailsProcedureListViewModel _viewModel;

        private LayoutInflater _inflater;

        private ExtendedBaseListItemViewModel _futureHeaderListItem;

        private ExtendedBaseListItemViewModel _pastHeaderListItem;
        
        private ExtendedBaseListItemViewModel _addMoreButtonListItem;

        private ObservableCollection<BaseListItemViewModel> _futureProcedures;

        private ObservableCollection<BaseListItemViewModel> _pastProcedures;

        private ObservableCollection<ExtendedBaseListItemViewModel> _listDataSource;


        public MyHealthDetailsProcedureListFragment(HealthDetailsProcedureListViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            _inflater = inflater;

            _futureHeaderListItem = new ExtendedBaseListItemViewModel(ApplicationResources.UpcomingProcedures,
                string.Empty, null, true);

            _pastHeaderListItem = new ExtendedBaseListItemViewModel(ApplicationResources.ProcedureHistory, string.Empty,
                null, true);

            _addMoreButtonListItem = new ExtendedBaseListItemViewModel(_viewModel.AddButtonTitle, string.Empty, null,
                false, true);

            return base.OnCreateView(inflater, container, savedInstanceState);
        }

        //Here we decide to go to the lookup page and assign what we want to do on select
        protected override void ViewModel_RequestAddPage(object sender, EventArgs e)
        {
            var nextViewModel = new GenericProcedureLookupViewModel(
                IocContainer.GetContainer().Resolve<IProcedureBL>(), 
                IocContainer.GetContainer().Resolve<IGeolocator>());

            nextViewModel.OnSelect = OnSelect;
            
            NavigationService.NavigateTo(ViewModelLocator.GENERIC_PROCEDURE_LOOKUP_PAGE, nextViewModel);
        }

        //Here we set what we want to do when a item in the lookup is selected
        #region OnSelect
        protected override void OnSelect(IIdentifiable procedure)
        {
            var postLookupSelectViewModel =
                new GenericProcedurePromptInformationViewModel(IocContainer.GetContainer().Resolve<IProcedureBL>(), new PatientProcedure(procedure as Procedure));

            postLookupSelectViewModel.RequestPostSaveReturnPage += postLookupSelectViewModel_RequestPostSaveReturnPage;

            postLookupSelectViewModel.RequestCancelPage += postLookupSelectViewModel_RequestCancelPage;

            NavigationService.NavigateTo(ViewModelLocator.GENERIC_PROCEDURE_INFORMATION_PAGE,postLookupSelectViewModel);
        }

        void postLookupSelectViewModel_RequestCancelPage(object sender, EventArgs e)
        {
            NavigationService.NavigateTo(ViewModelLocator.HOME_CONTAINER_KEY,null,new ActivityFlags[]{ActivityFlags.ClearTop,ActivityFlags.SingleTop});
        }

        void postLookupSelectViewModel_RequestPostSaveReturnPage(object sender, EventArgs e)
        {
            NavigationService.NavigateTo(ViewModelLocator.HOME_CONTAINER_KEY,null,new ActivityFlags[]{ActivityFlags.ClearTop,ActivityFlags.SingleTop});
        }
        #endregion



        protected override void RetrieveData()
        {
            _futureProcedures = _viewModel.GetFutureProcedures();

            _pastProcedures = _viewModel.GetPastProcedures();

            ListDataSource.Clear();

            ListDataSource.Add(_futureHeaderListItem);

            foreach (var data in _futureProcedures)
            {
                var item = new ExtendedBaseListItemViewModel(data);
                item.RequestEditPageAction += RequestEditPageAction;
                ListDataSource.Add(item);
            }

            ListDataSource.Add(_addMoreButtonListItem);

            if (_pastProcedures.Any())
            {
                ListDataSource.Add(_pastHeaderListItem);

                foreach (var data in _pastProcedures)
                {
                    var item = new ExtendedBaseListItemViewModel(data);
                    item.RequestEditPageAction += RequestEditPageAction;
                    ListDataSource.Add(item);
                }
            }
            base.RetrieveData();
        }

        private class ViewHolder : Java.Lang.Object, IViewHolder
        {
            public TextView Body { get; set; }
            public TextView Footer { get; set; }
            public RelativeLayout BackGroundLayout { get; set; }
        }

        protected override View SetupCell(int position, ExtendedBaseListItemViewModel cellViewModel, View convertView)
        {
            var rowView = convertView;

            if (cellViewModel.IsSectionHeader)
            {
                if (rowView == null || rowView.Id != Resource.Id.BaseListSectionHeaderMainLayout)
                {
                    rowView = _inflater.Inflate(Resource.Layout.BaseListSectionHeader, null);

                    TextView sectionHeader = rowView.FindViewById<TextView>(Resource.Id.BaseSectionTitle);
                    if (string.IsNullOrEmpty(cellViewModel.Title))
                        sectionHeader.Visibility = ViewStates.Gone;
                    else
                        sectionHeader.Text = cellViewModel.Title;

                    rowView.Tag = sectionHeader;
                }
                else
                {
                    TextView sectionHeader = rowView.Tag as TextView;
                    if (string.IsNullOrEmpty(cellViewModel.Title))
                    {
                        sectionHeader.Visibility = ViewStates.Gone;
                    }
                    else
                    {
                        sectionHeader.Visibility = ViewStates.Visible;
                        sectionHeader.Text = cellViewModel.Title;
                    }
                }
                return rowView;
            }
            if (cellViewModel.IsAddMoreButton)
            {
                if (rowView == null || rowView.Id != Resource.Id.BaseAddAnotherButtonLayout)
                {
                    rowView = _inflater.Inflate(Resource.Layout.BaseListAddAnotherButton, null);
                    var buttonLayout = rowView.FindViewById<RelativeLayout>(Resource.Id.BaseAddAnotherButtonLayout);
                    buttonLayout.Click += addAnotherButton_Click;
                    var buttonText = buttonLayout.FindViewById<TextView>(Resource.Id.BaseAddAnotherButtonText);
                    buttonText.Text = cellViewModel.Title;
                }
                return rowView;
            }

            ViewHolder viewHolder;

            if (rowView == null || rowView.Id != Resource.Id.GetConnectedPromptListCellMainLayout)
            {
                rowView = _inflater.Inflate(Resource.Layout.GetConnectedPromptListCell, null);

                rowView.FindViewById<TextView>(Resource.Id.GetConnectedPromptListHeader).Visibility = ViewStates.Gone;
                rowView.FindViewById<TextView>(Resource.Id.GetConnectedPromptListFooterTwo).Visibility = ViewStates.Gone;

                viewHolder = new ViewHolder()
                {
                    Body = rowView.FindViewById<TextView>(Resource.Id.GetConnectedPromptListBody),
                    Footer = rowView.FindViewById<TextView>(Resource.Id.GetConnectedPromptListFooter),
                    BackGroundLayout = rowView.FindViewById<RelativeLayout>(Resource.Id.GetConnectedPromptListCellMainLayout)
                };

                rowView.Tag = new CellDataHolder() {ViewHolder = viewHolder};
            }
            else
            {
                viewHolder = (rowView.Tag as CellDataHolder).ViewHolder as ViewHolder;
            }

            viewHolder.BackGroundLayout.SetBackgroundColor(cellViewModel.BackgroundColor.ToDroidColor());
            viewHolder.Body.Text = cellViewModel.Title;
            viewHolder.Footer.Text = cellViewModel.Subtitle;

            return rowView;
        }

        void addAnotherButton_Click(object sender, EventArgs e)
        {
            _viewModel.AddCommand.Execute(null);
        }

        private void RequestEditPageAction(BaseListItemViewModel baseListItemViewModel)
        {
            var postLookupSelectViewModel =
                new GenericProcedurePromptInformationViewModel(IocContainer.GetContainer().Resolve<IProcedureBL>(), baseListItemViewModel.Model as PatientProcedure,true);
            
            postLookupSelectViewModel.RequestPostSaveReturnPage += postLookupSelectViewModel_RequestPostSaveReturnPage;

            postLookupSelectViewModel.RequestCancelPage += postLookupSelectViewModel_RequestCancelPage;

            NavigationService.NavigateTo(ViewModelLocator.GENERIC_PROCEDURE_INFORMATION_PAGE, postLookupSelectViewModel);
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
            FragmentManager.PopBackStack(MyHealthDetailsProcedureFragment.PROCEDURE_PROMPT_BACK_KEY, (int)PopBackStackFlags.Inclusive);
        }
    }
}

