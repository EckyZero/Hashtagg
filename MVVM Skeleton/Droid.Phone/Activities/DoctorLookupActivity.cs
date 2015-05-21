using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Opengl;
using Android.OS;
using Android.Runtime;
using Android.Views;

using Shared.Common;
using Shared.VM;
using GalaSoft.MvvmLight.Helpers;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.Unity;
using Android.Widget;
using Droid.Phone.UIHelpers.ViewHolders;

namespace Droid.Phone.Activities
{
    [Activity(Label = "DoctorLookupActivity")]
    public class DoctorLookupActivity : GetConnectedLookupBaseActivity<Provider,PatientProvider>
    {
        private IDoctorLookupViewModel _viewModel;
        
        private DoctorLookupControllerParameters _arguments;

        protected override void OnCreate(Bundle bundle)
        {
            
            base.OnCreate(bundle);
        }

        private class ViewHolder : Java.Lang.Object, IViewHolder
        {
            public TextView Header { get; set; }
            public TextView Body { get; set; }
            public TextView Footer { get; set; }
        }

        protected override View GetProviderTemplate(int position, Provider provider, View convertView)
        {

            View rowView = convertView;

            ViewHolder viewHolder;

            if (rowView == null)
            {
                rowView = LayoutInflater.Inflate(Resource.Layout.GetConnectedLookupCell, null);

                viewHolder = new ViewHolder()
                {
                    Body = rowView.FindViewById<TextView>(Resource.Id.GetConnectedLookupBody),
                    Footer = rowView.FindViewById<TextView>(Resource.Id.GetConnectedLookupFooter),
                    Header = rowView.FindViewById<TextView>(Resource.Id.GetConnectedLookupHeader)
                };

                rowView.Tag = viewHolder;
            }
            else
            {
                viewHolder = rowView.Tag as ViewHolder;
            }

            viewHolder.Header.Text = provider.SpecialtiesDescription;
            viewHolder.Body.Text = provider.DoctorName;
            viewHolder.Footer.Text = provider.SingleLineAddress;

            return rowView;
        }

        protected override LookupViewModelBase<Provider,PatientProvider> ViewModel
        {
            get { return MainApplication.VMStore.DoctorLookupVM as DoctorLookupViewModel; }
        }
        
        protected override string ParameterQueryString
        {
            get { return _arguments == null ? string.Empty : _arguments.DoctorName;  }
        }

        protected override Action ParameterAction
        {
            get { return _arguments == null ? null : _arguments.OnPatientSelected; }
        }

        protected override void GetAndStoreParameters()
        {
            _arguments = _navigationService.GetAndRemoveParameter<DoctorLookupControllerParameters>(Intent);
        }

        protected override void InitAndStoreViewModel()
        {
            _viewModel = IocContainer.GetContainer().Resolve<IDoctorLookupViewModel>();
            MainApplication.VMStore.DoctorLookupVM = _viewModel;
        }

        protected override string QueryHint
        {
            get { return ViewModel.Placeholder; }
        }

        protected override Action<Provider> OnSelectAndReturn
        {
            get { return _arguments == null ? null : _arguments.OnPatientSelectedAndReturn; }
        }
    }

}