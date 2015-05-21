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
    [Activity(Label = "ProcedureLookupActivity")]
    public class ProcedureLookupActivity : GetConnectedLookupBaseActivity<Procedure, PatientProcedure>
    {
        private IProcedureLookupViewModel _viewModel;
        private ProcedureInformationControllerParameters _parameters;

        protected override LookupViewModelBase<Procedure, PatientProcedure> ViewModel
        {
            get { return _viewModel as ProcedureLookupViewModel; }
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
            _viewModel = IocContainer.GetContainer().Resolve<IProcedureLookupViewModel>();
            MainApplication.VMStore.ProcedureLookupVM = _viewModel;
        }

        private class ViewHolder: Java.Lang.Object
        {
            public TextView Body { get; set; }
        }

        protected override View GetProviderTemplate(int position, Procedure procedure, View convertView)
        {

            View rowView = convertView;
            ViewHolder viewHolder;

            if (rowView == null)
            {
                rowView = LayoutInflater.Inflate(Resource.Layout.GetConnectedLookupCell, null);
                rowView.FindViewById<TextView>(Resource.Id.GetConnectedLookupFooter).Visibility = ViewStates.Gone;
                rowView.FindViewById<TextView>(Resource.Id.GetConnectedLookupHeader).Visibility = ViewStates.Gone;

                viewHolder = new ViewHolder()
                {
                    Body = rowView.FindViewById<TextView>(Resource.Id.GetConnectedLookupBody)
                };
                rowView.Tag = viewHolder;
            }
            else
            {
                viewHolder = rowView.Tag as ViewHolder;
            }

            viewHolder.Body.Text = procedure.Description;

            return rowView;
        }

        protected override void GetAndStoreParameters()
        {
            //No Parameters
        }

        protected override Action<Procedure> OnSelectAndReturn
        {
            get { return null; }
        }
    }
}