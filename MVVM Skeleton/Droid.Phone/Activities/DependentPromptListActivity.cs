using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
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
    [Activity(Label = "DependentPromptListActivity")]
    public class DependentPromptListActivity : GetConnectedPromptListBaseActivity<DependentViewModel, Dependent>
    {
        private IDependentListViewModel _viewModel;

        protected override Shared.VM.Interfaces.IGetConnectedPromptListViewModel<DependentViewModel> ViewModel
        {
            get { return _viewModel as DependentListViewModel; }
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
            _viewModel = IocContainer.GetContainer().Resolve<IDependentListViewModel>();
            MainApplication.VMStore.DependentListVM = _viewModel;
        }

        protected override string AddAnotherText
        {
            get { return "Add another dependent"; }
        }

        public override void Dismiss()
        {
            _navigationService.NavigateTo(ViewModelLocator.DEPENDENTS_PROMPT_VIEW_KEY, null, new[] { ActivityFlags.SingleTop, ActivityFlags.ClearTop });
        }

        public override void GoBack()
        {
            _navigationService.NavigateTo(ViewModelLocator.DEPENDENTS_PROMPT_VIEW_KEY, null, new[] { ActivityFlags.SingleTop, ActivityFlags.ClearTop });
        }

        private class ViewHolder : Java.Lang.Object, IViewHolder
        {
            public TextView Name { get; set; }
            public TextView Relationship { get; set; }
            public TextView Email { get; set; }
            public TextView Birthdate { get; set; }
            public TextView Gender { get; set; }
        }

        protected override View SetupCell(int position, DependentViewModel cellViewModel, View convertView)
        {
            var dependent = cellViewModel.Model;

            var rowView = convertView;

            ViewHolder viewHolder;

            if (rowView == null)
            {
                rowView = LayoutInflater.Inflate(Resource.Layout.DependentsPromptListCell, null);
                viewHolder = new ViewHolder()
                {
                    Name = rowView.FindViewById<TextView>(Resource.Id.DependentPromptListCellDependentName),
                    Relationship = rowView.FindViewById<TextView>(Resource.Id.DependentsPromptListCellRelationship),
                    Email = rowView.FindViewById<TextView>(Resource.Id.DependentPromptListCellEmailAddress),
                    Birthdate = rowView.FindViewById<TextView>(Resource.Id.DependentPromptListCellLongBirthdate),
                    Gender = rowView.FindViewById<TextView>(Resource.Id.DependentPromptListCellGender)
                };
                rowView.Tag = new CellDataHolder() {ViewHolder = viewHolder};
            }
            else
            {
                viewHolder = (rowView.Tag as CellDataHolder).ViewHolder as ViewHolder;
            }

            viewHolder.Name.Text = String.Format("{0} {1}", dependent.FirstName, dependent.LastName);
            viewHolder.Relationship.Text = dependent.Relationship;
            viewHolder.Email.Text = dependent.Username;
            viewHolder.Birthdate.Text = dependent.DateOfBirth != null
                ? dependent.DateOfBirth.Value.ToString("MMMM dd, yyyy")
                : String.Empty;
            viewHolder.Gender.Text = dependent.Gender;

            return rowView;
        }
    }
}