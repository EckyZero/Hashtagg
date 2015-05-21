using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Droid.Phone.Activities;
using Droid.Phone.UIHelpers.ViewHolders;
using Shared.BL;
using Fragment = Android.Support.V4.App.Fragment;

namespace Droid.Phone.Fragments
{
    public abstract class MyHealthDetailsBaseListFragment : BaseFragment
    {
        protected abstract BaseListViewModel ViewModel { get; }

        protected abstract ObservableCollection<ExtendedBaseListItemViewModel> ListDataSource { get; set; }

        protected abstract int MainImage { get; }

        private ViewGroup _container;

        private LayoutInflater _inflater;

        private bool _isFragmentResume;

        private ObservableAdapter<ExtendedBaseListItemViewModel> _adapter;

        //Full Layout

        public ListView BasePromptListView { get; private set; }

        private View _basePromptListMainView;

        private View _basePromptListHeaderView;

        private View _basePromptListFooterView;

        //Full Layout Ends

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            _container = container;
            _inflater = inflater;

            _basePromptListMainView = _inflater.Inflate(Resource.Layout.MyHealthDetails_BasePromptList, container, false);

            //Bind Controls and ViewModel to Class
            InitBindings();

            ViewShouldAppear();

            //Prepare and Bind ListView
            PrepareListView();

            //Bind Event to Listen to Data Changes from here on out
            ViewModel.Models.CollectionChanged += Models_CollectionChanged;

            ViewModel.RequestAddPage += ViewModel_RequestAddPage;

            ViewModel.RequestPreviousPage += ViewModel_RequestPreviousPage;

            return _basePromptListMainView;

        }

        protected abstract void ViewModel_RequestPreviousPage(object sender, EventArgs e);

        //Here we decide to go to the lookup page and assign what we want to do on select
        protected abstract void ViewModel_RequestAddPage(object sender, EventArgs e);

        protected virtual void OnSelect(IIdentifiable model) { }

        protected virtual void PostSelectionPage(IIdentifiable model) { }

        protected virtual void Models_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            RetrieveData();
        }

        private async void ReleaseData()
        {
            await ViewModel.WillDisappear();
        }

        public override void OnPause()
        {
            ReleaseData();

            ViewModel.Models.CollectionChanged -= Models_CollectionChanged;

            _isFragmentResume = true;

            base.OnPause();
        }

       
        protected virtual void RetrieveData()
        {
            if (_adapter != null)
            {
                _adapter.NotifyDataSetChanged();
            }
        }

        private async void ViewShouldAppear()
        {
            await ViewModel.WillAppear();

            RetrieveData();
        }

        public override void OnResume()
        {
            if (_isFragmentResume)
            {
                ViewShouldAppear();

                _isFragmentResume = false;

                ViewModel.Models.CollectionChanged += Models_CollectionChanged;
            }
            base.OnResume();
        }

        private void PrepareListView()
        {
            _adapter = new ObservableAdapter<ExtendedBaseListItemViewModel>()
            {
                DataSource = ListDataSource,
                GetTemplateDelegate = GetListTemplate
            };

            BasePromptListView.ItemClick += _basePromptListView_ItemClick;

            BasePromptListView.AddHeaderView(_basePromptListHeaderView);

            BasePromptListView.AddFooterView(_basePromptListFooterView);

            BasePromptListView.Adapter = _adapter;
        }

        void _basePromptListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e) {}

        private void InitBindings()
        {
            //Get the primary list view (Future procedures)
            BasePromptListView = _basePromptListMainView.FindViewById<ListView>(Resource.Id.MyHealthDetailsBasePromptListView);

            //Build the Header for the primary list view
            _basePromptListHeaderView = _inflater.Inflate(Resource.Layout.MyHealthDetails_BasePromptListHeader, null);

            //Get the header description field
            _basePromptListHeaderView.FindViewById<TextView>(Resource.Id.MyHealthDetailsBasePromptListHeaderDescription)
                .Text = ViewModel.Header;

            //Get the header image field
            _basePromptListHeaderView.FindViewById<ImageView>(Resource.Id.MyHealthDetailsBasePromptListHeaderMainImage).SetImageResource(MainImage);

            //Build the footer for the primary list view
            _basePromptListFooterView = _inflater.Inflate(Resource.Layout.MyHealthDetails_BasePromptListFooter, null);
        }

        private View GetListTemplate(int position, ExtendedBaseListItemViewModel cellViewModel, View convertView)
        {
            var rowView = convertView;
            rowView = SetupCell(position, cellViewModel, rowView);

            if (rowView.Id == Resource.Id.GetConnectedPromptListCellMainLayout)
            {

                (rowView.Tag as CellDataHolder).Tag = position.ToString();

                rowView.Click -= dots_Click;
                rowView.Click += dots_Click;

            }
            return rowView;    
            
        }

        void dots_Click(object sender, EventArgs e)
        {
            var rowView = sender as RelativeLayout;
            var position = -1;
            int.TryParse((rowView.Tag as CellDataHolder).Tag, out position);
            var cellViewModel = ListDataSource[position];
            var dialog = new Dialog(_container.Context);
            dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            View dialogView = _inflater.Inflate(Resource.Layout.GetConnectedPromptListDialog, null);
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
                var builder = new AlertDialog.Builder(_container.Context)
                    .SetMessage(ApplicationResources.RemoveAlertMessage)
                    .SetNegativeButton(ApplicationResources.Remove, (s, a) =>
                    {
                        cellViewModel.DeleteCommand.Execute(null);
                    })
            .SetPositiveButton(ApplicationResources.Cancel, (s, a) => { })
                    .SetTitle(ApplicationResources.RemoveAlertTitle);

                dialog.Dismiss();

                var alert = builder.Create();

                alert.Show();

                alert.GetButton((int)DialogButtonType.Negative).SetTextColor(Color.Red);
            };

            dialog.FindViewById<Button>(Resource.Id.GetConnectedPromptListDialogCancelButton).Click +=
                (o, eventArgs) =>
                {
                    dialog.Dismiss();
                };

            dialog.Window.SetBackgroundDrawable(new ColorDrawable(Android.Graphics.Color.Transparent));

            dialog.Show();
        }
        protected abstract View SetupCell(int position, ExtendedBaseListItemViewModel cellViewModel, View convertView);
    }
}

