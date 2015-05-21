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
using Fragment = Android.Support.V4.App.Fragment;

namespace Droid.Phone.Fragments
{
    public class MyHealthDetailsConditionsListFragment : MyHealthDetailsBaseListFragment
    {
        private HealthDetailsConditionListViewModel _viewModel;

        private LayoutInflater _inflater;

        private ExtendedBaseListItemViewModel _emptyHeaderListItem;

        private ExtendedBaseListItemViewModel _addMoreButtonListItem;

        private ObservableCollection<BaseListItemViewModel> _viewModelDataSource;

        private ObservableCollection<ExtendedBaseListItemViewModel> _listDataSource;


        public MyHealthDetailsConditionsListFragment(HealthDetailsConditionListViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            _inflater = inflater;

            _emptyHeaderListItem = new ExtendedBaseListItemViewModel(string.Empty,
                string.Empty, null, true);

            _addMoreButtonListItem = new ExtendedBaseListItemViewModel(_viewModel.AddButtonTitle, string.Empty, null,
                false, true);

            return base.OnCreateView(inflater, container, savedInstanceState);
        }

        //Here we decide to go to the lookup page and assign what we want to do on select
        protected override void ViewModel_RequestAddPage(object sender, EventArgs e)
        {
            var nextViewModel = new GenericConditionLookupViewModel(
                IocContainer.GetContainer().Resolve<IConditionBL>(), IocContainer.GetContainer().Resolve<IGeolocator>());

            nextViewModel.RequestPostSelectionPage = PostSelectionPage;

            NavigationService.NavigateTo(ViewModelLocator.GENERIC_CONDITIONS_LOOKUP_PAGE, nextViewModel);
        }

        protected override void PostSelectionPage(IIdentifiable procedure)
        {
            NavigationService.NavigateTo(ViewModelLocator.HOME_CONTAINER_KEY, null, new ActivityFlags[] { ActivityFlags.ClearTop, ActivityFlags.SingleTop });
        }



        protected override void RetrieveData()
        {
            ListDataSource.Clear();

            _viewModelDataSource = _viewModel.Models;

            //This is used to create the seperator
            ListDataSource.Add(_emptyHeaderListItem);

            foreach (var data in _viewModelDataSource)
            {
                var item = new ExtendedBaseListItemViewModel(data);
                item.RequestEditPageAction += RequestEditPageAction;
                ListDataSource.Add(item);
            }

            ListDataSource.Add(_addMoreButtonListItem);

            base.RetrieveData();
        }

        private class ViewHolder : Java.Lang.Object, IViewHolder
        {
            public TextView Body { get; set; }
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
                rowView.FindViewById<TextView>(Resource.Id.GetConnectedPromptListFooter).Visibility = ViewStates.Gone;
                rowView.FindViewById<TextView>(Resource.Id.GetConnectedPromptListFooterTwo).Visibility = ViewStates.Gone;

                viewHolder = new ViewHolder()
                {
                    Body = rowView.FindViewById<TextView>(Resource.Id.GetConnectedPromptListBody),
                    BackGroundLayout = rowView.FindViewById<RelativeLayout>(Resource.Id.GetConnectedPromptListCellMainLayout)
                };

                rowView.Tag = new CellDataHolder()
                {
                    ViewHolder = viewHolder
                };
            }
            else
            {
                viewHolder = (rowView.Tag as CellDataHolder).ViewHolder as ViewHolder;
            }

            viewHolder.BackGroundLayout.SetBackgroundColor(cellViewModel.BackgroundColor.ToDroidColor());
            viewHolder.Body.Text = cellViewModel.Title;
            
            return rowView;
        }

        void addAnotherButton_Click(object sender, EventArgs e)
        {
            _viewModel.AddCommand.Execute(null);
        }

        private void RequestEditPageAction(BaseListItemViewModel baseListItemViewModel)
        {
            var nextViewModel = new GenericConditionLookupViewModel(
                IocContainer.GetContainer().Resolve<IConditionBL>(), 
                IocContainer.GetContainer().Resolve<IGeolocator>());

            var condition = baseListItemViewModel.Model as PatientCondition;

            nextViewModel.QueryString = condition != null ? condition.Description : string.Empty;

            nextViewModel.EditMode = true;

            nextViewModel.PreviousResult = condition;

            nextViewModel.RequestPostSelectionPage = PostSelectionPage;

            NavigationService.NavigateTo(ViewModelLocator.GENERIC_CONDITIONS_LOOKUP_PAGE, nextViewModel);
        }


        protected override ObservableCollection<ExtendedBaseListItemViewModel> ListDataSource
        {
            get { return _listDataSource = _listDataSource ?? new ObservableCollection<ExtendedBaseListItemViewModel>(); }
            set { _listDataSource = value; }
        }

        protected override int MainImage
        {
            get { return Resource.Drawable.Conditions; }
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
            FragmentManager.PopBackStack(MyHealthDetailsConditionsFragment.CONDITIONS_PROMPT_BACK_KEY, (int)PopBackStackFlags.Inclusive);
        }
    }
}

