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
using Shared.VM.Interfaces;
using Microsoft.Practices.Unity;
using Object = Java.Lang.Object;

namespace Droid.Phone.Activities
{
    [Activity(Label = "ProcedurePromptListActivity")]
    public class ProcedurePromptListActivity : GetConnectedPromptListBaseActivity<PatientProcedureViewModel, PatientProcedure>
    {
        private IProcedurePromptListViewModel _viewModel;

        protected override IGetConnectedPromptListViewModel<PatientProcedureViewModel> ViewModel
        {
            get { return _viewModel as ProcedurePromptListViewModel; }
        }

        protected override string TitleText
        {
            get { return ApplicationResources.GreatHaveAnyOthers; }
        }

        protected override string DescriptionText
        {
            get { return String.Empty; }
        }

        protected override void InitAndStoreViewModel()
        {
            _viewModel = IocContainer.GetContainer().Resolve<IProcedurePromptListViewModel>();
            MainApplication.VMStore.ProcedurePromptListVM = _viewModel;
        }

        private class ViewHolder : Java.Lang.Object, IViewHolder
        {
            public TextView Header { get; set; }
            public TextView Body { get; set; }
            public TextView Footer { get; set; }
        }

        protected override View SetupCell(int position, PatientProcedureViewModel cellViewModel, View convertView)
        {

            var patientProcedure = cellViewModel.Model;

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
                rowView.Tag = new CellDataHolder() {ViewHolder = viewHolder};
            }
            else
            {
                viewHolder = (rowView.Tag as CellDataHolder).ViewHolder as ViewHolder;
            }

            viewHolder.Header.Text = String.Format("Procedure {0}", position + 1);
            viewHolder.Body.Text = patientProcedure.Description;
            viewHolder.Footer.Text = String.Format("{0}", patientProcedure.Date.ToCompassDate());
         
            return rowView;
        }
        protected override string AddAnotherText
        {
            get { return ApplicationResources.AddAnotherProcedure; }
        }

        public override void Dismiss()
        {
            _navigationService.NavigateTo(ViewModelLocator.PROCEDURE_PROMPT_VIEW_KEY, null, new[] { ActivityFlags.SingleTop, ActivityFlags.ClearTop });
        }

        public override void GoBack()
        {
            _navigationService.NavigateTo(ViewModelLocator.PROCEDURE_PROMPT_VIEW_KEY, null, new[] { ActivityFlags.SingleTop, ActivityFlags.ClearTop });
        }
    }
}