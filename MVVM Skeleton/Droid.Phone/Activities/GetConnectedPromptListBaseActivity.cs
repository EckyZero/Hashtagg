using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Shared.Common;
using Shared.VM;
using Shared.VM.Interfaces;
using GalaSoft.MvvmLight.Helpers;
using Microsoft.Practices.Unity;
using Android.Graphics;
using Droid.Phone.UIHelpers.ViewHolders;

namespace Droid.Phone.Activities
{
    //T = PatientProviderViewModel, // R = PatientProvider
    public abstract class GetConnectedPromptListBaseActivity<T,R> : BaseActivity where T : EditableViewModelBase<R>
    {
        protected abstract IGetConnectedPromptListViewModel<T> ViewModel { get; }

        protected abstract string TitleText { get; }

        protected abstract string DescriptionText { get; }

        protected abstract string AddAnotherText { get; }

        private ListView _listView;

        private View _header;

        private TextView _headerTitle;

        private TextView _headerDescription;


        private ProgressBar _progressBar;

        private View _footer;

        private View _addAnotherButton;

        private Button _nextButton;
        private TextView _addAnotherText;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.GetConnectedPromptList);

            //Bind Controls and ViewModel to Class
            InitBindings();

            //Prepare and Bind ListView
            PrepareListView();

            //Prepare and Bind Buttons
            PrepareButtons();

            // Set screen to match data
            SetupScreen();
        }

        protected override void OnPause()
        {
            ViewModel.Unsubscribe();
            base.OnPause();
        }

        private void SetupScreen()
        {
            // set images and textviews to properties
            _headerTitle.Text = TitleText;
            _headerDescription.Text = DescriptionText;
        }

        protected override void OnResume()
        {
            ViewModel.Subscribe();
            base.OnResume();
        }

        private void PrepareListView()
        {
            ObservableAdapter<T> adapter = new ObservableAdapter<T>()
            {
                DataSource = ViewModel.LookupData,
                GetTemplateDelegate = GetPromptListTemplate
            };

            _listView.AddHeaderView(_header);
            _listView.AddFooterView(_footer);

            _listView.Adapter = adapter;
        }

        private void PrepareButtons()
        {
            _nextButton.SetCommand("Click", ViewModel.NextCommand);

            _addAnotherButton.SetCommand("Click", ViewModel.AddCommand);
        }

        protected abstract void InitAndStoreViewModel();

        private void InitBindings()
        {
            InitAndStoreViewModel();
            _listView = FindViewById<ListView>(Resource.Id.GetConnectedPromptListView);

            _header = LayoutInflater.Inflate(Resource.Layout.GetConnectedPromptListHeader, null);
            _headerTitle = _header.FindViewById<TextView>(Resource.Id.GetConnectedPromptListTitle);
            _headerDescription = _header.FindViewById<TextView>(Resource.Id.GetConnectedPromptListDescription);
            _progressBar = _header.FindViewById<ProgressBar>(Resource.Id.GetConnectedPromptListProgressBar);

            _footer = LayoutInflater.Inflate(Resource.Layout.GetConnectedPromptListFooter, null);
            _addAnotherButton = _footer.FindViewById<View>(Resource.Id.GetConnectedPromptListAddMoreLayout);
            _addAnotherText = _addAnotherButton.FindViewById<TextView>(Resource.Id.GetConnectedAddAnotherTextView);
            _addAnotherText.Text = AddAnotherText;
            _nextButton = _footer.FindViewById<Button>(Resource.Id.GetConnectedPromptListNextButton);

            _progressBar.Progress = (int)Math.Round(ViewModel.Progress * 100);

            ViewModel.LookupData.CollectionChanged += ViewModelLookupDataCollectionChanged;
        }
 
        private void ViewModelLookupDataCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            _nextButton.RequestFocus();
        }

        protected abstract View SetupCell(int position, T cellViewModel, View convertView);

        private View GetPromptListTemplate(int position, T cellViewModel, View convertView)
        {
            var rowView = convertView;
            rowView = SetupCell(position,cellViewModel, rowView);

            (rowView.Tag as CellDataHolder).Tag = position.ToString();

            rowView.Click -= dots_Click;
            rowView.Click += dots_Click;
            
            return rowView;
        }



        void dots_Click(object sender, EventArgs e)
        {
            var cellMainLayout = sender as RelativeLayout;
            int position = -1;
            int.TryParse((cellMainLayout.Tag as CellDataHolder).Tag, out position);
            var cellViewModel = ViewModel.LookupData[position];
            var dialog = new Dialog(this);
            dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            View dialogView = LayoutInflater.Inflate(Resource.Layout.GetConnectedPromptListDialog, null);
            dialog.SetContentView(dialogView);

            var editButton = dialog.FindViewById<Button>(Resource.Id.GetConnectedPromptListDialogEditButton);
            editButton.Click += (o, eventArgs) =>
            {
                cellViewModel.EditCommand.Execute(null);
                dialog.Dismiss();
            };

            var deleteButton = dialog.FindViewById<Button>(Resource.Id.GetConnectedPromptListDialogDeleteButton);
            deleteButton.Click += (o, eventArgs) =>
            {
                var builder = new AlertDialog.Builder(this)
                    .SetMessage(ApplicationResources.RemoveAlertMessage)
                    .SetNegativeButton(ApplicationResources.Remove, (s, a) => cellViewModel.DeleteCommand.Execute(null))
                    .SetPositiveButton(ApplicationResources.Cancel, (s, a) => { })
                    .SetTitle(ApplicationResources.RemoveAlertTitle);

                dialog.Dismiss();

                var alert = builder.Create();

                alert.Show();

                alert.GetButton((int)DialogButtonType.Negative).SetTextColor(Color.Red);
            };

            dialog.FindViewById<Button>(Resource.Id.GetConnectedPromptListDialogCancelButton).Click +=
                (o, eventArgs) =>                {
                    dialog.Dismiss();
                };

            dialog.Window.SetBackgroundDrawable(new ColorDrawable(Android.Graphics.Color.Transparent));

            dialog.Show();
        }

        //Dissable Hardware Back
        public override void OnBackPressed() { }
        
    }


}