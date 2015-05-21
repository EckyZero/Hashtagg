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
using Microsoft.Practices.Unity;

namespace Droid.Phone.Activities
{
    [Activity(Label = "PrescriptionLookupActivity")]
    public class PrescriptionLookupActivity : GetConnectedLookupBaseActivity<Prescription,PatientPrescription>
    {
        private IPrescriptionLookupViewModel _viewModel;
        private PrescriptionInformationControllerParameters _parameters;

        protected override Shared.VM.LookupViewModelBase<Prescription,PatientPrescription> ViewModel
        {
            get { return _viewModel as PrescriptionLookupViewModel; }
        }

        protected override string ParameterQueryString
        {
            get { return ""; }
        }

        protected override Action ParameterAction
        {
            get { return () => { }; }
        }

        protected override string QueryHint
        {
            get { return ViewModel.Placeholder; }
        }

        protected override void InitAndStoreViewModel()
        {
            _viewModel = IocContainer.GetContainer().Resolve<IPrescriptionLookupViewModel>();
            MainApplication.VMStore.PrescriptionLookupVM = _viewModel;
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
            //No Parameters
        }

        protected override Action<Prescription> OnSelectAndReturn
        {
            get { return null; }
        }
    }
}