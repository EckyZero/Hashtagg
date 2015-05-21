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
    [Activity(Label = "GenericConditionsLookupActivity")]
    public class GenericConditionsLookupActivity : GenericLookupBaseActivity<Condition,PatientCondition>
    {

        private GenericConditionLookupViewModel _viewModel;

        protected override GenericLookupViewModelBase<Condition,PatientCondition> ViewModel
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

        protected override void GetAndStoreParameters()
        {
            //Note that navigation/action after selecting is handed into the viewModel as an action
            _viewModel = _navigationService.GetAndRemoveParameter<GenericConditionLookupViewModel>(Intent);
        }


        private class ViewHolder : Java.Lang.Object
        {
            public TextView Body { get; set; }
        }

        protected override View GetProviderTemplate(int position, Condition condition, View convertView)
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

            viewHolder.Body.Text = condition.Description;

            return rowView;
        }
    }
}