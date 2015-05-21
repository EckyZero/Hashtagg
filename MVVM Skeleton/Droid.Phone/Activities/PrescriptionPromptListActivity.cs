using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Droid.Phone.UIHelpers.ViewHolders;
using Shared.Common;
using Shared.VM;
using Microsoft.Practices.Unity;

namespace Droid.Phone.Activities
{
    [Activity(Label = "PrescriptionPromptListActivity")]
    public class PrescriptionPromptListActivity : GetConnectedPromptListBaseActivity<PatientPrescriptionViewModel, PatientPrescription>
    {
        private IPrescriptionPromptListViewModel _viewModel;

        protected override Shared.VM.Interfaces.IGetConnectedPromptListViewModel<PatientPrescriptionViewModel> ViewModel
        {
            get { return _viewModel as PrescriptionPromptListViewModel; }
        }

        protected override string TitleText
        {
            get { return "Great! Have Any Others?"; }
        }

        protected override string DescriptionText
        {
            get { return ""; }
        }

        protected override void InitAndStoreViewModel()
        {
            _viewModel = IocContainer.GetContainer().Resolve<IPrescriptionPromptListViewModel>();
            MainApplication.VMStore.PrescriptionPromptListVM = _viewModel;
        }

        private class ViewHolder : Java.Lang.Object, IViewHolder
        {
            public TextView Header { get; set; }
            public TextView Body { get; set; }
            public TextView Footer { get; set; }
        }

        protected override View SetupCell(int position, PatientPrescriptionViewModel cellViewModel, View convertView)
        {

            var patientPrescription = cellViewModel.Model;

            var rowView = convertView;

            ViewHolder viewHolder;

            if (rowView == null)
            {
                rowView = LayoutInflater.Inflate(Resource.Layout.GetConnectedPromptListCell, null);

                //Remove Unncesseary parts of view, do not track them as we never use them
                rowView.FindViewById<TextView>(Resource.Id.GetConnectedPromptListFooterTwo).Visibility = ViewStates.Gone;

                viewHolder = new ViewHolder()
                {
                    Header = rowView.FindViewById<TextView>(Resource.Id.GetConnectedPromptListHeader),
                    Body = rowView.FindViewById<TextView>(Resource.Id.GetConnectedPromptListBody),
                    Footer = rowView.FindViewById<TextView>(Resource.Id.GetConnectedPromptListFooter),
                };

                rowView.Tag = new CellDataHolder() { ViewHolder = viewHolder };
            }
            else
            {
                viewHolder = (rowView.Tag as CellDataHolder).ViewHolder as ViewHolder;
            }


            var header = string.Format("Medication {0}", position + 1);

				string footer;
				if (!String.IsNullOrWhiteSpace(patientPrescription.Dosage) && !String.IsNullOrWhiteSpace(patientPrescription.Frequency))
				{
					footer = string.Format("{0}, {1}", patientPrescription.Dosage, patientPrescription.Frequency);
				}
				else if (String.IsNullOrWhiteSpace(patientPrescription.Dosage) && !String.IsNullOrWhiteSpace(patientPrescription.Frequency))
				{
					footer = string.Format("{0}", patientPrescription.Frequency);
				}
				else if (!String.IsNullOrWhiteSpace(patientPrescription.Dosage) && String.IsNullOrWhiteSpace(patientPrescription.Frequency))
				{
					footer = string.Format("{0}", patientPrescription.Dosage);
				}
				else
				{
					footer = string.Empty;
				}

            viewHolder.Header.Text = header;
            viewHolder.Body.Text = patientPrescription.Name;
            viewHolder.Footer.Text = footer;
            
            return rowView;
        }

        protected override string AddAnotherText
        {
            get { return ApplicationResources.PrescriptionPromptListAddButtonTitle; }
        }

        public override void Dismiss()
        {
            _navigationService.NavigateTo(ViewModelLocator.PRESCRIPTION_PROMPT_VIEW_KEY,null, new []{ActivityFlags.SingleTop,ActivityFlags.ClearTop});
        }

        public override void GoBack()
        {
            _navigationService.NavigateTo(ViewModelLocator.PRESCRIPTION_PROMPT_VIEW_KEY, null, new []{ActivityFlags.SingleTop,ActivityFlags.ClearTop});
        }
    }
}