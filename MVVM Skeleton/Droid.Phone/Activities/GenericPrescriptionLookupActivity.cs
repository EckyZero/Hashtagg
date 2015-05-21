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
using Shared.Common;
using Shared.VM;

namespace Droid.Phone.Activities
{
    [Activity(Label = "GenericPrescriptionLookupActivity")]
    public class GenericPrescriptionLookupActivity : GenericLookupBaseActivity<Prescription,PatientPrescription>
    {
        private GenericPrescriptionLookupViewModel _viewModel;
        protected override Shared.VM.GenericLookupViewModelBase<Prescription, PatientPrescription> ViewModel
        {
            get { return _viewModel; }
        }

        protected override string ParameterQueryString
        {
            get { return !string.IsNullOrEmpty(_viewModel.QueryString) ? _viewModel.QueryString : ""; }
        }

        protected override string QueryHint
        {
            get { return _viewModel.Placeholder; }
        }

        private class ViewHolder : Java.Lang.Object
        {
            public TextView Header { get; set; }
            public TextView Body { get; set; }
            public TextView Footer { get; set; }
        }

        protected override View GetProviderTemplate(int position, Prescription prescription, View convertView)
        {

            View rowView = convertView;
            ViewHolder viewHolder;

            if (rowView == null)
            {
                rowView = LayoutInflater.Inflate(Resource.Layout.GetConnectedLookupCell, null);

                viewHolder = new ViewHolder()
                {
                    Header = rowView.FindViewById<TextView>(Resource.Id.GetConnectedLookupHeader),
                    Body = rowView.FindViewById<TextView>(Resource.Id.GetConnectedLookupBody),
                    Footer = rowView.FindViewById<TextView>(Resource.Id.GetConnectedLookupFooter)
                };
                
                rowView.Tag = viewHolder;
            }
            else
            {
                viewHolder = rowView.Tag as ViewHolder;
            }

            viewHolder.Header.Visibility = ViewStates.Gone;
            viewHolder.Body.Text = prescription.Name;
            viewHolder.Footer.Visibility = ViewStates.Gone;

            return rowView;
        }

        protected override void GetAndStoreParameters()
        {
            //Note that navigation/action after selecting is handed into the viewModel as an action
            _viewModel = _navigationService.GetAndRemoveParameter<GenericPrescriptionLookupViewModel>(Intent);
        }
    }
}