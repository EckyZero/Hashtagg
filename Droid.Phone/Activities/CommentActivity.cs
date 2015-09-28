﻿
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
using Shared.VM;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Helpers;
using Toolbar = Android.Support.V7.Widget.Toolbar;
namespace Droid.Phone
{
    [Activity(Label = "CommentActivity")]			
    public class CommentActivity : ActionBarBaseActivity
    {
        private CommentViewModel _viewModel;

        private ListView _commentsList;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Comments);

            SetSupportActionBar(FindViewById<Toolbar>(Resource.Id.CommentsToolbar));
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            _commentsList = FindViewById<ListView>(Resource.Id.CommentsListView);

            _commentsList.Adapter = new ObservableAdapter<IListItem>()
            {
                    DataSource = _viewModel.CardViewModels,
                    GetTemplateDelegate = GetCell,
            };
        }

        private View GetCell(int position, IListItem cVm, View convertView)
        {
            var a = cVm;
            return LayoutInflater.Inflate(Resource.Layout.DefaultCell, null, false);
        }
    }
}

