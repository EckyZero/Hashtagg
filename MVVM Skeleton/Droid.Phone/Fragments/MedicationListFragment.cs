using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Droid.Phone.UIHelpers;
using Droid.Phone.UIHelpers.ViewHolders;
using Droid.UIHelpers;
using Shared.BL;
using Shared.Common;
using Shared.VM;
using GalaSoft.MvvmLight.Command;
using Microsoft.Practices.Unity;

namespace Droid.Phone.Fragments
{
    public class MedicationListFragment : MyHealthDetailsBaseListFragment
    {
        private MedicationListViewModel _viewModel;

        private LayoutInflater _inflater;

        private ExtendedBaseListItemViewModel _emptyHeaderListItem;

        private ExtendedBaseListItemViewModel _addMoreButtonListItem;

        private ObservableCollection<BaseListItemViewModel> _viewModelDataSource;

        private ObservableCollection<ExtendedBaseListItemViewModel> _listDataSource;

        public MedicationListFragment(MedicationListViewModel viewModel)
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

        protected override void ViewModel_RequestAddPage(object sender, EventArgs e)
        {
            var nextViewModel = new GenericPrescriptionLookupViewModel(
                IocContainer.GetContainer().Resolve<IPrescriptionBL>(),
                IocContainer.GetContainer().Resolve<IGeolocator>());

            nextViewModel.RequestPostSelectionPage = PostSelectionPage;

            NavigationService.NavigateTo(ViewModelLocator.GENERIC_PRESCRIPTION_LOOKUP_PAGE, nextViewModel);
        }

        protected override void PostSelectionPage(IIdentifiable prescription)
        {
            var informationViewModel = new GenericPrescriptionInformationViewModel(
                IocContainer.GetContainer().Resolve<IPrescriptionBL>(),
                new PatientPrescription(prescription as Prescription), false);

            informationViewModel.RequestPostSaveReturnPage += nextViewModel_RequestPostSaveReturnPage;
            informationViewModel.RequestCancelPage += informationViewModel_RequestCancelPage;

            NavigationService.NavigateTo(ViewModelLocator.GENERIC_PRESCRIPTION_INFORMATION_PAGE, informationViewModel);
        }

        void informationViewModel_RequestCancelPage(object sender, EventArgs e)
        {
            NavigationService.NavigateTo(ViewModelLocator.HOME_CONTAINER_KEY, null,
                new ActivityFlags[] {ActivityFlags.ClearTop, ActivityFlags.SingleTop});
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
                    var buttonText = buttonLayout.FindViewById<TextView>(Resource.Id.BaseAddAnotherButtonText);
                    buttonLayout.Click += addAnotherButton_Click;
                    buttonText.Text = cellViewModel.Title;
                }
                return rowView;
            } 

            ViewHolder viewHolder;

            if (rowView == null || rowView.Id != Resource.Layout.GetConnectedPromptListCell)
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
            var prescription = baseListItemViewModel.Model as PatientPrescription;
            var nextViewModel = new GenericPrescriptionInformationViewModel(
                IocContainer.GetContainer().Resolve<IPrescriptionBL>(),
                prescription, true);
            
            nextViewModel.RequestPostSaveReturnPage += nextViewModel_RequestPostSaveReturnPage;
            nextViewModel.RequestCancelPage += nextViewModel_RequestCancelPage;
            NavigationService.NavigateTo(ViewModelLocator.GENERIC_PRESCRIPTION_INFORMATION_PAGE, nextViewModel);
        }

        void nextViewModel_RequestCancelPage(object sender, EventArgs e)
        {
            NavigationService.NavigateTo(ViewModelLocator.HOME_CONTAINER_KEY, null,
                new ActivityFlags[] { ActivityFlags.ClearTop, ActivityFlags.SingleTop });
        }

        void nextViewModel_RequestPostSaveReturnPage(object sender, EventArgs e)
        {
            NavigationService.NavigateTo(ViewModelLocator.HOME_CONTAINER_KEY, null,
                new ActivityFlags[] { ActivityFlags.ClearTop, ActivityFlags.SingleTop });
        }


        protected override ObservableCollection<ExtendedBaseListItemViewModel> ListDataSource
        {
            get
            {
                return _listDataSource = _listDataSource ?? new ObservableCollection<ExtendedBaseListItemViewModel>();
            }
            set { _listDataSource = value; }
        }

        protected override int MainImage
        {
            get { return Resource.Drawable.Med2; }
        }

        protected override BaseListViewModel ViewModel
        {
            get { return _viewModel; }
        }

        protected override void ViewModel_RequestPreviousPage(object sender, EventArgs e)
        {
            FragmentManager.PopBackStack(MyMedicationsFragment.MEDICATIONS_PROMPT_BACK_KEY,
                (int) PopBackStackFlags.Inclusive);
        }
    }
}