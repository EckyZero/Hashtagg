using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Droid.Phone.UIHelpers.ViewHolders;
using Shared.Common;
using Shared.VM;
using GalaSoft.MvvmLight.Helpers;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.Unity;

namespace Droid.Phone.Activities
{
    [Activity(Label = "DoctorPromptListActivity")]
    public class DoctorPromptListActivity : GetConnectedPromptListBaseActivity<PatientProviderViewModel, PatientProvider>
    {
        private IDoctorPromptListViewModel _viewModel;

        protected override Shared.VM.Interfaces.IGetConnectedPromptListViewModel<PatientProviderViewModel> ViewModel
        {
            get { return _viewModel as DoctorPromptListViewModel; }
        }

        protected override string TitleText
        {
            get { return ApplicationResources.GreatHaveAnyOthers; }
        }

        protected override string DescriptionText
        {
            get { return "Dentist, Eye Doctor or any other specialists"; }
        }

        protected override void InitAndStoreViewModel()
        {
            _viewModel = IocContainer.GetContainer().Resolve<IDoctorPromptListViewModel>();
            MainApplication.VMStore.DoctorPromptListVM = _viewModel;
        }

        private class ViewHolder : Java.Lang.Object, IViewHolder
        {
            public TextView Header { get; set; }
            public TextView Body { get; set; }
            public TextView Footer { get; set; }
            public TextView SecondFooter { get; set; }
        }

        protected override View SetupCell(int position, PatientProviderViewModel cellViewModel, View convertView)
        {


            var patientProvider = cellViewModel.Model;

            var rowView = convertView;

            ViewHolder viewHolder;

            if (rowView == null)
            {
                rowView = LayoutInflater.Inflate(Resource.Layout.GetConnectedPromptListCell, null);

                viewHolder = new ViewHolder()
                {
                    Header = rowView.FindViewById<TextView>(Resource.Id.GetConnectedPromptListHeader),
                    Body = rowView.FindViewById<TextView>(Resource.Id.GetConnectedPromptListBody),
                    Footer = rowView.FindViewById<TextView>(Resource.Id.GetConnectedPromptListFooter),
                    SecondFooter = rowView.FindViewById<TextView>(Resource.Id.GetConnectedPromptListFooterTwo)
                };

                rowView.Tag = new CellDataHolder() {ViewHolder = viewHolder};
            }
            else
            {
                viewHolder = (rowView.Tag as CellDataHolder).ViewHolder as ViewHolder;
            }

            viewHolder.Header.Text = patientProvider.SpecialtiesDescription;
            viewHolder.Body.Text = patientProvider.Name;
            viewHolder.Footer.Text = patientProvider.AddressDisplayLine;
            viewHolder.SecondFooter.Text = patientProvider.CityStateDisplayLine;

            return rowView;
        }

        protected override string AddAnotherText
        {
            get { return "Add another specialist"; }
        }

        public override void Dismiss()
        {
            _navigationService.NavigateTo(ViewModelLocator.DOCTOR_PROMPT_VIEW_KEY, null,new ActivityFlags[] {ActivityFlags.SingleTop, ActivityFlags.ClearTop});
        }

        public override void GoBack()
        {
            _navigationService.NavigateTo(ViewModelLocator.DOCTOR_PROMPT_VIEW_KEY, null, new ActivityFlags[] {ActivityFlags.SingleTop, ActivityFlags.ClearTop });
        }
    }
}